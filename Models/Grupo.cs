using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniformProjectOmar.Models;

public partial class Grupo
{
    [Required]
    [MaxLength(2, ErrorMessage = "Máximo 2 caracteres permitidos.")]   
    public string Id { get; set; } = string.Empty;

    [Required]
    public string? Grupo1 { get; set; }

    [Required]
    public int? IdTipoEmpleado { get; set; }

    public virtual ICollection<Empleado> Empleado { get; set; } = new List<Empleado>();

    public virtual TipoEmpleado? IdTipoEmpleadoNavigation { get; set; }
}
