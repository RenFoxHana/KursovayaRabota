using System.Windows;
using Version3.ViewModel;

namespace Version3.View
{
    public partial class WindowSpeCategory : Window
    {
        private SpendCategoryViewModel vmSpeCategory;
        public WindowSpeCategory()
        {
            InitializeComponent();
            vmSpeCategory = new SpendCategoryViewModel();
            DataContext = vmSpeCategory;
        }
        private void Spend_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow wSpend = new MainWindow();
            wSpend.Show();
            this.Close();
        }
    }
}
