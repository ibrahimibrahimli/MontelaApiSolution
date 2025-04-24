using Application.Abstractions.Services;
using MediatR;

namespace Application.Features.Commands.Role.DeleteRole
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommandRequest, DeleteRoleCommandResponse>
    {
        readonly IRoleService _roleService;

        public DeleteRoleCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<DeleteRoleCommandResponse> Handle(DeleteRoleCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _roleService.DeleteRoleAsync(request.Name);
            return new()
            {
                Succedeed = result,
            };
        }
    }
}
