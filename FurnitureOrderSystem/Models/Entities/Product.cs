using System.Collections.Generic;

namespace FurnitureOrderSystem.Models.Entities;

public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? Category { get; set; }
    public string? ImagePath { get; set; }
    public int StockQuantity { get; set; }
    public int? CategoryId { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}