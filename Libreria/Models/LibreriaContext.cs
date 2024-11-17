using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Libreria.Models;

public partial class LibreriaContext : DbContext
{
    public LibreriaContext()
    {
    }

    public LibreriaContext(DbContextOptions<LibreriaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autor> Autors { get; set; }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Editoriale> Editoriales { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<LibrosAutor> LibrosAutors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=MSI\\SQLEXPRESS;Initial Catalog=Libreria;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autor>(entity =>
        {
            entity.HasKey(e => e.IdAutor).HasName("PK__Autor__DD33B031CD0C61B7");

            entity.ToTable("Autor");

            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nacionalidad)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.CodigoCategoria).HasName("PK__Categori__738F04ADE313E401");

            entity.Property(e => e.CodigoCategoria).HasColumnName("Codigo_categoria");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Editoriale>(entity =>
        {
            entity.HasKey(e => e.Nit).HasName("PK__Editoria__C7D1D6DB5E6ADDA4");

            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Sitioweb)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.Isbn).HasName("PK__Libros__9271CED1E65D58E4");

            entity.Property(e => e.CodigoCategoria).HasColumnName("Codigo_categoria");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FechaRegistro).HasColumnName("Fecha_registro");
            entity.Property(e => e.NitEditorial).HasColumnName("Nit_editorial");
            entity.Property(e => e.NombreAutor)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Nombre_autor");
            entity.Property(e => e.Titulo)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.CodigoCategoriaNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.CodigoCategoria)
                .HasConstraintName("FK__Libros__Codigo_c__4D94879B");

            entity.HasOne(d => d.NitEditorialNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.NitEditorial)
                .HasConstraintName("FK__Libros__Nit_edit__4E88ABD4");
        });

        modelBuilder.Entity<LibrosAutor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LibrosAu__3214EC0744D7A11B");

            entity.ToTable("LibrosAutor");

            entity.HasOne(d => d.IdAutorNavigation).WithMany(p => p.LibrosAutors)
                .HasForeignKey(d => d.IdAutor)
                .HasConstraintName("FK__LibrosAut__IdAut__571DF1D5");

            entity.HasOne(d => d.IdLibroNavigation).WithMany(p => p.LibrosAutors)
                .HasForeignKey(d => d.IdLibro)
                .HasConstraintName("FK__LibrosAut__IdLib__5629CD9C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
