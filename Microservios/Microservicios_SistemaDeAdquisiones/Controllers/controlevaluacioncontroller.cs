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
    [Route("controlevaluacion")]
    [EnableCors("CorsPolicy")]
    public class ControlEvaluacionController : ControllerBase
    {
        #region Instancias
        
        private readonly IConfiguration _configuration;
        #endregion
        public ControlEvaluacionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #region Operaciones
        
        [HttpGet("Get/Evaluacion/Solicitud/{Solicitud}")]
        public IActionResult GetEvaluacion(String Solicitud)
        {
            control_evaluacion_negocio Negocio = new control_evaluacion_negocio();
            List<grid_evaluacion_propuestas_solicitud> Query = Negocio.Get_Evaluacion_Propuesta(Solicitud).Response;
            return Ok(Query);
        }

        [HttpPost("Add")]
        public IActionResult AddEvaluacion([FromBody] evaluacion_propuesta_add evaluacion)
        {
            control_evaluacion_negocio Negocio = new control_evaluacion_negocio();
            Crudresponse Query = Negocio.Add(evaluacion).Response;
            return Ok(Query);
        }
        #endregion
    }
}
