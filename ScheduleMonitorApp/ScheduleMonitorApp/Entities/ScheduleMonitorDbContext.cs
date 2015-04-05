using System.Data.Entity;

namespace ScheduleMonitorApp.Entities
{
    public class ScheduleMonitorDb : DbContext
    {
        public ScheduleMonitorDb()
            : base("name=ScheduleMonitorDbContext")
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<ClientCommand> ClientCommands { get; set; }
        public virtual DbSet<ClientCommandLog> ClientCommandLogs { get; set; }
    }
}