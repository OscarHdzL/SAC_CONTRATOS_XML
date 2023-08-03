using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Modelos.Modelos;
using Negocio_SistemaAdquisiciones;
using Newtonsoft.Json;

namespace Microservicios_SistemaDeAdquisiciones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class CotizacionesController : ControllerBase
    {
        #region Instancias

        private readonly IConfiguration _configuration;
        #endregion
        public CotizacionesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #region Operaciones
        private cotizaciones_negocio Negocio = new cotizaciones_negocio();

        [HttpPost("Add")]
        public IActionResult UpdateSolicitud()        {

            List<string> proveedores = new List<string>();
            List<string> archivos_adjuntos = new List<string>();

            String OBJ_proveedores = HttpContext.Request.Form["OBJ_proveedores"].ToString();
            String OBJ_archivos_adjuntos = HttpContext.Request.Form["OBJ_archivos_adjuntos"].ToString();
            String solicitud = HttpContext.Request.Form["id_solicitud"].ToString();


            proveedores = JsonConvert.DeserializeObject<List<string>>(OBJ_proveedores);
            archivos_adjuntos = JsonConvert.DeserializeObject<List<string>>(OBJ_archivos_adjuntos);
            
            List<Crudresponse> Query = Negocio.Add(proveedores, archivos_adjuntos, solicitud).Response;
            return Ok(Query);
        }

        [HttpGet("Get_documentos_cotizacion/{id_solicitud}")]
        public IActionResult Get_documentos_cotizacion(String id_solicitud)
        {
            List<tbl_cotizacion_solicitud> Query = Negocio.Get_documentos_cotizacion(id_solicitud).Response;
            return Ok(Query);
        }

        [HttpPost("Add_cotizacion")]
        public IActionResult Add_cotizacion()
        {
            tbl_cotizacion_sol_crud cotizacion_sol = new tbl_cotizacion_sol_crud();
            String OBJ_cotizacion_sol = HttpContext.Request.Form["OBJ_cotizacion_sol"].ToString();
            cotizacion_sol = JsonConvert.DeserializeObject<tbl_cotizacion_sol_crud>(OBJ_cotizacion_sol);

            List<Crudresponse> Query = Negocio.Add_cotizacion(cotizacion_sol).Response;
            return Ok(Query);
        }
        [HttpPost("Delete_cotizacion/{id_cotizacion_solic}")]
        public IActionResult Delete_cotizacion(string id_cotizacion_solic)
        {
            List<Crudresponse> Query = Negocio.Delete_cotizacion(id_cotizacion_solic).Response;
            return Ok(Query);
        }

        #endregion

    }
}