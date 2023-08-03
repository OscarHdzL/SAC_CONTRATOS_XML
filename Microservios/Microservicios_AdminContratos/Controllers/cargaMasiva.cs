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
using Negocio_AdminContratos;
using NegocioAdministracionContratos;
using Newtonsoft.Json;
 
namespace Servicios_AdminitracionContratos.Controllers
{

    [Produces("application/json")]
    [Route("CargaMasiva")]
    [EnableCors("CorsPolicy")]
    public class MasivasController : ControllerBase
    {
        private tbl_area_negocio_core Negocio = new tbl_area_negocio_core();

        [HttpPost("Area/{Dependencia}")]
        public IActionResult Area([FromForm]String CM,Guid Dependencia)
        {
            //Carga_Masiva_negocio Negocio = new Carga_Masiva_negocio();
            //List<String> Areas = JsonConvert.DeserializeObject<List<String>>(CM);
            //List<Crudresponse> CrudResponse = Negocio.CM_Area(Areas, Dependencia);
            //return Ok(CrudResponse);
            return Ok();
        }
   
    }
}