using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SwitchgearInfo.Models.XML
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class OnePointData
    {
        public long Id { get; set; }
        [DefaultValue(typeof(DateTime), "0001-01-01T00:00:00")]
        public DateTime DateFrom { get; set; }
        [DefaultValue(typeof(DateTime), "0001-01-01T00:00:00")]
        public DateTime DateTo { get; set; }

        public override string ToString()
        {
            return $"{{\"Id\":{Id},\"DateFrom\":\"{DateFrom}\",\"DateTo\":\"{DateTo}\"}}";
        }

        /// <summary>
        /// Преобразует модель к строковому виду путем сериализации данных в XML
        /// </summary>
        /// <returns>XML строку с данными модели</returns>
        public string ToXMLString()
        {
            var res = "";
            using (TextWriter tw = new StringWriter())
            {
                var serialiser = new XmlSerializer(typeof(OnePointData));
                serialiser.Serialize(tw, this);
                res = tw.ToString();
            }

            return res.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
        }
    }
}
