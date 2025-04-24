using Application.Abstractions.Services.Configuration;
using Application.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MontelaApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="Admin")]
    public class ApplicationServicesController : ControllerBase
    {
        readonly IAuthorizeDefinitionService _authorizationDefinitionService;

        public ApplicationServicesController(IAuthorizeDefinitionService authorizationDefinitionService)
        {
            _authorizationDefinitionService = authorizationDefinitionService;
        }

        [HttpGet]
        [AuthorizeDefinition(ActionType = Application.Enums.ActionType.Reading, Definition ="Get Authorize Definition Endpoint", Menu = "Application Services")]
        public IActionResult GetAuthorizeDefinitionEndpoints()
        {
            var datas = _authorizationDefinitionService.GetAuthorizeDefinitionEndpoints(typeof(Program));

            return Ok(datas);
        }
    }
}
