using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Interface;
using ProyectoFinal.Models;

namespace ProyectoFinal.Controllers
{
    [Authorize]
    public class MantenimientoController : Controller
    {
        private readonly IConsumo _consumo;
        private readonly IPago _pago;
        public MantenimientoController(IConsumo consumo, IPago pago)
        {
            _consumo = consumo;
            _pago = pago;
        }
        // GET: Mantenimiento
        public ActionResult Index()
        {
            return View();
        }

        // GET: Mantenimiento/Create
        public ActionResult CreatePago(int? id, string sortOrder)
        {
            ViewData["CodeSort"] = String.IsNullOrEmpty(sortOrder) ? "code_desc" : "";
            ViewData["DateSort"] = sortOrder == "date" ? "date_desc" : "date";
            ViewData["DescripSort"] = sortOrder == "descrip" ? "descrip_desc" : "descrip";
            if (id!=null)
            {
                ViewBag.Pagos = sortOrder != "" ? _pago.GetPagos(sortOrder) : _pago.GetPagos("");
                return View(_pago.GetPagoById(id.Value));
            }
            ViewBag.Pagos = sortOrder != "" ? _pago.GetPagos(sortOrder) : _pago.GetPagos("");
            return View();
        }

        public ActionResult CreateConsumo(int? id, string sortOrder)
        {
            ViewData["CodeSort"] = String.IsNullOrEmpty(sortOrder) ? "code_desc" : "";
            ViewData["DateSort"] = sortOrder == "date" ? "date_desc" : "date";
            ViewData["DescripSort"] = sortOrder == "descrip" ? "descrip_desc" : "descrip";
            if (id != null)
            {
                ViewBag.Consumos = sortOrder != "" ? _consumo.GetConsumos(sortOrder) : _consumo.GetConsumos("");
                return View(_consumo.GetConsumoById(id.Value));
            }
            ViewBag.Consumos = sortOrder != "" ? _consumo.GetConsumos(sortOrder) : _consumo.GetConsumos("");
            return View();
        }

        // POST: Mantenimiento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePago(Pago collection)
        {
            if (ModelState.IsValid)
            {
                var result = collection.IdPago != 0 
                    ? _pago.EditPago(collection.IdPago, collection) 
                    : _pago.AddPago(collection);
                if (result == 200)
                    return Redirect("/Mantenimiento/CreatePago/");
                return View(collection);
            }
            return View(collection);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateConsumo(Consumo collection)
        {
            if (ModelState.IsValid)
            {
                var result = collection.IdConsumo != 0
                    ? _consumo.EditConsumo(collection.IdConsumo, collection)
                    : _consumo.AddConsumo(collection);
                if (result == 200)
                    return Redirect("/Mantenimiento/CreateConsumo/");
                return View(collection);
            }
            return View(collection);
        }

        // GET: Mantenimiento/Delete/5
        public ActionResult DeletePago(int id)
        {
            var result = _pago.DeletePago(id);
            if(result==200)
                return RedirectToAction("CreatePago");
            return RedirectToAction("CreatePago");
        }

        // GET: Mantenimiento/Delete/5
        public ActionResult DeleteConsumo(int id)
        {
            var result = _consumo.DeleteConsumo(id);
            if (result == 200)
                return RedirectToAction("CreateConsumo");
            return RedirectToAction("CreateConsumo");
        }
    }
}