using AtsCommerce.Core.CQRS.Commands;
using AtsCommerce.Core.CQRS.Queries;
using Microsoft.AspNetCore.Mvc;
using ProductsService.Dtos;
using System.Threading;

namespace ProductsService.Controllers
{
    [Route("~/product-api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IQueryBus _queryBus;
        public ProductsController(IQueryBus queryBus)
        {
            _queryBus = queryBus;
        }

        [HttpGet]
        public async Task<ProductsResult> Products([FromQuery] ProductsQuery queryRequest)
        {
            var result = await _queryBus.Send(queryRequest);

            return result;
        }
    }
}
