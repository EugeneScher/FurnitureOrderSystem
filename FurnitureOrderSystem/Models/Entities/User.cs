using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Position { get; set; } // Должность (админ, менеджер и т.д.)
}