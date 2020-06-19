using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SwitchgearInfo.Connections
{
    public class QueryExecutor
    {
        string _QueryStatenment;

        public QueryExecutor(string QueryStatenment)
        {
            _QueryStatenment = QueryStatenment;
        }

        public List<SqlParameter> Parameters { get; set; } = new List<SqlParameter>();

        public DataSet GetDataSet
        {
            get
            {
                DataSet res = new DataSet();
                using (SqlConnection conn = DefaultConnection.GetSettingsConnection)
                {
                    SqlCommand com = conn.CreateCommand();
                    conn.Open();
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = _QueryStatenment;
                    foreach(var p in Parameters)
                    {
                        com.Parameters.Add(p);
                    }
                    SqlDataAdapter SDA = new SqlDataAdapter(com);
                    SDA.Fill(res);

                }
                return res;
            }
        }
    }
}
