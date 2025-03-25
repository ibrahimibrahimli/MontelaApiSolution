using MediatR;

namespace Application.Features.Commands.Order.CreateOrder
{
    public class CreateOrderCommandRequest : IRequest<CreateOrderCommandResponse>
    {
        public string Description { get; set; }
        public string Adress { get; set; }
    }
}