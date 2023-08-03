using AccesoDatos;
using Microsoft.Extensions.Configuration;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Dependencia;
using Modelos.Modelos.Usuarios;
using Modelos.Response;
using Solucion_Negocio.Util.EnvioSmtp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Utilidades.Log4Net;

namespace Solucion_Negocio
{
    public class TBL_Usuarios_Negocio : CRUD<tbl_usuario>
    {
        private tbl_usuarios_acceso_datos _Usuarios = new tbl_usuarios_acceso_datos();
        private readonly ILoggerManager _logger;
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();

        public TBL_Usuarios_Negocio()
        {
            _logger = new LoggerManager();
        }
        public ResponseGeneric<List<tbl_usuario>> Consultar(tbl_usuario entidad)
        {
            try
            {
                return _Usuarios.Consultar(entidad);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_usuario>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Add(tbl_usuario_add usuario)
        {
            try
            {
                if (usuario.p_id_persona == null || usuario.p_id_persona == Guid.Empty.ToString())
                {
                    usuario.p_id_persona = Guid.NewGuid().ToString();
                    usuario.p_opt = 2;
                }

                var resultado = _Usuarios.Guardar(usuario);

                if(resultado.Response[0].cod == "success")
                {
                    var password = _Usuarios.ObtenerPassword(usuario.p_usuario);

                    List<String> Correo = new List<String>();
                    Correo.Add(usuario.p_email);
                    //Correo.Add("alejandro.rocha@people-media.com.mx");

                    string Body = $"<p>Se ha dado de alta a <strong> {usuario.p_nombre} {usuario.p_ap_paterno} {usuario.p_ap_materno} </strong>con los siguientes datos de acceso:</p>";

                    Body += $"<p>Usuario:\t{usuario.p_usuario}</p>";
                    Body += $"<p>Contraseña:\t{password.Response[0].Value}</p>";
                    Body += $"<p>Este correo ha sido enviado automáticamente por el Sistema SACPRO, no responder este mensaje.</p>";

                    string Asunto = "Alta de usuario en Sistema de Administración de Contratos y Proveedores.";

                    string SMTPHost = Configuration["SMTPHost"].ToString();
                    string remitenteSMTP = Configuration["remitenteSMTP"].ToString();
                    string SMTPPass = Configuration["SMTPPass"].ToString();
                    int Port = int.Parse(Configuration["Port"].ToString());

                    //EnvioCorreos.SendEmail("webmail.pmsoluciones.mx", "sentrega@pmsoluciones.mx", "Ventana1010", 465, Correo, Asunto, Body);
                    EnvioCorreos mail = new EnvioCorreos();
                    mail.SendEmail(SMTPHost, remitenteSMTP, SMTPPass, Port, Correo, Asunto, Body);
                }

                return resultado;
                //return null;
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<tbl_instancia_contrato_get> Get(string Instancia)
        {
            try
            {
                return _Usuarios.Get(Instancia);
            }
            catch (Exception ex)
            {
                _logger.LogError("get", ex);
                return new ResponseGeneric<tbl_instancia_contrato_get>(ex);
            }
        }

        //public Response Eliminar(TBL_USUARIOS entidad)
        //{
        //    try
        //    {
        //        return _Usuarios.Eliminar(entidad);
        //    }
        //    catch (Exception ex)
        //    {

        //        return new Response(ex);
        //    }
        //}

        //public Response Guardar(TBL_USUARIOS entidad)
        //{
        //    try
        //    {
        //        return _Usuarios.Guardar(entidad);
        //    }
        //    catch (Exception ex)
        //    {

        //        return new Response(ex);
        //    }
        //}

        //public Response Modificar(TBL_USUARIOS entidad)
        //{
        //    try
        //    {
        //        return _Usuarios.Modificar(entidad);
        //    }
        //    catch (Exception ex)
        //    {

        //        return new Response(ex);
        //    }
        //}
    }
}
