using Conexion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modelos.Modelos;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AccesoDatos_SistemaAdquisiciones
{
    public class tbl_fallo_acceso_datos
    {
        public BDParametros GeneracionParametros = new BDParametros();
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string sp_get_proposiciones_fallo = "sp_get_proposiciones_fallo";
        string sp_json_contrato = "sp_json_contrato";
        public ResponseGeneric<List<tbl_fallo>> Proposiciones_Get(string id_sol)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "solicitud", Tipo = "String", Valor = id_sol });

                List<tbl_fallo> Lista = new List<tbl_fallo>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_proposiciones_fallo);
                            Lista = conexion.Query<tbl_fallo>().FromSql<tbl_fallo>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_fallo>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_fallo>>(ex);
            }
        }

        public ResponseGeneric<List<sp_config_contrato>> Get_New_Con(string id_sol)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "solicitud", Tipo = "String", Valor = id_sol });

                List<sp_config_contrato> Lista = new List<sp_config_contrato>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, "sp_contrato_x_fallo");
                            Lista = conexion.Query<sp_config_contrato>().FromSql<sp_config_contrato>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<sp_config_contrato>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<sp_config_contrato>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_firmantes>> Get_Firm_Sol(string id_sol)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "solicitud", Tipo = "String", Valor = id_sol });

                List<tbl_firmantes> Lista = new List<tbl_firmantes>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, "sp_firmantes_x_fallo");
                            Lista = conexion.Query<tbl_firmantes>().FromSql<tbl_firmantes>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_firmantes>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_firmantes>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_Responsable>> Get_Res_Sol(string id_sol)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "solicitud", Tipo = "String", Valor = id_sol });

                List<tbl_Responsable> Lista = new List<tbl_Responsable>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, "sp_responsables_x_fallo");
                            Lista = conexion.Query<tbl_Responsable>().FromSql<tbl_Responsable>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_Responsable>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_Responsable>>(ex);
            }
        }

        public ResponseGeneric<tbl_Proveedores> Get_lista_prov(string rfc_licitante)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_rfc_licitante", Tipo = "String", Valor = rfc_licitante });

                tbl_Proveedores Lista = new tbl_Proveedores();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, "sp_licitante_a_proveedor");
                            Lista = conexion.Query<tbl_Proveedores>().FromSql<tbl_Proveedores>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<tbl_Proveedores>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<tbl_Proveedores>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> config(sp_config_contrato contrato)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt ", Tipo = "String", Valor = contrato.p_opt == null ? "" : contrato.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id ", Tipo = "String", Valor = contrato.p_id == null ? "" : contrato.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_contrato_id ", Tipo = "String", Valor = contrato.p_tbl_tipo_contrato_id == null ? "" : contrato.p_tbl_tipo_contrato_id.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_prioridad_id ", Tipo = "String", Valor = contrato.p_tbl_prioridad_id == null ? "" : contrato.p_tbl_prioridad_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_estatus_contrato_id ", Tipo = "String", Valor = contrato.p_tbl_estatus_contrato_id == null ? "" : contrato.p_tbl_estatus_contrato_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_proyecto_id ", Tipo = "String", Valor = contrato.p_tbl_proyecto_id == null ? "" : contrato.p_tbl_proyecto_id.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_procedimiento_id ", Tipo = "String", Valor = contrato.p_tbl_procedimiento_id == null ? "" : contrato.p_tbl_procedimiento_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_numero ", Tipo = "String", Valor = contrato.p_numero == null ? "" : contrato.p_numero.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_objeto ", Tipo = "String", Valor = contrato.p_objeto == null ? "" : contrato.p_objeto.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_firma  ", Tipo = "String", Valor = (contrato.p_fecha_firma == null || contrato.p_fecha_firma.ToString().Contains("01/01/0001")) ? "1800-01-01 00:00:00" : contrato.p_fecha_firma.ToString("yyyy-MM-dd") });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_Iinicio ", Tipo = "String", Valor = (contrato.p_fecha_Iinicio == null || contrato.p_fecha_Iinicio.ToString().Contains("01/01/0001")) ? "1800-01-01 00:00:00" : contrato.p_fecha_Iinicio.ToString("yyyy-MM-dd") });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_fin  ", Tipo = "String", Valor = (contrato.p_fecha_fin == null || contrato.p_fecha_fin.ToString().Contains("01/01/0001")) ? "1800-01-01 00:00:00" : contrato.p_fecha_fin.ToString("yyyy-MM-dd") });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_formalizacion ", Tipo = "String", Valor = (contrato.p_fecha_formalizacion == null || contrato.p_fecha_formalizacion.ToString().Contains("01/01/0001")) ? "1800-01-01 00:00:00" : contrato.p_fecha_formalizacion.ToString("yyyy-MM-dd") });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_ampliacion  ", Tipo = "String", Valor = contrato.p_ampliacion == 0 ? "0" : contrato.p_ampliacion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_requiere_renovacion  ", Tipo = "String", Valor = contrato.p_requiere_renovacion == 0 ? "0" : contrato.p_requiere_renovacion.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_satisfactorio  ", Tipo = "String", Valor = contrato.p_satisfactorio == 0 ? "0" : contrato.p_satisfactorio.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_porc_max_penalizacion  ", Tipo = "String", Valor = contrato.p_porc_max_penalizacion == 0 ? "0" : contrato.p_porc_max_penalizacion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_porc_max_deductivas  ", Tipo = "String", Valor = contrato.p_porc_max_deductivas == 0 ? "0" : contrato.p_porc_max_deductivas.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_presento_garantia  ", Tipo = "String", Valor = contrato.p_presento_garantia == 0 ? "0" : contrato.p_presento_garantia.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_porc_garantia  ", Tipo = "String", Valor = contrato.p_porc_garantia == 0 ? "0" : contrato.p_porc_garantia.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_garantia ", Tipo = "String", Valor = contrato.p_monto_garantia == 0 ? "0" : contrato.p_monto_garantia.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_es_administradora ", Tipo = "String", Valor = contrato.p_es_administradora == 0 ? "0" : contrato.p_es_administradora.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_activo  ", Tipo = "String", Valor = contrato.p_activo == 0 ? "0" : contrato.p_activo.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_token  ", Tipo = "String", Valor = contrato.p_token == null ? "" : contrato.p_token.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_nombre  ", Tipo = "String", Valor = contrato.p_nombre == null ? "" : contrato.p_nombre.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_max_sin_iva  ", Tipo = "String", Valor = contrato.p_monto_max_sin_iva < 0 ? "0" : contrato.p_monto_max_sin_iva.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_min_sin_iva  ", Tipo = "String", Valor = contrato.p_monto_min_sin_iva < 0 ? "0" : contrato.p_monto_min_sin_iva.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_registro", Tipo = "String", Valor = (contrato.p_fecha_registro == null || contrato.p_fecha_registro.ToString().Contains("01/01/0001")) ? "1800-01-01 00:00:00" : contrato.p_fecha_registro.ToString("yyyy-MM-dd") });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_necesidad", Tipo = "String", Valor = contrato.p_necesidad == null ? "" : contrato.p_necesidad.ToString() });

                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, "sp_config_contrato");
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

        public ResponseGeneric<List<Crudresponse>> responsables_contrato(Guid p_tbl_servidor_publico_id, Guid p_tbl_contrato_id, int p_responsable, String opt)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_servidor_publico_id ", Tipo = "String", Valor = p_tbl_servidor_publico_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_id ", Tipo = "String", Valor = p_tbl_contrato_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_responsable ", Tipo = "String", Valor = p_responsable.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "opt ", Tipo = "String", Valor = opt });


                List<Crudresponse> Lista = new List<Crudresponse>();
                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, "sp_responsables_contrato");
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

        public ResponseGeneric<List<Crudresponse>> add_contrato_proveedor(string p_tbl_proveedor_id, Guid p_tbl_contrato_id, String opt)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_id ", Tipo = "String", Valor = p_tbl_contrato_id.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_proveedor_id ", Tipo = "String", Valor = p_tbl_proveedor_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "opt ", Tipo = "String", Valor = opt });



                List<Crudresponse> Lista = new List<Crudresponse>();
                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, "sp_add_contrato_proveedor");
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

        public ResponseGeneric<List<Crudresponse>> comprometer_presupuesto_area(comprometer_presupuesto_area_input obj)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt ", Tipo = "String", Valor = obj.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_id ", Tipo = "String", Valor = obj.p_tbl_contrato_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_dependencia_id ", Tipo = "String", Valor = obj.p_tbl_dependencia_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_capitulo_gasto_id ", Tipo = "String", Valor = obj.p_tbl_capitulo_gasto_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_ejercicio_id ", Tipo = "String", Valor = obj.p_tbl_ejercicio_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_area_id ", Tipo = "String", Valor = obj.p_tbl_area_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_a_comprometer ", Tipo = "String", Valor = obj.p_monto_a_comprometer.ToString() });

                List<Crudresponse> Lista = new List<Crudresponse>();
                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, "sp_comprometer_presupuesto_area");
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
        public ResponseGeneric<List<CrudresponseNum>> get_sp_json_contrato(Guid p_tbl_contrato_id, String p_json_)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_id", Tipo = "String", Valor = p_tbl_contrato_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_json_", Tipo = "String", Valor = p_json_ });



                List<CrudresponseNum> Lista = new List<CrudresponseNum>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_json_contrato);
                            Lista = conexion.Query<CrudresponseNum>().FromSql<CrudresponseNum>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }
                }
                #endregion
                return new ResponseGeneric<List<CrudresponseNum>>(Lista);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<CrudresponseNum>>(ex);
            }
        }

        public ResponseGeneric<List<json_presupuesto_sol>> Get_Json_Pres_Sol(string id_sol)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "solicitud", Tipo = "String", Valor = id_sol });

                List<json_presupuesto_sol> Lista = new List<json_presupuesto_sol>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, "sp_get_json_pres_by_solicitud");
                            Lista = conexion.Query<json_presupuesto_sol>().FromSql<json_presupuesto_sol>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<json_presupuesto_sol>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<json_presupuesto_sol>>(ex);
            }
        }
    }
}
