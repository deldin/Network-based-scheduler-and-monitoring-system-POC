using System.Data.Entity.Migrations;
using ScheduleMonitorApp.Entities;

namespace ScheduleMonitorApp.Migrations
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
                new Client() {ClientId = 1, ClientName = "Sample Client 1"},
                new Client() {ClientId = 2, ClientName = "Sample Client 2"}
                );


            context.ClientCommands.AddOrUpdate(
                x => x.ClientCommandId,
                new ClientCommand() {ClientCommandId = 101, ClientId = 1, Command = "Sample Command 1"},
                new ClientCommand() {ClientCommandId = 102, ClientId = 1, Command = "Sample Command 2"},
                new ClientCommand() {ClientCommandId = 103, ClientId = 2, Command = "Sample Command 3"}
                );
            context.ClientCommandLogs.AddOrUpdate(
                x => x.ClientCommandLogId,
                new ClientCommandLog()
                {
                    ClientCommandLogId = 1001,
                    LogType = "Output Log",
                    LogText = "Sample Output Info Logged",
                    ClientCommandId = 101
                },
                new ClientCommandLog()
                {
                    ClientCommandLogId = 1002,
                    LogType = "Error Log",
                    LogText = "Sample Error Info Logged",
                    ClientCommandId = 102
                },
                new ClientCommandLog()
                {
                    ClientCommandLogId = 1003,
                    LogType = "Error Log",
                    LogText = "Sample Error Info Logged",
                    ClientCommandId = 102
                },
                new ClientCommandLog()
                {
                    ClientCommandLogId = 1004,
                    LogType = "Info Log",
                    LogText = "Sample Info Logged",
                    ClientCommandId = 103
                });
        }
    }
}
