
namespace esta1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    
    public  class usuarios
    {
        public int id_usuario { get; set; }

        [Required(ErrorMessage = "El proveedor es requerido")]
        public string nombres { get; set; }

        public string apellidos { get; set; }
        public string DUI { get; set; }
        public decimal Monto { get; set; }
        public string Detalle { get; set; }
        public int id_rol { get; set; }
    }
}
