
using System.Diagnostics;
using FoodApp.ViewModels;
using FoodApp.Views;

namespace FoodApp.UIElements
{
    public class CustomButton : Button
    {
        public CustomButton()
        {
            this.CornerRadius = 10;
        }
    }

    public class CustomWelcomeButton : CustomButton
    {
        public CustomWelcomeButton()
        {
            this.Text = "Take me there!";
            this.TextColor = Color.FromRgb(255, 255, 255);
            this.FontSize = 20;

            this.BackgroundColor = Color.FromRgba(255, 255, 255, 0);

            this.BorderWidth = 1;
            this.BorderColor = Color.FromRgb(255, 255, 255);

            this.HorizontalOptions = LayoutOptions.Center;
            this.VerticalOptions = LayoutOptions.Center;

            this.Clicked += OnClicked;
        }
        public async void OnClicked(object sender, EventArgs e)
        {
            if (BindingContext is WelcomeViewModel welcomeViewModel)
            {
                await welcomeViewModel.NavigateToMainPage();
            }
        }
    }

    public class CustomMenuButton : CustomButton
    {
        private Color _defaultColor;
        private Color _pressedColor;

        public CustomButtonAction _customButtonAction = new CustomButtonAction();

        public CustomMenuButton()
        {
            this.Margin = new Thickness(0, 0, 0, -10);
            this.MinimumHeightRequest = 45;
            this.Padding = 10;
            this.HorizontalOptions = LayoutOptions.End;
            this.VerticalOptions= LayoutOptions.End;

            this.Pressed += OnPressed;
            this.Released += OnReleased;
            this.Clicked += OnClicked;

        }

        public void SetButtonColor(Color defaultColor, Color pressedColor)
        {
            _defaultColor = defaultColor;
            _pressedColor = pressedColor;
            this.BackgroundColor = _defaultColor;
        }

        private void OnPressed(object sender, EventArgs e) => this.BackgroundColor = _pressedColor;
        private void OnReleased(object sender, EventArgs e) => this.BackgroundColor = _defaultColor;

        public virtual void OnClicked(object sender, EventArgs e)
        {
            Console.WriteLine("MenuButtonClicked");
        }
    }

    class CustomOrderButton : CustomMenuButton
    {
        public CancellationTokenSource CancellationTokenSource { get; set; }
        public CustomOrderButton()
        {
            this.Text = "Order";
            this.TextColor = Color.FromRgb(255, 255, 255);

            SetButtonColor(Color.FromRgb(80, 80, 80), Color.FromRgb(40, 40, 40));
        }
        public override void OnClicked(object sender, EventArgs e)
        {
            Task task = OnClickedAsync(sender);
        }

        public async Task OnClickedAsync(object sender)
        {
            await _customButtonAction.OnOrderClickedAsync(sender);
        }
    }


    class CustomCancelButton : CustomMenuButton
    {
        public CustomCancelButton()
        {
            this.Text = "Cancel";
            this.TextColor = Color.FromRgb(255, 255, 255);

            SetButtonColor(Color.FromRgb(255, 0, 0), Color.FromRgb(180, 80, 80));
        }
        public override void OnClicked(object sender, EventArgs e)
        {
            _customButtonAction.OnCancelClicked(sender);
        }
    }


    class CustomCollectButton : CustomMenuButton
    {
        public CustomCollectButton()
        {
            this.Text = "Collect";
            this.BackgroundColor = Color.FromRgb(0, 128, 0);
            this.TextColor = Color.FromRgb(255, 255, 255);

            SetButtonColor(Color.FromRgb(0, 128, 0), Color.FromRgb(40, 80, 40));
        }
        public override void OnClicked(object sender, EventArgs e)
        {
            _customButtonAction.OnCollectClicked(sender);
        }

    }
}









