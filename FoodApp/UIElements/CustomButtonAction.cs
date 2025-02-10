
namespace FoodApp.UIElements
{
    public class CustomButtonAction
    {
        ElementFunction _elementFunction = new ElementFunction();
        MealPreparation _mealPreparation = new MealPreparation();

        public async Task OnOrderClickedAsync(object sender)
        {
            CustomOrderButton orderButton = sender as CustomOrderButton;
            StackLayout parentLayout = orderButton?.Parent?.Parent as StackLayout;

            CustomCancelButton cancelButton = _elementFunction.FindElementByName<CustomCancelButton>(parentLayout, "CancelButton");
            CustomCollectButton collectButton = _elementFunction.FindElementByName<CustomCollectButton>(parentLayout, "CollectButton");
            Label statusLabel = _elementFunction.FindElementByName<Label>(parentLayout, "StatusLabel");

            //Find menuItem's title and image path to keep track of last order
            var menuItem = orderButton?.BindingContext as Database.MenuItem;
            Label lastFoodTitle = _elementFunction.FindElementByName<Label>(parentLayout, "FoodTitle");
            Image lastImage = _elementFunction.FindElementByName<Image>(parentLayout, "Image");
            

            //Make Cancel button available 
            cancelButton.IsVisible = true;
            orderButton.IsVisible = false;

            statusLabel.Text = "Your meal is now being prepared.";

            orderButton.CancellationTokenSource = new CancellationTokenSource();

            //Start meal preparation
            bool isPrepared = await _mealPreparation.PrepareMealAsync(menuItem.PreparationTimeInSec ,orderButton.CancellationTokenSource.Token);

            if (isPrepared)
            {
                //Meal is ready 
                cancelButton.IsVisible = false;
                collectButton.IsVisible = true;

                //Used to save last ordered item
                string foodTitle = lastFoodTitle.Text;
                string imageSource = lastImage.Source.ToString();

                var lastOrder = new Database.MenuItem(foodTitle, imageSource);
                await Database.SaveLastOrderAsync(lastOrder);

                statusLabel.Text = "Your meal is ready.";
            }
        }

        public void OnCancelClicked(object sender)
        {
            CustomCancelButton cancelButton = sender as CustomCancelButton;
            StackLayout parentLayout = cancelButton?.Parent?.Parent as StackLayout;

            CustomOrderButton orderButton = parentLayout != null ? _elementFunction.FindElementByName<CustomOrderButton>(parentLayout, "OrderButton") : null;
            Label statusLabel = parentLayout != null ? _elementFunction.FindElementByName<Label>(parentLayout, "StatusLabel") : null;

            //Make Order button available 
            cancelButton.IsVisible = false;
            orderButton.IsVisible = true;

            if (orderButton != null && orderButton.CancellationTokenSource != null)
            {
                //Cancel is pressed
                orderButton.CancellationTokenSource.Cancel();
                orderButton.CancellationTokenSource = null;

                cancelButton.IsVisible = false;
                orderButton.IsVisible = true;

                statusLabel.Text = "Meal preparation was canceled.";
                _elementFunction.ClearStatusLabel(statusLabel);
            }
        }

        public void OnCollectClicked(object sender)
        {
            CustomCollectButton collectButton = sender as CustomCollectButton;
            StackLayout parentLayout = collectButton?.Parent?.Parent as StackLayout;

            CustomOrderButton orderButton = parentLayout != null ? _elementFunction.FindElementByName<CustomOrderButton>(parentLayout, "OrderButton") : null;
            Label statusLabel = parentLayout != null ? _elementFunction.FindElementByName<Label>(parentLayout, "StatusLabel") : null;

            //Make Order button available
            collectButton.IsVisible = false;
            orderButton.IsVisible = true;

            statusLabel.Text = "Thank you, enjoy!";
            _elementFunction.ClearStatusLabel(statusLabel);
        }


    }
}

