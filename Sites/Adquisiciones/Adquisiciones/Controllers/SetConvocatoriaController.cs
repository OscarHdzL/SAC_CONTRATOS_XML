using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SistemaDeAdquisiciones.Controllers
{
    public class SetConvocatoriaController : Controller
    {
        public IActionResult Formulario(Guid id)
        {
            //COPUB - ELABORAR CONVOCATORIA
            HttpContext.Session.SetString("FaseAdquisiciones", "COPUB");

            ViewBag.id_solicitud = id;
            return View("Index");
        }
    }
}