using SwitchgearInfo.Connections;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchgearInfo.Models
{
    [Table("SGSections")]
    public class SGSection : ModelPropertyChanges
    {
        int _IdSwitchgear=-1;
        string _ShortName;
        string _FullName;
        string _Explantation;

        /// <summary>
        /// Идентификатор
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Идентификатор распред устройства
        /// </summary>
        public int IdSwitchgear
        {
            get { return _IdSwitchgear; }
            set
            {
                if (_IdSwitchgear != value)
                {
                    _IdSwitchgear = value;
                    OnPropertyChanged("IdSwitchgear");
                }
            }
        }

        /// <summary>
        /// Красткое наименование секции
        /// </summary>
        public string ShortName
        {
            get { return _ShortName; }
            set
            {
                if (!_ShortName.Equals(value))
                {
                    _ShortName = value;
                    OnPropertyChanged("ShortName");
                }
            }
        }

        /// <summary>
        /// Полное наименоваине секции
        /// </summary>
        public string FullName
        {
            get { return _FullName; }
            set
            {
                if (!_FullName.Equals(value))
                {
                    _FullName = value;
                    OnPropertyChanged("FullName");
                }
            }
        }

        /// <summary>
        /// Примечание/Пояснение
        /// </summary>
        public string Explantation
        {
            get { return _Explantation; }
            set
            {
                if (!_Explantation.Equals(value))
                {
                    _Explantation = value;
                    OnPropertyChanged("Explantation");
                }
            }
        }
       
        /// <summary>
        /// Получает все секции распред устройств
        /// </summary>
        public static List<SGSection> GetAll
        {
            get
            {
                var context = new DefaultContext<SGSection>();

                return context.Values.ToList();
            }
        }

        /// <summary>
        /// Получает секцию по идентификатору
        /// </summary>
        /// <param name="Id">Идентификатор секции</param>
        /// <returns>Модель секции</returns>
        public static SGSection GetById(int Id)
        {
            var res = new DefaultContext<SGSection>();
            return res.Values.Where(m=>m.id==Id).FirstOrDefault();
        }

        /// <summary>
        /// Точки принадлежащие к текущей секции
        /// </summary>
        public List<SGSPoint> Points
        {
            get
            {
                var res = new DefaultContext<SGSPoint>();

                return res.Values.Where(m=>m.IdSGSection==id).ToList();
            }
        }

        public override string ToString()
        {
            return $"{{\"id\":{id},\"IdSwitchgear\":{IdSwitchgear},\"ShortName\":\"{ShortName}\",\"FullName\":\"{FullName}\",\"Explantation\":\"{Explantation}\"{PointsStr}}}";
        }

        /// <summary>
        /// Строка с данными точек принадлежащих к секции
        /// </summary>
        string PointsStr
        {
            get
            {
                var res = new StringBuilder();
                if (Points.Count > 0)
                {
                    res.AppendLine(",");
                    res.AppendLine("Points:{");
                    var first = true;
                    foreach (var m in Points)
                    {
                        if (!first)
                        {
                            res.Append(",");
                            res.AppendLine($"{m}");
                        }
                        else
                        {
                            res.AppendLine($"{m}");
                            first = false;
                        }
                    }
                    res.AppendLine("}");
                }
                return res.ToString();
            }
        }

    }
}
