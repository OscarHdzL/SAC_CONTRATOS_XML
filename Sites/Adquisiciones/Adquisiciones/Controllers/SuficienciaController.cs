using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SistemaDeAdquisiciones.Controllers
{
    public class SuficienciaController : Controller
    {
        public IActionResult Index(Guid id)
        {
            return View(id);
        }
    }
}