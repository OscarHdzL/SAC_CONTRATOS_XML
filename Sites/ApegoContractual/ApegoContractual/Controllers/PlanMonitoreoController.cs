using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ApegoContractual.Controllers
{
    public class PlanMonitoreoController : Controller
    {
        public IActionResult Index(Guid id)
        {

            return View(id);
        }

        public ActionResult ReporteMonitoreo(Guid id)
        {
            return View(id);
        }
    }
}