using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AdministracionContractual.Controllers
{
    public class InstanciaController : Controller
    {
        public IActionResult Index()
        {
            //ViewBag.Nombre = "INE";
            //ViewBag.Color = "Verde";
            return View();
        }
    }
}