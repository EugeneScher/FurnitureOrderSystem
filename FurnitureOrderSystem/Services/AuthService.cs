using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

public class AuthService : IAuthService
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public AuthService(SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<bool> LoginAsync(string username, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(username, password, false, false);
        return result.Succeeded;
    }

    public async Task<bool> RegisterAsync(string username, string email, string password, string firstName, string lastName)
    {
        var user = new User
        {
            UserName = username,
            Email = email,
            FirstName = firstName,
            LastName = lastName
        };

        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            return true;
        }
        return false;
    }

    public async Task LogoutAsync() => await _signInManager.SignOutAsync();

    public bool IsAuthenticated() => _signInManager.IsSignedIn(_signInManager.Context.User);

    public string GetCurrentUsername() => _signInManager.Context.User.Identity.Name;
}