
using System.Data.Entity.Migrations;
using SchedulerMonitorDataEntities.Entities;

namespace SchedulerMonitorDataEntities.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ScheduleMonitorDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ScheduleMonitorDb context)
        {
            context.Clients.AddOrUpdate(
                x => x.ClientId,
                new Client() { ClientId = 1, ClientName = "Sample Client 1" },
                new Client() { ClientId = 2, ClientName = "Sample Client 2" }
                );


            context.ClientCommands.AddOrUpdate(
                x => x.ClientCommandId,
                new ClientCommand() { ClientCommandId = 101, ClientId = 1, Command = "128", IsExecuted = false, IsScheduled = true},
                new ClientCommand() { ClientCommandId = 102, ClientId = 1, Command = "136", IsExecuted = false, IsScheduled = false },
                new ClientCommand() { ClientCommandId = 103, ClientId = 2, Command = "145", IsExecuted = false, IsScheduled = true },
                new ClientCommand() { ClientCommandId = 104, ClientId = 2, Command = "147", IsExecuted = false, IsScheduled = false }
                );
            context.ClientCommandLogs.AddOrUpdate(
                x => x.ClientCommandLogId,
                new ClientCommandLog()
                {
                    ClientCommandLogId = 1001,
                    LogType = "INFO",
                    LogText = "Sample Output Info Logged",
                    ClientCommandId = 101
                },
                new ClientCommandLog()
                {
                    ClientCommandLogId = 1002,
                    LogType = "ERROR",
                    LogText = "Sample Error Info Logged",
                    ClientCommandId = 102
                },
                new ClientCommandLog()
                {
                    ClientCommandLogId = 1003,
                    LogType = "ERROR",
                    LogText = "Sample Error Info Logged",
                    ClientCommandId = 102
                },
                new ClientCommandLog()
                {
                    ClientCommandLogId = 1004,
                    LogType = "WARNING",
                    LogText = "Sample Info Logged",
                    ClientCommandId = 103
                });
        }
    }
}
