//Confidential Source Code Property Toys2Life LLC Colorado 2017
//www.toys2life.org

using System;
using System.Configuration;
using System.IO;

namespace DialogEngine.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class SessionVariables
    {

        public static bool DebugFlag
        {
            get
            {
                bool result=false;
                //Verify the flags and strings in the app settings config file
                if (ConfigurationManager.AppSettings["DebugFlag"] != null)
                    result = Convert.ToBoolean(AppSet.ReadSetting("DebugFlag"));

                return result;
            }

        }
        public static bool TagUsageCheck
        {
            get
            {
                bool result = false;

                if (ConfigurationManager.AppSettings["TagUsageCheck"] != null)
                    result = Convert.ToBoolean(AppSet.ReadSetting("TagUsageCheck"));

                return result;
            }
        }

        public static  bool AudioDialogsOn
        {
            get
            {
                bool result = false;

                if (ConfigurationManager.AppSettings["AudioDialogsOn"] != null)
                    result = Convert.ToBoolean(AppSet.ReadSetting("AudioDialogsOn"));

                return result;
            }
        }
        public static  bool TextDialogsOn
        {
            get
            {
                bool result=false;

                if (ConfigurationManager.AppSettings["TextDialogsOn"] != null)
                    result = Convert.ToBoolean(AppSet.ReadSetting("TextDialogsOn"));

                return result;
            }
        }
        public static bool ForceCharactersAndDialogModel
        {
            get
            {
                bool result = false;

                if (ConfigurationManager.AppSettings["ForceCharactersAndDialogModel"] != null)
                    result = Convert.ToBoolean(AppSet.ReadSetting("ForceCharactersAndDialogModel"));

                return result;
            }
        }
        public static  bool WaitIndefinatelyForMove
        {
            get
            {
                bool result=false;

                if (ConfigurationManager.AppSettings["WaitIndefinatelyForMove"] != null)
                    result = Convert.ToBoolean(AppSet.ReadSetting("WaitIndefinatelyForMove"));

                return result;
            }
        }

        public static bool WriteSerialLog
        {
            get
            {
                bool result = false;

                if (ConfigurationManager.AppSettings["WriteSerialLog"] != null)
                    result = Convert.ToBoolean(AppSet.ReadSetting("WriteSerialLog"));

                return result;

            }
        }

        public static bool HeatMapFullMatrixDispMode
        {
            get
            {
                bool result = false;

                if (ConfigurationManager.AppSettings["HeatMapFullMatrixDispMode"] != null)
                    result = Convert.ToBoolean(AppSet.ReadSetting("HeatMapFullMatrixDispMode"));

                return result;
            }
        }

        public static bool UseSerialPort
        {
            get
            {
                bool result = false;

                if (ConfigurationManager.AppSettings["UseSerialPort"] != null)
                    result = Convert.ToBoolean(AppSet.ReadSetting("UseSerialPort"));

                return result;
            }
        }

        public static string CurrentParentalRating
        {
            get
            {
                string result = "PG";

                if (ConfigurationManager.AppSettings["CurrentParentalRating"] != null)
                    result = AppSet.ReadSetting("CurrentParentalRating");

                return result;
            }
        }

        public static string ComPortName
        {
            get
            {
                string result=string.Empty;

                if (ConfigurationManager.AppSettings["ComPortName"] != null)
                {
                  result   = AppSet.ReadSetting("ComPortName");
                }

                return result;
            }
        }


        public static  bool ShowDupePhrases;
        public static  bool HeatMapSumsMode;
        public static  bool HeatMapOnlyMode;
        public static  bool CheckStuckTransmissions;
        public static  bool MonitorReceiveBufferSize;
        public static  bool MonitorMessageParseFails;
        public static  string BaseDirectory;
        public static  string LogsDirectory;
        public static  string CharactersDirectory;
        public static  string DialogsDirectory;
        public static  string AudioDirectory;
        public static  string DecimalLogFileName = "DecimalSerialLog.txt";
        public static  string HexLogFileName = "HexSerialLog.txt";
        public static  string DialogLogFileName = "LogDialog.txt";

        static SessionVariables()
        {

            if (ConfigurationManager.AppSettings["ShowDupePhrases"] != null)
                ShowDupePhrases = Convert.ToBoolean(AppSet.ReadSetting("ShowDupePhrases"));
            else
                ShowDupePhrases = false;


            if (ConfigurationManager.AppSettings["HeatMapSumsMode"] != null)
                HeatMapSumsMode = Convert.ToBoolean(AppSet.ReadSetting("HeatMapSumsMode"));
            else
                HeatMapSumsMode = false;

            if (ConfigurationManager.AppSettings["HeatMapOnlyMode"] != null)
                HeatMapOnlyMode = Convert.ToBoolean(AppSet.ReadSetting("HeatMapOnlyMode"));
            else
                HeatMapOnlyMode = false;


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




            //get executable path of DialogGenerator.exe
            string _path = Environment.CurrentDirectory;

            //path is  "solutionDir/bin/outputDir", so we need to go back 2 times to solutionDir
            BaseDirectory = Path.GetFullPath(Path.Combine(_path, @"..\..\"));

            LogsDirectory = BaseDirectory + @"Logs\";
            DialogsDirectory = BaseDirectory + @"DialogJSON\";
            AudioDirectory = BaseDirectory + @"DialogAudio\";
            CharactersDirectory = BaseDirectory + @"CharacterJSON\";

            
            if (LogsDirectory == null && WriteSerialLog)
            {
                Console.WriteLine("Valid LogsDirectory path not found in config file exiting.");
                Console.ReadLine();
                Environment.Exit(0);
            }
        

            if (ConfigurationManager.AppSettings["CharactersDirectory"] != null)
                CharactersDirectory = AppSet.ReadSetting("CharactersDirectory");

            if (CharactersDirectory == null)
            { 
                Console.WriteLine("Valid CharactersDirectory path not found in config file exiting.");
                Console.ReadLine();
                Environment.Exit(0);
            }

            if (ConfigurationManager.AppSettings["DialogsDirectory"] != null)
                DialogsDirectory = AppSet.ReadSetting("DialogsDirectory");
            
            if(DialogsDirectory == null)
            {
                Console.WriteLine("Valid DialogsDirectory path not found in config file exiting.");
                Console.ReadLine();
                Environment.Exit(0);
            }

            if (ConfigurationManager.AppSettings["AudioDirectory"] != null)
            {
                AudioDirectory = AppSet.ReadSetting("AudioDirectory");
            }

            if (AudioDirectory == null && AudioDialogsOn)
            { 
                Console.WriteLine("Valid AudioDirectory path not found in config file exiting.");
                Console.ReadLine();
                Environment.Exit(0);
            }


            if (ComPortName == null && UseSerialPort)
            {
                Console.WriteLine("Valid ComPortName  not found in config file exiting.");
                Console.ReadLine();
                Environment.Exit(0);
            }



            DecimalLogFileName = "DecimalSerialLog.txt";  //TODO are these redundant?
            HexLogFileName = "HexSerialLog.txt";
            DialogLogFileName = "LogDialog.txt";
        }
    }
}
    