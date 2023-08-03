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
    [Route("solicitudjustificacion")]
    [EnableCors("CorsPolicy")]
    public class SolicitudJustificacionController : ControllerBase
    {
        #region Instancias
        
        private readonly IConfiguration _configuration;
        #endregion
        public SolicitudJustificacionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #region Operaciones
        
        [HttpPost("Add")]
        public IActionResult GuardarSolicitudJustifacion([FromBody] tbl_solicitud_justificacion justificacion)
        {
            tbl_solicitud_justificacion_negocio Negocio = new tbl_solicitud_justificacion_negocio();
            List<Crudresponse> Query = Negocio.Guardar(justificacion).Response;
            return Ok(Query);
        }
        #endregion
    }
}
