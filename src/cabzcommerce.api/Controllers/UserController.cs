
using cabzcommerce.api.Repositories;
using cabzcommerce.cshared.DTOs;
using cabzcommerce.cshared.DTOs.User;
using cabzcommerce.cshared.Models;
using BCryptNet = BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace cabzcommerce.api.Controllers 
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase 
    {
        private readonly IUserRepo repo;
        
        public UserController(IUserRepo _uRepo)
        {
            repo = _uRepo;
        }

        // [HttpGet]
        // public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        // {
        //     var items = (await repo.GetItemsAsync())
        //                 .Select(i => i.AsDto());
        //     return items;
        // }

        // [HttpGet("{Id}")]
        // public async Task<ActionResult<ItemDto>> GetItemAsync(Guid Id)
        // {
        //     var item = await repo.GetItemAsync(Id);
        //     if(item is null)
        //     {
        //         return NotFound();
        //     }
        //     return Ok(item.AsDto());
        // }

        [HttpGet("GetUserProfile/{Id}")]
        public async Task<ActionResult<ApiResponse>> GetUserProfile(Guid Id)
        {
            try
            {
                User FoundUser = await repo.GetUser(Id);
                if(FoundUser != null)
                {
                    return new ApiResponse {
                        Data = new Profile {
                            DateOfBirth = FoundUser.DateOfBirth,
                            Email = FoundUser.Email,
                            FirstName = FoundUser.FirstName,
                            LastName=FoundUser.LastName,
                            PhoneNumber=FoundUser.PhoneNumber,
                            UserAccess=null,
                            UserType=FoundUser.UserType
                        },
                        ErrorMessage = "",
                        Message = "Success!",
                        StatusCode = NotFound().StatusCode
                    };
                }else
                {
                    return new ApiResponse {
                        Data = null,
                        ErrorMessage = "User Not found!",
                        StatusCode = 200,
                        Message = "Invalid User Id!"
                    };
                }

            }catch(Exception err)
            {
                return new ApiResponse {
                    Data = null,
                    ErrorMessage = err.Message,
                    StatusCode = 500,
                    Message = "Error!"
                };
            }
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ApiResponse>> Register(Registration _user)
        {
            try
            {
                if(repo.GetUserByEmail(_user.Email)==null)
                {
                    return Ok(new ApiResponse {
                        Data = await repo.Register(_user),
                        ErrorMessage = "",
                        StatusCode = Ok().StatusCode,
                        Message = "Registration Successful!"
                    });
                }else
                {
                    return BadRequest(new ApiResponse {
                        Data = null,
                        ErrorMessage = "Email Already exist!",
                        StatusCode = BadRequest().StatusCode,
                        Message = "Error!"
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

        [HttpPost("Login")]
        public async Task<ActionResult<ApiResponse>> Login(Login _user)
        {
            try
            {
                User FoundUserByEmail = await repo.GetUserByEmail(_user.Username);
                if(FoundUserByEmail != null)
                {
                    if(BCryptNet.Verify(_user.Password,FoundUserByEmail.Password))
                    {
                        UserAccessToken UAToken = await repo.GrantUserAccess(FoundUserByEmail);
                        return new ApiResponse {
                            Data = new Profile {
                                DateOfBirth = FoundUserByEmail.DateOfBirth,
                                Email = FoundUserByEmail.Email,
                                FirstName = FoundUserByEmail.FirstName,
                                LastName=FoundUserByEmail.LastName,
                                PhoneNumber=FoundUserByEmail.PhoneNumber,
                                UserAccess= new Access {
                                    RefreshToken = UAToken.RefreshToken,
                                    RefreshTokenExp = UAToken.RefreshTokenExp,
                                    Token = UAToken.Token,
                                    TokenExp = UAToken.TokenExp
                                },
                                UserType=FoundUserByEmail.UserType
                            },
                            ErrorMessage = "",
                            Message = "Success!",
                            StatusCode = Ok().StatusCode
                        };
                    }else
                    {
                        return BadRequest(new ApiResponse {
                            Data = null,
                            ErrorMessage = "Invali Username/Password",
                            StatusCode = BadRequest().StatusCode,
                            Message = "Error!"
                        });
                    }
                }else
                {
                    return BadRequest(new ApiResponse {
                        Data = null,
                        ErrorMessage = "Invali Username/Password",
                        StatusCode = BadRequest().StatusCode,
                        Message = "Login error!"
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
    
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("CheckToken")]
        public ActionResult CheckToken()
        {
            return Ok("Authenticated!");
        }
    }
}