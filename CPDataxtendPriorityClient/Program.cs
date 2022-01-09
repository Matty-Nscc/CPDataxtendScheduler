using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using ReplicationPriorities;
using Logs;

namespace CPDataxtendPriorityClient
{
    class Program
    {
        private static Settings settings = null;
        private static LogWriter log = null;

        static void Main(string[] args)
        {
            //Create the logwriter.
            log = new LogWriter(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\Logs");

            //Load the settings
            if (LoadSettings())
            {    
                //Load Priorities
                if(LoadPriorities())
                {
                    settings.client.Start(settings, log);
                }
            }

            log.WriteInfo("Program closing.");
        }

        private static bool LoadSettings()
        {
            try
            {
                var serializer = new XmlSerializer(typeof(Settings));

                using (var reader = XmlReader.Create("Settings.ini"))
                {
                    settings = (Settings)serializer.Deserialize(reader);
                    log.WriteInfo("Settings have been read.");

                    if (!settings.DB.HasAllSettings())
                    {
                        log.WriteError("Settings file is missing some database settings that are required.");
                        return false;
                    }
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                log.WriteError("The Settings.ini file does not exist.");
                return false;
            }
            catch (Exception ex)
            {
                log.WriteError(ex.ToString());
                return false;
            }

            return true;
        }

        public static bool LoadPriorities()
        {
            var serializer = new XmlSerializer(typeof(PriorityReplications));

            try
            {
                using (var reader = XmlReader.Create("Priorities.ini"))
                {
                    settings.PriorityList = (PriorityReplications)serializer.Deserialize(reader);
                    log.WriteInfo("Priorities have been read.");

                    if (settings.PriorityList.Priorities.Count == 0)
                    {
                        log.WriteError("There are no priorities in the Priorities.ini file.");
                        return false;
                    }
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                log.WriteError("The Settings.ini file does not exist.");
                return false;
            }
            catch (Exception ex)
            {
                log.WriteError(ex.ToString());
                return false;
            }

            return true;
        }

    }
}
