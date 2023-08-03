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
using Negocio_SistemaAdquisiciones;

namespace Microservicios_SistemaDeAdquisiciones.Controllers
{

    [Produces("application/json")]
    [Route("Sidebar")]
    [EnableCors("CorsPolicy")]
    public class siderbarController : ControllerBase
    {
        #region Instancias
        private readonly IConfiguration _configuration;
        #endregion
        public siderbarController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #region Operaciones
        [HttpGet("Get/{rol}")]
        public IActionResult Get(String rol)
        {
            vs_siderbar_negocio Negocio = new vs_siderbar_negocio();
            List<vs_siderbar> Query = Negocio.Consultar(rol).Response;
            return Ok(Query);
        }
        #endregion

       

 
    }
}
