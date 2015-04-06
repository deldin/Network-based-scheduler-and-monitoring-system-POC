using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace ScheduleRunnerService
{
    public partial class ScheduleRunnerService : ServiceBase
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Thread m_serviceThread;
        private AutoResetEvent m_done = new AutoResetEvent(false);
        private ManualResetEvent m_running = new ManualResetEvent(true);
        public ScheduleRunnerService()
        {
            
            log4net.Config.XmlConfigurator.Configure();
            System.Diagnostics.EventLog.WriteEntry("ScheduleRunnerService", DateTime.Now.ToLongTimeString() + " Started in the constructor");
            Log.Info("Service has started in the constructor");
            InitializeComponent();

            this.CanHandlePowerEvent = true;
            this.CanHandleSessionChangeEvent = true;
            this.CanPauseAndContinue = true;
            this.CanShutdown = true;
            this.CanStop = true;
        }

        protected override void OnStart(string[] args)
        {
            Log.Info("Service has started");

            // Kick off a thread to do work
            m_serviceThread = new Thread(new ThreadStart(ThreadProcess));
            m_serviceThread.Start();


            //log4net.Config.XmlConfigurator.Configure();
            Log.Info("Service has Second started");
            System.Diagnostics.EventLog.WriteEntry("ScheduleRunnerService", DateTime.Now.ToLongTimeString() + " Started");
            base.OnStart(args);
        }

        /// <summary>
        /// The background thread.
        /// </summary>
        private void ThreadProcess()
        {

            while (!m_done.WaitOne(1000, false))
            {
                if (m_running.WaitOne(0, false))
                {
                    // Log4Net logging methods.
                    //Log.Debug("This is the debug message");
                    //Log.Info("This is the info message");
                    //Log.Warn("This is the warn message");
                    //Log.Error("This is the error message");
                    //Log.Fatal("This is the fatal message");

                    //// Standard .NET tracing methods.
                    //Debug.WriteLine("This is .NET Debug");
                    //Trace.WriteLine("This is .NET Trace");
                }
                else
                {
                    // Wait to be signalled
                    m_running.WaitOne();
                }
            }
        }

        //protected override void OnStop()
        //{
        //    base.OnStop();
        //    System.Diagnostics.EventLog.WriteEntry("ScheduleRunnerService", DateTime.Now.ToLongTimeString() + " Stopped ");
        //    Log.Info("Service has Stopped");
        //}

        protected override void OnCustomCommand(int command)
        {
            try
            {
                base.OnCustomCommand(command);
                System.Diagnostics.EventLog.WriteEntry("ScheduleRunnerService", DateTime.Now.ToLongTimeString() + " Custom command recieved " + command);
                //log4net.LogicalThreadContext.Properties["CommandValue"] = command.ToString();
                Log.Info("Ran custom command : " + command);
            }
            catch (Exception ex)
            {
                Log.Error("Error while running custom command : " + command + " " + ex.Message);
            }

        }

    }
}
