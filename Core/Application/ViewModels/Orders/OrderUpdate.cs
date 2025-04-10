﻿using Domain.Entities;

namespace Application.ViewModels.Orders
{
    public class OrderUpdate
    {
        public Guid CustomerId { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public Customer Customer { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
