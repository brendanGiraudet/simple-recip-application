using simple_recip_application.Data.ApplicationCore.ValidationAttributes;
using simple_recip_application.Data.Persistence.Entities;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Entities;
using simple_recip_application.Resources;
using System.ComponentModel.DataAnnotations;

namespace simple_recip_application.Features.IngredientsManagement.Persistence.Entities;

public class IngredientModel : EntityBase, IIngredientModel
{
    [Required(ErrorMessageResourceName = nameof(MessagesTranslator.NameRequired), ErrorMessageResourceType = typeof(MessagesTranslator))]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    [RequiredImage(ErrorMessageResourceName = nameof(MessagesTranslator.ImageRequired), ErrorMessageResourceType = typeof(MessagesTranslator))]
    public byte[] Image { get; set; } = Array.Empty<byte>();

    public DateTime CreationDate { get; private set; } = DateTime.UtcNow;

    public DateTime ModificationDate { get; private set; } = DateTime.UtcNow;

    public DateTime? RemoveDate { get; private set; }
}
