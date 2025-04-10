using MediatR;

namespace Application.Features.Commands.CompleteOrder
{
    public class CompleteOrderCommandRequest : IRequest<CompleteOrderCommandResponse>
    {
    }
}