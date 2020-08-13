using ProyectoFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.Interface
{
    public interface IGasto
    {
        int AddGasto(Gasto model);
        int EditGasto(int idgasto, Gasto model);
        int DeleteGasto(int idgasto);
        IEnumerable<Gasto> GetGastos();
        IEnumerable<Gasto> GetGastosByConsumo(int idconsumo);
        IEnumerable<Gasto> GetGastosByPago(int idpago);
        IEnumerable<Gasto> GetGastosByFilter(string sorter);
    }
}
