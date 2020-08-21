using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.Models
{
    public class SearchViewModel
    {
        [Required]
        public DateTime Desde { get; set; }
        [Required]
        public DateTime Hasta { get; set; }
        [Required]
        public int IdConsumo { get; set; }
        [Required]
        public int IdPago { get; set; }
    }
}
