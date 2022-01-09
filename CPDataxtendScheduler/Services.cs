using System;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using System.Diagnostics;
using DBAccess;
using Logs;

namespace CPDataxtendScheduler
{
    public class Services
    {
        private static bool PandoraPresent = false;
        private static string PandoraServiceName = "PandoraFMSAgent";
        private static LogWriter lw;

        public static bool CheckServiceStatus(LogWriter lw, string ServiceName)
        {
            lw.WriteInfo("Checking " + ServiceName + " service status.");

            try
            {
                var service = ServiceController.GetServices().Any(s => s.ServiceName == ServiceName);

                if (service)
                {
                    using (ServiceController sc = new ServiceController(ServiceName))
                    {
                        switch(sc.Status)
                        {
                            case ServiceControllerStatus.StartPending:
                                lw.WriteInfo("The service status is StartPending.");
                                break;
                            case ServiceControllerStatus.Running:
                                lw.WriteInfo("The service status is Running.");
                                break;
                            case ServiceControllerStatus.Stopped:
                                lw.WriteInfo("The service status is Stopped.");
                                break;
                            case ServiceControllerStatus.Paused:
                                lw.WriteInfo("The service status is Paused.");
                                break;
                            case ServiceControllerStatus.StopPending:
                                lw.WriteInfo("The service status is StopPending.");
                                break;
                            case ServiceControllerStatus.ContinuePending:
                                lw.WriteInfo("The service status is ContinuePending.");
                                break;
                            case ServiceControllerStatus.PausePending:
                                lw.WriteInfo("The service status is PausePending.");
                                break;
                            default:
                                lw.WriteInfo("The service status is unknown.");
                                break;
                        }

                        //Program is starting or stopping.
                        if (sc.Status == ServiceControllerStatus.StartPending || sc.Status == ServiceControllerStatus.Running)
                            return true;
                        else
                            return false;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                lw.WriteError(ex.ToString());
                return false;
            }
        }

        private static bool ServiceExists(string name)
        {
            return ServiceController.GetServices().Any(s => s.ServiceName == name);
        }

        private static void IsPandoraInstalled()
        {
            Console.WriteLine("Checking if Pandora is installed.");
            lw.WriteInfo("Checking if Pandora is installed.");

            try
            {
                PandoraPresent = ServiceExists(PandoraServiceName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                lw.WriteError(ex.ToString());
            }


            if (PandoraPresent)
            {
                Console.WriteLine("Pandora is installed.");
                lw.WriteInfo("Pandora is installed.");
            }
            else
            {
                Console.WriteLine("Pandora is not installed.");
                lw.WriteInfo("Pandora is not installed.");
            }
        }

        private static void StopPandora()
        {
            StopService(PandoraServiceName, 5, 5000);
        }

        private static void StartPandora()
        {
            StartService(PandoraServiceName);
        }

        public static bool StopDataxtendServices()
        {
            StopService("PDAgentSvc", 2, 5000);
            return StopService("PDRESvc", 36, 5000);
        }

        public static void StartDataxtendServices()
        {
            StartService("PDRESvc");
            StartService("PDAgentSvc");
        }

        private static bool StopService(string ServiceName, int WaitCounts, int WaitInterval)
        {
            Console.WriteLine("Attempting to stop the " + ServiceName + " service.");
            lw.WriteInfo("Attempting to stop the " + ServiceName + " service.");

            try
            {
                var service = ServiceController.GetServices().Any(s => s.ServiceName == ServiceName);

                //Agent service exists
                if (service)
                {
                    using (ServiceController sc = new ServiceController(ServiceName))
                    {
                        //Program is starting or stopping.
                        if (sc.Status == ServiceControllerStatus.StartPending || sc.Status == ServiceControllerStatus.StopPending)
                        {
                            for (int i = 0; i < WaitCounts; i++)
                            {
                                if (sc.Status == ServiceControllerStatus.StartPending || sc.Status == ServiceControllerStatus.StopPending)
                                    Thread.Sleep(WaitInterval);
                                else
                                    break;
                            }
                        }

                        if (sc.Status == ServiceControllerStatus.Running || sc.Status == ServiceControllerStatus.Paused)
                            sc.Stop();

                        if (sc.Status != ServiceControllerStatus.Stopped)
                            TaskkillService(sc.ServiceName);

                        Console.WriteLine(ServiceName + " has been stopped.");
                        lw.WriteInfo(ServiceName + " has been stopped.");
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                lw.WriteError(ex.ToString());
                return false;
            }
        }

        private static void StartService(string ServiceName)
        {
            Console.WriteLine("Starting the " + ServiceName + " service.");
            lw.WriteInfo("Starting the " + ServiceName + " service.");
            try
            {
                using (ServiceController sc = new ServiceController(ServiceName))
                {
                    if (sc.Status != ServiceControllerStatus.Running)
                    {
                        sc.Start();
                        Console.WriteLine("Service started successfully.");
                        lw.WriteInfo("Service started successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message == "An instance of the service is already running")
                {
                    Console.WriteLine("Service was already running.");
                    lw.WriteInfo("Service was already running.");
                }
                else
                {
                    Console.Write(ex.ToString());
                    lw.WriteError(ex.ToString());
                }
            }
        }

        private static void TaskkillService(string ServiceName)
        {
            foreach (Process Prc in Process.GetProcessesByName(ServiceName))
            {
                using (Prc)
                {
                    Prc.Kill();
                }
            }
        }
    }
}
