using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using Version3.Helper;
using Version3.Models;
using Version3.View;

namespace Version3.ViewModel
{
    public class SpendViewModel : INotifyPropertyChanged
    {
        private Spend selectedSpend;
        public Spend SelectedSpend
        {
            get { return selectedSpend; }
            set
            {
                selectedSpend = value;
                OnPropertyChanged("SelectedSpend");
            }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }
        private SpeCategoryItem selectedCategory;
        public SpeCategoryItem SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value;
                OnPropertyChanged("SelectedCategory");
                Name = selectedCategory.Name;
            }
        }

        private BochagovaKursovikContext db = new BochagovaKursovikContext();
        public ObservableCollection<Spend> ListSpend { get; set; }
        public ObservableCollection<SpendCategory> ListSpeCategory { get; set; }
        public SpendViewModel()
        {
            ListSpend = new ObservableCollection<Spend>(db.Spends.ToList());
            ListSpeCategory = new ObservableCollection<SpendCategory>(db.SpendCategories.ToList());

        }

        private RelayCommand editSpend;
        public RelayCommand EditSpend
        {
            get
            {
                return editSpend ??
                 (editSpend = new RelayCommand(obj =>
                 {
                     WindowNewSpend wnSpend = new WindowNewSpend()
                     {
                         Title = "Редактирование данных по расходы",
                     };
                     Spend spend = SelectedSpend;
                     Spend tempspend = new Spend();
                     tempspend = spend.ShallowCopy();

                     wnSpend.DataContext = tempspend;
                     wnSpend.CbSpendCategory.ItemsSource = ListSpeCategory;
                     wnSpend.CbSpendCategory.Text = tempspend.SpendCategory.Name;
                     if (wnSpend.ShowDialog() == true)
                     {
                         SpendCategory selectedCategory = (SpendCategory)wnSpend.CbSpendCategory.SelectedItem;
                         string categoryName = selectedCategory.Name;
                         SpendCategory c = ListSpeCategory.FirstOrDefault(ic => ic.Name == categoryName);
                         spend.SpendCategory = c;
                         spend.Cost = tempspend.Cost;
                         spend.Date = tempspend.Date;
                         db.SaveChanges();
                         OnPropertyChanged("SelectedSpend");
                         ICollectionView view = CollectionViewSource.GetDefaultView(ListSpend);
                         view.Refresh();
                         CheckExpenseLimit();
                     }

                 }, (obj) => SelectedSpend != null && ListSpend.Count > 0));
            }
        }


        private RelayCommand deleteSpend;
        public RelayCommand DeleteSpend
        {
            get
            {
                return deleteSpend ??
               (deleteSpend = new RelayCommand(obj =>
               {
                   Spend spend = SelectedSpend;
                   MessageBoxResult result = MessageBox.Show("Удалить данные по трате: \n"
                        + spend.Cost + ", сделанной " + spend.Cost + " в категории " + spend.SpendCategory.Name, "Предупреждение",
                        MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                   if (result == MessageBoxResult.OK)
                   {
                       SpendIncome si = db.SpendIncomes.FirstOrDefault(s => s.SpendId == spend.SpendId);
                       if (si != null)
                       {
                           db.SpendIncomes.Remove(si);
                           db.SaveChanges();
                       }

                       ListSpend.Remove(spend);
                       db.Spends.Remove(spend);
                       db.SaveChanges();
                       CheckExpenseLimit();
                   }
               }, (obj) => SelectedSpend != null && ListSpend.Count > 0));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
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
