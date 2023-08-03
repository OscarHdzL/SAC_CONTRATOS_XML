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
using Newtonsoft.Json;
using Solucion_Negocio;
using Utilidades.Log4Net;

namespace Servicio.Controllers
{

    [Produces("application/json")]
    [Route("contratoproductos")]
    [EnableCors("CorsPolicy")]
    public class ContratoProductosController : ControllerBase
    {
        #region Instancias
        //private tbl_responsable_apego_contrato_negocio Negocio = new tbl_responsable_apego_contrato_negocio();
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        #endregion
        public ContratoProductosController(IConfiguration configuration)
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
                tbl_contrato_productos_negocio Negocio = new tbl_contrato_productos_negocio();
                List<DropDownList> Query = Negocio.FillDrop(Contrato).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
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
