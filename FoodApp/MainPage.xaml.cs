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
            InitializeComponent();

            Database db = new Database();
            var options = db.GetDB();


            // Display menu based on Item Category
            Options = options.GroupBy(item => item.CategoryTitle)
                             .Select(categoryTitle => new Database.FoodsCategorized(categoryTitle.Key, categoryTitle.ToList()))
                             .ToList();

            BindingContext = this;


        }

        public async void OnOrderClicked(object sender, EventArgs e)
        {
            ControlLabel lb = new ControlLabel();
            Meal meal = new Meal();
            var cancellationTokenSource = new CancellationTokenSource();

            Button orderButton = (Button)sender;
            StackLayout parentLayout = (StackLayout)orderButton.Parent.Parent;
            Button cancelButton = lb.FindElement<Button>(parentLayout, "CancelButton");
            Button collectButton = lb.FindElement<Button>(parentLayout, "CollectButton");
            Label statusLabel = lb.FindElement<Label>(parentLayout, "StatusLabel");


            //Make cancel button available
            cancelButton.IsVisible = true;
            orderButton.IsVisible = false;

            cancellationTokens[orderButton] = cancellationTokenSource;

            //Order Pressed
            statusLabel.Text = "Your meal is now being prepared.";
            //Debug.WriteLine("Order is pressed.");

            bool isPrepared = await meal.PrepareMealAsync(cancellationTokenSource.Token);

            if (isPrepared)
            {
                //Meal is ready 
                cancelButton.IsVisible = false;
                collectButton.IsVisible = true;
                statusLabel.Text = "Your meal is ready.";

                //Debug.WriteLine("Meal is ready.");
            }
        }

        public void OnCancelClicked(object sender, EventArgs e)
        {
            ControlLabel lb = new ControlLabel();

            Button cancelButton = (Button)sender;
            StackLayout parentLayout = (StackLayout)cancelButton.Parent.Parent;
            Button orderButton = lb.FindElement<Button>(parentLayout, "OrderButton");
            Label statusLabel = lb.FindElement<Label>(parentLayout, "StatusLabel");

            //Make Order button available 
            cancelButton.IsVisible = false;
            orderButton.IsVisible = true;

            if (cancellationTokens.Remove(orderButton, out var cancellationTokenSource))
            {
                //Cancel is pressed
                cancellationTokenSource.Cancel();
                statusLabel.Text = "Meal preparation was canceled.";
                lb.ClearStatusLabel(statusLabel);

                //Debug.WriteLine("Cancel is pressed.");
            }
        }

        public void OnCollectClicked(object sender, EventArgs e)
        {
            ControlLabel lb = new ControlLabel();

            Button collectButton = (Button)sender;
            StackLayout parentLayout = (StackLayout)collectButton.Parent.Parent;
            Button orderButton = lb.FindElement<Button>(parentLayout, "OrderButton");
            Label statusLabel = lb.FindElement<Label>(parentLayout, "StatusLabel");
                
            //Collect is pressed

            collectButton.IsVisible = false;
            orderButton.IsVisible = true;

            statusLabel.Text = "Thank you, enjoy!";
            lb.ClearStatusLabel(statusLabel);

            //Debug.WriteLine("Collect is pressed.");
        }
    }
}
