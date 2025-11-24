using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TradingCompany.DAL.EF.Models;

namespace TradingCompany.DAL.EF.Data;

public partial class TradingCompanyContext : DbContext
{
    public TradingCompanyContext()
    {
    }

    public TradingCompanyContext(DbContextOptions<TradingCompanyContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Manufacture> Manufactures { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductLog> ProductLogs { get; set; }
    public virtual DbSet<Privilege> Privileges { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserPrivilege> UserPrivileges { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=TradingCompany_2;Integrated Security=True;TrustServerCertificate=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Privilege>(entity =>
        {
            entity.Property(e => e.PrivilegeId).ValueGeneratedNever();
        });

        modelBuilder.Entity<User>(entity =>
        {
            
            entity.Property(e => e.Password).IsFixedLength();
        });

        modelBuilder.Entity<UserPrivilege>(entity =>
        {
            entity.HasOne(d => d.Privilege).WithMany(p => p.UserPrivileges)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserPrivileges_Privileges");

            entity.HasOne(d => d.User).WithMany(p => p.UserPrivileges)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserPrivileges_Users");

            entity.Property(e => e.RowInsertTime).HasDefaultValueSql("GETDATE()");

        });

        modelBuilder.Entity<Manufacture>(entity =>
        {
            entity.HasKey(e => e.ManufacturerId).HasName("PK_Manufacturer");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Category");

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Manufacture");
        });

        modelBuilder.Entity<ProductLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__ProductL__9E2397E01BB4B9E5");

            entity.Property(e => e.Date).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductLog_Product");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
