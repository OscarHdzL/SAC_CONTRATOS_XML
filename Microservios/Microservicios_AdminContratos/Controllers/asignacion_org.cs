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
    [Route("Asignacion")]
    [EnableCors("CorsPolicy")]
    public class asignacion_orgcontroller : ControllerBase
    {
        #region Instancias

        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        #endregion
        public asignacion_orgcontroller(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = new LoggerManager();
        }
        private AsignacionOrganizacional_negocio_core  Negocio = new AsignacionOrganizacional_negocio_core();
        #region Operaciones
        [HttpGet("Get/{tbl_instancia_id}")]
        public IActionResult get_lista_proveedores_by_id(Guid tbl_instancia_id)
        {
            try
            {
                List<estructura_lista_dep_areas> Query = Negocio.Get_lista_dependencias_areas(tbl_instancia_id.ToString()).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }

        [HttpGet("Get/Capitulos/{tipo}/{id_item}/{id_instancia}/{id_ejercicio}")]
        public IActionResult get_lista_cap_gastos_by_dep(string tipo,Guid id_item, Guid id_instancia, Guid id_ejercicio)
        {
            try
            {
                if (tipo == "dependencia")
                {
                    List<lista_cap_gastos_dep> Query = Negocio.lista_cap_gastos_dep(tipo, id_item.ToString(), id_instancia.ToString(), id_ejercicio.ToString()).Response;
                    return Ok(Query);
                }
                else
                {
                    List<lista_cap_gastos_areas> Query = Negocio.lista_cap_gastos_areas(tipo, id_item.ToString(), id_instancia.ToString(), id_ejercicio.ToString()).Response;
                    return Ok(Query);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
            
        }
        [HttpPost("add/Partida")]
        public IActionResult Update_Partida([FromBody] List<tbl_partida_area> _tbl_partida)
        {
            try
            {
                List<CrudresponseIdentificador> Query = Negocio.Add_Partida_Area(_tbl_partida).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
            
        }
        #endregion

    }
}