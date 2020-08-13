using ProyectoFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.Interface
{
    public interface IConsumo
    {
        int AddConsumo(Consumo model);
        int EditConsumo(int idconsumo, Consumo model);
        int DeleteConsumo(int idconsumo);
        IEnumerable<Consumo> GetConsumos(string sorter);
        Consumo GetConsumoById(int idconsumo);
    }
}
