using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Interface;
using ProyectoFinal.Models;

namespace ProyectoFinal.Controllers
{
    [Authorize]
    public class GastoController : Controller
    {
        private readonly IGasto _gasto;
        private readonly IConsumo _consumo;
        private readonly IPago _pago;
        private readonly UserManager<IdentityUser> _userManager;
        public GastoController(IGasto gasto, IConsumo consumo,
            IPago pago, UserManager<IdentityUser> userManager)
        {
            _gasto = gasto;
            _consumo = consumo;
            _pago = pago;
            _userManager = userManager;
        }
        // GET: Gasto
        public ActionResult Index()
        {
            return RedirectToAction("Create");
        }

        // GET: Gasto/Details/5
        public async Task<IActionResult> Details(string filter, int? consumo,
            int? pago, int? pageNumber, string sortOrder)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CodeSort"] = String.IsNullOrEmpty(sortOrder) ? "code_desc" : "";
            ViewData["DateSort"] = sortOrder == "date" ? "date_desc" : "date";
            ViewData["UserSort"] = sortOrder == "user" ? "user_desc" : "user";
            ViewData["MontoSort"] = sortOrder == "monto" ? "monto_desc" : "monto";
            ViewData["ConsumoSort"] = sortOrder == "consumo" ? "consumo_desc" : "consumo";
            ViewData["PagoSort"] = sortOrder == "pago" ? "pago_desc" : "pago";
            var type = consumo != null ? consumo : pago;
            var gastos = filter != ""
                ? _gasto.GetGastosByFilter(filter, type)
                : sortOrder != "" ? _gasto.GetGastosP(sortOrder) : _gasto.GetGastosP("");
            ViewBag.IdConsumo = _consumo.GetConsumos("");
            ViewBag.IdPago = _pago.GetPagos("");
            int pageSize = 5;
            return View(await PaginatedList<Gasto>.CreateAsync(gastos, pageNumber ?? 1, pageSize));
        }

        // GET: Gasto/Create
        public async Task<IActionResult> Create(int? id, string sortOrder, 
            int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CodeSort"] = String.IsNullOrEmpty(sortOrder) ? "code_desc" : "";
            ViewData["DateSort"] = sortOrder == "date" ? "date_desc" : "date";
            ViewData["UserSort"] = sortOrder == "user" ? "user_desc" : "user";
            ViewData["MontoSort"] = sortOrder == "monto" ? "monto_desc" : "monto";
            ViewData["ConsumoSort"] = sortOrder == "consumo" ? "consumo_desc" : "consumo";
            ViewData["PagoSort"] = sortOrder == "pago" ? "pago_desc" : "pago";
            ViewBag.IdConsumo = _consumo.GetConsumos("");
            ViewBag.IdPago = _pago.GetPagos("");
            int pageSize = 5;
            var data = sortOrder != "" ? _gasto.GetGastosP(sortOrder) : _gasto.GetGastosP("");
            ViewBag.Gastos = await PaginatedList<Gasto>.CreateAsync(data, pageNumber ?? 1, pageSize);
            if (id != null)
            {
                return View( _gasto.GetGastoById(id.Value));
            }
            return View();
        }

        // POST: Gasto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Gasto collection)
        {
            var user =  _userManager.GetUserId(HttpContext.User);
            collection.IdUsuario = user;
            if (ModelState.IsValid)
            {
                var result = collection.IdGasto != 0
                    ? _gasto.EditGasto(collection.IdGasto, collection)
                    : _gasto.AddGasto(collection);
                if (result == 200)
                    return Redirect("/Gasto/Create/");
                return View(collection);
            }
            return View(collection);
        }

        public IActionResult Dashboard()
        {
            ViewData["Message"] = "Dashboard.";
            ViewData["GastoT"] = _gasto.GetGastos("")
                .Sum(r => r.Monto);
            ViewData["GastoM"] = _gasto.GetGastos("")
                .Where(r => r.Fecha.Month == DateTime.Now.Month)
                .Sum(r => r.Monto);
            ViewData["Observaciones"] = _gasto.GetGastos("")
                .Where(r => r.Observacion != "")
                .Count();
            ViewData["GastosC"] = _gasto.GetGastos("")
                .Count();
            ViewBag.GastosR = _gasto.GetGastos("date_desc")
                .Take(5);
            ViewBag.Consumos = _consumo.GetConsumos("");
            ViewBag.Pagos = _pago.GetPagos("");
            ViewBag.Top = _gasto.GetGastos("")
                .GroupBy(r => r.IdConsumo)
                .OrderByDescending(r => r.Count())
                .Select(r => r.Key).Take(3);
            return View();
        }

        // GET: Gasto/Edit/5
        public ActionResult TConsumo(int id)
        {
            var sum = _gasto.GetGastosByConsumo(id)
                .Sum(r => r.Monto);
            return Json(new { total = sum });
        }

        public ActionResult TPago(int id)
        {
            var sum = _gasto.GetGastosByPago(id)
                .Sum(r => r.Monto);
            return Json(new { total = sum });
        }

        public ActionResult TGraph()
        {
            var sum = _gasto.GetGastos("")
                .GroupBy(r => r.Fecha.Month)
                .OrderByDescending(r => r.Count())
                .Select(r => r.Key).Take(4);
            List<decimal> montos = new List<decimal>();
            List<string> meses = new List<string>();
            foreach (var item in sum)
            {
               var monto = _gasto.GetGastos("")
                        .Where(r => r.Fecha.Year == DateTime.Now.Year && r.Fecha.Month == item)
                        .Sum(r => r.Monto);
                montos.Add(monto);
                meses.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item));

            }
            return Json(new { montos = montos, meses=meses });
        }

        // GET: Gasto/Delete/5
        public ActionResult Delete(int id)
        {
            var result = _gasto.DeleteGasto(id);
            if (result == 200)
                return RedirectToAction("Create");
            return RedirectToAction("Create");
        }
    }
}