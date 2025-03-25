using Domain.Entities;

namespace Application.DTOs.Order
{
    public class CreateOrderDto
    {
        public string? BasketId { get; set; }
        public string Description { get; set; }
        public string Adress { get; set; }
    }
}
