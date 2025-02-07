using FoodApp.ViewModels;

namespace FoodApp.Views;


public partial class WelcomePage : ContentPage
{
	public WelcomePage()
	{
        // Hide the navigation bar
        NavigationPage.SetHasNavigationBar(this, false);


        InitializeComponent();
		BindingContext = new WelcomeViewModel();
	}
}