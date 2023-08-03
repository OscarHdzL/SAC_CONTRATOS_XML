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
using Newtonsoft.Json;
using Solucion_Negocio;
using Utilidades.Log4Net;

namespace Servicio.Controllers
{

    [Produces("application/json")]
    [Route("obligaciones")]
    [EnableCors("CorsPolicy")]
    public class ObligacionesController : ControllerBase
    {
        #region Instancias
 
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        #endregion
        public ObligacionesController(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = new LoggerManager();
        }
 
        #region Operaciones

        [HttpGet]
        [Route("Contrato/List/{id}")]
        public IActionResult Get(String id)
        {
            try
            {
                tbl_obligacion_negocio Negocio = new tbl_obligacion_negocio();
                List<tbl_obligacion> Query = Negocio.Consultar(id).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }
        [HttpGet]
        [Route("Obligacion/{tbl_obligacion_id}")]
        public IActionResult GetObligacionId(Guid tbl_obligacion_id)
        {
            try
            {
                tbl_obligacion_negocio Negocio = new tbl_obligacion_negocio();
                List<tbl_obligacion_unitario> Query = Negocio.ConsultarId(tbl_obligacion_id).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }

        [HttpGet]
        [Route("Get/Periodos")]
        public IActionResult GetPeriodo()
        {
            try
            {
                tbl_obligacion_negocio Negocio = new tbl_obligacion_negocio();
                List<tbl_periodo> Query = Negocio.GetPeriodo().Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
           
        }

        [HttpPost("Add/Periodos")]
        public IActionResult Add_tipo_periodo([FromBody] tbl_tipo_periodo_add tbl_tipo_periodo_add)
        {
            try
            {
                tbl_tipo_periodo_add.p_opt = 2;
                tbl_obligacion_negocio Negocio = new tbl_obligacion_negocio();
                var respuesta = Negocio.Add_tipo_periodo(tbl_tipo_periodo_add);
                if (respuesta.CurrentException == null)
                {
                    return Ok(respuesta);
                }
                else
                {
                    return BadRequest(respuesta);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
            
        }

        [HttpPost("Update/Periodos")]
        public IActionResult Update_tipo_periodo([FromBody] tbl_tipo_periodo_add tbl_tipo_periodo_add)
        {
            try
            {
                tbl_tipo_periodo_add.p_opt = 3;
                tbl_obligacion_negocio Negocio = new tbl_obligacion_negocio();
                var respuesta = Negocio.Add_tipo_periodo(tbl_tipo_periodo_add);
                if (respuesta.CurrentException == null)
                {
                    return Ok(respuesta);
                }
                else
                {
                    return BadRequest(respuesta);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("updte", ex);
                return BadRequest(ex);
            }
            
        }

        [HttpDelete("Delete/Periodos/{tbl_tipo_periodo_id}")]
        public IActionResult Delete_tipo_periodo(Guid tbl_tipo_periodo_id)
        {
            try
            {
                tbl_tipo_periodo_add tbl_Tipo_Periodo = new tbl_tipo_periodo_add();
                tbl_Tipo_Periodo.p_opt = 4;
                tbl_Tipo_Periodo.p_id = tbl_tipo_periodo_id;
                tbl_obligacion_negocio Negocio = new tbl_obligacion_negocio();
                List<Crudresponse> Query = Negocio.Delete_tipo_periodo(tbl_Tipo_Periodo).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return BadRequest();
            }
            
        }


        [HttpGet]
        [Route("Contrato/List/Detalle/{id}")]
        public IActionResult GetDetalle(String id)
        {
            try
            {
                tbl_obligacion_negocio Negocio = new tbl_obligacion_negocio();
                List<tbl_obligacion_detalle> Query = Negocio.ConsultarDetalle(id).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }

        [HttpGet]
        [Route("ObligacionesxProducto/Contrato/{contrato}/Producto/{producto}")]
        public IActionResult GetObligacionesxProducto(String contrato, String producto)
        {
            try
            {
                tbl_obligacion_negocio Negocio = new tbl_obligacion_negocio();
                var Query = Negocio.ConsultarObligacionProducto(contrato, producto);
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
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
            
        }

        [HttpGet]
        [Route("Get/Tipooblig")]
        public IActionResult GetTipooblig()
        {
            try
            {
                tbl_obligacion_negocio Negocio = new tbl_obligacion_negocio();
                List<tbl_tipo_obligacion> Query = Negocio.GetTipooblig().Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }

        [HttpPost("Add/Obligaciones")]
        public IActionResult Add_tipo_obligacion([FromBody] tbl_tipo_obligacion_add tbl_tipo_obligacion_add)
        {
            try
            {
                tbl_tipo_obligacion_add.p_opt = 2;
                tbl_obligacion_negocio Negocio = new tbl_obligacion_negocio();
                var respuesta = Negocio.Add_tipo_obligacion(tbl_tipo_obligacion_add);
                if (respuesta.CurrentException == null)
                {
                    return Ok(respuesta);
                }
                else
                {
                    return BadRequest(respuesta);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
            
        }

        [HttpPost("Update/Obligaciones")]
        public IActionResult Update_tipo_obligacion([FromBody] tbl_tipo_obligacion_add tbl_tipo_obligacion_add)
        {
            try
            {
                tbl_tipo_obligacion_add.p_opt = 3;
                tbl_obligacion_negocio Negocio = new tbl_obligacion_negocio();
                var respuesta = Negocio.Add_tipo_obligacion(tbl_tipo_obligacion_add);
                if (respuesta.CurrentException == null)
                {
                    return Ok(respuesta);
                }
                else
                {
                    return BadRequest(respuesta);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("update", ex);
                return BadRequest();
            }
            
        }

        [HttpDelete("Delete/Obligaciones/{tbl_tipo_obligacion_id}")]
        public IActionResult Delete_tipo_obligacion(Guid tbl_tipo_obligacion_id)
        {
            try
            {
                tbl_tipo_obligacion_add tbl_Tipo_Obligacion = new tbl_tipo_obligacion_add();
                tbl_Tipo_Obligacion.p_opt = 4;
                tbl_Tipo_Obligacion.p_id = tbl_tipo_obligacion_id;
                tbl_obligacion_negocio Negocio = new tbl_obligacion_negocio();
                List<Crudresponse> Query = Negocio.Delete_tipo_obligacion(tbl_Tipo_Obligacion).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return BadRequest();
            }
            
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] tbl_obligacion_input_conatiner input)
        {
            try
            {
                int p_opt = 2;
                tbl_obligacion_negocio Negocio = new tbl_obligacion_negocio();
                var respuesta = Negocio.Update(input, p_opt);
                if (respuesta.CurrentException == null)
                {
                    return Ok(respuesta);
                }
                else
                {
                    return BadRequest(respuesta);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
            
            
        }

        [HttpPut]
        [Route("Put")]
        public IActionResult Put([FromBody] tbl_obligacion_input_conatiner input)
        {
            try
            {
                int p_opt = 3;
                tbl_obligacion_negocio Negocio = new tbl_obligacion_negocio();
                return Ok(Negocio.Update(input, p_opt));
            }
            catch (Exception ex)
            {
                _logger.LogError("put", ex);
                return BadRequest();
            }
            
        }


        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                tbl_obligacion_negocio Negocio = new tbl_obligacion_negocio();
                return Ok(Negocio.Delete(id));
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return BadRequest();
            }
            
        }

        [HttpGet]
        [Route("Get/Definicion")]
        public IActionResult example()
        {
            try
            {
                tbl_obligacion_input_conatiner container = new tbl_obligacion_input_conatiner();
                container.tbl_area_obligacion = new tbl_area_obligacion_input();
                container.tbl_link_obligacion = new tbl_link_obligacion_input();
                container.tbl_obligacion = new tbl_obligacion_input();
                container.tbl_responsable_obligacion = new tbl_responsable_obligacion_input();

                return Ok(container);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }

        [HttpGet]
        [Route("Get/TipoAplicacion")]
        public IActionResult TipoAplicacion()
        {
            try
            {
                tbl_obligacion_negocio Negocio = new tbl_obligacion_negocio();
                List<tbl_tipo_aplicacion> Query = Negocio.Get_tipo_aplicacion().Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }



        [HttpGet]
        [Route("Verificar/{idObligacion}")]
        public IActionResult Verificaroblig(Guid idObligacion)
        {
            try
            {
                tbl_obligacion_negocio Negocio = new tbl_obligacion_negocio();
                verificar_oblig Query = Negocio.VerificarObligacion(idObligacion).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }

        #endregion
        #region Reportes
        [HttpGet]
        [Route("GetReporteSanciones/Contrato/{contrato}/Periodo/{periodo}")]
        public IActionResult GetReporteSanciones(String contrato, String periodo)
        {
            try
            {
                tbl_obligacion_negocio Negocio = new tbl_obligacion_negocio();
                var Query = Negocio.Get_reporte_sanciones(contrato, periodo);
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
                _logger.LogError("GetReporteSanciones", ex);
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetReporteSancionesAgrupado/Contrato/{contrato}/Periodo/{periodo}")]
        public IActionResult GetReporteSancionesAgrupado(String contrato, String periodo)
        {
            try
            {
                tbl_obligacion_negocio Negocio = new tbl_obligacion_negocio();
                var Query = Negocio.Get_reporte_sanciones_agrupado(contrato, periodo);
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
                _logger.LogError("GetReporteSanciones", ex);
                return BadRequest(ex);
            }
        }

        #endregion




    }
}
