<<<<<<< HEAD
﻿using CommunityToolkit.Mvvm.ComponentModel;
using FoodApp.UIElements;
using static FoodApp.Database;
=======
﻿using static FoodApp.Menu;
>>>>>>> origin/main

namespace FoodApp.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        public List<IFood> MenuItems { get; set; }

        public List<FoodsCategorized> MenuCategorized { get; set; }

<<<<<<< HEAD
        private Database _menu;

        [ObservableProperty]
        private Database.MenuItem _lastOrder;

        [ObservableProperty]
        private bool _hasLastOrder;

        public MainViewModel()
        {
            _menu = new Database();
=======
        private Menu _menu;
        public MainViewModel()
        {
            _menu = new Menu();
>>>>>>> origin/main
            MenuItems = _menu.GetMenuItems();
            CategorizeMenuItems();

            Task.Run(async () => await GetLastOrderAsync());
        }

        private void CategorizeMenuItems()
        {
            var grouped = MenuItems
                .GroupBy(item => item.CategoryTitle)
                .Select((itemFromOneCategory) => new FoodsCategorized(itemFromOneCategory.Key, itemFromOneCategory.ToList()))
                .ToList();

            MenuCategorized = grouped;
        }

        public async Task GetLastOrderAsync()
        {
            var lastOrder = await Database.LoadLastOrderAsync();
            if (lastOrder != null)
            {
                LastOrder = lastOrder;
                HasLastOrder = true;
            }
            else
            {
                HasLastOrder = false;
            }
        }

    }
}
