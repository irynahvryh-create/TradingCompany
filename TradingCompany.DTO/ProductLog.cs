

namespace TradingCompany.DTO
{
    public class ProductLog
    {
        public int LogID { get; set; }
        public int ProductID { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public bool Status { get; set; } // true = активний, false = неактивний
        public string? Comment { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
