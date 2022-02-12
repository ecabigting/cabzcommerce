
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
    public class ProductController : ApiBaseController 
    {
        private readonly IProductRepo repo;
        
        public ProductController(IProductRepo _pRepo)
        {
            repo = _pRepo;
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        
    }
}