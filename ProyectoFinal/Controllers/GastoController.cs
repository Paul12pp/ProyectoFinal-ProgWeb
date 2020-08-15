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
        public GastoController(IGasto gasto, IConsumo consumo,
            IPago pago)
        {
            _gasto = gasto;
            _consumo = consumo;
            _pago = pago;
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
        public ActionResult Create()
        {
            ViewBag.IdConsumo = _consumo.GetConsumos("").AsEnumerable();
            ViewBag.IdPago = _pago.GetPagos("").AsEnumerable();
            ViewBag.Gastos = _gasto.GetGastos();
            return View();
        }

        // POST: Gasto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Gasto collection)
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            ViewData["Message"] = "Dashboard.";

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
            return View();
        }

        // POST: Gasto/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}