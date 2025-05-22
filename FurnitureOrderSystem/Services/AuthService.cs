using FurnitureOrderSystem.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FurnitureOrderSystem.Services;

public class AuthService : IAuthService
{
    public bool IsAuthenticated => throw new System.NotImplementedException();

    public string CurrentUsername => CurrentUser.Identity!.Name ?? string.Empty;

    public ClaimsPrincipal CurrentUser => _signInManager.Context.User;

    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public AuthService(SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<AuthResult> LoginAsync(string username, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(username, password, false, false);

        string error = string.Empty;
        if (!result.Succeeded)
        {
            if (result.IsNotAllowed)
                error = "is not allowed";
            else if (result.IsLockedOut)
                error = "locked out";
            else if (result.RequiresTwoFactor)
                error = "requires two factor";
        }

        return new AuthResult(result.Succeeded, error);
    }

    public async Task<AuthResult> RegisterAsync(UserRegistrationDto userDto)
    {
        var user = new User
        {
            UserName = userDto.Username,
            Email = userDto.Email,
            FirstName = userDto.FirstName,
            LastName = userDto.LastName
        };

        IdentityResult result = await _userManager.CreateAsync(user, userDto.Password);

        string error = string.Empty;
        if (!result.Succeeded && result.Errors.Any())
            error = string.Join(", ", result.Errors);

        if (result.Succeeded)
            await _signInManager.SignInAsync(user, isPersistent: false);

        return new AuthResult(result.Succeeded, error);
    }

    public async Task<AuthResult> ResetPasswordAsync(string token, string newPassword)
    {
        User? user = await _userManager.GetUserAsync(CurrentUser);
        if (user == null)
            return new AuthResult(false, "User not found.");

        IdentityResult result = await _userManager.ResetPasswordAsync(user, token, newPassword);

        string error = string.Empty;
        if (!result.Succeeded && result.Errors.Any())
            error = string.Join(", ", result.Errors);

        return new AuthResult(result.Succeeded, error);
    }

    public async Task<bool> ChangePasswordAsync(string oldPassword, string newPassword)
    {
        User? user = await _userManager.GetUserAsync(CurrentUser);
        if (user == null)
            return false;

        IdentityResult result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        return result.Succeeded;
    }

    public Task SendPasswordResetEmailAsync(string email)
    {
        throw new System.NotImplementedException(); // TODO: Make a send Email message
    }
    
    public async Task LogoutAsync() => await _signInManager.SignOutAsync();
}