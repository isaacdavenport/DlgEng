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

        public static bool DebugFlag;
        public static  bool TagUsageCheck;
        public static  bool AudioDialogsOn;
        public static  bool TextDialogsOn;
        public static  bool ForceCharactersAndDialogModel;
        public static  bool WaitIndefinatelyForMove;
        public static  bool ShowDupePhrases;
        public static  bool HeatMapFullMatrixDispMode;
        public static  bool HeatMapSumsMode;
        public static  bool HeatMapOnlyMode;
        public static  bool WriteSerialLog;
        public static  bool UseSerialPort;
        public static  bool CheckStuckTransmissions;
        public static  bool MonitorReceiveBufferSize;
        public static  bool MonitorMessageParseFails;
        public static  string CurrentParentalRating;
        public static  string BaseDirectory;
        public static  string LogsDirectory;
        public static  string CharactersDirectory;
        public static  string DialogsDirectory;
        public static  string AudioDirectory;
        public static  string DecimalLogFileName = "DecimalSerialLog.txt";
        public static  string HexLogFileName = "HexSerialLog.txt";
        public static  string DialogLogFileName = "LogDialog.txt";
        public static  string ComPortName;

        static SessionVariables()
        {


            //Verify the flags and strings in the app settings config file
            if (ConfigurationManager.AppSettings["DebugFlag"] != null) 
                DebugFlag = Convert.ToBoolean(AppSet.ReadSetting("DebugFlag"));
            else 
                DebugFlag = false;

            if (ConfigurationManager.AppSettings["TagUsageCheck"] != null)
                TagUsageCheck = Convert.ToBoolean(AppSet.ReadSetting("TagUsageCheck"));
            else
                TagUsageCheck = false;

            if (ConfigurationManager.AppSettings["AudioDialogsOn"] != null)
                AudioDialogsOn = Convert.ToBoolean(AppSet.ReadSetting("AudioDialogsOn"));
            else
                AudioDialogsOn = false;

            if (ConfigurationManager.AppSettings["TextDialogsOn"] != null)
                TextDialogsOn = Convert.ToBoolean(AppSet.ReadSetting("TextDialogsOn"));
            else
                TextDialogsOn = true;

            if (ConfigurationManager.AppSettings["ForceCharactersAndDialogModel"] != null)
                ForceCharactersAndDialogModel = Convert.ToBoolean(AppSet.ReadSetting("ForceCharactersAndDialogModel"));
            else
                ForceCharactersAndDialogModel = false;

            if (ConfigurationManager.AppSettings["WaitIndefinatelyForMove"] != null)
                WaitIndefinatelyForMove = Convert.ToBoolean(AppSet.ReadSetting("WaitIndefinatelyForMove"));
            else
                WaitIndefinatelyForMove = false;

            if (ConfigurationManager.AppSettings["ShowDupePhrases"] != null)
                ShowDupePhrases = Convert.ToBoolean(AppSet.ReadSetting("ShowDupePhrases"));
            else
                ShowDupePhrases = false;

            if (ConfigurationManager.AppSettings["HeatMapFullMatrixDispMode"] != null)
                HeatMapFullMatrixDispMode = Convert.ToBoolean(AppSet.ReadSetting("HeatMapFullMatrixDispMode"));
            else
                HeatMapFullMatrixDispMode = false;

            if (ConfigurationManager.AppSettings["HeatMapSumsMode"] != null)
                HeatMapSumsMode = Convert.ToBoolean(AppSet.ReadSetting("HeatMapSumsMode"));
            else
                HeatMapSumsMode = false;

            if (ConfigurationManager.AppSettings["HeatMapOnlyMode"] != null)
                HeatMapOnlyMode = Convert.ToBoolean(AppSet.ReadSetting("HeatMapOnlyMode"));
            else
                HeatMapOnlyMode = false;

            if (ConfigurationManager.AppSettings["WriteSerialLog"] != null)
                WriteSerialLog = Convert.ToBoolean(AppSet.ReadSetting("WriteSerialLog"));
            else
                WriteSerialLog = false;

            if (ConfigurationManager.AppSettings["UseSerialPort"] != null)
                UseSerialPort = Convert.ToBoolean(AppSet.ReadSetting("UseSerialPort"));
            else
                UseSerialPort = false;

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

            if (ConfigurationManager.AppSettings["CurrentParentalRating"] != null)
                CurrentParentalRating = AppSet.ReadSetting("CurrentParentalRating");
            else
                CurrentParentalRating = "PG";


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


            if (ConfigurationManager.AppSettings["ComPortName"] != null)
            {
                ComPortName = AppSet.ReadSetting("ComPortName");
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
    