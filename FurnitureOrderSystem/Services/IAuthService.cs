using System.Threading.Tasks;

public interface IAuthService
{
    Task<bool> LoginAsync(string username, string password);
    Task<bool> RegisterAsync(string username, string email, string password, string firstName, string lastName);
    Task LogoutAsync();
    bool IsAuthenticated();
    string GetCurrentUsername();
}