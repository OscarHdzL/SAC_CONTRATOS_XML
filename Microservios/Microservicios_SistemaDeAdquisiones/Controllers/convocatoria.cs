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
    [Route("Convocatoria")]
    [EnableCors("CorsPolicy")]
    public class convocatoriaController : ControllerBase
    {
        #region Instancias
        
        private readonly IConfiguration _configuration;
        #endregion
        public convocatoriaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #region Operaciones

        [HttpGet("Get/{id_solicitud}")]
        public IActionResult getModalidad(String id_solicitud)
        {
            tbl_convocatoria_negocio Negocio = new tbl_convocatoria_negocio();
            return Ok(Negocio.Get_convocatoria(id_solicitud).Response);
        }

        [HttpPost("Add")]
        public IActionResult Add([FromBody] tbl_convocatoria_add tbl_Convocatoria)
        {
            tbl_convocatoria_negocio Negocio = new tbl_convocatoria_negocio();
            return Ok(Negocio.Add(tbl_Convocatoria).Response[0]);
        }
        [HttpPut("Update")]
        public IActionResult Update([FromBody] tbl_convocatoria_add tbl_Convocatoria)
        {
            tbl_convocatoria_negocio Negocio = new tbl_convocatoria_negocio();
            return Ok(Negocio.Update(tbl_Convocatoria).Response[0]);
        }



        [HttpGet("Get/Areas/{id_dependencia}")]
        public IActionResult getAreas(Guid id_dependencia)
        {
            tbl_convocatoria_negocio Negocio = new tbl_convocatoria_negocio();
            return Ok(Negocio.Get_Areas(id_dependencia.ToString()).Response);
        }
        [HttpGet("Get/Servidor/{id_area}")]
        public IActionResult getSerPub(Guid id_area)
        {
            tbl_convocatoria_negocio Negocio = new tbl_convocatoria_negocio();
            return Ok(Negocio.Get_SerPub(id_area.ToString()).Response);
        }
        [HttpGet("Get/TipoObligaciones")]
        public IActionResult get_tipo_obligaciones()
        {
            tbl_convocatoria_negocio Negocio = new tbl_convocatoria_negocio();
            return Ok(Negocio.GetTipooblig().Response);
        }



        [HttpPost("Add/Obligacion")]
        public IActionResult Add_Obligacion([FromBody] tbl_obligacion_conv_add tbl_Obligacion_)
        {
            tbl_convocatoria_negocio Negocio = new tbl_convocatoria_negocio();
            return Ok(Negocio.Add_obligacion_conv(tbl_Obligacion_).Response[0]);
        }
        [HttpPut("Update/Obligacion")]
        public IActionResult Update_Obligacion([FromBody] tbl_obligacion_conv_add tbl_Obligacion_)
        {
            tbl_convocatoria_negocio Negocio = new tbl_convocatoria_negocio();
            return Ok(Negocio.Update_obligacion_conv(tbl_Obligacion_).Response[0]);
        }
        [HttpDelete("Delete/{id_convocatoria_obl}")]
        public IActionResult Delete_Obligacion(Guid id_convocatoria_obl)
        {
            tbl_convocatoria_negocio Negocio = new tbl_convocatoria_negocio();
            tbl_obligacion_conv_add nuevo = new tbl_obligacion_conv_add();
            nuevo.p_opt = 0;
            nuevo.p_id = id_convocatoria_obl;
            nuevo.p_tbl_tipo_obligacion_id = Guid.NewGuid();
            nuevo.p_tbl_estatus_obligacion_id = Guid.NewGuid();
            nuevo.p_obligacion = "";
            nuevo.p_inclusion = DateTime.Now;
            return Ok(Negocio.Delete_obligacion_conv(nuevo).Response[0]);
        }
        [HttpGet("Get/Obligaciones/{id_solicitud}")]
        public IActionResult get_conv_obligaciones(Guid id_solicitud)
        {
            tbl_convocatoria_negocio Negocio = new tbl_convocatoria_negocio();
            return Ok(Negocio.Get_convocatoria_obl(id_solicitud.ToString()).Response);
        }



        [HttpPost("Add/Responsable")]
        public IActionResult Add_Responsable([FromBody] tbl_responsable_convocatoria tbl_Responsable_)
        {
            tbl_convocatoria_negocio Negocio = new tbl_convocatoria_negocio();
            return Ok(Negocio.Add_responsable(tbl_Responsable_).Response[0]);
        }
        [HttpPut("Update/Responsable")]
        public IActionResult Update_Responsable([FromBody] tbl_responsable_convocatoria tbl_Responsable_)
        {
            tbl_convocatoria_negocio Negocio = new tbl_convocatoria_negocio();
            return Ok(Negocio.Update_responsable(tbl_Responsable_).Response[0]);
        }
        [HttpDelete("Delete/Responsable/{id_responsable_conv}")]
        public IActionResult Delete_Responsable(Guid id_responsable_conv)
        {
            tbl_convocatoria_negocio Negocio = new tbl_convocatoria_negocio();
            tbl_responsable_convocatoria nuevo = new tbl_responsable_convocatoria();
            nuevo.p_opt = 0;
            nuevo.p_id = id_responsable_conv;
            nuevo.p_tbl_servidor_publico_id = Guid.NewGuid();
            nuevo.p_tbl_convocatoria_id = Guid.NewGuid();
            nuevo.p_token = "";
            return Ok(Negocio.Delete_responsable(nuevo).Response[0]);
        }
        [HttpGet("Get/Responsable/{id_solicitud}")]
        public IActionResult get_conv_responsable(Guid id_solicitud)
        {
            tbl_convocatoria_negocio Negocio = new tbl_convocatoria_negocio();
            return Ok(Negocio.Get_convocatoria_lista_resp(id_solicitud.ToString()).Response);
        }



        [HttpPost("Add/Penalizacion")]
        public IActionResult Add_penalizacion([FromBody] tbl_convocatoria_penalizacion tbl_penalizacion_)
        {
            tbl_convocatoria_negocio Negocio = new tbl_convocatoria_negocio();
            return Ok(Negocio.Add_penalizacion(tbl_penalizacion_).Response[0]);
        }
        [HttpPut("Update/Penalizacion")]
        public IActionResult Update_penalizacion([FromBody] tbl_convocatoria_penalizacion tbl_penalizacion_)
        {
            tbl_convocatoria_negocio Negocio = new tbl_convocatoria_negocio();
            return Ok(Negocio.Update_penalizacion(tbl_penalizacion_).Response[0]);
        }
        [HttpDelete("Delete/Penalizacion/{id_penalizacion_conv}")]
        public IActionResult Delete_penalizacion(Guid id_penalizacion_conv)
        {
            tbl_convocatoria_negocio Negocio = new tbl_convocatoria_negocio();
            tbl_convocatoria_penalizacion nuevo = new tbl_convocatoria_penalizacion();
            nuevo.p_opt = 0;
            nuevo.p_id = id_penalizacion_conv;
            nuevo.p_tbl_convocatoria_id = Guid.NewGuid();
            nuevo.p_tbl_estatus_obligacion_id = Guid.NewGuid();
            nuevo.p_penalizacion = "";
            nuevo.p_porcentaje_deductiva = 0;
            nuevo.p_porcentaje_garantia = 0;
            nuevo.p_porcentaje_penalizacion = 0;
            nuevo.p_monto_garantia = "";
            return Ok(Negocio.Delete_penalizacion(nuevo).Response[0]);
        }
        [HttpGet("Get/Penalizacion/{id_solicitud}")]
        public IActionResult get_conv_penalizacion(Guid id_solicitud)
        {
            tbl_convocatoria_negocio Negocio = new tbl_convocatoria_negocio();
            return Ok(Negocio.Get_convocatoria_lista_penalizacion(id_solicitud.ToString()).Response);
        }



        [HttpPost("Add/Condicion")]
        public IActionResult Add_condicion([FromBody] tbl_convocatoria_condicion _Condicion)
        {
            tbl_convocatoria_negocio Negocio = new tbl_convocatoria_negocio();
            return Ok(Negocio.Add_condicion(_Condicion).Response[0]);
        }
        [HttpPut("Update/Condicion")]
        public IActionResult Update_condicion([FromBody] tbl_convocatoria_condicion _Condicion)
        {
            tbl_convocatoria_negocio Negocio = new tbl_convocatoria_negocio();
            return Ok(Negocio.Update_condicion(_Condicion).Response[0]);
        }
        [HttpDelete("Delete/Condicion/{id_conv_condicion}")]
        public IActionResult Delete_condicion(Guid id_conv_condicion)
        {
            tbl_convocatoria_negocio Negocio = new tbl_convocatoria_negocio();
            tbl_convocatoria_condicion nuevo = new tbl_convocatoria_condicion();
            nuevo.p_opt = 0;
            nuevo.p_id = id_conv_condicion;
            nuevo.p_tbl_convocatoria_id = Guid.NewGuid();
            nuevo.p_tbl_estatus_obligacion_id = Guid.NewGuid();
            nuevo.p_periodo = "";
            nuevo.p_condicion = "";
            return Ok(Negocio.Delete_condicion(nuevo).Response[0]);
        }
        [HttpGet("Get/Condicion/{id_solicitud}")]
        public IActionResult get_conv_condicion(Guid id_solicitud)
        {
            tbl_convocatoria_negocio Negocio = new tbl_convocatoria_negocio();
            return Ok(Negocio.Get_convocatoria_lista_condicion(id_solicitud.ToString()).Response);
        }



        [HttpPost("Add/Pago")]
        public IActionResult Add_pago([FromBody] tbl_convocatoria_pago _Pago)
        {
            tbl_convocatoria_negocio Negocio = new tbl_convocatoria_negocio();
            return Ok(Negocio.Add_pago(_Pago).Response[0]);
        }
        [HttpPut("Update/Pago")]
        public IActionResult Update_pago([FromBody] tbl_convocatoria_pago _Pago)
        {
            tbl_convocatoria_negocio Negocio = new tbl_convocatoria_negocio();
            return Ok(Negocio.Update_pago(_Pago).Response[0]);
        }
        [HttpDelete("Delete/Pago/{id_conv_pago}")]
        public IActionResult Delete_pago(Guid id_conv_pago)
        {
            tbl_convocatoria_negocio Negocio = new tbl_convocatoria_negocio();
            tbl_convocatoria_pago nuevo = new tbl_convocatoria_pago();
            nuevo.p_opt = 0;
            nuevo.p_id = id_conv_pago;
            nuevo.p_tbl_convocatoria_id = Guid.NewGuid();
            nuevo.p_tbl_estatus_obligacion_id = Guid.NewGuid();
            nuevo.p_condicion_pago = "";
            nuevo.p_metodo_pago = "";
            nuevo.p_tipo_pago = "";
            nuevo.p_porcentaje_cantidad = "";
            return Ok(Negocio.Delete_pago(nuevo).Response[0]);
        }
        [HttpGet("Get/Pago/{id_solicitud}")]
        public IActionResult get_conv_pago(Guid id_solicitud)
        {
            tbl_convocatoria_negocio Negocio = new tbl_convocatoria_negocio();
            return Ok(Negocio.Get_convocatoria_lista_pago(id_solicitud.ToString()).Response);
        }


        [HttpPost("Add/Criterio")]
        public IActionResult Add_criterio([FromBody] tbl_convocatoria_criterio _Criterio)
        {
            tbl_convocatoria_negocio Negocio = new tbl_convocatoria_negocio();
            return Ok(Negocio.Add_criterio(_Criterio).Response[0]);
        }
        [HttpPut("Update/Criterio")]
        public IActionResult Update_criterio([FromBody] tbl_convocatoria_criterio _Criterio)
        {
            tbl_convocatoria_negocio Negocio = new tbl_convocatoria_negocio();
            return Ok(Negocio.Update_criterio(_Criterio).Response[0]);
        }
        [HttpDelete("Delete/Criterio/{id_conv_pago}")]
        public IActionResult Delete_criterio(Guid id_conv_pago)
        {
            tbl_convocatoria_negocio Negocio = new tbl_convocatoria_negocio();
            tbl_convocatoria_criterio nuevo = new tbl_convocatoria_criterio();
            nuevo.p_opt = 0;
            nuevo.p_id = id_conv_pago;
            nuevo.p_tbl_convocatoria_id = Guid.NewGuid();
            nuevo.p_tbl_tipocriterio_id = Guid.NewGuid();
            nuevo.p_tbl_estatus_obligacion_id = Guid.NewGuid();
            nuevo.p_criterio = "";
            nuevo.p_evaluacion = "";
            return Ok(Negocio.Delete_criterio(nuevo).Response[0]);
        }
        [HttpGet("Get/Criterio/{id_solicitud}")]
        public IActionResult get_conv_criterio(Guid id_solicitud)
        {
            tbl_convocatoria_negocio Negocio = new tbl_convocatoria_negocio();
            return Ok(Negocio.Get_convocatoria_lista_criterios(id_solicitud.ToString()).Response);
        }
        [HttpGet("Get/TipoCriterio")]
        public IActionResult get_tipo_criterios()
        {
            tbl_convocatoria_negocio Negocio = new tbl_convocatoria_negocio();
            return Ok(Negocio.Get_convocatoria_tipo_criterios().Response);
        }


        [HttpPost("Add/Documento")]
        public IActionResult Add_documento([FromBody] tbl_documento_validacion tbl_Documento_)
        {
            tbl_convocatoria_negocio Negocio = new tbl_convocatoria_negocio();
            return Ok(Negocio.Add_Doc(tbl_Documento_).Response[0]);
        }
        [HttpPut("Update/Documento")]
        public IActionResult Update_documento([FromBody] tbl_documento_validacion tbl_Documento_)
        {
            tbl_convocatoria_negocio Negocio = new tbl_convocatoria_negocio();
            return Ok(Negocio.Update_Doc(tbl_Documento_).Response[0]);
        }
        [HttpDelete("Delete/Documento/{id_conv_doc}")]
        public IActionResult Delete_documento(Guid id_conv_doc)
        {
            tbl_convocatoria_negocio Negocio = new tbl_convocatoria_negocio();
            tbl_documento_validacion nuevo = new tbl_documento_validacion();
            nuevo.p_opt = 0;
            nuevo.p_id = id_conv_doc;
            nuevo.p_tbl_convocatoria_id = Guid.NewGuid();
            nuevo.p_tbl_tipo_documento_id = Guid.NewGuid();
            nuevo.p_justificacion = "";
            return Ok(Negocio.Delete_Doc(nuevo).Response[0]);
        }
        [HttpGet("Get/Documento/{id_solicitud}")]
        public IActionResult get_conv_documento(Guid id_solicitud)
        {
            tbl_convocatoria_negocio Negocio = new tbl_convocatoria_negocio();
            return Ok(Negocio.Get_convocatoria_lista_documento(id_solicitud.ToString()).Response);
        }
        [HttpGet("Get/TipoDoc/{id_instancia}")]
        public IActionResult get_tipo_criterios(Guid id_instancia)
        {
            tbl_convocatoria_negocio Negocio = new tbl_convocatoria_negocio();
            return Ok(Negocio.Get_convocatoria_tipo_documento(id_instancia.ToString()).Response);
        }

        #endregion
    }
}
