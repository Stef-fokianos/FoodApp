
namespace FoodApp.UIElements
{
    public class CustomButton : Button
    {
        public CustomButton()
        {
            this.CornerRadius = 10;
            this.Padding = 10;
        }
    }

    public class CustomMenuButton : CustomButton
    {
        private Color _defaultColor;
        private Color _pressedColor;

        public CustomMenuButton()
        {
            this.MinimumHeightRequest = 45;
            this.HorizontalOptions = LayoutOptions.End;
            this.VerticalOptions = LayoutOptions.Center;
            this.Margin = new Thickness(0, 0, 0, -10);

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
        private Meal meal = new Meal();
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
            await meal.OnOrderClickedAsync(sender);
        }
    }


    class CustomCancelButton : CustomMenuButton
    {
        Meal meal = new Meal();
        public CustomCancelButton()
        {
            this.Text = "Cancel";
            this.TextColor = Color.FromRgb(255, 255, 255);

            SetButtonColor(Color.FromRgb(255, 0, 0), Color.FromRgb(180, 80, 80));
        }
        public override void OnClicked(object sender, EventArgs e)
        {
            meal.OnCancelClicked(sender);
        }
    }


    class CustomCollectButton : CustomMenuButton
    {
        Meal meal = new Meal();
        public CustomCollectButton()
        {
            this.Text = "Collect";
            this.BackgroundColor = Color.FromRgb(0, 128, 0);
            this.TextColor = Color.FromRgb(255, 255, 255);

            SetButtonColor(Color.FromRgb(0, 128, 0), Color.FromRgb(40, 80, 40));
        }
        public override void OnClicked(object sender, EventArgs e)
        {
            meal.OnCollectClicked(sender);
        }

    }
}









