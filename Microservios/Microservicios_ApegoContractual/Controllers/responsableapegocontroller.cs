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
using Modelos.Modelos.ResponsablesApego;
using Modelos.Modelos.ServidoresPublicos;
using Newtonsoft.Json;
using Solucion_Negocio;
using Utilidades.Log4Net;

namespace Servicio.Controllers
{

    [Produces("application/json")]
    [Route("responsablesapego")]
    [EnableCors("CorsPolicy")]
    public class ResponsableApegoController : ControllerBase
    {
        #region Instancias
        //private tbl_responsable_apego_contrato_negocio Negocio = new tbl_responsable_apego_contrato_negocio();
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        #endregion
        public ResponsableApegoController(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = new LoggerManager();
        }
        #region Operaciones
        [HttpGet("Get/Dropdown/{Contrato}")]
        public IActionResult Get(string Contrato)
        {
            try
            {
                tbl_responsable_apego_contrato_negocio Negocio = new tbl_responsable_apego_contrato_negocio();
                List<DropDownList> Query = Negocio.FillDrop(Contrato).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }

        [HttpGet("Get/ResponsablesApego/RegSolDoc/{Contrato}")]
        public IActionResult GetResponsablesApego_RegSolDoc(string Contrato)
        {
            try
            {
                tbl_responsable_apego_contrato_negocio Negocio = new tbl_responsable_apego_contrato_negocio();
                List<tbl_responsable_apego_contrato_regsoldoc> Query = Negocio.ConsultarResposables_RegSolDoc(Contrato).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
          
        }

        [HttpGet("Get/ResponsablesApego/Responsabilidad/{Contrato}")]
        public IActionResult GetResponsablesApego_Responsabilidad(string Contrato)
        {
            try
            {
                tbl_responsable_apego_contrato_negocio Negocio = new tbl_responsable_apego_contrato_negocio();
                List<tbl_responsable_apego_contrato_responsabilidad> Query = Negocio.ConsultarResponsables_Responsabilidades(Contrato).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
       
        }


        [HttpGet("Get/ResponsablesApego/EsquemaPago/{Contrato}")]
        public IActionResult GetResponsable_EsquemaPago(string Contrato)
        {
            try
            {
                tbl_responsable_apego_contrato_negocio Negocio = new tbl_responsable_apego_contrato_negocio();
                tbl_contrato_servidor_resp_esquemapago Query = Negocio.ConsultarResposable_EsquemaPago(Contrato).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
         
        }

        

        [HttpGet("Get/ResponsablesApegoById/Responsabilidad/{Contrato}/{Responsable}")]
        public IActionResult GetResponsablesApegoById(string Contrato, string Responsable)
        {
            try
            {
                tbl_responsable_apego_contrato_negocio Negocio = new tbl_responsable_apego_contrato_negocio();
                tbl_responsable_apego_contrato_responsabilidad Query = Negocio.ConsultarResponsablesById(Contrato, Responsable).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
           
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] tbl_contrato_servidor_resp_add responsable)
        {
            try
            {
                tbl_responsable_apego_contrato_negocio Negocio = new tbl_responsable_apego_contrato_negocio();
                List<Crudresponse> Query = Negocio.add(responsable).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
        
        }


        [HttpPut("{update}")]
        public IActionResult update([FromBody] tbl_contrato_servidor_resp_add responsable)
        {
            try
            {
                tbl_responsable_apego_contrato_negocio Negocio = new tbl_responsable_apego_contrato_negocio();
                List<Crudresponse> Query = Negocio.update(responsable).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("update", ex);
                return BadRequest();
            }
         
        }

        [HttpDelete("{delete}")]
        public IActionResult delete([FromBody] tbl_contrato_servidor_resp_add responsable)
        {
            try
            {
                tbl_responsable_apego_contrato_negocio Negocio = new tbl_responsable_apego_contrato_negocio();
                List<Crudresponse> Query = Negocio.delete(responsable).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
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
