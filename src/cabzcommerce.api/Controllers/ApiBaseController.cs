using cabzcommerce.api.Repositories;
using cabzcommerce.cshared.DTOs;
using cabzcommerce.cshared.Models;
using Microsoft.AspNetCore.Mvc;

namespace cabzcommerce.api.Controllers 
{
    public class ApiBaseController : ControllerBase 
    {
        public ApiBaseController(){}

        public static bool BearerTokenExist(string token)
        {
            return String.IsNullOrEmpty(token == null ? "" : token.Split(' ').Length > 1 ? token.Split(' ')[1] : "" );
        }

        public ActionResult<ApiResponse> ReturnInvalidBearerTokenResponse()
        {
            return BadRequest(new ApiResponse{
                    Data = null,
                    ErrorMessage = "Invalid Bearer Token!",
                    Message = "Failed to get new Token!",
                    StatusCode = BadRequest().StatusCode
                });
        }

        public async Task<Guid> GetTokenUserID(IUserRepo repo, string Token)
        {
            UserAccessToken uat = await repo.GetUserAccessByCurrentToken(Token);
            return uat.UserId;
        } 
        
    }
}