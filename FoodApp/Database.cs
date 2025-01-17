using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
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

        public class MenuItem : IFood
        {
            public string CategoryTitle { get; set; }
            public string FoodTitle { get; set; }

            public MenuItem()
            {

            }

            public MenuItem(string category, string foodTitle)
            {
                CategoryTitle = category;
                FoodTitle = foodTitle;
            }
        }

        public List<IFood> GetDB()
        {
            var menuItems = new List<IFood>();

            try
            {
                var assembly = Assembly.GetExecutingAssembly();

                using (Stream stream = assembly.GetManifestResourceStream("FoodApp.Resources.menu.json"))
                {
                    if (stream == null)
                    {
                        Debug.WriteLine("Failed to load menu.json from resources.");
                        return menuItems;
                    }

                    using (StreamReader reader = new StreamReader(stream))
                    {
                        var jsonData = reader.ReadToEnd();
                        var options = JsonSerializer.Deserialize<List<MenuItem>>(jsonData);

                        if (options != null)
                        {
                            menuItems.AddRange(options);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading food data: {ex.Message}");
            }

            return menuItems;
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
