using System;
using System.Collections.Generic;

namespace DAL8.Models.DB;

public partial class UserRole
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? RoleGroupId { get; set; }

    public long? Roles { get; set; }

    public virtual RoleGroup? RoleGroup { get; set; }

    public virtual User? User { get; set; }
}
