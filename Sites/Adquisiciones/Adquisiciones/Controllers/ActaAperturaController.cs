using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace SistemaDeAdquisiciones.Controllers
{
    public class ActaAperturaController : Controller
    {  
        public IActionResult ActaPresentacionAperturaEconomica(Guid id)
        {
            //APECO - Apertura economica
            HttpContext.Session.SetString("FaseAdquisiciones", "APECO");
            return View(id);
        }

        public IActionResult ActaPresentacionAperturaTecnica(Guid id)
        {
            //APTEC - Apertura tecnica
            HttpContext.Session.SetString("FaseAdquisiciones", "APTEC");
            return View(id);
        }

    }
}