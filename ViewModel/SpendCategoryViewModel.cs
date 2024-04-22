using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using Version3.Helper;
using Version3.Models;
using Version3.View;

namespace Version3.ViewModel
{
    public class SpendCategoryViewModel: INotifyPropertyChanged
    {
        private SpendCategory selectedSpeCategory;
        public SpendCategory SelectedSpeCategory
        {
            get { return this.selectedSpeCategory; }
            set
            {
                this.selectedSpeCategory = value;
                OnPropertyChanged("SelectedSpeCategory");
                EditSpeCategory.CanExecute(true);
            }
        }

        BochagovaKursovikContext db = new BochagovaKursovikContext();
        public ObservableCollection<SpendCategory> ListSpeCategory { get; set; } =
                new ObservableCollection<SpendCategory>();
        public SpendCategoryViewModel()
        {
            ListSpeCategory = new ObservableCollection<SpendCategory>(db.SpendCategories.ToList());
        }

        private RelayCommand addSpeCategory;
        public RelayCommand AddSpeCategory
        {
            get
            {
                return addSpeCategory ??
                 (addSpeCategory = new RelayCommand(obj =>
                 {
                     WindowNewSpeCategory wnSpeCategory = new WindowNewSpeCategory
                     {
                         Title = "Новая категория",
                     };
                     SpendCategory specategory = new SpendCategory();
                     wnSpeCategory.DataContext = specategory;
                     if (wnSpeCategory.ShowDialog() == true)
                     {
                         ListSpeCategory.Add(specategory);
                         db.SpendCategories.Add(specategory);
                         db.SaveChanges();
                     }
                     SelectedSpeCategory = specategory;
                 }));
            }
        }

        private RelayCommand editSpeCategory;
        public RelayCommand EditSpeCategory
        {
            get
            {
                return editSpeCategory ??
                (editSpeCategory = new RelayCommand(obj =>
                {
                    WindowNewSpeCategory wnSpeCategory = new WindowNewSpeCategory
                    {
                        Title = "Редактирование категории",
                    };
                    SpendCategory specategory = SelectedSpeCategory;
                    SpendCategory tempSpeCategory = new SpendCategory();
                    tempSpeCategory = specategory.ShallowCopy();
                    wnSpeCategory.DataContext = tempSpeCategory;
                    if (wnSpeCategory.ShowDialog() == true)
                    {
                        specategory.Name = tempSpeCategory.Name;
                        db.SaveChanges();
                        OnPropertyChanged("SelectedSpeCategory");
                        ICollectionView view = CollectionViewSource.GetDefaultView(ListSpeCategory);
                        view.Refresh();
                    }
                }, (obj) => SelectedSpeCategory != null && ListSpeCategory.Count > 0));
            }
        }

        private RelayCommand deleteSpeCategory;
        public RelayCommand DeleteSpeCategory
        {
            get
            {
                return deleteSpeCategory ??
                (deleteSpeCategory = new RelayCommand(obj =>
                {
                    SpendCategory specategory = SelectedSpeCategory;
                    MessageBoxResult result = MessageBox.Show("Удалить данные по категории расходов: "
                        + specategory.Name, "Предупреждение", MessageBoxButton.OKCancel,
                    MessageBoxImage.Warning);
                    if (result == MessageBoxResult.OK)
                    {
                        ListSpeCategory.Remove(specategory);
                        db.SpendCategories.Remove(specategory);
                        db.SaveChanges();
                    }
                }, (obj) => SelectedSpeCategory != null && ListSpeCategory.Count > 0));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
