using FurnitureOrderSystem.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using System.Windows;

namespace FurnitureOrderSystem.Models.ViewModels.Account
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IAuthService _authService;
        private readonly NavigationService _navigationService;

        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        private string _password;

        public LoginViewModel(IAuthService authService, NavigationService navigationService)
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

            bool success = await _authService.LoginAsync(Username, Password);
            if (success)
            {
                _navigationService.ShowMainWindow();
            }
            else
            {
                MessageBox.Show("Ошибка входа");
            }
        }

        [RelayCommand]
        private void NavigateToRegister() => _navigationService.ShowRegisterWindow();
    }
}