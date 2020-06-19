using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using SwitchgearInfo.Properties;
using Microsoft.EntityFrameworkCore;
using SwitchgearInfo.Models.XML;

namespace SwitchgearInfo.Connections
{
    public static class DefaultConnection
    {
        static string ConnectionString
        {
            get
            {
                return $"Data Source={Resources.DefaultServer};Initial Catalog={Resources.DefaultCatalog};Persist Security Info=True;User {Resources.DefaultUser}";
            }
        }

        public static SqlConnection GetConnection
        {
            get
            {
                return new SqlConnection(ConnectionString);
            }
        }

        public static SqlConnection GetSettingsConnection
        {
            get
            {
                var res = Settings.SettingsFromXML.Connection[0];

                return new SqlConnection(
                    $"Data Source={res.Server};Initial Catalog={res.Catalog};Persist Security Info=True;User {res.User}"
                    );
            }
        }

        public static DbContextOptions DefaultDBOption
            {
            get
            {
                var res = new DbContextOptionsBuilder();
                res.UseSqlServer(GetConnection);
                return res.Options;
            }
        }
    }
}
