using simple_recip_application.Data.ApplicationCore.Entities;

namespace simple_recip_application.Features.IngredientsManagement.ApplicationCore;

public interface IIngredientModel : IEntityBase
{
    public string Name { get; set; }
    public byte[] Image { get; set; }
    public DateTime CreationDate { get; }
    public DateTime ModificationDate { get; }
    public DateTime? RemoveDate { get; }
}