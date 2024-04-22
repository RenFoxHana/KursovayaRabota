namespace Version3.Models;

public partial class IncomeCategory
{
    public int IncomeCategoryId { get; set; }
    public string Name { get; set; }
    public IncomeCategory() { }
    public IncomeCategory(int incomeCategoryId, string name)
    {
        IncomeCategoryId = incomeCategoryId;
        Name = name;
    }
    public virtual ICollection<Income> Incomes { get; set; } = new List<Income>();
    public IncomeCategory ShallowCopy()
    {
        return (IncomeCategory)this.MemberwiseClone();
    }
}
