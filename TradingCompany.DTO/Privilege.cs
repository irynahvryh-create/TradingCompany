using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingCompany.DTO
{
    public class Privilege
    {
        public int PrivilegeID { get; set; }
        public string Name { get; set; }
        public DateTime RowInsertTime { get; set; }

        public override string ToString()
        {
            return Name.ToString();
        }
    }
}
