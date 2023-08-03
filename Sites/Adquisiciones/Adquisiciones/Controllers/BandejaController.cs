using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace SistemaDeAdquisiciones.Controllers
{
    public class BandejaController : Controller
    {  
        public IActionResult Index()
        {
            
            return View("Solicitante/Index");
        }

    }
}