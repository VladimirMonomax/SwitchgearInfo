using NUnit.Framework;
using SwitchgearInfo.Models;
using SwitchgearInfo.Models.XML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwitchgearInfoTest.Models
{
    class SGSPointDataTest
    {
        [Test]
        [Category("Integration/AllTime")]
        public void SGSPDGetPointDataTest_pid_1()
        {
            var res = SGSPointData.GetPointData(1);
            Assert.IsTrue(res.Count > 0);
            foreach (var m in res)
            {
                Console.WriteLine(m);
            }
        }

        [Test]
        [Category("Integration/AllTime")]
        public void SGSPDGetPointDataTest_pid_1_DT_2020_04_01_07_39_0_0()
        {
            var contDT = new DateTime(2020, 4, 1, 7, 39, 0, 0);            
            var res = SGSPointData.GetPointData(1,contDT,contDT);
            Assert.AreEqual(1, res.Count);
            foreach (var m in res)
            {
                Console.WriteLine(m);
            }
        }

        [Test]
        [Category("Integration/AllTime")]
        public void SGSPDGetPointDataTest_pid_1_4()
        {
            var contDT = new List<long> { 1, 4 };
            var res = SGSPointData.GetPointData(contDT);
            Assert.IsTrue(res.Count>0);
            foreach (var m in res)
            {
                Console.WriteLine(m);
            }
        }

        [Test]
        [Category("Integration/AllTime")]
        public void SGSPDPutTest_7_mess()
        {
            var res = new List<SGSPointData>();
            res.Add(new SGSPointData {
                IdSGSPoint = 1,
                PointValue = 226.782165527343,
                DateOfValue = new DateTime(2020, 4, 1, 0, 0, 0),
                Explantation ="added on unit test"
            });
            res.Add(new SGSPointData
            {
                IdSGSPoint = 1,
                PointValue = 213.99836730957,
                DateOfValue = new DateTime(2020, 4, 1, 0, 1, 0),
                Explantation = "added on unit test"
            });
            res.Add(new SGSPointData
            {
                IdSGSPoint = 1,
                PointValue = 214.845962524413,
                DateOfValue = new DateTime(2020, 4, 1, 0, 2, 0),
                Explantation = "added on unit test"
            });
            res.Add(new SGSPointData
            {
                IdSGSPoint = 1,
                PointValue = 220.641265869141,
                DateOfValue = new DateTime(2020, 4, 1, 0, 9, 0),
                Explantation = "added on unit test"
            });
            res.Add(new SGSPointData
            {
                IdSGSPoint = 1,
                PointValue = 219.261199951172,
                DateOfValue = new DateTime(2020, 4, 1, 0, 12, 0),
                Explantation = "added on unit test"
            });
            res.Add(new SGSPointData
            {
                IdSGSPoint = 1,
                PointValue = 215.261199951172,
                DateOfValue = new DateTime(2020, 4, 1, 0, 14, 0),
                Explantation = "added on unit test"
            });
            res.Add(new SGSPointData
            {
                IdSGSPoint = 1,
                PointValue = 210.261199951172,
                DateOfValue = new DateTime(2020, 4, 1, 0, 15, 0),
                Explantation = "added on unit test"
            });
            var resm = SGSPointData.Put(res);
            Assert.AreEqual(7, res.Count);
            foreach(var m in resm)
            {
                Console.WriteLine($"{{\"MNumber\":{m.MNumber}, \"MessadgeString\": \"{m.MessadgeString}\"");
            }
        }

        [Test]
        [Category("Integration/AllTime")]
        public void SGSPDPutTest_7_messTroubleShot()
        {
            var res = new List<SGSPointData>();
            var idp = 5;
            res.Add(new SGSPointData
            {
                IdSGSPoint = idp,
                PointValue = 11.0920066833496,
                DateOfValue = new DateTime(2020, 4, 1, 0, 0, 0),
                Explantation = "added on unit test"
            });
            res.Add(new SGSPointData
            {
                IdSGSPoint = idp,
                PointValue = 9.23535823822021,
                DateOfValue = new DateTime(2020, 4, 1, 0, 1, 0),
                Explantation = "added on unit test"
            });
            res.Add(new SGSPointData
            {
                IdSGSPoint = idp,
                PointValue = 10.4018611907959,
                DateOfValue = new DateTime(2020, 4, 1, 0, 2, 0),
                Explantation = "added on unit test"
            });
            res.Add(new SGSPointData
            {
                IdSGSPoint = idp,
                PointValue = 11.5134706497192,
                DateOfValue = new DateTime(2020, 4, 1, 0, 3, 0),
                Explantation = "added on unit test"
            });
            res.Add(new SGSPointData
            {
                IdSGSPoint = idp,
                PointValue = 10.34092617,
                DateOfValue = new DateTime(2020, 4, 1, 0, 4, 0),
                Explantation = "added on unit test"
            });
            res.Add(new SGSPointData
            {
                IdSGSPoint = idp,
                PointValue = 10.64092617,
                DateOfValue = new DateTime(2020, 4, 1, 0, 5, 0),
                Explantation = "added on unit test"
            });
            res.Add(new SGSPointData
            {
                IdSGSPoint = idp,
                PointValue = 10.85793114,
                DateOfValue = new DateTime(2020, 4, 1, 0, 6, 0),
                Explantation = "added on unit test"
            });
            var resm = SGSPointData.Put(res);
            Assert.AreEqual(7, res.Count);
            var point = SGSPoint.GetAll.Where(m => m.Id == idp).FirstOrDefault();
            Assert.AreEqual(res[6].DateOfValue, point.LastDTSafeValue);
            foreach (var m in resm)
            {
                Console.WriteLine($"{{\"MNumber\":{m.MNumber}, \"MessadgeString\": \"{m.MessadgeString}\"");
            }
        }

        [Test]
        [Category("Integration/AllTime")]
        public void SGSPDGetPointDataTest_pid_1Z()
        {
            var opd = new OnePointData { Id = 1 };
            var res = SGSPointData.GetPointData(opd);
            Assert.IsTrue(res.Count > 0);
            foreach (var m in res)
            {
                Console.WriteLine(m);
            }
            
        }

        [Test]
        [Category("Integration/AllTime")]
        public void SGSPDGetPointDataTest_pid_1_DT_2020_04_01_07_39_0_0_Z()
        {
            var contDT = new DateTime(2020, 4, 1, 7, 39, 0, 0);
            var opd = new OnePointData { Id = 1, DateFrom = contDT, DateTo = contDT };
            var res = SGSPointData.GetPointData(opd);
            Assert.AreEqual(1, res.Count);
            foreach (var m in res)
            {
                Console.WriteLine(m);
            }            
        }

        [Test]
        [Category("Integration/AllTime")]
        public void SGSPDGetPointDataTest_pid_1_4_Z()
        {
            var contDT = new List<long> { 1, 4 };
            var mpd = new ManyPointsData { PointsId = contDT };
            var res = SGSPointData.GetPointData(contDT);
            Assert.IsTrue(res.Count > 0);
            foreach (var m in res)
            {
                Console.WriteLine(m);
            }
            
        }

        [Test]
        [Category("Integration/AllTime")]
        public void SGSPDGetPointDataTest_LastData()
        {
            var res = SGSPointData.GetPointData();
            var control = SGSPoint.GetAll.Count;
            foreach (var m in res)
            {
                Console.WriteLine(m);
            }
            Assert.IsTrue(res.Count <= control);            
           
        }

        [Test]
        [Category("Integration/AllTime")]
        public void SGSPDGetPointDataQTest()
        {
            var res = SGSPointData.GetPointDataQ();
            var control = SGSPoint.GetAll.Count;
            foreach (var m in res)
            {
                Console.WriteLine(m);
            }
            Assert.IsTrue(res.Count <= control);

        }
    }
}
