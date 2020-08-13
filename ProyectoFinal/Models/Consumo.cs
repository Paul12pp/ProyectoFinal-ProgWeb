using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.Models
{
    public class Consumo
    {
        [Key]
        public int IdConsumo { get; set; }
        [Required]
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
        [Required]
        [DisplayName("Url de Imagen")]
        public string Imagen { get; set; }
        public DateTime FechaCreacion { get; set; }
        public List<Gasto> Gastos { get; set; }
    }
}
