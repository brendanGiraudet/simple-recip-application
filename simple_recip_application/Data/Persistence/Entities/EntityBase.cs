using simple_recip_application.Data.ApplicationCore.Entities;

namespace simple_recip_application.Data.Persistence.Entities;

public abstract class EntityBase : IEntityBase
{
   public Guid? Id { get; set; }

   public DateTime CreationDate { get; set; }

    public DateTime? ModificationDate { get; set; }

    public DateTime? RemoveDate { get; set; }
}