using Microsoft.EntityFrameworkCore;
using SwitchgearInfo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwitchgearInfo.Connections
{
    public class DefaultContext : DbContext
    {
        public DefaultContext() : base(DefaultConnection.DefaultDBOption)
        {

        }     
    }

    public class DefaultContext<T> : DefaultContext where T : ModelPropertyChanges
    {
        public DbSet<T> Values { get; set; }
    }

    public class JoinContext : DefaultContext
    {
        public DbSet<Switchgear> Switchgears { get; set; }
        public DbSet<SGSection> Sections { get; set; }
        public DbSet<SGSPoint> Points { get; set; }
        public DbSet<SGSPointData> PointsData { get; set; }
    }

}
