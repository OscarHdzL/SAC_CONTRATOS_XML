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
    [Route("Comentario")]
    [EnableCors("CorsPolicy")]
    public class ComentarioController : ControllerBase
    {
        #region Instancias
        
        private readonly IConfiguration _configuration;
        #endregion
        public ComentarioController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #region Operaciones
        
        [HttpGet("Get/Solicitud/{Solicitud}")]
        public IActionResult GetComentarios(String Solicitud)
        {
            comentarios_negocio Negocio = new comentarios_negocio();
            List<comentarios> Query = Negocio.get_comentarios_solicitud(Solicitud).Response;
            return Ok(Query);
        }

        [HttpPost("Add")]
        public IActionResult GetComentarios([FromBody] comentario_add comentario)
        {
            comentarios_negocio Negocio = new comentarios_negocio();
            Crudresponse Query = Negocio.Add(comentario).Response;
            return Ok(Query);
        }


        #endregion
    }
}
