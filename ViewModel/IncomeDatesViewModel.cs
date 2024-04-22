using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Version3.Models;

namespace Version3.ViewModel
{
    public class IncomeDatesViewModel : INotifyPropertyChanged
    {
        private DateTime _startDate;
        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; OnPropertyChanged(); LoadIncomesBetweenDates(); }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; OnPropertyChanged(); LoadIncomesBetweenDates(); }
        }

        private BochagovaKursovikContext db = new BochagovaKursovikContext();
        public ObservableCollection<IncomeDataSearch> IncomeSearch { get; set; }

        public IncomeDatesViewModel()
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            LoadIncomesBetweenDates();
        }
        private void LoadIncomesBetweenDates()
        {
            IncomeSearch = new ObservableCollection<IncomeDataSearch>(db.Incomes
                .Where(i => i.Date >= StartDate && i.Date <= EndDate)
                .Select(i => new IncomeDataSearch(i.Cost, i.IncomeCategory.Name, i.Date))
                .ToList());
            OnPropertyChanged(nameof(IncomeSearch));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
