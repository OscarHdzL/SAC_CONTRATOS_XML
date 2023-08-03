using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace SistemaDeAdquisiciones.Controllers
{
    public class FalloController : Controller
    {
        public IActionResult Fallo(Guid id)
        {

            //FALLO - Fallo
            HttpContext.Session.SetString("FaseAdquisiciones", "FALLO");

            return View(id);
        }

    }
}