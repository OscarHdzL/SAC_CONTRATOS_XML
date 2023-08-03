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
    [Route("Catalogos")]
    [EnableCors("CorsPolicy")]
    public class catalogoscontroller : ControllerBase
    {
        #region Instancias

        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        #endregion
        public catalogoscontroller(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = new LoggerManager();
        }
        private catalogos_negocio_core  Negocio = new catalogos_negocio_core();
        #region Operaciones
        [HttpPost("Add/TRiesgo")]
        public IActionResult Add_tipo_riesgo([FromBody] tbl_tipo_riesgo_add _tbl_tipo_riesgo_add)
        {
            try
            {
                _tbl_tipo_riesgo_add.p_opt = 2;
                List<Crudresponse> Query = Negocio.Add_tipo_riesgo(_tbl_tipo_riesgo_add).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
           
        }
        [HttpPut("Update/TRiesgo")]
        public IActionResult Update_tipo_riesgo([FromBody] tbl_tipo_riesgo_add _tbl_tipo_riesgo_add)
        {
            try
            {
                _tbl_tipo_riesgo_add.p_opt = 3;
                List<Crudresponse> Query = Negocio.Add_tipo_riesgo(_tbl_tipo_riesgo_add).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("upd", ex);
                return BadRequest();
            }
            
        }

        [HttpDelete("Delete/TRiesgo/{tbl_tipo_riesgo_id}")]
        public IActionResult Delete(Guid tbl_tipo_riesgo_id)
        {
            try
            {
                tbl_tipo_riesgo_add tbl_Tipo_Riesgo = new tbl_tipo_riesgo_add();
                tbl_Tipo_Riesgo.p_opt = 4;
                tbl_Tipo_Riesgo.p_id = tbl_tipo_riesgo_id;
                List<Crudresponse> Query = Negocio.Delete_tipo_riesgo(tbl_Tipo_Riesgo).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return BadRequest();
            }
            
        }

        [HttpPost("Add/TDocumento")]
        public IActionResult Add_tipo_documento([FromBody] tbl_tipo_documento_add _tbl_tipo_documento_add)
        {
            try
            {
                _tbl_tipo_documento_add.p_opt = 2;
                List<Crudresponse> Query = Negocio.Add_tipo_documento(_tbl_tipo_documento_add).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
           
        }
        [HttpPut("Update/TDocumento")]
        public IActionResult Update_tipo_documento([FromBody] tbl_tipo_documento_add _tbl_tipo_documento_add)
        {
            try
            {
                _tbl_tipo_documento_add.p_opt = 3;
                List<Crudresponse> Query = Negocio.Add_tipo_documento(_tbl_tipo_documento_add).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("upd", ex);
                return BadRequest();
            }
           
        }

        [HttpDelete("Delete/TDocumento/{tbl_tipo_documento_id}")]
        public IActionResult Delete_TDocumento(Guid tbl_tipo_documento_id)
        {
            try
            {
                tbl_tipo_documento_add tbl_Tipo_Documento = new tbl_tipo_documento_add();
                tbl_Tipo_Documento.p_opt = 4;
                tbl_Tipo_Documento.p_id = tbl_tipo_documento_id;
                List<Crudresponse> Query = Negocio.Delete_tipo_documento(tbl_Tipo_Documento).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return BadRequest();
            }
         
        }

        [HttpPost("Add/TProyecto")]
        public IActionResult Add_tipo_proyecto([FromBody] tbl_tipo_proyecto_add _tbl_tipo_proyecto_add)
        {
            try
            {
                _tbl_tipo_proyecto_add.p_opt = 2;
                List<Crudresponse> Query = Negocio.Add_tipo_proyecto(_tbl_tipo_proyecto_add).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
           
        }
        [HttpPut("Update/TProyecto")]
        public IActionResult Update_tipo_proyecto([FromBody] tbl_tipo_proyecto_add _tbl_tipo_proyecto_add)
        {
            try
            {
                _tbl_tipo_proyecto_add.p_opt = 3;
                List<Crudresponse> Query = Negocio.Add_tipo_proyecto(_tbl_tipo_proyecto_add).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Update", ex);
                return BadRequest();
            }
          
        }

        [HttpDelete("Delete/TProyecto/{tbl_tipo_proyecto_id}")]
        public IActionResult Delete_TProyecto(Guid tbl_tipo_proyecto_id)
        {
            try
            {
                tbl_tipo_proyecto_add tbl_Tipo_Proyecto = new tbl_tipo_proyecto_add();
                tbl_Tipo_Proyecto.p_opt = 4;
                tbl_Tipo_Proyecto.p_id = tbl_tipo_proyecto_id;
                List<Crudresponse> Query = Negocio.Delete_tipo_proyecto(tbl_Tipo_Proyecto).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return BadRequest();
            }
         
        }

        [HttpGet("Get/TRiesgo/{tbl_instancia_id}")]
        public IActionResult get_lista_tipo_riesgos(Guid tbl_instancia_id)
        {
            try
            {
                List<tbl_tipo_riesgo> Query = Negocio.Get_tipo_riesgo(tbl_instancia_id.ToString()).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
       
        }
        [HttpGet("Get/TDocumento/{tbl_instancia_id}")]
        public IActionResult get_lista_tipo_documentos(Guid tbl_instancia_id)
        {
            try
            {
                List<tbl_tipo_documento> Query = Negocio.Get_tipo_documento(tbl_instancia_id.ToString()).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
          
        }
        [HttpGet("Get/TProyecto/{tbl_instancia_id}")]
        public IActionResult get_lista_tipo_proyecto(Guid tbl_instancia_id)
        {
            try
            {
                List<tbl_tipo_proyecto> Query = Negocio.Get_tipo_proyecto(tbl_instancia_id.ToString()).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
          
        }
        [HttpGet("Get/Tipo_Ejecucion/{tbl_instancia_id}")]
        public IActionResult Get_Tipo_Ejecucion(Guid tbl_instancia_id)
        {
            try
            {
                var Query = Negocio.Get_tipo_ejecucion(tbl_instancia_id.ToString()).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
           
        }
        [HttpPost("Add/TEjecucion")]
        public IActionResult Add_tipo_ejecucion([FromBody] tbl_tipo_ejecucion_add _tbl_tipo_ejecucion_add)
        {
            try
            {
                _tbl_tipo_ejecucion_add.p_opt = 2;
                List<Crudresponse> Query = Negocio.Add_tipo_ejecucion(_tbl_tipo_ejecucion_add).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
            
        }
        [HttpPut("Update/TEjecucion")]
        public IActionResult Update_tipo_ejecucion([FromBody] tbl_tipo_ejecucion_add _tbl_tipo_ejecucion_add)
        {
            try
            {
                _tbl_tipo_ejecucion_add.p_opt = 3;
                List<Crudresponse> Query = Negocio.Add_tipo_ejecucion(_tbl_tipo_ejecucion_add).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("upd", ex);
                return BadRequest();
            }
           
        }

        [HttpDelete("Delete/TEjecucion/{tbl_tipo_ejecucion_id}")]
        public IActionResult Delete_tipo_ejecucion(Guid tbl_tipo_ejecucion_id)
        {
            try
            {
                tbl_tipo_ejecucion_add _tbl_tipo_ejecucion_add = new tbl_tipo_ejecucion_add();
                _tbl_tipo_ejecucion_add.p_opt = 4;
                _tbl_tipo_ejecucion_add.p_id = tbl_tipo_ejecucion_id;
                List<Crudresponse> Query = Negocio.Delete_tipo_ejecucion(_tbl_tipo_ejecucion_add).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return BadRequest();
            }
           
        }
        [HttpPost("Add/TContrato")]
        public IActionResult Add_tipo_contrato([FromBody] tbl_tipo_contrato_add _tbl_tipo_contrato_add)
        {
            try
            {
                _tbl_tipo_contrato_add.p_opt = 2;
                List<Crudresponse> Query = Negocio.Add_tipo_contrato(_tbl_tipo_contrato_add).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
        
        }
        [HttpPut("Update/TContrato")]
        public IActionResult Update_tipo_contrato([FromBody] tbl_tipo_contrato_add _tbl_tipo_contrato_add)
        {
            try
            {
                _tbl_tipo_contrato_add.p_opt = 3;
                List<Crudresponse> Query = Negocio.Add_tipo_contrato(_tbl_tipo_contrato_add).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("upd", ex);
                return BadRequest();
            }
         
        }

        [HttpDelete("Delete/TContrato/{tbl_tipo_contrato_id}")]
        public IActionResult Delete_TContrato(Guid tbl_tipo_contrato_id)
        {
            try
            {
                tbl_tipo_contrato_add tbl_Tipo_Contrato = new tbl_tipo_contrato_add();
                tbl_Tipo_Contrato.p_opt = 4;
                tbl_Tipo_Contrato.p_id = tbl_tipo_contrato_id;
                List<Crudresponse> Query = Negocio.Delete_tipo_contrato(tbl_Tipo_Contrato).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return BadRequest();
            }
          
        }
        [HttpGet("Get/TContrato/{tbl_instancia_id}")]
        public IActionResult get_lista_tipo_contrato(Guid tbl_instancia_id)
        {
            try
            {
                List<tbl_tipo_contrato> Query = Negocio.Get_tipo_contrato(tbl_instancia_id.ToString()).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
         
        }


        [HttpPost("Add/TPrioridad")]
        public IActionResult Add_tipo_prioridad([FromBody] tbl_tipo_prioridad_add _tbl_tipo_prioridad_add)
        {
            try
            {
                _tbl_tipo_prioridad_add.p_opt = 2;
                List<Crudresponse> Query = Negocio.Add_tipo_prioridad(_tbl_tipo_prioridad_add).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
         
        }
        [HttpPut("Update/TPrioridad")]
        public IActionResult Update_tipo_prioridad([FromBody] tbl_tipo_prioridad_add _tbl_tipo_prioridad_add)
        {
            try
            {
                _tbl_tipo_prioridad_add.p_opt = 3;
                List<Crudresponse> Query = Negocio.Add_tipo_prioridad(_tbl_tipo_prioridad_add).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("upd", ex);
                return BadRequest();
            }
       
        }

        [HttpDelete("Delete/TPrioridad/{tbl_tipo_prioridad_id}")]
        public IActionResult Delete_TPrioridad(Guid tbl_tipo_prioridad_id)
        {
            try
            {
                tbl_tipo_prioridad_add tbl_Tipo_Prioridad = new tbl_tipo_prioridad_add();
                tbl_Tipo_Prioridad.p_opt = 4;
                tbl_Tipo_Prioridad.p_id = tbl_tipo_prioridad_id;
                List<Crudresponse> Query = Negocio.Delete_tipo_prioridad(tbl_Tipo_Prioridad).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return BadRequest();
            }
          
        }
        [HttpGet("Get/TPrioridad/{tbl_instancia_id}")]
        public IActionResult get_lista_tipo_prioridad(Guid tbl_instancia_id)
        {
            try
            {
                List<tbl_tipo_prioridad> Query = Negocio.Get_tipo_prioridad(tbl_instancia_id.ToString()).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
        
        }

        [HttpGet]
        [Route("Get/UnidadesMedida")]
        public IActionResult GetUnidadesMedida()
        {
            try
            {
                var response = Negocio.Get_lista_unidad_medida();
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

        [HttpPost("Add/UnidadesMedida")]
        public IActionResult Add_unidad_medida([FromBody] tbl_unidad_medida_add unidad_medida_add)
        {
            try
            {
                unidad_medida_add.p_opt = 2;
                var response = Negocio.Add_unidad_medida(unidad_medida_add);

                if (response.CurrentException == null)
                {
                    if (response.Response[0].cod == "success")
                    {
                        return Ok(response.Response);
                    }
                    else
                    {
                        return BadRequest(response.Response);
                    }
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
            
        }
        [HttpPut("Update/UnidadesMedida")]
        public IActionResult Update_unidad_medida([FromBody] tbl_unidad_medida_add unidad_medida_add)
        {
            try
            {
                unidad_medida_add.p_opt = 3;
                var response = Negocio.Add_unidad_medida(unidad_medida_add);

                if (response.CurrentException == null)
                {
                    if (response.Response[0].cod == "success")
                    {
                        return Ok(response.Response);
                    }
                    else
                    {
                        return BadRequest(response.Response);
                    }
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("upd", ex);
                return BadRequest();
            }
           
        }

        [HttpDelete("Delete/UnidadesMedida/{tbl_unidad_medida_id}")]
        public IActionResult Delete_unidad_medida(Guid tbl_unidad_medida_id)
        {
            try
            {
                tbl_unidad_medida_add unidad_medida = new tbl_unidad_medida_add();
                unidad_medida.p_opt = 4;
                unidad_medida.p_id = tbl_unidad_medida_id;
                var response = Negocio.Delete_unidad_medida(unidad_medida);
                if (response.CurrentException == null)
                {
                    if (response.Response[0].cod == "success")
                    {
                        return Ok(response.Response);
                    }
                    else
                    {
                        return BadRequest(response.Response);
                    }
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return BadRequest();
            }
         

        }

        [HttpPost("Add/TInterlocutor")]
        public IActionResult Add_tipo_interlocutor([FromBody] tbl_tipo_interlocutor_add _tbl_tipo_interlocutor_add)
        {
            try
            {
                _tbl_tipo_interlocutor_add.p_opt = 2;
                List<Crudresponse> Query = Negocio.Add_tipo_interlocutor(_tbl_tipo_interlocutor_add).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
         
        }
        [HttpPut("Update/TInterlocutor")]
        public IActionResult Update_tipo_interlocutor([FromBody] tbl_tipo_interlocutor_add _tbl_tipo_interlocutor_add)
        {
            try
            {
                _tbl_tipo_interlocutor_add.p_opt = 3;
                List<Crudresponse> Query = Negocio.Add_tipo_interlocutor(_tbl_tipo_interlocutor_add).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
           
        }

        [HttpDelete("Delete/TInterlocutor/{tbl_tipo_interlocutor_id}")]
        public IActionResult Delete_TInterlocutor(Guid tbl_tipo_interlocutor_id)
        {
            try
            {
                tbl_tipo_interlocutor_add tbl_Tipo_Interlocutor = new tbl_tipo_interlocutor_add();
                tbl_Tipo_Interlocutor.p_opt = 4;
                tbl_Tipo_Interlocutor.id = tbl_tipo_interlocutor_id;
                List<Crudresponse> Query = Negocio.Delete_tipo_interlocutor(tbl_Tipo_Interlocutor).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return BadRequest();
            }
      
        }
        [HttpGet("Get/TInterlocutor")]
        public IActionResult get_lista_tipo_interlocutor()
        {
            try
            {
                List<lista_tipo_interlocutor> Query = Negocio.Get_tipo_interlocutor().Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
         
        }


        [HttpPost("Add/Procedimiento")]
        public IActionResult Add_procedimiento([FromBody] tbl_procedimiento_add procedimiento_add)
        {
            try
            {
                procedimiento_add.p_opt = 2;
                var Query = Negocio.Add_procedimiento(procedimiento_add);
                if (Query.CurrentException == null)
                {
                    if (Query.Response[0].cod == "success")
                    {
                        return Ok(Query.Response);
                    }
                    else
                    {
                        return BadRequest(Query.Response);
                    }
                }
                else
                {
                    return BadRequest(Query);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
         
            
        }
        [HttpPut("Update/Procedimiento")]
        public IActionResult Update_procedimiento([FromBody] tbl_procedimiento_add procedimiento_add)
        {
            try
            {
                procedimiento_add.p_opt = 3;
                var Query = Negocio.Add_procedimiento(procedimiento_add);
                if (Query.CurrentException == null)
                {
                    if (Query.Response[0].cod == "success")
                    {
                        return Ok(Query.Response);
                    }
                    else
                    {
                        return BadRequest(Query.Response);
                    }
                }
                else
                {
                    return BadRequest(Query);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("upd", ex);
                return BadRequest();
            }
         
        }

        [HttpDelete("Delete/Procedimiento/{tbl_procedimiento_id}")]
        public IActionResult Delete_procedimiento(Guid tbl_procedimiento_id)
        {
            try
            {
                tbl_procedimiento_add procedimiento = new tbl_procedimiento_add();
                procedimiento.p_opt = 4;
                procedimiento.p_id = tbl_procedimiento_id;
                var Query = Negocio.Delete_procedimiento(procedimiento);
                if (Query.CurrentException == null)
                {
                    if (Query.Response[0].cod == "success")
                    {
                        return Ok(Query.Response);
                    }
                    else
                    {
                        return BadRequest(Query.Response);
                    }
                }
                else
                {
                    return BadRequest(Query);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return BadRequest();
            }
          
        }
        [HttpGet("Get/Procedimiento")]
        public IActionResult get_lista_procedimiento()
        {
            try
            {
                var Query = Negocio.Get_procedimiento();
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
        #endregion
    }
}