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
using Utilidades.Log4Net;

namespace AccesoDatos
{
    public class tbl_notificacionessanciones_datos : crud_tbl_notificacionessanciones
    {
        public BDParametros GeneracionParametros = new BDParametros();
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        private readonly ILoggerManager _logger;
        string sp_get_notificaciones = "sp_get_notificaciones";
        string sp_get_notificaciones_po = "sp_get_plan_obligacion";

        public tbl_notificacionessanciones_datos()
        {
            _logger = new LoggerManager();
        }
        public ResponseGeneric<List<tbl_notificacionsanciones>> Consultar(Guid idContrato, string periodo)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tbl_contrato_id", Tipo = "String", Valor = idContrato.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "fecha_ejecucion_inicio", Tipo = "String", Valor = (periodo + "-01") });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "fecha_ejecucion_fin", Tipo = "String", Valor = Convert.ToDateTime(periodo + "-01").AddMonths(1).ToString("yyyy-MM-dd") });

                List<tbl_notificacionsanciones> Lista = new List<tbl_notificacionsanciones>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_notificaciones);
                    Lista = conexion.Query<tbl_notificacionsanciones>().FromSql<tbl_notificacionsanciones>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                }
                #endregion

                return new ResponseGeneric<List<tbl_notificacionsanciones>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_notificacionsanciones>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_plan_por_obligacion>> ConsultarPO(Guid idOblig, string tipo, string periodo)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id_obligacion", Tipo = "String", Valor = idOblig.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "fecha_ejecucion_inicio", Tipo = "String", Valor = (periodo + "-01") });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "fecha_ejecucion_fin", Tipo = "String", Valor = Convert.ToDateTime(periodo + "-01").AddMonths(1).ToString("yyyy-MM-dd") });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tipo", Tipo = "String", Valor = tipo.ToString() });

                List<tbl_plan_por_obligacion> Lista = new List<tbl_plan_por_obligacion>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_notificaciones_po);
                    Lista = conexion.Query<tbl_plan_por_obligacion>().FromSql<tbl_plan_por_obligacion>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                }
                #endregion

                return new ResponseGeneric<List<tbl_plan_por_obligacion>>(Lista);

            }
            catch (Exception ex)
                {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_plan_por_obligacion>>(ex);
            }
        }
    }
}
