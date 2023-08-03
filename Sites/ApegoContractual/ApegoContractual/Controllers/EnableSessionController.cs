using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ApegoContractual.Controllers
{
    public class EnableSessionController : Microsoft.AspNetCore.Mvc.Controller
    {
        public IActionResult Index(Guid id)
        {
            HttpContext.Session.SetString("IdContrato", id.ToString());
            return Json(HttpContext.Session.GetString("Contrato"));
        }
    }
}