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
using SchedulerMonitorDataEntities.Entities;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace ScheduleRunnerService
{
    public partial class ScheduleRunnerService : ServiceBase
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Thread m_serviceThread;
        private AutoResetEvent m_done = new AutoResetEvent(false);
        private ManualResetEvent m_running = new ManualResetEvent(true);
        private ScheduleMonitorDb _db = new ScheduleMonitorDb();
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
            m_serviceThread = new Thread(ThreadProcess);
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
                    using (_db = new ScheduleMonitorDb())
                    {
                        try
                        {
                            if (_db.ClientCommands.Any(x => x.IsScheduled == true && x.IsExecuted == false))
                            {
                                var command = _db.ClientCommands.First(x => x.IsScheduled == true && x.IsExecuted == false);
                                EventLog.WriteEntry("ScheduleRunnerService", DateTime.Now.ToLongTimeString() + " " + command.Command);

                                base.OnCustomCommand(Convert.ToInt32(command.Command));

                                command.IsExecuted = true;

                                var commandLog = _db.ClientCommandLogs.Add(new ClientCommandLog()
                                {
                                    ClientCommandId = command.ClientCommandId,
                                    LogText = "Executed the Scheduled command : " + command.Command,
                                    LogType = "OUTPUT"
                                });

                                _db.SaveChanges();

                                Log.Info("Executed the Scheduled command : " + command.Command);
                            }
                        }
                        catch (Exception ex)
                        {
                            EventLog.WriteEntry("ScheduleRunnerService", DateTime.Now.ToLongTimeString() + " " + ExceptionMessageFull(ex));
                            Log.Error(ExceptionMessageFull(ex));
                            break;
                        }
                        
                    }
                   
                    // Log4Net logging methods.
                    //Log.Debug("This is the debug message");
                }
                else
                {   
                    // Wait to be signalled
                    m_running.WaitOne();
                }
            }
        }

        protected override void OnStop()
        {
            base.OnStop();
            System.Diagnostics.EventLog.WriteEntry("ScheduleRunnerService", DateTime.Now.ToLongTimeString() + " Stopped ");
            Log.Info("Service has Stopped");
        }

        protected override void OnCustomCommand(int command)
        {
            try
            {
                base.OnCustomCommand(command);
                System.Diagnostics.EventLog.WriteEntry("ScheduleRunnerService", DateTime.Now.ToLongTimeString() + " Custom command recieved " + command);
                //log4net.LogicalThreadContext.Properties["CommandValue"] = command.ToString();
                var commandString = command.ToString();
                using (var _db = new ScheduleMonitorDb())
                {
                    try
                    {
                        if (_db.ClientCommands.Any(x => x.Command == commandString))
                        {
                            var clientCommand = _db.ClientCommands.First(x => x.Command == commandString);
                            EventLog.WriteEntry("ScheduleRunnerService", DateTime.Now.ToLongTimeString() + " " + clientCommand.Command);

                            //base.OnCustomCommand(command);

                            clientCommand.IsExecuted = true;

                            var commandLog = _db.ClientCommandLogs.Add(new ClientCommandLog()
                            {
                                ClientCommandId = clientCommand.ClientCommandId,
                                LogText = "Executed the Custom command : " + clientCommand.Command,
                                LogType = "CUSTOM COMMAND OUTPUT"
                            });
                            EventLog.WriteEntry("ScheduleRunnerService", DateTime.Now.ToLongTimeString() + " Custom command recieved and linked to a client" + clientCommand.Command);

                            _db.SaveChanges();
                        }
                        else
                        {
                            var commandLog = _db.ClientCommandLogs.Add(new ClientCommandLog()
                            {
                                ClientCommandId = null,
                                LogText = "Executed the Custom command : " + command,
                                LogType = "CUSTOM COMMAND OUTPUT"
                            });

                            _db.SaveChanges();
                            EventLog.WriteEntry("ScheduleRunnerService", DateTime.Now.ToLongTimeString() + " Custom command recieved " + command);
                        }
                        Log.Info("Executed the Custom command : " + command);
                    }
                    catch (Exception ex)
                    {
                        EventLog.WriteEntry("ScheduleRunnerService", DateTime.Now.ToLongTimeString() + " " + ExceptionMessageFull(ex));
                        Log.Error(ExceptionMessageFull(ex));
                    }

                }

                //using (_db = new ScheduleMonitorDb())
                //{
                //    _db.ClientCommandLogs.Add(new ClientCommandLog
                //    {
                //        ClientCommandId = 103,
                //        LogType = "OUTPUT",
                //        LogText = " Custom command recieved " + command
                //    });

                //    _db.SaveChanges();
                //}

                //Log.Info("Ran custom command : " + command);
            }
            catch (Exception ex)
            {
                Log.Error("Error while running custom command : " + command + " " + ex.Message);
            }

        }

        private string ExceptionMessageFull(Exception ex)
        {
            var message = ex.Message;
                if(ex.InnerException != null) 
                    message += ExceptionMessageFull(ex.InnerException);

            return message;
        }

    }
}
