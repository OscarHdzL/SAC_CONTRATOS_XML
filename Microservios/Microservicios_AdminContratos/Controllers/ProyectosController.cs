using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Proyectos;
using NegocioAdministracionContratos;
using Utilidades.Log4Net;

namespace Microservicios_AdministracionDeContratos.Controllers
{
    [Produces("application/json")]
    [Route("Proyectos")]
    [EnableCors("CorsPolicy")]
    public class ProyectosController : ControllerBase
    {
        private tbl_proyectos_negocio Negocio = new tbl_proyectos_negocio();
        private readonly ILoggerManager _logger;

        public ProyectosController()
        {
            _logger = new LoggerManager();
        }

        [HttpGet("Get_Lista/{id_dep}")]
        public IActionResult Get_Sub(String id_dep)
        {
            try
            {
                List<tbl_lista_proyectos> Query = Negocio.Get_Lista(id_dep).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
         
        }
        [HttpGet("Get_Proyecto/{id_proyecto}")]
        public IActionResult Get_Persona(string id_proyecto)
        {
            try
            {
                List<tbl_proyectos> Query = Negocio.Get_Proyecto(id_proyecto).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
      
        }
        [HttpPost("Add")]
        public IActionResult Add([FromBody] tbl_proyectos _tbl_proyectos)
        {
            try
            {
                List<Crudresponse> Query = Negocio.Add(_tbl_proyectos).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
          
        }
        [HttpPut("Update")]
        public IActionResult update([FromBody] tbl_proyectos _tbl_proyectos)
        {
            try
            {
                List<Crudresponse> Query = Negocio.update(_tbl_proyectos).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("upd", ex);
                return BadRequest();
            }
      
        }
        [HttpDelete("Delete")]
        public IActionResult delete([FromBody] tbl_proyectos _tbl_proyectos)
        {
            try
            {
                List<Crudresponse> Query = Negocio.delete(_tbl_proyectos).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return BadRequest();
            }
          
        }
        [HttpGet("Get/Tipo_P/{id_ins}")]
        public IActionResult Get_Tipo_P(Guid id_ins)
        {
            try
            {
                List<DropDownList> Query = Negocio.Get_Tipo_P(id_ins.ToString()).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
       
        }
        [HttpGet("Get/Criticidad")]
        public IActionResult Get_Criticidad()
        {
            try
            {
                List<DropDownList> Query = Negocio.Get_Criticidad().Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
    
        }
        [HttpGet("Get/Tipo_Ejecucion")]
        public IActionResult Get_Tipo_Ejecucion()
        {
            try
            {
                List<DropDownList> Query = Negocio.Get_Tipo_Ejecucion().Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
       
        }
        [HttpGet("Get/Estatus_Proyecto")]
        public IActionResult Get_Estatus_Proyecto()
        {
            try
            {
                List<DropDownList> Query = Negocio.Get_Estatus_Proyecto().Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
       
        }
    }
}