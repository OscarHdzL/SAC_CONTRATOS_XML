using Conexion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.ResponsablesApego;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.IO;
using Utilidades.Log4Net;

namespace AccesoDatos
{
    public class tbl_responsable_apego_contrato_acceso_datos : crud_responsablesapegocontrato
    {
        public BDParametros GeneracionParametros = new BDParametros();
        private readonly ILoggerManager _logger;
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string StoreProcedure = "sp_get_responsableapegocontrato_dropdown";
        string StoreProcedure_RegSolDoc = "sp_get_responsableapegocontrato_regsoldoc";
        string StoreProcedure_Responsabilidad = "sp_get_responsableapegocontrato_responsabilidad";
        string StoreProcedure_add = "sp_contrato_servidor_resp";
        string StoreProcedure_ById = "sp_get_responsableapegocontrato_byid";
        string StoreProcedure_ResponsableContrato = "sp_get_responsableapegocontrato_respcontrato";

        public tbl_responsable_apego_contrato_acceso_datos()
        {
            _logger = new LoggerManager();
        }

        public ResponseGeneric<List<DropDownList>> FillDrop(String contrato)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "contrato", Tipo = "String", Valor = contrato});

                List<DropDownList> Lista = new List<DropDownList>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure);
                            Lista = conexion.Query<DropDownList>().FromSql<DropDownList>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<DropDownList>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_responsable_apego_contrato_regsoldoc>> ConsultarResposables_RegSolDoc(String contrato)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "contrato", Tipo = "String", Valor = contrato });

                List<tbl_responsable_apego_contrato_regsoldoc> Lista = new List<tbl_responsable_apego_contrato_regsoldoc>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_RegSolDoc);
                            Lista = conexion.Query<tbl_responsable_apego_contrato_regsoldoc>().FromSql<tbl_responsable_apego_contrato_regsoldoc>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_responsable_apego_contrato_regsoldoc>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_responsable_apego_contrato_regsoldoc>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_responsable_apego_contrato_responsabilidad>> ConsultarResponsables_Responsabilidades(String contrato)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "contrato", Tipo = "String", Valor = contrato });

                List<tbl_responsable_apego_contrato_responsabilidad> Lista = new List<tbl_responsable_apego_contrato_responsabilidad>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_Responsabilidad);
                            Lista = conexion.Query<tbl_responsable_apego_contrato_responsabilidad>().FromSql<tbl_responsable_apego_contrato_responsabilidad>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_responsable_apego_contrato_responsabilidad>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_responsable_apego_contrato_responsabilidad>>(ex);
            }
        }


        public ResponseGeneric<tbl_responsable_apego_contrato_responsabilidad> ConsultarResponsablesById(String contrato, String responsable)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "contrato", Tipo = "String", Valor = contrato });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "responsable", Tipo = "String", Valor = responsable });

                tbl_responsable_apego_contrato_responsabilidad Lista = new tbl_responsable_apego_contrato_responsabilidad();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_ById);
                            Lista = conexion.Query<tbl_responsable_apego_contrato_responsabilidad>().FromSql<tbl_responsable_apego_contrato_responsabilidad>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<tbl_responsable_apego_contrato_responsabilidad>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<tbl_responsable_apego_contrato_responsabilidad>(ex);
            }
        }

        public ResponseGeneric<tbl_contrato_servidor_resp_esquemapago> ConsultarResposable_EsquemaPago(String contrato)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "contrato", Tipo = "String", Valor = contrato });

                tbl_contrato_servidor_resp_esquemapago Lista = new tbl_contrato_servidor_resp_esquemapago();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_ResponsableContrato);
                            Lista = conexion.Query<tbl_contrato_servidor_resp_esquemapago>().FromSql<tbl_contrato_servidor_resp_esquemapago>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<tbl_contrato_servidor_resp_esquemapago>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<tbl_contrato_servidor_resp_esquemapago>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> add(tbl_contrato_servidor_resp_add responsable)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "Int", Valor = responsable.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = responsable.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_id", Tipo = "String", Valor = responsable.p_tbl_contrato_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_inclusion", Tipo = "String", Valor = responsable.p_inclusion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_estatus", Tipo = "String", Valor = responsable.p_estatus.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_rol_usuario_id", Tipo = "String", Valor = responsable.p_tbl_rol_usuario_id.ToString() });
                
                

                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBDAS
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_add);
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


        public ResponseGeneric<List<Crudresponse>> update(tbl_contrato_servidor_resp_add responsable)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "Int", Valor = responsable.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = responsable.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_id", Tipo = "String", Valor = responsable.p_tbl_contrato_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_inclusion", Tipo = "String", Valor = responsable.p_inclusion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_estatus", Tipo = "String", Valor = responsable.p_estatus.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_rol_usuario_id", Tipo = "String", Valor = responsable.p_tbl_rol_usuario_id.ToString() });


                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_add);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("update", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> delete(tbl_contrato_servidor_resp_add responsable)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "Int", Valor = NullToString(responsable.p_opt) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = NullToString(responsable.p_id) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_id", Tipo = "String", Valor = NullToString(responsable.p_tbl_contrato_id) });
                
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_inclusion", Tipo = "String", Valor = NullToString(responsable.p_inclusion) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_estatus", Tipo = "String", Valor = NullToString(responsable.p_estatus) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_rol_usuario_id", Tipo = "String", Valor = NullToString(responsable.p_tbl_rol_usuario_id) });

                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_add);
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


        public static string NullToString(object Value)
        {

            // Value.ToString() allows for Value being DBNull, but will also convert int, double, etc.
            return Value == null ? "" : Value.ToString();



        }


    }
}
