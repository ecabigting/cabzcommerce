
using cabzcommerce.api.Repositories;
using cabzcommerce.cshared.DTOs;
using cabzcommerce.cshared.DTOs.Product;
using cabzcommerce.cshared.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cabzcommerce.api.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
    public class BrandController : ApiBaseController 
    {
        private readonly IBrandRepo brepo;
        private readonly IUserRepo urepo;
        
        public BrandController(IBrandRepo _bRepo,IUserRepo _uRepo)
        {
            brepo = _bRepo;
            urepo = _uRepo;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ApiResponse>> GetBrandById(Guid Id)
        {
            try
            {
                Brand FoundBrand = await brepo.GetBrandByID(Id);
                if(FoundBrand != null)
                {
                    return Ok(new ApiResponse{
                        Data = FoundBrand,
                        ErrorMessage = "",
                        Message = "Fetched Brand successfully!",
                        StatusCode = Ok().StatusCode
                    });
                }else
                {
                    return NotFound(new ApiResponse{
                        Data = null,
                        ErrorMessage = "",
                        Message = "Brand not found!",
                        StatusCode = NotFound().StatusCode
                    });
                }   

            }catch(Exception err)
            {
                return BadRequest(new ApiResponse {
                    Data = null,
                    ErrorMessage = err.Message,
                    StatusCode = BadRequest().StatusCode,
                    Message = "Error!"
                });
            }
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetAllBrands()
        {
            try
            {
                return Ok(new ApiResponse{
                    Data = await brepo.GetAllBrands(),
                    ErrorMessage = "",
                    Message = "Listing all Brands successfully!",
                    StatusCode = Ok().StatusCode
                });   

            }catch(Exception err)
            {
                return BadRequest(new ApiResponse {
                    Data = null,
                    ErrorMessage = err.Message,
                    StatusCode = BadRequest().StatusCode,
                    Message = "Error!"
                });
            }
        }
        
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> Add([FromHeader]string Authorization,BrandDto brand)
        {
            try
            {
                //
                // check if brand name already exist.
                if(await brepo.BrandNameExist(brand.Name))
                {
                    return BadRequest(new ApiResponse {
                        Data = null,
                        ErrorMessage = "Brand NAME already exist!",
                        StatusCode = BadRequest().StatusCode,
                        Message = "Error!"
                    });
                }

                //
                // get the user id via access token
                UserAccessToken UAT = await urepo.GetUserAccessByCurrentToken(Authorization.Split(' ')[1]);

                //
                // fill out server side data
                Brand NewBrand = new Brand{
                    CreatedBy = UAT.UserId,
                    CreatedDateTime = DateTimeOffset.Now,
                    IsEnabled = true,
                    UpdatedBy = UAT.UserId,
                    IsEnabledBy = UAT.UserId,
                    UpdatedDateTime = DateTimeOffset.Now,
                    Name = brand.Name,
                    Description = brand.Desc
                };

                //
                // add the new brand and return
                return Ok(new ApiResponse{
                    Data = await brepo.Add(NewBrand),
                    ErrorMessage = "",
                    Message = "New brand added successfully!",
                    StatusCode = Ok().StatusCode
                });

            }catch(Exception err)
            {
                return BadRequest(new ApiResponse {
                    Data = null,
                    ErrorMessage = err.Message,
                    StatusCode = BadRequest().StatusCode,
                    Message = "Error!"
                });
            }
        }
        
        [HttpPut("{Id}")]
        public async Task<ActionResult<ApiResponse>> Update([FromHeader]string Authorization,BrandDto brand,Guid Id)
        {
            try
            {
                //
                // Get ExistingBrand
                Brand ExistingBrand = await brepo.GetBrandByID(Id);

                // check if brand name already exist.
                // and if that brand name is not 
                // the current existing one you are trying to update
                if(await brepo.CheckBrandNameWithId(brand.Name,ExistingBrand.Id))
                {
                    return BadRequest(new ApiResponse {
                        Data = null,
                        ErrorMessage = "Brand NAME already exist!",
                        StatusCode = BadRequest().StatusCode,
                        Message = "Error!"
                    });
                }

                //
                // get the user id via access token
                UserAccessToken UAT = await urepo.GetUserAccessByCurrentToken(Authorization.Split(' ')[1]);

                //
                // fill out server side data
                ExistingBrand.UpdatedBy = UAT.UserId;
                ExistingBrand.IsEnabledBy = UAT.UserId;
                ExistingBrand.UpdatedDateTime = DateTimeOffset.Now;
                ExistingBrand.Name = brand.Name;
                ExistingBrand.Description = brand.Desc;
                
                //
                // add the new brand and return
                return Ok(new ApiResponse{
                    Data = await brepo.Update(ExistingBrand),
                    ErrorMessage = "",
                    Message = "Brand updated successfully!",
                    StatusCode = Ok().StatusCode
                });

            }catch(Exception err)
            {
                return BadRequest(new ApiResponse {
                    Data = null,
                    ErrorMessage = err.Message,
                    StatusCode = BadRequest().StatusCode,
                    Message = "Error!"
                });
            }
        }
    }
}