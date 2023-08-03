using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ApegoContractual.Controllers
{
    public class MatrizRiesgosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult MatrizRiesgos(Guid id)
        {
            return View(id);
        }
    }
}