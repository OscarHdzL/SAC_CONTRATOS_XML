using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace SistemaDeAdquisiciones.Controllers
{
    public class VisitaSitioController : Controller
    {
        public IActionResult ActaVisitaSitio(Guid id)
        {
            //SITIO - VISITA SITIO
            HttpContext.Session.SetString("FaseAdquisiciones", "SITIO");

            return View(id);
        }

    }
}