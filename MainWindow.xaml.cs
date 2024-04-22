using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Version3.Models;
using Version3.View;
using Version3.ViewModel;

namespace Version3
{
    public partial class MainWindow : Window
    {
        private BochagovaKursovikContext db = new BochagovaKursovikContext();

        public MainWindow()
        {
            InitializeComponent();
            SpendCategoryItemViewModel viewModel = new SpendCategoryItemViewModel();
            this.DataContext = viewModel;
        }
        private void SpendCategory_OnClick(object sender, RoutedEventArgs e)
        {
            WindowSpeCategory wSpeCategory = new WindowSpeCategory();
            wSpeCategory.Show();
            this.Close();
        }
        private void Income_OnClick(object sender, RoutedEventArgs e)
        {
            WindowIncome wIncome = new WindowIncome();
            wIncome.Show();
            this.Close();
        }
        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListView listView = sender as ListView;
            var selectedCategory = (SpeCategoryItem)listView.SelectedItem;

            if (selectedCategory != null)
            {
                WindowSpendDetails windowSpendDetails = new WindowSpendDetails(selectedCategory);
                windowSpendDetails.Title = "Доходы по категории";
                windowSpendDetails.Show();
            }
            this.Close();
        }
        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SpendSearchWindow spendSearchWindow = new SpendSearchWindow();
            spendSearchWindow.Show();
            this.Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowLimit limitWindow = new WindowLimit();
            limitWindow.Show();
        }
    }
}