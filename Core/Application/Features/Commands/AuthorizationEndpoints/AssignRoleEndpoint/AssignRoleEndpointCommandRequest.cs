using MediatR;

namespace Application.Features.Commands.AuthorizationEndpoints.AssignRole
{
    public class AssignRoleEndpointCommandRequest : IRequest<AssignRoleEndpointCommandResponse>
    {
        public string[] Roles { get; set; }
        public string EndpointCode { get; set; }
        public string Menu { get; set; }
        public Type Type { get; set; }
    }
}