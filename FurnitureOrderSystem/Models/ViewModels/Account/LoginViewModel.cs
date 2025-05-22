using FurnitureOrderSystem.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using System.Windows;

namespace FurnitureOrderSystem.Models.ViewModels.Account;

public partial class LoginViewModel : ObservableObject
{
    private readonly IAuthService _authService;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    private string? _username;

    [ObservableProperty]
    private string? _password;

    public LoginViewModel(IAuthService authService, INavigationService navigationService)
    {
        _authService = authService;
        _navigationService = navigationService;
    }

    [RelayCommand]
    private async Task Login()
    {
        if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
        {
            MessageBox.Show("Заполните все поля");
            return;
        }

        AuthResult authResult = await _authService.LoginAsync(Username, Password);

        if (authResult.Succeeded)
            _navigationService.ShowMainWindow();
        else
            MessageBox.Show("Ошибка входа: " + authResult.ErrorMessage);
    }

    [RelayCommand]
    private void NavigateToRegister() => _navigationService.ShowRegisterWindow();
}