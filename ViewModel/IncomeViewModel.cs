using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Version3.Models;
using Version3.Helper;
using Version3.View;
using System.Windows.Data;
using System.Windows;

namespace Version3.ViewModel
{
    public class IncomeViewModel : INotifyPropertyChanged
    {
        private Income selectedIncome;
        public Income SelectedIncome
        {
            get { return selectedIncome; }
            set
            {
                selectedIncome = value;
                OnPropertyChanged("SelectedIncome");
            }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }
        private IncCategoryItem selectedCategory;
        public IncCategoryItem SelectedCategory
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
        public ObservableCollection<Income> ListIncome { get; set; }
        public ObservableCollection<IncomeCategory> ListIncCategory { get; set; }
        public IncomeViewModel()
        {
            ListIncome = new ObservableCollection<Income>(db.Incomes.ToList());
            ListIncCategory = new ObservableCollection<IncomeCategory>(db.IncomeCategories.ToList());
            
        }

        private RelayCommand editIncome;
        public RelayCommand EditIncome
        {
            get
            {
                return editIncome ??
                 (editIncome = new RelayCommand(obj =>
                 {
                     WindowNewIncome wnIncome = new WindowNewIncome()
                     {
                         Title = "Редактирование данных по доходу",
                     };
                     Income income = SelectedIncome;
                     Income tempincome = new Income();
                     tempincome = income.ShallowCopy();

                     wnIncome.DataContext = tempincome;
                     wnIncome.CbIncomeCategory.ItemsSource = ListIncCategory;
                     wnIncome.CbIncomeCategory.Text = tempincome.IncomeCategory.Name;
                     if (wnIncome.ShowDialog() == true)
                     {
                         IncomeCategory selectedCategory = (IncomeCategory)wnIncome.CbIncomeCategory.SelectedItem;
                         string categoryName = selectedCategory.Name;
                         IncomeCategory c = ListIncCategory.FirstOrDefault(ic => ic.Name == categoryName);
                         income.IncomeCategory = c;
                         income.Cost = tempincome.Cost;
                         income.Date = tempincome.Date;
                         db.SaveChanges();
                         OnPropertyChanged("SelectedIncome");
                         ICollectionView view = CollectionViewSource.GetDefaultView(ListIncome);
                         view.Refresh();
                     }

                 }, (obj) => SelectedIncome != null && ListIncome.Count > 0));
            }
        }


        private RelayCommand deleteIncome;
        public RelayCommand DeleteIncome
        {
            get
            {
                return deleteIncome ??
               (deleteIncome = new RelayCommand(obj =>
               {
                   Income income = SelectedIncome;

                   MessageBoxResult result = MessageBox.Show("Удалить данные по доходу: \n"
                        + income.Cost + ", сделанной " + income.Cost + " в категории " + income.IncomeCategory.Name, "Предупреждение",
                        MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                   if (result == MessageBoxResult.OK)
                   {
                       SpendIncome si = db.SpendIncomes.FirstOrDefault(s => s.IncomeId == income.IncomeId);
                       if (si != null)
                       {
                           db.SpendIncomes.Remove(si);
                           db.SaveChanges();
                       }

                       ListIncome.Remove(income);
                       db.Incomes.Remove(income);
                       db.SaveChanges();
                   }
               }, (obj) => SelectedIncome != null && ListIncome.Count > 0));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
