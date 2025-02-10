using System.Diagnostics;
using System.Reflection;
using System.Text.Json;

namespace FoodApp
{
    public class Database
    {
        public interface IFood
        {
            string CategoryTitle { get; set; }
            string FoodTitle { get; set; }
            int PreparationTimeInSec { get; set; }
            public string Image { get; set; }
        }

        public class MenuItem : IFood
        {
            public string CategoryTitle { get; set; }
            public string FoodTitle { get; set; }
            public int PreparationTimeInSec { get; set; }
            public string Image { get; set; }


            public MenuItem() { }

            public MenuItem(string foodTitle, string image)
            {
                FoodTitle = foodTitle;
                Image = image;
            }

            public MenuItem(string category, string foodTitle, int preparationTimeInSec, string image)
            {
                CategoryTitle = category;
                FoodTitle = foodTitle;
                PreparationTimeInSec = preparationTimeInSec;
                Image = image;
            }
        }

        public List<IFood> GetMenuItems()
        {
            var menuItems = new List<IFood>();
            try
            {
                var assembly = Assembly.GetExecutingAssembly();

                using (Stream stream = assembly.GetManifestResourceStream("FoodApp.Data.menu.json"))
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



        private static readonly string lastOrderFilePath = Path.Combine(FileSystem.AppDataDirectory,"Data", "last_order.json");

        public static async Task SaveLastOrderAsync(Database.MenuItem lastOrder)
        {
            try
            {
                if (!Directory.Exists(lastOrderFilePath))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(lastOrderFilePath));
                }

                if (lastOrder.Image.StartsWith("File:"))
                {
                    lastOrder.Image = lastOrder.Image.Replace("File:", "").Trim();
                }

                string json = JsonSerializer.Serialize(lastOrder, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(lastOrderFilePath, json);
                //Debug.WriteLine($"File saved in : {lastOrderFilePath}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error while saving order in json file : {ex.Message}");
            }
        }


        public static async Task<MenuItem> LoadLastOrderAsync()
        {
            try
            {
                if (!File.Exists(lastOrderFilePath))
                    return null;

                string json = await File.ReadAllTextAsync(lastOrderFilePath);
                //Debug.WriteLine("File Loaded");
                return JsonSerializer.Deserialize<MenuItem>(json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error while loading order from json file : {ex.Message}");
                return null;
            }

        }

    }
}
