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
using Modelos.Modelos.Contrato;
using Modelos.Interfaz;
using Utilidades.Log4Net;
using Newtonsoft.Json;
using Solucion_Negocio;


namespace Servicio.Controllers
{

    [Produces("application/json")]
    [Route("contratos")]
    [EnableCors("CorsPolicy")]
    public class ContratoController : ControllerBase
    {
        #region Instancias
        private tbl_contrato_negocio Negocio = new tbl_contrato_negocio();
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        #endregion
        public ContratoController(IConfiguration configuration)
        {

            _configuration = configuration;
            _logger = new LoggerManager();
        }
        #region Operaciones
        [HttpGet("Get/{Dependencia}")]
        public IActionResult Get(string Dependencia)
        {
            try
            {
                tbl_contrato_negocio Negocio = new tbl_contrato_negocio();
                List<tbl_contrato> Query = Negocio.Consultar(Dependencia).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
           
        }

        [HttpGet("Get/ListadoContratos/{Dependencia}")]
        public IActionResult Listado(string Dependencia)
        {
            try
            {
                tbl_contrato_negocio Negocio = new tbl_contrato_negocio();
                List<tbl_contrato_list> Query = Negocio.ConsultarLista(Dependencia).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetListadoContratos", ex);
                return BadRequest();
            }
            
        }

        [HttpGet("Get/ListadoContratos/RolDependenciaUsuario/{Rol}/{Dependencia}/{Usuario}")]
        public IActionResult ListadoXRol(string Rol, string Dependencia, string Usuario)
        {
            try
            {
                tbl_contrato_negocio Negocio = new tbl_contrato_negocio();
                var respuesta = Negocio.ConsultarListaXRol(Rol, Dependencia, Usuario);
                if (respuesta.CurrentException == null)
                {
                    return Ok(respuesta.Response);
                }
                else
                {
                    return BadRequest(respuesta.CurrentException);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }

        [HttpGet("Get/ListadoAcuerdos/{usuario}/{contrato}")]
        public IActionResult ListadoAcuerxUsuarioContrato(string usuario, string contrato)
        {
            try
            {
                tbl_contrato_negocio Negocio = new tbl_contrato_negocio();
                var respuesta = Negocio.ConsultarAcuerdosPE(usuario, contrato);
                if (respuesta.CurrentException == null)
                {
                    return Ok(respuesta.Response);
                }
                else
                {
                    return BadRequest(respuesta.CurrentException);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }

        }

        [HttpGet("Get/ListadoAcuerdosRC/{contrato}")]
        public IActionResult ListadoAcuerxUsuarioRC(string contrato)
        {
            try
            {
                tbl_contrato_negocio Negocio = new tbl_contrato_negocio();
                var respuesta = Negocio.ConsultarAcuerdosRC(contrato);
                if (respuesta.CurrentException == null)
                {
                    return Ok(respuesta.Response);
                }
                else
                {
                    return BadRequest(respuesta.CurrentException);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }

        }



        [HttpGet("Get/VistaContrato/{Contrato}")]
        public IActionResult VistaContrato(string Contrato)
        {
            try
            {
                tbl_contrato_negocio Negocio = new tbl_contrato_negocio();
                List<tbl_contrato_vista> Query = Negocio.ConsultarVista(Contrato).Response;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();

                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetVistacontrato", ex);
                return BadRequest();
            }
            
        }

        [HttpPost("CargaMasiva")]
        public IActionResult CargaMasivaContrato([FromBody] List<tbl_contrato_add> contratos)
        {
            try
            {
                tbl_contrato_negocio Negocio = new tbl_contrato_negocio();
                List<Crudresponse> Query = Negocio.CargaMasivaContrato(contratos).Response;
                //return Ok(Query);
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Carga masiva", ex);
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
