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
    [Route("sanciones")]
    [EnableCors("CorsPolicy")]
    public class SancionController : ControllerBase
    {
        #region Instancias
        private tbl_sanciones_negocio Negocio = new tbl_sanciones_negocio();
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        #endregion
        public SancionController(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = new LoggerManager();
        }
        #region Operaciones
        [HttpGet("Get")]
        public IActionResult Get()
        {
            try
            {
                tbl_sanciones_negocio Negocio = new tbl_sanciones_negocio();
                List<tbl_sanciones> Query = Negocio.Consultar().Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }


        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }


        [HttpPost("Add/Sanciones")]
        public IActionResult Add_tipo_sancion([FromBody] tbl_tipo_sancion_add tbl_tipo_sancion_add)
        {
            try
            {
                tbl_tipo_sancion_add.p_opt = 2;
                tbl_sanciones_negocio Negocio = new tbl_sanciones_negocio();
                var respuesta = Negocio.Add_tipo_sancion(tbl_tipo_sancion_add);
                if (respuesta.CurrentException == null)
                {
                    return Ok(respuesta);
                }
                else
                {
                    return BadRequest(respuesta);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
           
        }
        [HttpPost("Update/Sanciones")]
        public IActionResult Update_tipo_sancion([FromBody] tbl_tipo_sancion_add tbl_tipo_sancion_add)
        {
            try
            {
                tbl_tipo_sancion_add.p_opt = 3;
                tbl_sanciones_negocio Negocio = new tbl_sanciones_negocio();
                var respuesta = Negocio.Add_tipo_sancion(tbl_tipo_sancion_add);
                if (respuesta.CurrentException == null)
                {
                    return Ok(respuesta);
                }
                else
                {
                    return BadRequest(respuesta);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("update", ex);
                return BadRequest();
            }
           
        }

        [HttpDelete("Delete/Sanciones/{tbl_tipo_sancion_id}")]
        public IActionResult Delete_tipo_periodo(Guid tbl_tipo_sancion_id)
        {
            try
            {
                tbl_tipo_sancion_add tbl_Tipo_Sancion = new tbl_tipo_sancion_add();
                tbl_Tipo_Sancion.p_opt = 4;
                tbl_Tipo_Sancion.p_id = tbl_tipo_sancion_id;
                tbl_sanciones_negocio Negocio = new tbl_sanciones_negocio();
                List<Crudresponse> Query = Negocio.Delete_tipo_sancion(tbl_Tipo_Sancion).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return BadRequest();
            }
           
        }




    }
}
#endregion