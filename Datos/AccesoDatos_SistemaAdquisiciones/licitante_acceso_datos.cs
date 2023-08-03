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
    public class licitante_acceso_datos
    {
        public BDParametros GeneracionParametros = new BDParametros();
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string StoreProcedure = "sp_get_proveedor_instancia";
        string sp_get_licitante_rfc_convocatoria = "sp_get_licitante_rfc_convocatoria";
        string sp_licitante = "sp_licitante";
        string sp_get_licitantes_convocatoria = "sp_get_licitantes_convocatoria";
        string sp_get_proveedor_RFC = "sp_get_proveedor_RFC";
        string sp_licitante_propuesta = "sp_licitante_propuesta";
        string sp_get_licitante_propuesta_solicitud_tipo = "sp_get_licitante_propuesta_solicitud_tipo";
        

        public ResponseGeneric<List<tbl_proveedor>> GetProveedores_instancia(String instancia)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "instancia", Tipo = "String", Valor = instancia });

                List<tbl_proveedor> Lista = new List<tbl_proveedor>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure);
                            Lista = conexion.Query<tbl_proveedor>().FromSql<tbl_proveedor>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_proveedor>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_proveedor>>(ex);
            }
        }


        public ResponseGeneric<tbl_proveedor> GetProveedor_RFC(String RFC)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "RFC", Tipo = "String", Valor = RFC });

                tbl_proveedor Lista = new tbl_proveedor();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_proveedor_RFC);
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

        public ResponseGeneric<Crudresponse> AddLicitante(tbl_licitante_add licitante)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = licitante.p_opt.ToString() }) ;
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = licitante.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_solicitud_id", Tipo = "String", Valor = licitante.p_tbl_solicitud_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_razon_social", Tipo = "String", Valor = licitante.p_razon_social.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_rep_legal_nombre", Tipo = "String", Valor = licitante.p_rep_legal_nombre.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_rep_legal_ap_paterno", Tipo = "String", Valor = licitante.p_rep_legal_ap_paterno.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_rep_legal_ap_materno", Tipo = "String", Valor = licitante.p_rep_legal_ap_materno.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_rfc", Tipo = "String", Valor = licitante.p_rfc.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_es_proveedor", Tipo = "String", Valor = licitante.p_es_proveedor.ToString() });


                Crudresponse Lista = new Crudresponse();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_licitante);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<Crudresponse>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<Crudresponse>(ex);
            }
        }


        public ResponseGeneric<List<Crudresponse>> DeleteLicitante(tbl_licitante_add licitante)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = licitante.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = licitante.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_solicitud_id", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_razon_social", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_rep_legal_nombre", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_rep_legal_ap_paterno", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_rep_legal_ap_materno", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_rfc", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_es_proveedor", Tipo = "String", Valor = "0" });


                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_licitante);
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

        public ResponseGeneric<List<tbl_licitante>> ValidaLicitante(String RFC, String Solicitud)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "RFC", Tipo = "String", Valor = RFC.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "Solicitud", Tipo = "String", Valor = Solicitud.ToString() });
                


                List<tbl_licitante> Lista = new List<tbl_licitante>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_licitante_rfc_convocatoria);
                            Lista = conexion.Query<tbl_licitante>().FromSql<tbl_licitante>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_licitante>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_licitante>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_licitante>> GetLicitantes_Solicitud(String Solicitud)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "Solicitud", Tipo = "String", Valor = Solicitud.ToString() });



                List<tbl_licitante> Lista = new List<tbl_licitante>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_licitantes_convocatoria);
                            Lista = conexion.Query<tbl_licitante>().FromSql<tbl_licitante>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_licitante>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_licitante>>(ex);
            }

        }

        //PROPUESTA LICITANTE

        public ResponseGeneric<Crudresponse> AddPropuestaLicitante(tbl_licitante_propuesta_add licitante_propuesta)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = licitante_propuesta.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = licitante_propuesta.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_licitante_id", Tipo = "String", Valor = licitante_propuesta.p_tbl_licitante_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_sigla", Tipo = "String", Valor = licitante_propuesta.p_sigla.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_token", Tipo = "String", Valor = licitante_propuesta.p_token.ToString() });


                Crudresponse Lista = new Crudresponse();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_licitante_propuesta);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<Crudresponse>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<Crudresponse>(ex);
            }
        }

        public ResponseGeneric<Crudresponse> AddPropuestaLicitante_C(tbl_licitante_propuesta_add licitante_propuesta)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = licitante_propuesta.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = licitante_propuesta.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_licitante_id", Tipo = "String", Valor = licitante_propuesta.p_tbl_licitante_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_sigla", Tipo = "String", Valor = "eco" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_sigla_s", Tipo = "String", Valor = "tec" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_token", Tipo = "String", Valor = licitante_propuesta.p_token.ToString() });


                Crudresponse Lista = new Crudresponse();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, "sp_licitante_propuesta_cotizacion");
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<Crudresponse>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<Crudresponse>(ex);
            }
        }


        public ResponseGeneric<List<licitante_propuesta>> GetLicitantes_Propuesta_Solicitud(String Solicitud, String TipoPropuesta)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "solicitud", Tipo = "String", Valor = Solicitud.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tipo_propuesta", Tipo = "String", Valor = TipoPropuesta.ToString() });



                List<licitante_propuesta> Lista = new List<licitante_propuesta>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_licitante_propuesta_solicitud_tipo);
                            Lista = conexion.Query<licitante_propuesta>().FromSql<licitante_propuesta>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<licitante_propuesta>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<licitante_propuesta>>(ex);
            }

        }
    }
}