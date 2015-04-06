using System.Data.Entity;

namespace SchedulerMonitorDataEntities.Entities
{
    public class ScheduleMonitorDb : DbContext
    {
        public ScheduleMonitorDb()
            : base("name=ScheduleMonitorDbContext")
        {
            Database.SetInitializer<ScheduleMonitorDb>(null);
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Logs> Logs { get; set; }
        public virtual DbSet<ClientCommand> ClientCommands { get; set; }
        public virtual DbSet<ClientCommandLog> ClientCommandLogs { get; set; }
    }
}