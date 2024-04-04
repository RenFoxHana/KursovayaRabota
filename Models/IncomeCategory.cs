using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Version3.Models;

public partial class IncomeCategory : INotifyPropertyChanged
{
    public int IncomeCategoryId { get; set; }

    private string _name;
    public string Name
    {
        get { return _name; }
        set { _name = value; OnPropertyChanged("Name"); }
    }
    public IncomeCategory() { }
    public IncomeCategory(int incomeCategoryId, string name)
    {
        IncomeCategoryId = incomeCategoryId;
        Name = name;
    }
    public virtual ICollection<Income> Incomes { get; set; } = new List<Income>();
    public IncomeCategory ShallowCopy()
    {
        return (IncomeCategory)this.MemberwiseClone();
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
