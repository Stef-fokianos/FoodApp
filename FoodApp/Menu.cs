using System.Diagnostics;
using System.Reflection;
using System.Text.Json;


namespace FoodApp
{
    public class Menu
    {
        public interface IFood
        {
            string CategoryTitle { get; set; }
            string FoodTitle { get; set; }
            int PreparationTime { get; set; }
            public string Image { get; set; }
        }

        public class MenuItem : IFood
        {
            public string CategoryTitle { get; set; }
            public string FoodTitle { get; set; }
            public int PreparationTime { get; set; }
            public string Image { get; set; }


            public MenuItem(){}

            public MenuItem(string category, string foodTitle, int preparationTime, string image)
            {
                CategoryTitle = category;
                FoodTitle = foodTitle;
                PreparationTime = preparationTime;
                Image = image;
            }
        }

        public List<IFood> GetMenuItems()
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
