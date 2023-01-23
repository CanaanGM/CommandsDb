using AutoMapper;

using CommandAPI.Data;
using CommandAPI.Dtos;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;

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
        public async Task<ActionResult> GetAll()
        {
            var commands = _repo.GetAllCommands();

            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>( commands)?? null );
        }


        [HttpGet("{id}", Name = "GetCommandById")]
        public async Task<ActionResult<CommandReadDto>> GetCommandById(int id)
        {
            var command = _mapper.Map<CommandReadDto>( _repo.GetCommandById(id)) ;
            return command == null ? NotFound() : Ok(command);
        }

        [HttpPost]
        public async Task<ActionResult<CommandReadDto>> CreateCommand([FromBody] CommandCreateDto commandDto)
        {
            var commandModel = _mapper.Map<Command>(commandDto);
            _repo.CreateCommand(commandModel);
            _repo.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);

            return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDto.Id }, commandReadDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCommandById([FromRoute] int id, [FromBody] CommandUpdateDto commandUpdate)
        {
            var command = _repo.GetCommandById(id);
            if (command == null) return NotFound();

            _mapper.Map(commandUpdate, command);

            _repo.UpdateCommand(command); // here for show xD or when i decide to use dapper or something. . .
            _repo.SaveChanges();

            return NoContent();
        }


        [HttpPatch("{id}")]
        public async Task<ActionResult> Partialupdate([FromRoute] int Id, [FromBody] JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var command = _repo.GetCommandById(Id);
            if (command == null) return NotFound();

            var commandToPatch = _mapper.Map<CommandUpdateDto>(command);
            patchDoc.ApplyTo(commandToPatch, ModelState);

            if (!TryValidateModel(commandToPatch)) return ValidationProblem(ModelState);

            _mapper.Map(commandToPatch, command);
            _repo.UpdateCommand(command);
            _repo.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCommand([FromRoute] int id)
        {
            var commandToDelete = _repo.GetCommandById(id);
            if(commandToDelete == null) return NotFound();

            _repo.DeleteCommand(commandToDelete);
            _repo.SaveChanges();
            return NoContent();


        }
    }
}
