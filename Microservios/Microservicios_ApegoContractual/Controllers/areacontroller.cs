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
using Modelos.Interfaz;
using Utilidades.Log4Net;
using Modelos.Modelos.ServidoresPublicos;
using Newtonsoft.Json;
using Solucion_Negocio;

namespace Servicio.Controllers
{

    [Produces("application/json")]
    [Route("areas")]
    [EnableCors("CorsPolicy")]
    public class AreaController : ControllerBase
    {
        #region Instancias
        
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        #endregion
        public AreaController(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = new LoggerManager();

        }
        #region Operaciones
        
        [HttpGet("Get/Dropdown/{Dependencia}")]
        public IActionResult GetDrop(String Dependencia)
        {
            try
            {
                tbl_areas_negocio Negocio = new tbl_areas_negocio();
                List<DropDownList> Query = Negocio.FillDrop(Dependencia).Response;
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
