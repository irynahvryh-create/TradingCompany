using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TradingCompany.DAL.EF.Models;

[PrimaryKey("UserId", "PrivilegeId")]
public partial class UserPrivilege
{
    [Key]
    [Column("UserID")]
    public int UserId { get; set; }

    [Key]
    [Column("PrivilegeID")]
    public int PrivilegeId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime RowInsertTime { get; set; }

    [ForeignKey("PrivilegeId")]
    [InverseProperty("UserPrivileges")]
    public virtual Privilege Privilege { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("UserPrivileges")]
    public virtual User User { get; set; } = null!;
}
