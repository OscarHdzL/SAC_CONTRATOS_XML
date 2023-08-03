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
using Utilidades.Log4Net;
using Solucion_Negocio;
using Modelos.Response;

namespace Servicio.Controllers
{

    [Produces("application/json")]
    [Route("acuerdos")]
    [EnableCors("CorsPolicy")]
    public class AcuerdosController : ControllerBase
    {
        #region Instancias
        
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        #endregion
        public AcuerdosController(IConfiguration configuration)
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
                tbl_acuerdo_negocio Negocio = new tbl_acuerdo_negocio();
                List<tbl_acuerdos_lista> Query = Negocio.Consultar(Contrato).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetContrato", ex);
                return BadRequest();
            }
        }

        [HttpGet("Get/AcuerdoEdit/{Acuerdo}")]
        public IActionResult GetAcuerdo(String Acuerdo)
        {
            try
            {
                tbl_acuerdo_negocio Negocio = new tbl_acuerdo_negocio();
                tbl_acuerdos_lista Query = Negocio.ConsultarAcuerdo(Acuerdo).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {

                _logger.LogError("GetAcuerdoEdit", ex);
                return BadRequest();
            }
            
        }

        [HttpGet("Get/AcuerdoEditRC/{Acuerdo}/{Contrato}")]
        public IActionResult GetAcuerdoRC(String Acuerdo, String Contrato)
        {
            try
            {
                tbl_acuerdo_negocio Negocio = new tbl_acuerdo_negocio();
                tbl_acuerdos_lista Query = Negocio.ConsultarAcuerdoRC(Acuerdo, Contrato).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {

                _logger.LogError("GetAcuerdoEdit", ex);
                return BadRequest();
            }

        }

        [HttpGet("Get/DropDown/Tipos")]
        public IActionResult GetTipos()
        {
            try
            {
                tbl_acuerdo_negocio Negocio = new tbl_acuerdo_negocio();
                List<DropDownList> Query = Negocio.ConsultarTipos().Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetDropdownTipos", ex);
                return BadRequest();
            }
           
        }

        [HttpPost("Add/Acuerdos")]
        public IActionResult Add_tipo_acuerdo([FromBody] tbl_tipo_acuerdo_add tbl_tipo_acuerdo_add)
        {

            try
            {
                tbl_tipo_acuerdo_add.p_opt = 2;
                tbl_acuerdo_negocio Negocio = new tbl_acuerdo_negocio();
                var respuesta = Negocio.Add_tipo_acuerdo(tbl_tipo_acuerdo_add);
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
                _logger.LogError("AddAcuerdos", ex);
                return BadRequest();
            }
            
        }

        [HttpPost("Update/Acuerdos")]
        public IActionResult Update_tipo_acuerdo([FromBody] tbl_tipo_acuerdo_add tbl_tipo_acuerdo_add)
        {
            try
            {
                tbl_tipo_acuerdo_add.p_opt = 3;
                tbl_acuerdo_negocio Negocio = new tbl_acuerdo_negocio();
                var respuesta = Negocio.Add_tipo_acuerdo(tbl_tipo_acuerdo_add);
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
                _logger.LogError("UpdateAcuerdos", ex);
                return BadRequest();
            }
            
        }

        [HttpDelete("Delete/Acuerdos/{tbl_tipo_acuerdo_id}")]
        public IActionResult Delete_tipo_acuerdo(Guid tbl_tipo_acuerdo_id)
        {
            try
            {
                tbl_tipo_acuerdo_add tbl_tipo_acuerdo_add = new tbl_tipo_acuerdo_add();
                tbl_tipo_acuerdo_add.p_opt = 4;
                tbl_tipo_acuerdo_add.p_id = tbl_tipo_acuerdo_id;
                tbl_acuerdo_negocio Negocio = new tbl_acuerdo_negocio();
                List<Crudresponse> Query = Negocio.Delete_tipo_acuerdo(tbl_tipo_acuerdo_add).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("DeleteAcuerdos", ex);
                return BadRequest();
            }
            
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] tbl_acuerdo_add Acuerdo)
        {
            try
            {
                tbl_acuerdo_negocio Negocio = new tbl_acuerdo_negocio();
                List<Crudresponse> Query = Negocio.add(Acuerdo).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
           
        }

        [HttpPut("{update}")]
        public IActionResult update([FromBody] tbl_acuerdo_add Acuerdo)
        {
            try
            {
                tbl_acuerdo_negocio Negocio = new tbl_acuerdo_negocio();
                List<Crudresponse> Query = Negocio.update(Acuerdo).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Update", ex);
                return BadRequest();
            }
            
        }
       
        [HttpDelete("{delete}")]
        public IActionResult delete([FromBody] tbl_acuerdo_add Acuerdo)
        {
            try
            {
                tbl_acuerdo_negocio Negocio = new tbl_acuerdo_negocio();
                List<Crudresponse> Query = Negocio.delete(Acuerdo).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Delete", ex);
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
