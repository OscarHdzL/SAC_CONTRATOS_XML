using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace SistemaDeAdquisiciones.Controllers
{
    public class EvaluarJustificacionController : Controller
    {
        public IActionResult Index()
        {

            //JUSTI - Justificar
            HttpContext.Session.SetString("FaseAdquisiciones", "JUSTI");
            return PartialView("BandejaBody");

            
        }
    }
}