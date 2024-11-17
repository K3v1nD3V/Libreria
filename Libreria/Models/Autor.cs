using System;
using System.Collections.Generic;

namespace Libreria.Models;

public partial class Autor
{
    public int IdAutor { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Nacionalidad { get; set; } = null!;

    public virtual ICollection<LibrosAutor> LibrosAutors { get; set; } = new List<LibrosAutor>();
}
