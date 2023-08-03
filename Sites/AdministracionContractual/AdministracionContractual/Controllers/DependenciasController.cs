using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace AdministracionContractual.Controllers
{
    public class DependenciasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Dependencias_areas(String id)
        {
            if (id == null) {
                id = "";
            }
            HttpContext.Session.SetString("Dep_Area_Navegacion", id.ToString());
            return Json(HttpContext.Session.GetString("Dep_Area_Navegacion"));
        }
    }
}