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
    public class Proposiciones_acceso_datos
    {
        public BDParametros GeneracionParametros = new BDParametros();
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string sp_get_proposiciones_eco_solicitud = "sp_get_proposiciones_eco_solicitud";
        string sp_get_proposiciones_tec_solicitud = "sp_get_proposiciones_tec_solicitud";
        string sp_proposicion_tecnica_economica = "sp_proposicion_tecnica_economica";
        string sp_get_proposiciones_evaluadas_solicitud_tipo = "sp_get_proposiciones_evaluadas_solicitud_tipo";


        public ResponseGeneric<List<proposiciones>> get_proposiciones_tec(String solicitud)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "solicitud", Tipo = "String", Valor = solicitud });

                List<proposiciones> Lista = new List<proposiciones>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_proposiciones_tec_solicitud);
                            Lista = conexion.Query<proposiciones>().FromSql<proposiciones>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<proposiciones>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<proposiciones>>(ex);
            }
        }
        public ResponseGeneric<List<proposiciones>> get_proposiciones_eco(String solicitud)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "solicitud", Tipo = "String", Valor = solicitud });

                List<proposiciones> Lista = new List<proposiciones>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_proposiciones_eco_solicitud);
                            Lista = conexion.Query<proposiciones>().FromSql<proposiciones>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<proposiciones>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<proposiciones>>(ex);
            }
        }

        public ResponseGeneric<List<proposiciones_evaluadas>> get_proposiciones_evaluadas_tipo(String solicitud, String tipo)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "solicitud", Tipo = "String", Valor = solicitud });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tipo", Tipo = "String", Valor = tipo });

                List<proposiciones_evaluadas> Lista = new List<proposiciones_evaluadas>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_proposiciones_evaluadas_solicitud_tipo);
                            Lista = conexion.Query<proposiciones_evaluadas>().FromSql<proposiciones_evaluadas>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<proposiciones_evaluadas>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<proposiciones_evaluadas>>(ex);
            }
        }





        public ResponseGeneric<Crudresponse> add_proposicion(proposicion_tec_eco_add proposicion)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "int", Valor = proposicion.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = proposicion.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_licitante_id", Tipo = "String", Valor = proposicion.p_tbl_licitante_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_cumplimiento", Tipo = "String", Valor = proposicion.p_cumplimiento.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_analisis", Tipo = "String", Valor = proposicion.p_analisis.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_justificacion", Tipo = "String", Valor = proposicion.p_justificacion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tipo_proposicion", Tipo = "String", Valor = proposicion.p_tipo_proposicion.ToString() });
                

                Crudresponse Lista = new Crudresponse();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_proposicion_tecnica_economica);
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


        public ResponseGeneric<Crudresponse> update_proposicion(proposicion_tec_eco_add proposicion)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "int", Valor = proposicion.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = proposicion.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_licitante_id", Tipo = "String", Valor = proposicion.p_tbl_licitante_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_cumplimiento", Tipo = "String", Valor = proposicion.p_cumplimiento.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_analisis", Tipo = "String", Valor = proposicion.p_analisis.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_justificacion", Tipo = "String", Valor = proposicion.p_justificacion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tipo_proposicion", Tipo = "String", Valor = proposicion.p_tipo_proposicion.ToString() });


                Crudresponse Lista = new Crudresponse();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_proposicion_tecnica_economica);
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


    }
}