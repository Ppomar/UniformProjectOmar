using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniformProjectOmar.Models;

public partial class TipoEmpleado
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Descripción requerida")]
    public string? Tipo { get; set; }

    public virtual ICollection<Grupo> Grupo { get; set; } = new List<Grupo>();
}
