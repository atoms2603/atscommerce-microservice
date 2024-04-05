using AtsCommerce.Core.CQRS.Queries;
using Infrastructure.Core.Commands;
using Microsoft.AspNetCore.Mvc;
using ProductsService.Commands;
using ProductsService.Dtos;
using ProductsService.Queries;

namespace ProductsService.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IQueryBus _queryBus;
        private readonly ICommandBus _commandBus;
        public ProductsController(IQueryBus queryBus, ICommandBus commandBus)
        {
            _queryBus = queryBus;
            _commandBus = commandBus;
        }

        [HttpGet]
        public async Task<ProductsResult> Products([FromQuery] ProductsQuery queryRequest)
        {
            var result = await _queryBus.Send(queryRequest);

            return result;
        }

        [HttpGet("{id}")]
        public async Task<ProductByIdResult> ProductById([FromRoute] Guid id)
        {
            var queryRequest = new ProductByIdQuery
            {
                Id = id
            };
            var result = await _queryBus.Send(queryRequest);

            return result;
        }

        [HttpPost]
        public async Task<CreateProductResult> CreateProduct([FromBody] CreateOrUpdateProductDto commandRequest)
        {
            var command = new CreateProductCommand
            {
                Name = commandRequest.Name
            };
            var result = await _commandBus.Send(command);

            return result;
        }

        [HttpPatch("{id}")]
        public async Task<UpdateProductResult> UpdateProduct(Guid id, [FromBody] CreateOrUpdateProductDto commandRequest)
        {
            var command = new UpdateProductCommand(id, commandRequest.Name);
            var result = await _commandBus.Send(command);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<DeleteProductResult> DeleteProduct([FromRoute] Guid id)
        {
            var command = new DeleteProductCommand(id);
            var result = await _commandBus.Send(command);

            return result;
        }
    }
}
