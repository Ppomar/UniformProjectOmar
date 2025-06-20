namespace UniformProjectOmar.Models
{
    public class Movimiento
    {
        public DateTime Fecha { get; set; }
        public string TipoMovimiento { get; set; } = default!;
        public int Cantidad { get; set; }
        public string Talla { get; set; } = default!;
        public string Articulo { get; set; } = default!;
        public string NombreEmpleado { get; set; } = default!;
        public string Grupo { get; set; } = default!;
        public string TipoEmpleado { get; set; } = default!;
    }
}
