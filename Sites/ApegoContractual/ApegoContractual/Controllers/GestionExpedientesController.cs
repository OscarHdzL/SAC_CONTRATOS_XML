﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ApegoContractual.Controllers
{
    public class GestionExpedientesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult ExpedientesContrato(Guid id)
        {
            return View(id);
        }
    }
}