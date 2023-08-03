using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace AdministracionContractual.Controllers
{
    public class AreasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult NombreArea(String id)
        {
            HttpContext.Session.SetString("Nombre_Area", id.ToString());
            return Json(HttpContext.Session.GetString("Nombre_Area"));
        }
        public IActionResult NombreSubarea(String id)
        {
            HttpContext.Session.SetString("Nombre_Subarea", id.ToString());
            return Json(HttpContext.Session.GetString("Nombre_Subarea"));
        }
        public IActionResult Subareas(Guid id)
        {
            HttpContext.Session.SetString("Id_area", id.ToString());
            return View(id);
        }
        public IActionResult Subordinados(Guid id)
        {
            return View(id);
        }
    }
}