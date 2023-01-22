
using Microsoft.EntityFrameworkCore;

namespace CommandAPI.Data
{
    public class CommandAPIRepo : ICommandAPIRepo
    {
        private readonly CommandContext _context;

        public CommandAPIRepo(CommandContext context)
        {
            _context = context;
        }

        public void CreateCommand(Command command)
        {
            throw new NotImplementedException();
        }

        public void DeleteCommand(Command command)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            var commands = _context.CommandItems.ToArray();
            return commands;
        }

        public  Command GetCommandById(int id)
        {
            var command = _context.CommandItems
                .Where(c => c.Id == id)
                .FirstOrDefault();

            return command ?? null;
            

        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateCommand(Command command)
        {
            throw new NotImplementedException();
        }
    }
}
