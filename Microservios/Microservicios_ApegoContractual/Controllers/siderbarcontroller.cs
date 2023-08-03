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
using Newtonsoft.Json;
using Solucion_Negocio;
using Utilidades.Log4Net;

namespace Servicio.Controllers
{

    [Produces("application/json")]
    [Route("Sidebar")]
    [EnableCors("CorsPolicy")]
    public class siderbarController : ControllerBase
    {
        #region Instancias
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        #endregion
        public siderbarController(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = new LoggerManager();

        }
        #region Operaciones
        [HttpGet("Get/{rol}")]
        public IActionResult Get(String rol)
        {
            try
            {
                vs_siderbar_negocio Negocio = new vs_siderbar_negocio();
                List<vs_siderbar> Query = Negocio.Consultar(rol).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }
        #endregion

       

 
    }
}
