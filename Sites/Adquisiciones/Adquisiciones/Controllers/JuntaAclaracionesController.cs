using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace SistemaDeAdquisiciones.Controllers
{
    public class JuntaAclaracionesController : Controller
    {
        public IActionResult ActaJuntaAclaraciones(Guid id)
        {
            //ACLAR - Junta de aclaraciones
            HttpContext.Session.SetString("FaseAdquisiciones", "ACLAR");

            return View(id);
        }

    }
}