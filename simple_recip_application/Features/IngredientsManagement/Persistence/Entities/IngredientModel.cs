using System.ComponentModel.DataAnnotations;
using simple_recip_application.Data.Persistence.Entities;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.ValidationAttributes;
using simple_recip_application.Resources;

namespace simple_recip_application.Features.IngredientsManagement.Persistence.Entities;

public class IngredientModel : EntityBase, IIngredientModel
{
    [Required(ErrorMessageResourceName = nameof(Messages.NameRequired), ErrorMessageResourceType = typeof(Messages))]
    public string Name { get; set; } = string.Empty;

    [RequiredImage(ErrorMessageResourceName = nameof(Messages.ImageRequired), ErrorMessageResourceType = typeof(Messages))]
    public byte[] Image { get; set; } = Array.Empty<byte>();

    public DateTime CreationDate { get; private set; } = DateTime.UtcNow;

    public DateTime ModificationDate { get; private set; } = DateTime.UtcNow;

    public DateTime? RemoveDate { get; private set; }
}
