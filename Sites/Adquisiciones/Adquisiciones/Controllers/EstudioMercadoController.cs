using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace SistemaDeAdquisiciones.Controllers
{
    public class EstudioMercadoController : Controller
    {
        public IActionResult Index(Guid id)
        {
            return View(id);
        }
    }
}