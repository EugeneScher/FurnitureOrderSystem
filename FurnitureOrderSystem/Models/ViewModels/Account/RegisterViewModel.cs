using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FurnitureOrderSystem.Services;
using System.Threading.Tasks;
using System.Windows;

namespace FurnitureOrderSystem.Models.ViewModels.Account;

public partial class RegisterViewModel : ObservableObject
{
    private readonly IAuthService _authService;

    [ObservableProperty]
    private string? _username;

    [ObservableProperty]
    private string? _email;

    [ObservableProperty]
    private string? _password;

    [ObservableProperty]
    private string? _confirmPassword;

    [ObservableProperty]
    private string? _firstName;

    [ObservableProperty]
    private string? _lastName;

    public RegisterViewModel(IAuthService authService)
    {
        _authService = authService;
    }

    [RelayCommand]
    private async Task Register()
    {
        if (Password != ConfirmPassword)
        {
            MessageBox.Show("Пароли не совпадают");
            return;
        }

        if (Username == null || Email == null || Password == null || FirstName == null || LastName == null)
        {
            MessageBox.Show("Проверьте правильность введенных данных.");
            return;
        }

        AuthResult authResult = await _authService.RegisterAsync(new (Username, Email, Password, FirstName, LastName));
        if (authResult.Succeeded)
        {
            MessageBox.Show("Регистрация успешна!");
            // Переход на главное окно
        }
        else
            MessageBox.Show("Ошибка регистрации");
    }
}