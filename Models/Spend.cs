namespace Version3.Models;
public partial class Spend
{
    public int SpendId { get; set; }
    public decimal Cost { get; set; }
    public DateTime Date { get; set; }
    public int SpendCategoryId { get; set; }
    public virtual SpendCategory SpendCategory { get; set; } = null!;
    public Spend() { }
    public Spend (int spendid, decimal cost, DateTime date, int spendCategoryId, SpendCategory spendCategory)
    {
        SpendId = spendid;
        Cost = cost;
        Date = date;
        SpendCategoryId = spendCategoryId;
        SpendCategory = spendCategory;
    }
    public Spend ShallowCopy()
    {
        return (Spend)this.MemberwiseClone();
    }

    public virtual ICollection<SpendIncome> SpendIncomes { get; set; } = new List<SpendIncome>();
}
