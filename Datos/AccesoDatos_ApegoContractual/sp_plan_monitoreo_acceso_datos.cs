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
    public class sp_plan_monitoreo_acceso_datos 
    {
        public BDParametros GeneracionParametros = new BDParametros();
        private readonly ILoggerManager _logger;
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string StoreProcedure_estado = "sp_get_estado_planmonitoreo";
        string StoreProcedure_lista = "sp_get_planmonitoreo_contrato";
        string StoreProcedure_ubicaciones_planmonitoreo = "sp_get_ubicaciones_planmonitoreo";
        string StoreProcedure_productos_ubicacion_monitoreo = "sp_get_productos_planmonitoreo_ubicacion";
        string StoreProcedure_obligaciones_producto_ubicacion_monitoreo = "sp_get_obligaciones_planmonitoreo_ubicacion_producto";
        string StoreProcedure_obligaciones_nocumple = "sp_get_obligaciones_planmonitoreo_nocumple";
        string StoreProcedure_planmonitoreo = "sp_plan_monitoreo";
        string StoreProcedure_planmonitoreo_ubicacion = "sp_plan_monitoreo_ubicacion";
        string StoreProcedure_obligaciones_incumplidas = "sp_get_obligacionesincumplidas_planmonitoreo";
        string sp_tbl_ArchivosMonitoreo = "sp_tbl_ArchivosMonitoreo";
        string _sp__documentary_information_download_filename_Monitoreo = "_sp__documentary_information_download_filename_Monitoreo";
        string StoreProcedure_planmonitoreo_automatico = "sp_plan_monitoreo_automatico";

        public sp_plan_monitoreo_acceso_datos()
        {
            _logger = new LoggerManager();
        }

        public ResponseGeneric<List<tbl_plan_monitoreo_estado>> ConsultarEstado()
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                //ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_contrato_id", Tipo = "String", Valor = contrato.ToString() });

                List<tbl_plan_monitoreo_estado> Lista = new List<tbl_plan_monitoreo_estado>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_estado);
                            Lista = conexion.Query<tbl_plan_monitoreo_estado>().FromSql<tbl_plan_monitoreo_estado>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_plan_monitoreo_estado>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_plan_monitoreo_estado>>(ex);
            }
        }


        public ResponseGeneric<CrudresponseIdentificador> sp_plan_monitoreo(sp_plan_monitoreo_input input)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = input.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = input.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_plan_entrega_id ", Tipo = "String", Valor = input.p_tbl_plan_entrega_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_plan_monitoreo_estado_id  ", Tipo = "String", Valor = input.p_tbl_plan_monitoreo_estado_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_servidor_resp_id", Tipo = "String", Valor = input.p_tbl_contrato_servidor_resp_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_periodo", Tipo = "String", Valor = input.p_periodo.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_ejecucion", Tipo = "String", Valor = input.p_ejecucion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_inclusion ", Tipo = "String", Valor = input.p_inclusion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_activo", Tipo = "String", Valor = input.p_activo.ToString() });
                


                CrudresponseIdentificador Lista = new CrudresponseIdentificador();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_planmonitoreo);
                            Lista = conexion.Query<CrudresponseIdentificador>().FromSql<CrudresponseIdentificador>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<CrudresponseIdentificador>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<CrudresponseIdentificador>(ex);
            }
        }

        public ResponseGeneric<CrudresponseIdentificador> sp_plan_monitoreo_ubicaciones(sp_plan_monitoreo_ubicacion ubicacion)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = ubicacion.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = ubicacion.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_plan_monitoreo_id ", Tipo = "String", Valor = ubicacion.p_tbl_plan_monitoreo_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_ubicacion_plan_entrega_id  ", Tipo = "String", Valor = ubicacion.p_tbl_ubicacion_plan_entrega_id.ToString() });
                
                CrudresponseIdentificador Lista = new CrudresponseIdentificador();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_planmonitoreo_ubicacion);
                            Lista = conexion.Query<CrudresponseIdentificador>().FromSql<CrudresponseIdentificador>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<CrudresponseIdentificador>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<CrudresponseIdentificador>(ex);
            }
        }

        public ResponseGeneric<CrudresponseIdentificador> sp_plan_monitoreo_automatico(String plan_entrega)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = plan_entrega.ToString() });
                CrudresponseIdentificador Lista = new CrudresponseIdentificador();
                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_planmonitoreo_automatico);
                            Lista = conexion.Query<CrudresponseIdentificador>().FromSql<CrudresponseIdentificador>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<CrudresponseIdentificador>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<CrudresponseIdentificador>(ex);
            }
        }


        public ResponseGeneric<List<tbl_plan_monitoreo_lista>> ConsultarPlanes_Monitoreo(Guid contrato)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "contrato", Tipo = "String", Valor = contrato.ToString() });

                List<tbl_plan_monitoreo_lista> Lista = new List<tbl_plan_monitoreo_lista>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_lista);
                            Lista = conexion.Query<tbl_plan_monitoreo_lista>().FromSql<tbl_plan_monitoreo_lista>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_plan_monitoreo_lista>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_plan_monitoreo_lista>>(ex);
            }
        }


        public ResponseGeneric<List<tbl_ubicaciones_planmonitoreo>> ConsultarUbicaciones_PlanMonitoreo(Guid idPlanMonitoreo)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "plan_monitoreo", Tipo = "String", Valor = idPlanMonitoreo.ToString() });

                List<tbl_ubicaciones_planmonitoreo> Lista = new List<tbl_ubicaciones_planmonitoreo>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_ubicaciones_planmonitoreo);
                            Lista = conexion.Query<tbl_ubicaciones_planmonitoreo>().FromSql<tbl_ubicaciones_planmonitoreo>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_ubicaciones_planmonitoreo>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_ubicaciones_planmonitoreo>>(ex);
            }
        }

        public ResponseGeneric<List<sp_productos_ubicacion_monitoreo>> ConsultarProductos_Ubic_PlanMon(Guid idPlanMonitoreo, Guid idUbicacion)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "plan_monitoreo", Tipo = "String", Valor = idPlanMonitoreo.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "ubicacion", Tipo = "String", Valor = idUbicacion.ToString() });

                List<sp_productos_ubicacion_monitoreo> Lista = new List<sp_productos_ubicacion_monitoreo>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_productos_ubicacion_monitoreo);
                            Lista = conexion.Query<sp_productos_ubicacion_monitoreo>().FromSql<sp_productos_ubicacion_monitoreo>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<sp_productos_ubicacion_monitoreo>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<sp_productos_ubicacion_monitoreo>>(ex);
            }
        }


        


        public ResponseGeneric<List<sp_obligaciones_ubicacion_producto_planmonitoreo>> ConsultarObligaciones_Ubic_Producto(Guid idPlanMonitoreo, Guid idUbicacion, Guid idProducto)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "plan_monitoreo", Tipo = "String", Valor = idPlanMonitoreo.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "ubicacion", Tipo = "String", Valor = idUbicacion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "producto", Tipo = "String", Valor = idProducto.ToString() });

                List<sp_obligaciones_ubicacion_producto_planmonitoreo> Lista = new List<sp_obligaciones_ubicacion_producto_planmonitoreo>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_obligaciones_producto_ubicacion_monitoreo);
                            Lista = conexion.Query<sp_obligaciones_ubicacion_producto_planmonitoreo>().FromSql<sp_obligaciones_ubicacion_producto_planmonitoreo>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<sp_obligaciones_ubicacion_producto_planmonitoreo>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<sp_obligaciones_ubicacion_producto_planmonitoreo>>(ex);
            }
        }

        public ResponseGeneric<List<sp_obligaciones_nocumple>> ConsultarObligaciones_NoCumple(Guid idPlanMonitoreo, Guid idProducto)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "plan_monitoreo", Tipo = "String", Valor = idPlanMonitoreo.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "producto", Tipo = "String", Valor = idProducto.ToString() });

                List<sp_obligaciones_nocumple> Lista = new List<sp_obligaciones_nocumple>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_obligaciones_nocumple);
                            Lista = conexion.Query<sp_obligaciones_nocumple>().FromSql<sp_obligaciones_nocumple>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<sp_obligaciones_nocumple>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<sp_obligaciones_nocumple>>(ex);
            }
        }


        public ResponseGeneric<List<sp_obligaciones_incumplidas>> ConsultarObligaciones_Incumplidas(Guid idPlanMonitoreo)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "plan_monitoreo", Tipo = "String", Valor = idPlanMonitoreo.ToString() });
                
                List<sp_obligaciones_incumplidas> Lista = new List<sp_obligaciones_incumplidas>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_obligaciones_incumplidas);
                            Lista = conexion.Query<sp_obligaciones_incumplidas>().FromSql<sp_obligaciones_incumplidas>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<sp_obligaciones_incumplidas>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<sp_obligaciones_incumplidas>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> add_archivosPM_(sp_tbl_ArchivosMonitoreo input)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "id_", Tipo = "String", Valor = input.id_.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tbl_plan_moniotoreo_id_", Tipo = "String", Valor = input.tbl_plan_moniotoreo_id_.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tbl_Ubicacion_id_", Tipo = "String", Valor = input.tbl_Ubicacion_id_.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tbl_obligacion_id_", Tipo = "String", Valor = input.tbl_obligacion_id_.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "token_", Tipo = "String", Valor = input.token_.ToString() });


                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_tbl_ArchivosMonitoreo);
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

        public ResponseGeneric<List<File_name>> _sp_download_filename_monitoreo(string monitoreo)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "id_monitoreo", Tipo = "String", Valor = monitoreo.ToString() });
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
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, _sp__documentary_information_download_filename_Monitoreo);
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
