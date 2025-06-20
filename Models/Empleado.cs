using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniformProjectOmar.Models;

public partial class Empleado
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Nombre completo requerido")]
    public string? NombreEmpleado { get; set; }

    [ForeignKey("Grupo")]
    [Required(ErrorMessage = "Grupo requerido")]
    public string? IdGrupo { get; set; }

    public virtual Grupo? Grupo { get; set; }
}
