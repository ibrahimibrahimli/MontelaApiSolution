using MediatR;

namespace Application.Features.Queries.Role.GetRoles
{
    public class GetRolesQueryRequest : IRequest<GetRolesQueryResponse>
    {
    }
}