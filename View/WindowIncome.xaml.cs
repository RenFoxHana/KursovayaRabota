using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Version3.Models;
using Version3.ViewModel;
namespace Version3.View
{
    public partial class WindowIncome : Window
    {
        public WindowIncome()
        {
            InitializeComponent();
            IncomeCategoryItemViewModel viewModel = new IncomeCategoryItemViewModel();
            this.DataContext = viewModel;
        }
        private void IncomeCategory_OnClick(object sender, RoutedEventArgs e)
        {
            WindowIncCategory wIncCategory = new WindowIncCategory();
            wIncCategory.Show();
            this.Close();
        }
        private void Spend_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow wSpend = new MainWindow();
            wSpend.Show();
            this.Close();
        }
        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListView listView = sender as ListView;
            var selectedCategory = (IncCategoryItem)listView.SelectedItem;

            if (selectedCategory != null)
            {
                WindowIncomeDetails windowIncomeDetails = new WindowIncomeDetails(selectedCategory);
                windowIncomeDetails.Title = "Доходы по категории";
                windowIncomeDetails.Show();
            }
            this.Close();
        }
        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            IncomeSearchWindow incomeSearchWindow = new IncomeSearchWindow();
            incomeSearchWindow.Show();
            this.Close();
        }

    }
}
