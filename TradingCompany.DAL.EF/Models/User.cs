using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TradingCompany.DAL.EF.Models;

public partial class User
{
    [Key]
    [Column("UserID")]
    public int UserId { get; set; }

    [StringLength(50)]
    public string Login { get; set; } = null!;

    [StringLength(50)]
    public string Email { get; set; } = null!;

    [MaxLength(50)]
    public byte[] Password { get; set; } = null!;

    public Guid Salt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime RowInsertTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime RowUpdateTime { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<UserPrivilege> UserPrivileges { get; set; } = new List<UserPrivilege>();
}
