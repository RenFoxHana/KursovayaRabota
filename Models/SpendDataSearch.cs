namespace Version3.Models
{
    public class SpendDataSearch
    {
        public decimal Cost { get; set; }
        public string SpendCategoryName { get; set; }
        public DateTime Date { get; set; }
        public SpendDataSearch(decimal cost, string spendCategoryName, DateTime date)
        {
            Cost = cost;
            SpendCategoryName = spendCategoryName;
            Date = date;
        }
    }
}
