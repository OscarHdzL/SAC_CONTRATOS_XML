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
using Modelos.Modelos.Dependencia;
using Negocio_AdminContratos;
using Newtonsoft.Json;
using Utilidades.Log4Net;

namespace Microservicios_AdministracionDeContratos.Controllers
{
    [Route("api/[controller]")]
    [Route("Instancia")]
    [EnableCors("CorsPolicy")]
    public class InstanciaController : ControllerBase
    {
        private tbl_instancia_negocio Negocio = new tbl_instancia_negocio();
        private readonly ILoggerManager _logger;

        public InstanciaController()
        {
            _logger = new LoggerManager();
        }

        [HttpGet("Get_Drop")]
        public IActionResult GetDrop()
        {
            try
            {
                List<DropDownList> Query = Negocio.FillDropC().Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
           
        }
        [HttpPost("Add")]
        public IActionResult Add([FromBody] tbl_instancia_contrato inst)
        {
            try
            {
                List<Crudresponse> Query = Negocio.Add(inst).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
          
        }
        [HttpPut("Update")]
        public IActionResult update([FromBody] tbl_instancia_contrato inst)
        {
            try
            {
                List<Crudresponse> Query = Negocio.update(inst).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("upd", ex);
                return BadRequest();
            }
         
        }
        [HttpGet("Get/{Instancia}")]
        public IActionResult Get(Guid Instancia)
        {
            try
            {
                tbl_instancia_contrato_get Query = Negocio.Get(Instancia.ToString()).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
           
        }
        [HttpDelete("Delete")]
        public IActionResult delete([FromBody] tbl_instancia_contrato inst)
        {
            try
            {
                List<Crudresponse> Query = Negocio.delete(inst).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return BadRequest();
            }
     
        }
    }
}