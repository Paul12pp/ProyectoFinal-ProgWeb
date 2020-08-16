using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Interface;
using ProyectoFinal.Models;

namespace ProyectoFinal.Controllers
{
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
        public ActionResult Details(int id)
        {
            return View();
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
            ViewBag.GastosR = _gasto.GetGastos("date_desc")
                .Take(4);
            ViewBag.Consumos = _consumo.GetConsumos("");
            return View();
        }

        // GET: Gasto/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Gasto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
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