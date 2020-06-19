using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SwitchgearInfo.Models
{
    public interface IRow
    {
        void FromRow(DataRow Row);
    }
}
