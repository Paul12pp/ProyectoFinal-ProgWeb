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

        public Gasto GetGastoById(int idgasto)
        {
            return _appDbContext.Gastos
                .FirstOrDefault(r => r.IdGasto == idgasto)
;        }

        public IEnumerable<Gasto> GetGastos(string sorter)
        {
            var pagos = _appDbContext.Gastos.ToList();
            switch (sorter)
            {
                case "code_desc":
                    pagos = pagos.OrderByDescending(s => s.IdGasto).ToList();
                    break;
                case "date":
                    pagos = pagos.OrderBy(s => s.Fecha).ToList();
                    break;
                case "date_desc":
                    pagos = pagos.OrderByDescending(s => s.Fecha).ToList();
                    break;
                case "user":
                    pagos = pagos.OrderBy(s => s.IdUsuario).ToList();
                    break;
                case "user_desc":
                    pagos = pagos.OrderByDescending(s => s.IdUsuario).ToList();
                    break;
                case "monto":
                    pagos = pagos.OrderBy(s => s.Monto).ToList();
                    break;
                case "monto_desc":
                    pagos = pagos.OrderByDescending(s => s.Monto).ToList();
                    break;
                case "consumo":
                    pagos = pagos.OrderBy(s => s.IdConsumo).ToList();
                    break;
                case "consumo_desc":
                    pagos = pagos.OrderByDescending(s => s.IdConsumo).ToList();
                    break;
                case "pago":
                    pagos = pagos.OrderBy(s => s.IdPago).ToList();
                    break;
                case "pago_desc":
                    pagos = pagos.OrderByDescending(s => s.IdPago).ToList();
                    break;
                default:
                    pagos = pagos.OrderBy(s => s.IdGasto).ToList();
                    break;
            }
            return pagos;
        }

        public IEnumerable<Gasto> GetGastosByConsumo(int idconsumo)
        {
            return _appDbContext.Gastos
               .Where(r => r.IdConsumo == idconsumo)
               .ToList();
        }

        public IEnumerable<Gasto> GetGastosByFilter(string sorter)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Gasto> GetGastosByPago(int idpago)
        {
            return _appDbContext.Gastos
                .Where(r => r.IdPago == idpago)
                .ToList();
        }

        public IQueryable<Gasto> GetGastosP(string sorter)
        {
            var pagos = from s in _appDbContext.Gastos
                             select s;
            switch (sorter)
            {
                case "code_desc":
                    pagos = pagos.OrderByDescending(s => s.IdGasto);
                    break;
                case "date":
                    pagos = pagos.OrderBy(s => s.Fecha);
                    break;
                case "date_desc":
                    pagos = pagos.OrderByDescending(s => s.Fecha);
                    break;
                case "user":
                    pagos = pagos.OrderBy(s => s.IdUsuario);
                    break;
                case "user_desc":
                    pagos = pagos.OrderByDescending(s => s.IdUsuario);
                    break;
                case "monto":
                    pagos = pagos.OrderBy(s => s.Monto);
                    break;
                case "monto_desc":
                    pagos = pagos.OrderByDescending(s => s.Monto);
                    break;
                case "consumo":
                    pagos = pagos.OrderBy(s => s.IdConsumo);
                    break;
                case "consumo_desc":
                    pagos = pagos.OrderByDescending(s => s.IdConsumo);
                    break;
                case "pago":
                    pagos = pagos.OrderBy(s => s.IdPago);
                    break;
                case "pago_desc":
                    pagos = pagos.OrderByDescending(s => s.IdPago);
                    break;
                default:
                    pagos = pagos.OrderBy(s => s.IdGasto);
                    break;
            }
            return pagos;

        }
    }
}
