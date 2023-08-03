using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ApegoContractual.Controllers
{
    public class PlanDeMonitoreoController : Controller
    {
        //public ActionResult Index(Guid id)
        //{
        //    return View(id);
        //}
        public ActionResult Lista(Guid id)
        {
            return View(id);
        }
        public ActionResult EjecucionListaUbicaciones(Guid id)
        {
            return View(id);
        }
    }   
}