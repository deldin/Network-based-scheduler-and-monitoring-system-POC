using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ScheduleMonitorApp.Entities
{
    public sealed class ClientCommand
    {
        public ClientCommand()
        {
            this.ClientCommandLogs = new HashSet<ClientCommandLog>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientCommandId { get; set; }
        public int ClientId { get; set; }
        public string Command { get; set; }
        public ICollection<ClientCommandLog> ClientCommandLogs { get; set; }
    }
}