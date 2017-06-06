//Confidential Source Code Property Toys2Life LLC Colorado 2017
//www.toys2life.org

using System;
using System.Configuration;

namespace DialogEngine
{
    public static class SessionVars
    {

        public static readonly bool DebugFlag;
        public static readonly bool TagUsageCheck;
        public static readonly bool AudioDialogsOn;
        public static readonly bool TextDialogsOn;
        public static readonly bool ForceCharactersAndDialogModel;
        public static readonly bool WaitIndefinatelyForMove;
        public static readonly bool ShowDupePhrases;
        public static readonly bool HeatMapFullMatrixDispMode;
        public static readonly bool HeatMapSumsMode;
        public static readonly bool HeatMapOnlyMode;
        public static readonly bool WriteSerialLog;
        public static readonly bool NoSerialPort;
        public static readonly bool CheckStuckTransmissions;
        public static readonly bool MonitorReceiveBufferSize;
        public static readonly bool MonitorMessageParseFails;
        public static readonly string CurrentParentalRating;
        public static readonly string LogsDirectory;
        public static readonly string CharactersDirectory;
        public static readonly string DialogsDirectory;
        public static readonly string AudioDirectory;
        public static readonly string DecimalLogFileName = "DecimalSerialLog.txt";
        public static readonly string HexLogFileName = "HexSerialLog.txt";
        public static readonly string DialogLogFileName = "LogDialog.txt";
        public static readonly string ComPortName;

        static SessionVars() {
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

            if (ConfigurationManager.AppSettings["NoSerialPort"] != null)
                NoSerialPort = Convert.ToBoolean(AppSet.ReadSetting("NoSerialPort"));
            else
                NoSerialPort = true;

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

            if (ConfigurationManager.AppSettings["LogsDirectory"] == null && WriteSerialLog) {
                Console.WriteLine("Valid LogsDirectory path not found in config file exiting.");
                Console.ReadLine();
                Environment.Exit(0);
            }

            if (ConfigurationManager.AppSettings["LogsDirectory"] != null)
                LogsDirectory = AppSet.ReadSetting("LogsDirectory");
            else
                LogsDirectory = "";  //logs not used based on check above

            if (ConfigurationManager.AppSettings["CharactersDirectory"] != null)
                CharactersDirectory = AppSet.ReadSetting("CharactersDirectory");
            else {
                Console.WriteLine("Valid CharactersDirectory path not found in config file exiting.");
                Console.ReadLine();
                Environment.Exit(0);
            }

            if (ConfigurationManager.AppSettings["DialogsDirectory"] != null)
                DialogsDirectory = AppSet.ReadSetting("DialogsDirectory");
            else
            {
                Console.WriteLine("Valid DialogsDirectory path not found in config file exiting.");
                Console.ReadLine();
                Environment.Exit(0);
            }

            if (ConfigurationManager.AppSettings["AudioDirectory"] == null && AudioDialogsOn)
            {
                Console.WriteLine("Valid AudioDirectory path not found in config file exiting.");
                Console.ReadLine();
                Environment.Exit(0);
            }

            if (ConfigurationManager.AppSettings["AudioDirectory"] != null)
                AudioDirectory = AppSet.ReadSetting("AudioDirectory");
            else
                AudioDirectory = "";  //Audio not used based on check above


            if (ConfigurationManager.AppSettings["ComPortName"] == null && !NoSerialPort)
            {
                Console.WriteLine("Valid ComPortName  not found in config file exiting.");
                Console.ReadLine();
                Environment.Exit(0);
            }

            if (ConfigurationManager.AppSettings["ComPortName"] != null)
                ComPortName = AppSet.ReadSetting("ComPortName");
            else
                ComPortName = "";  //serial not used based on check above

            DecimalLogFileName = "DecimalSerialLog.txt";  //TODO are these redundant?
            HexLogFileName = "HexSerialLog.txt";
            DialogLogFileName = "LogDialog.txt";
        }
    }
}
    