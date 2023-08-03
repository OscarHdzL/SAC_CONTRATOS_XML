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
using Modelos.Modelos.Dependencia;
using NegocioAdministracionContratos;
using Newtonsoft.Json;
using Utilidades.Log4Net;

namespace Servicios_AdminitracionContratos.Controllers
{

    [Produces("application/json")]
    [Route("Dependencia")]
    [EnableCors("CorsPolicy")]
    public class dependenciacontroller : ControllerBase
    {
        private tbl_dependencia_negocio_core Negocio = new tbl_dependencia_negocio_core();
        private readonly ILoggerManager _logger;

        public dependenciacontroller()
        {
            _logger = new LoggerManager();
        }

        [HttpGet("Get/{Instancia}")]
        public IActionResult Get(Guid Instancia)
        {
            try
            {
                List<tbl_dependencia> Query = Negocio.Get(Instancia.ToString()).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }

        [HttpGet("Get/XPermisoUsuario/{Instancia}/{IdUsuario}")]
        public IActionResult GetXPermisoUsuario(Guid Instancia,Guid IdUsuario)
        {
            try
            {
                var Query = Negocio.GetXPermisoUsuario(Instancia.ToString(), IdUsuario.ToString());
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
                _logger.LogError("GetXPermisoUsuario", ex);
                return BadRequest(ex);
            }
        }

        [HttpGet("Get/Dropdown/Ciudad")]
        public IActionResult GetDrop()
        {
            try
            {
                List<DropDownList> Query = Negocio.FillDropC().Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }
        [HttpPost("Add")]
        public IActionResult Add([FromBody] tbl_dependencia tbl_dependencia)
        {
            try
            {
                List<Crudresponse> Query = Negocio.Add(tbl_dependencia).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
            
        }
        [HttpPut("Update")]
        public IActionResult update([FromBody] tbl_dependencia tbl_dependencia)
        {
            try
            {
                List<Crudresponse> Query = Negocio.update(tbl_dependencia).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("upd", ex);
                return BadRequest();
            }
            
        }
        [HttpDelete("Delete")]
        public IActionResult delete([FromBody] tbl_dependencia tbl_dependencia)
        {
            try
            {
                List<Crudresponse> Query = Negocio.delete(tbl_dependencia).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return BadRequest();
            }
            
        }
        [HttpGet("Get/Estados")]
        public IActionResult GetEstados()
        {
            try
            {
                List<DropDownList> Query = Negocio.get_tbl_estado().Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }
        [HttpGet("Get/Ciudades/Estado/{id}")]
        public IActionResult GetCiudadesEstados(Guid id)
        {
            try
            {
                List<DropDownList> Query = Negocio.get_tbl_estado_ciudad(id).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }
        [HttpGet("Get/Ejercicio")]
        public IActionResult GetEjercicios()
        {
            try
            {
                List<DropDownList> Query = Negocio.get_Ejercicios().Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }
        [HttpGet("Get_list_partida/{id_ejercicio}/{id_instancia}/{id_dependencia}")]
        public IActionResult Get_Lista_Partidas (Guid id_ejercicio, Guid id_instancia, Guid id_dependencia)
        {
            try
            {
                var Query = Negocio.Get_Lista_Partidas(id_ejercicio.ToString(), id_instancia.ToString(), id_dependencia.ToString());

                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }
        [HttpPost("Add/Partida")]
        public IActionResult Add_Partida([FromBody] tbl_partida _tbl_partida)
        {
            try
            {
                List<Crudresponse> Query = Negocio.Add_Partida(_tbl_partida).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
           
        }
        [HttpPut("Update/Partida")]
        public IActionResult Update_Partida([FromBody] List<tbl_partida_upd>  _tbl_partida)
        {
            try
            {
                List<CrudresponseIdentificador> Query = Negocio.Update_Partida(_tbl_partida).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("upd", ex);
                return BadRequest();
            }
            
        }
        [HttpDelete("Delete/Partida")]
        public IActionResult delete_partida([FromBody] tbl_partida _tbl_partida)
        {
            try
            {
                List<Crudresponse> Query = Negocio.delete_partida(_tbl_partida).Response;
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