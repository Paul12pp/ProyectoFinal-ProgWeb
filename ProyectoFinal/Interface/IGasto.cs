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
        Task<IEnumerable<Gasto>> GetGastos(string sorter);
        IEnumerable<Gasto> GetGastos(string iduser, bool role);
        Task<IQueryable<Gasto>> GetGastosP(string sorter);
        IEnumerable<Gasto> GetGastosByConsumo(int idconsumo, string iduser, bool role);
        IEnumerable<Gasto> GetGastosByPago(int idpago, string iduser, bool role);
        Task<IQueryable<Gasto>> GetGastosByFilter(string sorter, int? id);
        Task<IQueryable<Gasto>> SearchGastos(SearchViewModel model);
        IQueryable<Gasto> Sorter(IQueryable<Gasto> gastos, string sorter);
    }
}
