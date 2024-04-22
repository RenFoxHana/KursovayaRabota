using LiveCharts;
using LiveCharts.Wpf;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using Version3.Helper;
using Version3.Models;
using Version3.View;

namespace Version3.ViewModel
{
    public class SpendCategoryItemViewModel : INotifyPropertyChanged
    {
        private BochagovaKursovikContext db = new BochagovaKursovikContext();
        public ObservableCollection<SpeCategoryItem> ListSpeCategoryItems { get; set; }
        public ObservableCollection<Spend> ListSpend { get; set; }
        public ObservableCollection<SpendCategory> ListSpeCategory { get; set; }
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
        public SpendCategoryItemViewModel()
        {
            ListSpeCategoryItems = new ObservableCollection<SpeCategoryItem>();
            ListSpend = new ObservableCollection<Spend>(db.Spends.ToList());
            ListSpeCategory = new ObservableCollection<SpendCategory>(db.SpendCategories.ToList());

            PieSeriesCollection = new SeriesCollection();
            CalculateDiagramm();
            decimal totalCost = ListSpend.Sum(i => i.Cost);
            TotalCost = totalCost.ToString();
        }
        public void CalculateDiagramm()
        {
            decimal totalCost = ListSpend.Sum(i => i.Cost);

            foreach (var specategory in ListSpeCategory)
            {
                decimal categoryCost = ListSpend.Where(i => i.SpendCategoryId == specategory.SpendCategoryId).Sum(i => i.Cost);
                decimal percentage = Math.Round(categoryCost / totalCost * 100, 2);
                int spendCategoryId = specategory.SpendCategoryId;
                ListSpeCategoryItems.Add(new SpeCategoryItem(specategory.Name, categoryCost, percentage, spendCategoryId));
            }

            foreach (var item in ListSpeCategoryItems)
            {
                PieSeriesCollection.Add(new PieSeries
                {
                    Title = item.Name,
                    Values = new ChartValues<decimal> { item.Percentage }
                });
            }
        }
        private RelayCommand addSpend;
        public RelayCommand AddSpend
        {
            get
            {
                return addSpend ??
                 (addSpend = new RelayCommand(obj =>
                 {
                     WindowNewSpend wnSpend = new WindowNewSpend()
                     {
                         Title = "Добавление данных по трате",
                     };
                     Spend spe = new Spend
                     {
                         Date = DateTime.Now,
                     };
                     wnSpend.DataContext = spe;
                     wnSpend.CbSpendCategory.ItemsSource = ListSpeCategory;
                     if (wnSpend.ShowDialog() == true)
                     {
                         SpendCategory selectedCategory = (SpendCategory)wnSpend.CbSpendCategory.SelectedItem;
                         string categoryName = selectedCategory.Name;
                         SpendCategory c = ListSpeCategory.FirstOrDefault(ic => ic.Name == categoryName);
                         spe.SpendCategory = c;
                         ListSpend.Add(spe);
                         db.Spends.Add(spe);
                         db.SaveChanges();

                         SpendIncome si = new SpendIncome { SpendId = spe.SpendId };
                         db.SpendIncomes.Add(si);
                         db.SaveChanges();

                         TotalCost = ListSpend.Sum(i => i.Cost).ToString();
                         OnPropertyChanged("TotalCost");

                         ListSpeCategoryItems.Clear();
                         PieSeriesCollection.Clear();
                         CalculateDiagramm();
                         OnPropertyChanged("ListSpeCategoryItems");
                         OnPropertyChanged("PieSeriesCollection");
                         CheckExpenseLimit();
                     }
                 }, (obj) => true)
                 {

                 });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly string _path = @"D:\jobs\RPM\Kursach\Version3\DataModels\Limit.json";
        private void CheckExpenseLimit()
        {
            var spendLimits = LoadSpendLimit();
            if (spendLimits != null && spendLimits.Count > 0)
            {
                decimal spendLimit = spendLimits[0].Limit;

                decimal totalSpends = (from spe in db.Spends
                                       join si in db.SpendIncomes on spe.SpendId equals si.SpendId
                                       select spe.Cost).Sum();

                if (totalSpends > spendLimit)
                {
                    var exceededAmount = totalSpends - spendLimit;
                    MessageBoxResult result = MessageBox.Show($"Превышение лимита на {exceededAmount}!", "Предупреждение",
                           MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Лимит не найден. Установлен лимит по умолчанию.");
            }
        }
        private ObservableCollection<SpendLimit> LoadSpendLimit()
        {
            if (File.Exists(_path))
            {
                var json = File.ReadAllText(_path);
                var limits = JsonConvert.DeserializeObject<ObservableCollection<SpendLimit>>(json);
                if (limits != null)
                {
                    return limits;
                }
                else
                {
                    MessageBox.Show("Лимит не найден. Установлен лимит по умолчанию.");
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
