using System;
using System.Collections.Generic;

namespace DAL8.Models.DB;

public partial class RoleGroup
{
    public int Id { get; set; }

    public string? GroupName { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
