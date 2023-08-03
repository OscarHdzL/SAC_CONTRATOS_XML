using Conexion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modelos.Modelos;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AccesoDatos_SistemaAdquisiciones
{
    public class tbl_juntaclaraciones_acceso_datos
    {
        public BDParametros GeneracionParametros = new BDParametros();
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string sp_solicitud_observador = "sp_solicitud_observador";
        string sp_get_solicitud_observador = "sp_get_solicitud_observador";
        string sp_junta_aclaraciones = "sp_junta_aclaraciones";
        string sp_get_junta_aclaracion = "sp_get_junta_aclaracion";

        public ResponseGeneric<List<Crudresponse>> Add_Obs(tbl_solicitud_observador _tbl_solicitud_observador)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "int", Valor = "2" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = _tbl_solicitud_observador.id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_solicitud_id", Tipo = "String", Valor = _tbl_solicitud_observador.tbl_solicitud_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tipo_acta", Tipo = "String", Valor = _tbl_solicitud_observador.tipo_acta.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_observador", Tipo = "String", Valor = _tbl_solicitud_observador.observador.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_programacion_id", Tipo = "String", Valor = _tbl_solicitud_observador.tbl_programacion_id.ToString() });

                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_solicitud_observador);
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

        public ResponseGeneric<List<Crudresponse>> Add_Junta(tbl_junta_aclaraciones _tbl_junta_aclaraciones)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "int", Valor = "2" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = _tbl_junta_aclaraciones.id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_solicitud_id", Tipo = "String", Valor = _tbl_junta_aclaraciones.tbl_solicitud_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_numero_junta", Tipo = "int", Valor = _tbl_junta_aclaraciones.numero_junta.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_aclaracion", Tipo = "String", Valor = _tbl_junta_aclaraciones.aclaracion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_aclaracion", Tipo = "String", Valor = _tbl_junta_aclaraciones.fecha_aclaracion.ToString("yyyy-MM-ddTHH:mm:ss") });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_req_junta", Tipo = "String", Valor = _tbl_junta_aclaraciones.req_junta.ToString() });

                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_junta_aclaraciones);
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

        public ResponseGeneric<List<tbl_solicitud_observador_list>> Get_Obs(string id_sol, string tipo_acta, string prog)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id_solicitud", Tipo = "String", Valor = id_sol });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tipo_acta", Tipo = "String", Valor = tipo_acta });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_programacion_id", Tipo = "String", Valor = prog });

                List<tbl_solicitud_observador_list> Lista = new List<tbl_solicitud_observador_list>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_solicitud_observador);
                            Lista = conexion.Query<tbl_solicitud_observador_list>().FromSql<tbl_solicitud_observador_list>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_solicitud_observador_list>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_solicitud_observador_list>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_junta_aclaraciones_list>> Get_Juntas(string id_sol)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id_solicitud", Tipo = "String", Valor = id_sol });

                List<tbl_junta_aclaraciones_list> Lista = new List<tbl_junta_aclaraciones_list>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_junta_aclaracion);
                            Lista = conexion.Query<tbl_junta_aclaraciones_list>().FromSql<tbl_junta_aclaraciones_list>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_junta_aclaraciones_list>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_junta_aclaraciones_list>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Delete_Obs(string id_sol_obs)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "int", Valor = "5" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = id_sol_obs });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_solicitud_id", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tipo_acta", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_observador", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_programacion_id", Tipo = "String", Valor = "" });


                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_solicitud_observador);
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

        public ResponseGeneric<List<Crudresponse>> Delete_Junta(string id_junta_acl)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "int", Valor = "4" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = id_junta_acl });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_solicitud_id", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_numero_junta", Tipo = "int", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_aclaracion", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_aclaracion", Tipo = "String", Valor = "0001-01-01T00:00:00" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_req_junta", Tipo = "String", Valor = "" });

                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_junta_aclaraciones);
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
