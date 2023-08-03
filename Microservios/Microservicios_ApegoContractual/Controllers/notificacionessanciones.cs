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
    [Route("NotificacionesSanciones")]
    [EnableCors("CorsPolicy")]
    public class NotificacionesSancionesController : ControllerBase
    {
        #region Instancias
 
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        #endregion
        public NotificacionesSancionesController(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = new LoggerManager();
        }

        #region Operaciones

        [HttpGet]
        [Route("Get/Notificacion/Contrato/{idContrato}/Periodo/{periodo}")]
        public IActionResult GetNotificaciones(Guid idContrato, string periodo)
        {
            try
            {
                tbl_notificacionessanciones_negocio Negocio = new tbl_notificacionessanciones_negocio();
                List<tbl_notificacionsanciones> Query = Negocio.Consultar(idContrato, periodo).Response;

                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
           
        }

        [HttpGet]
        [Route("Get/planes/obligacion/{id_oblig}/{tipo}/{periodo}")]
        public IActionResult Getplanes_obligacion(Guid id_oblig, string tipo, string periodo)
        {
            try
            {
                tbl_notificacionessanciones_negocio Negocio = new tbl_notificacionessanciones_negocio();
                List<tbl_plan_por_obligacion> Query = Negocio.ConsultarPO(id_oblig, tipo, periodo).Response;

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
