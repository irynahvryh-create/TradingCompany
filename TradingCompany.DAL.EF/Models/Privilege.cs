using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TradingCompany.DAL.EF.Models;

public partial class Privilege
{
    [Key]
    [Column("PrivilegeID")]
    public int PrivilegeId { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column(TypeName = "datetime")] 
    public DateTime RowInsertTime { get; set; }

    [InverseProperty("Privilege")]
    public virtual ICollection<UserPrivilege> UserPrivileges { get; set; } = new List<UserPrivilege>();
}
