namespace Version3.Models
{
    public class IncomeDataSearch
    {
        public decimal Cost { get; set; }
        public string IncomeCategoryName { get; set;}
        public DateTime Date { get; set; }
        public IncomeDataSearch(decimal cost, string incomeCategoryName, DateTime date)
        {
            Cost = cost;
            IncomeCategoryName = incomeCategoryName;
            Date = date;
        }
    }
}
