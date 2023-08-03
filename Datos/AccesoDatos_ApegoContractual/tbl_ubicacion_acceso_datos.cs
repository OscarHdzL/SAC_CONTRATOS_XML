using Conexion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modelos.Interfaz;
using Modelos.Modelos;
 
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.IO;
using Utilidades.Log4Net;

namespace AccesoDatos
{
    public class tbl_ubicacion_acceso_datos : crud_tbl_ubicacion
    {
        public BDParametros GeneracionParametros = new BDParametros();
        private readonly ILoggerManager _logger;
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string sp_get_ubicaciones = "sp_get_ubicaciones";
        string sp_ubicacion = "sp_ubicacion";
        string sp_get_tbl_ubicacion_ejecutor = "sp_get_tbl_ubicacion_ejecutor";
        string sp_str_ubicacion_servidor = "sp_str_ubicacion_servidor";
        string Get_ubicaciones_plantentrega_id = "Get_ubicaciones_plantentrega_id";
        string sp_get_ubic_pe_group = "sp_get_ubic_pe_group";
        string sp_get_token_pe_ubicacion = "sp_get_token_pe_ubicacion";
        string sp_delete_token_pe_ubicacion = "sp_delete_token_pe_ubicacion";
        String sp_deleted_file_plan_ubicacion_archivo = "sp_deleted_file_plan_entrega_archivo";
        String sp_deleted_file_plan_entrega_archivo_global = "sp_deleted_file_plan_entrega_archivo_global";
        string sp_ubicacion_servidor = "sp_ubicacion_servidor";
        string sp_validar_ubicacion_ligada = "sp_validar_ubicacion_ligada";
        string _sp__documentary_information_download_filename_ubicacion = "_sp__documentary_information_download_filename_ubicacion";
        string _sp__documentary_download_filename_ubicacion_multiple = "_sp__documentary_download_filename_ubicacion_multiple";

        public tbl_ubicacion_acceso_datos()
        {
            _logger = new LoggerManager();
        }


