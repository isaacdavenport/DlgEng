//Confidential Source Code Property Toys2Life LLC Colorado 2017
//www.toys2life.org

using DialogEngine.Models.Dialog;
using log4net;
using System;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace DialogEngine.Helpers
{
    /// <summary>
    /// Aggregator of all application settings 
    /// </summary>
    public static class SessionHelper
    {
        private static readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static bool ShowDupePhrases;
        public static bool CheckStuckTransmissions;
        public static bool MonitorReceiveBufferSize;
        public static bool MonitorMessageParseFails;
        public static string BaseDirectory;
        public static readonly string WizardDirectory;
        public static readonly string WizardVideoDirectory;
        public static readonly string WizardAudioDirectory;
        public static readonly string TutorialDirectory;
        public static readonly string TutorialFileName;
        public static readonly string Toys2LifeWebsiteUrl;
        public static readonly string TempDirectory;
        
        // !!! IMPORTANT !!!  If you change name, you must also change name in log4net.config 
        public static readonly string DecimalLogFileName = "DecimalSerialLog";

        // !!! IMPORTANT !!! If you change name, you must also change name in log4net.config 
        public static readonly string  DialogLogFileName = "LogDialog";

        /// <summary>
        /// Maximum time to play current dialog after speaker characters changed in seconds
        /// </summary>
        public static double MaxTimeToPlayFile
        {
            get
            {
                string result = ConfigHelper.Instance.GetValue("MaxTimeToPlayFile");

                if (result != null)
                    return Convert.ToDouble(result);
                else
                    return 0;
            }

        }

        public static bool TagUsageCheck
        {
            get
            {
                string result = ConfigHelper.Instance.GetValue("TagUsageCheck");

                if (result != null)
                    return bool.Parse(result);
                else
                    return false;
            }
        }

        public static  bool TextDialogsOn
        {
            get
            {
                string result = ConfigHelper.Instance.GetValue("TextDialogsOn");

                if (result != null)
                    return bool.Parse(result);
                else
                    return true;
            }
        }

        /// <summary>
        /// Serial or random selection of characters
        /// </summary>
        public static bool UseSerialPort
        {
            get
            {
                string result = ConfigHelper.Instance.GetValue("UseSerialPort");

                if (result != null)
                    return bool.Parse(result);
                else
                    return false;
            }
        }

        /// <summary>
        /// <see cref="ParentalRatings"/>
        /// </summary>
        public static string CurrentParentalRating
        {
            get
            {
                string result = ConfigHelper.Instance.GetValue("CurrentParentalRating");

                if (result != null)
                    return result;
                else
                    return "PG";
            }
        }

        /// <summary>
        /// COM port name
        /// </summary>
        public static string ComPortName
        {
            get
            {
               string value = ConfigHelper.Instance.GetValue("ComPortName");

               return value;
            }
        }


        /// <summary>
        /// Static constructor
        /// </summary>
        static SessionHelper()
        {
            if (ConfigurationManager.AppSettings["ShowDupePhrases"] != null)
                ShowDupePhrases = Convert.ToBoolean(AppSet.ReadSetting("ShowDupePhrases"));
            else
                ShowDupePhrases = false;

            if (ConfigurationManager.AppSettings["CheckStuckTransmissions"] != null)
                CheckStuckTransmissions = Convert.ToBoolean(AppSet.ReadSetting("CheckStuckTransmissions"));
            else
                CheckStuckTransmissions = false;

            if (ConfigurationManager.AppSettings["MonitorReceiveBufferSize"] != null)
                MonitorReceiveBufferSize = Convert.ToBoolean(AppSet.ReadSetting("MonitorReceiveBufferSize"));
            else
                MonitorReceiveBufferSize = false;

            if (ConfigurationManager.AppSettings["MonitorMessageParseFails"] != null)
                MonitorMessageParseFails = Convert.ToBoolean(AppSet.ReadSetting("MonitorMessageParseFails"));
            else
                MonitorMessageParseFails = false;

            TutorialFileName = ConfigurationManager.AppSettings["TutorialFileName"];
            Toys2LifeWebsiteUrl = ConfigurationManager.AppSettings["Toys2LifeWebsiteUrl"];

            //path is  "solutionDir/bin/outputDir", so we need to go back 2 times to solutionDir
            BaseDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            WizardDirectory = BaseDirectory + @"\WizardJSON\";
            WizardVideoDirectory = BaseDirectory + @"\WizardVideo\";
            WizardAudioDirectory = BaseDirectory + @"\WizardAudio\";
            TutorialDirectory = BaseDirectory + @"\TutorialCharacterCreation\";
            TempDirectory = BaseDirectory + @"\Temp\";
        }
    }
}
    