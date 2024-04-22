using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Version3.Models;

namespace Version3.ViewModel
{
    class SpendDatesViewModel: INotifyPropertyChanged
    {
        private DateTime _startDate;
        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; OnPropertyChanged(); LoadSpendsBetweenDates(); }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; OnPropertyChanged(); LoadSpendsBetweenDates(); }
        }

        private BochagovaKursovikContext db = new BochagovaKursovikContext();
        public ObservableCollection<SpendDataSearch> SpendSearch { get; set; }

        public SpendDatesViewModel()
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            LoadSpendsBetweenDates();
        }
        private void LoadSpendsBetweenDates()
        {
            SpendSearch = new ObservableCollection<SpendDataSearch>(db.Spends
                .Where(i => i.Date >= StartDate && i.Date <= EndDate)
                .Select(i => new SpendDataSearch(i.Cost, i.SpendCategory.Name, i.Date))
                .ToList());
            OnPropertyChanged(nameof(SpendSearch));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
