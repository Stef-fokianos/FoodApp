using static FoodApp.Database;

namespace FoodApp.ViewModels
{
    public partial class MainViewModel
    {
        public List<IFood> MenuItems { get; set; }

        public List<FoodsCategorized> MenuCategorized { get; set; }

        private Database _database;
        public MainViewModel()
        {
            _database = new Database();
            MenuItems = _database.GetDB();
            CategorizeMenuItems();
        }

        private void CategorizeMenuItems()
        {
            var grouped = MenuItems
                .GroupBy(item => item.CategoryTitle)
                .Select(itemFromOneCategory => new FoodsCategorized(itemFromOneCategory.Key, itemFromOneCategory.ToList()))
                .ToList();

            MenuCategorized = grouped;
        }    
    }
}
