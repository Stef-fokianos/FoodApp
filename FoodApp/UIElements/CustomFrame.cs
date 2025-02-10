using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.UIElements
{
    public class CustomFrame : Frame { }

    public class CustomMenuItemFrame :CustomFrame
    {
        public CustomMenuItemFrame()
        {
            this.BorderColor = Color.FromRgb(170, 170, 170);
            this.Padding = 5;
            this.Margin = 2;
            this.BackgroundColor = Color.FromRgb(0, 0, 0);
            this.WidthRequest = 312;
            this.HeightRequest = 280;
        }
    }
    
}
