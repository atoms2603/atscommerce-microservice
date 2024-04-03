using AtsCommerce.Core.CQRS.Queries;

namespace ProductsService.Dtos
{
    public class ProductsQuery : IQuery<ProductsResult>
    {
    }

    public class ProductsResult
    {
        public string? Name { get; set; }
    }
}
