using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

//using DropDowDep = PMS.ApegoContractual.Negocio.Controls.DropdownList_Dependencia;
//using ModelSession = PMS.ApegoContractual.Negocio.Modelos.SessionOn;

namespace ApegoContractual.Controllers
{
    public class ListadoVerificacionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ListadoVerificacion()
        {
            //ModelSession ModelSession = new ModelSession();
            //ModelSession = ApegoContractual.GetSessions.GetModelSession();
            //Guid Instancia = ModelSession.Instancia.ID_INSTANCIA_PK;
            //ViewBag.lstDependencias = DropDowDep.Fill(Instancia);
            return View();
        }
        public IActionResult ListaPuntosEvaluar(Guid id)
        {
            return View(id);
        }
    }
}