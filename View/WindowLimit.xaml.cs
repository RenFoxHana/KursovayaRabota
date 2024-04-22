using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using Version3.Models;
using Version3.ViewModels;

namespace Version3.View
{
    public partial class WindowLimit : Window, INotifyPropertyChanged
    {
        private readonly string _path = @"D:\jobs\RPM\Kursach\Version3\DataModels\Limit.json";
        private BochagovaKursovikContext db = new BochagovaKursovikContext();
        private decimal _limit;
        public string Error { get; set; }
        public ObservableCollection<SpendLimit> Limit
    = new ObservableCollection<SpendLimit>();
        public decimal Limits
        {
            get { return _limit; }
            set { _limit = value; OnPropertyChanged(); }
        }

        public WindowLimit()
        {
            InitializeComponent();
            DataContext = this;

            var spendLimit = LoadSpendLimit();

            if (spendLimit != null && spendLimit.Count > 0)
            {
                Limits = spendLimit[0].Limit;
            }
        }
        private void SetLimitButton_Click(object sender, RoutedEventArgs e)
        {
            string input = LimitTextBox.Text;
            input = input.Replace('.', ',');
            if (decimal.TryParse(input, out decimal limit))
            {
                Limits = limit;

                Limit = LoadSpendLimit();

                decimal TotalSpend = (from spe in db.Spends
                                      join si in db.SpendIncomes on spe.SpendId equals si.SpendId
                                      select spe.Cost).Sum();

                decimal dif = TotalSpend - Limits;

                if (Limits < TotalSpend)
                {
                    MessageBoxResult result = MessageBox.Show($"Превышение лимита на {dif.ToString()}!", "Предупреждение",
                            MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                }

                // Добавить новый лимит к коллекции
                Limit.Clear();
                Limit.Add(new SpendLimit { Limit = Limits });

                SaveChanges(Limit);
            }
            else
            {
                MessageBox.Show("Введите корректное значение лимита.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveChanges(ObservableCollection<SpendLimit> limit)
        {
            var settings = new JsonSerializerSettings { DateFormatString = "MM/dd/yyyy" };
            var jsonSpend = JsonConvert.SerializeObject(limit, settings);

            try
            {
                File.WriteAllText(_path, jsonSpend);
            }
            catch (IOException e)
            {
                Error = "Ошибка записи в файл: " + e.Message;
            }
        }

        private ObservableCollection<SpendLimit> LoadSpendLimit()
        {
            if (File.Exists(_path))
            {
                var json = File.ReadAllText(_path);
                var limits = JsonConvert.DeserializeObject<ObservableCollection<SpendLimit>>(json);
                if (limits != null)
                {
                    return limits;
                }
                else
                {
                    MessageBox.Show("Лимит не найден. Установлен лимит по умолчанию.");
                    return null;
                }
            }
            else
            {   
                return null;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
