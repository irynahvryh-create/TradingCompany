using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TradingCompany.DAL.EF.Models;

[Table("Product")]
public partial class Product
{
    [Key]
    [Column("ProductID")]
    public int ProductId { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("CategoryID")]
    public int CategoryId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal PriceIn { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal PriceOut { get; set; }

    [Column("ManufacturerID")]
    public int ManufacturerId { get; set; }

    public bool Status { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("Products")]
    public virtual Category Category { get; set; } = null!;

    [ForeignKey("ManufacturerId")]
    [InverseProperty("Products")]
    public virtual Manufacture Manufacturer { get; set; } = null!;

    [InverseProperty("Product")]
    public virtual ICollection<ProductLog> ProductLogs { get; set; } = new List<ProductLog>();
}
