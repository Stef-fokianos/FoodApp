using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp
{
    class CustomCancelButton : Button
    {

        public CustomCancelButton()
        {
            this.Text = "Cancel";
            this.BackgroundColor = Color.FromRgb(255, 0, 0);
            this.TextColor = Color.FromRgb(255, 255, 255);
            this.CornerRadius = 10;
        }
    }
}
