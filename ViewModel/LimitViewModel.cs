using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using Version3.Models;

namespace Version3.ViewModels
{
    public class LimitViewModel : INotifyPropertyChanged
    {
        private readonly string _path = @"D:\jobs\RPM\Kursach\Version3\DataModels\Limit.json";
        public ObservableCollection<SpendLimit> Limit 
            = new ObservableCollection<SpendLimit>();

        public string Error { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public LimitViewModel()
        {
            Limit = new ObservableCollection<SpendLimit>();
            Limit = LoadSpendLimit();
        }
        private ObservableCollection<SpendLimit> LoadSpendLimit()
        {
            if (File.Exists(_path))
            {
                var json = File.ReadAllText(_path);
                Limit = JsonConvert.DeserializeObject<ObservableCollection<SpendLimit>>(json);
                if (Limit != null)
                {
                    return Limit;
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

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}