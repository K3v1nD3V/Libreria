using System;
using System.Collections.Generic;

namespace Libreria.Models;

public partial class LibrosAutor
{
    public int Id { get; set; }

    public int? IdLibro { get; set; }

    public int? IdAutor { get; set; }

    public virtual Autor? IdAutorNavigation { get; set; }

    public virtual Libro? IdLibroNavigation { get; set; }
}
