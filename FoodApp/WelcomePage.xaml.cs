using Microsoft.Maui.Controls;

namespace FoodApp
{
    public partial class WelcomePage: ContentPage
    {
        public WelcomePage()
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
            await Navigation.PushAsync(new MainPage());

            LoadingIndicator.IsVisible = false;
            LoadingIndicator.IsRunning = false;
        }
    }
}
