namespace simple_recip_application.Data.ApplicationCore.Entities;

public interface IEntityBase
{
   public Guid? Id { get; set; }
   public DateTime CreationDate { get; set; }

    public DateTime? ModificationDate { get; set; }

    public DateTime? RemoveDate { get; set; }
}