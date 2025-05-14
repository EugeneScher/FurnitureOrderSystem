using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using System.Windows;

public partial class RegisterViewModel : ObservableObject
{
    private readonly IAuthService _authService;

    [ObservableProperty]
    private string _username;

    [ObservableProperty]
    private string _email;

    [ObservableProperty]
    private string _password;

    [ObservableProperty]
    private string _confirmPassword;

    [ObservableProperty]
    private string _firstName;

    [ObservableProperty]
    private string _lastName;

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

        var result = await _authService.RegisterAsync(Username, Email, Password, FirstName, LastName);
        if (result)
        {
            MessageBox.Show("Регистрация успешна!");
            // Переход на главное окно
        }
        else
        {
            MessageBox.Show("Ошибка регистрации");
        }
    }
}