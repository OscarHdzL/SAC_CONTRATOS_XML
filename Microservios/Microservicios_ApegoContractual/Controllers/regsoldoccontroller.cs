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
using Modelos.Modelos.RegSolDoc;
using Modelos.Modelos.ResponsablesApego;
using Newtonsoft.Json;
using Solucion_Negocio;
using Utilidades.Log4Net;

namespace Servicio.Controllers
{

    [Produces("application/json")]
    [Route("regsoldoc")]
    [EnableCors("CorsPolicy")]
    public class RegSolDocController : ControllerBase
    {
        #region Instancias
        //private tbl_responsable_apego_contrato_negocio Negocio = new tbl_responsable_apego_contrato_negocio();
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        #endregion
        public RegSolDocController(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = new LoggerManager();
        }
        #region Operaciones
       
        [HttpGet("Get/Lista/{Contrato}")]
        public IActionResult GetResponsablesApego_RegSolDoc(string Contrato)
        {
            try
            {
                tbl_registro_solicitud_docto_negocio Negocio = new tbl_registro_solicitud_docto_negocio();
                List<tbl_registro_solicitud_docto_list> Query = Negocio.Consultar_RegSolDoc(Contrato).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }

        [HttpGet("Get/Solicitudedit/{Contrato}/{Solicitud}")]
        public IActionResult GetSolicitud_RegSolDoc(string Contrato, string Solicitud)
        {
            try
            {
                tbl_registro_solicitud_docto_negocio Negocio = new tbl_registro_solicitud_docto_negocio();
                tbl_contrato_solicitud_docto Query = Negocio.GetSolicitud_RegSolDoc(Contrato, Solicitud).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
           
        }


        [HttpGet("Get/Solicitud/Expediente/{Contrato}")]
        public IActionResult GetSolicitud_Expediente(string Contrato)
        {
            try
            {
                tbl_registro_solicitud_docto_negocio Negocio = new tbl_registro_solicitud_docto_negocio();
                List<tbl_contrato_solicitud_docto_expediente> Query = Negocio.GetSolicitud_Expediente(Contrato).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
           
        }


        [HttpPost("add")]
        public IActionResult Post([FromBody] tbl_contrato_solicitud_docto_add regsoldoc)
        {
            try
            {
                tbl_registro_solicitud_docto_negocio Negocio = new tbl_registro_solicitud_docto_negocio();
                List<Crudresponse> Query = Negocio.add(regsoldoc, 2).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
           
        }
        [HttpPut("Put")]
        public IActionResult Put([FromBody] tbl_contrato_solicitud_docto_add regsoldoc)
        {
            try
            {
                tbl_registro_solicitud_docto_negocio Negocio = new tbl_registro_solicitud_docto_negocio();
                List<Crudresponse> Query = Negocio.add(regsoldoc, 3).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("put", ex);
                return BadRequest();
            }
          
        }

        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}


        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}


        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}


        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
        #endregion




    }
}
