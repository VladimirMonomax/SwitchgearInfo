using System;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;

namespace SwitchgearInfo.Models.XML
{

    // Примечание. Для запуска созданного кода может потребоваться NET Framework версии 4.5 или более поздней версии и .NET Core или Standard версии 2.0 или более поздней.
    /// <remarks/>
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public partial class Settings
    {        
        public SQLConnection[] Connection { get; set; }

        public static Settings SettingsFromXML
        {
            get
            {
                using (Stream reader = new FileStream("settings.xml", FileMode.Open))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                    return (Settings)serializer.Deserialize(reader);
                }
            }
        }

        public override string ToString()
        {
            using (TextWriter tw = new StringWriter())
            {
                var serialiser = new XmlSerializer(typeof(Settings));
                serialiser.Serialize(tw, this);
                return tw.ToString();
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class Connection
    {
        public SQLConnection[] SQLConnection { get; set; }
    }

    /// <remarks/>
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class SQLConnection
    {       
        /// <remarks/>
        [XmlAttribute()]
        public string TypeSQL { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public string Server { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public string Catalog { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public string User { get; set; }
    }


}
