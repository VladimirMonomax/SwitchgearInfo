using NUnit.Framework;
using SwitchgearInfo.Models.XML;
using System;
using System.Collections.Generic;

namespace SwitchgearInfoTest.Models.XML
{
    [TestOf("ManyPointsData")]
    class ManyPointsDataTest
    {
        [Test]
        [Category("Unit")]
        public void MPDToXMLStringTest()
        {
            var exam = @"
<ManyPointsData xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <DateFrom>2020-06-04T20:35:00</DateFrom>
  <DateTo>2020-06-04T21:00:00</DateTo>
  <PointsId>1</PointsId>
  <PointsId>4</PointsId>
  <PointsId>5</PointsId>
</ManyPointsData>";
            var pids = new List<long>();
            pids.Add(1);
            pids.Add(4);
            pids.Add(5);
            var res = new ManyPointsData {
                PointsId = pids,
                DateFrom = new DateTime(2020, 06, 4, 20, 35, 0),
                DateTo = new DateTime(2020, 06, 4, 21, 00, 0)
            };           
            Console.WriteLine(res.ToXMLString());
            Assert.AreEqual(exam, res.ToXMLString());

            exam = @"
<ManyPointsData xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <DateFrom>2020-06-04T20:35:00</DateFrom>
  <PointsId>1</PointsId>
  <PointsId>4</PointsId>
  <PointsId>5</PointsId>
</ManyPointsData>";
            res = new ManyPointsData
            {
                PointsId = pids,
                DateFrom = new DateTime(2020, 06, 4, 20, 35, 0)
            };
            Console.WriteLine(res.ToXMLString());
            Assert.AreEqual(exam, res.ToXMLString());

            exam = @"
<ManyPointsData xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <PointsId>1</PointsId>
  <PointsId>4</PointsId>
  <PointsId>5</PointsId>
</ManyPointsData>";
            res = new ManyPointsData
            {
                PointsId = pids
            };
            Console.WriteLine(res.ToXMLString());
            Assert.AreEqual(exam, res.ToXMLString());
        }
    }
}
