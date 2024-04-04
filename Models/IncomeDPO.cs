using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Version3.Models
{
    class IncomeDPO : INotifyPropertyChanged
    {
        public int IncomeId { get; set; }

        private decimal cost { get; set; }
        public decimal Cost
        {
            get
            { return cost; }
            set
            { cost = value; OnPropertyChanged("Cost"); }
        }
        private DateOnly date { get; set; }
        public DateOnly Date
        {
            get { return date; }
            set { date = value; OnPropertyChanged("Date"); }
        }

        private string incomeCategoryName { get; set; }
        public string IncomeCategoryName
        {
            get { return incomeCategoryName; }
            set { incomeCategoryName = value; OnPropertyChanged("IncomeCategoryName"); }
        }

        public virtual IncomeCategory IncomeCategory { get; set; } = null!;

        public IncomeDPO() { }
        public IncomeDPO(int incomeId, decimal cost, DateOnly date, string incomeCategoryName, IncomeCategory incomeCategory)
        {
            IncomeId = incomeId;
            Cost = cost;
            Date = date;
            IncomeCategoryName = incomeCategoryName;
            IncomeCategory = incomeCategory;
        }

        public Income ShallowCopy()
        {
            return (Income)this.MemberwiseClone();
        }

        public virtual ICollection<SpendIncome> SpendIncomes { get; set; } = new List<SpendIncome>();

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
