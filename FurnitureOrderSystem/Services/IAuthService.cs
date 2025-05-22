using System.Security.Claims;
using System.Threading.Tasks;

public interface IAuthService
{
    // Основные методы аутентификации
    Task<AuthResult> LoginAsync(string username, string password);
    Task<AuthResult> RegisterAsync(UserRegistrationDto registrationData);
    Task LogoutAsync();

    // Проверка состояния
    bool IsAuthenticated { get; }
    string CurrentUsername { get; }
    ClaimsPrincipal CurrentUser { get; }

    // Дополнительные функции
    Task<bool> ChangePasswordAsync(string oldPassword, string newPassword);
    Task SendPasswordResetEmailAsync(string email);
    Task<AuthResult> ResetPasswordAsync(string token, string newPassword);
}

// Вспомогательные DTO и модели
public record AuthResult(bool Succeeded, string ErrorMessage = null);
public record UserRegistrationDto(
    string Username,
    string Email,
    string Password,
    string FirstName,
    string LastName
);
