using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp
{
    public class Database
    {
        public interface IFood
        {
            string CategoryTitle { get; set; }
            string FoodTitle { get; set; }
        }

        public List<IFood> GetDB()
        {
            return new List<IFood>
            {
                new MenuItem("Pizza", "Margarita"),
                new MenuItem("Pizza", "Special"),
                new MenuItem("Pizza", "Pepperoni"),
                new MenuItem("Burger", "Cheeseburger"),
                new MenuItem("Burger", "Bacon Cheese"),
                new MenuItem("Burger", "Crispy Chicken"),
                new MenuItem("Pasta", "Napolitana"),
                new MenuItem("Pasta", "Carbonara")
            };
        }

        public class MenuItem : IFood
        {
            public string CategoryTitle { get; set; }
            public string FoodTitle { get; set; }

            public MenuItem(string category, string foodTitle)
            {
                CategoryTitle = category;
                FoodTitle = foodTitle;
            }
        }

        public class FoodsCategorized : List<IFood>
        {
            public string CategoryTitle { get; set; }

            public FoodsCategorized(string category, List<IFood> foods) : base(foods)
            {
                CategoryTitle = category;
            }
        }

    }
}
