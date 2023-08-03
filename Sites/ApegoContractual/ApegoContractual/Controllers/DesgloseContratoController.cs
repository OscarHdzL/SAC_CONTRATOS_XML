using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
namespace ApegoContractual.Controllers
{
    public class DesgloseContratoController : Controller
    { 
        public IActionResult Index(Guid id)
        {
            return View(id);
        }
    }
}