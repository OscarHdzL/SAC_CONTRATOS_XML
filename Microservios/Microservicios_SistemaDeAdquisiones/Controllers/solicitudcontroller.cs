using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
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
    [Route("solicitud")]
    [EnableCors("CorsPolicy")]
    public class SolicitudController : ControllerBase
    {
        #region Instancias

        private readonly IConfiguration _configuration;
        #endregion
        public SolicitudController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #region Operaciones

        [HttpGet("Get/TipoSolicitud/Dropdown")]
        public IActionResult GetTipoSolicitudDrop()
        {
            tbl_tipo_solicitud_negocio Negocio = new tbl_tipo_solicitud_negocio();
            List<DropDownList> Query = Negocio.FillDrop().Response;
            return Ok(Query);
        }
        [HttpGet("Get/Fuente_financiamiento/Dropdown/{id_dep}")]
        public IActionResult GetFuente_financiamiento(string id_dep)
        {
            tbl_solicitud_negocio Negocio = new tbl_solicitud_negocio();
            List<DropDownList> Query = Negocio.GetFuente_financiamiento(id_dep).Response;
            return Ok(Query);
        }

        [HttpGet("Get/TipoContratoSolicitud/Dropdown/{instancia}")]
        public IActionResult GetTipoContratoSolicitudDrop(String instancia)
        {
            tbl_tipo_contrato_solicitud_negocio Negocio = new tbl_tipo_contrato_solicitud_negocio();
            List<DropDownList> Query = Negocio.FillDrop(instancia).Response;
            return Ok(Query);
        }

        [HttpGet("Get/area_turnar/Dropdown/{id_dep}")]
        public IActionResult Get_area_turnar(string id_dep)
        {
            tbl_solicitud_negocio Negocio = new tbl_solicitud_negocio();
            List<DropDownList> Query = Negocio.Get_area_turnar(id_dep).Response;
            return Ok(Query);
        }

        [HttpGet("Get/{RolUsuario}/{Estatus}")]
        public IActionResult GetSolicitudes(String RolUsuario, String Estatus)
        {
            tbl_solicitud_negocio Negocio = new tbl_solicitud_negocio();
            List<tbl_solicitud> Query = Negocio.getSolicitudes_rolusuario_estatus(RolUsuario, Estatus).Response;
            return Ok(Query);
        }
        
        [HttpGet("Get/Contador/{RolUsuario}/{Estatus}")]
        public IActionResult GetContadorSolicitudes(String RolUsuario, String Estatus)
        {
            tbl_solicitud_negocio Negocio = new tbl_solicitud_negocio();
            contador_solicitud Query = Negocio.get_contador_Solicitudes_rolusuario_estatus(RolUsuario, Estatus).Response;
            return Ok(Query);
        }

        [HttpGet("Get/{idSolicitud}")]
        public IActionResult GetSolicitud(String idSolicitud)
        {
            tbl_solicitud_negocio Negocio = new tbl_solicitud_negocio();
            tbl_solicitud Query = Negocio.getSolicitud(idSolicitud).Response;
            return Ok(Query);
        }

        [HttpGet("Get_lista_tipo_documento/{id_instancia}")]
        public IActionResult Get_lista_tipo_documento(String id_instancia)
        {
            tbl_solicitud_negocio Negocio = new tbl_solicitud_negocio();
            List<tbl_tipo_documento> Query = Negocio.Get_lista_tipo_documento(id_instancia).Response;
            return Ok(Query);
        }

        [HttpGet("Get_Documentos_Solicitud/{id_solicitud}")]
        public IActionResult Get_docts_Solicitud(String id_solicitud)
        {
            tbl_solicitud_negocio Negocio = new tbl_solicitud_negocio();
            List<tbl_documento_adjunto_solicitud> Query = Negocio.Get_docts_Solicitud(id_solicitud).Response;
            return Ok(Query);
        }
        
        [HttpGet("Get_Solicitud_suficiencia_det/{id_solicitud}")]
        public IActionResult Get_Solicitud_suficiencia_det(String id_solicitud)
        {
            tbl_solicitud_negocio Negocio = new tbl_solicitud_negocio();
            tbl_solicitud_suficiencia Query = Negocio.Get_Solicitud_suficiencia_det(id_solicitud).Response;
            return Ok(Query);
        }

        [HttpGet("Get_Solicitud_Est_Merc/{id_solicitud}")]
        public IActionResult Get_Solicitud_Est_Merc(String id_solicitud)
        {
            tbl_solicitud_negocio Negocio = new tbl_solicitud_negocio();
            tbl_solicitud_estudio_mercado Query = Negocio.Get_Solicitud_Est_Merc(id_solicitud).Response;
            return Ok(Query);
        }

        [HttpGet("Get_tipo_dictamen/{id_dependencia}")]
        public IActionResult Get_tipo_dictamen(String id_dependencia)
        {
            tbl_solicitud_negocio Negocio = new tbl_solicitud_negocio();
            List<tbl_tipo_dictamen> Query = Negocio.Get_tipo_dictamen(id_dependencia).Response;
            return Ok(Query);
        }

        [HttpPost("add_suficiencia")]
        public IActionResult add_suficiencia([FromBody] tbl_suficiencia_add entidad)
        {
            tbl_solicitud_negocio Negocio = new tbl_solicitud_negocio();
            List<Crudresponse> Query = Negocio.add_suficiencia(entidad).Response;
            return Ok(Query);
        }

        [HttpPost("add_estudio_mercado")]
        public IActionResult add_estudio_mercado([FromBody] tbl_estudio_mercado entidad)
        {
            tbl_solicitud_negocio Negocio = new tbl_solicitud_negocio();
            List<Crudresponse> Query = Negocio.add_estudio_mercado(entidad).Response;
            return Ok(Query);
        }

        [HttpPost("add_dictamen_solicitud")]
        public IActionResult add_dictamen_solicitud([FromBody] tbl_dictamen entidad)
        {
            tbl_solicitud_negocio Negocio = new tbl_solicitud_negocio();
            List<Crudresponse> Query = Negocio.add_dictamen_solicitud(entidad).Response;
            return Ok(Query);
        }

        [HttpPost("update_sol_metodo/{parametro}/{id_sol}/{variable}")]
        public IActionResult update_sol_metodo(string parametro, string id_sol, string variable)
        {
            tbl_solicitud_negocio Negocio = new tbl_solicitud_negocio();
            List<Crudresponse> Query = Negocio.update_sol_metodo(parametro, id_sol, variable).Response;
            return Ok(Query);
        }

        [HttpPost("Add_dcto_adj")]
        public IActionResult AddDocumentoAdjunto([FromBody] tbl_solicitud_documento_adjunto _tbl_solicitud_documento_adjunto)
        {
            tbl_solicitud_negocio Negocio = new tbl_solicitud_negocio();
            List<Crudresponse> Query = Negocio.AddDocumentoAdjunto(_tbl_solicitud_documento_adjunto).Response;
            return Ok(Query);
        }
        [HttpPost("Delete_dcto_adj/{id_docto}")]
        public IActionResult Delete_dcto_adj(string id_docto)
        {
            tbl_solicitud_negocio Negocio = new tbl_solicitud_negocio();
            List<Crudresponse> Query = Negocio.Delete_dcto_adj(id_docto).Response;
            return Ok(Query);
        }


        [HttpPost("Add")]
        public IActionResult AddSolicitud()
        {

            sp_solicitud_en SolicitudForm = new sp_solicitud_en();
            List<Responsables_Solicitud> ResponsablesForm = new List<Responsables_Solicitud>();
            List<tbl_partida_solicitud_contrato_temp_add> PartidasTemp = new List<tbl_partida_solicitud_contrato_temp_add>();
            List<tbl_contrato_area_add> ContratoTemp = new List<tbl_contrato_area_add>();

            String OBJ_Solicitud = HttpContext.Request.Form["SolicitudFront"].ToString();
            SolicitudForm = JsonConvert.DeserializeObject<sp_solicitud_en>(OBJ_Solicitud);


            SolicitudForm.p_json_pres = HttpContext.Request.Form["json_pres"];


            String OBJ_Responsables = HttpContext.Request.Form["Responsables"].ToString();
            ResponsablesForm = JsonConvert.DeserializeObject<List<Responsables_Solicitud>>(OBJ_Responsables);

            ////Se comenta haasta que se finalice lo de presupuestos en core
            //String OBJ_PartidasTemp = HttpContext.Request.Form["PartidasTemp"].ToString();
            //PartidasTemp = JsonConvert.DeserializeObject<List<tbl_partida_solicitud_contrato_temp_add>>(OBJ_PartidasTemp);

            //String OBJ_ContratoAreaTemp = HttpContext.Request.Form["AreasContratoTemp"].ToString();
            //ContratoTemp = JsonConvert.DeserializeObject<List<tbl_contrato_area_add>>(OBJ_ContratoAreaTemp);


            tbl_solicitud_negocio Negocio = new tbl_solicitud_negocio();
            List<CrudresponseIdentificador> Query = Negocio.Guardar(SolicitudForm, ResponsablesForm, PartidasTemp, ContratoTemp).Response;
            return Ok(Query);
        }
        [HttpPut("Update/AprRch")]
        public IActionResult UpdateSolicitud([FromBody] sp_solicitud_en _tbl_solicitud)
        {
            tbl_solicitud_negocio Negocio = new tbl_solicitud_negocio();
            List<Crudresponse> Query = Negocio.Update_sol(_tbl_solicitud).Response;
            return Ok(Query);
        }


        [HttpPost("Update")]
        public IActionResult UpdateSolicitud()
        {

            sp_solicitud_en SolicitudForm = new sp_solicitud_en();
            
            String OBJ_Solicitud = HttpContext.Request.Form["SolicitudObject"].ToString();
            SolicitudForm = JsonConvert.DeserializeObject<sp_solicitud_en>(OBJ_Solicitud);

            tbl_solicitud_negocio Negocio = new tbl_solicitud_negocio();
            List<Crudresponse> Query = Negocio.Update(SolicitudForm).Response;
            return Ok(Query);
        }

        [HttpGet("get_partidas_montos_area_unitario/{Dep}/{area}/{cap}")]
        public IActionResult get_partidas_montos_area_unitario(string Dep, string area, string cap)
        {
            tbl_solicitud_negocio Negocio = new tbl_solicitud_negocio();
            List<Crudresponse> Query = Negocio.get_partidas_montos_area_unitario(Dep, area, cap).Response;
            return Ok(Query);
        }

        #endregion
    }
}
