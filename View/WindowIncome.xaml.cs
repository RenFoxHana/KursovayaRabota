using System.Windows;
namespace Version3.View
{

    public partial class WindowIncome : Window
    {
        public WindowIncome()
        {
            InitializeComponent();
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
    }
}
