using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Modelos.Modelos;
using Modelos.Interfaz;
using Utilidades.Log4Net;
using Modelos.Modelos.EsquemaPago;
using Newtonsoft.Json;
using Solucion_Negocio;

namespace Servicio.Controllers
{

    [Produces("application/json")]
    [Route("esquemapago")]
    [EnableCors("CorsPolicy")]
    public class EsquemaPagoController : ControllerBase
    {
        #region Instancias
        
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        #endregion
        public EsquemaPagoController(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = new LoggerManager();
        }
        #region Operaciones
        [HttpGet("Get/{Contrato}")]
        public IActionResult Get(String Contrato)
        {
            try
            {
                tbl_esquema_pago_negocio Negocio = new tbl_esquema_pago_negocio();
                List<tbl_esquema_pago_new> Query = Negocio.ConsultarEsquemasPago(Contrato).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest(ex);
            }
            
        }

        [HttpGet("Get/{Esquema}/{Contrato}")]
        public IActionResult Get(String Esquema, String Contrato)
        {
            try
            {
                tbl_esquema_pago_negocio Negocio = new tbl_esquema_pago_negocio();
                tbl_esquema_pago_new Query = Negocio.ConsultarEsquemaPago_esquemacontrato(Esquema, Contrato).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest(ex);
            }
            
        }

        [HttpGet("Get/Proveedores/{Contrato}")]
        public IActionResult GetProveedores(String Contrato)
        {
            try
            {
                tbl_esquema_pago_negocio Negocio = new tbl_esquema_pago_negocio();
                List<tbl_proveedores_esquemapago> Query = Negocio.ConsultarProveedores_Contrato(Contrato).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetProveedores", ex);
                return BadRequest(ex);
            }
            
        }

        [HttpGet("ConsultarCuentasXPagar/{id_dependencia}")]
        public IActionResult cuentas_x_pagar(Guid id_dependencia)
        {
            try
            {
                tbl_esquema_pago_negocio Negocio = new tbl_esquema_pago_negocio();
                var response = Negocio.get_cuentas_pagar(id_dependencia);
                if (response.CurrentException == null)
                {
                    return Ok(response.Response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("cuentas_x_pagar", ex);
                return BadRequest(ex);
            }

        }

        [HttpGet("Get/Instancia/{Instancia}")]
        public IActionResult GetInstancia(String Instancia)
        {
            try
            {
                tbl_esquema_pago_negocio Negocio = new tbl_esquema_pago_negocio();
                tbl_instancia Query = Negocio.ConsultarInstancia(Instancia).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetInstancia", ex);
                return BadRequest(ex);
            }
            
        }

        [HttpPost("Add")]
        public IActionResult Add([FromBody] tbl_esquema_pago_add Factura)
        {
            try
            {
                tbl_esquema_pago_negocio Negocio = new tbl_esquema_pago_negocio();

                var response = Negocio.add(Factura);
                if (response.CurrentException == null)
                {
                    
                    return Ok(response.Response);
                }
                else {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Add", ex);
                return BadRequest(ex);
            }
            
        }

        [HttpPut("Update")]
        public IActionResult Update([FromBody] tbl_esquema_pago_add Esquema)
        {
            try
            {
                tbl_esquema_pago_negocio Negocio = new tbl_esquema_pago_negocio();
                List<Crudresponse> Query = Negocio.update(Esquema).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Update", ex);
                return BadRequest(ex);
            }
            
        }

        [HttpDelete("Delete/{idEsquema}")]
        public IActionResult Delete(string idEsquema)
        {
            try
            {
                tbl_esquema_pago_negocio Negocio = new tbl_esquema_pago_negocio();

                var Query = Negocio.delete(idEsquema);
                if (Query.CurrentException == null)
                {
                    return Ok(Query.Response);
                }
                else {
                    return BadRequest(Query);
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError("Delete", ex);
                return BadRequest(ex);
            }
            
        }
        [HttpGet("Get/PlanesSinEsquema/{Contrato}")]
        public IActionResult GetPlanesSinEsquema(String Contrato)
        {
            try
            {
                tbl_esquema_pago_negocio Negocio = new tbl_esquema_pago_negocio();
                var Query = Negocio.ConsultarPlanes_Sin_Esquema(Contrato);
                if (Query.CurrentException == null)
                {
                    return Ok(Query.Response);
                }
                else {
                    return BadRequest(Query);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("GetPlanesSinEsquema", ex);
                return BadRequest(ex);
            }
            
        }

        [HttpGet("Get/PlanDelEsquema/{idEsquema}")]
        public IActionResult PlanDelEsquema(String idEsquema)
        {
            try
            {
                tbl_esquema_pago_negocio Negocio = new tbl_esquema_pago_negocio();
                var Query = Negocio.ConsultarPlan_Del_Esquema(idEsquema);
                if (Query.CurrentException == null)
                {
                    return Ok(Query.Response);
                }
                else {
                    return BadRequest(Query);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("PlanDelEsquema", ex);
                return BadRequest(ex);
            }

        }
        #endregion




    }
}
