﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace SistemaDeAdquisiciones.Controllers
{
    public class FuncionarioController : Controller
    { 
        public IActionResult Index()
        {
            
            return View();
        }
    }
}