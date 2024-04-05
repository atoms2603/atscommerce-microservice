using AtsCommerce.Core.CQRS.Commands;
using FluentValidation;
using ProductsService.Data;

namespace ProductsService.Commands
{
    public class CreateProductCommand : ICommand<CreateProductResult>
    {
        public string Name { get; set; }
    }

    public class CreateProductResult
    {
        public Guid Id { get; set; }
    }

    public class Validator : AbstractValidator<CreateProductCommand>
    {
        public Validator()
        {
            RuleFor(cmd => cmd.Name).NotEmpty();
        }
    }

    public class CreateProductCommandHandler
    {
        public class Handler : ICommandHandler<CreateProductCommand, CreateProductResult>
        {

            public Handler()
            {
            }
            public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
            {
                var product = new Product
                {
                    Name = command.Name,
                };

                await Task.Delay(100);
                ProductData.Products.Add(product);

                var result = new CreateProductResult
                {
                    Id = product.Id
                };

                return result;
            }
        }
    }
}
