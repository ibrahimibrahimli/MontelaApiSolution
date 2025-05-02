using Microsoft.AspNetCore.Mvc;

namespace MontelaApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationEndpointsController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AssignRoleEndpoint()
        {

        }
    }
}
