using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProyectoFinal.Data;
using ProyectoFinal.Models;

namespace ProyectoFinal.Interface
{
    public class PagoRepository: IPago
    {
        private readonly ApplicationDbContext _appDbContext;
        public PagoRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public int AddPago(Pago model)
        {
            try
            {
                _appDbContext.Pagos
                    .Add(model);
                _appDbContext.SaveChanges();
                return 200;
            }
            catch (Exception)
            {
                return 500;
            }
        }

        public int DeletePago(int idpago)
        {
            try
            {
                var pago = _appDbContext.Pagos
                    .FirstOrDefault(r => r.IdPago == idpago);
                _appDbContext.Pagos.Remove(pago);
                _appDbContext.SaveChanges();
                return 200;
            }
            catch (Exception)
            {
                return 500;
            }
        }

        public int EditPago(int idpago, Pago model)
        {
            try
            {
                var pago = _appDbContext.Pagos
                    .FirstOrDefault(r => r.IdPago == idpago);
                if (pago != null)
                {
                    pago.Descripcion = model.Descripcion;
                    pago.Imagen = model.Imagen;
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

        public Pago GetPagoById(int idpago)
        {
            return _appDbContext.Pagos
                 .FirstOrDefault(p => p.IdPago == idpago);
        }

        public IEnumerable<Pago> GetPagos(string sorter)
        {
            var pagos = _appDbContext.Pagos.ToList();
            switch (sorter)
            {
                case "code_desc":
                    pagos = pagos.OrderByDescending(s => s.IdPago).ToList();
                    break;
                case "date":
                    pagos = pagos.OrderBy(s => s.FechaCreacion).ToList();
                    break;
                case "date_desc":
                    pagos = pagos.OrderByDescending(s => s.FechaCreacion).ToList();
                    break;
                case "descrip":
                    pagos = pagos.OrderBy(s => s.Descripcion).ToList();
                    break;
                case "descrip_desc":
                    pagos = pagos.OrderByDescending(s => s.Descripcion).ToList();
                    break;
                default:
                    pagos = pagos.OrderBy(s => s.IdPago).ToList();
                    break;
            }
            return pagos;
        }
    }
}
