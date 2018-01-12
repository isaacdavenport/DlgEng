﻿//Confidential Source Code Property Toys2Life LLC Colorado 2017
//www.toys2life.org

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
    public static class SessionVariables
    {
        private static readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        public static bool ShowDupePhrases;
        public static bool MonitorReceiveBufferSize;
        public static bool MonitorMessageParseFails;
        public static string BaseDirectory;
        public static string LogsDirectory;
        public static string CharactersDirectory;
        public static string DialogsDirectory;
        public static string AudioDirectory;
        public static string DecimalLogFileName = "DecimalSerialLog.txt";
        public static string HexLogFileName = "HexSerialLog.txt";
        public static string DialogLogFileName = "LogDialog.txt";

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


        /// <summary>
        /// Static constructor
        /// </summary>
        static SessionVariables()
        {

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
                mcLogger.Debug("Valid LogsDirectory path not found in config file exiting.");
                Environment.Exit(0);
            }
        

            if (ConfigurationManager.AppSettings["CharactersDirectory"] != null)
                CharactersDirectory = AppSet.ReadSetting("CharactersDirectory");

            if (CharactersDirectory == null)
            {
                mcLogger.Debug("Valid CharactersDirectory path not found in config file exiting.");
                Environment.Exit(0);
            }

            if (ConfigurationManager.AppSettings["DialogsDirectory"] != null)
                DialogsDirectory = AppSet.ReadSetting("DialogsDirectory");
            
            if(DialogsDirectory == null)
            {
                mcLogger.Debug("Valid DialogsDirectory path not found in config file exiting.");
                Environment.Exit(0);
            }

            if (ConfigurationManager.AppSettings["AudioDirectory"] != null)
            {
                AudioDirectory = AppSet.ReadSetting("AudioDirectory");
            }

            if (AudioDirectory == null && AudioDialogsOn)
            { 
                mcLogger.Debug("Valid AudioDirectory path not found in config file exiting.");
                Environment.Exit(0);
            }


            if (ComPortName == null && UseSerialPort)
            {
                mcLogger.Debug("Valid ComPortName  not found in config file exiting.");
                Environment.Exit(0);
            }



            DecimalLogFileName = "DecimalSerialLog.txt";  //TODO are these redundant?
            HexLogFileName = "HexSerialLog.txt";
            DialogLogFileName = "LogDialog.txt";
        }
    }
}
    