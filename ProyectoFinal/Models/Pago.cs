using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.Models
{
    public class Pago
    {
        [Key]
        public int IdPago { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public DateTime FechaCreacion { get; set; }
        public List<Gasto> Gastos { get; set; }
    }
}
