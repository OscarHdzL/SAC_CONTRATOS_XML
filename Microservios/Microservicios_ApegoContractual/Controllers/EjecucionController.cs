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
    [Route("Ejecucion")]
    [EnableCors("CorsPolicy")]
 
 
    public class EjecucionController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        public EjecucionController(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = new LoggerManager();
        }

        [HttpPost("PE/{opcion}/{tbl_link_obligacion_id}/{tbl_plan_entrega_producto_id}")]
        public IActionResult EjecutarPE(string opcion, Guid tbl_link_obligacion_id, Guid tbl_plan_entrega_producto_id)
        {
            try
            {
                tbl_plan_cumplimiento_negocio Negocio = new tbl_plan_cumplimiento_negocio();
                return Ok(Negocio.tbl_plan_cumplimiento(opcion, Guid.NewGuid(), tbl_link_obligacion_id, tbl_plan_entrega_producto_id, "PE").Response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }

        [HttpPost("PM/{opcion}/{tbl_link_obligacion_id}/{tbl_plan_entrega_producto_id}")]
        public IActionResult EjecutarPM(string opcion, Guid tbl_link_obligacion_id, Guid tbl_plan_entrega_producto_id)
        {
            try
            {
                tbl_plan_cumplimiento_negocio Negocio = new tbl_plan_cumplimiento_negocio();
                return Ok(Negocio.tbl_plan_cumplimiento(opcion, Guid.NewGuid(), tbl_link_obligacion_id, tbl_plan_entrega_producto_id, "PM").Response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ejecutar", ex);
                return BadRequest();
            }
            
        }

        [HttpPost("CerrarContrato/{tbl_contrato_id}")]
        public IActionResult EjecutarCierreContrato(Guid tbl_contrato_id)
        {
            try
            {
                tbl_plan_cumplimiento_negocio Negocio = new tbl_plan_cumplimiento_negocio();
                return Ok(Negocio.CerrarContrato(tbl_contrato_id).Response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ejecutar", ex);
                return BadRequest();
            }
            
        }


        [HttpPost("ConfirmarPM/{tbl_plan_monitoreo_id}")]
        public IActionResult ConfirmacionPM(Guid tbl_plan_monitoreo_id)
        {
            try
            {
                tbl_plan_cumplimiento_negocio Negocio = new tbl_plan_cumplimiento_negocio();
                return Ok(Negocio.ConfirmarPM(tbl_plan_monitoreo_id).Response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ejecutar", ex);
                return BadRequest();
            }
            
        }

    }
}