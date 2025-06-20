using System;
using System.Collections.Generic;

namespace UniformProjectOmar.Models;

public partial class Movimiento
{
    public int Id { get; set; }

    public int? IdArticulo { get; set; }

    public string? Talla { get; set; }

    public DateTime? Fecha { get; set; }

    public string? TipoMovimiento { get; set; }

    public int? Cantidad { get; set; }

    public string? Motivo { get; set; }

    public int? IdEmpleado { get; set; }
}
