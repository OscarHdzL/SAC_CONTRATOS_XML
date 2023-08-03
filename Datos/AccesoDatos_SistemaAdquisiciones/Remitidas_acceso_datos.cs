using Conexion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.IO;

namespace AccesoDatos_SistemaAdquisiciones
{
    public class Remitidas_acceso_datos
    {
        public BDParametros GeneracionParametros = new BDParametros();
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string sp_get_remitidas_eco_rolusuario = "sp_get_remitidas_eco_rolusuario";
        string sp_get_remitidas_tec_rolusuario = "sp_get_remitidas_tec_rolusuario";

        public ResponseGeneric<List<remitidas>> get_remitidas_tec(String rol_usuario)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "rol_usuario", Tipo = "String", Valor = rol_usuario });

                List<remitidas> Lista = new List<remitidas>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_remitidas_tec_rolusuario);
                            Lista = conexion.Query<remitidas>().FromSql<remitidas>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<remitidas>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<remitidas>>(ex);
            }
        }

        public ResponseGeneric<List<remitidas>> get_remitidas_eco(String rol_usuario)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "rol_usuario", Tipo = "String", Valor = rol_usuario });

                List<remitidas> Lista = new List<remitidas>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_remitidas_eco_rolusuario);
                            Lista = conexion.Query<remitidas>().FromSql<remitidas>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<remitidas>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<remitidas>>(ex);
            }
        }


    }
}