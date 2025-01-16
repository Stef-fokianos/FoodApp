using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp
{
    class CustomButton : Button
    {

        public CustomButton()
        {
            this.CornerRadius = 10;
        }

 

      
    }

    class CustomCancelButton : CustomButton
    {

        public CustomCancelButton()
        {
            this.Text = "Cancel";
            this.BackgroundColor = Color.FromRgb(255, 0, 0);
            this.TextColor = Color.FromRgb(255, 255, 255);


            this.Pressed += OnPressed;
            this.Released += OnReleased;
        }


        public void OnPressed(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.FromRgb(0, 0, 0);
        }

        public void OnReleased(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.FromRgb(255, 0, 0);
        }
    }
}
