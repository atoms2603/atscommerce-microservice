using AtsCommerce.Core.CQRS.Commands;
using FluentValidation;
using ProductsService.Data;

namespace ProductsService.Commands
{
    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSuccess);

    public class DeleteProductCommandHandler
    {
        internal class Handler : ICommandHandler<DeleteProductCommand, DeleteProductResult>
        {

            public Handler()
            {
            }
            public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
            {
                await Task.Delay(100);
                var product = ProductData.Products.Find(x => x.Id == command.Id);
                if (product == null)
                {
                    throw new Exception("Product not found!");
                }
                ProductData.Products.Remove(product);

                return new DeleteProductResult(true);
            }
        }
    }
}
