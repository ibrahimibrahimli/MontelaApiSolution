namespace Application.DTOs.Order
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string Adress { get; set; }
        public DateTime CreatedDate { get; set; }
        public object BasketItems{ get; set; }
        public string Description{ get; set; }
        public string OrderNumber{ get; set; }
    }
}
