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
    public class tbl_acuerdo_acceso_datos : crud_acuerdos
    {
        private readonly ILoggerManager _logger;
        public BDParametros GeneracionParametros = new BDParametros();
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string StoreProcedureLista = "sp_get_acuerdo";
        string StoreProcedureEdit = "sp_get_acuerdoEdit";
        string StoreProcedureEditRC = "sp_get_acuerdoEdit_rc";
        //string StoreProcedure = "sp_acuerdo";
        string StoreProcedure = "sp_acuerdo_rol";
        string StoreProcedureTipos = "sp_get_tipoacuerdo_dropdown";
        string StoreProcedureAcuerdos = "sp_tipo_acuerdo";

        public tbl_acuerdo_acceso_datos()
        {
            _logger = new LoggerManager();
        }
        public ResponseGeneric<List<tbl_acuerdos_lista>> Consultar(String contrato)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_contrato_id", Tipo = "String", Valor = contrato.ToString() });

                List<tbl_acuerdos_lista> Lista = new List<tbl_acuerdos_lista>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedureLista);
                            Lista = conexion.Query<tbl_acuerdos_lista>().FromSql<tbl_acuerdos_lista>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_acuerdos_lista>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_acuerdos_lista>>(ex);
            }
        }

        public ResponseGeneric<tbl_acuerdos_lista> ConsultarAcuerdo(String acuerdo)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_acuerdo_id", Tipo = "String", Valor = acuerdo.ToString() });

                tbl_acuerdos_lista Lista = new tbl_acuerdos_lista();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedureEdit);
                            Lista = conexion.Query<tbl_acuerdos_lista>().FromSql<tbl_acuerdos_lista>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<tbl_acuerdos_lista>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("ConsultarAcuerdo", ex);
                return new ResponseGeneric<tbl_acuerdos_lista>(ex);
            }
        }

        public ResponseGeneric<tbl_acuerdos_lista> ConsultarAcuerdoRC(String acuerdo, String contrato)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_acuerdo_id", Tipo = "String", Valor = acuerdo.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_contrato_id", Tipo = "String", Valor = contrato.ToString() });

                tbl_acuerdos_lista Lista = new tbl_acuerdos_lista();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedureEditRC);
                            Lista = conexion.Query<tbl_acuerdos_lista>().FromSql<tbl_acuerdos_lista>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<tbl_acuerdos_lista>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("ConsultarAcuerdo", ex);
                return new ResponseGeneric<tbl_acuerdos_lista>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> add(tbl_acuerdo_add acuerdo)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "Int", Valor = acuerdo.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = acuerdo.p_id.ToString() });   
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_servidor_resp_id", Tipo = "String", Valor = acuerdo.p_tbl_contrato_servidor_resp_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_id", Tipo = "String", Valor = acuerdo.p_tbl_contrato_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_acuerdo_id", Tipo = "String", Valor = acuerdo.p_tbl_tipo_acuerdo_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_acuerdo", Tipo = "String", Valor = acuerdo.p_acuerdo.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_registro", Tipo = "String", Valor = acuerdo.p_fecha_registro.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_compromiso", Tipo = "String", Valor = acuerdo.p_fecha_compromiso.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_cierre", Tipo = "String", Valor = acuerdo.p_fecha_cierre.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_estatus_acuerdo", Tipo = "String", Valor = acuerdo.p_estatus_acuerdo.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_comentario", Tipo = "String", Valor = acuerdo.p_comentario.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_estatus", Tipo = "String", Valor = acuerdo.p_estatus.ToString() });

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
                _logger.LogError("Add", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> update(tbl_acuerdo_add acuerdo)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "Int", Valor = acuerdo.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = acuerdo.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_servidor_resp_id", Tipo = "String", Valor = acuerdo.p_tbl_contrato_servidor_resp_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_id", Tipo = "String", Valor = acuerdo.p_tbl_contrato_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_acuerdo_id", Tipo = "String", Valor = acuerdo.p_tbl_tipo_acuerdo_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_acuerdo", Tipo = "String", Valor = acuerdo.p_acuerdo.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_registro", Tipo = "String", Valor = acuerdo.p_fecha_registro.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_compromiso", Tipo = "String", Valor = acuerdo.p_fecha_compromiso.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_cierre", Tipo = "String", Valor = acuerdo.p_fecha_cierre.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_estatus_acuerdo", Tipo = "String", Valor = acuerdo.p_estatus_acuerdo.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_comentario", Tipo = "String", Valor = acuerdo.p_comentario.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_estatus", Tipo = "String", Valor = acuerdo.p_estatus.ToString() });

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
                _logger.LogError("Update", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> delete(tbl_acuerdo_add acuerdo)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "Int", Valor = NullToString(acuerdo.p_opt)  });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = NullToString(acuerdo.p_id) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_servidor_resp_id", Tipo = "String", Valor = NullToString(acuerdo.p_tbl_contrato_servidor_resp_id) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_id", Tipo = "String", Valor = NullToString(acuerdo.p_tbl_contrato_id) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_acuerdo_id", Tipo = "String", Valor = NullToString(acuerdo.p_tbl_tipo_acuerdo_id) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_acuerdo", Tipo = "String", Valor = NullToString(acuerdo.p_acuerdo) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_registro", Tipo = "String", Valor = NullToString(acuerdo.p_fecha_registro) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_compromiso", Tipo = "String", Valor = NullToString(acuerdo.p_fecha_compromiso) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_cierre", Tipo = "String", Valor = NullToString(acuerdo.p_fecha_cierre) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_estatus_acuerdo", Tipo = "String", Valor = NullToString(acuerdo.p_estatus_acuerdo) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_comentario", Tipo = "String", Valor = NullToString(acuerdo.p_comentario) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_estatus", Tipo = "String", Valor = NullToString(acuerdo.p_estatus) });

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
                _logger.LogError("Delete", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }

        }

        public ResponseGeneric<List<DropDownList>> ConsultarTipos()
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                //ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_contrato_id", Tipo = "String", Valor = contrato.ToString() });

                List<DropDownList> Lista = new List<DropDownList>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedureTipos);
                            Lista = conexion.Query<DropDownList>().FromSql<DropDownList>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<DropDownList>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("ConsultarTipos", ex);
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Add_tipo_acuerdo(tbl_tipo_acuerdo_add tbl_tipo_acuerdo_add)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = tbl_tipo_acuerdo_add.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = tbl_tipo_acuerdo_add.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tipo_acuerdo", Tipo = "String", Valor = tbl_tipo_acuerdo_add.p_tipo_acuerdo.ToString() });
                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedureAcuerdos);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("AddTipoAcuerdo", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Delete_tipo_acuerdo(tbl_tipo_acuerdo_add tbl_tipo_acuerdo_add)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = tbl_tipo_acuerdo_add.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = tbl_tipo_acuerdo_add.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tipo_acuerdo", Tipo = "String", Valor = tbl_tipo_acuerdo_add.p_tipo_acuerdo == null ? "" : tbl_tipo_acuerdo_add.p_tipo_acuerdo.ToString() });
                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedureAcuerdos);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("DeleteTipoAcuerdo", ex);
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
