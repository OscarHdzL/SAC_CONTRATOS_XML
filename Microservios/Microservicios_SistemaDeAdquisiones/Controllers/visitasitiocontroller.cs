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
    [Route("visitasitio")]
    [EnableCors("CorsPolicy")]
    public class VisitaSitioController : ControllerBase
    {
        #region Instancias
        
        private readonly IConfiguration _configuration;
        #endregion
        public VisitaSitioController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #region Operaciones
        
        [HttpPost("Add")]
        public IActionResult Add()
        {
            visita_sitio_negocio Negocio = new visita_sitio_negocio();

            tbl_control_evento_add control = new tbl_control_evento_add();

            String OBJ_Control = HttpContext.Request.Form["data"].ToString();
            control = JsonConvert.DeserializeObject<tbl_control_evento_add>(OBJ_Control);

            Crudresponse Query = Negocio.Add(control).Response;
            return Ok(Query);
        }
        #endregion
    }
}
