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
    public class vs_siderbar_acces_odatos : crud_siderbar
    {
        public BDParametros GeneracionParametros = new BDParametros();
        private readonly ILoggerManager _logger;
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string StoreProcedure = "sp_get_siderbar";

        public vs_siderbar_acces_odatos()
        {
            _logger = new LoggerManager();
        }
        public ResponseGeneric<List<vs_siderbar>> Consultar(string rol)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_rol_id", Tipo = "String", Valor = rol.ToString() });

                List<vs_siderbar> Lista = new List<vs_siderbar>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure);
                            Lista = conexion.Query<vs_siderbar>().FromSql<vs_siderbar>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<vs_siderbar>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<vs_siderbar>>(ex);
            }
        }




    }
}
