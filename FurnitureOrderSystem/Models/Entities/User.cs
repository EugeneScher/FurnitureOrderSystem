using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class User : IdentityUser
{
    // Персональные данные
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }

    // Профессиональная информация
    [MaxLength(100)]
    public string Position { get; set; }

    [MaxLength(100)]
    public string Department { get; set; }

    // Административные поля
    public bool IsActive { get; set; } = true;
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    public DateTime? LastLoginDate { get; set; }

    // Навигационные свойства
    public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
    public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    public virtual ICollection<IdentityUserToken<string>> Tokens { get; set; }

    // Вспомогательные методы
    public string FullName => $"{FirstName} {LastName}";
}
