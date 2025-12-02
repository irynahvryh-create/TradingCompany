using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingCompany.DTO
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int ? CategoryID { get; set; }
        public decimal PriceIn { get; set; }
        public decimal PriceOut { get; set; }
        public int ManufacturerID { get; set; }
        public bool Status { get; set; }
    }
}
