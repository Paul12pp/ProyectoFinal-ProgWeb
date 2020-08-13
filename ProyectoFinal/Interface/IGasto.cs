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
        IQueryable<Gasto> GetGastos();
        IQueryable<Gasto> GetGastosByFilter(string sorter);
    }
}
