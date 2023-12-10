using System;
using System.Collections.Generic;

namespace DAL8.Models.DB;

public partial class Action
{
    public int Id { get; set; }

    public long? ActionNumber { get; set; }

    public string? ActionName { get; set; }

    public int? IdModule { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Module? IdModuleNavigation { get; set; }
}
