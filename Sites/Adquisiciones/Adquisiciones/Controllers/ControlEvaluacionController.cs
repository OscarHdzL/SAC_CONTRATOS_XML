using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace SistemaDeAdquisiciones.Controllers
{
    public class ControlEvaluacionController : Controller
    {
        public IActionResult ControlEvaluacion(Guid id)
        {

            //CONEV - Control de evaluación 
            HttpContext.Session.SetString("FaseAdquisiciones", "CONEV");
            return View(id);
        }

        public IActionResult ListadoEvaluaciones()
        {

            return View();
        }

    }
}