using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Version3.Models;

public partial class Income
{
    public int IncomeId { get; set; }

    public decimal Cost { get; set; }

    public DateOnly Date { get; set; }

    public int IncomeCategoryId { get; set; }

    public virtual IncomeCategory IncomeCategory { get; set; } = null!;

    public Income() { }
    public Income(int incomeId, decimal cost, DateOnly date, int incomeCategoryId, IncomeCategory incomeCategory)
    {
        IncomeId = incomeId;
        Cost = cost;
        Date = date;
        IncomeCategoryId = incomeCategoryId;
        IncomeCategory = incomeCategory;
    }

    public Income ShallowCopy()
    {
        return (Income)this.MemberwiseClone();
    }

    public virtual ICollection<SpendIncome> SpendIncomes { get; set; } = new List<SpendIncome>();


    //public Income CopyFromIncomeDPO(IncomeDPO p)
    //{
    //    IncomeCategoryViewModel vmIncCategory = new IncomeCategoryViewModel();
    //    int inccategoryId = 0;
    //    foreach (var c in vmIncCategory.ListIncCategory)
    //    {
    //        if (c.Name == p.CategoryName)
    //        {
    //            categoryId = c.CategoryId; break;
    //        }
    //    }
    //    if (categoryId != 0)
    //    {
    //        this.SpendId = p.SpendId;
    //        this.Cost = p.Cost;
    //        this.DateSpend = p.DateSpend;
    //        this.CategoryId = categoryId;
    //    }
    //    return this;
    //}

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

