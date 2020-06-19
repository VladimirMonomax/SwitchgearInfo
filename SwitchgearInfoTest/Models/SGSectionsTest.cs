using NUnit.Framework;
using SwitchgearInfo.Models;
using System;

namespace SwitchgearInfoTest.Models
{
    class SGSectionsTest
    {
        [Test]
        [Category("Integration/AllTime")]
        public void SGSGetAllTest()
        {
            var res = SGSection.GetAll;
            Assert.IsTrue(res.Count > 0);
            foreach(var m in res)
            {
                Console.WriteLine(m);
            }
        }

        [Test]
        [Category("Integration/AllTime")]
        public void SGSGetByIdTest_id_1()
        {
            var res = SGSection.GetById(1);
            Assert.AreEqual(1, res.id);

            Console.WriteLine(res);

        }
    }
}