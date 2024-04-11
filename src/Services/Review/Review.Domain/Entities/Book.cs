using Review.Domain.Interfaces;

namespace Review.Domain.Entities;

public class Book : IBaseEntity
{
    public int Id { get; set; }
    public virtual List<ReviewEntity> Reviews { get; set; }
    public DateTimeOffset DateCreated { get; set; }
    public DateTimeOffset? DateUpdated { get; set; }
    public DateTimeOffset? DateDeleted { get; set; }
}