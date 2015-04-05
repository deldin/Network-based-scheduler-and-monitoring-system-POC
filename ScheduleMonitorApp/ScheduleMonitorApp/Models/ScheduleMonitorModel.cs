using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScheduleMonitorApp.Entities;

namespace ScheduleMonitorApp.Models
{
    public class ScheduleMonitorModel
    {
        private ScheduleMonitorDb _db;

        public ScheduleMonitorModel()
        {
            if(_db == null)
                _db = new ScheduleMonitorDb();
        }

        public ScheduleMonitorModel(ScheduleMonitorDb db)
        {
            _db = db;
        }

        internal System.Collections.IEnumerable getAllLogTypes()
        {
            var result = _db.ClientCommandLogs.Select(x => new{Text = x.LogType, Value= x.LogType}).Distinct().ToList();
            return result;
        }
    }
}