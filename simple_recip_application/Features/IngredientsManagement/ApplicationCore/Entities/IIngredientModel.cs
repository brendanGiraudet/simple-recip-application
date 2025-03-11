using simple_recip_application.Data.ApplicationCore.Entities;

namespace simple_recip_application.Features.IngredientsManagement.ApplicationCore.Entities;

public interface IIngredientModel : IEntityBase
{
    public string Name { get; set; }
    public byte[] Image { get; set; }
    public string MeasureUnit { get; set; }
}