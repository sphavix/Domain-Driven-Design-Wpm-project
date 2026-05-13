using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wpm.Management.Api.Application.Commands;
using Wpm.Management.Api.Application.Services;

namespace Wpm.Management.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagementController(ManagementApplicationService appService,
                                      ICommandHandler<SetWeightCommand> commandHandler) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Post(CreatePetCommand command)
        {
            await appService.Handle(command);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put(SetWeightCommand command)
        {
            await commandHandler.Handle(command);
            return Ok();
        }
    }
}
