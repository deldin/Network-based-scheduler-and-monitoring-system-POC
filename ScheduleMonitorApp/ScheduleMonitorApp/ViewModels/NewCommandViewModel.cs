using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScheduleMonitorApp.ViewModels
{
    public class NewCommandViewModel
    {
        public int ClientCommandId { get; set; }
        public int ClientId { get; set; }
        [RegularExpression("\\d{3}", ErrorMessage = "Only 3 digit numbers for the commands")]
        public string Command { get; set; }

        private bool isScheduled = true;
        public bool IsScheduled
        {
            get
            {
                return isScheduled;
            }
            set { isScheduled = value; }
        }

        public bool IsExecuted { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ScheduledDate { get; set; }

        [DataType(DataType.Time)]
        public DateTime? ScheduledTime { get; set; }
    }
}