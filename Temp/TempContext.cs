using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using UniformProjectOmar.Models;

namespace UniformProjectOmar.Temp;

public partial class TempContext : DbContext
{
    public TempContext(DbContextOptions<TempContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Movimiento> Movimiento { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Movimien__3214EC0760092683");

            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Motivo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Talla)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TipoMovimiento)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
