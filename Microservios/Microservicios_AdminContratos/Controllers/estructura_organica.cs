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
    [Route("Estructura")]
    [EnableCors("CorsPolicy")]
    public class estructura_organicacontroller : ControllerBase
    {
        #region Instancias

        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        #endregion
        public estructura_organicacontroller(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = new LoggerManager();
        }
        private estructura_organica_negocio  Negocio = new estructura_organica_negocio();

        #region Operaciones
        [HttpGet("Get/{tbl_instancia_id}")]
        public IActionResult get_estructura(Guid tbl_instancia_id)
        {
            try
            {
                List<estructura_organica_core> Query = Negocio.Get_lista_estructura_organica(tbl_instancia_id.ToString()).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }


        [HttpPost("Add/CapituloG/Dependencia")]
        public IActionResult Add_Partida([FromBody] tbl_capitulo_gasto_dependencia _tbl_partida)
        {
            try
            {
                List<CrudresponseIdentificador> Query = Negocio.Add_partida_dependencia(_tbl_partida).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
            
        }
        [HttpPut("Update/CapituloG/Dependencia")]
        public IActionResult Update_Partida([FromBody] List<tbl_capitulo_gasto_dependencia> _tbl_partida)
        {
            try
            {
                List<CrudresponseIdentificador> Query = Negocio.Update_partida_dependencia(_tbl_partida).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("upd", ex);
                return BadRequest();
            }
           
        }
        [HttpDelete("Delete/CapituloG/Dependencia")]
        public IActionResult Dlete_Partida([FromBody] tbl_capitulo_gasto_dependencia _tbl_partida)
        {
            try
            {
                List<CrudresponseIdentificador> Query = Negocio.Delete_partida_dependencia(_tbl_partida).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return BadRequest();
            }
           
        }



        [HttpPost("Add/CapituloG/Area")]
        public IActionResult Add_Partida_Area([FromBody] List<tbl_capitulo_gasto_area> _tbl_partida)
        {
            try
            {
                List<CrudresponseIdentificador> Query = Negocio.Add_partida_area(_tbl_partida).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
           
        }


        [HttpPost("Add/CapituloG/Subarea")]
        public IActionResult Add_Partida_Subarea([FromBody] List<tbl_capitulo_gasto_subarea> _tbl_partida)
        {
            try
            {
                List<CrudresponseIdentificador> Query = Negocio.Add_partida_subarea(_tbl_partida).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
           
        }

        [HttpPost("Add/CapituloG/Subordinada")]
        public IActionResult Add_Partida_AreaSub([FromBody] List<tbl_capitulo_gasto_area_subordinada> _tbl_partida)
        {
            try
            {
                List<CrudresponseIdentificador> Query = Negocio.Add_partida_area_sub(_tbl_partida).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
           
        }



        [HttpGet("Get/Lista/Capitulos/{id_item}/{id_ejercicio}")]
        public IActionResult get_capitulos_g_dep(Guid id_item, Guid id_ejercicio)
        {
            try
            {
                List<lista_capitulos_gastos> Query = Negocio.get_lista_capitulos_gastos(id_item.ToString(), id_ejercicio.ToString()).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }
        [HttpGet("Get/Lista/Capitulos/Area/{id_dependencia}/{id_area}/{id_ejercicio}")]
        public IActionResult get_capitulos_g_area(Guid id_dependencia, Guid id_area, Guid id_ejercicio)
        {
            try
            {
                List<lista_capitulos_gastos_area_estructura> Query = Negocio.get_lista_capitulos_gastos_area(id_dependencia.ToString(), id_area.ToString(), id_ejercicio.ToString()).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }
        [HttpGet("Get/Lista/Capitulos/Subarea/{id_dependencia}/{id_subarea}/{id_ejercicio}")]
        public IActionResult get_capitulos_g_subarea(Guid id_dependencia, Guid id_subarea, Guid id_ejercicio)
        {
            try
            {
                List<lista_capitulos_gastos_subarea_estructura> Query = Negocio.get_lista_capitulos_gastos_subarea(id_dependencia.ToString(), id_subarea.ToString(), id_ejercicio.ToString()).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
           
        }
        [HttpGet("Get/Lista/Capitulos/Subordinada/{id_dependencia}/{id_area_sub}/{id_ejercicio}")]
        public IActionResult get_capitulos_g_area_sub(Guid id_dependencia, Guid id_area_sub, Guid id_ejercicio)
        {
            try
            {
                List<lista_capitulos_gastos_area_subordinada> Query = Negocio.get_lista_capitulos_gastos_area_sub(id_dependencia.ToString(), id_area_sub.ToString(), id_ejercicio.ToString()).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }

        [HttpGet("Get/Lista/Info/{opt}/{id_capitulo_gasto}/{id_item}")]
        public IActionResult Get_informacion(int opt, Guid id_capitulo_gasto, Guid id_item)
        {
            try
            {
                List<lista_info_capitulo_gasto> Query = Negocio.get_lista_info(opt, id_capitulo_gasto.ToString(), id_item.ToString()).Response;
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