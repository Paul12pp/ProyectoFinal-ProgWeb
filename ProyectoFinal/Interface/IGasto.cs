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
        Gasto GetGastoById(int idgasto);
        IEnumerable<Gasto> GetGastos(string sorter);
        IQueryable<Gasto> GetGastosP(string sorter);
        IEnumerable<Gasto> GetGastosByConsumo(int idconsumo);
        IEnumerable<Gasto> GetGastosByPago(int idpago);
        IQueryable<Gasto> GetGastosByFilter(string sorter, int? id);
    }
}
