using NUnit.Framework;
using SwitchgearInfo.Connections;
using SwitchgearInfo.Models.XML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace SwitchgearInfoTest.Connections
{
    class QueryExecutorTest
    {
        [Test]
        [Category("Integration/AllTime")]
        public void GetDataSetTest_SGSectionsGetByIDSG()
        {
            var res = new QueryExecutor("SGSectionsGetByIDSG");
            res.Parameters.Add(new SqlParameter("@IDSG", 1));
            var ds = res.GetDataSet;
            foreach(DataRow row in ds.Tables[0].Rows)
            {
                foreach(DataColumn cn in row.Table.Columns)
                {
                    Console.Write($"{cn.ColumnName}:{row[cn]}| ");
                }
                Console.WriteLine();
                Console.WriteLine("________________________________________________________________________________________________________");
            }
            Assert.AreEqual(1, ds.Tables[0].Rows.Count);
        }

        [Test]
        [Category("Integration/AllTime")]
        public void GetDataSetTest_SGSectionGetById()
        {
            var res = new QueryExecutor("SGSectionGetById");
            res.Parameters.Add(new SqlParameter("@SGSId", 1));
            var ds = res.GetDataSet;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                foreach (DataColumn cn in row.Table.Columns)
                {
                    Console.Write($"{cn.ColumnName}:{row[cn]}| ");
                }
                Console.WriteLine();
                Console.WriteLine("________________________________________________________________________________________________________");
            }
            Assert.AreEqual(1, ds.Tables[0].Rows.Count);

        }

        [Test]
        [Category("Integration/AllTime")]
        public void GetDataSetTest_GetPointData_onePoint_pid1()
        {
            var res = new QueryExecutor("GetPointData");
            var opd = new OnePointData { Id = 1 };
            res.Parameters.Add(new SqlParameter("@Point", opd.ToXMLString()));
            var ds = res.GetDataSet;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                foreach (DataColumn cn in row.Table.Columns)
                {
                    Console.Write($"{cn.ColumnName}:{row[cn]}| ");
                }
                Console.WriteLine();
                Console.WriteLine("________________________________________________________________________________________________________");
            }
            Assert.AreEqual(670, ds.Tables[0].Rows.Count);

        }

        [Test]
        [Category("Integration/AllTime")]
        public void GetDataSetTest_GetPointData_onePoint_pid_1_DT_2020_04_01_07_39_0_0()
        {
            var res = new QueryExecutor("GetPointData");
            var opd = new OnePointData { Id = 1, DateFrom = new DateTime(2020, 4, 1, 7, 39, 0, 0),  DateTo= new DateTime(2020, 4, 1, 7, 39, 0, 0) };
            res.Parameters.Add(new SqlParameter("@Point", opd.ToXMLString()));
            var ds = res.GetDataSet;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                foreach (DataColumn cn in row.Table.Columns)
                {
                    Console.Write($"{cn.ColumnName}:{row[cn]}| ");
                }
                Console.WriteLine();
                Console.WriteLine("________________________________________________________________________________________________________");
            }
            Assert.AreEqual(1, ds.Tables[0].Rows.Count);

        }


        [Test]
        [Category("Integration/AllTime")]
        public void GetDataSetTest_GetPointData_manyPoint_pid_1_4_DT_2020_04_01_07_39_0_0()
        {
            var res = new QueryExecutor("GetPointData");
            var pids = new List<long>() { 1, 4 };
            var mpd = new ManyPointsData { PointsId = pids, DateFrom = new DateTime(2020, 4, 1, 7, 39, 0, 0), DateTo = new DateTime(2020, 4, 1, 7, 39, 0, 0) };
            res.Parameters.Add(new SqlParameter("@Points", mpd.ToXMLString()));
            var ds = res.GetDataSet;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                foreach (DataColumn cn in row.Table.Columns)
                {
                    Console.Write($"{cn.ColumnName}:{row[cn]}| ");
                }
                Console.WriteLine();
                Console.WriteLine("________________________________________________________________________________________________________");
            }
            Assert.AreEqual(1, ds.Tables[0].Rows.Count);

        }

    }
}