using System;
using System.Configuration;

namespace DialogEngine.Helpers
{
    /// <summary>
    /// Helper class which search for properties with defined key in App.config file 
    /// </summary>
    class AppSet
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_key"></param>
        /// <returns>Value in App.config for assigned key</returns>
        public static string ReadSetting(string _key) {  //returns null if key or file not found
            try {
                var _appSettings = ConfigurationManager.AppSettings;
                string _result = _appSettings[_key];
                if (_result == null) {
                    Console.WriteLine(_key + " not found in AppSettings File.");
                }
                return _result;
            }
            catch (ConfigurationErrorsException) {
                Console.WriteLine("Error reading app settings");
                return null;
            }
        }
    }
}
