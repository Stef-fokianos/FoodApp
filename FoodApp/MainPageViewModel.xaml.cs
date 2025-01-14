﻿using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodApp
{
    public partial class MainPageViewModel : ContentPage
    {
        private Dictionary<Button, CancellationTokenSource> cancellationTokens = new();
        public List<Database.FoodsCategorized> Options { get; set; }

        public MainPageViewModel()
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

            Button orderButton = sender as Button;
            StackLayout parentLayout = orderButton?.Parent?.Parent as StackLayout;

            Button cancelButton = parentLayout != null ? lb.FindElement<Button>(parentLayout, "CancelButton") : null;
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

            Button cancelButton = sender as Button;
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

        public void OnCollectClicked(object sender, EventArgs e)
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
