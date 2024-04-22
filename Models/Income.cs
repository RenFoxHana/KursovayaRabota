namespace Version3.Models;

public partial class Income
{
    public int IncomeId { get; set; }
    public decimal Cost { get; set; }
    public DateTime Date { get; set; }
    public int IncomeCategoryId { get; set; }
    public virtual IncomeCategory IncomeCategory { get; set; } = null!;
    public Income() { }
    public Income(int incomeId, decimal cost, DateTime date, int incomeCategoryId, IncomeCategory incomeCategory)
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
}

