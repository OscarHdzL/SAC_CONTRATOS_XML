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
    [Route("ubicaciones")]
    [EnableCors("CorsPolicy")]
    public class UbicacionesController : ControllerBase
    {
        #region Instancias
        
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        #endregion
        public UbicacionesController(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = new LoggerManager();
        }
        #region Operaciones
        [HttpGet("Get/{tipo}/{parametro}")]
        public IActionResult Get(String tipo, Guid parametro)
        {
            try
            {
                if (tipo.ToLower() != "dependencia" && tipo.ToLower() != "servidor" && tipo.ToLower() != "unitario")
                {
                    return BadRequest();
                }
                else
                {
                    tbl_ubicacion_input input = new tbl_ubicacion_input();
                    input.idparameter = parametro;
                    input.tipo = tipo;
                    tbl_ubicacion_negocio Negocio = new tbl_ubicacion_negocio();
                    List<tbl_ubicacion_output> Query = Negocio.Consultar(input).Response;

                    return Ok(Query);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            
        }
        [HttpPost("Add")]
        public IActionResult Add([FromBody] UbicacionEjecutores_add tbl_ubicacion_add)
        {
            try
            {
                tbl_ubicacion_add.Ubicacion.p_opt = 2;
                tbl_ubicacion_add.Ubicacion.p_id = Guid.NewGuid();
                tbl_ubicacion_negocio Negocio = new tbl_ubicacion_negocio();
                var respuesta = Negocio.Add(tbl_ubicacion_add);
                if (respuesta.CurrentException == null)
                {
                    return Ok(respuesta.Response);
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

        ///MICROSERVICIO DE CORRECCION
        [HttpPut("Update/Ubicacion")]
        public IActionResult UpdateUbicacion([FromBody] tbl_ubicacion_add tbl_ubicacion_add)
        {
            try
            {
                tbl_ubicacion_negocio Negocio = new tbl_ubicacion_negocio();
                return Ok(Negocio.update_ubicacion(tbl_ubicacion_add).Response);
            }
            catch (Exception ex)
            {
                _logger.LogError("update", ex);
                return BadRequest();
            }
          
        }

        ///MICROSERVICIO DE CORRECCION --
        [HttpPost("Update/Ejecutor")]
        public IActionResult UpdateEjecutor([FromBody] ubicacion_ejecutor tbl_ubicacion_add)
        {
            try
            {
                tbl_ubicacion_negocio Negocio = new tbl_ubicacion_negocio();
                return Ok(Negocio.add_update_ubicacion_ejecutor(tbl_ubicacion_add).Response);
            }
            catch (Exception ex)
            {
                _logger.LogError("update", ex);
                return BadRequest();
            }
     
        
        }

        ///MICROSERVICIO DE CORRECCION
        [HttpDelete("Eliminar/Ejecutor")]
        public IActionResult EliminarEjecutor([FromBody] ubicacion_ejecutor tbl_ubicacion_add)
        {
            try
            {
                tbl_ubicacion_negocio Negocio = new tbl_ubicacion_negocio();
                return Ok(Negocio.eliminar_ubicacion_ejecutor(tbl_ubicacion_add).Response);
            }
            catch (Exception ex)
            {
                _logger.LogError("eliminar", ex);
                return BadRequest();
            }
          

        }

        //
        [HttpGet("Validar/{ubicacion}")]
        public IActionResult validar_ubicacion_ligada(Guid ubicacion)
        {
            try
            {
                tbl_ubicacion_negocio Negocio = new tbl_ubicacion_negocio();
                return Ok(Negocio.validar_ubicacion_ligada(ubicacion).Response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
          
        }



        [HttpGet("Get/example")]
        public IActionResult Getex()
        {
            try
            {
                UbicacionEjecutores_add parent = new UbicacionEjecutores_add();
                parent.Ubicacion = new tbl_ubicacion_add();
                parent.Ejecutores = new ubicacionEjecutores();
                return Ok(parent);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
        

        } 
        


        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                tbl_ubicacion_negocio Negocio = new tbl_ubicacion_negocio();
                return Ok(Negocio.Delete(id));
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return BadRequest();
            }
         
        }




        [HttpGet("Get/PE/{tbl_plan_entrega_id_}")]
        public IActionResult GetPE(Guid tbl_plan_entrega_id_)
        {
            try
            {
                tbl_ubicacion_negocio Negocio = new tbl_ubicacion_negocio();
                List<plan_entrega_ubicacion> Query = Negocio.ConsultarPlanEntrega(tbl_plan_entrega_id_).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
       
        }

        [HttpGet("Get/Ubiacaiones/PE/{tbl_plan_entrega_id_}")]
        public IActionResult GetPEUbiacaiones(Guid tbl_plan_entrega_id_, Guid tbl_usuario_id)
        {
            try
            {
                tbl_ubicacion_negocio Negocio = new tbl_ubicacion_negocio();
                List<plan_entrega_ubicacion> Query = Negocio.ConsultarPlanEntregaUbicaciones(tbl_plan_entrega_id_, tbl_usuario_id).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
         
        }

        [HttpGet("Get/lista/Ubiacaiones/PE/{tbl_plan_entrega_id_}/{tbl_usuario_id}")]
        public IActionResult Get_PE_Ubiacaiones(Guid tbl_plan_entrega_id_, Guid tbl_usuario_id)
        {
            try
            {
                tbl_ubicacion_negocio Negocio = new tbl_ubicacion_negocio();
                List<lista_plan_entrega_ubicacion> Query = Negocio.ConsultarPlanEntregaUbicaciones_token(tbl_plan_entrega_id_, tbl_usuario_id).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
        
        }

        [HttpGet("Get/lista/Ubicaciones/PE/{ubicacion}/{clave}")]
        public IActionResult Get_PE_Ubicaciones_Archivos(String ubicacion, string clave)
        {
            try
            {
                tbl_ubicacion_negocio Negocio = new tbl_ubicacion_negocio();
                List<File_name> Query = Negocio._sp_download_filename_ubicacion(ubicacion, clave).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }

        }




        [HttpGet("Delete/ReporteUbicacion/PE/{tbl_plan_entrega_id}/{tbl_ubicacion_id}")]
        public IActionResult Delete_PE_Ubiacaiones(Guid tbl_plan_entrega_id, Guid tbl_ubicacion_id)
        {
            try
            {
                tbl_ubicacion_negocio Negocio = new tbl_ubicacion_negocio();
                dynamic Query = Negocio.DeleteReportePlanEntregaUbicaciones_token(tbl_plan_entrega_id, tbl_ubicacion_id);
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return BadRequest();
            }
       
        }

        [HttpGet("Deleted/FileUbicacion/id/{token}")]
        public IActionResult DeletedFileArchivoUbicacion(String token)
        {
            try
            {
                tbl_ubicacion_negocio Negocio = new tbl_ubicacion_negocio();

                var respose = Negocio.deleted_file_archivo_ubicacion(token);
                return Ok(respose.Response);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return BadRequest();
            }

        }








        #endregion




    }
}
