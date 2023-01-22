namespace CommandAPI.Data
{
    public class CommandAPIRepo : ICommandAPIRepo
    {
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
            var commands = new List<Command>();
            commands.Add(new Command { Id = 1, Platform = "Windows", CommandLine = "", HowTo = "" });
            return commands;
        }

        public Command GetCommandById(int id)
        {
            throw new NotImplementedException();
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
