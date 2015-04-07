using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchedulerMonitorDataEntities.Entities
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
        [Required]
        [RegularExpression("\\d{3}", ErrorMessage = "Only 3 digit numbers for the commands")]
        public string Command { get; set; }
        public bool IsScheduled { get; set; }
        public bool IsExecuted { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ScheduledTime { get; set; }
        public ICollection<ClientCommandLog> ClientCommandLogs { get; set; }
    }
}