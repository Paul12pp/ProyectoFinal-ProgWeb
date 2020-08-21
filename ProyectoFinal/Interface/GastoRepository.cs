using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ProyectoFinal.Data;
using ProyectoFinal.Models;

namespace ProyectoFinal.Interface
{
    public class GastoRepository : IGasto
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GastoRepository(ApplicationDbContext appDbContext,
            UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
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

        public async Task<IEnumerable<Gasto>> GetGastos(string sorter)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            var role = await _userManager.IsInRoleAsync(user, "Admin");
            var pagos = role
                ? _appDbContext.Gastos.ToList()
                : _appDbContext.Gastos.Where(r => r.IdUsuario == user.Id).ToList();
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

        public IEnumerable<Gasto> GetGastos(string iduser,bool role)
        {

            return role
                ? _appDbContext.Gastos
                .ToList()
                : _appDbContext.Gastos
                .Where(r => r.IdUsuario == iduser)
                .ToList();
        }

        public IEnumerable<Gasto> GetGastosByConsumo(int idconsumo, string iduser, bool role)
        {

            return role
                ? _appDbContext.Gastos
               .Where(r => r.IdConsumo == idconsumo)
               .ToList()
               : _appDbContext.Gastos
               .Where(r => r.IdConsumo == idconsumo && r.IdUsuario == iduser)
               .ToList();
        }

        public async Task<IQueryable<Gasto>> GetGastosByFilter(string sorter, int? id)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            var role = await _userManager.IsInRoleAsync(user, "Admin");
            var gastos = role
                ? from s in _appDbContext.Gastos
                  select s
                : from s in _appDbContext.Gastos.Where(r => r.IdUsuario == user.Id)
                  select s;
            switch (sorter)
            {
                case "month":
                    gastos = gastos
                        .Where(m => m.Fecha.Month == DateTime.Now.Month);
                    break;
                case "total":
                    
                    break;
                case "consumo":
                    gastos = gastos
                        .Where(m => m.IdConsumo==id);
                    break;
                case "pago":
                    gastos = gastos
                        .Where(m => m.IdPago == id);
                    break;
                default:
                    gastos = gastos.OrderBy(s => s.IdGasto);
                    break;

            }
            return gastos;
        }

        public IEnumerable<Gasto> GetGastosByPago(int idpago, string iduser, bool role)
        {
            return role
                ? _appDbContext.Gastos
                .Where(r => r.IdPago == idpago)
                .ToList()
                : _appDbContext.Gastos
                .Where(r => r.IdPago == idpago && r.IdUsuario == iduser)
                .ToList();
        }

        public async Task<IQueryable<Gasto>> GetGastosP(string sorter)
        {

            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            var role = await _userManager.IsInRoleAsync(user, "Admin");
            var pagos = role
                ? from s in _appDbContext.Gastos
                  select s
                : from s in _appDbContext.Gastos.Where(r => r.IdUsuario == user.Id)
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

        public async Task<IQueryable<Gasto>> SearchGastos(SearchViewModel model)
        {

            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            var role = await _userManager.IsInRoleAsync(user, "Admin");
            var gastos = role
                ? from s in _appDbContext.Gastos
                  select s
                : from s in _appDbContext.Gastos.Where(r => r.IdUsuario == user.Id)
                  select s;

            return gastos
                .Where(r => r.Fecha >= model.Desde && r.Fecha <= model.Hasta
                && r.IdConsumo == model.IdConsumo && r.IdPago == r.IdPago);
        }

        public IQueryable<Gasto> Sorter(IQueryable<Gasto> pagos, string sorter)
        {
            switch (sorter)
            {
                case "date":
                    pagos =  pagos.OrderBy(s => s.Fecha);
                    break;
                case "date_desc":
                    pagos = pagos.OrderByDescending(s => s.Fecha);
                    break;
                case "monto":
                    pagos = pagos.OrderBy(s => s.Monto);
                    break;
                case "monto_desc":
                    pagos = pagos.OrderByDescending(s => s.Monto);
                    break;
                default:
                    pagos = pagos.OrderBy(s => s.IdGasto);
                    break;

            }
            return pagos;
        }
    }
}
