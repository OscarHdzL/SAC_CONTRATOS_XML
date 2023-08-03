using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelos.Modelos;
using NegocioAdministracionContratos;
using System.Security.Cryptography;
using System.Text;
using Modelos.Interfaz;
using Utilidades.Log4Net;

namespace Microservicios_AdministracionDeContratos.Controllers
{

    [Produces("application/json")]
    [Route("Usuarios")]
    [EnableCors("CorsPolicy")]
    public class UsuariosController : Controller
    {
        private tbl_usuarios_negocio_core Negocio = new tbl_usuarios_negocio_core();
        private readonly ILoggerManager _logger;

        public UsuariosController()
        {
            _logger = new LoggerManager();
        }

        [HttpGet("Get_Lista/{Instancia}")]
        public IActionResult Get_Lista(string Instancia)
        {
            try
            {
                List<tbl_usuarios_list> Query = Negocio.Get_Lista(Instancia).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
          
        }
        [HttpGet("Get/Dropdown/Rol")]
        public IActionResult GetDrop()
        {
            try
            {
                List<DropDownList> Query = Negocio.FillDrop().Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
         
        }

        [HttpGet("Get/DependenciasAsignadas/Usuario/{usuario_id}")]
        public IActionResult GetDependenciasAsignadas(string usuario_id)
        {
            try
            {
                var Query = Negocio.GetDependenciasAsignadas(usuario_id);
                if (Query.CurrentException == null)
                {
                    return Ok(Query.Response);
                }
                else
                {
                    return BadRequest(Query);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
          
        }

        [HttpGet("Get/exist/{opt}/{param}")]
        public IActionResult exist(int opt, string param)
        {
            try
            {
                List<Crudresponse> Query = Negocio.Exist(opt, param).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
  
        }

        [HttpGet("Get_Persona/{id_persona}")]
        public IActionResult Get_Persona(string id_persona)
        {
            try
            {
                List<tbl_usuarios_list> Query = Negocio.Get_Persona(id_persona).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
        
        }
        [HttpPost("Add")]
        public IActionResult Add([FromBody] tbl_usuarios _tbl_usuarios)
        {
            try
            {
                var Query = Negocio.Add(_tbl_usuarios);
                if (Query.CurrentException == null)
                {
                    return Ok(Query.Response);
                }
                else
                {
                    return BadRequest(Query);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
           
        }
        [HttpPut("Activ")]
        public IActionResult Activ([FromBody] tbl_usuarios _tbl_usuarios)
        {
            try
            {
                List<Crudresponse> Query = Negocio.Activ(_tbl_usuarios).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
       
        }
        [HttpPut("Update")]
        public IActionResult update([FromBody] tbl_usuarios _tbl_usuarios)
        {
            try
            {
                List<Crudresponse> Query = Negocio.update(_tbl_usuarios).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("upd", ex);
                return BadRequest();
            }
       
        }

        [HttpDelete("Delete")]
        public IActionResult delete([FromBody] tbl_usuarios _tbl_usuarios)
        {
            try
            {
                List<Crudresponse> Query = Negocio.delete(_tbl_usuarios).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return BadRequest();
            }
         
        }
        [HttpGet("{correo}")]
        public ActionResult<string> Get(string correo)
        {
            try
            {
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
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            } 
        }

        [HttpGet("Get_Roles/{idUsuario}")]
        public ActionResult<string> GetRoles(string idusuario)
        {
            try
            {
                List<tbl_rol_usuario_response> Query = Negocio.ConsultarRoles(new tbl_rol_usuario_request() { idUsuario = idusuario.ToString() }).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get", ex);
                return BadRequest();
            }
       
        }

        [HttpPost("Add_Roles")]
        public IActionResult AddRol([FromBody] add_rol_usuario_request add_Rol_Usuario_Request)
        {
            try
            {
                List<Crudresponse> Query = Negocio.AddRol(add_Rol_Usuario_Request).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return BadRequest();
            }
     
        }

        [HttpDelete("Delete_Roles")]
        public IActionResult deleteRol([FromBody] delete_rol_usuario_request delete_Rol_Usuario_Request)
        {
            try
            {
                List<Crudresponse> Query = Negocio.deleteRol(delete_Rol_Usuario_Request).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return BadRequest();
            }
     
        }

        [HttpPut("Update_Roles")]
        public IActionResult updateRol([FromBody] update_rol_usuario_request update_Rol_Usuario_Request)
        {
            try
            {
                List<Crudresponse> Query = Negocio.updateRol(update_Rol_Usuario_Request).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("upd", ex);
                return BadRequest();
            }

        }

        [HttpPost("VerificarPassword")]
        public ActionResult<tbl_usuario_verifica> Post([FromBody] verificacion_usuario Usuario)
        {
            try
            {
                string PasswordEncoded = seguridad.GetSHA1(Usuario.Password);

                Usuario.Password = PasswordEncoded;

                tbl_usuario_verifica User = new tbl_usuario_verifica();
                List<tbl_usuario_verifica> Query = Negocio.ConsultarVP(User, Usuario).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("post", ex);
                return BadRequest();
            }
       

        }


        [HttpPut("NewPassword")]
        public IActionResult NewPassword([FromBody] tbl_usuario_verifica pass)
        {
            try
            {
                var newpassword = seguridad.GetSHA1(pass.password);
                pass.password = newpassword;
                List<Crudresponse> Query = Negocio.NewPassword(pass).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("put", ex);
                return BadRequest();
            }
    
        }
        [HttpPut("Recuperarpassword")]
        public IActionResult Recuperarpassword([FromBody] tbl_usuario recpass)
        {
            try
            {
                string bckpass = recpass.password;
                var newpassword = seguridad.GetSHA1(recpass.password);
                recpass.password = newpassword;
                List<Crudresponse> Query = Negocio.Recuperarpassword(recpass, bckpass).Response;
                return Ok(Query);
            }
            catch (Exception ex)
            {
                _logger.LogError("password", ex);
                return BadRequest();
            }
   
        }


    }
    public class seguridad
    {
        public static string GetSHA1(string str)
        {

            SHA1 sha1 = SHA1Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha1.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
    }
}