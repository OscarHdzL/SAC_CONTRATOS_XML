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
using Newtonsoft.Json;
using Modelos.Interfaz;
using Utilidades.Log4Net;
using Solucion_Negocio;

namespace Servicio.Controllers
{

    [Produces("application/json")]
    [Route("ContratoProducto")]
    [EnableCors("CorsPolicy")]
    public class ContratoProductoController : ControllerBase
    {
        #region Instancias
        private tbl_sanciones_negocio Negocio = new tbl_sanciones_negocio();
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        #endregion
        public ContratoProductoController(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = new LoggerManager();
        }
        #region Operaciones
        [HttpGet]
        [Route("Get/Contrato/{id}")]
        public IActionResult GetListContrato(Guid id) 
        {
            try
            {
                //GetListContrato
                tbl_contrato_producto_negocio negocio = new tbl_contrato_producto_negocio();
                return Ok(negocio.GetListContrato(id).Response);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetListContrato", ex);
                return BadRequest();
            }
            
        }

        [HttpGet]
        [Route("Get/{id}")]
        public IActionResult Getunitario(Guid id)
        {

            try
            {
                tbl_contrato_producto_negocio negocio = new tbl_contrato_producto_negocio();
                return Ok(negocio.GetUnitario(id).Response);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetListContrato", ex);
                return BadRequest();
            }
            //GetListContrato
            
        }

        [HttpGet]
        [Route("Get/Dependencia/{tbl_dependencia_id}")]
        public IActionResult GetListDependencia(Guid tbl_dependencia_id)
        {
            try
            {
                tbl_contrato_producto_negocio negocio = new tbl_contrato_producto_negocio();
                return Ok(negocio.GetListDependencia(tbl_dependencia_id).Response);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetDependencia", ex);
                return BadRequest();
            }
            //GetListContrato
            
        }
        [HttpPost]
        [Route("Add")]
        public IActionResult add([FromBody] tbl_contrato_producto_add tbl_contrato_producto_add_)
        {
            try
            {
                tbl_contrato_producto_add_.p_opt = 2;
                tbl_contrato_producto_negocio negocio = new tbl_contrato_producto_negocio();
                return Ok(negocio.update(tbl_contrato_producto_add_).Response);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
            
        }

        [HttpPut]
        [Route("Put")]
        public IActionResult Put([FromBody] tbl_contrato_producto_add tbl_contrato_producto_add_)
        {
            try
            {
                tbl_contrato_producto_add_.p_opt = 3;
                tbl_contrato_producto_negocio negocio = new tbl_contrato_producto_negocio();
                return Ok(negocio.update(tbl_contrato_producto_add_).Response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Put", ex);
                return BadRequest();
            }
            
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                tbl_contrato_producto_add tbl_contrato_producto_add_ = new tbl_contrato_producto_add();
                tbl_contrato_producto_add_.p_opt = 4;
                tbl_contrato_producto_add_.p_id = id;


                tbl_contrato_producto_negocio negocio = new tbl_contrato_producto_negocio();
                return Ok(negocio.update(tbl_contrato_producto_add_).Response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Delete", ex);
                return BadRequest();
            }
            
        }

        #endregion




    }
}
