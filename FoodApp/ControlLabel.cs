using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp
{
    
    class ControlLabel
    {
        public async void ClearStatusLabel(Label statusLabel, int delayMilliseconds = 3000)
        {
            await Task.Delay(delayMilliseconds);
            statusLabel.Text = "";
        }


        public T FindElement<T>(StackLayout parentLayout, string controlName)
        {
            return parentLayout.FindByName<T>(controlName);
        }

    }
}
