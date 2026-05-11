using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wpm.Management.Api.Application.Commands;

namespace Wpm.Management.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagementController(ManagementApplicationService appService) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Post(CreatePetCommand command)
        {
            await appService.Handle(command);
            return Ok();
        }
    }
}
