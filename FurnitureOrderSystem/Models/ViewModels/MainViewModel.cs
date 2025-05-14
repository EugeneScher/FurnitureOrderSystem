using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FurnitureOrderSystem.Services;
using System.Windows; // Важная директива
using System.Linq;
using System.Threading.Tasks;

namespace FurnitureOrderSystem.Models.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IAuthService _authService;
        private readonly NavigationService _navigationService;

        [ObservableProperty]
        private string _currentUser;

        public MainViewModel(IAuthService authService, NavigationService navigationService)
        {
            _authService = authService;
            _navigationService = navigationService;
            CurrentUser = _authService.GetCurrentUsername();
        }

        [RelayCommand]
        private async Task Logout()
        {
            await _authService.LogoutAsync();
            _navigationService.ShowLoginWindow();
            Application.Current.Windows.OfType<MainWindow>().FirstOrDefault()?.Close();
        }
    }
}