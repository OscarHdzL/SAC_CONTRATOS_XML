using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SistemaDeAdquisiciones.Controllers
{
    public class ResponsablesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}