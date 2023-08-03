using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ApegoContractual.Controllers
{
    public class PlanDeEntregaController : Controller
    {

        public ActionResult Index(Guid id)
        {
            return View(id);
        }
        public ActionResult Lista(Guid id)
        {

            return View(id);
        }
        public ActionResult EjecucionListaUbicaciones(Guid id)
        {
            ViewBag.IdContrato = HttpContext.Session.GetString("IdContrato");
            return View(id);
        }

        public ActionResult Edit(Guid id, Guid idPlan) 
        {
            ViewBag.Plan = idPlan;
            return View(id);
        }

        public ActionResult DeletePosiciones(Guid id, Guid idPlan)
        {
            ViewBag.Plan = idPlan;
            return View(id);
        }

    }
}