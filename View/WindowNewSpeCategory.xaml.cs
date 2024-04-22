using System.Windows;

namespace Version3.View
{
    public partial class WindowNewSpeCategory : Window
    {
        public WindowNewSpeCategory()
        {
            InitializeComponent();
        }
        private void BtSave_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
