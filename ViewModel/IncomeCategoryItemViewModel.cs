using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Version3.Models;
using Version3.Helper;

namespace Version3.ViewModel
{
    internal class IncomeCategoryItemViewModel
    {
        BochagovaKursovikContext db = new BochagovaKursovikContext();

        public ObservableCollection<IncCategoryItem> ListIncCategoryItems =
            new ObservableCollection<IncCategoryItem>();
        public ObservableCollection<Income> ListIncome { get; set; } =
            new ObservableCollection<Income>();
        public ObservableCollection<IncomeCategory> ListIncCategory { get; set; } =
                new ObservableCollection<IncomeCategory>();
        public IncomeCategoryItemViewModel() {
            ListIncCategoryItems = new ObservableCollection<IncCategoryItem>();
            ListIncome = new ObservableCollection<Income>(db.Incomes.ToList());
            ListIncCategory = new ObservableCollection<IncomeCategory>(db.IncomeCategories.ToList());
            CalculateDiagramm();
        }
        public void CalculateDiagramm()
        {
            decimal totalCost = ListIncome.Sum(i => i.Cost);

            foreach (var inccategory in ListIncCategory)
            {
                decimal categoryCost = ListIncome.Where(i => i.IncomeCategoryId == inccategory.IncomeCategoryId).Sum(i => i.Cost);
                decimal percentage = Math.Round((categoryCost / totalCost) * 100, 2);
                ListIncCategoryItems.Add(new IncCategoryItem(inccategory.Name, categoryCost, percentage));
            }
        }
    }
}
