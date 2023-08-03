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
    [Route("ConfirmarPE")]
    [EnableCors("CorsPolicy")]
    public class operacionesPEcontroler : ControllerBase
    {
        #region Instancias
        
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        #endregion
        public operacionesPEcontroler(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = new LoggerManager();
        }
        #region Operaciones

        [HttpPost("PE/{p_tbl_plan_entrega_id}")]
        public IActionResult Add(String p_tbl_plan_entrega_id)
        {
            try
            {
                sp_confirmarPE_negocio Negocio = new sp_confirmarPE_negocio();
                Crudresponse Query = Negocio.Confirmar(p_tbl_plan_entrega_id).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
            
        }
        [HttpGet("PE/Incumplidas/{p_opt}/{p_tbl_plan_entrega_id}/{p_plan_entrega_producto_id}")]
        public IActionResult GetIncumplidas(int p_opt, Guid p_tbl_plan_entrega_id, Guid p_plan_entrega_producto_id)
        {
            try
            {
                sp_confirmarPE_negocio Negocio = new sp_confirmarPE_negocio();
                List<tbl_obligacion_cls_PE> Query = Negocio.get_obligacion_PE_Incumplidas(p_opt, p_tbl_plan_entrega_id, p_plan_entrega_producto_id).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }

        [HttpPost("PE/EliminarProducto")]
        public IActionResult EliminarProducto([FromBody] EliminarProductoModel model)
        {
            try
            {
                sp_confirmarPE_negocio Negocio = new sp_confirmarPE_negocio();
                var response = Negocio.Eliminar_producto_plan_entrega(model.tbl_plan_entrega_id, model.plan_entrega_producto_id, model.tbl_ubicacion_plan_entrega_id, model.tbl_contrato_producto_id);
                if (response.CurrentException == null)
                {
                    if (response.Response.cod == "success")
                    {
                        return Ok(response.Response);
                    }
                    else
                    {
                        return BadRequest(response.Response);
                    }
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("eliminar", ex);
                return BadRequest();
            }
            
        }

        [HttpPost("PE/EliminarUbicacion")]
        public IActionResult EliminarUbicacion([FromBody] EliminarUbicacionModel model) 
        {
            try
            {
                sp_confirmarPE_negocio Negocio = new sp_confirmarPE_negocio();
                var response = Negocio.Eliminar_ubicacion_plan_entrega(model.tbl_plan_entrega_id, model.tbl_ubicacion_id);
                if (response.CurrentException == null)
                {
                    if (response.Response.cod == "success")
                    {
                        return Ok(response.Response);
                    }
                    else
                    {
                        return BadRequest(response.Response);
                    }
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("eliminar", ex);
                return BadRequest();
            }
          
        }

        [HttpPost("PE/EliminarPlan")]
        public IActionResult EliminarPlan([FromBody] EliminarPlanModel model)
        {
            try
            {
                sp_confirmarPE_negocio Negocio = new sp_confirmarPE_negocio();
                var response = Negocio.Eliminar_plan_entrega(model.tbl_plan_entrega_id);
                if (response.CurrentException == null)
                {
                    if (response.Response.cod == "success")
                    {
                        return Ok(response.Response);
                    }
                    else
                    {
                        return BadRequest(response.Response);
                    }
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("eliminar", ex);
                return BadRequest();
            }
           
        }

        #endregion
        public class EliminarProductoModel
        {
            public String tbl_plan_entrega_id { get; set; }
            public String plan_entrega_producto_id { get; set; }
            public String tbl_ubicacion_plan_entrega_id { get; set; }
            public String tbl_contrato_producto_id { get; set; }
        }
        public class EliminarUbicacionModel 
        { 
            public String tbl_plan_entrega_id { get; set; }
            public String tbl_ubicacion_id { get; set; }
        }
        public class EliminarPlanModel
        {
            public String tbl_plan_entrega_id { get; set; }
        }



    }
}
