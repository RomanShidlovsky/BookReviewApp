using Microsoft.AspNetCore.Identity;
using Shared;

namespace Identity.DataAccess.Entities;

public class User : IdentityUser<int>, IBaseEntity
{
    public string? ImageUrl { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public DateTimeOffset DateCreated { get; set; }
    public DateTimeOffset? DateUpdated { get; set; }
    public DateTimeOffset? DateDeleted { get; set; }
}