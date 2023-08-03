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
    public class tbl_sanciones_acceso_datos : crud_sanciones<tbl_sanciones>
    {
        public BDParametros GeneracionParametros = new BDParametros();
        private readonly ILoggerManager _logger;
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string StoreProcedure = "sp_get_sancion";
        string StoreProcedureSanciones = "sp_tipo_sancion";

        public tbl_sanciones_acceso_datos()
        {
            _logger = new LoggerManager();
        }

        public ResponseGeneric<List<tbl_sanciones>> Consultar()
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                //ListaEnvioParam.Add(new EntidadParametro { Nombre = "id", Tipo = "Int", Valor = entidad.ToString() });

                List<tbl_sanciones> Lista = new List<tbl_sanciones>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure);
                            Lista = conexion.Query<tbl_sanciones>().FromSql<tbl_sanciones>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_sanciones>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_sanciones>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Add_tipo_sancion(tbl_tipo_sancion_add tbl_tipo_sancion_add)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = tbl_tipo_sancion_add.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = tbl_tipo_sancion_add.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_sancion", Tipo = "String", Valor = tbl_tipo_sancion_add.p_sancion.ToString() });
                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedureSanciones);
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

        public ResponseGeneric<List<Crudresponse>> Delete_tipo_sanciones(tbl_tipo_sancion_add tbl_tipo_sancion_add)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = tbl_tipo_sancion_add.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = tbl_tipo_sancion_add.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_sancion", Tipo = "String", Valor = tbl_tipo_sancion_add.p_sancion == null ? "" : tbl_tipo_sancion_add.p_sancion.ToString() });
                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedureSanciones);
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
