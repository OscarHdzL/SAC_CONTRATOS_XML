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
using Newtonsoft.Json;
using Solucion_Negocio;
using Utilidades.Log4Net;

namespace Servicio.Controllers
{

    [Produces("application/json")]
    [Route("tipoaudiencia")]
    [EnableCors("CorsPolicy")]
    public class AudienciaController : ControllerBase
    {
        #region Instancias
        private tbl_sanciones_negocio Negocio = new tbl_sanciones_negocio();
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        #endregion
        public AudienciaController(IConfiguration configuration)
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
                tbl_tipo_audiencia_negocio Negocio = new tbl_tipo_audiencia_negocio();
                List<tbl_tipo_audiencia_> Query = Negocio.Consultar().Response;
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
