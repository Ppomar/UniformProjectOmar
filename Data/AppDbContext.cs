using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using UniformProjectOmar.Models;

namespace UniformProjectOmar.Data;

public partial class AppDbContext : DbContext
{    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Articulo> Articulo { get; set; }

    public DbSet<TipoArticulo> TipoArticulo { get; set; }

    public DbSet<Movimiento> MovimientosEmpleado { get; set; }

    public virtual DbSet<Empleado> Empleado { get; set; }

    public virtual DbSet<Grupo> Grupo { get; set; }

    public virtual DbSet<TipoEmpleado> TipoEmpleado { get; set; }  

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Articulo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Articulo__3214EC0733D5064D");

            entity.ToTable("Articulo");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UnidadTalla)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.IdTipoArticuloNavigation).WithMany(p => p.Articulos)
                .HasForeignKey(d => d.IdTipoArticulo)
                .HasConstraintName("FK__Articulo__IdTipo__32AB8735");
        });

        modelBuilder.Entity<TipoArticulo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TipoArti__3214EC07177BC656");

            entity.ToTable("TipoArticulo");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Aplicacion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
        });             

        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.HasNoKey(); // Es una vista
            entity.ToView("vw_MovimientosEmpleado");
        });

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

        base.OnModelCreating(modelBuilder);

        OnModelCreatingPartial(modelBuilder);
    }
    
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public async Task RecordDeliveryAsync(int idEmpleado, int idArticulo, string talla, int cantidad)
    {
        var parameters = new[]
        {
        new SqlParameter("@Id_Empleado", idEmpleado),
        new SqlParameter("@Id_Articulo", idArticulo),
        new SqlParameter("@Talla", talla),
        new SqlParameter("@Cantidad", cantidad),
    };

        await Database.ExecuteSqlRawAsync("EXEC sp_RegistrarEntrega @Id_Empleado, @Id_Articulo, @Talla, @Cantidad", parameters);
    }
}
