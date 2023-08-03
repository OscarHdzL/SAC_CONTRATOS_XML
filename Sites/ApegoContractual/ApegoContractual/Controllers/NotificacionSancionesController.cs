using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace ApegoContractual.Controllers
{
    public class NotificacionSancionesController : Controller
    {
        // GET: NotificacionSanciones
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListaSanciones()
        {
            return View();
        }

        public ActionResult ReporteListaSanciones(Guid id)
        {
            return View(id);
        }

    }
}