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
    [Route("Apertura")]
    [EnableCors("CorsPolicy")]
    public class AperturaController : ControllerBase
    {
        #region Instancias
        
        private readonly IConfiguration _configuration;
        #endregion
        public AperturaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private tbl_apertura_negocio Negocio = new tbl_apertura_negocio();

        #region Operaciones

        [HttpPost("Add")]
        public IActionResult Add([FromBody] tbl_apertura _tbl_apertura)
        {
            List<Crudresponse> Query = Negocio.Add(_tbl_apertura).Response;
            return Ok(Query);
        }

        [HttpGet("Municipio/{id_estado}")]
        public IActionResult Get_Municipio(Guid id_estado)        
        {            
            List<DropDownList> LST = Negocio.Get_Municipio(id_estado).Response;
            return Ok(LST);
        }

        [HttpGet("DeclararDesierta/{Solicitud}")]
        public IActionResult DeclararDesierta(Guid Solicitud)
        {
            Crudresponse LST = Negocio.DeclararDesierta(Solicitud).Response;
            return Ok(LST);
        }

        #endregion
    }
}
