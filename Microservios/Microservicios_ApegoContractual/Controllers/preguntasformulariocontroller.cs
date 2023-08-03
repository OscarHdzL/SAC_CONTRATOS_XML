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
using Modelos.Modelos.PreguntasFormulario;
using Newtonsoft.Json;
using Solucion_Negocio;
using Utilidades.Log4Net;

namespace Servicio.Controllers
{

    [Produces("application/json")]
    [Route("preguntasformulario")]
    [EnableCors("CorsPolicy")]
    public class PreguntasFormularioController : ControllerBase
    {
        #region Instancias
        
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        #endregion
        public PreguntasFormularioController(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = new LoggerManager();
        }
        #region Operaciones
           
        [HttpPost("add")]
        public IActionResult Add([FromBody] tbl_pregunta_formulario_add Pregunta)
        {
            try
            {
                tbl_pregunta_formulario_negocio Negocio = new tbl_pregunta_formulario_negocio();
                List<Crudresponse> Query = Negocio.add(Pregunta).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
            
        }

        [HttpPut("update")]
        public IActionResult update([FromBody] tbl_pregunta_formulario_add Pregunta)
        {
            try
            {
                tbl_pregunta_formulario_negocio Negocio = new tbl_pregunta_formulario_negocio();
                List<Crudresponse> Query = Negocio.update(Pregunta).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("update", ex);
                return BadRequest();
            }
            
        }


        [HttpDelete("delete")]
        public IActionResult delete([FromBody] tbl_pregunta_formulario_add Pregunta)
        {
            try
            {
                tbl_pregunta_formulario_negocio Negocio = new tbl_pregunta_formulario_negocio();
                List<Crudresponse> Query = Negocio.delete(Pregunta).Response;
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
