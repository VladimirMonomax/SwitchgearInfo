using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;

namespace SwitchgearInfo.Models.XML
{
    /// <summary>
    /// Модель для сериализации в XML
    /// </summary>
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class SGSPDPut
    {
        /// <summary>
        /// Массив SGSPointData, который будет сериализован
        /// </summary>
        public SGSPointData[] DataPoints { get; set; }

        /// <summary>
        /// Добавляет единичную модель в набор для сериализации
        /// </summary>
        /// <param name="PD">Модель для добавления</param>
        public void Add(SGSPointData PD)
        {
            if (DataPoints != null)
            {
                var res = new List<SGSPointData>();
                res.AddRange(DataPoints);
                res.Add(PD);
                DataPoints = res.ToArray();
            }
            else
            {
                DataPoints = new SGSPointData[1] { PD };
            }
        }

        /// <summary>
        /// Добавляет набор моделей для сериализации
        /// </summary>
        /// <param name="PD">Набор моделей</param>
        public void AddRange(IEnumerable<SGSPointData> PD)
        {
            if (DataPoints != null)
            {
                var res = new List<SGSPointData>();
                res.AddRange(DataPoints);
                res.AddRange(PD);
                DataPoints = res.ToArray();
            }
            else
            {
                var res = new List<SGSPointData>();                
                res.AddRange(PD);
                DataPoints = res.ToArray();
            }
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
                var serialiser = new XmlSerializer(typeof(SGSPDPut));
                serialiser.Serialize(tw, this);
                res = tw.ToString();
            }

            return res.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
        }
    }
}
