using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ApegoContractual.Controllers
{
    public class PlanComunicacionController : Controller
    {
        public IActionResult PlanComunicacion(Guid id)
        {
            return View(id);
        }
    }
}