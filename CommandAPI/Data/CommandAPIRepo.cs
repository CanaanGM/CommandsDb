
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
            if (command == null)
                throw new ArgumentException(nameof(command));
           _context.CommandItems.Add(command);

        }

        public void DeleteCommand(Command command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));
            _context.CommandItems.Remove(command);
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

        public bool SaveChanges() => (_context.SaveChanges() >= 0);

        public void UpdateCommand(Command command)
        {
           // No nedd as AUTMAPPER does the work \(@^0^@)/ ;
           // when mapping happens the model in  the context is updated, we only need to call save changes to perssit it 
        }
    }
}
