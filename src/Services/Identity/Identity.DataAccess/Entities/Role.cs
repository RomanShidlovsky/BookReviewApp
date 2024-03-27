﻿using Microsoft.AspNetCore.Identity;
using Shared;

namespace Identity.DataAccess.Entities;

public class Role: IdentityRole<int>, IBaseEntity
{
    public DateTimeOffset DateCreated { get; set; }
    public DateTimeOffset? DateUpdated { get; set; }
    public DateTimeOffset? DateDeleted { get; set; }
}