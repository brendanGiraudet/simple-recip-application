using System.ComponentModel.DataAnnotations;
using simple_recip_application.Data.ApplicationCore;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore;
using simple_recip_application.Resources;

namespace simple_recip_application.Features.IngredientsManagement.Persistence.Entities;

public class IngredientModel : EntityBase, IIngredientModel
{
    [Required(ErrorResourceName=nameof(Messages.NameRequired), ErrorResourceType=typeof(Messages))]
    public string Name { get; set; } = string.Empty;

    public byte[] Image { get; set; } = Array.Empty<byte>();

    public DateTime CreationDate { get; private set; } = DateTime.UtcNow;

    public DateTime ModificationDate { get; private set; } = DateTime.UtcNow;

    public DateTime? RemoveDate { get; private set; }

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
