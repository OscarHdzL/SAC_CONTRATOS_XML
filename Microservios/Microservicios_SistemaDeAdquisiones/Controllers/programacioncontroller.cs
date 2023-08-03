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
using Modelos.Modelos;
using Modelos.Modelos.ServidoresPublicos;
using Negocio_SistemaAdquisiciones;
using Newtonsoft.Json;


namespace Microservicios_SistemaDeAdquisiciones.Controllers
{

    [Produces("application/json")]
    [Route("ProgramacionEventos")]
    [EnableCors("CorsPolicy")]
    public class ProgramacionEventosController : ControllerBase
    {
        #region Instancias
        
        private readonly IConfiguration _configuration;
        #endregion
        public ProgramacionEventosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #region Operaciones
        
        [HttpPost("Agendar")]
        public IActionResult AgendarAdd([FromBody] ProgramacionEventosEntidad ProgramacionEventosEntidad_)
        {
            ProgramacionEventosNegocio programacionEventosNegocio_ = new ProgramacionEventosNegocio();
 
            return Ok(programacionEventosNegocio_.add(ProgramacionEventosEntidad_).Response);
        }
        [HttpPost("Token")]
        public IActionResult Token(String p_Token_,Guid p_id_)
        {
            ProgramacionEventosNegocio programacionEventosNegocio_ = new ProgramacionEventosNegocio();
            ProgramacionEventosEntidad Ent = new ProgramacionEventosEntidad();
            Ent.p_id = p_id_;
            Ent.p_token = p_Token_;
            Ent.p_action = 3;
            /*****/
            Ent.p_direccion = ""; Ent.p_Fecha_Evento = DateTime.Now; Ent.p_inclusion = DateTime.Now; Ent.p_tbl_ciudad_id = Guid.Empty; 
            Ent.p_tbl_estatus_programacion_id = Guid.Empty; Ent.p_tbl_solicitud_id = Guid.Empty;Ent.p_tbl_tipo_programacion_id = Guid.Empty;
            /*****/
            String value = programacionEventosNegocio_.add(Ent).Response[0].cod;
            return Ok(value);
        }
        [HttpPost("Estatus")]
        public IActionResult Estatus(Guid p_id_)
        {
            ProgramacionEventosNegocio programacionEventosNegocio_ = new ProgramacionEventosNegocio();
            ProgramacionEventosEntidad Ent = new ProgramacionEventosEntidad();
            Ent.p_id = p_id_;
            Ent.p_tbl_estatus_programacion_id = Guid.Parse("0210bf0d-5fe6-11ea-8324-00155d1b3502");
            Ent.p_action = 2;
            /*****/
            Ent.p_direccion = ""; Ent.p_Fecha_Evento = DateTime.Now; Ent.p_inclusion = DateTime.Now; Ent.p_tbl_ciudad_id = Guid.Empty;
            Ent.p_tbl_estatus_programacion_id = Guid.Empty; Ent.p_tbl_solicitud_id = Guid.Empty; Ent.p_tbl_tipo_programacion_id = Guid.Empty;
            Ent.p_token = "";
            /*****/
            String value = programacionEventosNegocio_.add(Ent).Response[0].cod;
            return Ok(value);
        }

        [HttpGet("TipoProgramacion")]
        public IActionResult get_sp_tbl_tipo_programacion()
        {
            ProgramacionEventosNegocio programacionEventosNegocio_ = new ProgramacionEventosNegocio();
            List<DropDownList> LST = programacionEventosNegocio_.get_sp_tbl_tipo_programacion().Response;
            return Ok(LST);
        }
        [HttpGet("Estado")]
        public IActionResult Estado()
        {
            ProgramacionEventosNegocio programacionEventosNegocio_ = new ProgramacionEventosNegocio();
            List<DropDownList> LST = programacionEventosNegocio_.get_sp_getestado().Response;
            return Ok(LST);
        }
        [HttpGet("Estado/{id}/Ciudad")]

        public IActionResult Estado(Guid id)
        {
            ProgramacionEventosNegocio programacionEventosNegocio_ = new ProgramacionEventosNegocio();
            List<DropDownList> LST = programacionEventosNegocio_.get_sp_getciudad(id).Response;
            return Ok(LST);
        }
        [HttpGet("Agendados/{id}/TipoEvento/{tipo}")]
        public IActionResult Agendados(Guid id,Guid tipo)
        {
            ProgramacionEventosNegocio programacionEventosNegocio_ = new ProgramacionEventosNegocio();
            List<tbl_agendados> LST = programacionEventosNegocio_.get_sp_eventos_agendados(id, tipo).Response;
            return Ok(LST);
        }

        [HttpGet("Get/Solicitud/{idSolicitud}/SiglaTipoEvento/{tipo}")]
        public IActionResult UltimoEventoActivo(String idSolicitud, String tipo)
        {
            ProgramacionEventosNegocio programacionEventosNegocio_ = new ProgramacionEventosNegocio();
            tbl_programacion LST = programacionEventosNegocio_.get_ultimo_evento_activo(idSolicitud, tipo).Response;
            return Ok(LST);
        }
        [HttpGet("Get/Tipo_Prog/{sigla}")]
        public IActionResult tipo_pro_x_sigla(String sigla)
        {
            ProgramacionEventosNegocio programacionEventosNegocio_ = new ProgramacionEventosNegocio();
            List<DropDownList> LST = programacionEventosNegocio_.tipo_pro_x_sigla(sigla).Response;
            return Ok(LST);
        }

        #endregion
    }
}
