using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SistemaDeAdquisiciones.Controllers
{
    public class ProposicionesController : Controller
    {
       

        public IActionResult EvaluacionTecnica(Guid id)
        {

            //EVTEC - Evalauacion tecnica
            HttpContext.Session.SetString("FaseAdquisiciones", "EVTEC");

            return View(id);
        }

        public IActionResult EvaluacionEconomica(Guid id)
        {

            //EVECO - Evalauacion tecnica
            HttpContext.Session.SetString("FaseAdquisiciones", "EVECO");
            return View(id);
        }


    }
}