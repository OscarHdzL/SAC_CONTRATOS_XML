using Conexion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Contrato;
using Modelos.Modelos.Dependencia;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.IO;
using Utilidades.Log4Net;

namespace AccesoDatos_AdminContratos
{
    public class catalogos_acceso_datos_core
    {
        public BDParametros GeneracionParametros = new BDParametros();
        private readonly ILoggerManager _logger;
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string sp_get_tipo_riesgo = "sp_get_tipo_riesgo";
        string sp_get_tipo_documento = "sp_get_tipo_documento";
        string sp_get_tipo_proyecto = "sp_get_tipo_proyecto";
        string sp_get_tipo_contrato = "sp_get_tipo_contrato";
        string sp_tipo_riesgo = "sp_tipo_riesgo";
        string sp_tipo_documento = "sp_tipo_documento";
        string sp_tipo_contrato = "sp_tipo_contrato";
        string sp_tipo_proyecto = "sp_tipo_proyecto";
        string sp_tipo_ejecucion = "sp_tipo_ejecucion";
        string sp_get_tipo_ejecucion = "sp_get_tipo_ejecucion";

        string sp_get_tipo_prioridad = "sp_get_tipo_prioridad";
        string sp_get_tipo_interlocutor = "sp_get_tipo_interlocutor";
        string sp_tipo_prioridad = "sp_tipo_prioridad";
        string sp_tipo_interlocutor = "sp_tipo_interlocutor";

        string sp_get_tbl_unidad_medida = "sp_get_tbl_unidad_medida";
        string sp_unidad_medida = "sp_unidad_medida";
        string sp_procedimiento = "sp_procedimiento";
        string sp_get_procedimiento = "sp_get_procedimiento";

