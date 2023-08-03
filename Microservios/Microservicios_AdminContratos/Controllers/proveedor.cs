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
    [Route("Proveedor")]
    [EnableCors("CorsPolicy")]
    public class proveedorcontroller : ControllerBase
    {
        #region Instancias

        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        #endregion
        public proveedorcontroller(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = new LoggerManager();
        }
        private tbl_proveedor_negocio  Negocio = new tbl_proveedor_negocio();
        #region Operaciones
        [HttpPost("Add")]
        public IActionResult Add([FromBody] tbl_proveedor_add tbl_proveedor)
        {
            try
            {
                tbl_proveedor.p_opt = 2;
                List<Crudresponse> Query = Negocio.Add(tbl_proveedor).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest(ex);
            }
       
        }
        [HttpPut("Update")]
        public IActionResult Put([FromBody]tbl_proveedor_add tbl_proveedor)
        {
            try
            {
                tbl_proveedor.p_opt = 3;
                List<Crudresponse> Query = Negocio.Add(tbl_proveedor).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("upd", ex);
                return BadRequest(ex);
            }
     
        }
        [HttpDelete("Delete/{tbl_proveedor_id}")]
        public IActionResult Delete(Guid tbl_proveedor_id)
        {
            try
            {
                tbl_proveedor_add tbl_Proveedor_Add = new tbl_proveedor_add();
                tbl_Proveedor_Add.p_opt = 4;
                tbl_Proveedor_Add.p_id = tbl_proveedor_id;
                List<Crudresponse> Query = Negocio.Delete(tbl_Proveedor_Add).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return BadRequest(ex);
            }
          
        }

        [HttpGet("Get/lista/{tbl_instancia_id}")]
        public IActionResult get_lista_p(Guid tbl_instancia_id)
        {
            try
            {
                List<lista_proveedores_estructura> Query = Negocio.Get_lista_proveedores_contrato(tbl_instancia_id.ToString()).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest(ex);
            }
       
        }

        [HttpGet("Get/{tbl_proveedor_id}")]
        public IActionResult get_lista_proveedores_by_id(Guid tbl_proveedor_id)
        {
            try
            {
                List<tbl_proveedor> Query = Negocio.Get_proveedor_id(tbl_proveedor_id.ToString()).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest(ex);
            }
  
        }
        #endregion
        [HttpGet("Get/lista_tipo_interlocutor/{activo}")]
        public IActionResult get_lista_i(string activo)
        {
            try
            {
                List<lista_tipo_interlocutor> Query = Negocio.Get_lista_tipo_interlocutor(activo).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest(ex);
            }
        
        }

        //[HttpGet("Get/info_comercial_interlocutor/{id}")]
        //public IActionResult info_comercial_interlocutor(string id)
        //{
        //    try
        //    {
        //        var Query = Negocio.Get_info_interlocutor_comercial(id);
        //        if (Query.CurrentException == null)
        //        {
        //            return Ok(Query.Response);
        //        }
        //        else
        //        {
        //            return BadRequest(Query);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Get", ex);
        //        return BadRequest();
        //    }
                    
        //}

        [HttpGet("Get/proveedor_dependencias/{id}")]
        public IActionResult proveedor_dependencias(string id)
        {
            try
            {
                var Query = Negocio.proveedor_dependencias(id);
                if (Query.CurrentException == null)
                {
                    return Ok(Query.Response);
                }
                else
                {
                    return BadRequest(Query);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest(ex);
            }

        }

    }
}