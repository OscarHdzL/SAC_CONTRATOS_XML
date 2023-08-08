using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdministracionContractual.Controllers
{
    public class FacturasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult NombreFactura(String id)
        {
            HttpContext.Session.SetString("Nombre_Factura", id.ToString());
            return Json(HttpContext.Session.GetString("Nombre_Factura"));
        }

        public IActionResult NombreConcepto(String id)
        {
            HttpContext.Session.SetString("Nombre_Concepto", id.ToString());
            return Json(HttpContext.Session.GetString("Nombre_Concepto"));
        }
        public IActionResult Conceptos(Guid id)
        {
            HttpContext.Session.SetString("Id_concepto", id.ToString());
            return View(id);
        }

    }
}