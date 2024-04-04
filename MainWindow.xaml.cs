using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Version3.Models;
using Version3.View;

namespace Version3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Income_OnClick(object sender, RoutedEventArgs e)
        {
            WindowIncome wIncome = new WindowIncome();
            wIncome.Show();
            this.Close();
        }
    }
}