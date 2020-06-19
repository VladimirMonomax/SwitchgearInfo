using NUnit.Framework;
using SwitchgearInfo.Models;
using System;


namespace SwitchgearInfoTest.Models
{
    class SGSPointTest
    {
        [Test]
        [Category("Integration/AllTime")]
        public void SGSPGetAllTest()
        {
            var res = SGSPoint.GetAll;
            Assert.IsTrue(res.Count > 0);
            foreach (var m in res)
            {
                Console.WriteLine(m);
            }
        }
    }
}
