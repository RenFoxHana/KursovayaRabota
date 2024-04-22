using System.Windows;
using Version3.ViewModel;

namespace Version3.View
{
    public partial class IncomeSearchWindow : Window
    {
        private IncomeDatesViewModel _incomeDatesViewModel;
        public IncomeSearchWindow()
        {
            InitializeComponent();
            _incomeDatesViewModel = new IncomeDatesViewModel();
            DataContext = _incomeDatesViewModel;
        }
        private void Income_OnClick(object sender, RoutedEventArgs e)
        {
            WindowIncome wIncome = new WindowIncome();
            wIncome.Show();
            this.Close();
        }
    }
}
