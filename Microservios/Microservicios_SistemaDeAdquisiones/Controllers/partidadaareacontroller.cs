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
    [Route("partidaarea")]
    [EnableCors("CorsPolicy")]
    public class PartidaAreaController : ControllerBase
    {
        #region Instancias
        
        private readonly IConfiguration _configuration;
        #endregion
        public PartidaAreaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #region Operaciones
        
        [HttpGet("Get/Dropdown/{area}/{ejercicio}")]
        public IActionResult GetDrop(String area, String ejercicio)
        {
            tbl_partida_area_negocio Negocio = new tbl_partida_area_negocio();
            List<DropDownList> Query = Negocio.FillDrop(area, ejercicio).Response;
            return Ok(Query);
        }

        [HttpGet("Get/MontoSeleccionado/{area}/{partida}")]
        public IActionResult MontoSeleccionado_area_partida(String area, String partida)
        {
            tbl_partida_area_negocio Negocio = new tbl_partida_area_negocio();
            monto_seleccionado_area_partida Query = Negocio.MontoSeleccionado_area_partida(area, partida).Response;
            return Ok(Query);
        }
        #endregion
    }
}
