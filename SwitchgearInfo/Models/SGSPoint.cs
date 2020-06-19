using SwitchgearInfo.Connections;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SwitchgearInfo.Models
{
    [Table("SGSPoints")]
    public class SGSPoint : ModelPropertyChanges
    {
        int _IdSGSection = -1;
        string _ShortName;
        string _FullName;
        DateTime _LastDTSafeValue;
        string _Explantation;

        /// <summary>
        /// Идентификатор точки
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор секции в которой расположена точка
        /// </summary>
        public int IdSGSection
        {
            get { return _IdSGSection; }
            set
            {
                if (_IdSGSection != value)
                {
                    _IdSGSection = value;
                    OnPropertyChanged("IdSwitchgear");
                }
            }
        }

        /// <summary>
        /// Краткое наименование точки
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
        /// Полное наименоваине точки
        /// </summary>
        public string FullName
        {
            get { return _FullName; }
            set
            {
                if (!_FullName.Equals(value))
                {
                    _FullName = value;
                    OnPropertyChanged("ShortName");
                }
            }
        }

        /// <summary>
        /// Дата последнего сохранения данных точки
        /// </summary>
        public DateTime LastDTSafeValue
        {
            get
            {
                return _LastDTSafeValue;
            }
            set
            {
                if (_LastDTSafeValue != value)
                {
                    _LastDTSafeValue = value;
                    OnPropertyChanged("LastDTSafeValue");
                }
            }
        }
        
        /// <summary>
        /// Примечание/пояснение/расшифровка
        /// </summary>
        public string Explantation
        {
            get { return _Explantation; }
            set
            {
                if (!_Explantation.Equals(value))
                {
                    _Explantation = value;
                    OnPropertyChanged("ShortName");
                }
            }
        }

        /// <summary>
        /// получает все точки
        /// </summary>
        public static List<SGSPoint> GetAll
        {
            get
            {
                var res = new DefaultContext<SGSPoint>();

                return res.Values.ToList();
            }
        }

        public override string ToString()
        {
            return $"{{\"Id\":{Id},\"IdSGSection\":{IdSGSection},\"ShortName\":\"{ShortName}\",\"FullName\":\"{FullName}\",\"LastDTSafeValue\":\"{LastDTSafeValue.ToString("dd.MM.yyyy HH:mm:ss,FFF")}\",\"Explantation\":\"{Explantation}\"}}";
        }

    }
}
