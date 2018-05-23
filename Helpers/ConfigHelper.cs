
using DialogEngine.Models.Shared;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DialogEngine.Helpers
{
    public class ConfigHelper
    {
        #region - fields-

        private static readonly ILog mcLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static ConfigHelper mInstance;
        private static object mLocker = new object();
        private List<Setting> mSettings;
        private Dictionary<string, Setting> mSettingsDict;
        private string mSettingsFilePath = Path.Combine(SessionHelper.BaseDirectory, "ApplicationSettings.xml");
        private XmlSerializer mSerializer;

        #endregion

        #region - constructor -

        public ConfigHelper()
        {
            XmlRootAttribute _xRoot = new XmlRootAttribute();
            _xRoot.ElementName = "Settings";
            _xRoot.IsNullable = true;
            mSerializer = new XmlSerializer(typeof(List<Setting>), _xRoot);
        }

        #endregion

        #region - public functions -

        public async Task DeserializeSettingsFromFile()
        {
            await Task.Run(() =>
            {
                try
                {
                    StreamReader reader = new StreamReader(mSettingsFilePath);
                    Settings = (List<Setting>)mSerializer.Deserialize(reader);
                }
                catch (Exception ex)
                {
                    mcLogger.Error("DeserializeSettingsFromFile. " + ex.Message);
                }
            });
        }

        public  Task SerializeSettingsToFile()
        {
            return Task.Run(() =>
            {
                try
                {
                    FileStream fs = new FileStream(mSettingsFilePath, FileMode.Create);
                    mSerializer.Serialize(fs, Settings);
                }
                catch (Exception ex)
                {
                    mcLogger.Error("SerializeSettingsToFile. " + ex.Message);
                }
            });
        }

        public string GetValue(string key)
        {
            try
            {
                return mSettingsDict[key].Value;
            }
            catch (Exception ex)
            {
                mcLogger.Error("GetValue " + ex.Message);

                return null;
            }
        }

        public void SetValue(string key,string value)
        {
            try
            {
                 mSettingsDict[key].Value = value;
            }
            catch (Exception ex)
            {
                mcLogger.Error("SetValue " + ex.Message);
            }
        }

        #endregion

        #region - properties -

        /// <summary>
        /// Gets Config instance.
        /// </summary>
        public static ConfigHelper Instance
        {
            get
            {
                lock (mLocker)
                {
                    if (mInstance == null)
                    {
                        mInstance = new ConfigHelper();
                    }
                }

                return mInstance;
            }
        }


        public List<Setting> Settings
        {
            get { return mSettings; }
            set
            {
                mSettings = value;

                mSettingsDict = new Dictionary<string, Setting>();
                foreach(Setting setting in mSettings)
                {
                    mSettingsDict.Add(setting.Key, setting);
                }
            }
        }

        #endregion
    }
}
