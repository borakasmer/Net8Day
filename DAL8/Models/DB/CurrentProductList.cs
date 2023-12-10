using System;
using System.Collections.Generic;

namespace DAL8.Models.DB;

public partial class CurrentProductList
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;
}
