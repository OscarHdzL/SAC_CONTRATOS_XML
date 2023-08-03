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
using Newtonsoft.Json;
using Negocio_SistemaAdquisiciones;

namespace Microservicios_SistemaDeAdquisiciones.Controllers
{
    [Produces("application/json")]
    [Route("dependencia")]
    [EnableCors("CorsPolicy")]
    public class DependenciaController : ControllerBase
    {
        #region Instancias
        //private tbl_servidorespublicos_negocio Negocio = new tbl_servidorespublicos_negocio();
        private readonly IConfiguration _configuration;
        #endregion
        public DependenciaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #region Operaciones
        
        [HttpGet("Get/Dropdown/{Instancia}")]
        public IActionResult GetDrop(String Instancia)
        {
            tbl_dependencia_negocio Negocio = new tbl_dependencia_negocio();
            List<DropDownList> Query = Negocio.FillDrop(Instancia).Response;
            return Ok(Query);
        }
        #endregion
    }
}
