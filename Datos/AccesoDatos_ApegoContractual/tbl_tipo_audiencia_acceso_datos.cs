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
    public class tbl_tipo_audiencia_acceso_datos : crud_audiencia
    {
        private readonly ILoggerManager _logger;
        public BDParametros GeneracionParametros = new BDParametros();
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string StoreProcedure = "sp_get_tipoaudiencia";

        public tbl_tipo_audiencia_acceso_datos()
        {
            _logger = new LoggerManager();
        }
        public ResponseGeneric<List<tbl_tipo_audiencia_>> Consultar()
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                //ListaEnvioParam.Add(new EntidadParametro { Nombre = "id", Tipo = "Int", Valor = entidad.ToString() });

                List<tbl_tipo_audiencia_> Lista = new List<tbl_tipo_audiencia_>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure);
                            Lista = conexion.Query<tbl_tipo_audiencia_>().FromSql<tbl_tipo_audiencia_>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_tipo_audiencia_>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_tipo_audiencia_>>(ex);
            }
        }


    }
}
