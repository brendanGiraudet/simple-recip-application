using simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.Factories;

public interface IHouseholdProductFactory
{
    IHouseholdProductModel CreateHouseholdProduct(string? name = null, byte[]? image = null, string? measureUnit = null);
}
