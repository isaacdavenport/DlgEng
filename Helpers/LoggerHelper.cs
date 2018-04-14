﻿//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using log4net;
using System;
using System.Runtime.CompilerServices;


namespace DialogEngine.Helpers
{
    /// <summary>
    /// Logger factory class
    /// </summary>
    public static class LoggerHelper
    {
        #region - fields -

        private static readonly ILog mcDecimalSerialLog = LogManager.GetLogger(SessionHelper.DecimalLogFileName);
        private static readonly ILog mcLogDialog = LogManager.GetLogger(SessionHelper.DialogLogFileName);

        #endregion

        #region - private functions -

        // returns logger depends on type
        private static ILog _getLogger(string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                return null;
            }
            else if( string.Equals(type, SessionHelper.DecimalLogFileName, StringComparison.OrdinalIgnoreCase))
            {
                return mcDecimalSerialLog;
            }
            else if(string.Equals(type, SessionHelper.DialogLogFileName, StringComparison.OrdinalIgnoreCase))
            {
                return mcLogDialog;
            }

            return null;
        }

        #endregion

        #region - public functions -

        /// <summary>
        /// Logs error message
        /// </summary>
        /// <param name="_loggerType">Type of logger</param>
        /// <param name="_message">Custom message</param>
        /// <param name="_file">File name where method is called</param>
        /// <param name="_line">Line in file where method is called</param>
        public static void Error(string _loggerType,string _message, [CallerFilePath] string _file = "", [CallerLineNumber] int _line = 0)
        {
            _getLogger(_loggerType)?.Error(_file + " " + _line + " Message : " + _message);
        }


        /// <summary>
        /// Logs warning message
        /// </summary>
        /// <param name="_loggerType">Type of logger</param>
        /// <param name="_message">Custom message</param>
        /// <param name="_file">File name where method is called</param>
        /// <param name="_line">Line in file where method is called</param>
        public static void Warning(string _loggerType, string _message, [CallerFilePath] string _file = "", [CallerLineNumber] int _line = 0)
        {
            _getLogger(_loggerType)?.Warn(_file + " " + _line + " Message : " + _message);
        }

        /// <summary>
        /// Logs info message
        /// </summary>
        /// <param name="_loggerType">Type of logger</param>
        /// <param name="_message">Custom message</param>
        /// <param name="_file">File name where method is called</param>
        /// <param name="_line">Line in file where method is called</param>
        public static void Info(string _loggerType, string _message, [CallerFilePath] string _file = "", [CallerLineNumber] int _line = 0)
        {
            _getLogger(_loggerType)?.Info( _message);
        }

        #endregion
    }
}
