using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Modelos.Modelos;
using Modelos.Modelos.ServidoresPublicos;
using Negocio_SistemaAdquisiciones;
using Newtonsoft.Json;


namespace Microservicios_SistemaDeAdquisiciones.Controllers
{

    [Produces("application/json")]
    [Route("proposiciones")]
    [EnableCors("CorsPolicy")]
    public class ProposicionesController : ControllerBase
    {
        #region Instancias
        
        private readonly IConfiguration _configuration;
        #endregion
        public ProposicionesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #region Operaciones



        [HttpGet("Get/Solicitud/{Solicitud}/Tipo/{Tipo}")]
        public IActionResult GetProposiciones(String Solicitud, String Tipo)
        {
            proposiciones_negocio Negocio = new proposiciones_negocio();
            List<proposiciones> Query = new List<proposiciones>();
            if (Tipo == "tec")
            {
                Query = Negocio.get_proposiciones_tec(Solicitud).Response;
            }
            else if (Tipo == "eco")
            {
                Query = Negocio.get_proposiciones_eco(Solicitud).Response;
            }
            
            return Ok(Query);
        }

        [HttpGet("Get/Evaluadas/Solicitud/{Solicitud}/Tipo/{Tipo}")]
        public IActionResult GetProposicionesEvaluadas(String Solicitud, String Tipo)
        {
            proposiciones_negocio Negocio = new proposiciones_negocio();
            List<proposiciones_evaluadas> Query = new List<proposiciones_evaluadas>();

                Query = Negocio.get_proposiciones_evaluadas_tipo(Solicitud, Tipo).Response;
            
            return Ok(Query);
        }


        [HttpGet("Get/Tec/{Solicitud}")]
        public IActionResult GetTec(String Solicitud)
        {
            proposiciones_negocio Negocio = new proposiciones_negocio();
            List<proposiciones> Query = Negocio.get_proposiciones_tec(Solicitud).Response;
            return Ok(Query);
        }

        [HttpGet("Get/Eco/{Solicitud}")]
        public IActionResult GetEco(String Solicitud)
        {
            proposiciones_negocio Negocio = new proposiciones_negocio();
            List<proposiciones> Query = Negocio.get_proposiciones_eco(Solicitud).Response;
            return Ok(Query);
        }


        [HttpPost("Evaluadas/Add")]
        public IActionResult AddProposicionEvaluada([FromBody]proposicion_tec_eco_add proposicion)
        {
            proposiciones_negocio Negocio = new proposiciones_negocio();

            //proposicion_tec_eco_add proposicion = new proposicion_tec_eco_add();
            //String OBJ_Proposicion = HttpContext.Request.Form["Evaluacion"].ToString();
            //proposicion = JsonConvert.DeserializeObject<proposicion_tec_eco_add>(OBJ_Proposicion);

            Crudresponse Query = Negocio.add_proposicion(proposicion).Response;

            return Ok(Query);
        }

        [HttpPost("Evaluadas/Update")]
        public IActionResult UpdateProposicionEvaluada([FromBody]proposicion_tec_eco_add proposicion)
        {
            proposiciones_negocio Negocio = new proposiciones_negocio();

            //proposicion_tec_eco_add proposicion = new proposicion_tec_eco_add();
            //String OBJ_Proposicion = HttpContext.Request.Form["Evaluacion"].ToString();
            //proposicion = JsonConvert.DeserializeObject<proposicion_tec_eco_add>(OBJ_Proposicion);

            Crudresponse Query = Negocio.update_proposicion(proposicion).Response;

            return Ok(Query);
        }



        //[HttpGet("Get/Evaluadas/Tec/{Solicitud}")]
        //public IActionResult GetProposicionesEvaluadas_Tec(String Solicitud)
        //{
        //    proposiciones_negocio Negocio = new proposiciones_negocio();
        //    List<proposiciones> Query = Negocio.get_proposiciones_eco(Solicitud).Response;
        //    return Ok(Query);
        //}

        //[HttpGet("Get/Evaluadas/Eco/{Solicitud}")]
        //public IActionResult GetProposicionesEvaluadas_Eco(String Solicitud)
        //{
        //    proposiciones_negocio Negocio = new proposiciones_negocio();
        //    List<proposiciones> Query = Negocio.get_proposiciones_eco(Solicitud).Response;
        //    return Ok(Query);
        //}





        #endregion




    }
}
