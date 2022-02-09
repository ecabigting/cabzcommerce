
using cabzcommerce.api.Repositories;
using cabzcommerce.cshared.DTOs;
using cabzcommerce.cshared.DTOs.User;
using cabzcommerce.cshared.Models;
using BCryptNet = BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace cabzcommerce.api.Controllers 
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ApiBaseController 
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
                User FoundUserByEmail = await repo.GetUserByEmail(_user.Email);
                if(FoundUserByEmail == null)
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

        [HttpGet("RefreshToken/{RefToken}")]
        public async Task<ActionResult<ApiResponse>> RefreshToken([FromHeader]string Authorization,Guid RefToken)
        {
            //
            // Check if there is a Bearer Token
            if(BearerTokenExist(Authorization)) ReturnInvalidBearerTokenResponse();

            string userToken = Authorization.Split(' ')[1];

            //
            // Check if the Bearer token exist even if it is expired
            if(await repo.GetUserAccessByCurrentToken(userToken)==null)
            {
                return BadRequest(new ApiResponse{
                    Data = null,
                    ErrorMessage = "Invalid Bearer Token!",
                    Message = "Failed to get new Token!",
                    StatusCode = BadRequest().StatusCode
                });
            }

            //
            // try to get a new Token with a valid refresh token
            UserAccessToken NewUAT = await repo.RefreshToken(RefToken,userToken);
            if(NewUAT == null)
            {
                return Unauthorized(new ApiResponse{
                    Data = null,
                    ErrorMessage = "Refresh Token Expired! Please login!",
                    Message = "Failed to get new Token!",
                    StatusCode = Unauthorized().StatusCode
                });
            }else
            {
                string successMsg = "";
                if(RefToken == NewUAT.RefreshToken)
                    successMsg = "Old Token is not yet expired! Please generated a new one when it expires!";
                else 
                    successMsg = "Getting new token success!";
                return Ok(new ApiResponse{
                    Data = new Access {
                        RefreshToken = NewUAT.RefreshToken,
                        RefreshTokenExp = NewUAT.RefreshTokenExp,
                        Token = NewUAT.Token,
                        TokenExp = NewUAT.TokenExp
                    },
                    ErrorMessage = "",
                    Message = successMsg,
                    StatusCode = Ok().StatusCode
                });
            }
        }
    }
}