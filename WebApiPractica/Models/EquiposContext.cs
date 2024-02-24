using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApiPractica.Models;

public partial class EquiposContext : DbContext
{
    public EquiposContext()
    {
    }

    public EquiposContext(DbContextOptions<EquiposContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Carrera> Carreras { get; set; }

    public virtual DbSet<Equipo> Equipos { get; set; }

    public virtual DbSet<EstadosEquipo> EstadosEquipos { get; set; }

    public virtual DbSet<EstadosReserva> EstadosReservas { get; set; }

    public virtual DbSet<Facultade> Facultades { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<TipoEquipo> TipoEquipos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Carrera>(entity =>
        {
            entity.HasKey(e => e.CarreraId).HasName("PK__carreras__1F1EC700DF006F1C");

            entity.ToTable("carreras");

            entity.Property(e => e.CarreraId).HasColumnName("carrera_id");
            entity.Property(e => e.FacultadId).HasColumnName("facultad_id");
            entity.Property(e => e.NombreCarrera)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nombre_carrera");
        });

        modelBuilder.Entity<Equipo>(entity =>
        {
            entity.HasKey(e => e.IdEquipos);

            entity.ToTable("equipos");

            entity.Property(e => e.IdEquipos).HasColumnName("id_equipos");
            entity.Property(e => e.AnioCompra).HasColumnName("anio_compra");
            entity.Property(e => e.Costo)
                .HasColumnType("numeric(18, 4)")
                .HasColumnName("costo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.EstadoEquipoId).HasColumnName("estado_equipo_id");
            entity.Property(e => e.MarcaId).HasColumnName("marca_id");
            entity.Property(e => e.Modelo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("modelo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.TipoEquipoId).HasColumnName("tipo_equipo_id");
            entity.Property(e => e.VidaUtil).HasColumnName("vida_util");
        });

        modelBuilder.Entity<EstadosEquipo>(entity =>
        {
            entity.HasKey(e => e.IdEstadosEquipo).HasName("PK__estados___BE0FBCF9D919A2F4");

            entity.ToTable("estados_equipo");

            entity.Property(e => e.IdEstadosEquipo).HasColumnName("id_estados_equipo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("estado");
        });

        modelBuilder.Entity<EstadosReserva>(entity =>
        {
            entity.HasKey(e => e.EstadoResId).HasName("PK__estados___5E7C248CB1E10FCF");

            entity.ToTable("estados_reserva");

            entity.Property(e => e.EstadoResId).HasColumnName("estado_res_id");
            entity.Property(e => e.Estado)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("estado");
        });

        modelBuilder.Entity<Facultade>(entity =>
        {
            entity.HasKey(e => e.FacultadId).HasName("PK__facultad__6407F1AE600088A0");

            entity.ToTable("facultades");

            entity.Property(e => e.FacultadId).HasColumnName("facultad_id");
            entity.Property(e => e.NombreFacultad)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nombre_facultad");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.IdMarcas).HasName("PK__marcas__7918CF24CB5AEFC0");

            entity.ToTable("marcas");

            entity.Property(e => e.IdMarcas).HasColumnName("id_marcas");
            entity.Property(e => e.Estados)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("estados");
            entity.Property(e => e.NombreMarca)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_marca");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.ReservaId).HasName("PK__reservas__F1437E4867774DE6");

            entity.ToTable("reservas");

            entity.Property(e => e.ReservaId).HasColumnName("reserva_id");
            entity.Property(e => e.EquipoId).HasColumnName("equipo_id");
            entity.Property(e => e.EstadoReservaId).HasColumnName("estado_reserva_id");
            entity.Property(e => e.FechaRetorno)
                .HasColumnType("datetime")
                .HasColumnName("fecha_retorno");
            entity.Property(e => e.FechaSalida)
                .HasColumnType("datetime")
                .HasColumnName("fecha_salida");
            entity.Property(e => e.HoraRetorno)
                .HasColumnType("datetime")
                .HasColumnName("hora_retorno");
            entity.Property(e => e.HoraSalida)
                .HasColumnType("datetime")
                .HasColumnName("hora_salida");
            entity.Property(e => e.TiempoReserva).HasColumnName("tiempo_reserva");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");
        });

        modelBuilder.Entity<TipoEquipo>(entity =>
        {
            entity.HasKey(e => e.IdTipoEquipo).HasName("PK__tipo_equ__82617B326DB7EDEE");

            entity.ToTable("tipo_equipo");

            entity.Property(e => e.IdTipoEquipo).HasColumnName("id_tipo_equipo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("estado");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__usuarios__2ED7D2AFE4355549");

            entity.ToTable("usuarios");

            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");
            entity.Property(e => e.Carnet)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("carnet");
            entity.Property(e => e.CarreraId).HasColumnName("carrera_id");
            entity.Property(e => e.Documento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("documento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Tipo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("tipo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
