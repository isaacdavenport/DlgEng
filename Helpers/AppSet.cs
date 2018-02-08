//  Confidential Source Code Property Toys2Life LLC Colorado 2017
//  www.toys2life.org

using log4net;
using System;
using System.Configuration;
using System.Reflection;

namespace DialogEngine.Helpers
{
    /// <summary>
    /// Helper class which searches for properties with defined key in App.config file 
    /// </summary>
    class AppSet
    {

        private static readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        /// <summary>
        /// Try to find value in ConfigurationManager.AppSettings for a specified key
        /// </summary>
        /// <param name="_key"></param>
        /// <returns>Value in App.config for a specified key, or null if key not found</returns>
        public static string ReadSetting(string _key)
        {



            try
            {
                var _AppSettings = ConfigurationManager.AppSettings;
                string _result = _AppSettings[_key];
                if (_result == null)
                {
                    mcLogger.Debug(_key + " not found in AppSettings File.");
                }
                return _result;
            }
            catch (ConfigurationErrorsException)
            {
                mcLogger.Debug("Error reading app settings");
                return null;
            }
        }
    }
}
