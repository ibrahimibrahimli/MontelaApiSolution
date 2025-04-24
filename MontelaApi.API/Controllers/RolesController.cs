using Application.Attributes;
using Application.Enums;
using Application.Features.Commands.Role.CreateRole;
using Application.Features.Commands.Role.DeleteRole;
using Application.Features.Commands.Role.UpdateRole;
using Application.Features.Queries.Role.GetRoleById;
using Application.Features.Queries.Role.GetRoles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MontelaApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class RolesController : ControllerBase
    {
        readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get all Roles", Menu ="Roles" )]
        public async Task<IActionResult> GetRoles([FromQuery]GetRolesQueryRequest request)
        {
          GetRolesQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Role", Menu = "Roles")]
        public async Task<IActionResult> GetRole([FromRoute]GetRoleByIdQueryRequest request)
        {
            GetRoleByIdQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        [AuthorizeDefinition(ActionType = ActionType.Writing, Definition = "Create Role", Menu = "Roles")]
        public async Task<IActionResult> CreateRole([FromBody]CreateRoleCommandRequest request)
        {
            CreateRoleCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("{Id}")]
        [AuthorizeDefinition(ActionType = ActionType.Updating, Definition = "Update Role", Menu = "Roles")]
        public async Task<IActionResult> UpdateRole([FromBody, FromRoute]UpdateRoleCommandRequest request)
        {
            UpdateRoleCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("{name}")]
        [AuthorizeDefinition(ActionType = ActionType.Deleting, Definition = "Delete Role", Menu = "Roles")]
        public async Task<IActionResult> DeleteRole([FromRoute]DeleteRoleCommandRequest request)
        {
            DeleteRoleCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
