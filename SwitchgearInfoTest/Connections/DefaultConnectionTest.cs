using NUnit.Framework;
using SwitchgearInfo.Connections;

namespace SwitchgearInfoTest.Connections
{

    class DefaultConnectionTest
    {
        [Test]
        [Category("Integration/Connection")]
        public void GetConnectionTest()
        {
            var conn = DefaultConnection.GetConnection;
            conn.Open();
            conn.Close();
            conn.Dispose();
        }

        [Test]
        [Category("Integration/Connection")]
        public void GetSettingsConnectionTest()
        {
            var conn = DefaultConnection.GetSettingsConnection;
            conn.Open();
            conn.Close();
            conn.Dispose();
        }
    }
}
