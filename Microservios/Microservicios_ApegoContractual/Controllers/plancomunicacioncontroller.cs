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
    [Route("plancomunicacion")]
    [EnableCors("CorsPolicy")]
    public class PlanComunicacionController : ControllerBase
    {
        #region Instancias
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        #endregion
        public PlanComunicacionController(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = new LoggerManager();
        }
        #region Operaciones
        [HttpGet("Get/Proveedores/{Contrato}")]
        public IActionResult Get(String Contrato)
        {
            try
            {
                tbl_plan_comunicacion_negocio Negocio = new tbl_plan_comunicacion_negocio();
                List<tbl_pc_proveedor> Query = Negocio.Consultar_Proveedor_contrato(Contrato).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
           
        }


        [HttpGet("Get/Proveedor/{idProveedor}")]
        public IActionResult GetProveedor(String idProveedor)
        {
            try
            {
                tbl_plan_comunicacion_negocio Negocio = new tbl_plan_comunicacion_negocio();
                tbl_pc_proveedor Query = Negocio.Consultar_Proveedor(idProveedor).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }


        [HttpPost("Proveedor/add")]
        public IActionResult Add([FromBody] tbl_pc_proveedor_add Proveedor)
        {
            try
            {
                tbl_plan_comunicacion_negocio Negocio = new tbl_plan_comunicacion_negocio();
                List<Crudresponse> Query = Negocio.addProveedor(Proveedor).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
           
        }

        [HttpDelete("Proveedor/delete/{idProveedor}")]
        public IActionResult Delete(String idProveedor)
        {
            try
            {
                tbl_plan_comunicacion_negocio Negocio = new tbl_plan_comunicacion_negocio();
                List<Crudresponse> Query = Negocio.deleteProveedor(idProveedor).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return BadRequest();
            }
            
        }

        #endregion


    }
}
