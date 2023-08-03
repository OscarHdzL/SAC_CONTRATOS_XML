using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SistemaDeAdquisiciones.Controllers
{
    public class SolicitudController : Controller
    {
        public IActionResult Index()
        {
            //ALSOL - Alta de solicitud (tbl_estatus_solicitud) ~ FASE
            HttpContext.Session.SetString("FaseAdquisiciones", "ALSOL");
            return View();
        }
        public IActionResult ConsultaParcialSolicitud(String id)
        {
            return View(Guid.Parse(id));
        }
        public IActionResult Edita()
        {
            return View();
        }


        public IActionResult Consulta(String id)
        {
            //ALSOL - Alta de solicitud (tbl_estatus_solicitud) ~ FASE
            HttpContext.Session.SetString("FaseAdquisiciones", "ALSOL");
            return View(Guid.Parse(id));
        }

        public IActionResult Enviadas(String id)
        {
            //APROB - En aprobacion
            HttpContext.Session.SetString("FaseAdquisiciones", "APROB");
            return View("Consulta", Guid.Parse(id));
        }

        public IActionResult Complementa(String id)
        {
            //COMPL - Complementar solicitud
            HttpContext.Session.SetString("FaseAdquisiciones", "COMPL");

            return View("Consulta", Guid.Parse(id));
        }

        public IActionResult Rechazadas(String id)
        {
            //RECHA - Solicitud Rechazada
            HttpContext.Session.SetString("FaseAdquisiciones", "RECHA");
            return View("Consulta", Guid.Parse(id));
        }

        public IActionResult PorAprobar(String id)
        {
            //ACCOS - Aceptacion por costos
            HttpContext.Session.SetString("FaseAdquisiciones", "ACCOS");
            return View("Consulta", Guid.Parse(id));
        }

        public IActionResult Aprobadas(String id)
        {
            //ACCOS - Aceptacion por costos
            HttpContext.Session.SetString("FaseAdquisiciones", "ACCOS");
            return View("Consulta", Guid.Parse(id));
        }
        public IActionResult Detalle(String id)
        {
            //ACCOS - Aceptacion por costos
            HttpContext.Session.SetString("FASE_CERO", "");
            return View("Consulta", Guid.Parse(id));
        }

    }
}