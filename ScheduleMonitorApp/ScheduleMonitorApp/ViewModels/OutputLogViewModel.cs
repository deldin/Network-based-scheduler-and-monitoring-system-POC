using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScheduleMonitorApp.Models;

namespace ScheduleMonitorApp.ViewModels
{
    public class OutputLogViewModel
    {
        private readonly ScheduleMonitorModel _scheduleMonitorModel = new ScheduleMonitorModel();
        public OutputLogViewModel()
        {
            //ddlLogType = new SelectList(_scheduleMonitorModel.getAllLogTypes(), "LogType", "LogType", "Output Log");            
        }

        //public SelectList ddlLogType { get; set; }

        public IEnumerable<SelectListItem> LogTypeItems
        {
            get
            {
                return new SelectList(_scheduleMonitorModel.getAllLogTypes(), "Value", "Text", "Output Log");      
            }
        }

        public string LogText { get; set; }
        public string LogType { get; set; }
        public int ClientCommandId { get; set; }
        public int ClientCommandLogId { get; set; }
        
    }
}