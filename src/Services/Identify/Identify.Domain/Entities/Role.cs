using Identify.Domain.Shared;
using Microsoft.AspNetCore.Identity;

namespace Identify.Domain.Entities;

public class Role: IdentityRole<int>, IBaseEntity
{
    public DateTimeOffset DateCreated { get; set; }
    public DateTimeOffset? DateUpdated { get; set; }
    public DateTimeOffset? DateDeleted { get; set; }
}