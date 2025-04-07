using simple_recip_application.Data.ApplicationCore.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.Entities;
using simple_recip_application.Resources;
using simple_recip_application.Data.Persistence.Entities;

namespace simple_recip_application.Features.HouseholdProductsManagement.Persistence.Entities;

public class HouseholdProductModel : EntityBase, IHouseholdProductModel
{
    [Required(ErrorMessageResourceName = nameof(MessagesTranslator.NameRequired), ErrorMessageResourceType = typeof(MessagesTranslator))]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    [RequiredImage(ErrorMessageResourceName = nameof(MessagesTranslator.ImageRequired), ErrorMessageResourceType = typeof(MessagesTranslator))]
    public byte[] Image { get; set; } = Array.Empty<byte>();

    [Required(ErrorMessageResourceName = nameof(MessagesTranslator.MeasureUnitRequired), ErrorMessageResourceType = typeof(MessagesTranslator))]
    [MaxLength(50)]
    public string MeasureUnit { get; set; } = string.Empty;
}
