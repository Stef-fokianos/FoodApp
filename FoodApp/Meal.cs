using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp
{
    class Meal
    {
        public async Task<bool> PrepareMealAsync(CancellationToken cancellationToken)
        {
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        return false;
                    }
                    await Task.Delay(1000);
                }
                return true;
            }
            catch (OperationCanceledException)
            {
                return false;
            }
        }
    }
}
