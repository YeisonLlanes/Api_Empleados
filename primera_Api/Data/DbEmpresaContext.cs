using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using primera_Api.Models;

namespace primera_Api.Data;

public partial class DbEmpresaContext : DbContext
{
    public DbEmpresaContext()
    {
    }

    public DbEmpresaContext(DbContextOptions<DbEmpresaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cargos> Cargos { get; set; }

    public virtual DbSet<Departamentos> Departamentos { get; set; }

    public virtual DbSet<Empleados> Empleados { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cargos>(entity =>
        {
            entity.HasKey(e => e.IdCargo);

            entity.ToTable("cargos");

            entity.Property(e => e.IdCargo).HasColumnName("idCargo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdDepartamento).HasColumnName("idDepartamento");
            entity.Property(e => e.Salario)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("salario");

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Cargos)
                .HasForeignKey(d => d.IdDepartamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cargos_departamentos");
        });

        modelBuilder.Entity<Departamentos>(entity =>
        {
            entity.HasKey(e => e.IdDepartamento);

            entity.ToTable("departamentos");

            entity.Property(e => e.IdDepartamento).HasColumnName("idDepartamento");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Empleados>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado);

            entity.ToTable("empleados");

            entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");
            entity.Property(e => e.FechaIngreso)
                .HasColumnType("datetime")
                .HasColumnName("fechaIngreso");
            entity.Property(e => e.IdCargo).HasColumnName("idCargo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdCargoNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdCargo)
                .HasConstraintName("FK_empleados_cargos");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
