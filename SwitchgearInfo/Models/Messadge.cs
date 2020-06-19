using SwitchgearInfo.Models.XML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SwitchgearInfo.Models
{
    public class Messadge : IRow
    {
        /// <summary>
        /// Порядковый номер по модели SGSPointData пришедшей на кконтроллер
        /// </summary>
        public int MNumber { get; set; }
        /// <summary>
        /// Сообщение об операции
        /// </summary>
        public string MessadgeString { get; set; }

        /// <summary>
        /// Преобразует в модель из DataRow
        /// </summary>
        /// <param name="Row">DataRow с данными модели</param>
        public void FromRow(DataRow Row)
        {
            MNumber = Row["MNumber"] == DBNull.Value ? 0 : Convert.ToInt32(Row["MNumber"]);
            MessadgeString = Row["MessadgeString"].ToString();
        }

        /// <summary>
        /// Набор сообщений по действиям по моделям SGSPointData
        /// </summary>
        /// <param name="SGSPointsData">Набор моделей SGSPointData для проверки на необходимость вставки обновления с последующим проведением операции</param>
        /// <returns></returns>
        public static List<Messadge> Put(IEnumerable<SGSPointData> SGSPointsData)
        {
            var res = new UniversalModel<Messadge>();
            var pdp = new SGSPDPut();
            pdp.AddRange(SGSPointsData);
            res.QParameters.Add(new SqlParameter("@SGSPDPut", pdp.ToXMLString()));
            return res.GetFromDBList("PutPointData");
        }

        public override string ToString()
        {
            return $@"{{
    ""MNumber"":{MNumber},
    ""MessadgeString"":""{MessadgeString}""
}}";
        }
    }
}
