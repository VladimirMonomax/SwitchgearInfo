using SwitchgearInfo.Connections;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchgearInfo.Models
{
    [Table("Switchgears")]
    public class Switchgear: ModelPropertyChanges
    {
        string _NameSw;
        string _FullName;
        string _Explantation;
            
        /// <summary>
        /// Идентификатор распред устройства
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Краткое наименование распред устройства
        /// </summary>
        public string NameSw {
            get { return _NameSw; }
            set {
                if (!_NameSw.Equals(value))
                {
                    _NameSw = value;
                    OnPropertyChanged("NameSw");
                }                
            }
        }

        /// <summary>
        /// Полное наименование распред устройства
        /// </summary>
        public string FullName
        {
            get { return _FullName; }
            set {
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
            set {
                if (!_Explantation.Equals(value))
                {
                    _Explantation = value;
                    OnPropertyChanged("Explantation");
                }                
            }
        }

        /// <summary>
        /// Секции принадлежащие к распред устройсву секции
        /// </summary>
        public List<SGSection> Sections
        {
            get
            {
                var context = new DefaultContext<SGSection>();
                return context.Values.Where(m=>m.IdSwitchgear==id).ToList();
            }
        }
        
        /// <summary>
        /// Получает все распред устройства
        /// </summary>
        public static List<Switchgear> GetAll
        {
            get
            {
                var context = new DefaultContext<Switchgear>();
                return context.Values.ToList();
            }
        }

        public override string ToString()
        {
            return $"{{\"id\":{id},\"NameSw\":\"{NameSw}\",\"FullName\":\"{FullName}\",\"Explantation\":\"{Explantation}\"{Sectionsstr}}}";
        }

        /// <summary>
        /// Преобразование в подобную строку всех сеций текущего экземпляра
        /// </summary>
        string Sectionsstr
        {
            get
            {
                var res = new StringBuilder();
                if (Sections.Count > 0)
                {
                    res.AppendLine(",");
                    res.AppendLine("Sections:{");
                    var first = true;
                    foreach(var m in Sections)
                    {
                        if (!first)
                        {
                            res.Append(",");
                            res.AppendLine($"{m}");
                        }
                        else {
                            res.AppendLine($"{m}");
                            first = false;
                        }                                               
                    }

                    res.AppendLine("}");
                }
                return res.ToString();
            }
        }

        /// <summary>
        /// Получает распред устройство по идентификатору
        /// </summary>
        /// <param name="Id">Идентификатор распред устройства</param>
        /// <returns>Распред устройство с определеным идентификатором</returns>
        public static Switchgear GetById(int Id)
        {
            var context = new DefaultContext<Switchgear>();
            return context.Values.Where(m => m.id == Id).FirstOrDefault();
        }
    }
}
