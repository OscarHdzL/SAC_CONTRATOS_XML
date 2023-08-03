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
    [Route("PlanEntrega")]
    [EnableCors("CorsPolicy")]
    public class sp_plan_entregaController : ControllerBase
    {
        #region Instancias

        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;


        #endregion
        public sp_plan_entregaController(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = new LoggerManager();
        }
        #region Operaciones

        [HttpGet("Get")]
        public IActionResult Get()
        {
            try
            {
                Guid idpe = Guid.NewGuid();
                EstructuraPalnEntrega obj = new EstructuraPalnEntrega();
                obj.Header = new sp_plan_entrega_input();
                obj.Header.p_opt = 2;
                obj.Header.p_id = idpe;
                obj.Header.p_tbl_contrato_servidor_resp_id = new Guid("0e340548-42bd-11ea-9fcf-00155d1b3502");
                obj.Header.p_identificador = "amon06720";
                obj.Header.p_periodo = "Periodo";
                obj.Header.p_descripcion = "amon";
                obj.Header.p_fecha_ejecucion = DateTime.Now;
                obj.Header.p_activo = "1";
                obj.Header.p_tipo_entrega = "tipoentrega";
                Guid U1 = new Guid("3e5dfff1-4131-11ea-9fcf-00155d1b3502");
                Guid U2 = new Guid("4bded821-445b-11ea-9fcf-00155d1b3502");

                sp_plan_entrega_producto P1 = new sp_plan_entrega_producto();
                P1.p_opt = 2;
                P1.p_id = idpe;
                P1.p_tbl_contrato_producto_id = new Guid("3c0dd3ca-3ba2-11ea-9fcf-00155d1b3502");
                P1.p_tbl_ubicacion_plan_entrega_id = idpe;
                P1.p_estatus = "1";
                P1.p_cantidad = 100;
                P1.p_detalle_actividad = "thor";
                P1.p_tipo = "tipo";
                sp_plan_entrega_producto P2 = new sp_plan_entrega_producto();
                P2.p_opt = 2;
                P2.p_id = idpe;
                P2.p_tbl_contrato_producto_id = new Guid("3f1d2ca0-3efb-11ea-9fcf-00155d1b3502");
                P2.p_tbl_ubicacion_plan_entrega_id = idpe;
                P2.p_estatus = "1";
                P2.p_cantidad = 852;
                P2.p_detalle_actividad = "tyr";
                P2.p_tipo = "tipo tyr thor odin";

                sp_plan_entrega_producto P3 = new sp_plan_entrega_producto();
                P3.p_opt = 2;
                P3.p_id = idpe;
                P3.p_tbl_contrato_producto_id = new Guid("6d49d221-3ef7-11ea-9fcf-00155d1b3502");
                P3.p_tbl_ubicacion_plan_entrega_id = idpe;
                P3.p_estatus = "1";
                P3.p_cantidad = 100;
                P3.p_detalle_actividad = "thor";
                P3.p_tipo = "tipo";


                obj.UbicacionesProductos = new List<UbicacionProductos>();
                UbicacionProductos UP1 = new UbicacionProductos();
                UbicacionProductos UP2 = new UbicacionProductos();

                List<Guid> ListEjecutores = new List<Guid>();

                UP1.EjecutorPorUbicacion = Guid.NewGuid();
                UP2.EjecutorPorUbicacion = Guid.NewGuid();

                UP1.productos = new List<sp_plan_entrega_producto>();
                UP1.productos.Add(P1);
                UP1.productos.Add(P2);
                UP1.productos.Add(P3);
                UP1.tbl_ubicacion_id = new Guid("3e5dfff1-4131-11ea-9fcf-00155d1b3502");
                obj.UbicacionesProductos.Add(UP1);

                UP2.productos = new List<sp_plan_entrega_producto>();
                UP2.productos.Add(P1);
                UP2.productos.Add(P2);
                UP2.productos.Add(P3);
                UP2.tbl_ubicacion_id = new Guid("4bded821-445b-11ea-9fcf-00155d1b3502");
                obj.UbicacionesProductos.Add(UP2);



                return Ok(obj);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            

        }

        [HttpGet("Get/tipo/contrato/id/{idcontrato}")]
        public IActionResult Gettipo2(String idcontrato)
        {
            try
            {
                sp_plan_entrega_negocio Negocio = new sp_plan_entrega_negocio();
                Guid valor_guid = Guid.Parse(idcontrato);
                return Ok(Negocio.get_plan_entrega(valor_guid, "contrato").Response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }

        [HttpGet("Get/PE/detalle/idPlan/{idPlan}")]
        public IActionResult GetPlanEntregaDetallado(String idPlan)
        {
            try
            {
                sp_plan_entrega_negocio Negocio = new sp_plan_entrega_negocio();
                Guid valor_guid = Guid.Parse(idPlan);
                var response = Negocio.get_plan_entrega_detalle_producto(valor_guid);
                if (response.CurrentException == null)
                {
                    return Ok(response.Response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
           
        }

        [HttpGet("Get/PE/confirmados/{idcontrato}")]
        public IActionResult GetPEDrop(String idcontrato)
        {
            try
            {
                sp_plan_entrega_negocio Negocio = new sp_plan_entrega_negocio();
                Guid valor_guid = Guid.Parse(idcontrato);
                return Ok(Negocio.get_plan_entrega_select(valor_guid, "contrato").Response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }

        [HttpGet("Get/lista/tipo/contrato/id/{idcontrato}/{usuario}")]
        public IActionResult Get_PE_Lista(String idcontrato, string usuario)
        {
            try
            {
                sp_plan_entrega_negocio Negocio = new sp_plan_entrega_negocio();
                Guid valor_guid = Guid.Parse(idcontrato);
                var respose = Negocio.get_plan_entrega_lista(valor_guid, "contrato", usuario);
                if (respose.CurrentException == null)
                {
                    return Ok(respose.Response);
                }
                else
                {
                    return BadRequest(respose);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }

        [HttpGet("Get/lista/entrega/id/{entrega}")]
        public IActionResult Get_PE_Lista_FileName_Token(String entrega)
        {
            try
            {
                sp_plan_entrega_negocio Negocio = new sp_plan_entrega_negocio();
                var respose = Negocio._sp_download_filename(entrega);
                if (respose.CurrentException == null)
                {
                    return Ok(respose.Response);
                }
                else
                {
                    return BadRequest(respose);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }

        }


        [HttpGet("DeletedFile/contrato/id/{token_id}")]
        public IActionResult DeletedFile(String token_id)
        {
            try
            {
                sp_plan_entrega_negocio Negocio = new sp_plan_entrega_negocio();
                //Guid valor_guid = Guid.Parse(idcontrato);
                var respose = Negocio.deleted_file(token_id);
                return Ok(respose.Response);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return BadRequest();
            }
            
        }

        [HttpGet("DeletedFile/entrada/id/{token}")]
        public IActionResult DeletedFileArchivo(String token)
        {
            try
            {
                sp_plan_entrega_negocio Negocio = new sp_plan_entrega_negocio();
                
                var respose = Negocio.deleted_file_archivo(token);
                return Ok(respose.Response);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return BadRequest();
            }

        }

        [HttpGet("Get/Obligaciones/PlanEntrega/{tbl_plan_entrega_id_}/Producto/{tbl_producto_servicio_id}")]
        public IActionResult GetObligacionesProducto(Guid tbl_plan_entrega_id_, Guid tbl_producto_servicio_id)
        {
            try
            {
                sp_plan_entrega_negocio Negocio = new sp_plan_entrega_negocio();
                return Ok(Negocio.get_obligacion_producto(tbl_plan_entrega_id_, tbl_producto_servicio_id));
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }

        [HttpGet("Get/lista/Obligaciones/PlanEntrega/{tbl_plan_entrega_id_}/Producto/{tbl_producto_servicio_id}")]
        public IActionResult Get_ObligacionesProducto(Guid tbl_plan_entrega_id_, Guid tbl_producto_servicio_id)
        {
            try
            {
                sp_plan_entrega_negocio Negocio = new sp_plan_entrega_negocio();
                return Ok(Negocio.get_obligacion_producto_ubic(tbl_plan_entrega_id_, tbl_producto_servicio_id));
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }

        [HttpGet("Get/token/PlanEntrega/{tbl_plan_entrega_id_}")]
        public IActionResult Get_Token_cumplimiento(Guid tbl_plan_entrega_id_)
        {
            try
            {
                sp_plan_entrega_negocio Negocio = new sp_plan_entrega_negocio();
                return Ok(Negocio.get_token_cumplimiento(tbl_plan_entrega_id_.ToString()));
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }

        [HttpGet("Get/Proveedor/contrato/{id}")]
        public IActionResult Get_contrato(Guid id)
        {
            try
            {
                sp_plan_entrega_negocio Negocio = new sp_plan_entrega_negocio();

                return Ok(Negocio.get_plan_entrega_contrato(id));
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }


        [HttpGet("Get/Ejecutores/Ubicacion/{id}")]
        public IActionResult Get_ejecutores_ubicacion(Guid id)
        {
            try
            {
                sp_plan_entrega_negocio Negocio = new sp_plan_entrega_negocio();
                return Ok(Negocio.get_ubicacion_servidor(id));
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }


        [HttpPost("add")]
        public IActionResult addheader([FromBody] EstructuraPalnEntrega input)
        {
            try
            {
                int accion = 2;
                sp_plan_entrega_negocio Negocio = new sp_plan_entrega_negocio();
                List<Crudresponse> Query = Negocio.addheader(input, accion).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest(ex);
            }
           
        }

        [HttpPost("modify")]
        public IActionResult modifyHeader([FromBody] EstructuraPalnEntrega input)
        {
            try
            {
                int accion = 3;
                sp_plan_entrega_negocio Negocio = new sp_plan_entrega_negocio();
                List<Crudresponse> Query = Negocio.modifyHeader(input, accion).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("modify", ex);
                return BadRequest();
            }
            
        }

        [HttpPut("Ejecucion")]
        public IActionResult SetEjecucion()
        {
            return Ok();
        }

        [HttpPost("add/Doc/PE")]
        public IActionResult Add_ArchivoPE([FromBody] sp_tbl_archivosPE input)
        {
            try
            {
                sp_plan_entrega_negocio Negocio = new sp_plan_entrega_negocio();
                List<Crudresponse> Query = Negocio.Add_Archivo_PE(input).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("post", ex);
                return BadRequest();
            }
            
        }

        #endregion




    }
}
