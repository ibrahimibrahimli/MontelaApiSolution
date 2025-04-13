using Application.Abstractions.Services.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace MontelaApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationServicesController : ControllerBase
    {
        readonly IAuthorizeDefinitionService _authorizationDefinitionService;

        public ApplicationServicesController(IAuthorizeDefinitionService authorizationDefinitionService)
        {
            _authorizationDefinitionService = authorizationDefinitionService;
        }

        [HttpGet]
        public IActionResult GetAuthorizeDefinitionEndpoints()
        {
            var datas = _authorizationDefinitionService.GetAuthorizeDefinitionEndpoints(typeof(Program));

            return Ok(datas);
        }
    }
}
