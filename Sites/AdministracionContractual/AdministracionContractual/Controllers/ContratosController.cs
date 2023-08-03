using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AdministracionContractual.Controllers
{
    public class ContratosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DatosGenerales_parcial()
        {
            return View();
        }

        public IActionResult Timeline_parcial()
        {
            return View();
        }
        public IActionResult Fechas_pacial()
        {
            return View();
        }
        public IActionResult Areas()
        {
            return View();
        }
        public IActionResult Firmantes()
        {
            return View();
        }
        public IActionResult Proveedores()
        {
            return View();
        }
        public IActionResult PenasDeductivas()
        {
            return View();
        }
        public IActionResult formalizacion()
        {
            return View();
        }
        public IActionResult Actualizacion(Guid id)
        {
            ViewData["contrato"] = id.ToString();
            return View();
        }

        public IActionResult Lista(Guid id)
        {
            return View(id);
        }
        public IActionResult EstructuraOrganica(string idDependencia) 
        {
            EndPointAdmon endPointAdmon = new EndPointAdmon();
            string instancia = HttpContext.Session.GetString("IdInstancia");
            HttpClient clienteHttp = new HttpClient();
            var AppGAdmon = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("EndPointAdmon")["EndPointGateway_Admon"];

            clienteHttp.BaseAddress = new Uri(AppGAdmon);
            clienteHttp.DefaultRequestHeaders.Accept.Clear();
            clienteHttp.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            HttpResponseMessage request = clienteHttp.GetAsync("Estructura/Get/" + instancia).Result;

            JArray lista_json = JArray.Parse(request.Content.ReadAsStringAsync().Result);
            List<estructura_organica_core> estructura_l = JsonConvert.DeserializeObject<List<estructura_organica_core>>(lista_json.ToString());
            var index = estructura_l.FindIndex(x => x._dependencia.id == idDependencia);
            var lista = new List<estructura_organica_core>();

            if (index != -1)
            {
                var dependencia = estructura_l[index];
                lista.Add(dependencia);
                return View("_EstructuraOrganica",lista);
            }
            else 
            {
                return View("_EstructuraOrganica",lista);
            }
        }
    }
}