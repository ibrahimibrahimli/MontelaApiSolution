namespace Application.DTOs.Order
{
    public class CompletedOrderDto
    {
        public string OrderCode { get; set; }
        public DateTime  Orderdate { get; set; }
        public string Username { get; set; }
        public string UserSurname { get; set; }
        public string Email { get; set; }
    }
}
