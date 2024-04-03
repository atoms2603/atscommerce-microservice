using AtsCommerce.Core.CQRS.Queries;
using ProductsService.Dtos;

namespace ProductsService.Queries;

public class GetProductsQueryHandler : IQueryHandler<ProductsQuery, ProductsResult>
{
    //private readonly DatabaseContext _db;

    public GetProductsQueryHandler(
        //DatabaseContext db
        )
    {
        //_db = db;
    }

    public async Task<ProductsResult> Handle(ProductsQuery request, CancellationToken cancellationToken)
    {
        await Task.Delay(100, cancellationToken);
        return new ProductsResult()
        {
            Name = "ProductsResult"
        };
    }
}
