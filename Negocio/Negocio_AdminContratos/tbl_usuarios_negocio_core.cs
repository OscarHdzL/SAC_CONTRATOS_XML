using System;
using System.Collections.Generic;
using System.Text;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Response;
using AccesoDatos_AdministracionDeContratos;
using Microsoft.Extensions.Configuration;
using System.IO;
using Solucion_Negocio_contractual.Util.EnvioSmtp;
using Utilidades.Log4Net;

namespace NegocioAdministracionContratos
{
    public class tbl_usuarios_negocio_core : crud_usuarios
    {
        private tbl_usuarios_acceso_datos _tbl_usuarios_acceso_datos = new tbl_usuarios_acceso_datos();
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        private readonly ILoggerManager _logger;
        public tbl_usuarios_negocio_core() 
        {
            _logger = new LoggerManager();
        }
        public ResponseGeneric<List<tbl_usuarios_list>> Get_Lista(string Instancia)
        {
            try
            {
                return _tbl_usuarios_acceso_datos.Get_Lista(Instancia);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_usuarios_list>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_usuarios_list>> Get_Persona(string id_persona)
        {
            try
            {
                return _tbl_usuarios_acceso_datos.Get_Persona(id_persona);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_usuarios_list>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Add(tbl_usuarios _tbl_usuarios)
        {
            try
            {
                _logger.LogAdvertencia("Guardando nuevo usuario");

                if (_tbl_usuarios.id_persona == null || _tbl_usuarios.id_persona == Guid.Empty.ToString() || _tbl_usuarios.id_persona == "")
                {
                    _tbl_usuarios.id_persona = Guid.NewGuid().ToString();
                }

                var resultado = _tbl_usuarios_acceso_datos.Add(_tbl_usuarios);

                //string[] p_usuario = _tbl_usuarios.email.Split('@');
                //var username = p_usuario[0];
                var username = _tbl_usuarios.usuario;

                if (resultado.Response[0].cod == "success")
                {
                    var password = _tbl_usuarios_acceso_datos.ObtenerPassword(username);

                    List<String> Correo = new List<String>();
                    Correo.Add(_tbl_usuarios.email);
                    //Correo.Add("alejandro.rocha@people-media.com.mx");

                    string Body = $"<p>Se ha dado de alta a <strong> {_tbl_usuarios.nombre} {_tbl_usuarios.ap_paterno} {_tbl_usuarios.ap_materno} </strong>con los siguientes datos de acceso:</p>";

                    Body += $"<p>Usuario:\t{username}</p>";
                    Body += $"<p>Contraseña:\t{password.Response[0].Value}</p>";
                    Body += $"<p>Este correo ha sido enviado automáticamente por el Sistema SACPRO, no responder este mensaje.</p>";

                    string Asunto = "Alta de usuario en Sistema de Administración de Contratos y Proveedores.";

                    string SMTPHost = Configuration["SMTPHost"].ToString();
                    string remitenteSMTP = Configuration["remitenteSMTP"].ToString();
                    string SMTPPass = Configuration["SMTPPass"].ToString();
                    int Port = int.Parse(Configuration["Port"].ToString());
                    _logger.LogAdvertencia("Mandando correo a "+ _tbl_usuarios.email);

                    //EnvioCorreos.SendEmail("webmail.pmsoluciones.mx", "sentrega@pmsoluciones.mx", "Ventana1010", 465, Correo, Asunto, Body);
                    EnvioCorreos correos = new EnvioCorreos();
                    correos.SendEmail(SMTPHost, remitenteSMTP, SMTPPass, Port, Correo, Asunto, Body);
                }

                return resultado;
                //return _tbl_usuarios_acceso_datos.Add(_tbl_usuarios);
            }
            catch (Exception ex)
            {
                _logger.LogError("tbl_usuarios_negocio_core - Add", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> AddRol(add_rol_usuario_request add_Rol_Usuario_Request)
        {
            try
            {
                return _tbl_usuarios_acceso_datos.AddRoles(add_Rol_Usuario_Request);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Activ(tbl_usuarios _tbl_usuarios)
        {
            try
            {
                return _tbl_usuarios_acceso_datos.Activ(_tbl_usuarios);
            }
            catch (Exception ex)
            {
                _logger.LogError("activ", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> update(tbl_usuarios _tbl_usuarios)
        {
            try
            {
                return _tbl_usuarios_acceso_datos.update(_tbl_usuarios);
            }
            catch (Exception ex)
            {
                _logger.LogError("upd", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> updateRol(update_rol_usuario_request update_Rol_Usuario_Request)
        {
            try
            {
                return _tbl_usuarios_acceso_datos.updateRol(update_Rol_Usuario_Request);
            }
            catch (Exception ex)
            {
                _logger.LogError("upd", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> delete(tbl_usuarios _tbl_usuarios)
        {
            try
            {
                return _tbl_usuarios_acceso_datos.delete(_tbl_usuarios);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> deleteRol(delete_rol_usuario_request delete_Rol_Usuario_Request)
        {
            try
            {
                return _tbl_usuarios_acceso_datos.deleteRol(delete_Rol_Usuario_Request);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<DropDownList>> FillDrop()
        {
            try
            {
                return _tbl_usuarios_acceso_datos.FillDrop();
            }
            catch (Exception ex)
            {
                _logger.LogError("fill", ex);
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }

        public ResponseGeneric<List<dependencias_usuario>> GetDependenciasAsignadas(string usuario_id)
        {
            try
            {
                return _tbl_usuarios_acceso_datos.GetDependenciasAsignadas(usuario_id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<dependencias_usuario>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_usuario>> Consultar(tbl_usuario entidad)
        {
            try
            {
                return _tbl_usuarios_acceso_datos.Consultar(entidad);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_usuario>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_rol_usuario_response>> ConsultarRoles(tbl_rol_usuario_request entidad)
        {
            try
            {
                return _tbl_usuarios_acceso_datos.ConsultarRoles(entidad);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_rol_usuario_response>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_usuario_verifica>> ConsultarVP(tbl_usuario_verifica entidad, verificacion_usuario usuario)
        {
            try
            {                
                //var resultado = _tbl_usuarios_acceso_datos.ConsultarVP(entidad, usuario);
                ////var regreso = "CONTRASEÑA NO ACTUALIZADA";

                //if (resultado.Response[0].estatus_autenticacion == "CONTRASEÑA NO ACTUALIZADA")
                //{
                //    return null;
                //}
                //else
                //{
                //    return resultado;
                //}
                return _tbl_usuarios_acceso_datos.ConsultarVP(entidad, usuario);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_usuario_verifica>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> NewPassword(tbl_usuario_verifica pass)
        {
            try
            {
                return _tbl_usuarios_acceso_datos.NewPassword(pass);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Recuperarpassword(tbl_usuario recpass, string bckpass)
        {
            try
            {
                //_tbl_usuarios_acceso_datos.NewPassword(pass);                               
                
                tbl_usuario_verifica pass = new tbl_usuario_verifica();

                var consultauser = _tbl_usuarios_acceso_datos.Consultar(recpass);
                //var id_user = 
                pass.id = consultauser.Response[0].id;
                pass.password = recpass.password;

                var resultado_cambiopass = _tbl_usuarios_acceso_datos.NewPassword(pass);

                if (resultado_cambiopass.Response[0].cod == "success")
                {
                    //var password = bckpass;

                    List<String> Correo = new List<String>();
                    Correo.Add(consultauser.Response[0].email);
                    //Correo.Add("alejandro.rocha@people-media.com.mx");

                    string Body = $"<p>Se ha solicitado la restauración de contraseña, con los siguientes datos de acceso:</p>";

                    Body += $"<p>Usuario:\t{consultauser.Response[0].usuario}</p>";
                    Body += $"<p>Contraseña:\t{bckpass}</p>";
                    Body += $"<p>Este correo ha sido enviado automáticamente por el Sistema SACPRO, no responder este mensaje.</p>";

                    string Asunto = "Restauración de usuario en Sistema de Administración de Contratos y Proveedores.";

                    string SMTPHost = Configuration["SMTPHost"].ToString();
                    string remitenteSMTP = Configuration["remitenteSMTP"].ToString();
                    string SMTPPass = Configuration["SMTPPass"].ToString();
                    int Port = int.Parse(Configuration["Port"].ToString());

                    //EnvioCorreos.SendEmail("webmail.pmsoluciones.mx", "sentrega@pmsoluciones.mx", "Ventana1010", 465, Correo, Asunto, Body);
                    EnvioCorreos correos = new EnvioCorreos();
                    correos.SendEmail(SMTPHost, remitenteSMTP, SMTPPass, Port, Correo, Asunto, Body);
                }

                return resultado_cambiopass;

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Exist(int opt, string param)
        {
            try
            {
                return _tbl_usuarios_acceso_datos.Exist(opt, param);
            }
            catch (Exception ex)
            {
                _logger.LogError("exist", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }


    }
}
