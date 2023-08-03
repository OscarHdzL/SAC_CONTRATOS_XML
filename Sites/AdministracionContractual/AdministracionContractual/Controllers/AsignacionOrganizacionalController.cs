using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using AdmonContratos = AdministracionContractual;
using Microsoft.Extensions.Options;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;

namespace AdministracionContractual.Controllers
{
    
    public class AsignacionOrganizacionalController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult lista_estructura()
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

            return PartialView("_estructura_organica", estructura_l);
        }
    }
}
