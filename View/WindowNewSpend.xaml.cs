using System.Windows;
using System.Windows.Input;

namespace Version3.View
{
    public partial class WindowNewSpend : Window
    {
        public WindowNewSpend()
        {
            InitializeComponent();
        }
        private void BtSave_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0) && e.Text != ".")
            {
                e.Handled = true;
            }
            else
            {
                System.Windows.Controls.TextBox textBox = sender as System.Windows.Controls.TextBox;
                string text = textBox.Text.Insert(textBox.CaretIndex, e.Text);
                string[] parts = text.Split('.');

                if (parts.Length > 2 || (parts.Length == 2 && parts[1].Length > 2) || (parts.Length == 1 && parts[0].Length > 10))
                {
                    e.Handled = true;
                }
            }
        }
    }
}
