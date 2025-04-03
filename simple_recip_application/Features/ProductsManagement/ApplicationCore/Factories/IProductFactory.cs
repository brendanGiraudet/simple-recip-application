using simple_recip_application.Features.ProductsManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.ProductsManagement.ApplicationCore.Factories;

public interface IProductFactory
{
    IProductModel CreateProduct(string? name = null, byte[]? image = null, string? measureUnit = null);
}
