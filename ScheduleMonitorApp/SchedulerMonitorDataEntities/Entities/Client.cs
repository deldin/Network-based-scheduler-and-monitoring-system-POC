using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchedulerMonitorDataEntities.Entities
{
    public sealed class Client
    {
        public Client()
        {
            this.ClientCommands = new HashSet<ClientCommand>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public ICollection<ClientCommand> ClientCommands { get; set; }
    }
}