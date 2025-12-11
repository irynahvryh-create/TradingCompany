using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingCompany.DTO
{
    public class Category
    {
        public int CategoryID { get; set; }
        [Required(ErrorMessage = "Name is required")]   // поле обов'язкове
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters")] // максимальна довжина
        public string Name { get; set; } = string.Empty;
        public bool Status { get; set; }   // true = активний, false = неактивний
    }
}
