

using System;
using System.Xml.Serialization;

namespace DialogEngine.Models.Shared
{
    [Serializable()]
    public class Setting
    {
        [XmlAttribute("Key")]
        public string Key { get; set; }

        [XmlAttribute("Value")]
        public string Value { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("RegexText")]
        public string RegexText { get; set; }

        [XmlAttribute("ErrorMessage")]
        public string ErrorMessage { get; set; }

        [XmlText]
        public string Description { get; set; }
    }
}
