using NUnit.Framework;
using SwitchgearInfo.Models;
using SwitchgearInfo.Models.XML;
using System;
using System.Collections.Generic;
using System.Text;

namespace SwitchgearInfoTest.Models.XML
{
    [TestOf("SGSPDPut")]
    class SGSPDPutTest
    {
        [Test]
        [Category("Unit")]
        public void SGSPDPutToXMLStringTest()
        {
            var res = new SGSPDPut();
            res.Add(new SGSPointData {
                IdSGSPoint = 4,
                PointValue = 5.112,
                DateOfValue = new DateTime(2020, 4, 1, 0, 5, 0, 0),
                Explantation = "added on unit test"
            });
            res.Add(new SGSPointData {
                IdSGSPoint = 4,
                PointValue = 5.072,
                DateOfValue = new DateTime(2020, 4, 1, 0, 6, 0, 0),
                Explantation = "added on unit test"
            });
            res.Add(new SGSPointData {
                IdSGSPoint = 4,
                PointValue = 5.046,
                DateOfValue = new DateTime(2020, 4, 1, 0, 7, 0, 0),
                Explantation = "added on unit test"
            });
            Assert.AreEqual(3, res.DataPoints.Length);
            var exam = @"
<SGSPDPut xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <DataPoints>
    <SGSPointData>
      <Id>0</Id>
      <IdSGSPoint>4</IdSGSPoint>
      <DateOfValue>2020-04-01T00:05:00</DateOfValue>
      <PointValue>5.112</PointValue>
      <Explantation>added on unit test</Explantation>
    </SGSPointData>
    <SGSPointData>
      <Id>0</Id>
      <IdSGSPoint>4</IdSGSPoint>
      <DateOfValue>2020-04-01T00:06:00</DateOfValue>
      <PointValue>5.072</PointValue>
      <Explantation>added on unit test</Explantation>
    </SGSPointData>
    <SGSPointData>
      <Id>0</Id>
      <IdSGSPoint>4</IdSGSPoint>
      <DateOfValue>2020-04-01T00:07:00</DateOfValue>
      <PointValue>5.046</PointValue>
      <Explantation>added on unit test</Explantation>
    </SGSPointData>
  </DataPoints>
</SGSPDPut>";
            Console.WriteLine(res.ToXMLString());
            Assert.AreEqual(exam, res.ToXMLString());
            
        }
    }
}
