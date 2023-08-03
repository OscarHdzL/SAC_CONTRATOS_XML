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
using Modelos.Modelos.Area;
using Modelos.Modelos.ServidoresPublicos;
using NegocioAdministracionContratos;
using Newtonsoft.Json;
using Modelos.Modelos.Contrato;
using Modelos.Interfaz;
using Utilidades.Log4Net;

namespace Servicios_AdminitracionContratos.Controllers
{

    [Produces("application/json")]
    [Route("Contratos")]
    [EnableCors("CorsPolicy")]
    public class Contratoscontrolles : ControllerBase
    {
        private sp_config_contratos_negocio Negocio = new sp_config_contratos_negocio();
        private readonly ILoggerManager _logger;

        public Contratoscontrolles()
        {
            _logger = new LoggerManager();
        }

        [HttpPost("Fase/Alta")]
        public IActionResult Get([FromForm]String contrato, [FromForm] String rc, [FromForm]String ProvContr, [FromForm]String LstPres)
        {
            try
            {

            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
            sp_config_contrato spconfig = JsonConvert.DeserializeObject<sp_config_contrato>(contrato);
            Responsable_contrato rescon = JsonConvert.DeserializeObject<Responsable_contrato>(rc);
           // List<PresupuestoContrato> lstPresupuestos = JsonConvert.DeserializeObject<List<PresupuestoContrato>>(LstPres);

            List<Guid> LST_ = JsonConvert.DeserializeObject<List<Guid>>(ProvContr);
            spconfig.p_opt = "creacion";
            Crudresponse Query = Negocio.Fase_0_ADD(spconfig).Response[0];
            rescon.Contrato = Query.msg;
            Negocio.responsables_contrato(rescon);
            Negocio.add_contrato_proveedor(LST_,Guid.Parse(Query.msg));
 
           // Negocio.comprometer_presupuesto_area(lstPresupuestos, Guid.Parse(Query.msg));
            return Ok(Query);
        }

        [HttpPost("Fase/Alta/v2")]
        public IActionResult Get2([FromForm]String contrato, [FromForm] String rc, [FromForm]String ProvContr, [FromForm]String LstPres, [FromForm]String areas_v2)
        {
            try
            {
                sp_config_contrato spconfig = JsonConvert.DeserializeObject<sp_config_contrato>(contrato);
                Responsable_contrato rescon = JsonConvert.DeserializeObject<Responsable_contrato>(rc);
                List<Guid> LST_ = JsonConvert.DeserializeObject<List<Guid>>(ProvContr);
                spconfig.p_opt = "creacion";
                Crudresponse Query = Negocio.Fase_0_ADD(spconfig).Response[0];
                rescon.Contrato = Query.msg;
                Negocio.responsables_contrato(rescon);
                Negocio.add_contrato_proveedor(LST_, Guid.Parse(Query.msg));
                // Negocio.comprometer_presupuesto_area(lstPresupuestos, Guid.Parse(Query.msg));
                Negocio.get_sp_json_contrato(Guid.Parse(Query.msg), areas_v2);
                //var resp = Negocio.AsignarE(asignacionArea);
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest(ex);
            }
            
        }

        [HttpPost("Fase/Update")]
        public IActionResult GetUpdate([FromForm]String contrato, [FromForm] String rc, String ProvContr, [FromForm]String LstPres, [FromForm]String areas_v2)
        {
            try
            {
                sp_config_contrato spconfig = JsonConvert.DeserializeObject<sp_config_contrato>(contrato);
                Responsable_contrato rescon = JsonConvert.DeserializeObject<Responsable_contrato>(rc);
                List<Guid> LST_ = JsonConvert.DeserializeObject<List<Guid>>(ProvContr);
                spconfig.p_opt = "actualiza";
                Crudresponse Query = Negocio.Fase_0_Update(spconfig).Response[0];
                rescon.Contrato = Query.msg;
                Negocio.responsables_contrato_update(rescon);
                Negocio.add_contrato_proveedor_update(LST_, Guid.Parse(Query.msg));
                Negocio.get_sp_json_contrato(Guid.Parse(Query.msg), areas_v2);
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("upd", ex);
                return BadRequest(ex);
            }

            
        }

        [HttpPost("Fase/formdata/{id}")]
        public IActionResult Getarchivo([FromBody]sp_config_contrato contrato, Guid id)
        {
            try
            {
                contrato.p_opt = "archivo";
                contrato.p_id = id;
                Crudresponse Query = Negocio.Fase_0_file(contrato).Response[0];
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest(ex);
            }
          
        }
        [HttpGet("tipocontrato")]
        public IActionResult Gettipocontrato()
        {
            try
            {
                List<tbl_tipo_contrato> Query = Negocio.tipocontrato().Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest(ex);
            }
         
        }
        [HttpGet("Set/Presupuesto/{idep}")]
        public IActionResult get_partidas_montos_area(Guid idep)
        {
            try
            {
                List<ContratoPresupuesto> Query = Negocio.get_partidas_montos_area(idep).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest(ex);
            }
           
        }
        [HttpGet("ConsultarServPub/{idep}")]
        public IActionResult ConsultarServPub(Guid idep)
        {
            try
            {
                List<tbl_servidor_publico> Query = Negocio.ConsultarServPub(idep).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest(ex);
            }
        
        }
        [HttpGet("ConsultarProveedor/{idproveedor}")]
        public IActionResult proveedoresdep(Guid idproveedor)
        {
            try
            {
                List<tbl_proveedor> Query = Negocio.proveedoresdep(idproveedor).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest(ex);
            }
          
        }
        [HttpGet("adicionalescontrato/{opt}")]
        public IActionResult adicionalescontrato(String opt)
        {
            try
            {
                List<DropDownList> Query = Negocio.get_adicionalescontrato(opt).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest(ex);
            }
         
        }
        [HttpGet("GetContratoporid/{contratoid}")]
        public IActionResult get_contratoporId(Guid contratoid)
        {
            try
            {
                List<sp_config_contrato_> Query = Negocio.get_contratoporId(contratoid).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest(ex);
            }
        
        }
        [HttpPost("ResponsableContrato/{contratoid}")]
        public IActionResult ResponsableContrato([FromBody]Responsable_contrato rc)
        {
            try
            {
                Boolean Query = Negocio.responsables_contrato(rc);
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest(ex);
            }
         
        }

        [HttpGet("Get/Lista/{Dependencia}")]
        public IActionResult Listado(string Dependencia)
        {
            try
            {
                List<tbl_contrato_list> Query = Negocio.Get_lista_contratos(Dependencia).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest(ex);
            }
        
        }
        [HttpPost("Set/Json")]
        public IActionResult Listado([FromForm]Guid p_tbl_contrato_id, [FromForm]String p_json_)
        {
            try
            {
                Negocio.get_sp_json_contrato(p_tbl_contrato_id, p_json_);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest(ex);
            }
     
        }
        [HttpGet("GET/Json/{p_tbl_contrato_id}")]
        public IActionResult Listado(Guid p_tbl_contrato_id)
        {
            try
            {
                return Ok(Negocio.get_sp_json_contrato(p_tbl_contrato_id).Response[0].cod);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest(ex);
            }
        
        }
        [HttpGet("GET/Json/Proveedores/{p_tbl_contrato_id}")]
        public IActionResult ListadoProveedores(Guid p_tbl_contrato_id)
        {
            try
            {
                return Ok(Negocio.get_contrProveedor(p_tbl_contrato_id));
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest(ex);
            }
           
        }
        [HttpGet("GET/Json/Responsable/{p_tbl_contrato_id}")]
        public IActionResult Responsablelst(Guid p_tbl_contrato_id)
        {
            try
            {
                return Ok(Negocio.sp_get_responsable_contrato_(p_tbl_contrato_id));
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest(ex);
            }
            
        }
        [HttpPost("Token")]
        public IActionResult updtoken(Guid p_id, String p_token)
        {
            try
            {
                return Ok(Negocio.upd_token_contrato(p_id, p_token));
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest(ex);
            }
           
        }
    }
}