using System.Collections.ObjectModel;
using System.Windows;
using Version3.Models;
using Version3.ViewModel;

namespace Version3.View
{
    public partial class WindowSpendDetails : Window
    {
        public WindowSpendDetails(SpeCategoryItem selectedCategory)
        {
            InitializeComponent();
            var spendViewModel = new SpendViewModel();
            spendViewModel.SelectedCategory = selectedCategory;
            spendViewModel.ListSpend = new ObservableCollection<Spend>
                (spendViewModel.ListSpend.Where(i => i.SpendCategoryId == selectedCategory.SpendCategoryId));
            this.DataContext = spendViewModel;
        }
        private void Spend_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow wSpend = new MainWindow();
            wSpend.Show();
            this.Close();
        }
    }
}
