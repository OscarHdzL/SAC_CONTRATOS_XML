using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SistemaDeAdquisiciones.Controllers
{
    public class MesaValController : Controller
    {
        //VisorVesiones
        public IActionResult Index(Guid id)
        {
            ViewData["MV"] = id.ToString();
            return View();
        }
        public IActionResult Evaluador(Guid id)
        {
            ViewData["MV"] = id.ToString();
            return View();
        }
        public IActionResult VisorVesiones(String id)
        {

            ViewData["IDD"] = id.Split('|')[0].ToString();
            ViewData["NumSol"] = id.Split('|')[1].ToString();
            ViewData["NomDoc_"] = id.Split('|')[2].ToString();
            ViewData["idgral"] = id.Split('|')[3].ToString();
            ViewData["alert"] = id.Split('|')[4].ToString();



            return View();
        }
        public IActionResult VisorVesionesEvaluador(String id)
        {

            ViewData["IDD"] = id.Split('|')[0].ToString();
            ViewData["NumSol"] = id.Split('|')[1].ToString();
            ViewData["NomDoc_"] = id.Split('|')[2].ToString();
            ViewData["idgral"] = id.Split('|')[3].ToString();
            ViewData["alert"] = id.Split('|')[4].ToString();



            return View();
        }
    }
}