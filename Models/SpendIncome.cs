namespace Version3.Models;

public partial class SpendIncome
{
    public int SpendIncomeId { get; set; }

    public int? IncomeId { get; set; }

    public int? SpendId { get; set; }

    public virtual Income? Income { get; set; }

    public virtual Spend? Spend { get; set; }
}
