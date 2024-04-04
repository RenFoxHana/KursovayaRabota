using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Version3.Helper
{
    public class IncCategoryItem
    {
        public string Name { get; set; }
        public decimal CategoryCost { get; set; }
        public decimal Percentage { get; set; }

        public IncCategoryItem(string name, decimal categoryCost, decimal percentage)
        {
            Name = name;
            CategoryCost = categoryCost;
            Percentage = percentage;
        }
    }
}
