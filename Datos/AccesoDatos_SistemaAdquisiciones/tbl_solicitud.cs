using System;

using Modelos.Interfaz;
using Modelos.Modelos;
using System.Collections.Generic;
using Conexion;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.IO;
using Modelos.Response;
using Modelos.Modelos.ServidoresPublicos;

namespace AccesoDatos_SistemaAdquisiciones
{
    public class sp_solicitud
    {
        public BDParametros GeneracionParametros = new BDParametros();
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string StoreProcedure = "sp_solicitud";
        string sp_get_solicitud = "sp_get_solicitud";
        string sp_get_solicitudes_rolusuario_estatus = "sp_get_solicitudes_rolusuario_estatus";
        string sp_get_contador_solicitudes_rolusuario_estatus = "sp_get_contador_solicitudes_rolusuario_estatus";



        public ResponseGeneric<List<sp_solicitud>> Consultar(sp_solicitud_en p_sp_solicitud)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = p_sp_solicitud.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = p_sp_solicitud.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_num_solicitud", Tipo = "String", Valor = p_sp_solicitud.p_num_solicitud.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_solicitud_id", Tipo = "String", Valor = p_sp_solicitud.p_tbl_tipo_solicitud_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_contrato_id", Tipo = "String", Valor = p_sp_solicitud.p_tbl_tipo_contrato_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_solicitud", Tipo = "String", Valor = p_sp_solicitud.p_fecha_solicitud.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_elaboro", Tipo = "String", Valor = p_sp_solicitud.p_elaboro.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_dependencia_id", Tipo = "String", Valor = p_sp_solicitud.p_tbl_dependencia_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_area_id", Tipo = "String", Valor = p_sp_solicitud.p_tbl_area_id.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_proyecto_id", Tipo = "String", Valor = p_sp_solicitud.p_tbl_proyecto_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_descripcion", Tipo = "String", Valor = p_sp_solicitud.p_descripcion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_solicitud", Tipo = "String", Valor = p_sp_solicitud.p_monto_solicitud.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_autorizado", Tipo = "String", Valor = p_sp_solicitud.p_monto_autorizado.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_comentarios", Tipo = "String", Valor = p_sp_solicitud.p_comentarios.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_token_solicitante", Tipo = "String", Valor = p_sp_solicitud.p_token_solicitante.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_token_autorizacion", Tipo = "String", Valor = p_sp_solicitud.p_token_autorizacion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_estatus_solicitud_id", Tipo = "String", Valor = p_sp_solicitud.p_tbl_estatus_solicitud_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_inclusion", Tipo = "String", Valor = p_sp_solicitud.p_inclusion.ToString() });

                List<sp_solicitud> Lista = new List<sp_solicitud>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure);
                            Lista = conexion.Query<sp_solicitud>().FromSql<sp_solicitud>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;

                            break;
                    }

                }
                #endregion

                return new ResponseGeneric<List<sp_solicitud>>(Lista);

            }
            catch (Exception ex)
            {

                return new ResponseGeneric<List<sp_solicitud>>(ex);
            }
        }

        public ResponseGeneric<List<CrudresponseIdentificador>> Guardar(sp_solicitud_en p_sp_solicitud)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = p_sp_solicitud.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = p_sp_solicitud.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_num_solicitud", Tipo = "String", Valor = p_sp_solicitud.p_num_solicitud.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_solicitud_id", Tipo = "String", Valor = p_sp_solicitud.p_tbl_tipo_solicitud_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_contrato_id", Tipo = "String", Valor = p_sp_solicitud.p_tbl_tipo_contrato_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_solicitud", Tipo = "String", Valor = p_sp_solicitud.p_fecha_solicitud.ToString("yyyy-MM-ddTHH:mm:ss") });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_elaboro", Tipo = "String", Valor = p_sp_solicitud.p_elaboro.ToString() });
                //ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_dependencia_id", Tipo = "String", Valor = p_sp_solicitud.p_tbl_dependencia_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_area_id", Tipo = "String", Valor = p_sp_solicitud.p_tbl_area_id.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_proyecto_id", Tipo = "String", Valor = p_sp_solicitud.p_tbl_proyecto_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_descripcion", Tipo = "String", Valor = p_sp_solicitud.p_descripcion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_solicitud", Tipo = "String", Valor = p_sp_solicitud.p_monto_solicitud.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_autorizado", Tipo = "String", Valor = p_sp_solicitud.p_monto_autorizado.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_comentarios", Tipo = "String", Valor = p_sp_solicitud.p_comentarios.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_token_solicitante", Tipo = "String", Valor = p_sp_solicitud.p_token_solicitante.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_token_autorizacion", Tipo = "String", Valor = p_sp_solicitud.p_token_autorizacion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_estatus_solicitud_id", Tipo = "String", Valor = p_sp_solicitud.p_tbl_estatus_solicitud_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_inclusion", Tipo = "String", Valor = p_sp_solicitud.p_inclusion.ToString("yyyy-MM-ddTHH:mm:ss") });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_json_pres", Tipo = "String", Valor = p_sp_solicitud.p_json_pres.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_nombre_bien_servicio", Tipo = "String", Valor = p_sp_solicitud.p_nombre_bien_servicio.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_visitasitio", Tipo = "String", Valor = p_sp_solicitud.p_visitasitio.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_mesa_validacion", Tipo = "String", Valor = p_sp_solicitud.p_mesa_validacion.ToString() });

                List<CrudresponseIdentificador> Lista = new List<CrudresponseIdentificador>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure);
                            Lista = conexion.Query<CrudresponseIdentificador>().FromSql<CrudresponseIdentificador>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;

                            break;
                    }

                }
                #endregion

                return new ResponseGeneric<List<CrudresponseIdentificador>>(Lista);

            }
            catch (Exception ex)
            {

                return new ResponseGeneric<List<CrudresponseIdentificador>>(ex);
            }
        }


        public ResponseGeneric<List<Crudresponse>> Update(sp_solicitud_en p_sp_solicitud)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = p_sp_solicitud.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = p_sp_solicitud.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_num_solicitud", Tipo = "String", Valor = p_sp_solicitud.p_num_solicitud.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_solicitud_id", Tipo = "String", Valor = p_sp_solicitud.p_tbl_tipo_solicitud_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_contrato_id", Tipo = "String", Valor = p_sp_solicitud.p_tbl_tipo_contrato_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_solicitud", Tipo = "String", Valor = p_sp_solicitud.p_fecha_solicitud.ToString("yyyy-MM-ddTHH:mm:ss") });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_elaboro", Tipo = "String", Valor = p_sp_solicitud.p_elaboro.ToString() });
                //ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_dependencia_id", Tipo = "String", Valor = p_sp_solicitud.p_tbl_dependencia_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_area_id", Tipo = "String", Valor = p_sp_solicitud.p_tbl_area_id.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_proyecto_id", Tipo = "String", Valor = p_sp_solicitud.p_tbl_proyecto_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_descripcion", Tipo = "String", Valor = p_sp_solicitud.p_descripcion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_solicitud", Tipo = "String", Valor = p_sp_solicitud.p_monto_solicitud.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_autorizado", Tipo = "String", Valor = p_sp_solicitud.p_monto_autorizado.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_comentarios", Tipo = "String", Valor = p_sp_solicitud.p_comentarios.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_token_solicitante", Tipo = "String", Valor = p_sp_solicitud.p_token_solicitante.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_token_autorizacion", Tipo = "String", Valor = p_sp_solicitud.p_token_autorizacion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_estatus_solicitud_id", Tipo = "String", Valor = p_sp_solicitud.p_tbl_estatus_solicitud_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_inclusion", Tipo = "String", Valor = p_sp_solicitud.p_inclusion.ToString("yyyy-MM-ddTHH:mm:ss") });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_json_pres", Tipo = "String", Valor = p_sp_solicitud.p_json_pres.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_nombre_bien_servicio", Tipo = "String", Valor = p_sp_solicitud.p_nombre_bien_servicio.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_visitasitio", Tipo = "String", Valor = p_sp_solicitud.p_visitasitio.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_mesa_validacion", Tipo = "String", Valor = p_sp_solicitud.p_mesa_validacion.ToString() });

                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure);
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

        public ResponseGeneric<List<tbl_solicitud>> getSolicitudes_rolusuario_estatus(String rol_usuario, String sigla_estatus)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "rol_usuario", Tipo = "String", Valor = rol_usuario });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "sigla_estatus", Tipo = "String", Valor = sigla_estatus });

                List<tbl_solicitud> Lista = new List<tbl_solicitud>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_solicitudes_rolusuario_estatus);
                            Lista = conexion.Query<tbl_solicitud>().FromSql<tbl_solicitud>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_solicitud>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_solicitud>>(ex);
            }
        }

        

       public ResponseGeneric<contador_solicitud> get_contador_Solicitudes_rolusuario_estatus(String rol_usuario, String sigla_estatus)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "rol_usuario", Tipo = "String", Valor = rol_usuario });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "sigla_estatus", Tipo = "String", Valor = sigla_estatus });

                contador_solicitud Lista = new contador_solicitud();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_contador_solicitudes_rolusuario_estatus);
                            Lista = conexion.Query<contador_solicitud>().FromSql<contador_solicitud>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<contador_solicitud>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<contador_solicitud>(ex);
            }
        }

        public ResponseGeneric<tbl_solicitud> getSolicitud(String id_solicitud)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = id_solicitud });
                

               tbl_solicitud Lista = new tbl_solicitud();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_solicitud);
                            Lista = conexion.Query<tbl_solicitud>().FromSql<tbl_solicitud>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<tbl_solicitud>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<tbl_solicitud>(ex);
            }
        }

        public ResponseGeneric<List<tbl_tipo_documento>> Get_lista_tipo_documento(String id_instancia)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_instancia_id", Tipo = "String", Valor = id_instancia.ToString() });
                List<tbl_tipo_documento> Lista = new List<tbl_tipo_documento>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, "sp_get_tipo_documento");
                            Lista = conexion.Query<tbl_tipo_documento>().FromSql<tbl_tipo_documento>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_tipo_documento>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_tipo_documento>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_documento_adjunto_solicitud>> Get_docts_Solicitud(String id_solicitud)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id_solicitud", Tipo = "String", Valor = id_solicitud.ToString() });
                List<tbl_documento_adjunto_solicitud> Lista = new List<tbl_documento_adjunto_solicitud>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, "sp_get_documentos_adjuntos_solicitud");
                            Lista = conexion.Query<tbl_documento_adjunto_solicitud>().FromSql<tbl_documento_adjunto_solicitud>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_documento_adjunto_solicitud>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_documento_adjunto_solicitud>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_tipo_dictamen>> Get_tipo_dictamen(String id_dependencia)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_dependencia", Tipo = "String", Valor = id_dependencia.ToString() });
                List<tbl_tipo_dictamen> Lista = new List<tbl_tipo_dictamen>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, "sp_get_tipo_dictamen");
                            Lista = conexion.Query<tbl_tipo_dictamen>().FromSql<tbl_tipo_dictamen>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_tipo_dictamen>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_tipo_dictamen>>(ex);
            }
        }

        public ResponseGeneric<tbl_solicitud_suficiencia> Get_Solicitud_suficiencia_det(String id_solicitud)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = id_solicitud.ToString() });
                tbl_solicitud_suficiencia Lista = new tbl_solicitud_suficiencia();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, "sp_get_solicitud_suficiencia");
                            Lista = conexion.Query<tbl_solicitud_suficiencia>().FromSql<tbl_solicitud_suficiencia>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<tbl_solicitud_suficiencia>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<tbl_solicitud_suficiencia>(ex);
            }
        }

        public ResponseGeneric<tbl_solicitud_estudio_mercado> Get_Solicitud_Est_Merc(String id_solicitud)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = id_solicitud.ToString() });
                tbl_solicitud_estudio_mercado Lista = new tbl_solicitud_estudio_mercado();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, "sp_get_solicitud_suficiencia_est_merc");
                            Lista = conexion.Query<tbl_solicitud_estudio_mercado>().FromSql<tbl_solicitud_estudio_mercado>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<tbl_solicitud_estudio_mercado>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<tbl_solicitud_estudio_mercado>(ex);
            }
        }

        public ResponseGeneric<List<DropDownList>> GetFuente_financiamiento(string id_dep)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = id_dep.ToString() });
                List<DropDownList> Lista = new List<DropDownList>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, "sp_get_fuente_financiamiento_dropdown");
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

        public ResponseGeneric<List<DropDownList>> Get_area_turnar(string id_dep)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_dependencia", Tipo = "String", Valor = id_dep.ToString() });
                List<DropDownList> Lista = new List<DropDownList>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, "sp_get_area_turnar_dependencia_dropdown");
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

        public ResponseGeneric<List<Crudresponse>> AddDocumentoAdjunto(tbl_solicitud_documento_adjunto _tbl_solicitud_documento_adjunto)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "int", Valor = "2" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = _tbl_solicitud_documento_adjunto.id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_token", Tipo = "String", Valor = _tbl_solicitud_documento_adjunto.token.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_documento_id", Tipo = "String", Valor = _tbl_solicitud_documento_adjunto.tbl_tipo_documento_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_solicitud_id", Tipo = "String", Valor = _tbl_solicitud_documento_adjunto.tbl_solicitud_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_nom_documento", Tipo = "String", Valor = _tbl_solicitud_documento_adjunto.nom_documento.ToString() });




                List<Crudresponse> Lista = new List<Crudresponse>();
                #endregion
                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, "sp_solicitud_documento_adjunto");
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
        public ResponseGeneric<List<Crudresponse>> Delete_dcto_adj(string id_docto)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "int", Valor = "4" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = id_docto.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_token", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_documento_id", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_solicitud_id", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_nom_documento", Tipo = "String", Valor = "" });

                List<Crudresponse> Lista = new List<Crudresponse>();
                #endregion
                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, "sp_solicitud_documento_adjunto");
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

        public ResponseGeneric<List<Crudresponse>> add_suficiencia(tbl_suficiencia_add entidad)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "int", Valor = entidad.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = entidad.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_solicitud_id", Tipo = "String", Valor = entidad.p_tbl_solicitud_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_autorizacion", Tipo = "String", Valor = entidad.p_fecha_autorizacion.ToString("yyyy-MM-dd") });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_folio_autorizacion", Tipo = "String", Valor = entidad.p_folio_autorizacion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_autorizo", Tipo = "String", Valor = entidad.p_autorizo.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_fuente_financiamiento_id", Tipo = "String", Valor = entidad.p_tbl_fuente_financiamiento_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_comentarios", Tipo = "String", Valor = entidad.p_comentarios.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_sigla", Tipo = "String", Valor = entidad.p_sigla.ToString() });




                List<Crudresponse> Lista = new List<Crudresponse>();
                #endregion
                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, "sp_suficiencia");
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

        public ResponseGeneric<List<Crudresponse>> add_estudio_mercado(tbl_estudio_mercado entidad)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "int", Valor = "2" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = entidad.id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_solicitud_id", Tipo = "String", Valor = entidad.tbl_solicitud_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_evento_estudio", Tipo = "String", Valor = entidad.fecha_evento_estudio.ToString("yyyy-MM-dd") });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_usuario_id", Tipo = "String", Valor = entidad.tbl_usuario_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_sigla", Tipo = "String", Valor = entidad.sigla_estatus.ToString() });

                List<Crudresponse> Lista = new List<Crudresponse>();
                #endregion
                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, "sp_estudio_mercado");
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

        public ResponseGeneric<List<Crudresponse>> add_dictamen_solicitud(tbl_dictamen entidad)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "int", Valor = "2" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = entidad.id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_solicitud_id", Tipo = "String", Valor = entidad.tbl_solicitud_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_evento_estudio", Tipo = "String", Valor = entidad.fecha_evento_dictamen.ToString("yyyy-MM-dd") });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_usuario_id", Tipo = "String", Valor = entidad.tbl_usuario_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_dictamen_id", Tipo = "String", Valor = entidad.tbl_tipo_dictamen_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_folio_dictamen", Tipo = "String", Valor = entidad.folio_dictamen.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_sigla", Tipo = "String", Valor = entidad.sigla_estatus.ToString() });

                List<Crudresponse> Lista = new List<Crudresponse>();
                #endregion
                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, "sp_dictamen_solicitud");
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

        public ResponseGeneric<List<Crudresponse>> update_sol_metodo(string parametro, string id_sol, string variable)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "parametro", Tipo = "int", Valor = parametro.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "id_sol", Tipo = "String", Valor = id_sol.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_variable", Tipo = "String", Valor = variable.ToString() });



                List<Crudresponse> Lista = new List<Crudresponse>();
                #endregion
                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, "sp_update_solicitud_metodo");
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


        public ResponseGeneric<List<Crudresponse>> get_partidas_montos_area_unitario(string Dep, string area, string cap)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_dependencia_id", Tipo = "String", Valor = Dep.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_area_id", Tipo = "String", Valor = area.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_capitulo_gasto_id", Tipo = "String", Valor = cap.ToString() });

                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, "sp_get_partidas_montos_area_unitario");
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

    }
}
