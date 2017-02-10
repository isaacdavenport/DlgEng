using System;
using System.Configuration;

namespace DialogEngine
{
    class AppSet
    {
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
