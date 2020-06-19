using NUnit.Framework;
using SwitchgearInfo.Models.XML;
using System;

namespace SwitchgearInfoTest.Models.XML
{
    class OnePointDataTest
    {
        [Test]
        [Category("Unit")]
        public void OPDToXMLStringTest()
        {
            var exam = @"
<OnePointData xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Id>1</Id>
  <DateFrom>2020-06-04T20:15:00</DateFrom>
  <DateTo>2020-06-04T20:35:00</DateTo>
</OnePointData>";

            var point = new OnePointData {
                Id = 1,
                DateFrom = new DateTime(2020, 06, 4, 20, 15, 0),
                DateTo = new DateTime(2020, 06, 4, 20, 35, 0)
            };
            Console.WriteLine(point.ToXMLString());
            Assert.AreEqual(exam, point.ToXMLString());

            exam = @"
<OnePointData xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Id>1</Id>
  <DateFrom>2020-06-04T20:15:00</DateFrom>
</OnePointData>";
            point = new OnePointData
            {
                Id = 1,
                DateFrom = new DateTime(2020, 06, 4, 20, 15, 0)               
            };
            Console.WriteLine(point.ToXMLString());
            Assert.AreEqual(exam, point.ToXMLString());

            exam = @"
<OnePointData xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Id>1</Id>
</OnePointData>";
            point = new OnePointData
            {
                Id = 1
            };
            Console.WriteLine(point.ToXMLString());
            Assert.AreEqual(exam, point.ToXMLString());
        }
    }
}
