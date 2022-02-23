
using cabzcommerce.api.Repositories;
using Microsoft.AspNetCore.Mvc;

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