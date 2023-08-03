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
    [Route("ProductoServicio")]
    [EnableCors("CorsPolicy")]
    public class ProductoServicioController : ControllerBase
    {
        #region Instancias
 
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        #endregion
        public ProductoServicioController(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = new LoggerManager();
        }

        #region Operaciones
        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] tbl_producto_servicio_add entidad)
        {
            try
            {
                entidad.p_opt = 2;
                tbl_producto_servicio_negocio Negocio = new tbl_producto_servicio_negocio();
                List<Crudresponse> Query = Negocio.Add(entidad).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
            
        }
        [HttpPut]
        [Route("Put")]
        public IActionResult Put([FromBody] tbl_producto_servicio_add entidad)
        {
            try
            {
                entidad.p_opt = 3;
                tbl_producto_servicio_negocio Negocio = new tbl_producto_servicio_negocio();
                List<Crudresponse> Query = Negocio.Add(entidad).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("put", ex);
                return BadRequest();
            }
            
        }
        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                tbl_producto_servicio_add entidad = new tbl_producto_servicio_add();
                entidad.p_opt = 4;
                entidad.p_id = id.ToString();
                entidad.p_tbl_dependencia_id = Guid.NewGuid();
                entidad.p_producto_servicio = "";
                entidad.p_clave_producto = "";
                entidad.p_elemento = "";
                entidad.p_elemento_desc = "";
                entidad.p_tbl_unidad_medida_id = Guid.NewGuid();
                entidad.p_activo = 1;
                entidad.p_comentario = "";
                entidad.p_tbl_tipo_id = "";
                tbl_producto_servicio_negocio Negocio = new tbl_producto_servicio_negocio();
                List<Crudresponse> Query = Negocio.Add(entidad).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return BadRequest();
            }
            
        }

        [HttpGet]
        [Route("List/Dependencia/{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                tbl_producto_servicio_negocio Negocio = new tbl_producto_servicio_negocio();
                List<tbl_producto_servicio> Query = Negocio.Consultar(id).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }

        [HttpGet]
        [Route("List/Contrato/{id}")]
        public IActionResult GetContrato(Guid id)
        {
            try
            {
                tbl_producto_servicio_negocio Negocio = new tbl_producto_servicio_negocio();
                List<tbl_producto_servicio_contrato> Query = Negocio.ConsultarContrato(id).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
         
        }

        [HttpGet]
        [Route("List/Productos/PE/{idPE}/Ubicacion/{idUbicacion}")]
        public IActionResult ProductosUbicacionPE(Guid idPE, Guid idUbicacion)
        {
            try
            {
                tbl_producto_servicio_negocio Negocio = new tbl_producto_servicio_negocio();
                List<DropDownList> Query = Negocio.ProductosUbicacionPE(idPE, idUbicacion).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }

        [HttpGet]
        [Route("Productos/PE/{idPE}/Ubicacion/{idUbicacion}")]
        public IActionResult Productos_UbicacionPE(Guid idPE, Guid idUbicacion)
        {
            try
            {
                tbl_producto_servicio_negocio Negocio = new tbl_producto_servicio_negocio();
                List<producto_servicio_pe> Query = Negocio.Lista_ProductosUbicacionPE(idPE, idUbicacion).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
           
        }

        [HttpGet]
        [Route("List/UnidadesMedida")]
        public IActionResult GetUnidadesMedida()
        {
            try
            {
                tbl_producto_servicio_negocio Negocio = new tbl_producto_servicio_negocio();
                List<tbl_unidad_medida> Query = Negocio.ConsultarUnidadesMedida().Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
         
        }
        [HttpGet]
        [Route("List/TipoPS")]
        public IActionResult Get_tipo_PS()
        {
            try
            {
                tbl_producto_servicio_negocio Negocio = new tbl_producto_servicio_negocio();
                List<DropDownList> Query = Negocio.ConsultarTipo_Prod_Serv().Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
        
        }
        [HttpGet]
        [Route("Unitario/{id}")]
        public IActionResult GetUnidadesMedida(Guid id)
        {
            try
            {
                tbl_producto_servicio_negocio Negocio = new tbl_producto_servicio_negocio();
                List<tbl_producto_servicio> Query = Negocio.ConsultarUnitario(id).Response;
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
