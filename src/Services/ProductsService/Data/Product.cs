namespace ProductsService.Data
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
    }

    public static class ProductData
    {
        public static List<Product> Products { get; set; } = new List<Product>()
        {
            new() {
                Id = Guid.NewGuid(),
                Name = "Product 1",
            },
            new() {
                Id = Guid.NewGuid(),
                Name = "Product 2",
            },
            new() {
                Id = Guid.NewGuid(),
                Name = "Product 3",
            },
            new() {
                Id = Guid.NewGuid(),
                Name = "Product 4",
            },
            new() {
                Id = Guid.NewGuid(),
                Name = "Product 5",
            },
            new() {
                Id = Guid.NewGuid(),
                Name = "Product 6",
            },
        };
    }
}
