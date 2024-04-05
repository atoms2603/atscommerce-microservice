using AtsCommerce.Core.CQRS.Queries;
using ProductsService.Data;

namespace ProductsService.Queries;
    public class ProductByIdQuery : IQuery<ProductByIdResult>
    {
        public Guid Id { get; set; }
    }

    public class ProductByIdResult
    {
        public Product Product { get; set; }
    }

public class GetProductByIdHandler
{
    public class Handler : IQueryHandler<ProductByIdQuery, ProductByIdResult>
    {
        public async Task<ProductByIdResult> Handle(ProductByIdQuery request, CancellationToken cancellationToken)
        {
            await Task.Delay(100);
            var product = ProductData.Products.Find(x => x.Id == request.Id);
            if (product == null)
            {
                throw new Exception("Product not found!");
            }

            return new ProductByIdResult
            {
                Product = product
            };
        }
    }
}

