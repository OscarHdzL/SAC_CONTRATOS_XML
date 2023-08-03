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
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.GestionExpediente;
using Newtonsoft.Json;
using Solucion_Negocio;
using Utilidades.Log4Net;

namespace Servicio.Controllers
{

    [Produces("application/json")]
    [Route("gestionexpediente")]
    [EnableCors("CorsPolicy")]
    public class GestionExpedienteController : ControllerBase
    {
        #region Instancias
        private tbl_sanciones_negocio Negocio = new tbl_sanciones_negocio();
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        #endregion
        public GestionExpedienteController(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = new LoggerManager();
        }
        #region Operaciones
        [HttpPost("Add")]
        public IActionResult Add(IFormFile archivo)
        {
            try
            {
                tbl_gestion_expediente_contrato_negocio Negocio = new tbl_gestion_expediente_contrato_negocio();

                tbl_gestion_expediente_contrato_add ExpedienteForm = new tbl_gestion_expediente_contrato_add();

                String OBJ = HttpContext.Request.Form["ExpedienteForm"].ToString();
                ExpedienteForm = JsonConvert.DeserializeObject<tbl_gestion_expediente_contrato_add>(OBJ);

                List<Crudresponse> Query = Negocio.add(ExpedienteForm).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
           
        }

        #endregion

       

 
    }
}
