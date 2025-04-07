namespace simple_recip_application.Features.HouseholdProductsManagement.Persistence.Factories;

using simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.Factories;
using simple_recip_application.Features.HouseholdProductsManagement.Persistence.Entities;

public class HouseholdProductFactory : IHouseholdProductFactory
{
    public IHouseholdProductModel CreateHouseholdProduct(string? name = null, byte[]? image = null, string? measureUnit = null) => new HouseholdProductModel
    {
        Name = name ?? string.Empty,
        Image = image ?? [],
        MeasureUnit = measureUnit ?? string.Empty
    };
}
