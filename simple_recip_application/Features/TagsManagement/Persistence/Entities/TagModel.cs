using System.ComponentModel.DataAnnotations;
using simple_recip_application.Data.Persistence.Entities;
using simple_recip_application.Features.TagsManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.TagsManagement.Persistence.Entities;

public class TagModel : EntityBase, ITagModel
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
}
