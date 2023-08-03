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
    [Route("avanzarfase")]
    [EnableCors("CorsPolicy")]
    public class AvanzarFaseController : ControllerBase
    {
        #region Instancias
        
        private readonly IConfiguration _configuration;
        #endregion
        public AvanzarFaseController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #region Operaciones
        
        [HttpGet("Solicitud/{Solicitud}")]
        public IActionResult Avanzar(String Solicitud)
        {
            avanzar_fase_negocio Negocio = new avanzar_fase_negocio();
            Crudresponse Query = Negocio.avanzar_fase(Solicitud).Response;
            return Ok(Query);
        }
        #endregion
    }
}
