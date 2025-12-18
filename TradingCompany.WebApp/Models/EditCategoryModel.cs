using System.ComponentModel.DataAnnotations;

namespace TradingCompany.WebApp.Models
{
    public class EditCategoryModel
    {
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Назва обовʼязкова")]
        [StringLength(100)]
        public string Name { get; set; }

        public bool Status { get; set; }
    }
}
