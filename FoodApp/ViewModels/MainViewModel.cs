using static FoodApp.Menu;

namespace FoodApp.ViewModels
{
    public partial class MainViewModel
    {
        public List<IFood> MenuItems { get; set; }

        public List<FoodsCategorized> MenuCategorized { get; set; }

        private Menu _menu;
        public MainViewModel()
        {
            _menu = new Menu();
            MenuItems = _menu.GetMenuItems();
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
