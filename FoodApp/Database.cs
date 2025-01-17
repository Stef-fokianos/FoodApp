using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace FoodApp
{
    public class Database
    {
        private SQLiteConnection db;
        private string dbPath;

        public Database()
        {
            try
            {
                dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "foodapp.db3");
                db = new SQLiteConnection(dbPath);
                db.CreateTable<MenuItem>();
                Debug.WriteLine("Database created successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error accessing database: {ex.Message}");
            }
        }

        public void DeleteDatabase()
        {
            if (File.Exists(dbPath))
            {
                File.Delete(dbPath);
                Debug.WriteLine("Database deleted.");
            }
            else
            {
                Debug.WriteLine("Database file not found.");
            }
        }

        public class MenuItem 
        {
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }

            public string CategoryTitle { get; set; }
            public string FoodTitle { get; set; }



        }

        public List<FoodsCategorized> GetItemsGroupedByCategory()
        {
            var items = db.Table<MenuItem>().ToList();

            var groupedItems = items
                .GroupBy(item => item.CategoryTitle)
                .Select(group => new FoodsCategorized(group.Key, group.ToList()))
                .ToList();

            return groupedItems;
        }

        public int SaveItem(MenuItem item)
        {
            if (item.Id != 0)
                return db.Update(item); 
            else
                return db.Insert(item);  
        }


        public class FoodsCategorized
        {
            public string CategoryTitle { get; set; }
            public List<MenuItem> Items { get; set; }

            public FoodsCategorized(string categoryTitle, List<MenuItem> items)
            {
                CategoryTitle = categoryTitle;
                Items = items;
            }
        }

        public void AddData()
        {


            try
            {
                SaveItem(new MenuItem { CategoryTitle = "Pizza", FoodTitle = "Margarita" });
                SaveItem(new MenuItem { CategoryTitle = "Pizza", FoodTitle = "Special" });
                SaveItem(new MenuItem { CategoryTitle = "Pizza", FoodTitle = "Pepperoni" });
                SaveItem(new MenuItem { CategoryTitle = "Burger", FoodTitle = "Cheeseburger" });
                SaveItem(new MenuItem { CategoryTitle = "Burger", FoodTitle = "Bacon Cheese" });
                SaveItem(new MenuItem { CategoryTitle = "Burger", FoodTitle = "Crispy Chicken" });
                SaveItem(new MenuItem { CategoryTitle = "Pasta", FoodTitle = "Napolitana" });
                SaveItem(new MenuItem { CategoryTitle = "Pasta", FoodTitle = "Carbonara" });

                Debug.WriteLine("Data added to the database successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error adding data: {ex.Message}");
            }

        }
    }

}
