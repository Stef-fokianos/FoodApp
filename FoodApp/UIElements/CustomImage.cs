using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.UIElements
{
    public class CustomImage : Image { }

    public class CustomMenuItemImage : CustomImage
    {
       public CustomMenuItemImage()
        {
            this.HorizontalOptions = LayoutOptions.Center;
            this.VerticalOptions = LayoutOptions.Start;
            this.WidthRequest = 300;
            this.HeightRequest = 200;
            this.Margin = new Thickness(0, 10, 0, 0);
        }
    }
}
