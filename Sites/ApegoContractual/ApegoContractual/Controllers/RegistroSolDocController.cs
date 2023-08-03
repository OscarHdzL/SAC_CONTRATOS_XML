using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace ApegoContractual.Controllers
{
    public class RegistroSolDocController : Controller
    { 
        public IActionResult RegistroSolicitudDocumento(Guid id)
        {
            //HttpContext.Session.SetString("HDidContrato", id.ToString());
            //ViewBag.idContrato = id;
            return View(id);
        }
    }
}