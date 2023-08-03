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
    [Route("Licitante")]
    [EnableCors("CorsPolicy")]
    public class LicitanteController : ControllerBase
    {
        #region Instancias
        
        private readonly IConfiguration _configuration;
        #endregion
        public LicitanteController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #region Operaciones
        
        [HttpGet("Get/Proveedores/{Instancia}")]
        public IActionResult GetProveedores(String Instancia)
        {
            licitante_negocio Negocio = new licitante_negocio();
            List<tbl_proveedor> Query = Negocio.GetProveedores_instancia(Instancia).Response;
            return Ok(Query);
        }

        [HttpGet("Valida/{RFC}/{Solicitud}")]
        public IActionResult ValidaLicitante(String RFC, String Solicitud)
        {
            licitante_negocio Negocio = new licitante_negocio();
            Crudresponse Query = Negocio.ValidaLicitante(RFC, Solicitud).Response;
            return Ok(Query);
        }

        [HttpGet("Get/Solicitud/{Solicitud}")]
        public IActionResult getLicitante(String Solicitud)
        {
            licitante_negocio Negocio = new licitante_negocio();
            List<tbl_licitante> Query = Negocio.GetLicitantes_Solicitud(Solicitud).Response;
            return Ok(Query);
        }

        [HttpPost("Add")]
        public IActionResult Add([FromBody] tbl_licitante_add licitante)
        {
            licitante_negocio Negocio = new licitante_negocio();
            Crudresponse Query = Negocio.AddLicitante(licitante).Response;
            return Ok(Query);
        }


        [HttpDelete("Delete/{IdLicitante}")]
        public IActionResult Delete(String IdLicitante)
        {
            licitante_negocio Negocio = new licitante_negocio();
            tbl_licitante_add lic = new tbl_licitante_add();
            lic.p_opt = 4;
            lic.p_id = IdLicitante;
            List<Crudresponse> Query = Negocio.DeleteLicitante(lic).Response;
            return Ok(Query);
        }



        //////PROPUESTA
        ///
        [HttpPost("Propuesta/Add")]
        public IActionResult PropuestaAdd()
        {
            tbl_licitante_propuesta_add _propuesta = new tbl_licitante_propuesta_add();

            String OBJ_Propuesta = HttpContext.Request.Form["LicitantePropuestaObj"].ToString();
            _propuesta = JsonConvert.DeserializeObject<tbl_licitante_propuesta_add>(OBJ_Propuesta);

            licitante_negocio Negocio = new licitante_negocio();
            Crudresponse Query = Negocio.AddPropuestaLicitante(_propuesta).Response;
            return Ok(Query);
        }

        [HttpPost("Propuesta_Cotizacion/Add")]
        public IActionResult Propuesta_C_Add()
        {
            tbl_licitante_propuesta_add _propuesta = new tbl_licitante_propuesta_add();

            String OBJ_Propuesta = HttpContext.Request.Form["LicitantePropuestaObj"].ToString();
            _propuesta = JsonConvert.DeserializeObject<tbl_licitante_propuesta_add>(OBJ_Propuesta);

            licitante_negocio Negocio = new licitante_negocio();
            Crudresponse Query = Negocio.AddPropuestaLicitante_C(_propuesta).Response;
            return Ok(Query);
        }

        [HttpGet("Get/Propuestas/Solicitud/{Solicitud}/{TipoPropuesta}")]
        public IActionResult getLicitante_Propuesta(String Solicitud, String TipoPropuesta)
        {
            licitante_negocio Negocio = new licitante_negocio();
            List<licitante_propuesta> Query = Negocio.GetLicitantes_Propuesta_Solicitud(Solicitud, TipoPropuesta).Response;
            return Ok(Query);
        }

        #endregion
    }
}
