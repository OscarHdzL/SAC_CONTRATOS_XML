using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AdministracionContractual.Controllers
{
    public class CatalogosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult TipoRiesgo_Index()
        {
            return View();
        }
        public IActionResult TipoDocumento_Index()
        {
            return View();
        }
        public IActionResult TipoProyecto_Index()
        {
            return View();
        }
        public IActionResult TipoEjecucion_Index()
        {
            return View();
        }
        public IActionResult TipoContrato_Index()
        {
            return View();
        }
        public IActionResult TipoPrioridad_Index()
        {
            return View();
        }
        public IActionResult TipoInterlocutor_Index()
        {
            return View();
        }
        public IActionResult UnidadMedida_Index()
        {
            return View();
        }
        public IActionResult Procedimiento_Index()
        {
            return View();
        }
        //ejemplo catalogo periodo
        public IActionResult TipoPeriodo_Index()
        {
            return View();
        }

        public IActionResult TipoSancion_Index()
        {
            return View();
        }

        public IActionResult TipoObligacion_Index()
        {
            return View();
        }

        public IActionResult TipoAcuerdo_Index()
        {
            return View();
        }

    }
}