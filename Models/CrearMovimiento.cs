using System.ComponentModel.DataAnnotations;

namespace UniformProjectOmar.Models
{
    public class CrearMovimiento
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Empleado requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes elegir un Empleado")]
        public int IdEmpleado { get; set; }

        [Required(ErrorMessage = "Articulo requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes elegir un Articulo")]
        public int IdArticulo { get; set; }

        [Required(ErrorMessage = "Talla requerida")]
        public string Talla { get; set; } = string.Empty;

        [Required(ErrorMessage = "Catidad requerida")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes elegir una Cantidad mayor a cero")]
        public int Cantidad { get; set; }
    }
}
