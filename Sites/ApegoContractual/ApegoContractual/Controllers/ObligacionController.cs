﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ApegoContractual.Controllers
{
    public class ObligacionController : Controller
    {
        public IActionResult Index(Guid id)
        {
            return View(id);
        }
    }
}