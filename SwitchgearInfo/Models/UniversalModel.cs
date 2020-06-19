using SwitchgearInfo.Connections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SwitchgearInfo.Models
{
    /// <summary>
    /// Универсальная модель для работы с данными модели
    /// </summary>
    /// <typeparam name="T">Тип модели наследованный от интерфейса IRow</typeparam>
    public class UniversalModel<T> where T : IRow, new()
    {
        /// <summary>
        /// Набор параметров для запросов
        /// </summary>
        public List<SqlParameter> QParameters { get; set; } = new List<SqlParameter>();

        /// <summary>
        /// Получает набор моделей соответсвующих условиям запроса
        /// </summary>
        /// <param name="Query">Условия запроса</param>
        /// <returns>набор моделей соответсвующих условиям запроса</returns>
        public List<T> GetFromDBList(string Query)
        {
            var res = new List<T>();
            var rows = new QueryExecutor(Query) {  Parameters = QParameters }.GetDataSet.Tables[0].Rows;
            foreach (DataRow row in rows)
            {
                var m = new T();
                m.FromRow(row);
                res.Add(m);
            }
            return res;

        }

        /// <summary>
        /// Получает из БД первую модель при наличии ответа, иначе возращает новую модель
        /// </summary>
        /// <param name="Query">Условия запроса</param>
        /// <returns>первую модель при наличии ответа, иначе возращает новую модель</returns>
        public T GetFromDBOneModel(string Query)
        {
            var res = new T();
            var rows = new QueryExecutor(Query) { Parameters = QParameters }.GetDataSet.Tables[0].Rows;
            if (rows.Count > 0)
            {
                var row = rows[0];
                res.FromRow(row);
            }
            return res;
        }
    }
}
