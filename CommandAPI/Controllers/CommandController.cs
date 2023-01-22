using AutoMapper;

using CommandAPI.Data;
using CommandAPI.Dtos;

using Microsoft.AspNetCore.Mvc;

namespace CommandAPI.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        private readonly ICommandAPIRepo _repo;
        private readonly IMapper _mapper;

        public CommandController(ICommandAPIRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task< ActionResult> GetAll()
        {
            var commands = _repo.GetAllCommands();

            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>( commands)?? null );
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var command = _mapper.Map<CommandReadDto>( _repo.GetCommandById(id)) ;
            return command == null ? NotFound() : Ok(command);
        }
    }
}
