using Conexion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Area;
using Modelos.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Utilidades.Log4Net;

namespace AccesoDatos_AdministracionDeContratos
{
    public class tbl_usuarios_acceso_datos
    {
        public BDParametros GeneracionParametros = new BDParametros();
        private readonly ILoggerManager _logger;
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string sp_get_usuarios = "sp_get_usuarios_instancia";
        string sp_get_rol_dropdown = "sp_get_rol_dropdown";
        string sp_get_persona_edit = "sp_get_persona_edit";
        string sp_usuario_servidor_publico = "sp_usuario_servidor_publico";
        string sp_add_rol_usuario = "sp_add_rol_usuario";
        string sp_delete_rol_usuario = "sp_delete_rol_usuario";
        string sp_update_rol_usuario = "sp_principal_rol_usuario";
        string StoreProcedure = "sp_get_usuario";
        string StoreProcedureGetRoles = "sp_get_roles_usuario";
        string StoreProcedureVP = "sp_valida_usuario";
        string sp_usuario_password = "sp_usuario_password";

        string sp_get_dependencias_usuario = "sp_get_dependencias_usuario";

        public tbl_usuarios_acceso_datos()
        {
            _logger = new LoggerManager();
        }
        public ResponseGeneric<List<tbl_usuarios_list>> Get_Lista(String Instancia)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "Instancia", Tipo = "String", Valor = Instancia});                

                List<tbl_usuarios_list> Lista = new List<tbl_usuarios_list>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_usuarios);
                            Lista = conexion.Query<tbl_usuarios_list>().FromSql<tbl_usuarios_list>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_usuarios_list>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_usuarios_list>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_usuarios_list>> Get_Persona(String id_persona)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "id_persona", Tipo = "String", Valor = id_persona });

                List<tbl_usuarios_list> Lista = new List<tbl_usuarios_list>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_persona_edit);
                            Lista = conexion.Query<tbl_usuarios_list>().FromSql<tbl_usuarios_list>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_usuarios_list>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_usuarios_list>>(ex);
            }
        }

        public ResponseGeneric<List<DropDownList>> ObtenerPassword(string username)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_usuario", Tipo = "String", Valor = username});

                List<DropDownList> Lista = new List<DropDownList>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, "sp_get_password_usuario");
                            Lista = conexion.Query<DropDownList>().FromSql<DropDownList>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }
                }

                #endregion

                return new ResponseGeneric<List<DropDownList>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("password", ex);
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Add(tbl_usuarios _tbl_usuarios)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "int", Valor = "2" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_persona_id", Tipo = "String", Valor = _tbl_usuarios.id_persona.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_dependencia_id", Tipo = "String", Valor = _tbl_usuarios.id_dependencia.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_area_id", Tipo = "String", Valor = _tbl_usuarios.id_area.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_rol_id", Tipo = "String", Valor = _tbl_usuarios.id_rol.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_nombre", Tipo = "String", Valor = _tbl_usuarios.nombre.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_ap_paterno", Tipo = "String", Valor = _tbl_usuarios.ap_paterno.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_ap_materno", Tipo = "String", Valor = _tbl_usuarios.ap_materno.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_email", Tipo = "String", Valor = _tbl_usuarios.email.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_rfc", Tipo = "String", Valor = _tbl_usuarios.rfc.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_telefono", Tipo = "String", Valor = _tbl_usuarios.telefono.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_extencion", Tipo = "String", Valor = _tbl_usuarios.extencion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_inicio", Tipo = "String", Valor = _tbl_usuarios.fecha_inicio.ToString("yyyy-MM-ddTHH:mm:ss") });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_fin", Tipo = "String", Valor = _tbl_usuarios.fecha_fin.ToString("yyyy-MM-ddTHH:mm:ss") });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_super_usuario", Tipo = "String", Valor = _tbl_usuarios.super_usuario.ToString()});
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_usuario", Tipo = "String", Valor = _tbl_usuarios.usuario.ToString()});

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_dependencias_json", Tipo = "JSON", Valor = _tbl_usuarios.dependencias_adicionales == null? "NULL": JsonConvert.SerializeObject(_tbl_usuarios.dependencias_adicionales) });

                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_usuario_servidor_publico);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> AddRoles(add_rol_usuario_request tbl_Rol_Usuario_Request)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_rol_id", Tipo = "String", Valor = tbl_Rol_Usuario_Request.idRol.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_usuario_id", Tipo = "String", Valor = tbl_Rol_Usuario_Request.idUsuario.ToString() });

                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_add_rol_usuario);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

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
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "int", Valor = "5" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_persona_id", Tipo = "String", Valor = _tbl_usuarios.id_persona.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_dependencia_id", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_area_id", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_rol_usuario_id", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_nombre", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_ap_paterno", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_ap_materno", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_email", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_rfc", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_telefono", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_extencion", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_inicio", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_fin", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_super_usuario", Tipo = "Boolean", Valor = _tbl_usuarios.super_usuario.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_usuario", Tipo = "String", Valor = _tbl_usuarios.usuario.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_dependencias_json", Tipo = "JSON", Valor = _tbl_usuarios.dependencias_adicionales == null ? "NULL" : JsonConvert.SerializeObject(_tbl_usuarios.dependencias_adicionales) });

                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_usuario_servidor_publico);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

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
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "int", Valor = "3" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_persona_id", Tipo = "String", Valor = _tbl_usuarios.id_persona.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_dependencia_id", Tipo = "String", Valor = _tbl_usuarios.id_dependencia.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_area_id", Tipo = "String", Valor = _tbl_usuarios.id_area.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_rol_id", Tipo = "String", Valor = _tbl_usuarios.id_rol.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_nombre", Tipo = "String", Valor = _tbl_usuarios.nombre.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_ap_paterno", Tipo = "String", Valor = _tbl_usuarios.ap_paterno.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_ap_materno", Tipo = "String", Valor = _tbl_usuarios.ap_materno.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_email", Tipo = "String", Valor = _tbl_usuarios.email.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_rfc", Tipo = "String", Valor = _tbl_usuarios.rfc.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_telefono", Tipo = "String", Valor = _tbl_usuarios.telefono.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_extencion", Tipo = "String", Valor = _tbl_usuarios.extencion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_inicio", Tipo = "String", Valor = _tbl_usuarios.fecha_inicio.ToString("yyyy-MM-ddTHH:mm:ss") });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_fin", Tipo = "String", Valor = _tbl_usuarios.fecha_fin.ToString("yyyy-MM-ddTHH:mm:ss") });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_super_usuario", Tipo = "String", Valor = _tbl_usuarios.super_usuario.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_usuario", Tipo = "String", Valor = _tbl_usuarios.usuario.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_dependencias_json", Tipo = "JSON", Valor = _tbl_usuarios.dependencias_adicionales == null ? "NULL" : JsonConvert.SerializeObject(_tbl_usuarios.dependencias_adicionales) });


                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_usuario_servidor_publico);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

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
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id_tbl_rol_usuario", Tipo = "String", Valor = update_Rol_Usuario_Request.idRolUsuario.ToString() });
               
                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_update_rol_usuario);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

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
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "int", Valor = "4" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_persona_id", Tipo = "String", Valor = _tbl_usuarios.id_persona.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_dependencia_id", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_area_id", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_rol_usuario_id", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_nombre", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_ap_paterno", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_ap_materno", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_email", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_rfc", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_telefono", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_extencion", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_inicio", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_fin", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_super_usuario", Tipo = "Boolean", Valor = _tbl_usuarios.super_usuario.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_usuario", Tipo = "String", Valor = _tbl_usuarios.usuario.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_dependencias_json", Tipo = "JSON", Valor = _tbl_usuarios.dependencias_adicionales == null ? "NULL" : JsonConvert.SerializeObject(_tbl_usuarios.dependencias_adicionales) });


                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_usuario_servidor_publico);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

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
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id_tbl_rol_usuario", Tipo = "String", Valor = delete_Rol_Usuario_Request.idRolUsuario.ToString() });

                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_delete_rol_usuario);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

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
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                
                List<DropDownList> Lista = new List<DropDownList>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_rol_dropdown);
                            Lista = conexion.Query<DropDownList>().FromSql<DropDownList>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<DropDownList>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("fill", ex);
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_usuario>> Consultar(tbl_usuario entidad)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "usuario_email", Tipo = "String", Valor = entidad.usuario });



                List<tbl_usuario> Lista = new List<tbl_usuario>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure);
                            Lista = conexion.Query<tbl_usuario>().FromSql<tbl_usuario>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion

                return new ResponseGeneric<List<tbl_usuario>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_usuario>>(ex);
            }
        }

        public ResponseGeneric<List<dependencias_usuario>> GetDependenciasAsignadas(string usuario_id)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "usuario_id", Tipo = "String", Valor = usuario_id });
                List<dependencias_usuario> Lista = new List<dependencias_usuario>();
                #endregion
                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_dependencias_usuario);
                            Lista = conexion.Query<dependencias_usuario>().FromSql<dependencias_usuario>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion

                return new ResponseGeneric<List<dependencias_usuario>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<dependencias_usuario>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_rol_usuario_response>> ConsultarRoles(tbl_rol_usuario_request entidad)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "id_usuario", Tipo = "String", Valor = entidad.idUsuario });



                List<tbl_rol_usuario_response> Lista = new List<tbl_rol_usuario_response>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedureGetRoles);
                            Lista = conexion.Query<tbl_rol_usuario_response>().FromSql<tbl_rol_usuario_response>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion

                return new ResponseGeneric<List<tbl_rol_usuario_response>>(Lista);

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
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "usuario_email", Tipo = "String", Valor = usuario.Email });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "usuario_password", Tipo = "String", Valor = usuario.Password });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "usuario_salto", Tipo = "String", Valor = usuario.Salto });

                List<tbl_usuario_verifica> Lista = new List<tbl_usuario_verifica>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedureVP);
                            Lista = conexion.Query<tbl_usuario_verifica>().FromSql<tbl_usuario_verifica>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion

                return new ResponseGeneric<List<tbl_usuario_verifica>>(Lista);

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
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "int", Valor = "3" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = pass.id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_password", Tipo = "String", Valor = pass.password.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_persona_id", Tipo = "String", Valor = "" });

                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_usuario_password);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("password", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Exist(int opt, string param)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "int", Valor = opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_param", Tipo = "String", Valor = param });

                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, "sp_existe_correo_rfc");
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("exist", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }


    }
}
