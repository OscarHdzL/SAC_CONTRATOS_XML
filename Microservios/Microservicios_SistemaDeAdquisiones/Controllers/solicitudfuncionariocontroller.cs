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
    [Route("solicitudfuncionario")]
    [EnableCors("CorsPolicy")]
    public class SolicitudFuncionarioController : ControllerBase
    {
        #region Instancias
        
        private readonly IConfiguration _configuration;
        #endregion
        public SolicitudFuncionarioController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #region Operaciones

        [HttpGet("Get/Servidores/{instancia}")]
        public IActionResult GetDrop(String instancia)
        {
            solicitud_funcionario_negocio Negocio = new solicitud_funcionario_negocio();
            List<DropDownList> Query = Negocio.GetServidores(instancia).Response;
            return Ok(Query);
        }


        [HttpGet("Get/Funcionario/{Solicitud}/{TipoActa}/{Programacion}")]
        public IActionResult GetDrop(String Solicitud, String TipoActa, String Programacion)
        {
            solicitud_funcionario_negocio Negocio = new solicitud_funcionario_negocio();
            List<solicitud_funcionario> Query = Negocio.GetFuncionariosSolicitud(Solicitud, TipoActa, Programacion).Response;
            return Ok(Query);
        }

        [HttpPost("Add")]
        public IActionResult Add([FromBody] solicitud_funcionario_add funcionario)
        {
            solicitud_funcionario_negocio Negocio = new solicitud_funcionario_negocio();
            Crudresponse Query = Negocio.Add(funcionario).Response;
            return Ok(Query);
        }

        [HttpDelete("Delete/{idFuncionario}")]
        public IActionResult Add(String idFuncionario)
        {
            solicitud_funcionario_negocio Negocio = new solicitud_funcionario_negocio();
            solicitud_funcionario_add func = new solicitud_funcionario_add();
            func.p_opt = 4;
            func.p_id = idFuncionario;
            Crudresponse Query = Negocio.Delete(func).Response;
            return Ok(Query);
        }
        #endregion
    }
}
