using System.Windows;
using Version3.ViewModel;

namespace Version3.View
{
    public partial class WindowIncCategory : Window
    {
        private IncomeCategoryViewModel vmIncCategory;
        public WindowIncCategory()
        {
            InitializeComponent();
            vmIncCategory = new IncomeCategoryViewModel();
            DataContext = vmIncCategory;
        }
        private void Income_OnClick(object sender, RoutedEventArgs e)
        {
            WindowIncome wIncome = new WindowIncome();
            wIncome.Show();
            this.Close();
        }
    }
}
