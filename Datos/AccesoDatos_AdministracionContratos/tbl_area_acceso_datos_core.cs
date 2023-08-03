using Conexion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Area;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.IO;
using Utilidades.Log4Net;

namespace AccesoDatos_AdministracionDeContratos
{
    public class tbl_area_acceso_datos_core
    {
        public BDParametros GeneracionParametros = new BDParametros();
        private readonly ILoggerManager _logger;
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string sp_area = "sp_area";
        string sp_get_area = "sp_get_areas_dependencia_core";
        string sp_get_sub_area = "sp_get_sub_area";
        string StoreProcedure = "sp_get_areasdependencia_dropdown";
        string sp_get_areas_d_i = "sp_get_areas_d_i";
        string sp_get_subareas_by_area = "sp_get_subareas_by_area";
        string sp_get_areasubordinada_by_subarea = "sp_get_areasubordinada_by_subarea";
        string sp_subareas = "sp_subareas";
        string sp_area_subordinada = "sp_area_subordinada";

        public tbl_area_acceso_datos_core()
        {
            _logger = new LoggerManager();
        }

        public ResponseGeneric<List<tbl_lista_areas>> Get(String dependencia, String su)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "dependencia", Tipo = "String", Valor = dependencia});
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "SU", Tipo = "String", Valor = su});

                List<tbl_lista_areas> Lista = new List<tbl_lista_areas>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_area);
                            Lista = conexion.Query<tbl_lista_areas>().FromSql<tbl_lista_areas>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_lista_areas>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_lista_areas>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_lista_areas>> Get_Sub(String Area)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "dependencia", Tipo = "String", Valor = Area });                

                List<tbl_lista_areas> Lista = new List<tbl_lista_areas>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_sub_area);
                            Lista = conexion.Query<tbl_lista_areas>().FromSql<tbl_lista_areas>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_lista_areas>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_lista_areas>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Add(tbl_area tbl_area)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "int", Valor = "2" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = tbl_area.id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_dependencia_id", Tipo = "String", Valor = tbl_area.tbl_dependencia_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_area", Tipo = "String", Valor = tbl_area.area.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_total_personal", Tipo = "int", Valor = tbl_area.total_personal.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_sueldo_promedio", Tipo = "int", Valor = tbl_area.sueldo_promedio.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_total_externos", Tipo = "int", Valor = tbl_area.total_externos.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_nivel", Tipo = "int", Valor = tbl_area.nivel.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id_area_padre", Tipo = "String", Valor = tbl_area.id_area_padre.ToString() });

                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_area);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> update(tbl_area tbl_area)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "int", Valor = "3" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = tbl_area.id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_dependencia_id", Tipo = "String", Valor = tbl_area.tbl_dependencia_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_area", Tipo = "String", Valor = tbl_area.area.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_total_personal", Tipo = "int", Valor = tbl_area.total_personal.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_sueldo_promedio", Tipo = "int", Valor = tbl_area.sueldo_promedio.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_total_externos", Tipo = "int", Valor = tbl_area.total_externos.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_nivel", Tipo = "int", Valor = tbl_area.nivel.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id_area_padre", Tipo = "String", Valor = tbl_area.id_area_padre.ToString() });

                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_area);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> delete(tbl_area tbl_area)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "int", Valor = "4" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = tbl_area.id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_dependencia_id", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_area", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_total_personal", Tipo = "int", Valor = "0" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_sueldo_promedio", Tipo = "int", Valor = "0" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_total_externos", Tipo = "int", Valor = "0" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_nivel", Tipo = "int", Valor = "0" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id_area_padre", Tipo = "String", Valor = "" });

                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_area);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<DropDownList>> FillDrop(String dependencia)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "dependencia", Tipo = "String", Valor = dependencia });

                List<DropDownList> Lista = new List<DropDownList>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure);
                            Lista = conexion.Query<DropDownList>().FromSql<DropDownList>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<DropDownList>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }

        /*Modificacion FSD*/
        public ResponseGeneric<List<tbl_areas_lista>> Get_areas(String p_id, String p_su)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = p_id });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_su", Tipo = "String", Valor = p_su });

                List<tbl_areas_lista> Lista = new List<tbl_areas_lista>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_areas_d_i);
                            Lista = conexion.Query<tbl_areas_lista>().FromSql<tbl_areas_lista>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_areas_lista>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_areas_lista>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_subareas_lista>> Get_subareas(String p_id)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_area_id", Tipo = "String", Valor = p_id });

                List<tbl_subareas_lista> Lista = new List<tbl_subareas_lista>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_subareas_by_area);
                            Lista = conexion.Query<tbl_subareas_lista>().FromSql<tbl_subareas_lista>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_subareas_lista>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_subareas_lista>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_areasubordinada_lista>> Get_areas_sub(String p_id)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_subarea_id", Tipo = "String", Valor = p_id });

                List<tbl_areasubordinada_lista> Lista = new List<tbl_areasubordinada_lista>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_areasubordinada_by_subarea);
                            Lista = conexion.Query<tbl_areasubordinada_lista>().FromSql<tbl_areasubordinada_lista>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_areasubordinada_lista>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_areasubordinada_lista>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> add_subarea(tbl_subarea tbl_subarea_)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = tbl_subarea_.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = tbl_subarea_.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_area_id", Tipo = "String", Valor = tbl_subarea_.p_tbl_area_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_subarea", Tipo = "String", Valor = tbl_subarea_.p_subarea.ToString() });
                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_subareas);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> add_area_subordinada(tbl_area_subordinada tbl_area_sub_)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = tbl_area_sub_.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = tbl_area_sub_.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_subarea_id", Tipo = "String", Valor = tbl_area_sub_.p_tbl_subarea_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_area_subordinada", Tipo = "String", Valor = tbl_area_sub_.p_area_subordinada.ToString() });
                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_area_subordinada);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        /*Modificacion FSD*/
    }
}
