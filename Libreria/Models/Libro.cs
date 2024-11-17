using System;
using System.Collections.Generic;

namespace Libreria.Models;

public partial class Libro
{
    public int Isbn { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? NombreAutor { get; set; }

    public DateOnly? Publicacion { get; set; }

    public DateOnly? FechaRegistro { get; set; }

    public int? CodigoCategoria { get; set; }

    public int? NitEditorial { get; set; }

    public virtual Categoria? CodigoCategoriaNavigation { get; set; }

    public virtual ICollection<LibrosAutor> LibrosAutors { get; set; } = new List<LibrosAutor>();

    public virtual Editoriale? NitEditorialNavigation { get; set; }
}
