using Microsoft.Maui.Controls;

namespace FoodApp
{
    public partial class WelcomePageViewModel : ContentPage
    {
        public WelcomePageViewModel()
        {
            // Hide the navigation bar
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();
        }

        private async void OnButtonClicked(object sender, EventArgs e)
        {

            LoadingIndicator.IsVisible = true;
            LoadingIndicator.IsRunning = true;

            // Navigate to MainPage when button is clicked
            await Navigation.PushAsync(new MainPageViewModel());

            LoadingIndicator.IsVisible = false;
            LoadingIndicator.IsRunning = false;
        }
    }
}
