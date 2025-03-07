using simple_recip_application.Data.ApplicationCore.Entities;

namespace simple_recip_application.Data.Persistence.Entities;

public abstract class EntityBase : IEntityBase
{
   public Guid? Id { get; set; }
}