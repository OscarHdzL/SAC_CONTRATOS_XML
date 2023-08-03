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
    public class tbl_matriz_riesgo_acceso_datos 
    {
        public BDParametros GeneracionParametros = new BDParametros();
        private readonly ILoggerManager _logger;
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string sp_get_matrizriegos_obligacion = "sp_get_matrizriegos_obligacion";
        string sp_matriz_riesgo = "sp_matriz_riesgo";
        string sp_get_tipo_respuesta = "sp_get_tiporespuesta_dropdown";
        string sp_get_nivel_riesgo = "sp_get_nivelriesgo_dropdown";

        public tbl_matriz_riesgo_acceso_datos()
        {
            _logger = new LoggerManager();
        }

        public ResponseGeneric<List<tbl_matriz_riesgo>> matrizriegos_obligacion(Guid tbl_obligacion_id_)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tbl_obligacion_id", Tipo = "String", Valor = tbl_obligacion_id_.ToString() });

                List<tbl_matriz_riesgo> Lista = new List<tbl_matriz_riesgo>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_matrizriegos_obligacion);
                            Lista = conexion.Query<tbl_matriz_riesgo>().FromSql<tbl_matriz_riesgo>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_matriz_riesgo>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("matrizriegos_obligacion", ex);
                return new ResponseGeneric<List<tbl_matriz_riesgo>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Add(tbl_matriz_riesgo_add tbl_obligacion_id_)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = tbl_obligacion_id_.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = tbl_obligacion_id_.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_riesgo", Tipo = "String", Valor = tbl_obligacion_id_.p_riesgo.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_nivel_riesgo_id", Tipo = "String", Valor = tbl_obligacion_id_.p_tbl_nivel_riesgo_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_probabilidad", Tipo = "String", Valor = tbl_obligacion_id_.p_probabilidad.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_impacto", Tipo = "String", Valor = tbl_obligacion_id_.p_impacto.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_prioridad", Tipo = "String", Valor = tbl_obligacion_id_.p_prioridad.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_link_obligacion_id", Tipo = "String", Valor = tbl_obligacion_id_.p_tbl_link_obligacion_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_respuesta_id", Tipo = "String", Valor = tbl_obligacion_id_.p_tbl_tipo_respuesta_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_accion", Tipo = "String", Valor = tbl_obligacion_id_.p_accion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_respuesta", Tipo = "String", Valor = tbl_obligacion_id_.p_respuesta.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_estatus", Tipo = "String", Valor = tbl_obligacion_id_.p_estatus.ToString() });


                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_matriz_riesgo);
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

        public ResponseGeneric<List<tbl_tipo_respuesta>> GetTipoRespuesta()
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                List<tbl_tipo_respuesta> Lista = new List<tbl_tipo_respuesta>();
                #endregion
                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_tipo_respuesta);
                            Lista = conexion.Query<tbl_tipo_respuesta>().FromSql<tbl_tipo_respuesta>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }
                }
                #endregion
                return new ResponseGeneric<List<tbl_tipo_respuesta>>(Lista);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetTipoRespuesta", ex);
                return new ResponseGeneric<List<tbl_tipo_respuesta>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_nivel_riesgo>> GetNivelRiesgo()
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                List<tbl_nivel_riesgo> Lista = new List<tbl_nivel_riesgo>();
                #endregion
                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_nivel_riesgo);
                            Lista = conexion.Query<tbl_nivel_riesgo>().FromSql<tbl_nivel_riesgo>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }
                }
                #endregion
                return new ResponseGeneric<List<tbl_nivel_riesgo>>(Lista);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetNivelRiesgo", ex);
                return new ResponseGeneric<List<tbl_nivel_riesgo>>(ex);
            }
        }

    }
}
