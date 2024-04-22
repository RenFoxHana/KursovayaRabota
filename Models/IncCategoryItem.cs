namespace Version3.Models
{
    public class IncCategoryItem
    {
        public int IncomeCategoryId { get; set; }
        public string Name { get; set; }
        public decimal CategoryCost { get; set; }
        public decimal Percentage { get; set; }

        public IncCategoryItem(string name, decimal categoryCost, decimal percentage, int incomeCategoryId)
        {
            Name = name;
            CategoryCost = categoryCost;
            Percentage = percentage;
            IncomeCategoryId = incomeCategoryId;
        }
    }
}
