namespace simple_recip_application.Features.IngredientsManagement.ApplicationCore;

public interface IIngredientModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public byte[] Image { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ModificationDate { get; set; }
    public DateTime? RemoveDate { get; set; }
}