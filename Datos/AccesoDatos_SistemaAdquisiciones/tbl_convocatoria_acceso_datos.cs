using Conexion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.IO;

namespace AccesoDatos_SistemaAdquisiciones
{
    public class tbl_convocatoria_acceso_datos
    {
        public BDParametros GeneracionParametros = new BDParametros();
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string sp_get_convocatoria = "sp_get_convocatoria";
        string sp_convocatoria = "sp_convocatoria";
        string sp_get_areasdependencia_dropdown = "sp_get_areasdependencia_dropdown";
        string sp_get_servidorpub_by_area = "sp_get_servidorpub_by_area";
        string StoreProcedureTipoOblig = "sp_get_tiposobligacion";
        string sp_convocatoria_obligacion = "sp_convocatoria_obligacion";
        string sp_get_lista_convocatoria_obl = "sp_get_lista_convocatoria_obl";
        string sp_convocatoria_responsable = "sp_convocatoria_responsable";
        string sp_get_responsable_convocatoria = "sp_get_responsable_convocatoria";
        string sp_convocatoria_penalizacion = "sp_convocatoria_penalizacion";
        string sp_get_convocatoria_penalizaciones = "sp_get_convocatoria_penalizaciones";
        string sp_convocatoria_condicion = "sp_convocatoria_condicion";
        string sp_get_convocatoria_condicion = "sp_get_convocatoria_condicion";
        string sp_convocatoria_pago = "sp_convocatoria_pago";
        string sp_get_convocatoria_pagos = "sp_get_convocatoria_pagos";
        string sp_convocatoria_criterio = "sp_convocatoria_criterio";
        string sp_get_convocatoria_criterio = "sp_get_convocatoria_criterio";
        string sp_get_tipo_criterio_convocatoria = "sp_get_tipo_criterio_convocatoria";
        string sp_convocatoria_documento = "sp_convocatoria_documento";
        string sp_get_convocatoria_documento = "sp_get_convocatoria_documento";
        string sp_get_tipo_documento_conv = "sp_get_tipo_documento_conv";

        public ResponseGeneric<List<tbl_convocatoria>> get_convocatoria_by_solicitud(String p_tbl_solicitud_id)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_solicitud_id", Tipo = "String", Valor = p_tbl_solicitud_id.ToString() });


