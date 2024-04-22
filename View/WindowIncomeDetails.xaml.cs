using System.Collections.ObjectModel;
using System.Windows;
using Version3.Models;
using Version3.ViewModel;

namespace Version3.View
{
    public partial class WindowIncomeDetails : Window
    {
        public WindowIncomeDetails(IncCategoryItem selectedCategory)
        {
            InitializeComponent();
            var incomeViewModel = new IncomeViewModel();
            incomeViewModel.SelectedCategory = selectedCategory;
            incomeViewModel.ListIncome = new ObservableCollection<Income>
                (incomeViewModel.ListIncome.Where(i => i.IncomeCategoryId == selectedCategory.IncomeCategoryId));
            this.DataContext = incomeViewModel;
        }
        private void Income_OnClick(object sender, RoutedEventArgs e)
        {
            WindowIncome wIncome = new WindowIncome();
            wIncome.Show();
            this.Close();
        }
    }
}
