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
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Contrato;
using Newtonsoft.Json;
using Solucion_Negocio;
using Utilidades.Log4Net;

namespace Servicio.Controllers
{

    [Produces("application/json")]
    [Route("matrizriesgo")]
    [EnableCors("CorsPolicy")]
    public class MatrizRiesgoController : ControllerBase
    {
        #region Instancias
 
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        #endregion
        public MatrizRiesgoController(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = new LoggerManager();
        }
        #region Operaciones
        [HttpGet("Get/{tbl_obligacion_id}")]
        public IActionResult Get(Guid tbl_obligacion_id)
        {
            try
            {
                tbl_matriz_riesgo_negocio Negocio = new tbl_matriz_riesgo_negocio();
                List<tbl_matriz_riesgo> Query = Negocio.GetObligacion(tbl_obligacion_id).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }
        [HttpPost("Add")]
        public IActionResult Add([FromBody]tbl_matriz_riesgo_add tbl_obligacion_id)
        {
            try
            {
                tbl_obligacion_id.p_opt = 2;
                tbl_matriz_riesgo_negocio Negocio = new tbl_matriz_riesgo_negocio();
                List<Crudresponse> Query = Negocio.Add(tbl_obligacion_id).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
            
            
        }
        [HttpPut("Put")]
        public IActionResult Put([FromBody]tbl_matriz_riesgo_add tbl_obligacion_id)
        {
            try
            {
                tbl_obligacion_id.p_opt = 3;
                tbl_matriz_riesgo_negocio Negocio = new tbl_matriz_riesgo_negocio();
                List<Crudresponse> Query = Negocio.Add(tbl_obligacion_id).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("put", ex);
                return BadRequest();
            }
            
        }
        [HttpDelete("Delete/{tbl_matriz_riesgo_id}")]
        public IActionResult Delete(Guid tbl_matriz_riesgo_id)
        {
            try
            {
                tbl_matriz_riesgo_add tbl_obligacion_id = new tbl_matriz_riesgo_add();
                tbl_obligacion_id.p_id = tbl_matriz_riesgo_id;
                tbl_obligacion_id.p_opt = 4;
                tbl_obligacion_id.p_riesgo = "";
                tbl_obligacion_id.p_prioridad = "";
                tbl_obligacion_id.p_respuesta = "";
                tbl_obligacion_id.p_accion = "";
                tbl_matriz_riesgo_negocio Negocio = new tbl_matriz_riesgo_negocio();
                List<Crudresponse> Query = Negocio.Add(tbl_obligacion_id).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return BadRequest();
            }
           
        }

        [HttpGet]
        [Route("Get/TipoRespuesta")]
        public ActionResult GetTipoRespuesta() 
        {
            try
            {
                tbl_matriz_riesgo_negocio Negocio = new tbl_matriz_riesgo_negocio();
                List<tbl_tipo_respuesta> Query = Negocio.GetTipoRespuesta().Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetTipoRespuesta", ex);
                return BadRequest();
            }
          
        }
        [HttpGet]
        [Route("Get/NivelRiesgo")]
        public ActionResult GetNivelRiesgo()
        {
            try
            {
                tbl_matriz_riesgo_negocio Negocio = new tbl_matriz_riesgo_negocio();
                List<tbl_nivel_riesgo> Query = Negocio.GetNivelRiesgo().Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetNivelRiesgo", ex);
                return BadRequest();
            }
            
        }
        #endregion

    }
}
