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
    [Route("Modalidad")]
    [EnableCors("CorsPolicy")]
    public class ModalidadController : ControllerBase
    {
        #region Instancias
        
        private readonly IConfiguration _configuration;
        #endregion
        public ModalidadController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #region Operaciones
        
        [HttpPost("Asignar")]
        public IActionResult add([FromBody] sp_modalidad sp_modalidad_in)
        {
            Modalidad_negocio Negocio = new Modalidad_negocio();
            return Ok(Negocio.add(sp_modalidad_in).Response);
        }

        [HttpGet("Parcial/{id}")]
        public IActionResult Parcial(Guid id)
        {
            Modalidad_negocio Negocio = new Modalidad_negocio();
            return Ok(Negocio.SolParcial(id).Response);
        }



        [HttpGet("Catalogos/{tipo}")]
        public IActionResult getcat(String tipo)
        {
            Modalidad_negocio Negocio = new Modalidad_negocio();
            return Ok(Negocio.get_modalidad_catalogos(tipo).Response);
        }

        [HttpGet("Validar/{sol}")]
        public IActionResult Validar(Guid sol)
        {
            Modalidad_negocio Negocio = new Modalidad_negocio();
            return Ok(Negocio.Validar(sol).Response[0].cod);
        }


        [HttpGet("Get/Solicitud/{Solicitud}")]
        public IActionResult getModaidad(String Solicitud)
        {
            Modalidad_negocio Negocio = new Modalidad_negocio();
            return Ok(Negocio.get_modalidad_solicitud(Solicitud));
        }
        #endregion
    }
}
