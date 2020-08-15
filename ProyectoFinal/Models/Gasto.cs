using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.Models
{
    public class Gasto
    {
        [Key]
        public int IdGasto { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        [DisplayName("Tipo de Consumo")]
        public int IdConsumo { get; set; }
        [Required]
        [Range(1,99999)]
        public decimal Monto { get; set; }
        [Required]
        [DisplayName("Tipo de Pago")]
        public int IdPago { get; set; }
        public string IdUsuario { get; set; }
        [DisplayName("Observación")]
        public string Observacion { get; set; }
    }
}
