using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchedulerMonitorDataEntities.Entities
{
    public class ClientCommandLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientCommandLogId { get; set; }
        public string LogType { get; set; }
        public string LogText { get; set; }
        public int? ClientCommandId { get; set; }
    }
}