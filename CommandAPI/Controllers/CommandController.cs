using CommandAPI.Data;

using Microsoft.AspNetCore.Mvc;

namespace CommandAPI.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        private readonly ICommandAPIRepo _repo;

        public CommandController(ICommandAPIRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task< ActionResult> GetAll()
        {
            var commands = _repo.GetAllCommands();
            return Ok(commands?? null );
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var command = _repo.GetCommandById(id);
            return command == null ? NotFound() : Ok(command);
        }
    }
}
