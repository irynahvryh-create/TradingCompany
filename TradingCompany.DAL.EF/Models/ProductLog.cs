using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TradingCompany.DAL.EF.Models;

[Table("ProductLog")]
public partial class ProductLog
{
    [Key]
    [Column("LogID")]
    public int LogId { get; set; }

    [Column("ProductID")]
    public int ProductId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal OldPrice { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal NewPrice { get; set; }

    public bool Status { get; set; }

    [StringLength(50)]
    public string? Comment { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Date { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("ProductLogs")]
    public virtual Product Product { get; set; } = null!;
}
