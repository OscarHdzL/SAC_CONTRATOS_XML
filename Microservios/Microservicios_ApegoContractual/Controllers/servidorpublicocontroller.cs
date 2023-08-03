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
using Modelos.Modelos.ServidoresPublicos;
using Newtonsoft.Json;
using Solucion_Negocio;
using Utilidades.Log4Net;

namespace Servicio.Controllers
{

    [Produces("application/json")]
    [Route("servidorespublicos")]
    [EnableCors("CorsPolicy")]
    public class ServidorPublicoController : ControllerBase
    {
        #region Instancias
        private tbl_servidorespublicos_negocio Negocio = new tbl_servidorespublicos_negocio();
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        #endregion

        public ServidorPublicoController(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = new LoggerManager();
        }
        #region Operaciones

        [HttpGet("Get/{tipo}/{id}/contrato/{escontrato}")]
        public IActionResult GetServ(String tipo, String id, int escontrato)
        {
            try
            {
                tbl_servidorespublicos_negocio Negocio = new tbl_servidorespublicos_negocio();
                sp_get_vs_servidor_publico_input obj = new sp_get_vs_servidor_publico_input();
                obj.id = id;
                obj.tipo = tipo.ToLower();
                obj.Escontrato = Convert.ToBoolean(escontrato);
                if (obj.tipo == "contrato" && !obj.Escontrato)
                {
                    return BadRequest();
                }
                List<sp_get_vs_servidor_publico_contrato> Query = Negocio.get_vs_servidor_publico(obj).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }

        [HttpGet("Get/sigla/{id}/dependencia/{dep}")]
        public IActionResult GetServDep( String id, String dep)
        {
            try
            {
                tbl_servidorespublicos_negocio Negocio = new tbl_servidorespublicos_negocio();
                sp_get_vs_servidor_publico_input obj = new sp_get_vs_servidor_publico_input();
                obj.id = id;
                obj.tipo = "sigla";
                obj.Escontrato = false;
                if (obj.tipo == "contrato" && !obj.Escontrato)
                {
                    return BadRequest();
                }
                List<sp_get_vs_servidor_publico_> Query = Negocio.get_vs_servidor_publico_dep(obj).Response;
                return Ok(Query.Where(x => x.tbl_dependencia_id == new Guid(dep)).ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }

        [HttpGet("Get/sigla/{id}/Contrato/{contrato}")]
        public IActionResult GetServcontrato( String id, String contrato)
        {
            try
            {
                tbl_servidorespublicos_negocio Negocio = new tbl_servidorespublicos_negocio();
                sp_get_vs_servidor_publico_input obj = new sp_get_vs_servidor_publico_input();
                obj.id = id;
                obj.tipo = "sigla";
                obj.Escontrato = true;
                if (obj.tipo == "contrato" && !obj.Escontrato)
                {
                    return BadRequest();
                }
                List<sp_get_vs_servidor_publico_contrato> Query = Negocio.get_vs_servidor_publico(obj).Response;
                return Ok(Query.Where(x => x.tbl_contrato_id == new Guid(contrato)).ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
         
        }

        [HttpGet("Get/sigla/All/Contrato/{contrato}")]
        public IActionResult GetServcontratoall(String contrato)
        {
            try
            {
                tbl_servidorespublicos_negocio Negocio = new tbl_servidorespublicos_negocio();
                sp_get_vs_servidor_publico_input obj = new sp_get_vs_servidor_publico_input();
                obj.id = contrato;
                obj.tipo = "contrato";
                obj.Escontrato = true;
                if (obj.tipo == "contrato" && !obj.Escontrato)
                {
                    return BadRequest();
                }
                List<sp_get_vs_servidor_publico_contrato_c> Query = Negocio.get_vs_servidor_publico_contrato(obj).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
           
        }

        [HttpGet("Get/Contrato/{contrato}")]
        public IActionResult GetServcontrato_c(String contrato)
        {
            try
            {
                tbl_servidorespublicos_negocio Negocio = new tbl_servidorespublicos_negocio();
                sp_get_vs_servidor_publico_input obj = new sp_get_vs_servidor_publico_input();
                obj.id = contrato;
                obj.tipo = "contrato";
                obj.Escontrato = true;
                if (obj.tipo == "contrato" && !obj.Escontrato)
                {
                    return BadRequest();
                }
                List<sp_get_vs_servidor_publico_contrato_c> Query = Negocio.get_vs_servidor_publico_contrato(obj).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
         
        }

        [HttpGet("Get/{Dependencia}")]
        public IActionResult Get(String Dependencia)
        {
            try
            {
                tbl_servidorespublicos_negocio Negocio = new tbl_servidorespublicos_negocio();
                List<tbl_servidor_publico> Query = Negocio.Consultar(Dependencia).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
     
        }

        [HttpGet("Get/Estados")]
        public IActionResult GetEstados()
        {
            try
            {
                tbl_servidorespublicos_negocio Negocio = new tbl_servidorespublicos_negocio();
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
                tbl_servidorespublicos_negocio Negocio = new tbl_servidorespublicos_negocio();
                List<DropDownList> Query = Negocio.get_tbl_estado_ciudad(id).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
   
        }

        [HttpGet("Ejecutores/PlanEntrega/Contrato/{id}")]
        public IActionResult get_responsable_pe_contrato(Guid id)
        {
            try
            {
                tbl_servidorespublicos_negocio Negocio = new tbl_servidorespublicos_negocio();
                List<DropDownList> Query = Negocio.get_responsable_pe_contrato(id).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
      
        }

        [HttpGet("Get/Ejecutores/Dependencia/{Dependencia}")]
        public IActionResult get_ejecutores_dependencia(Guid Dependencia)
        {
            try
            {
                tbl_servidorespublicos_negocio Negocio = new tbl_servidorespublicos_negocio();
                List<DropDownList> Query = Negocio.get_ejecutores_dependencia(Dependencia).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
     
        }

        [HttpGet("Get/ResponsableContrato/Dependencia/{Dependencia}")]
        public IActionResult get_responsablescontrato(Guid Dependencia)
        {
            try
            {
                tbl_servidorespublicos_negocio Negocio = new tbl_servidorespublicos_negocio();
                List<DropDownList> Query = Negocio.get_responsablescontrato(Dependencia).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
   
        }
        
        [HttpGet("Get/ResponsablesUbicaciones/Dependencia/{Dependencia}")]
        public IActionResult ResponsablesUbicaciones(Guid Dependencia)
        {
            try
            {
                tbl_servidorespublicos_negocio Negocio = new tbl_servidorespublicos_negocio();
                List<DropDownList> Query = Negocio.get_ResponsablesUbicaciones(Dependencia).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
       
        }

        [HttpGet("Get/Dropdown/{Dependencia}")]
        public IActionResult GetDrop(String Dependencia)
        {
            try
            {
                tbl_servidorespublicos_negocio Negocio = new tbl_servidorespublicos_negocio();
                List<DropDownList> Query = Negocio.FillDrop(Dependencia).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
        
        }

        [HttpGet("Get/ListaByRolApego/{Dependencia}")]
        public IActionResult GetListaByRolApego(string Dependencia)
        {
            try
            {
                tbl_responsable_apego_contrato_negocio Negocio = new tbl_responsable_apego_contrato_negocio();
                List<tbl_servidor_publico> Query = Negocio.ConsultarByRol(Dependencia).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
      
        }

        [HttpGet("Get/ListaByRolEjecutorPEntrega/{Dependencia}")]
        public IActionResult GetListaByRolEjecutorPEntrega(string Dependencia)
        {
            try
            {
                tbl_servidorespublicos_negocio Negocio = new tbl_servidorespublicos_negocio();
                List<tbl_servidor_publico> Query = Negocio.ConsultarByRolEjecutorPEntrega(Dependencia).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
        
        }

        [HttpGet("Get/ListaByRolEjecutorPMonitoreo/{Dependencia}")]
        public IActionResult GetListaByRolEjecutorPMonitoreo(string Dependencia)
        {
            try
            {
                tbl_servidorespublicos_negocio Negocio = new tbl_servidorespublicos_negocio();
                List<tbl_servidor_publico> Query = Negocio.ConsultarByRolEjecutorPMonitoreo(Dependencia).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
        
        }

        [HttpGet("Get/ServPub/{IdServidor}")]
        public IActionResult GetServidor(String IdServidor)
        {
            try
            {
                tbl_servidorespublicos_negocio Negocio = new tbl_servidorespublicos_negocio();
                tbl_servidor_publico Query = Negocio.ConsultarServidor(IdServidor).Response;
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
