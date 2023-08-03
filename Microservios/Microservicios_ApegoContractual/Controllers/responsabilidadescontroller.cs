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
using Modelos.Modelos.Responsabilidades;
using Newtonsoft.Json;
using Solucion_Negocio;
using Utilidades.Log4Net;

namespace Servicio.Controllers
{

    [Produces("application/json")]
    [Route("responsabilidades")]
    [EnableCors("CorsPolicy")]
    public class ResponsabilidadesController : ControllerBase
    {
        #region Instancias
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        #endregion
        public ResponsabilidadesController(IConfiguration configuration)
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
                tbl_responsabilidades_negocio Negocio = new tbl_responsabilidades_negocio();
                List<tbl_responsabilidad> Query = Negocio.Consultar().Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
           
        }

        [HttpPost("update/email/{idPersona}/{Correo}")]
        public IActionResult Update_Email(Guid idPersona, String Correo)
        {
            try
            {
                tbl_responsabilidades_negocio Negocio = new tbl_responsabilidades_negocio();
                List<Crudresponse> Query = Negocio.Update_Email(idPersona, Correo).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Update_Email", ex);
                return BadRequest();
            }
          
        }




        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}


        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}


        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}


        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
        #endregion




    }
}
