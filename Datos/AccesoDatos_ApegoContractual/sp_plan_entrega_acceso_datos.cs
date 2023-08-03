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
    public class sp_plan_entrega_acceso_datos
    {
        public BDParametros GeneracionParametros = new BDParametros();
        private readonly ILoggerManager _logger;
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        String sp_plan_entrega_store = "sp_plan_entrega";
        String sp_str_plan_entrega_ubicacion = "sp_str_plan_entrega_ubicacion";
        String sp_plan_entrega_producto = "sp_plan_entrega_producto";
        String sp_get_plan_entrega = "sp_get_plan_entrega";
        String sp_str_plan_entrega_ejecutor = "sp_str_plan_entrega_ejecutor";
        String sp_get_proveedor_contrato = "sp_get_proveedor_contrato";
        String sp_get_ubicacion_servidor = "sp_get_ubicacion_servidor";
        String sp_get_obligacion_producto = "sp_get_obligacion_producto";
        String get_obligaciones_incumplidas = "get_obligaciones_incumplidas";
        String sp_get_plan_entrega_ejec = "sp_get_plan_entrega_ejec";
        String sp_deleted_file_plan_entrega = "sp_deleted_file_plan_entrega_archivo";
        String sp_deleted_file_plan_entrega_archivo = "sp_deleted_file_plan_entrega_archivo";
        String sp_deleted_file_plan_entrega_archivo_global = "sp_deleted_file_plan_entrega_archivo_global";
        String sp_get_obligacion_producto_ejec = "sp_get_obligacion_producto_ejec";
        String sp_get_token_confirmacion_pe = "sp_get_token_confirmacion_pe_pr";
        String sp_tbl_ArchivosEntrega = "sp_tbl_ArchivosEntrega";
        String sp_tbl_MultiplesArchivosEntrega = "sp_tbl_MultiplesArchivosEntrega";
        string sp__documentary_information_download_filename = "_sp__documentary_information_download_filename";

        String sp_get_plan_entrega_detalle_producto = "sp_get_plan_entrega_detalle_producto";

        public sp_plan_entrega_acceso_datos()
        {
            _logger = new LoggerManager();
        }



        public ResponseGeneric<List<CrudresponseIdentificador>> sp_plan_entrega_header(sp_plan_entrega_input input)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = input.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = input.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_servidor_resp_id", Tipo = "String", Valor = input.p_tbl_contrato_servidor_resp_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_identificador", Tipo = "String", Valor = input.p_identificador.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_periodo", Tipo = "String", Valor = input.p_periodo.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_descripcion", Tipo = "String", Valor = input.p_descripcion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_ejecucion", Tipo = "String", Valor = input.p_fecha_ejecucion.ToString("yyyy-MM-dd") });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_activo", Tipo = "String", Valor = "1" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tipo_entrega", Tipo = "String", Valor = input.p_tipo_entrega.ToString() });


                List<CrudresponseIdentificador> Lista = new List<CrudresponseIdentificador>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_plan_entrega_store);
                            Lista = conexion.Query<CrudresponseIdentificador>().FromSql<CrudresponseIdentificador>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<CrudresponseIdentificador>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("", ex);
                return new ResponseGeneric<List<CrudresponseIdentificador>>(ex);
            }
        }
        public ResponseGeneric<List<CrudresponseIdentificador>> sp_plan_entrega_Ubicaciones(Guid p_tbl_plan_entrega_id, String p_str_ids, int op)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = op.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_plan_entrega_id", Tipo = "String", Valor = p_tbl_plan_entrega_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_str_ids", Tipo = "String", Valor = p_str_ids.ToString() });




                List<CrudresponseIdentificador> Lista = new List<CrudresponseIdentificador>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_str_plan_entrega_ubicacion);
                            Lista = conexion.Query<CrudresponseIdentificador>().FromSql<CrudresponseIdentificador>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<CrudresponseIdentificador>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("", ex);
                return new ResponseGeneric<List<CrudresponseIdentificador>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> sp_plan_entrega_Producto(sp_plan_entrega_producto input)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = input.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = input.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_producto_id", Tipo = "String", Valor = input.p_tbl_contrato_producto_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_ubicacion_plan_entrega_id", Tipo = "String", Valor = input.p_tbl_ubicacion_plan_entrega_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_estatus", Tipo = "String", Valor = input.p_estatus.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_cantidad", Tipo = "String", Valor = input.p_cantidad.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_detalle_actividad", Tipo = "String", Valor = input.p_detalle_actividad.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tipo", Tipo = "String", Valor = input.p_tipo.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto", Tipo = "String", Valor = input.p_monto != null ? input.p_monto.ToString().Replace(",", "") : "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_iva", Tipo = "String", Valor = input.p_monto_iva != null ? input.p_monto_iva.ToString().Replace(",", "") : "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_total", Tipo = "String", Valor = input.p_total != null ? input.p_total.ToString().Replace(",", "") : "" });


                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_plan_entrega_producto);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<vs_plan_entrega>> get_plan_entrega(Guid id, String opcion)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "id", Tipo = "String", Valor = id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "opcion", Tipo = "String", Valor = opcion });



                List<vs_plan_entrega> Lista = new List<vs_plan_entrega>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_plan_entrega);
                            Lista = conexion.Query<vs_plan_entrega>().FromSql<vs_plan_entrega>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<vs_plan_entrega>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("", ex);
                return new ResponseGeneric<List<vs_plan_entrega>>(ex);
            }
        }


        public ResponseGeneric<List<vs_plan_entrega_detalle_producto>> get_plan_entrega_detalle_productos(Guid id)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "id", Tipo = "String", Valor = id.ToString() });

                List<vs_plan_entrega_detalle_producto> Lista = new List<vs_plan_entrega_detalle_producto>();
                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_plan_entrega_detalle_producto);
                            Lista = conexion.Query<vs_plan_entrega_detalle_producto>().FromSql<vs_plan_entrega_detalle_producto>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<vs_plan_entrega_detalle_producto>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("", ex);
                return new ResponseGeneric<List<vs_plan_entrega_detalle_producto>>(ex);
            }
        }


        public ResponseGeneric<List<DropDownList>> get_plan_entrega_select(Guid id, String opcion)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "id", Tipo = "String", Valor = id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "opcion", Tipo = "String", Valor = opcion });



                List<DropDownList> Lista = new List<DropDownList>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_plan_entrega);
                            Lista = conexion.Query<DropDownList>().FromSql<DropDownList>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<DropDownList>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("", ex);
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }
        public ResponseGeneric<List<vs_plan_entrega_ejec>> get_plan_entrega_ejec(Guid id, String opcion, string usuario)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "opcion", Tipo = "String", Valor = opcion });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "usuario", Tipo = "String", Valor = usuario });



                List<vs_plan_entrega_ejec> Lista = new List<vs_plan_entrega_ejec>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_plan_entrega_ejec);
                            Lista = conexion.Query<vs_plan_entrega_ejec>().FromSql<vs_plan_entrega_ejec>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<vs_plan_entrega_ejec>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("", ex);
                return new ResponseGeneric<List<vs_plan_entrega_ejec>>(ex);
            }
        }

        public ResponseGeneric<dynamic> deleted_file(string token_id)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "token_id", Tipo = "String", Valor = token_id.ToString() });

                List<Token_confirmacion> Lista = new List<Token_confirmacion>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_deleted_file_plan_entrega);
                            Lista = conexion.Query<Token_confirmacion>().FromSql<Token_confirmacion>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
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

        public ResponseGeneric<dynamic> deleted_file_archivo(string token)
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

        public ResponseGeneric<List<Crudresponse>> str_plan_entrega_ejecutor(int p_opt, Guid p_tbl_ubicacion_plan_entrega_id, Guid p_str_ids)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_ubicacion_plan_entrega_id", Tipo = "String", Valor = p_tbl_ubicacion_plan_entrega_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_str_ids", Tipo = "String", Valor = p_str_ids.ToString() });



                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_str_plan_entrega_ejecutor);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_proveedor>> get_proveedor_contrato(Guid id)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tbl_contrato_id_", Tipo = "String", Valor = id.ToString() });

                List<tbl_proveedor> Lista = new List<tbl_proveedor>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_proveedor_contrato);
                            Lista = conexion.Query<tbl_proveedor>().FromSql<tbl_proveedor>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_proveedor>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("", ex);
                return new ResponseGeneric<List<tbl_proveedor>>(ex);
            }
        }
        public ResponseGeneric<List<DropDownList>> get_ubicacion_servidor(Guid id)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tbl_ubicacion_id_", Tipo = "String", Valor = id.ToString() });

                List<DropDownList> Lista = new List<DropDownList>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_ubicacion_servidor);
                            Lista = conexion.Query<DropDownList>().FromSql<DropDownList>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<DropDownList>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("", ex);
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_obligacion_cls>> get_obligacion_producto(Guid tbl_plan_entrega_id_, Guid tbl_producto_servicio_id)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tbl_producto_servicio_id", Tipo = "String", Valor = tbl_producto_servicio_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tbl_plan_entrega_id_", Tipo = "String", Valor = tbl_plan_entrega_id_.ToString() });


                List<tbl_obligacion_cls> Lista = new List<tbl_obligacion_cls>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_obligacion_producto);
                            Lista = conexion.Query<tbl_obligacion_cls>().FromSql<tbl_obligacion_cls>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_obligacion_cls>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("", ex);
                return new ResponseGeneric<List<tbl_obligacion_cls>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_obligacion_cls_PE>> get_obligacion_producto_ejec(Guid tbl_plan_entrega_id_, Guid tbl_producto_servicio_id)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tbl_plan_entrega_id_", Tipo = "String", Valor = tbl_plan_entrega_id_.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tbl_producto_servicio_id", Tipo = "String", Valor = tbl_producto_servicio_id.ToString() });


                List<tbl_obligacion_cls_PE> Lista = new List<tbl_obligacion_cls_PE>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_obligacion_producto_ejec);
                            Lista = conexion.Query<tbl_obligacion_cls_PE>().FromSql<tbl_obligacion_cls_PE>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_obligacion_cls_PE>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("", ex);
                return new ResponseGeneric<List<tbl_obligacion_cls_PE>>(ex);
            }
        }
        public ResponseGeneric<List<conteoitems>> sp_get_obligaciones_incumplidas(Guid tbl_plan_entrega_id_)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tbl_plan_entrega_id_", Tipo = "String", Valor = tbl_plan_entrega_id_.ToString() });



                List<conteoitems> Lista = new List<conteoitems>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, get_obligaciones_incumplidas);
                            Lista = conexion.Query<conteoitems>().FromSql<conteoitems>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<conteoitems>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("", ex);
                return new ResponseGeneric<List<conteoitems>>(ex);
            }
        }

        /***/
        public ResponseGeneric<List<Token_confirmacion>> sp_get_token_confirmacion(string tbl_plan_entrega_id_)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_plan_entrga_id", Tipo = "String", Valor = tbl_plan_entrega_id_.ToString() });



                List<Token_confirmacion> Lista = new List<Token_confirmacion>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_token_confirmacion_pe);
                            Lista = conexion.Query<Token_confirmacion>().FromSql<Token_confirmacion>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Token_confirmacion>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("", ex);
                return new ResponseGeneric<List<Token_confirmacion>>(ex);
            }
        }

        public ResponseGeneric<List<File_name>> _sp_download_filename(string entrega)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "id_entrega", Tipo = "String", Valor = entrega.ToString() });
                //ListaEnvioParam.Add(new EntidadParametro { Nombre = "expiration", Tipo = "String", Valor = exp.ToString()});
                //ListaEnvioParam.Add(new EntidadParametro { Nombre = "tipo_documento_", Tipo = "String", Valor = tipo_documento_ != null ? tipo_documento_ : "" });

                List<File_name> Lista = new List<File_name>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp__documentary_information_download_filename);
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
        public ResponseGeneric<List<Crudresponse>> add_archivosPE(sp_tbl_archivosPE input)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "id_", Tipo = "String", Valor = input.id_.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tbl_plan_entrega_id_", Tipo = "String", Valor = input.tbl_plan_entrega_id_.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tbl_Ubicacion_id_", Tipo = "String", Valor = input.tbl_Ubicacion_id_.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tbl_producto_servicio_id_", Tipo = "String", Valor = input.tbl_producto_servicio_id_.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "token_", Tipo = "String", Valor = input.token_.ToString() });


                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_tbl_MultiplesArchivosEntrega);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
    }
}
