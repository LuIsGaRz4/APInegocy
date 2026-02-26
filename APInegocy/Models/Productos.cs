using System.ComponentModel.DataAnnotations;

namespace APInegocy.Models
{
    public class Productos
    {
        [Key]
        public int IdProducto { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Activo { get; set; } = true;
    }
}
