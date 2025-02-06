
namespace FoodApp.UIElements
{
    public class ElementFunction
    {
        public T FindElementByName<T>(StackLayout parentLayout, string controlName)
        {
            return parentLayout.FindByName<T>(controlName);
        }

        public async Task ClearStatusLabel(Label statusLabel, int delayMilliseconds = 2000)
        {
            await Task.Delay(delayMilliseconds);
            statusLabel.Text = "";
        }
    }
}
