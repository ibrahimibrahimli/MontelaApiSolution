namespace Application.Features.Queries.Order.GetOrderById
{
    public class GetOrderByIdQueryResponse
    {
        public string Adress { get; set; }
        public DateTime CreatedDate { get; set; }
        public object BasketItems { get; set; }
        public string Description { get; set; }
        public string OrderNumber { get; set; }
    }
}