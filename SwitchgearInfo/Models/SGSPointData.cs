using SwitchgearInfo.Connections;
using SwitchgearInfo.Models.XML;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SwitchgearInfo.Models
{
    [Table("SGSPointData")]
    public class SGSPointData : ModelPropertyChanges, IRow
    {
        long _IdSGSPoint = -1;
        DateTime _DateOfValue;
        double _PointValue;
        string _Explantation;

        /// <summary>
        /// Идентификатор точки данных
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор точки
        /// </summary>
        public long IdSGSPoint
        {
            get { return _IdSGSPoint; }
            set
            {
                if (_IdSGSPoint != value)
                {
                    _IdSGSPoint = value;
                    OnPropertyChanged("IdSGSPoint");
                }
            }
        }

        /// <summary>
        /// Дата и время значений
        /// </summary>
        public DateTime DateOfValue
        {
            get { return _DateOfValue; }
            set
            {
                if (_DateOfValue != value)
                {
                    _DateOfValue = value;
                    OnPropertyChanged("DateOfValue");
                }
            }
        }

        /// <summary>
        /// Значение
        /// </summary>
        public double PointValue
        {
            get { return _PointValue; }
            set
            {
                if (_PointValue != value)
                {
                    _PointValue = value;
                    OnPropertyChanged("PointValue");
                }
            }
        }

        /// <summary>
        /// Примечание
        /// </summary>
        public string Explantation
        {
            get { return _Explantation; }
            set
            {
                if (_Explantation != value)
                {
                    _Explantation = value;
                    OnPropertyChanged("Explantation");
                }
            }
        }

        /// <summary>
        /// Набор данных для точки
        /// </summary>
        /// <param name="PointId">Идентификатор точки</param>
        /// <returns>Набор данных для точки</returns>
        public static List<SGSPointData> GetPointData(long PointId)
        {
            var res = new DefaultContext<SGSPointData>();
            return res.Values.Where(m=>m.IdSGSPoint==PointId && m.DateOfValue >= DateTime.Now.AddMonths(-3) && m.DateOfValue <= DateTime.Now).OrderBy(m => m.DateOfValue).ToList();
        }

        /// <summary>
        /// Получает набор данных для точки в промежутке между датами
        /// </summary>
        /// <param name="PointId">Идентификатор точки</param>
        /// <param name="DateFrom">Дата начала промежутка</param>
        /// <param name="DateTo">Дата окончания промежутка</param>
        /// <returns>Набор данных для точки</returns>
        public static List<SGSPointData> GetPointData(long PointId, DateTime DateFrom, DateTime DateTo)
        {
            var res = new DefaultContext<SGSPointData>();
            return res.Values.Where(m => m.IdSGSPoint == PointId && m.DateOfValue >= DateFrom && m.DateOfValue <= DateTo).OrderBy(m => m.DateOfValue).ToList();

        }

        /// <summary>
        /// Получает данные для набора точек
        /// </summary>
        /// <param name="PointsId">Набор точек для которых необходимо получить занчения</param>
        /// <returns>Набор данных для набора точек</returns>
        public static List<SGSPointData> GetPointData(IEnumerable<long> PointsId)
        {
            var res = new DefaultContext<SGSPointData>();
            return res.Values.Where(m => PointsId.Contains(m.IdSGSPoint) && m.DateOfValue >= DateTime.Now.AddMonths(-3) && m.DateOfValue <= DateTime.Now).OrderBy(m => m.DateOfValue).ToList();
        }

        /// <summary>
        /// Получает данные для набора точек за определенный промежуток времени
        /// </summary>
        /// <param name="PointsId">Набор точек для которых необходимо получить занчения</param>
        /// <param name="DateFrom">Дата начала промежутка</param>
        /// <param name="DateTo">Дата окончания промежутка</param>
        /// <returns>данные для набора точек за определенный промежуток времени</returns>
        public static List<SGSPointData> GetPointData(IEnumerable<long> PointsId, DateTime DateFrom, DateTime DateTo)
        {
            if(PointsId!=null && PointsId.ToList().Count > 0)
            {
                var res = new DefaultContext<SGSPointData>();
                return res.Values.Where(m => PointsId.Contains(m.IdSGSPoint) && m.DateOfValue >= DateFrom && m.DateOfValue <= DateTo).OrderBy(m=>m.DateOfValue).ToList();
            }
            else
            {
                return new List<SGSPointData>();
            }
        }

        /// <summary>
        /// Получает набор значений соответствующих условиям отбора одной точки
        /// </summary>
        /// <param name="DataPoint">Условия отбора одной точки</param>
        /// <returns></returns>
        public static List<SGSPointData> GetPointData(OnePointData DataPoint)
        {
            var res = new UniversalModel<SGSPointData>();
            res.QParameters.Add(new SqlParameter("@Point", DataPoint.ToXMLString()));
            return res.GetFromDBList("GetPointData");
        }

        /// <summary>
        /// Получает данные для набора точек за определенный промежуток времени
        /// </summary>
        /// <param name="MPD">Набор идентификаторов точек с промежутками дат</param>
        /// <returns>данные для набора точек за определенный промежуток времени</returns>
        public static List<SGSPointData> GetPointData(ManyPointsData MPD)
        {
            var res = new UniversalModel<SGSPointData>();
            res.QParameters.Add(new SqlParameter("@Points", MPD.ToXMLString()));
            return res.GetFromDBList("GetPointData");
        }

        /// <summary>
        /// Получает набор последних данных из бд через Join
        /// </summary>
        /// <returns></returns>
        public static List<SGSPointData> GetPointData()
        {
            using (var context = new JoinContext())
            {
               var res = (from pd in context.PointsData
                           join p in context.Points on pd.IdSGSPoint equals p.Id 
                           where p.LastDTSafeValue == pd.DateOfValue
                           select pd).ToList();
                return res;
            }
        }

        /// <summary>
        /// Получает набор последних данных путем обращения к хранимой процедуре
        /// </summary>
        /// <returns></returns>
        public static List<SGSPointData> GetPointDataQ()
        {
            var res = new UniversalModel<SGSPointData>();
            return res.GetFromDBList("LastDataPointValues");
        }

        public override string ToString()
        {
            return $@"{{
    ""IdSGSPoint"":{IdSGSPoint},
    ""DateOfValue"":""{DateOfValue.ToString("dd.MM.yyyy HH:mm:ss,FFF")}"",
    ""PointValue"":{PointValue},
    ""Explantation"":""{Explantation}""
}}";
        }

        /// <summary>
        /// Проверяет на наличие модели в БД, 
        /// если не существует, то добавляет, 
        /// если существует, но значение изменилось то заменяет новым значением, 
        /// если значение не менялось, то ничего не делает. 
        /// При этом каждой модели присваивается номер и сообщение о выполнении операции, 
        /// что и возвращается в виде набора
        /// </summary>
        /// <param name="SGSPointsData">Набор точек которые пытаемя положить в БД</param>
        /// <returns></returns>
        public static List<Messadge> Put(IEnumerable<SGSPointData> SGSPointsData)
        {
            var res = new List<Messadge>();            
            var spdl = SGSPointsData.ToList();
            var dspd = new Dictionary<int, SGSPointData>();
            foreach (var m in spdl)
            {
                dspd.Add(spdl.IndexOf(m) + 1, m);
                             
            }
            var context = new DefaultContext<SGSPointData>();
            var duspd = new Dictionary<int, SGSPointData>();
            var dispd = new Dictionary<int, SGSPointData>();
            var dnspd = new Dictionary<int, SGSPointData>();
            foreach(var kvp in dspd)
            {
                var mdb = context.Values.Where(m => m.IdSGSPoint == kvp.Value.IdSGSPoint && m.DateOfValue == kvp.Value.DateOfValue).FirstOrDefault();
                if (mdb != null)
                {
                    if (mdb.PointValue != kvp.Value.PointValue)
                    {
                        duspd.Add(kvp.Key, kvp.Value);
                    }
                    else
                    {
                        dnspd.Add(kvp.Key, kvp.Value);
                    }
                }
                else
                {
                    dispd.Add(kvp.Key, kvp.Value);
                }
            }
            var nupt = Task<List<Messadge>>.Factory.StartNew(() => {
                return GetNothingMessadge(dnspd);
            });
            var upt = Task<List<Messadge>>.Factory.StartNew(() => {
                return GetUPDMessadge(duspd);
            });
            var ipt = Task<List<Messadge>>.Factory.StartNew(() => {
                return GetINSMessadge(dispd);
            });

            res.AddRange(nupt.Result);
            res.AddRange(upt.Result);
            res.AddRange(ipt.Result);
            return res.OrderBy(m=>m.MNumber).ToList();
        }

        /// <summary>
        /// Производит попытку добавить данные в БД, 
        /// и выдает сообщение об проведенной/не проведенной операции
        /// </summary>
        /// <param name="Model">Модель которую пытаемся положить в БД</param>
        /// <param name="NMessadge">Порядковый номер модели в наборе</param>
        /// <returns></returns>
        [Obsolete]
        static Messadge GetMessadge (SGSPointData Model, int NMessadge)
        {
            var res = new Messadge { MNumber = NMessadge };
            Console.WriteLine(Model);
            var context = new DefaultContext<SGSPointData>();
            var mdb = context.Values.Where(m => m.IdSGSPoint == Model.IdSGSPoint && m.DateOfValue == Model.DateOfValue).FirstOrDefault();
            if (mdb != null)
            {
                if (mdb.PointValue != Model.PointValue)
                {
                    mdb.PointValue = Model.PointValue;
                    context.SaveChangesAsync();
                    res.MessadgeString = "Модель существует в БД, значение обновлено.";
                }
                else
                {
                    res.MessadgeString = "Модель существует в БД, данные не обновлены.";
                }
            }
            else
            {
                context.Values.Add(Model);
                context.SaveChangesAsync();
                var pContext = new DefaultContext<SGSPoint>();
                var p = pContext.Values.Where(m => m.Id == Model.IdSGSPoint).FirstOrDefault();
                if (p.LastDTSafeValue < Model.DateOfValue)
                {
                    p.LastDTSafeValue = Model.DateOfValue;
                    pContext.SaveChanges();
                }
                res.MessadgeString = "Модель успешно добавлена в БД.";
            }
            return res;
        }

        /// <summary>
        /// Сообщения о том что модель не изменилась
        /// </summary>
        /// <param name="NDict">Набор моделей которые не изменились с порядковыми номерами</param>
        /// <returns>Набор сообщений</returns>
        static List<Messadge> GetNothingMessadge(Dictionary<int, SGSPointData> NDict)
        {
            var res = new List<Messadge>();
            foreach(var kvp in NDict)
            {
                res.Add(new Messadge {
                    MNumber = kvp.Key,
                    MessadgeString = "Модель существует в БД, данные не обновлены."
                });
            }
            return res;
        }

        /// <summary>
        /// Обновляет модели и сообщает об изменении
        /// </summary>
        /// <param name="UDict">Набор моделей для обновления</param>
        /// <returns>Набор сообщений</returns>
        static List<Messadge> GetUPDMessadge(Dictionary<int, SGSPointData> UDict)
        {
            var res = new List<Messadge>();
            using (var context = new DefaultContext<SGSPointData>())
            {
                foreach (var kvp in UDict)
                {
                    var model = context.Values.Where(m => m.IdSGSPoint == kvp.Value.IdSGSPoint && m.DateOfValue == kvp.Value.DateOfValue).FirstOrDefault();
                    model.PointValue = kvp.Value.PointValue;
                    res.Add(new Messadge
                    {
                        MNumber = kvp.Key,
                        MessadgeString = "Модель существует в БД, значение обновлено."
                    });
                    
                }
                context.SaveChanges();
            }                                      
            return res;
        }

        /// <summary>
        /// Добавляет модели в БД и формирует по ним сообщения
        /// </summary>
        /// <param name="DictIns">Набор моделей для добавления с порядковыми номерами</param>
        /// <returns>Набор сообщений</returns>
        static List<Messadge> GetINSMessadge(Dictionary<int, SGSPointData> DictIns)
        {
            var res = new List<Messadge>();
            using (var context = new DefaultContext<SGSPointData>())
            {                
                foreach (var kvp in DictIns)
                {
                    context.Values.Add(kvp.Value);
                    res.Add(new Messadge
                    {
                        MNumber = kvp.Key,
                        MessadgeString = "Модель успешно добавлена в БД."
                    });
                    using (var pContext = new DefaultContext<SGSPoint>())
                    {
                        var p = pContext.Values.Where(m => m.Id == kvp.Value.IdSGSPoint).FirstOrDefault();
                        if (p.LastDTSafeValue < kvp.Value.DateOfValue)
                        {
                            p.LastDTSafeValue = kvp.Value.DateOfValue;
                            pContext.SaveChanges();
                        }
                    }
                }
                context.SaveChanges();
                
            }
                return res;
        }

        /// <summary>
        /// Преобразует DataRow в модель
        /// </summary>
        /// <param name="Row">DataRow для преобразования</param>
        public void FromRow(DataRow Row)
        {
            Id = Row["Id"] == DBNull.Value ? 0 : Convert.ToInt64(Row["Id"]);
            IdSGSPoint = Row["IdSGSPoint"] == DBNull.Value ? 0 : Convert.ToInt64(Row["IdSGSPoint"]);
            DateOfValue = Row["DateOfValue"] == DBNull.Value ? new DateTime() : Convert.ToDateTime(Row["DateOfValue"]);
            PointValue = Row["PointValue"] == DBNull.Value ? 0 : Convert.ToDouble(Row["PointValue"]);
            Explantation = Row["Explantation"].ToString();
        }
    }
       
}
