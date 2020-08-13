using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProyectoFinal.Data;
using ProyectoFinal.Models;

namespace ProyectoFinal.Interface
{
    public class ConsumoRepository : IConsumo
    {
        private readonly ApplicationDbContext _appDbContext;
        public ConsumoRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public int AddConsumo(Consumo model)
        {
            try
            {
                _appDbContext.Consumos
                    .Add(model);
                _appDbContext.SaveChanges();
                return 200;
            }
            catch (Exception)
            {
                return 500;
            }
        }

        public int DeleteConsumo(int idconsumo)
        {
            try
            {
                var consumo = _appDbContext.Consumos
                    .FirstOrDefault(r => r.IdConsumo == idconsumo);
                _appDbContext.Consumos.Remove(consumo);
                _appDbContext.SaveChanges();
                return 200;
            }
            catch (Exception)
            {
                return 500;
            }
        }

        public int EditConsumo(int idconsumo, Consumo model)
        {
            try
            {
                var consumo = _appDbContext.Consumos
                    .FirstOrDefault(r => r.IdConsumo == idconsumo);
                if (consumo != null)
                {
                    consumo.Descripcion = model.Descripcion;
                    consumo.Imagen = model.Imagen;
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

        public Consumo GetConsumoById(int idconsumo)
        {
            return _appDbContext.Consumos
                 .FirstOrDefault(p => p.IdConsumo == idconsumo);
        }

        public IEnumerable<Consumo> GetConsumos(string sorter)
        {
            var consumos = _appDbContext.Consumos.ToList();
            switch (sorter)
            {
                case "code_desc":
                    consumos = consumos.OrderByDescending(s => s.IdConsumo).ToList();
                    break;
                case "date":
                    consumos = consumos.OrderBy(s => s.FechaCreacion).ToList();
                    break;
                case "date_desc":
                    consumos = consumos.OrderByDescending(s => s.FechaCreacion).ToList();
                    break;
                case "descrip":
                    consumos = consumos.OrderBy(s => s.Descripcion).ToList();
                    break;
                case "descrip_desc":
                    consumos = consumos.OrderByDescending(s => s.Descripcion).ToList();
                    break;
                default:
                    consumos = consumos.OrderBy(s => s.IdConsumo).ToList();
                    break;
            }
            return consumos;
        }
    }
}
