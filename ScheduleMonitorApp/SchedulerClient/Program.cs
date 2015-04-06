using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;
using System.Threading;

namespace SchedulerClient
{
    class Program
    {
        private enum SimpleServiceCustomCommands { StopWorker = 195, RestartWorker, CheckWorker };
        static void Main(string[] args)
        {
            ServiceController myService = new ServiceController("ScheduleRunnerService");
            myService.ExecuteCommand((int)SimpleServiceCustomCommands.StopWorker);
            myService.ExecuteCommand((int)SimpleServiceCustomCommands.RestartWorker);
            myService.ExecuteCommand((int)SimpleServiceCustomCommands.CheckWorker);


        }
        //public enum SimpleServiceCustomCommands
        //{ StopWorker = 128, RestartWorker, CheckWorker };
        //static void Main(string[] args)
        //{
        //    ServiceController[] scServices;
        //    scServices = ServiceController.GetServices();

        //    foreach (ServiceController scTemp in scServices)
        //    {

        //        if (scTemp.ServiceName == "ScheduleRunnerService")
        //        {
        //            // Display properties for the Simple Service sample 
        //            // from the ServiceBase example.
        //            ServiceController sc = new ServiceController("ScheduleRunnerService");
        //            Console.WriteLine("Status = " + sc.Status);
        //            Console.WriteLine("Can Pause and Continue = " + sc.CanPauseAndContinue);
        //            Console.WriteLine("Can ShutDown = " + sc.CanShutdown);
        //            Console.WriteLine("Can Stop = " + sc.CanStop);
        //            if (sc.Status == ServiceControllerStatus.Stopped)
        //            {
        //                sc.Start();
        //                while (sc.Status == ServiceControllerStatus.Stopped)
        //                {
        //                    Thread.Sleep(1000);
        //                    sc.Refresh();
        //                }
        //            }
        //            // Issue custom commands to the service 
        //            // enum SimpleServiceCustomCommands  
        //            //    { StopWorker = 128, RestartWorker, CheckWorker };
        //            sc.ExecuteCommand((int)SimpleServiceCustomCommands.StopWorker);
        //            sc.ExecuteCommand((int)SimpleServiceCustomCommands.RestartWorker);
        //            sc.ExecuteCommand((int)SimpleServiceCustomCommands.CheckWorker);
                    
        //            Console.WriteLine("Status = " + sc.Status);
        //            // Display the event log entries for the custom commands 
        //            // and the start arguments.
        //            EventLog el = new EventLog("Application");
        //            EventLogEntryCollection elec = el.Entries;
        //            foreach (EventLogEntry ele in elec)
        //            {
        //                if (ele.Source.IndexOf("SimpleService.OnCustomCommand") >= 0 |
        //                    ele.Source.IndexOf("SimpleService.Arguments") >= 0)
        //                    Console.WriteLine(ele.Message);
        //            }
        //        }
        //    }


        //}
    }
}
