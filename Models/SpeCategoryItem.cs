namespace Version3.Models
{
    public class SpeCategoryItem
    {
        public int SpendCategoryId { get; set; }
        public string Name { get; set; }
        public decimal CategoryCost { get; set; }
        public decimal Percentage { get; set; }

        public SpeCategoryItem(string name, decimal categoryCost, decimal percentage, int spendCategoryId)
        {
            Name = name;
            CategoryCost = categoryCost;
            Percentage = percentage;
            SpendCategoryId = spendCategoryId;
        }
    }
}
