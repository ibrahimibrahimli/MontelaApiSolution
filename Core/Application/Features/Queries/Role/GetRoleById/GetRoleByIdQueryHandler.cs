using Application.Abstractions.Services;
using MediatR;

namespace Application.Features.Queries.Role.GetRoleById
{
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQueryRequest, GetRoleByIdQueryResponse>
    {
        readonly IRoleService _roleService;

        public GetRoleByIdQueryHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<GetRoleByIdQueryResponse> Handle(GetRoleByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var result= await _roleService.GetRoleById(request.Id);
            return new()
            {
                Id = result.id,
                Name = result.name
            };
        }
    }
}
