using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using Version3.Models;
using Version3.Helper;
using Version3.View;
using System.Windows;
using System.Windows.Data;


namespace Version3.ViewModel
{
    class IncomeCategoryViewModel : INotifyPropertyChanged
    {
        private IncomeCategory selectedIncCategory;
        public IncomeCategory SelectedIncCategory
        {
            get { return this.selectedIncCategory; }
            set
            {
                this.selectedIncCategory = value;
                OnPropertyChanged("SelectedIncCategory");
                EditIncCategory.CanExecute(true);
            }
        }

        BochagovaKursovikContext db = new BochagovaKursovikContext();
        public ObservableCollection<IncomeCategory> ListIncCategory { get; set; } =
                new ObservableCollection<IncomeCategory>();
        public IncomeCategoryViewModel()
        {
            ListIncCategory = new ObservableCollection<IncomeCategory>(db.IncomeCategories.ToList());
        }
        

        private RelayCommand addIncCategory;
        public RelayCommand AddIncCategory
        {
            get
            {
                return addIncCategory ??
                 (addIncCategory = new RelayCommand(obj =>
                 {
                     WindowNewIncCategory wnIncCategory = new WindowNewIncCategory
                     {
                         Title = "Новая категория",
                     };
                     IncomeCategory inccategory = new IncomeCategory();
                     wnIncCategory.DataContext = inccategory;
                     if (wnIncCategory.ShowDialog() == true)
                     {
                         ListIncCategory.Add(inccategory);
                         db.IncomeCategories.Add(inccategory);
                         db.SaveChanges();
                     }
                     SelectedIncCategory = inccategory;
                 }));
            }
        }

        private RelayCommand editIncCategory;
        public RelayCommand EditIncCategory
        {
            get
            {
                return editIncCategory ??
                (editIncCategory = new RelayCommand(obj =>
                {
                    WindowNewIncCategory wnIncCategory = new WindowNewIncCategory
                    {
                        Title = "Редактирование категории",
                    };
                    IncomeCategory inccategory = SelectedIncCategory;
                    IncomeCategory tempIncCategory = new IncomeCategory();
                    tempIncCategory = inccategory.ShallowCopy();
                    wnIncCategory.DataContext = tempIncCategory;
                    if (wnIncCategory.ShowDialog() == true)
                    {
                        inccategory.Name = tempIncCategory.Name;
                        db.SaveChanges();
                        OnPropertyChanged("SelectedIncCategory");
                        ICollectionView view = CollectionViewSource.GetDefaultView(ListIncCategory);
                        view.Refresh();
                    }
                }, (obj) => SelectedIncCategory != null && ListIncCategory.Count > 0));
            }
        }

        private RelayCommand deleteIncCategory;
        public RelayCommand DeleteIncCategory
        {
            get
            {
                return deleteIncCategory ??
                (deleteIncCategory = new RelayCommand(obj =>
                {
                    IncomeCategory inccategory = SelectedIncCategory;
                    MessageBoxResult result = MessageBox.Show("Удалить данные по категории доходов: "
                        + inccategory.Name, "Предупреждение", MessageBoxButton.OKCancel,
                    MessageBoxImage.Warning);
                    if (result == MessageBoxResult.OK)
                    {
                        ListIncCategory.Remove(inccategory);
                        db.IncomeCategories.Remove(inccategory);
                        db.SaveChanges();
                    }
                }, (obj) => SelectedIncCategory != null && ListIncCategory.Count > 0));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
