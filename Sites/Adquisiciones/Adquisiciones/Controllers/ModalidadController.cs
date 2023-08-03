using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SistemaDeAdquisiciones.Controllers
{
    public class ModalidadController : Controller
    {
        public IActionResult Index(Guid id)
        {
            //MODAL - Modalidad
            HttpContext.Session.SetString("FaseAdquisiciones", "MODAL");

            ViewBag.Solicitud = id;
            return View();
        }
        public IActionResult Test(Guid id)
        {
            return View();
        }
    }
}