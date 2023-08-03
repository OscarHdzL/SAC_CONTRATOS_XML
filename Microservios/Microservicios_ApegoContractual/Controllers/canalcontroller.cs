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
using Newtonsoft.Json;
using Modelos.Interfaz;
using Utilidades.Log4Net;
using Solucion_Negocio;

namespace Servicio.Controllers
{

    [Produces("application/json")]
    [Route("canal")]
    [EnableCors("CorsPolicy")]
    public class CanalController : ControllerBase
    {
        #region Instancias
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        #endregion
        public CanalController(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = new LoggerManager();
        }
        #region Operaciones
        [HttpGet("Get")]
        public IActionResult Get()
        {
            try
            {
                tbl_canal_negocio Negocio = new tbl_canal_negocio();
                List<tbl_canal> Query = Negocio.Consultar().Response;
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
