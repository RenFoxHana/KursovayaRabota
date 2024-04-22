using System.Windows;
using Version3.ViewModel;

namespace Version3.View
{
    public partial class SpendSearchWindow : Window
    {
        private SpendDatesViewModel _spendDatesViewModel;
        public SpendSearchWindow()
        {
            InitializeComponent();
            _spendDatesViewModel = new SpendDatesViewModel();
            DataContext = _spendDatesViewModel;
        }
        private void Spend_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow wSpend = new MainWindow();
            wSpend.Show();
            this.Close();
        }
    }
}
