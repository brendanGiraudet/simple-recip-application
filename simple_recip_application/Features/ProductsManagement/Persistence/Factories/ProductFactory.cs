namespace simple_recip_application.Features.ProductsManagement.Persistence.Factories;

using simple_recip_application.Features.ProductsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.ProductsManagement.ApplicationCore.Factories;
using simple_recip_application.Features.ProductsManagement.Persistence.Entities;

public class ProductFactory : IProductFactory
{
    public IProductModel CreateProduct(string? name = null, byte[]? image = null, string? measureUnit = null) => new ProductModel
    {
        Name = name ?? string.Empty,
        Image = image ?? [],
        MeasureUnit = measureUnit ?? string.Empty
    };
}
