
namespace FoodApp
{
    public class MealPreparation
    {
        public async Task<bool> PrepareMealAsync(int PreparationTimeInSec, CancellationToken cancellationToken)
        {
            try
            {
                for (int i = 0; i < PreparationTimeInSec; i++)
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
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return false;
            }
        }
    }
}




