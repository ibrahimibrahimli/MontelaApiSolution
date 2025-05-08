using Application.Features.Commands.AuthorizationEndpoints.AssignRole;
using Application.Features.Queries.AuthorizationEndpoints.GetRolesToEndpoint;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MontelaApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationEndpointsController : ControllerBase
    {
        readonly IMediator _mediator;

        public AuthorizationEndpointsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("getRolesToEndpoint}")]
        public async Task<IActionResult> GetRolesToEndpoint(GetRolesToEndpointQueryRequest request)
        {
            GetRolesToEndpointQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRoleEndpoint(AssignRoleEndpointCommandRequest request)
        {
            request.Type = typeof(Program);
            AssignRoleEndpointCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
