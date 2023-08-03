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
using Modelos.Modelos.Verificacion;
using Modelos.Modelos.VerificacionContrato;
using Newtonsoft.Json;
using Solucion_Negocio;
using Utilidades.Log4Net;

namespace Servicio.Controllers
{

    [Produces("application/json")]
    [Route("verificacion")]
    [EnableCors("CorsPolicy")]
    public class VerificacionController : ControllerBase
    {
        #region Instancias
        private tbl_sanciones_negocio Negocio = new tbl_sanciones_negocio();
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        #endregion
        public VerificacionController(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = new LoggerManager();
        }
        #region Operaciones
        [HttpGet("Get/Lista/{Dependencia}/{Contrato}")]
        public IActionResult Get(string Dependencia, string Contrato)
        {
            try
            {
                tbl_verificacion_contrato_negocio Negocio = new tbl_verificacion_contrato_negocio();
                List<lista_verificados> Query = Negocio.Consultar(Dependencia, Contrato).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }

        [HttpGet("Get/Lista/{idDependencia}")]
        public IActionResult Get(string idDependencia)
        {
            try
            {
                tbl_verificacion_contrato_negocio Negocio = new tbl_verificacion_contrato_negocio();
                List<lista_verificados> Query = Negocio.Consultar_SinContrato(idDependencia).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }

        [HttpPost("Add")]
        public IActionResult Add([FromBody] tbl_verificacion_contrato_add Verificacion)
        {
            try
            {
                tbl_verificacion_contrato_negocio Negocio = new tbl_verificacion_contrato_negocio();
                List<Crudresponse> Query = Negocio.add(Verificacion).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
            
        }

        [HttpPost("AddV2")]
        public IActionResult AddV2([FromBody] tbl_verificacion_contrato_add Verificacion)
        {
            try
            {
                tbl_verificacion_contrato_negocio Negocio = new tbl_verificacion_contrato_negocio();
                var Query = Negocio.addV2(Verificacion);
                if (Query.CurrentException == null)
                {
                    if (Query.Response[0].cod == "success")
                    {
                        return Ok(Query.Response);
                    }
                    else 
                    {
                        return BadRequest(Query);
                    }
                    
                }
                else 
                {
                    return BadRequest(Query);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }

        }

        [HttpPost("DeleteV2")]
        public IActionResult DeleteV2([FromBody] tbl_verificacion_contrato_add Verificacion)
        {
            try
            {
                tbl_verificacion_contrato_negocio Negocio = new tbl_verificacion_contrato_negocio();
                var Query = Negocio.deleteV2(Verificacion);
                if (Query.CurrentException == null)
                {
                    if (Query.Response[0].cod == "success")
                    {
                        return Ok(Query.Response);
                    }
                    else
                    {
                        return BadRequest(Query);
                    }

                }
                else
                {
                    return BadRequest(Query);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }

        }

        [HttpGet("Get/VerificacionXContrato/{idContrato}")]
        public IActionResult VerificacionXContrato(string idContrato)
        {
            try
            {
                tbl_verificacion_contrato_negocio Negocio = new tbl_verificacion_contrato_negocio();
                var Query = Negocio.GetVerificacionxContrato(idContrato);
                if (Query.CurrentException == null)
                {
                    return Ok(Query.Response);
                }
                else
                {
                    return BadRequest(Query);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("VerificacionXContrato", ex);
                return BadRequest();
            }
        }

        //[HttpDelete("Delete")]
        //public IActionResult Delete([FromBody] tbl_verificacion_contrato_add Verificacion)
        //{
        //    tbl_verificacion_contrato_negocio Negocio = new tbl_verificacion_contrato_negocio();
        //    List<Crudresponse> Query = Negocio.delete(Verificacion).Response;
        //    return Ok(Query);
        //}


        #endregion

    }
}
