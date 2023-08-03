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
    [Route("PlanMonitoreo")]
    [EnableCors("CorsPolicy")]
    public class planmonitoreocontroller : ControllerBase
    {
        #region Instancias
        private tbl_sanciones_negocio Negocio = new tbl_sanciones_negocio();
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        #endregion
        public planmonitoreocontroller(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = new LoggerManager();
        }
        #region Operaciones
        [HttpGet("Get/Estado")]
        public IActionResult GetEstado()
        {
            try
            {
                tbl_plan_monitoreo_negocio Negocio = new tbl_plan_monitoreo_negocio();
                List<tbl_plan_monitoreo_estado> Query = Negocio.ConsultarEstado().Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }

        [HttpPost("Add")]
        public IActionResult Add([FromBody] sp_plan_monitoreo_struc plan)
        {
            try
            {
                tbl_plan_monitoreo_negocio Negocio = new tbl_plan_monitoreo_negocio();
                List<CrudresponseIdentificador> Query = Negocio.sp_plan_monitoreo(plan).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
            
        }

        /////////////////////////////////////////
        ///EJECUCION

        [HttpGet("Get/List/{idContrato}")]
        public IActionResult GetEstado(Guid idContrato)
        {
            try
            {
                tbl_plan_monitoreo_negocio Negocio = new tbl_plan_monitoreo_negocio();
                List<tbl_plan_monitoreo_lista> Query = Negocio.ConsultarPlanes_Monitoreo(idContrato).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }


        [HttpGet("Get/Ubicaciones/Plan/{idPlanMonitoreo}")]
        public IActionResult GetUbicaciones_Plan(Guid idPlanMonitoreo)
        {
            try
            {
                tbl_plan_monitoreo_negocio Negocio = new tbl_plan_monitoreo_negocio();
                List<tbl_ubicaciones_planmonitoreo> Query = Negocio.ConsultarUbicaciones_PlanMonitoreo(idPlanMonitoreo).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }

        [HttpGet("Get/Productos/Plan/ubicacion/{idPlanMonitoreo}/{idUbicacion}")]
        public IActionResult GetProductos_PlanMon_Ubicacion(Guid idPlanMonitoreo, Guid idUbicacion)
        {
            try
            {
                tbl_plan_monitoreo_negocio Negocio = new tbl_plan_monitoreo_negocio();
                List<sp_productos_ubicacion_monitoreo> Query = Negocio.ConsultarProductos_Ubic_PlanMon(idPlanMonitoreo, idUbicacion).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }


        [HttpGet("Get/Obligaciones/Producto/Plan/ubicacion/{idPlanMonitoreo}/{idUbicacion}/{idProducto}")]
        public IActionResult GetObligaciones_Producto_PlanMon_Ubicacion(Guid idPlanMonitoreo, Guid idUbicacion, Guid idProducto)
        {
            try
            {
                tbl_plan_monitoreo_negocio Negocio = new tbl_plan_monitoreo_negocio();
                List<sp_obligaciones_ubicacion_producto_planmonitoreo> Query = Negocio.ConsultarObligaciones_Ubic_Producto(idPlanMonitoreo, idUbicacion, idProducto).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }


        [HttpGet("Get/Obligaciones/Nocumple/{idPlanMonitoreo}/{idProducto}")]
        public IActionResult GetObligaciones_NoCumple(Guid idPlanMonitoreo, Guid idProducto)
        {
            try
            {
                tbl_plan_monitoreo_negocio Negocio = new tbl_plan_monitoreo_negocio();
                List<sp_obligaciones_nocumple> Query = Negocio.ConsultarObligaciones_NoCumple(idPlanMonitoreo, idProducto).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }


        [HttpGet("Get/Obligaciones/Incumplidas/{idPlanMonitoreo}")]
        public IActionResult GetObligaciones_Incumplidas(Guid idPlanMonitoreo, Guid idProducto)
        {
            try
            {
                tbl_plan_monitoreo_negocio Negocio = new tbl_plan_monitoreo_negocio();
                List<sp_obligaciones_incumplidas> Query = Negocio.ConsultarObligaciones_Incumplidas(idPlanMonitoreo).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }

        [HttpPost("add/Doc/PM")]
        public IActionResult Add_ArchivoPE([FromBody] sp_tbl_ArchivosMonitoreo input)
        {
            try
            {
                tbl_plan_monitoreo_negocio Negocio = new tbl_plan_monitoreo_negocio();
                List<Crudresponse> Query = Negocio.Add_Archivo_PM_(input).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
           
        }

        [HttpGet("Get/lista/monitoreo/id/{monitoreo}")]
        public IActionResult Get_Mon_Lista_FileName_Token(String monitoreo)
        {
            try
            {
                tbl_plan_monitoreo_negocio Negocio = new tbl_plan_monitoreo_negocio();
                var respose = Negocio._sp_download_filename_monitoreo(monitoreo);
                if (respose.CurrentException == null)
                {
                    return Ok(respose.Response);
                }
                else
                {
                    return BadRequest(respose);
                }
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
