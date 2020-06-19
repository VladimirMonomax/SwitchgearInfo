using NUnit.Framework;
using SwitchgearInfo.Models;
using System;
using System.Collections.Generic;

namespace SwitchgearInfoTest.Models
{
    class MessadgeTest
    {
        [Test]
        [Category("Integration/AllTime")]
        public void MPutTest_7messadge()
        {
            var ipd = new List<SGSPointData>();
            ipd.Add(new SGSPointData
            {
                IdSGSPoint = 4,
                PointValue = 5.112,
                DateOfValue = new DateTime(2020, 4, 1, 0, 7, 0, 0),
                Explantation = "added on unit test"
            });
            ipd.Add(new SGSPointData
            {
                IdSGSPoint = 4,
                PointValue = 5.045,
                DateOfValue = new DateTime(2020, 4, 1, 0, 8, 0, 0),
                Explantation = "added on unit test"
            });
            ipd.Add(new SGSPointData
            {
                IdSGSPoint = 4,
                PointValue = 5.021,
                DateOfValue = new DateTime(2020, 4, 1, 0, 9, 0, 0),
                Explantation = "added on unit test"
            });
            Assert.AreEqual(3, ipd.Count);
            var res = Messadge.Put(ipd);
            foreach(var m in res)
            {
                Console.WriteLine(m);
            }
            Assert.AreEqual(3, res.Count);
        }
    }
}
