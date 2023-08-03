using Conexion;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Modelos.Response;
using Modelos.Modelos;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos_SistemaAdquisiciones
{
    public class Cotizaciones_datos
    {
        public BDParametros GeneracionParametros = new BDParametros();
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        

        public ResponseGeneric<tbl_proveedor> obtener_datos_proveedr(string rfc_prov)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "RFC", Tipo = "String", Valor = rfc_prov.ToString() });

                tbl_proveedor Lista = new tbl_proveedor();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, "sp_get_proveedor_RFC");
                            Lista = conexion.Query<tbl_proveedor>().FromSql<tbl_proveedor>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<tbl_proveedor>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<tbl_proveedor>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> update_sol_metodo(string parametro, string id_sol, string variable)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "parametro", Tipo = "int", Valor = parametro.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "id_sol", Tipo = "String", Valor = id_sol.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_variable", Tipo = "String", Valor = variable.ToString() });



                List<Crudresponse> Lista = new List<Crudresponse>();
                #endregion
                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, "sp_update_solicitud_metodo");
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }


        public ResponseGeneric<List<tbl_cotizacion_solicitud>> Get_documentos_cotizacion(string id_solicitud)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id_solic", Tipo = "String", Valor = id_solicitud.ToString() });



                List<tbl_cotizacion_solicitud> Lista = new List<tbl_cotizacion_solicitud>();
                #endregion
                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, "sp_get_cotizacion_solicitud");
                            Lista = conexion.Query<tbl_cotizacion_solicitud>().FromSql<tbl_cotizacion_solicitud>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_cotizacion_solicitud>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_cotizacion_solicitud>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> crud_cotizacion(tbl_cotizacion_sol_crud cotizacion_sol)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "int", Valor = cotizacion_sol.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = cotizacion_sol.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_proveedor_id", Tipo = "String", Valor = cotizacion_sol.p_tbl_proveedor_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_solicitud_documento_adjunto_id", Tipo = "String", Valor = cotizacion_sol.p_tbl_solicitud_documento_adjunto_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_documento_id", Tipo = "String", Valor = cotizacion_sol.p_tbl_tipo_documento_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_solicitud_id", Tipo = "String", Valor = cotizacion_sol.p_tbl_solicitud_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_nom_documento", Tipo = "String", Valor = cotizacion_sol.p_nom_documento.ToString() });



                List<Crudresponse> Lista = new List<Crudresponse>();
                #endregion
                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, "sp_cotizacion_solicitud");
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

    }
}
