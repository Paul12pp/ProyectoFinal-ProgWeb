using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.Models
{
    public class Gasto
    {
        [Key]
        public int IdGasto { get; set; }
        public DateTime Fecha { get; set; }
        public int IdConsumo { get; set; }
        public decimal Monto { get; set; }
        public int IdPago { get; set; }
        public string IdUsuario { get; set; }
        public string Observacion { get; set; }
    }
}
