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
using Modelos.Modelos.ServidoresPublicos;
using Negocio_SistemaAdquisiciones;
using Newtonsoft.Json;


namespace Microservicios_SistemaDeAdquisiciones.Controllers
{

    [Produces("application/json")]
    [Route("remitidas")]
    [EnableCors("CorsPolicy")]
    public class RemitidasController : ControllerBase
    {
        #region Instancias
        
        private readonly IConfiguration _configuration;
        #endregion
        public RemitidasController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #region Operaciones
        
        [HttpGet("Get/Tec/{Rol_Usuario}")]
        public IActionResult GetTec(String Rol_Usuario)
        {
            remitidas_negocio Negocio = new remitidas_negocio();
            List<remitidas> Query = Negocio.get_remitidas_tec(Rol_Usuario).Response;
            return Ok(Query);
        }

        [HttpGet("Get/Eco/{Rol_Usuario}")]
        public IActionResult GetEco(String Rol_Usuario)
        {
            remitidas_negocio Negocio = new remitidas_negocio();
            List<remitidas> Query = Negocio.get_remitidas_eco(Rol_Usuario).Response;
            return Ok(Query);
        }
        #endregion
    }
}
