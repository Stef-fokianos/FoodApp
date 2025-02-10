using FoodApp.ViewModels;
using FoodApp.UIElements;

namespace FoodApp.Views;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();

        BindingContext = new MainViewModel();
    }
}
