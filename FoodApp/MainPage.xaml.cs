using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodApp
{
    public partial class MainPage : ContentPage
    {
        private Dictionary<Button, CancellationTokenSource> cancellationTokens = new();
        public List<Database.FoodsCategorized> Options { get; set; }

        public MainPage()
        {
            

            Database db = new Database();

            //db.DeleteDatabase();

            Initialize();

            InitializeComponent();

            
        }

        private void Initialize()
        {
            Database db = new Database();

            try
            {
                // Fetch items from the database synchronously
                Options = db.GetItemsGroupedByCategory();

                // Log the Options object to check if it's populated
                Debug.WriteLine($"Total categories found: {Options.Count}");

                if (Options.Count == 0)
                {
                    Debug.WriteLine("Options is empty, adding data...");
                    db.AddData();  // Add data synchronously
                    Options = db.GetItemsGroupedByCategory();  // Fetch data again after adding it
                }

                // Log the contents of Options to verify data
                foreach (var category in Options)
                {
                    Debug.WriteLine($"Category: {category.CategoryTitle}, Items Count: {category.Items.Count}");
                    foreach (var item in category.Items)
                    {
                        Debug.WriteLine($"FoodTitle: {item.FoodTitle}");
                    }
                }

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    // After data is loaded, set the BindingContext
                    BindingContext = this;
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading data: {ex.Message}");
            }
        }

        public async void OnOrderClicked(object sender, EventArgs e)
        {
            ControlLabel lb = new ControlLabel();
            Meal meal = new Meal();
            var cancellationTokenSource = new CancellationTokenSource();

            Button orderButton = sender as Button;
            StackLayout parentLayout = orderButton?.Parent?.Parent as StackLayout;

            CustomCancelButton cancelButton = parentLayout != null ? lb.FindElement<CustomCancelButton>(parentLayout, "CancelButton") : null;
            Button collectButton = parentLayout != null ? lb.FindElement<Button>(parentLayout, "CollectButton") : null;
            Label statusLabel = parentLayout != null ? lb.FindElement<Label>(parentLayout, "StatusLabel") : null;

            //Used for debugging
            //if (orderButton == null || parentLayout == null || cancelButton == null || collectButton == null || statusLabel == null )
            //{
            //    Debug.WriteLine("Null check failed");
            //}
            //else
            //{
            //    Debug.WriteLine("Null check success");
            //}



            //Make cancel button available
            cancelButton.IsVisible = true;
            orderButton.IsVisible = false;

            cancellationTokens[orderButton] = cancellationTokenSource;

            //Order Pressed
            statusLabel.Text = "Your meal is now being prepared.";

            //Used for debugging
            //Debug.WriteLine("Order is pressed.");

            bool isPrepared = await meal.PrepareMealAsync(cancellationTokenSource.Token);

            if (isPrepared)
            {
                //Meal is ready 
                cancelButton.IsVisible = false;
                collectButton.IsVisible = true;
                statusLabel.Text = "Your meal is ready.";

                //Used for debugging
                //Debug.WriteLine("Meal is ready.");
            }
        }

        public void OnCancelClicked(object sender, EventArgs e)
        {
            ControlLabel lb = new ControlLabel();

            CustomCancelButton cancelButton = sender as CustomCancelButton;
            StackLayout parentLayout = cancelButton?.Parent?.Parent as StackLayout;

            Button orderButton = parentLayout != null ? lb.FindElement<Button>(parentLayout, "OrderButton") : null;
            Label statusLabel = parentLayout != null ? lb.FindElement<Label>(parentLayout, "StatusLabel") : null;

            //Used for debugging
            //if (cancelButton == null || parentLayout == null || orderButton == null || statusLabel == null )
            //{
            //    Debug.WriteLine("Null check failed");
            //}
            //else
            //{
            //    Debug.WriteLine("Null check success");
            //}


            //Make Order button available 
            cancelButton.IsVisible = false;
            orderButton.IsVisible = true;

            if (cancellationTokens.Remove(orderButton, out var cancellationTokenSource))
            {
                //Cancel is pressed
                cancellationTokenSource.Cancel();
                statusLabel.Text = "Meal preparation was canceled.";
                lb.ClearStatusLabel(statusLabel);

                //Used for debugging
                //Debug.WriteLine("Cancel is pressed.");
            }
        }

        public async void OnCollectClicked(object sender, EventArgs e)
        {
            ControlLabel lb = new ControlLabel();

            Button collectButton = sender as Button;
            StackLayout parentLayout = collectButton?.Parent?.Parent as StackLayout;

            Button orderButton = parentLayout != null ? lb.FindElement<Button>(parentLayout, "OrderButton") : null;
            Label statusLabel = parentLayout != null ? lb.FindElement<Label>(parentLayout, "StatusLabel") : null;
            
            //Used for debugging
            //if (collectButton == null || parentLayout == null || orderButton == null || statusLabel == null )
            //{
            //    Debug.WriteLine("Null check failed");
            //}
            //else
            //{
            //    Debug.WriteLine("Null check success");
            //}

            //Collect is pressed

            collectButton.IsVisible = false;
            orderButton.IsVisible = true;

            statusLabel.Text = "Thank you, enjoy!";
            lb.ClearStatusLabel(statusLabel);

            //Used for debugging
            //Debug.WriteLine("Collect is pressed.");
        }
    }
}
