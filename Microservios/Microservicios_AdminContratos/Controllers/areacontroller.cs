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
using NegocioAdministracionContratos;
using Newtonsoft.Json;
using Utilidades.Log4Net;

namespace Servicios_AdminitracionContratos.Controllers
{

    [Produces("application/json")]
    [Route("Area")]
    [EnableCors("CorsPolicy")]
    public class areacontroller : ControllerBase
    {
        private tbl_area_negocio_core Negocio = new tbl_area_negocio_core();
        private readonly ILoggerManager _logger;

        public areacontroller()
        {
            _logger = new LoggerManager();
        }

        [HttpGet("Get/{Dependencia}/{SU}")]
        public IActionResult Get(String Dependencia, String su)
        {
            try
            {
                List<tbl_lista_areas> Query = Negocio.Get(Dependencia, su).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
          
        }
        [HttpGet("Get_Sub/{area}")]
        public IActionResult Get_Sub(String Area)
        {
            try
            {
                List<tbl_lista_areas> Query = Negocio.Get_Sub(Area).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
         
        }
        [HttpPost("Add")]
        public IActionResult Add([FromBody] tbl_area tbl_area)
        {
            try
            {
                List<Crudresponse> Query = Negocio.Add(tbl_area).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
          
        }
        [HttpPut("Update")]
        public IActionResult update([FromBody] tbl_area tbl_area)
        {
            try
            {
                List<Crudresponse> Query = Negocio.update(tbl_area).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("update", ex);
                return BadRequest();
            }
        
        }
        [HttpDelete("Delete")]
        public IActionResult delete([FromBody] tbl_area tbl_area)
        {
            try
            {
                List<Crudresponse> Query = Negocio.delete(tbl_area).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return BadRequest();
            }
         
        }
        [HttpGet("Get/Dropdown/{Dependencia}")]
        public IActionResult GetDrop(String Dependencia)
        {
            try
            {
                List<DropDownList> Query = Negocio.FillDrop(Dependencia).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
        
        }

        /**/
        [HttpGet("Get/Lista/{p_id}/{su}")]
        public IActionResult Get_areas(String p_id, String su)
        {
            try
            {
                List<tbl_areas_lista> Query = Negocio.Get_areas(p_id, su).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
         
        }
        [HttpGet("Get/Subreas/{p_id}")]
        public IActionResult Get_subareas(String p_id)
        {
            try
            {
                List<tbl_subareas_lista> Query = Negocio.Get_subareas(p_id).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
       
        }
        [HttpGet("Get/Areasubordinada/{p_id}")]
        public IActionResult Get_areas_sub(String p_id)
        {
            try
            {
                List<tbl_areasubordinada_lista> Query = Negocio.Get_areas_sub(p_id).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
       
        }
        [HttpPost("Add/Subarea")]
        public IActionResult Add_subarea([FromBody] tbl_subarea _Subarea)
        {
            try
            {
                return Ok(Negocio.Add_subareas(_Subarea).Response[0]);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
          
        }
        [HttpPut("Update/Subarea")]
        public IActionResult Update_documento([FromBody] tbl_subarea _Subarea)
        {
            try
            {
                return Ok(Negocio.Upd_subareas(_Subarea).Response[0]);
            }
            catch (Exception ex)
            {
                _logger.LogError("update", ex);
                return BadRequest();
            }
           
        }
        [HttpDelete("Delete/Subarea")]
        public IActionResult Delete_documento([FromBody] tbl_subarea _Subarea)
        {
            try
            {

                return Ok(Negocio.Delete_subareas(_Subarea).Response[0]);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return BadRequest();
            }
        }

        [HttpPost("Add/Subordinado")]
        public IActionResult Add_Subordinado([FromBody] tbl_area_subordinada _Subordinada)
        {
            try
            {
                return Ok(Negocio.Add_area_sub(_Subordinada).Response[0]);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
            
        }
        [HttpPut("Update/Subordinado")]
        public IActionResult Update_Subordinado([FromBody] tbl_area_subordinada _Subordinada)
        {
            try
            {
                return Ok(Negocio.Upd_area_sub(_Subordinada).Response[0]);
            }
            catch (Exception ex)
            {
                _logger.LogError("update", ex);
                return BadRequest();
            }
           
        }
        [HttpDelete("Delete/Subordinado")]
        public IActionResult Delete_Subordinado([FromBody] tbl_area_subordinada _Subordinada)
        {
            try
            {
                return Ok(Negocio.Delete_area_sub(_Subordinada).Response[0]);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return BadRequest();
            }
            
        }
        /**/
    }
}