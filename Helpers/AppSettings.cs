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
        /// <param name="key"></param>
        /// <returns>Value in App.config for assigned key</returns>
        public static string ReadSetting(string key) {  //returns null if key or file not found
            try {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key];
                if (result == null) {
                    Console.WriteLine(key + " not found in AppSettings File.");
                }
                return result;
            }
            catch (ConfigurationErrorsException) {
                Console.WriteLine("Error reading app settings");
                return null;
            }
        }
    }
}
