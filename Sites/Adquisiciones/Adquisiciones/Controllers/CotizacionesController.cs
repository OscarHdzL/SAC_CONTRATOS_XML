using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace SistemaDeAdquisiciones.Controllers
{
    public class CotizacionesController : Controller
    {
        public IActionResult Cotizaciones(Guid id)
        {
            //RECOT - Recibir cotizaciones
            HttpContext.Session.SetString("FaseAdquisiciones", "RECOT");
            return View(id);
        }

    }
}