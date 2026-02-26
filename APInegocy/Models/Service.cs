using System.ComponentModel.DataAnnotations;

namespace APInegocy.Models
{
    public class Service
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Dispositivo { get; set; }
        public string? Descripcion { get; set; }
        public string? Phone { get; set; }
        public string? Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Activo { get; set; } = true;
    }
}
