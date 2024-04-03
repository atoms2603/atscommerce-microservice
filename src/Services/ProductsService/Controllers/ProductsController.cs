using Microsoft.AspNetCore.Mvc;

namespace ProductsService.Controllers
{
    [Route("~/product-api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : Controller
    {
        public ProductsController()
        {
        }

        [HttpGet]
        public string Products()
        {
            return "This is product controller";
        }
    }
}
