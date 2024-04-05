using AtsCommerce.Core.CQRS.Queries;
using ProductsService.Data;

namespace ProductsService.Queries;
    public class ProductsQuery : IQuery<ProductsResult>
    {
    }

    public class ProductsResult
    {
        public ICollection<Product> Products { get; set; }
    }

public class GetProductsQueryHandler
{
    public class Handler : IQueryHandler<ProductsQuery, ProductsResult>
    {
        //private readonly DatabaseContext _db;

        public Handler(
            //DatabaseContext db
            )
        {
            //_db = db;
        }

        public async Task<ProductsResult> Handle(ProductsQuery request, CancellationToken cancellationToken)
        {
            await Task.Delay(100);
            var products = ProductData.Products;
            return new ProductsResult
            {
                Products = products,
            };
        }
    }
}