        public catalogos_acceso_datos_core()
        {
            _logger = new LoggerManager();
        }
        #region catalogos
        public ResponseGeneric<List<tbl_tipo_riesgo>> Get_lista_tipo_riesgo(String id_instancia)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_instancia_id", Tipo = "String", Valor = id_instancia.ToString() });
                List<tbl_tipo_riesgo> Lista = new List<tbl_tipo_riesgo>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_tipo_riesgo);
                            Lista = conexion.Query<tbl_tipo_riesgo>().FromSql<tbl_tipo_riesgo>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_tipo_riesgo>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_tipo_riesgo>>(ex);
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
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_tipo_documento);
                            Lista = conexion.Query<tbl_tipo_documento>().FromSql<tbl_tipo_documento>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_tipo_documento>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_tipo_documento>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_tipo_proyecto>> Get_lista_tipo_proyecto(String id_instancia)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_instancia_id", Tipo = "String", Valor = id_instancia.ToString() });
                List<tbl_tipo_proyecto> Lista = new List<tbl_tipo_proyecto>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_tipo_proyecto);
                            Lista = conexion.Query<tbl_tipo_proyecto>().FromSql<tbl_tipo_proyecto>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_tipo_proyecto>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_tipo_proyecto>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_tipo_ejecucion>> Get_lista_tipo_ejecucion(String id_instancia)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_instancia_id", Tipo = "String", Valor = id_instancia.ToString() });
                List<tbl_tipo_ejecucion> Lista = new List<tbl_tipo_ejecucion>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_tipo_ejecucion);
                            Lista = conexion.Query<tbl_tipo_ejecucion>().FromSql<tbl_tipo_ejecucion>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_tipo_ejecucion>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_tipo_ejecucion>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_tipo_contrato>> Get_lista_tipo_contrato(String id_instancia)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_instancia_id", Tipo = "String", Valor = id_instancia.ToString() });
                List<tbl_tipo_contrato> Lista = new List<tbl_tipo_contrato>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_tipo_contrato);
                            Lista = conexion.Query<tbl_tipo_contrato>().FromSql<tbl_tipo_contrato>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_tipo_contrato>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_tipo_contrato>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Add_tipo_riesgo(tbl_tipo_riesgo_add tipo_riesgo_add)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = tipo_riesgo_add.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = tipo_riesgo_add.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tipo_riesgo", Tipo = "String", Valor = tipo_riesgo_add.p_tipo_riesgo.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_instancia_id", Tipo = "String", Valor = tipo_riesgo_add.p_tbl_instancia_id.ToString() });
                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_tipo_riesgo);
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
        public ResponseGeneric<List<Crudresponse>> Delete_tipo_riesgo(tbl_tipo_riesgo_add tipo_riesgo_add)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = tipo_riesgo_add.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = tipo_riesgo_add.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tipo_riesgo", Tipo = "String", Valor = tipo_riesgo_add.p_tipo_riesgo == null ? "" : tipo_riesgo_add.p_tipo_riesgo.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_instancia_id", Tipo = "String", Valor = tipo_riesgo_add.p_tbl_instancia_id == null ? "" : tipo_riesgo_add.p_tbl_instancia_id.ToString() });
                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_tipo_riesgo);
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
        public ResponseGeneric<List<Crudresponse>> Add_tipo_documento(tbl_tipo_documento_add tipo_documento_add)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = tipo_documento_add.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = tipo_documento_add.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tipo_documento", Tipo = "String", Valor = tipo_documento_add.p_tipo_documento.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_instancia_id", Tipo = "String", Valor = tipo_documento_add.p_tbl_instancia_id.ToString() });
                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_tipo_documento);
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
        public ResponseGeneric<List<Crudresponse>> Delete_tipo_documento(tbl_tipo_documento_add tipo_documento_add)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = tipo_documento_add.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = tipo_documento_add.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tipo_riesgo", Tipo = "String", Valor = tipo_documento_add.p_tipo_documento == null ? "" : tipo_documento_add.p_tipo_documento.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_instancia_id", Tipo = "String", Valor = tipo_documento_add.p_tbl_instancia_id == null ? "" : tipo_documento_add.p_tbl_instancia_id.ToString() });
                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_tipo_documento);
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
        public ResponseGeneric<List<Crudresponse>> Add_tipo_proyecto(tbl_tipo_proyecto_add tipo_proyecto_add)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = tipo_proyecto_add.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = tipo_proyecto_add.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tipo_proyecto", Tipo = "String", Valor = tipo_proyecto_add.p_tipo_proyecto.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_instancia_id", Tipo = "String", Valor = tipo_proyecto_add.p_tbl_instancia_id.ToString() });
                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_tipo_proyecto);
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
        public ResponseGeneric<List<Crudresponse>> Add_tipo_contrato(tbl_tipo_contrato_add tipo_contrato_add)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = tipo_contrato_add.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = tipo_contrato_add.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tipo_contrato", Tipo = "String", Valor = tipo_contrato_add.p_tipo_contrato.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_instancia_id", Tipo = "String", Valor = tipo_contrato_add.p_tbl_instancia_id.ToString() });
                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_tipo_contrato);
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
        public ResponseGeneric<List<Crudresponse>> Delete_tipo_contrato(tbl_tipo_contrato_add tipo_contrato_add)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = tipo_contrato_add.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = tipo_contrato_add.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tipo_riesgo", Tipo = "String", Valor = tipo_contrato_add.p_tipo_contrato == null ? "" : tipo_contrato_add.p_tipo_contrato.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_instancia_id", Tipo = "String", Valor = tipo_contrato_add.p_tbl_instancia_id == null ? "" : tipo_contrato_add.p_tbl_instancia_id.ToString() });
                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_tipo_contrato);
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
        public ResponseGeneric<List<Crudresponse>> Delete_tipo_proyecto(tbl_tipo_proyecto_add tipo_proyecto_add)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = tipo_proyecto_add.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = tipo_proyecto_add.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tipo_proyecto", Tipo = "String", Valor = tipo_proyecto_add.p_tipo_proyecto == null ? "" : tipo_proyecto_add.p_tipo_proyecto.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_instancia_id", Tipo = "String", Valor = tipo_proyecto_add.p_tbl_instancia_id == null ? "" : tipo_proyecto_add.p_tbl_instancia_id.ToString() });
                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_tipo_proyecto);
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

        public ResponseGeneric<List<Crudresponse>> Add_tipo_ejecucion(tbl_tipo_ejecucion_add tipo_ejecucion_add)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = tipo_ejecucion_add.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = tipo_ejecucion_add.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tipo_ejecucion", Tipo = "String", Valor = tipo_ejecucion_add.p_tipo_ejecucion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_instancia_id", Tipo = "String", Valor = tipo_ejecucion_add.p_tbl_instancia_id.ToString() });
                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_tipo_ejecucion);//crear el sp
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
        public ResponseGeneric<List<Crudresponse>> Delete_tipo_ejecucion(tbl_tipo_ejecucion_add tipo_ejecucion_add)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = tipo_ejecucion_add.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = tipo_ejecucion_add.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tipo_riesgo", Tipo = "String", Valor = tipo_ejecucion_add.p_tipo_ejecucion == null ? "" : tipo_ejecucion_add.p_tipo_ejecucion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_instancia_id", Tipo = "String", Valor = tipo_ejecucion_add.p_tbl_instancia_id == null ? "" : tipo_ejecucion_add.p_tbl_instancia_id.ToString() });
                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_tipo_ejecucion);//crear el sp
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
        #endregion
        public ResponseGeneric<List<tbl_tipo_prioridad>> Get_lista_tipo_prioridad(String id_instancia)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_instancia_id", Tipo = "String", Valor = id_instancia.ToString() });
                List<tbl_tipo_prioridad> Lista = new List<tbl_tipo_prioridad>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_tipo_prioridad);
                            Lista = conexion.Query<tbl_tipo_prioridad>().FromSql<tbl_tipo_prioridad>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_tipo_prioridad>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_tipo_prioridad>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Add_tipo_prioridad(tbl_tipo_prioridad_add tipo_prioridad_add)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = tipo_prioridad_add.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = tipo_prioridad_add.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tipo_prioridad", Tipo = "String", Valor = tipo_prioridad_add.p_tipo_prioridad.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_instancia_id", Tipo = "String", Valor = tipo_prioridad_add.p_tbl_instancia_id.ToString() });
                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_tipo_prioridad);//crear el sp
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
        public ResponseGeneric<List<Crudresponse>> Delete_tipo_prioridad(tbl_tipo_prioridad_add tipo_prioridad_add)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = tipo_prioridad_add.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = tipo_prioridad_add.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tipo_prioridad", Tipo = "String", Valor = tipo_prioridad_add.p_tipo_prioridad!=null? tipo_prioridad_add.p_tipo_prioridad.ToString():"NULL" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_instancia_id", Tipo = "String", Valor = tipo_prioridad_add.p_tbl_instancia_id!=null? tipo_prioridad_add.p_tbl_instancia_id.ToString():"NULL" });
                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_tipo_prioridad);//crear el sp
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


        public ResponseGeneric<List<tbl_unidad_medida>> Get_lista_unidad_medida()
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                List<tbl_unidad_medida> Lista = new List<tbl_unidad_medida>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_tbl_unidad_medida);
                            Lista = conexion.Query<tbl_unidad_medida>().FromSql<tbl_unidad_medida>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_unidad_medida>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_unidad_medida>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Add_unidad_medida(tbl_unidad_medida_add unidad_medida_add)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = unidad_medida_add.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = unidad_medida_add.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_unidad_medida", Tipo = "String", Valor = unidad_medida_add.p_unidad_medida.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_clave", Tipo = "String", Valor = unidad_medida_add.p_clave.ToString() });
                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_unidad_medida);//crear el sp
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
        public ResponseGeneric<List<Crudresponse>> Delete_unidad_medida(tbl_unidad_medida_add unidad_medida_add)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = unidad_medida_add.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = unidad_medida_add.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_unidad_medida", Tipo = "String", Valor = unidad_medida_add.p_unidad_medida!=null? unidad_medida_add.p_unidad_medida.ToString():"NULL" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_clave", Tipo = "String", Valor = unidad_medida_add.p_clave!=null? unidad_medida_add.p_clave.ToString():"NULL" });
                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_unidad_medida);//crear el sp
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

        public ResponseGeneric<List<lista_tipo_interlocutor>> Get_lista_tipo_interlocutor()
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_activo", Tipo = "String", Valor = "1" });
                List<lista_tipo_interlocutor> Lista = new List<lista_tipo_interlocutor>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_tipo_interlocutor);
                            Lista = conexion.Query<lista_tipo_interlocutor>().FromSql<lista_tipo_interlocutor>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<lista_tipo_interlocutor>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<lista_tipo_interlocutor>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Add_tipo_interlocutor(tbl_tipo_interlocutor_add tipo_interlocutor_add)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = tipo_interlocutor_add.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = tipo_interlocutor_add.id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "nombre", Tipo = "String", Valor = tipo_interlocutor_add.nombre.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "comercial", Tipo = "String", Valor = tipo_interlocutor_add.comercial.ToString() });
                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_tipo_interlocutor);//crear el sp
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
        public ResponseGeneric<List<Crudresponse>> Delete_tipo_interlocutor(tbl_tipo_interlocutor_add tipo_interlocutor_add)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = tipo_interlocutor_add.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = tipo_interlocutor_add.id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "nombre", Tipo = "String", Valor = tipo_interlocutor_add.nombre!= null ? tipo_interlocutor_add.nombre.ToString() : "NULL" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "comercial", Tipo = "String", Valor = tipo_interlocutor_add.comercial != null ? tipo_interlocutor_add.comercial.ToString() : "NULL" });
                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_tipo_interlocutor);//crear el sp
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



        public ResponseGeneric<List<tbl_procedimiento>> Get_lista_procedimiento()
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                List<tbl_procedimiento> Lista = new List<tbl_procedimiento>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_procedimiento);
                            Lista = conexion.Query<tbl_procedimiento>().FromSql<tbl_procedimiento>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_procedimiento>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_procedimiento>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Add_procedimiento(tbl_procedimiento_add procedimiento_add)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = procedimiento_add.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = procedimiento_add.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "procedimiento", Tipo = "String", Valor = procedimiento_add.p_procedimiento.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "sigla", Tipo = "String", Valor = procedimiento_add.p_sigla.ToString() });
                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_procedimiento);//crear el sp
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Delete_procedimiento(tbl_procedimiento_add procedimiento_add)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = procedimiento_add.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = procedimiento_add.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "procedimiento", Tipo = "String", Valor = procedimiento_add.p_procedimiento!=null? procedimiento_add.p_procedimiento.ToString():"" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "sigla", Tipo = "String", Valor = procedimiento_add.p_sigla!=null? procedimiento_add.p_sigla.ToString():"" });
                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_procedimiento);
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


    }
}
