using NUnit.Framework;
using SwitchgearInfo.Models.XML;
using System;
using System.Collections.Generic;
using System.Text;

namespace SwitchgearInfoTest.Models.XML
{
    class SettingsTest
    {
        [Test]
        [Category("Integration/Settings")]
        public void SSettingsFromXMLTest()
        {
            var exam = @"<?xml version=""1.0"" encoding=""utf-16""?>
<Settings xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Connection>
    <SQLConnection TypeSQL=""MSSQL"" Server=""MICROSOFT-PC\SQLEXPRESS"" Catalog=""SwitchgearTemperatureBD"" User=""ID=SiteMaster;Password=Qwerty1234"" />
  </Connection>
</Settings>";
            var res = Settings.SettingsFromXML;
            Console.WriteLine(res.ToString());
            
            Assert.AreEqual(exam, res.ToString());
        }
    }
}
