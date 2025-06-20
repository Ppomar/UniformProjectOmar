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

    public virtual DbSet<Empleado> Empleado { get; set; }

    public virtual DbSet<Grupo> Grupo { get; set; }

    public virtual DbSet<TipoEmpleado> TipoEmpleado { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Empleado__3214EC0796C5D086");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.IdGrupo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.NombreEmpleado)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Grupo).WithMany(p => p.Empleado)
                .HasForeignKey(d => d.IdGrupo)
                .HasConstraintName("FK__Empleado__IdGrup__2DE6D218");
        });

        modelBuilder.Entity<Grupo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Grupo__3214EC07CABEA5CF");

            entity.Property(e => e.Id)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Grupo1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Grupo");

            entity.HasOne(d => d.IdTipoEmpleadoNavigation).WithMany(p => p.Grupo)
                .HasForeignKey(d => d.IdTipoEmpleado)
                .HasConstraintName("FK__Grupo__IdTipoEmp__2B0A656D");
        });

        modelBuilder.Entity<TipoEmpleado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TipoEmpl__3214EC07080EED38");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
