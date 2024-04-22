namespace Version3.Models;

public partial class SpendCategory
{
    public int SpendCategoryId { get; set; }
    public string Name { get; set; }
    public SpendCategory() { }
    public SpendCategory(int spendCategoryId, string name)
    {
        SpendCategoryId = spendCategoryId;
        Name = name;
    }
    public SpendCategory ShallowCopy()
    {
        return (SpendCategory)this.MemberwiseClone();
    }
    public virtual ICollection<Spend> Spends { get; set; } = new List<Spend>();

}