                List<tbl_convocatoria> Lista = new List<tbl_convocatoria>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_convocatoria);
                            Lista = conexion.Query<tbl_convocatoria>().FromSql<tbl_convocatoria>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_convocatoria>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_convocatoria>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> add(tbl_convocatoria_add tbl_convocatoria)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = tbl_convocatoria.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = tbl_convocatoria.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_solicitud_id", Tipo = "String", Valor = tbl_convocatoria.p_tbl_solicitud_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_servidor_publico_id", Tipo = "String", Valor = tbl_convocatoria.p_tbl_servidor_publico_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_folio", Tipo = "String", Valor = tbl_convocatoria.p_folio.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_procedimiento", Tipo = "String", Valor = tbl_convocatoria.p_procedimiento.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_suscripcion", Tipo = "String", Valor = tbl_convocatoria.p_fecha_suscripcion.ToString("yyyy-MM-ddTHH:mm:ss") });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_inclusion", Tipo = "String", Valor = tbl_convocatoria.p_inclusion.ToString("yyyy-MM-ddTHH:mm:ss") });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_folio_publicacion", Tipo = "String", Valor = tbl_convocatoria.p_folio_publicacion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tipo_publicacion", Tipo = "String", Valor = tbl_convocatoria.p_tipo_publicacion.ToString() });


                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_convocatoria);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<DropDownList>> get_areas_by_dep(String p_id_dependencia)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "dependencia", Tipo = "String", Valor = p_id_dependencia.ToString() });


                List<DropDownList> Lista = new List<DropDownList>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_areasdependencia_dropdown);
                            Lista = conexion.Query<DropDownList>().FromSql<DropDownList>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<DropDownList>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }
        public ResponseGeneric<List<DropDownList>> get_servidor_by_dep(String p_area_id)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_area_id", Tipo = "String", Valor = p_area_id.ToString() });


                List<DropDownList> Lista = new List<DropDownList>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_servidorpub_by_area);
                            Lista = conexion.Query<DropDownList>().FromSql<DropDownList>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<DropDownList>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_tipo_obligacion>> Get_tipo_obligaciones()
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                List<tbl_tipo_obligacion> Lista = new List<tbl_tipo_obligacion>();
                #endregion
                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedureTipoOblig);
                            Lista = conexion.Query<tbl_tipo_obligacion>().FromSql<tbl_tipo_obligacion>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }
                }
                #endregion
                return new ResponseGeneric<List<tbl_tipo_obligacion>>(Lista);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_tipo_obligacion>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> add_obligacion(tbl_obligacion_conv_add tbl_Obligacion_)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = tbl_Obligacion_.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = tbl_Obligacion_.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_convocatoria_id", Tipo = "String", Valor = tbl_Obligacion_.p_tbl_convocatoria_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_obligacion_id", Tipo = "String", Valor = tbl_Obligacion_.p_tbl_tipo_obligacion_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_estatus_obligacion_id", Tipo = "String", Valor = tbl_Obligacion_.p_tbl_estatus_obligacion_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_obligacion", Tipo = "String", Valor = tbl_Obligacion_.p_obligacion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_inclusion", Tipo = "String", Valor = tbl_Obligacion_.p_inclusion.ToString("yyyy-MM-ddTHH:mm:ss") });

                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_convocatoria_obligacion);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_convocatoria_obligaciones>> get_convocatoria_obl(String id_solicitud)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_convocatoria_id", Tipo = "String", Valor = id_solicitud.ToString() });


                List<tbl_convocatoria_obligaciones> Lista = new List<tbl_convocatoria_obligaciones>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_lista_convocatoria_obl);
                            Lista = conexion.Query<tbl_convocatoria_obligaciones>().FromSql<tbl_convocatoria_obligaciones>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_convocatoria_obligaciones>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_convocatoria_obligaciones>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> add_responsable(tbl_responsable_convocatoria tbl_Responsable_)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = tbl_Responsable_.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = tbl_Responsable_.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_servidor_publico_id", Tipo = "String", Valor = tbl_Responsable_.p_tbl_servidor_publico_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_token", Tipo = "String", Valor = tbl_Responsable_.p_token.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_convocatoria_id", Tipo = "String", Valor = tbl_Responsable_.p_tbl_convocatoria_id.ToString() });

                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_convocatoria_responsable);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_responsable_convocatoria_lista>> get_convocatoria_responsable(String id_solicitud)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_solicitud_id", Tipo = "String", Valor = id_solicitud.ToString() });


                List<tbl_responsable_convocatoria_lista> Lista = new List<tbl_responsable_convocatoria_lista>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_responsable_convocatoria);
                            Lista = conexion.Query<tbl_responsable_convocatoria_lista>().FromSql<tbl_responsable_convocatoria_lista>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_responsable_convocatoria_lista>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_responsable_convocatoria_lista>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> add_penalizacion(tbl_convocatoria_penalizacion _Penalizacion)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = _Penalizacion.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = _Penalizacion.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_convocatoria_id", Tipo = "String", Valor = _Penalizacion.p_tbl_convocatoria_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_estatus_obligacion_id", Tipo = "String", Valor = _Penalizacion.p_tbl_estatus_obligacion_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_penalizacion", Tipo = "String", Valor = _Penalizacion.p_penalizacion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_porcentaje_penalizacion", Tipo = "String", Valor = _Penalizacion.p_porcentaje_penalizacion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_porcentaje_deductiva", Tipo = "String", Valor = _Penalizacion.p_porcentaje_deductiva.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_porcentaje_garantia", Tipo = "String", Valor = _Penalizacion.p_porcentaje_garantia.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_garantia", Tipo = "String", Valor = _Penalizacion.p_monto_garantia.ToString() });

                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_convocatoria_penalizacion);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_convocatoria_penalizacion_lista>> get_convocatoria_penalizacion(String id_solicitud)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_solicitud_id", Tipo = "String", Valor = id_solicitud.ToString() });


                List<tbl_convocatoria_penalizacion_lista> Lista = new List<tbl_convocatoria_penalizacion_lista>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_convocatoria_penalizaciones);
                            Lista = conexion.Query<tbl_convocatoria_penalizacion_lista>().FromSql<tbl_convocatoria_penalizacion_lista>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_convocatoria_penalizacion_lista>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_convocatoria_penalizacion_lista>>(ex);
            }
        } 
        public ResponseGeneric<List<Crudresponse>> add_condicion(tbl_convocatoria_condicion _condicion)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = _condicion.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = _condicion.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_convocatoria_id", Tipo = "String", Valor = _condicion.p_tbl_convocatoria_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_estatus_obligacion_id", Tipo = "String", Valor = _condicion.p_tbl_estatus_obligacion_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_periodo", Tipo = "String", Valor = _condicion.p_periodo.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_condicion", Tipo = "String", Valor = _condicion.p_condicion.ToString() });


                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_convocatoria_condicion);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_convocatoria_condicion_lista>> get_convocatoria_condicion(String id_solicitud)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_solicitud_id", Tipo = "String", Valor = id_solicitud.ToString() });


                List<tbl_convocatoria_condicion_lista> Lista = new List<tbl_convocatoria_condicion_lista>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_convocatoria_condicion);
                            Lista = conexion.Query<tbl_convocatoria_condicion_lista>().FromSql<tbl_convocatoria_condicion_lista>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_convocatoria_condicion_lista>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_convocatoria_condicion_lista>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> add_pago(tbl_convocatoria_pago _Pago)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = _Pago.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = _Pago.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_convocatoria_id", Tipo = "String", Valor = _Pago.p_tbl_convocatoria_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_estatus_obligacion_id", Tipo = "String", Valor = _Pago.p_tbl_estatus_obligacion_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_condicion_pago", Tipo = "String", Valor = _Pago.p_condicion_pago.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_metodo_pago", Tipo = "String", Valor = _Pago.p_metodo_pago.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tipo_pago", Tipo = "String", Valor = _Pago.p_tipo_pago.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_porcentaje_cantidad", Tipo = "String", Valor = _Pago.p_porcentaje_cantidad.ToString() });

                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_convocatoria_pago);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_convocatoria_pago_lista>> get_convocatoria_pagos(String id_solicitud)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_solicitud_id", Tipo = "String", Valor = id_solicitud.ToString() });


                List<tbl_convocatoria_pago_lista> Lista = new List<tbl_convocatoria_pago_lista>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_convocatoria_pagos);
                            Lista = conexion.Query<tbl_convocatoria_pago_lista>().FromSql<tbl_convocatoria_pago_lista>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_convocatoria_pago_lista>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_convocatoria_pago_lista>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> add_criterio(tbl_convocatoria_criterio _Criterio)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = _Criterio.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = _Criterio.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_convocatoria_id", Tipo = "String", Valor = _Criterio.p_tbl_convocatoria_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipocriterio_id", Tipo = "String", Valor = _Criterio.p_tbl_tipocriterio_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_estatus_obligacion_id", Tipo = "String", Valor = _Criterio.p_tbl_estatus_obligacion_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_criterio", Tipo = "String", Valor = _Criterio.p_criterio.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_evaluacion", Tipo = "String", Valor = _Criterio.p_evaluacion.ToString() });


                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_convocatoria_criterio);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_convocatoria_criterio_lista>> get_convocatoria_criterios(String id_solicitud)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_solicitud_id", Tipo = "String", Valor = id_solicitud.ToString() });


                List<tbl_convocatoria_criterio_lista> Lista = new List<tbl_convocatoria_criterio_lista>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_convocatoria_criterio);
                            Lista = conexion.Query<tbl_convocatoria_criterio_lista>().FromSql<tbl_convocatoria_criterio_lista>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_convocatoria_criterio_lista>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_convocatoria_criterio_lista>>(ex);
            }
        }
        public ResponseGeneric<List<DropDownList>> get_tipo_criterios()
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
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_tipo_criterio_convocatoria);
                            Lista = conexion.Query<DropDownList>().FromSql<DropDownList>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<DropDownList>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> add_documento(tbl_documento_validacion tbl_Documento_)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = tbl_Documento_.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = tbl_Documento_.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_convocatoria_id", Tipo = "String", Valor = tbl_Documento_.p_tbl_convocatoria_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_documento_id", Tipo = "String", Valor = tbl_Documento_.p_tbl_tipo_documento_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_justificacion", Tipo = "String", Valor = tbl_Documento_.p_justificacion.ToString() });
                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_convocatoria_documento);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_documento_validacion_lista>> get_convocatoria_documentos(String id_solicitud)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_solicitud_id", Tipo = "String", Valor = id_solicitud.ToString() });


                List<tbl_documento_validacion_lista> Lista = new List<tbl_documento_validacion_lista>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_convocatoria_documento);
                            Lista = conexion.Query<tbl_documento_validacion_lista>().FromSql<tbl_documento_validacion_lista>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_documento_validacion_lista>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_documento_validacion_lista>>(ex);
            }
        }
        public ResponseGeneric<List<DropDownList>> get_tipo_documento(String p_tbl_instancia_id)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_instancia_id", Tipo = "String", Valor = p_tbl_instancia_id.ToString() });
                List<DropDownList> Lista = new List<DropDownList>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_tipo_documento_conv);
                            Lista = conexion.Query<DropDownList>().FromSql<DropDownList>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<DropDownList>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }
    }
}
