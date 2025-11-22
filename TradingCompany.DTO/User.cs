using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingCompany.DTO
{
    public class User
    {
        public int UserID { get; set; }
        public string Login { get; set; }  
        public string Email { get; set;}
        public DateTime RowInsertTime { get; set; }
        public DateTime RowUpdateTime { get; set; }

        public List<Privilege> Privileges { get; set; }

        public override string ToString()
        {
            return $"UserID: {UserID}, Login: {Login}, Email: {Email}, RowInsertTime: {RowInsertTime}, RowUpdateTime: {RowUpdateTime}";
        }

    }
}
