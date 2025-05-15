using System;
using System.Collections.Generic;

namespace FurnitureOrderSystem.Models.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public DateTime RegistrationDate { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}