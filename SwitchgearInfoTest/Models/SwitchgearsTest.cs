using NUnit.Framework;
using SwitchgearInfo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SwitchgearInfoTest.Models
{
    class SwitchgearsTest
    {
        [Test]
        [Category("Integration/AllTime")]
        public void SWGetAllTest()
        {
            var res = Switchgear.GetAll;
            Assert.IsTrue(res.Count > 0);
            foreach(var m in res)
            {
                Console.WriteLine(m);
            }
        }

        [Test]
        [Category("Integration/AllTime")]
        public void SWGetByIdTest_id_1()
        {
            var contId = 1;
            var res = Switchgear.GetById(contId);
            Assert.AreEqual(contId, res.id);
            Console.WriteLine(res);
        }

        [Test]
        [Category("Integration/AllTime")]
        public void SWSectionTest_sgid_1()
        {            
            var contId = 1;
            var res = Switchgear.GetById(contId);
            Assert.IsTrue(res.Sections.Count > 0);
            foreach(var m in res.Sections)
            {
                Console.WriteLine(m);
            }
        }
    }
}
