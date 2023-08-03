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
using Modelos.Modelos;
using Modelos.Modelos.Usuarios;
using Newtonsoft.Json;
using Negocio_SistemaAdquisiciones;
using SistemaDeAdquisiciones.Util.Seguridad;
using Modelos.Modelos.Dependencia;

namespace Microservicios_SistemaDeAdquisiciones.Controllers
{

    [Produces("application/json")]
    [Route("usuarios")]
    [EnableCors("CorsPolicy")]
    public class UsuarioController : ControllerBase
    {
        #region Instancias
        //private tbl_usuarios_negocio Negocio = new tbl_usuarios_negocio();
        private readonly IConfiguration _configuration;
        #endregion
        public UsuarioController(IConfiguration configuration)
        {
            _configuration = configuration;
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
            List<tbl_usuario> Query = Negocio.Consultar(new tbl_usuario() { usuario = correo.ToString() }).Response;
            return Ok(Query);
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
            TBL_Usuarios_Negocio Negocio = new TBL_Usuarios_Negocio();

            var resultado = Negocio.Add(usuario).Response;

            return Ok(resultado);
        }
        [HttpGet("Instancia/Get/{Instancia}")]
        public IActionResult Get(Guid Instancia)
        {
            TBL_Usuarios_Negocio Negocio = new TBL_Usuarios_Negocio();
            tbl_instancia_contrato_get Query = Negocio.Get(Instancia.ToString()).Response;
            return Ok(Query);
        }
    }
}
