using System;
using System.Collections.Generic;

namespace DAL8.Models.DB;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? LastName { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public string? PasswordHash { get; set; }

    public string? Email { get; set; }

    public string? Gsm { get; set; }

    public bool IsDeleted { get; set; }

    public bool? IsAdmin { get; set; }

    public virtual ICollection<UserAction> UserActions { get; set; } = new List<UserAction>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
