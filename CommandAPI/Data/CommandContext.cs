using Microsoft.EntityFrameworkCore;

namespace CommandAPI.Data
{
    public class CommandContext : DbContext
    {
        public CommandContext(DbContextOptions<CommandContext> opt) : base(opt)
        {

        }


        public DbSet<Command> CommandItems { get; set; }
    }
}
