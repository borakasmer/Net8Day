using System;
using System.Collections.Generic;

namespace DAL8.Models.DB;

public partial class UserAction
{
    public int Id { get; set; }

    public int? IdUser { get; set; }

    public long? TotalAction { get; set; }

    public int? IdModule { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Module? IdModuleNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
