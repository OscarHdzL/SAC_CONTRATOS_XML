using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Dependencia;
using Modelos.Modelos.Usuarios;
using Newtonsoft.Json;
using Solucion_Negocio;
using Solucion_Negocio.Util.Seguridad;
using Utilidades.Log4Net;

namespace Servicio.Controllers
{

    [Produces("application/json")]
    [Route("usuarios")]
    [EnableCors("CorsPolicy")]
    public class UsuarioController : ControllerBase
    {
        #region Instancias
        //private tbl_usuarios_negocio Negocio = new tbl_usuarios_negocio();
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        #endregion
        public UsuarioController(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = new LoggerManager();
        }
        #region Operaciones
        //[HttpGet]
        //public IActionResult Get()
        //{
        //    TBL_Usuarios_Negocio Negocio = new TBL_Usuarios_Negocio();
        //    List<TBL_USUARIOS> Query = Negocio.Consultar(new TBL_USUARIOS()).Response;
        //    return Ok(Query);
        //}


        [HttpGet("{correo}")]
        public ActionResult<string> Get(string correo)
        {
            TBL_Usuarios_Negocio Negocio = new TBL_Usuarios_Negocio();
            var Query = Negocio.Consultar(new tbl_usuario() { usuario = correo.ToString() });
            if (Query.CurrentException == null)
            {
                return Ok(Query.Response);
            }
            else
            {
                return BadRequest(Query);
            }
        }


        [HttpPost("VerificarPassword")]
        public ActionResult<tbl_usuario_verifica> Post([FromBody] verificacion_usuario Usuario)
        {

            string PasswordEncoded = seguridad.GetSHA1(Usuario.Password);

            Usuario.Password = PasswordEncoded;

            verificar_usuario_negocio Negocio = new verificar_usuario_negocio();
            tbl_usuario_verifica User = new tbl_usuario_verifica();
            List<tbl_usuario_verifica> Query = Negocio.Consultar(User, Usuario).Response;
            return Ok(Query);

        }


        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        #endregion

        [HttpPost("Add")]
        public ActionResult Add([FromBody] tbl_usuario_add usuario)
        {
            try
            {
                TBL_Usuarios_Negocio Negocio = new TBL_Usuarios_Negocio();

                var resultado = Negocio.Add(usuario).Response;

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
            
        }

        [HttpGet("Instancia/Get/{Instancia}")]
        public IActionResult Get(Guid Instancia)
        {
            try
            {
                TBL_Usuarios_Negocio Negocio = new TBL_Usuarios_Negocio();
                tbl_instancia_contrato_get Query = Negocio.Get(Instancia.ToString()).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
           
        }
    }
}