        public List<DropDownList> ConsultarUbicacion_ejecutor(Guid tbl_ubicacion_id)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tbl_ubicacion_id", Tipo = "String", Valor = tbl_ubicacion_id.ToString() });
                List<DropDownList> Lista = new List<DropDownList>();
                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_tbl_ubicacion_ejecutor);
                            Lista = conexion.Query<DropDownList>().FromSql<DropDownList>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }
                }
                #endregion
                return Lista;

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new List<DropDownList>();
            }
        }

        public ResponseGeneric<List<tbl_ubicacion_output>> Consultar(tbl_ubicacion_input tbl_ubicacion_input)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "idparameter", Tipo = "String", Valor = tbl_ubicacion_input.idparameter.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tipo", Tipo = "String", Valor = tbl_ubicacion_input.tipo });
                List<tbl_ubicacion_output> Lista = new List<tbl_ubicacion_output>();
                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_ubicaciones);
                            Lista = conexion.Query<tbl_ubicacion_output>().FromSql<tbl_ubicacion_output>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            //List<tbl_ubicacion_interfaz> res = (List<tbl_ubicacion_interfaz>)Lista;
                            break;
                    }
                }
                #endregion
                return new ResponseGeneric<List<tbl_ubicacion_output>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_ubicacion_output>>(ex);
            }
        }


        public ResponseGeneric<List<plan_entrega_ubicacion>> ConsultarPlanEntrega(Guid tbl_plan_entrega_id_)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tbl_plan_entrega_id_", Tipo = "String", Valor = tbl_plan_entrega_id_.ToString() });
                List<plan_entrega_ubicacion> Lista = new List<plan_entrega_ubicacion>();
                #endregion
                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, Get_ubicaciones_plantentrega_id);
                            Lista = conexion.Query<plan_entrega_ubicacion>().FromSql<plan_entrega_ubicacion>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }
                }
                #endregion
                return new ResponseGeneric<List<plan_entrega_ubicacion>>(Lista);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<plan_entrega_ubicacion>>(ex);
            }
        }
        public ResponseGeneric<List<plan_entrega_ubicacion>> get_ubic_pe_group(Guid tbl_plan_entrega_id_, Guid tbl_usuari_id)
        {
            ///jmma
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tbl_plan_entrega_id_", Tipo = "String", Valor = tbl_plan_entrega_id_.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tbl_usuario_id", Tipo = "String", Valor = tbl_usuari_id.ToString() });
                List<plan_entrega_ubicacion> Lista = new List<plan_entrega_ubicacion>();
                #endregion
                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_ubic_pe_group);
                            Lista = conexion.Query<plan_entrega_ubicacion>().FromSql<plan_entrega_ubicacion>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }
                }
                #endregion
                return new ResponseGeneric<List<plan_entrega_ubicacion>>(Lista);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<plan_entrega_ubicacion>>(ex);
            }
        }

        public ResponseGeneric<List<token_ubicacion_PE>> sp_get_token_ubicacion(Guid tbl_plan_entrega_id_, Guid tbl_ubicacion_id_)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_plan_entrga_id", Tipo = "String", Valor = tbl_plan_entrega_id_.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_ubicacion_id", Tipo = "String", Valor = tbl_ubicacion_id_.ToString() });



                List<token_ubicacion_PE> Lista = new List<token_ubicacion_PE>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_token_pe_ubicacion);
                            Lista = conexion.Query<token_ubicacion_PE>().FromSql<token_ubicacion_PE>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<token_ubicacion_PE>>(Lista);

            }
            catch (Exception ex)

            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<token_ubicacion_PE>>(ex);
            }
        }

        public ResponseGeneric<dynamic> sp_delete_token_ubicacion(Guid tbl_plan_entrega_id_, Guid tbl_ubicacion_id_)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_plan_entrga_id", Tipo = "String", Valor = tbl_plan_entrega_id_.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_ubicacion_id", Tipo = "String", Valor = tbl_ubicacion_id_.ToString() });



                List<token_ubicacion_PE> Lista = new List<token_ubicacion_PE>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_delete_token_pe_ubicacion);
                            Lista = conexion.Query<token_ubicacion_PE>().FromSql<token_ubicacion_PE>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<dynamic>(true);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<dynamic>(ex);
            }
        }

        public ResponseGeneric<dynamic> deleted_file_archivo_ubicacion(string token)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "token", Tipo = "String", Valor = token.ToString() });

                List<Token_eliminar> Lista = new List<Token_eliminar>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_deleted_file_plan_entrega_archivo_global);
                            Lista = conexion.Query<Token_eliminar>().FromSql<Token_eliminar>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }
                }
                #endregion
                return new ResponseGeneric<dynamic>(true);

            }
            catch (Exception ex)
            {
                _logger.LogError("", ex);
                return new ResponseGeneric<dynamic>(ex);
            }
        }

        public ResponseGeneric<List<CrudresponseIdentificador>> Add(tbl_ubicacion_add tbl_ubicacion_add)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                if (tbl_ubicacion_add.p_opt == 2 || tbl_ubicacion_add.p_opt == 3)
                {
                    ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = tbl_ubicacion_add.p_opt.ToString() });
                    ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = tbl_ubicacion_add.p_id.ToString() });
                    ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_dependencia_id", Tipo = "String", Valor = tbl_ubicacion_add.p_tbl_dependencia_id.ToString() });
                    ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_ciudad_id", Tipo = "String", Valor = tbl_ubicacion_add.p_tbl_ciudad_id.ToString() });
                    ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_clave", Tipo = "String", Valor = tbl_ubicacion_add.p_clave.ToString() });
                    ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_unidad", Tipo = "String", Valor = tbl_ubicacion_add.p_unidad.ToString() });
                    ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_direccion", Tipo = "String", Valor = tbl_ubicacion_add.p_direccion.ToString() });
                    ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_referencia", Tipo = "String", Valor = tbl_ubicacion_add.p_referencia.ToString() });
                    ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_telefono", Tipo = "String", Valor = tbl_ubicacion_add.p_telefono.ToString() });
                    ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_activo", Tipo = "String", Valor = tbl_ubicacion_add.p_activo.ToString() == "true" ? "1" : "0" });
                    ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_dias_atencion", Tipo = "String", Valor = tbl_ubicacion_add.p_dias_atencion.ToString()  });
                    ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_horario_atencion", Tipo = "String", Valor = tbl_ubicacion_add.p_horario_atencion.ToString() });
                    ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_rol_usuario_id", Tipo = "String", Valor = tbl_ubicacion_add.p_tbl_rol_usuario_id.ToString() });
           
                }
                else
                {
                    ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = tbl_ubicacion_add.p_opt.ToString() });
                    ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = tbl_ubicacion_add.p_id.ToString() });
                    ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_dependencia_id", Tipo = "String", Valor = String.Empty });
                    ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_ciudad_id", Tipo = "String", Valor = String.Empty });
                    ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_clave", Tipo = "String", Valor = String.Empty  });
                    ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_unidad", Tipo = "String", Valor = String.Empty });
                    ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_direccion", Tipo = "String", Valor = String.Empty });
                    ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_referencia", Tipo = "String", Valor = String.Empty });
                    ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_telefono", Tipo = "String", Valor = String.Empty });
                    ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_activo", Tipo = "String", Valor = "0" });
                    ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_dias_atencion", Tipo = "String", Valor = String.Empty });
                    ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_horario_atencion", Tipo = "String", Valor = String.Empty });
                    ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_rol_usuario_id", Tipo = "String", Valor = String.Empty });
 

                }
                List<CrudresponseIdentificador> Lista = new List<CrudresponseIdentificador>();
                #endregion
                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_ubicacion);
                            Lista = conexion.Query<CrudresponseIdentificador>().FromSql<CrudresponseIdentificador>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }
                }
                #endregion
                return new ResponseGeneric<List<CrudresponseIdentificador>>(Lista);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return new ResponseGeneric<List<CrudresponseIdentificador>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Addservidor(int p_opt,String p_tbl_ubicacion_id, String p_str_ids)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_ubicacion_id", Tipo = "String", Valor = p_tbl_ubicacion_id });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_str_ids", Tipo = "String", Valor = p_str_ids });

                List<Crudresponse> Lista = new List<Crudresponse>();
                #endregion
                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_str_ubicacion_servidor);
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


        public ResponseGeneric<Crudresponse> add_update_ubicacion_ejecutor(ubicacion_ejecutor ejecutor)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = ejecutor.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_ubicacion_servidor_id", Tipo = "String", Valor = ejecutor.p_tbl_ubicacion_servidor_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_ubicacion_id", Tipo = "String", Valor = ejecutor.p_tbl_ubicacion_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_rol_usuario_id", Tipo = "String", Valor = ejecutor.p_tbl_rol_usuario_id.ToString() });

                Crudresponse Lista = new Crudresponse();
                #endregion
                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_ubicacion_servidor);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync().Result;
                            break;
                    }
                }
                #endregion
                return new ResponseGeneric<Crudresponse>(Lista);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return new ResponseGeneric<Crudresponse>(ex);
            }
        }

        public ResponseGeneric<validar_ubicacion_ligada> validar_ubicacion_ligada(Guid ubicacion)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = ubicacion.ToString() });

                validar_ubicacion_ligada Lista = new validar_ubicacion_ligada();
                #endregion
                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_validar_ubicacion_ligada);
                            Lista = conexion.Query<validar_ubicacion_ligada>().FromSql<validar_ubicacion_ligada>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync().Result;
                            break;
                    }
                }
                #endregion
                return new ResponseGeneric<validar_ubicacion_ligada>(Lista);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<validar_ubicacion_ligada>(ex);
            }
        }

        public ResponseGeneric<List<File_name>> _sp_download_filename_ubicacion(string ubicacion, string clave)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "id_ubicacion", Tipo = "String", Valor = ubicacion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "clave_ubi", Tipo = "String", Valor = clave.ToString() });

                List<File_name> Lista = new List<File_name>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, _sp__documentary_download_filename_ubicacion_multiple);
                            Lista = conexion.Query<File_name>().FromSql<File_name>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<File_name>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("", ex);
                return new ResponseGeneric<List<File_name>>(ex);
            }
        }




    }
}
