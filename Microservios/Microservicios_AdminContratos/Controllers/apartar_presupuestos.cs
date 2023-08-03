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
using Modelos.Modelos.Area;
using Negocio_AdminContratos;
using Newtonsoft.Json;
using Utilidades.Log4Net;

namespace Servicios_AdminitracionContratos.Controllers
{

    [Produces("application/json")]
    [Route("Presupuestos")]
    [EnableCors("CorsPolicy")]
    public class apartar_presupuestoscontroller : ControllerBase
    {
        #region Instancias

        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        #endregion
        public apartar_presupuestoscontroller(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = new LoggerManager();
        }
        private apartar_presupuestos_negocio_core Negocio = new apartar_presupuestos_negocio_core();

        [HttpPost("apartar")]
        public IActionResult Update_Partida([FromForm] String origen, [FromForm] String presupuestos)
        {
            try
            {
                origen_recurso_add origen_ = JsonConvert.DeserializeObject<origen_recurso_add>(origen);
                List<apartar_presupuesto_area_add> presupuestos_ = JsonConvert.DeserializeObject<List<apartar_presupuesto_area_add>>(presupuestos);

                return Ok(Negocio.apartar_presupuesto(origen_, presupuestos_));
            }
            catch (Exception ex)
            {
                _logger.LogError("update", ex);
                return BadRequest();
            }
           
        }
        

    }
}