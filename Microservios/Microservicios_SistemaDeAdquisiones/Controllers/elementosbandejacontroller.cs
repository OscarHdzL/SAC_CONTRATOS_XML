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
    [Route("ElementosBandeja")]
    [EnableCors("CorsPolicy")]
    public class ElementosBandejaController : ControllerBase
    {
        #region Instancias
        
        private readonly IConfiguration _configuration;
        #endregion
        public ElementosBandejaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #region Operaciones
        
        [HttpGet("Get/tabs/{Usuario}")]
        public IActionResult GetDrop(String Usuario)
        {
            elementos_bandeja_negocio Negocio = new elementos_bandeja_negocio();
            List<elementos_bandeja> Query = Negocio.getElementos(Usuario).Response;
            return Ok(Query);
        }
        #endregion
    }
}
