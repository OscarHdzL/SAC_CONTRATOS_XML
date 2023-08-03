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
    public class MesaValidacion_acceso_datos 
    {
        public BDParametros GeneracionParametros = new BDParametros();
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string sp_alta_solicitud_MesaVal = "sp_alta_solicitud_MesaVal";
        string sp_get_num_sol = "sp_get_num_sol"; 
        string sp_getmesa = "sp_getmesa"; 
        string sp_get_token_sol_mesa = "sp_get_token_sol_mesa";  




        public ResponseGeneric<CrudresponseNum> Mesa_Validacion(String solicitud,String Token)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "solicitud", Tipo = "String", Valor = solicitud });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_token", Tipo = "String", Valor = Token });


                CrudresponseNum Lista = new CrudresponseNum();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_alta_solicitud_MesaVal);
                            Lista = conexion.Query<CrudresponseNum>().FromSql<CrudresponseNum>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<CrudresponseNum>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<CrudresponseNum>(ex);
            }
        }

        public ResponseGeneric<CrudresponseNum> get_num_sol(Guid p_id)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = p_id.ToString() });


                CrudresponseNum Lista = new CrudresponseNum();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_num_sol);
                            Lista = conexion.Query<CrudresponseNum>().FromSql<CrudresponseNum>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<CrudresponseNum>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<CrudresponseNum>(ex);
            }
        }

        public ResponseGeneric<SolicitudMesaValidacion> get_mesa(Guid p_solicitud)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_solicitud", Tipo = "String", Valor = p_solicitud.ToString() });


                SolicitudMesaValidacion Lista = new SolicitudMesaValidacion();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_getmesa);
                            Lista = conexion.Query<SolicitudMesaValidacion>().FromSql<SolicitudMesaValidacion>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<SolicitudMesaValidacion>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<SolicitudMesaValidacion>(ex);
            }
        }

        public ResponseGeneric<List<DocumentsFileManager>> get_mesa_solicitante(Guid p_solicitud)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_sol", Tipo = "String", Valor = p_solicitud.ToString() });


                List<DocumentsFileManager> Lista = new List<DocumentsFileManager>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_token_sol_mesa);
                            Lista = conexion.Query<DocumentsFileManager>().FromSql<DocumentsFileManager>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<DocumentsFileManager>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<DocumentsFileManager>>(ex);
            }
        }

    }
}