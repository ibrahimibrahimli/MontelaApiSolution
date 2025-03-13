using Application.RequestParameters;
using MediatR;

namespace Application.Features.Queries.GetAllProduct
{
    public class GetAllProductQueryRequest : IRequest<GetAllProductQueryResponse>
    {
        public int Page { get; set; }
        public int Size { get; set; } = 5;
        // public Pagination Pagination { get; set; }
    }
}
