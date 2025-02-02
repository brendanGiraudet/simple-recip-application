using simple_recip_application.Data.ApplicationCore;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore;

namespace simple_recip_application.Features.IngredientsManagement.Persistence.Entities;

public class IngredientModel : EntityBase, IIngredientModel
{
    public string Name { get; set; } = string.Empty;
    public byte[] Image { get; set; } = Array.Empty<byte>();
    public DateTime CreationDate { get => CreationDate; set => CreationDate = value; }
    public DateTime ModificationDate { get => ModificationDate; set => ModificationDate = value; }
    public DateTime? RemoveDate { get => throw new NotImplementedException(); set => RemoveDate = value; }

    // Constructeur par défaut
    public IngredientModel() { }

    // Constructeur avec paramètres
    public IngredientModel(string name, byte[] image)
    {
        Name = name;
        Image = image;
        CreationDate = DateTime.UtcNow;
        ModificationDate = DateTime.UtcNow;
    }

    // Méthode pour mettre à jour le nom
    public void UpdateName(string newName)
    {
        if (!string.IsNullOrWhiteSpace(newName))
        {
            Name = newName;
            ModificationDate = DateTime.UtcNow;
        }
    }

    // Méthode pour mettre à jour l’image
    public void UpdateImage(byte[] newImage)
    {
        if (newImage != null && newImage.Length > 0)
        {
            Image = newImage;
            ModificationDate = DateTime.UtcNow;
        }
    }

    // Méthode pour marquer comme supprimé
    public void MarkAsRemoved()
    {
        RemoveDate = DateTime.UtcNow;
    }
}
