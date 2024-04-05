using AtsCommerce.Core.CQRS.Commands;
using FluentValidation;
using ProductsService.Data;

namespace ProductsService.Commands
{
    public record UpdateProductCommand(Guid Id, string Name) : ICommand<UpdateProductResult>;
    public record UpdateProductResult(Guid Id);

    public class UpdateProductCommandHandler
    {
        internal class Handler : ICommandHandler<UpdateProductCommand, UpdateProductResult>
        {

            public Handler()
            {
            }
            public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
            {
                await Task.Delay(100);
                var product = ProductData.Products.Find(x => x.Id == command.Id);
                if (product == null)
                {
                    throw new Exception("Product not found!");
                }
                product.Name = command.Name;

                return new UpdateProductResult(product.Id);
            }
        }
    }
}
