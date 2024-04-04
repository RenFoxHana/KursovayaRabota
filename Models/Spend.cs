using System;
using System.Collections.Generic;
using Version3.ViewModel;

namespace Version3.Models;

public partial class Spend
{
    public int SpendId { get; set; }

    public decimal Cost { get; set; }

    public DateOnly Date { get; set; }

    public int SpendCategoryId { get; set; }

    public virtual SpendCategory SpendCategory { get; set; } = null!;

    public Spend ShallowCopy()
    {
        return (Spend)this.MemberwiseClone();
    }

    public virtual ICollection<SpendIncome> SpendIncomes { get; set; } = new List<SpendIncome>();
}
