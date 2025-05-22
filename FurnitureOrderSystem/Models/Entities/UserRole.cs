using Microsoft.AspNetCore.Identity;

namespace FurnitureOrderSystem.Models.Entities;

public class UserRole : IdentityRole
{
    public override string Id { get => base.Id; set => base.Id = value; }
    public override string? Name { get => base.Name; set => base.Name = value; }
    public override string? NormalizedName { get => base.NormalizedName; set => base.NormalizedName = value; }
}
