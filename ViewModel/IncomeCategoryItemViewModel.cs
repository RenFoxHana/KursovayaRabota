using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Version3.Helper;
using Version3.Models;
using Version3.View;

namespace Version3.ViewModel
{
    public class IncomeCategoryItemViewModel : INotifyPropertyChanged
    {
        private BochagovaKursovikContext db = new BochagovaKursovikContext();
        public ObservableCollection<IncCategoryItem> ListIncCategoryItems { get; set; }
        public ObservableCollection<Income> ListIncome { get; set; }
        public ObservableCollection<IncomeCategory> ListIncCategory { get; set; }
        public SeriesCollection PieSeriesCollection { get; set; }

        private string _totalCost;
        public string TotalCost
        {
            get { return _totalCost; }
            set
            {
                _totalCost = value;
                OnPropertyChanged();
            }
        }
        public IncomeCategoryItemViewModel()
        {
            ListIncCategoryItems = new ObservableCollection<IncCategoryItem>();
            ListIncome = new ObservableCollection<Income>(db.Incomes.ToList());
            ListIncCategory = new ObservableCollection<IncomeCategory>(db.IncomeCategories.ToList());

            PieSeriesCollection = new SeriesCollection();
            CalculateDiagramm();
            decimal totalCost = ListIncome.Sum(i => i.Cost);
            TotalCost = totalCost.ToString();
        }
        public void CalculateDiagramm()
        {
            decimal totalCost = ListIncome.Sum(i => i.Cost);

            foreach (var inccategory in ListIncCategory)
            {
                decimal categoryCost = ListIncome.Where(i => i.IncomeCategoryId == inccategory.IncomeCategoryId).Sum(i => i.Cost);
                decimal percentage = Math.Round(categoryCost / totalCost * 100, 2);
                int incomeCategoryId = inccategory.IncomeCategoryId;
                ListIncCategoryItems.Add(new IncCategoryItem(inccategory.Name, categoryCost, percentage, incomeCategoryId));
            }

            foreach (var item in ListIncCategoryItems)
            {
                PieSeriesCollection.Add(new PieSeries
                {
                    Title = item.Name,
                    Values = new ChartValues<decimal> { item.Percentage }
                });
            }
        }
        private RelayCommand addIncome;
        public RelayCommand AddIncome
        {
            get
            {
                return addIncome ??
                 (addIncome = new RelayCommand(obj =>
                 {
                     WindowNewIncome wnIncome = new WindowNewIncome()
                     {
                         Title = "Добавление данных по доходу",
                     };
                     Income inc = new Income
                     {
                         Date = DateTime.Now,
                     };
                     wnIncome.DataContext = inc;
                     wnIncome.CbIncomeCategory.ItemsSource = ListIncCategory;
                     if (wnIncome.ShowDialog() == true)
                     {
                         IncomeCategory selectedCategory = (IncomeCategory)wnIncome.CbIncomeCategory.SelectedItem;
                         string categoryName = selectedCategory.Name;
                         IncomeCategory c = ListIncCategory.FirstOrDefault(ic => ic.Name == categoryName);
                         inc.IncomeCategory = c;
                         ListIncome.Add(inc);
                         db.Incomes.Add(inc);
                         db.SaveChanges();

                         SpendIncome si = new SpendIncome { IncomeId = inc.IncomeId };
                         db.SpendIncomes.Add(si);
                         db.SaveChanges();

                         TotalCost = ListIncome.Sum(i => i.Cost).ToString();
                         OnPropertyChanged("TotalCost");

                         ListIncCategoryItems.Clear();
                         PieSeriesCollection.Clear();
                         CalculateDiagramm();

                         OnPropertyChanged("ListIncCategoryItems");
                         OnPropertyChanged("PieSeriesCollection");
                     }
                 }, (obj) => true));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
