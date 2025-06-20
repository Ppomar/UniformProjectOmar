using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniformProjectOmar.Models;

public class TipoArticulo
{
    public int Id { get; set; } = 0;

    [Required]
    public string? Descripcion { get; set; }

    [Required]
    public string? Aplicacion { get; set; }

    public virtual ICollection<Articulo> Articulos { get; set; } = new List<Articulo>();
}
