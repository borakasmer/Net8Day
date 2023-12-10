using System;
using System.Collections.Generic;

namespace DAL8.Models.DB;

public partial class Module
{
    public int Id { get; set; }

    public string ModuleName { get; set; } = null!;

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Action> Actions { get; set; } = new List<Action>();

    public virtual ICollection<UserAction> UserActions { get; set; } = new List<UserAction>();
}
