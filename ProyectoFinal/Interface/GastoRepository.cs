using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProyectoFinal.Data;
using ProyectoFinal.Models;

namespace ProyectoFinal.Interface
{
    public class GastoRepository : IGasto
    {
        private readonly ApplicationDbContext _appDbContext;
        public GastoRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public int AddGasto(Gasto model)
        {
            try
            {
                _appDbContext.Gastos
                    .Add(model);
                _appDbContext.SaveChanges();
                return 200;
            }
            catch (Exception)
            {
                return 500;
            }
        }

        public int DeleteGasto(int idgasto)
        {
            try
            {
                var gasto = _appDbContext.Gastos
                    .FirstOrDefault(r => r.IdGasto == idgasto);
                _appDbContext.Gastos.Remove(gasto);
                _appDbContext.SaveChanges();
                return 200;
            }
            catch (Exception)
            {
                return 500;
            }
        }

        public int EditGasto(int idgasto, Gasto model)
        {
            try
            {
                var gasto = _appDbContext.Gastos
                    .FirstOrDefault(r => r.IdGasto == idgasto);
                if (gasto != null)
                {
                    gasto.IdPago = model.IdPago;
                    gasto.IdConsumo = model.IdConsumo;
                    gasto.Monto = model.Monto;
                    gasto.Observacion = model.Observacion;
                    _appDbContext.SaveChanges();
                    return 200;
                }
                return 500;
            }
            catch (Exception)
            {
                return 500;
            }
        }

        public IEnumerable<Gasto> GetGastos()
        {
            return _appDbContext.Gastos
                .ToList();
        }

        public IEnumerable<Gasto> GetGastosByConsumo(int idconsumo)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Gasto> GetGastosByFilter(string sorter)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Gasto> GetGastosByPago(int idpago)
        {
            throw new NotImplementedException();
        }
    }
}
