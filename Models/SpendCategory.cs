using System;
using System.Collections.Generic;

namespace Version3.Models;

public partial class SpendCategory
{
    public int SpendCategoryId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Spend> Spends { get; set; } = new List<Spend>();
}
