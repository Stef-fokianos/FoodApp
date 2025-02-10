using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FoodApp.Views;

namespace FoodApp.ViewModels;

public partial class WelcomeViewModel : ObservableObject
{
    [ObservableProperty]
    private bool isLoading = false;

    [RelayCommand]
    public async Task NavigateToMainPage()
    {
        IsLoading = true;

        //Without delay activity indicator does not display on windows
        await Task.Delay(100);

        await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
        IsLoading = false;
<<<<<<< HEAD
    }  
=======
    }

    
>>>>>>> origin/main
}