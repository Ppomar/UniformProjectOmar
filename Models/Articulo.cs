using System;
using System.Collections.Generic;

namespace UniformProjectOmar.Models;

public class Articulo
{
    public int Id { get; set; }

    
    public string? Descripcion { get; set; }

    public int? IdTipoArticulo { get; set; }

    public string? UnidadTalla { get; set; }

    public virtual TipoArticulo? IdTipoArticuloNavigation { get; set; }
}
