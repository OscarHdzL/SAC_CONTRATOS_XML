﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AdministracionContractual.Controllers
{
    public class InterlocutorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}