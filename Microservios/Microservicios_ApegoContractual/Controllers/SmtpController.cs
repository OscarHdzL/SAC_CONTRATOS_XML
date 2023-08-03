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
using Solucion_Negocio.Util.EnvioSmtp;
using Utilidades.Log4Net;

namespace Servicio.Controllers
{

    [Produces("application/json")]
    [Route("smtp")]
    [EnableCors("CorsPolicy")]
    public class SmtpController : ControllerBase
    {
        #region Instancias
 
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        #endregion
        public SmtpController(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = new LoggerManager();
        }
 
        #region Operaciones
        [HttpPost]
        [Route("Send/correo")]
        public IActionResult Send([FromBody]smtp obj)
        {
            try
            {
                string SMTPHost = _configuration.GetValue<string>("SMTPHost");
                string remitenteSMTP = _configuration.GetValue<string>("remitenteSMTP");
                string SMTPPass = _configuration.GetValue<string>("SMTPPass");
                int Port = _configuration.GetValue<int>("Port");

                EnvioCorreos.SendEmailE(SMTPHost, remitenteSMTP, SMTPPass, Port, obj.Email, obj.Asunto, obj.Body);
                return Ok("Se ha enviado el correo");
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest("Imposible Enviar Correo: " + ex.Message);
            }
        }
        

        #endregion





    }
}
