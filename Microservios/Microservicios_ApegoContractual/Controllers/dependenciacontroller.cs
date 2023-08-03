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
using Modelos.Modelos.ServidoresPublicos;
using Newtonsoft.Json;
using Solucion_Negocio;
using Utilidades.Log4Net;

namespace Servicio.Controllers
{

    [Produces("application/json")]
    [Route("dependencia")]
    [EnableCors("CorsPolicy")]
    public class DependenciaController : ControllerBase
    {
        #region Instancias
        private tbl_servidorespublicos_negocio Negocio = new tbl_servidorespublicos_negocio();
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        #endregion
        public DependenciaController(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = new LoggerManager();
        }
        #region Operaciones
        
        [HttpGet("Get/Dropdown/{Instancia}")]
        public IActionResult GetDrop(String Instancia)
        {
            try
            {
                tbl_dependencia_negocio Negocio = new tbl_dependencia_negocio();
                List<DropDownList> Query = Negocio.FillDrop(Instancia).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetDrop", ex);
                return BadRequest();
            }
            
        }

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
