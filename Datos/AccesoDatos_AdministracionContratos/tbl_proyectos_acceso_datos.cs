using Conexion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Proyectos;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.IO;
using Utilidades.Log4Net;

namespace AccesoDatos_AdministracionDeContratos
{
    public class tbl_proyectos_acceso_datos
    {
        public BDParametros GeneracionParametros = new BDParametros();
        private readonly ILoggerManager _logger;
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string sp_proyecto = "sp_proyecto";
        string sp_get_lista_proyectos = "sp_get_lista_proyectos";
        string sp_get_proyecto = "sp_get_proyecto";
        string sp_get_tipo_proyecto_dropdown = "sp_get_tipo_proyecto_dropdown";
        string sp_get_criticidad_proyecto_dropdown = "sp_get_criticidad_proyecto_dropdown";
        string sp_get_tipo_ejecucion_proyecto_dropdown = "sp_get_tipo_ejecucion_proyecto_dropdown";
        string sp_get_estatus_proyecto = "sp_get_estatus_proyecto";

        public tbl_proyectos_acceso_datos()
        {
            _logger = new LoggerManager();
        }
        public ResponseGeneric<List<tbl_lista_proyectos>> Get_Lista(String id_dep)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "dependencia_id", Tipo = "String", Valor = id_dep });

                List<tbl_lista_proyectos> Lista = new List<tbl_lista_proyectos>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_lista_proyectos);
                            Lista = conexion.Query<tbl_lista_proyectos>().FromSql<tbl_lista_proyectos>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_lista_proyectos>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_lista_proyectos>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_proyectos>> Get_Proyecto(String id_proyecto)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "id_proyecto", Tipo = "String", Valor = id_proyecto });

                List<tbl_proyectos> Lista = new List<tbl_proyectos>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_proyecto);
                            Lista = conexion.Query<tbl_proyectos>().FromSql<tbl_proyectos>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_proyectos>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_proyectos>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Add(tbl_proyectos _tbl_proyectos)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "int", Valor = "2" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = _tbl_proyectos.id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_proyecto_id", Tipo = "String", Valor = _tbl_proyectos.tbl_tipo_proyecto_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_proyecto", Tipo = "String", Valor = _tbl_proyectos.proyecto.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_objetivo", Tipo = "String", Valor = _tbl_proyectos.objetivo.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_alcance", Tipo = "String", Valor = _tbl_proyectos.alcance.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_criticidad_proyecto_id", Tipo = "String", Valor = _tbl_proyectos.tbl_criticidad_proyecto_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_inicio", Tipo = "String", Valor = _tbl_proyectos.fecha_incio.ToString("yyyy-MM-ddTHH:mm:ss") });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_fin", Tipo = "String", Valor = _tbl_proyectos.fecha_fin.ToString("yyyy-MM-ddTHH:mm:ss") });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_estatus_proyecto_id", Tipo = "String", Valor = _tbl_proyectos.tbl_estatus_proyecto_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_ejecucion_id", Tipo = "String", Valor = _tbl_proyectos.tbl_tipo_ejecucion_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_etapa_proyecto_id", Tipo = "String", Valor = _tbl_proyectos.tbl_etapa_proyecto_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_analisis_id", Tipo = "String", Valor = _tbl_proyectos.tbl_tipo_analisis_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_nivel_analisis_id", Tipo = "String", Valor = _tbl_proyectos.tbl_nivel_analisis_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_criterio_economico", Tipo = "int", Valor = _tbl_proyectos.criterio_economico.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_criterio_social", Tipo = "int", Valor = _tbl_proyectos.criterio_social.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_criterio_ambienta", Tipo = "int", Valor = _tbl_proyectos.criterio_ambienta.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_criterio_politico", Tipo = "int", Valor = _tbl_proyectos.criterio_politico.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_criterio_tecnico_institucional", Tipo = "int", Valor = _tbl_proyectos.criterio_tecnico_institucional.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_dependencia_id", Tipo = "String", Valor = _tbl_proyectos.tbl_dependencia_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_programa_id", Tipo = "String", Valor = "" });

                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_proyecto);
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

        public ResponseGeneric<List<Crudresponse>> update(tbl_proyectos _tbl_proyectos)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "int", Valor = "3" });                
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = _tbl_proyectos.id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_proyecto_id", Tipo = "String", Valor = _tbl_proyectos.tbl_tipo_proyecto_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_proyecto", Tipo = "String", Valor = _tbl_proyectos.proyecto.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_objetivo", Tipo = "String", Valor = _tbl_proyectos.objetivo.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_alcance", Tipo = "String", Valor = _tbl_proyectos.alcance.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_criticidad_proyecto_id", Tipo = "String", Valor = _tbl_proyectos.tbl_criticidad_proyecto_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_inicio", Tipo = "String", Valor = _tbl_proyectos.fecha_incio.ToString("yyyy-MM-ddTHH:mm:ss") });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_fin", Tipo = "String", Valor = _tbl_proyectos.fecha_fin.ToString("yyyy-MM-ddTHH:mm:ss") });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_estatus_proyecto_id", Tipo = "String", Valor = _tbl_proyectos.tbl_estatus_proyecto_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_ejecucion_id", Tipo = "String", Valor = _tbl_proyectos.tbl_tipo_ejecucion_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_etapa_proyecto_id", Tipo = "String", Valor = _tbl_proyectos.tbl_etapa_proyecto_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_analisis_id", Tipo = "String", Valor = _tbl_proyectos.tbl_tipo_analisis_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_nivel_analisis_id", Tipo = "String", Valor = _tbl_proyectos.tbl_nivel_analisis_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_criterio_economico", Tipo = "int", Valor = _tbl_proyectos.criterio_economico.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_criterio_social", Tipo = "int", Valor = _tbl_proyectos.criterio_social.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_criterio_ambienta", Tipo = "int", Valor = _tbl_proyectos.criterio_ambienta.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_criterio_politico", Tipo = "int", Valor = _tbl_proyectos.criterio_politico.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_criterio_tecnico_institucional", Tipo = "int", Valor = _tbl_proyectos.criterio_tecnico_institucional.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_dependencia_id", Tipo = "String", Valor = _tbl_proyectos.tbl_dependencia_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_programa_id", Tipo = "String", Valor = "" });

                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_proyecto);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("upd", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> delete(tbl_proyectos _tbl_proyectos)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "int", Valor = "4" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = _tbl_proyectos.id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_proyecto_id", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_proyecto", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_objetivo", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_alcance", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_criticidad_proyecto_id", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_inicio", Tipo = "String", Valor = "0001-01-01T00:00:00" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_fin", Tipo = "String", Valor = "0001-01-01T00:00:00" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_estatus_proyecto_id", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_ejecucion_id", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_etapa_proyecto_id", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_analisis_id", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_nivel_analisis_id", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_criterio_economico", Tipo = "int", Valor = "0" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_criterio_social", Tipo = "int", Valor = "0" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_criterio_ambienta", Tipo = "int", Valor = "0" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_criterio_politico", Tipo = "int", Valor = "0" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_criterio_tecnico_institucional", Tipo = "int", Valor = "0" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_dependencia_id", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_programa_id", Tipo = "String", Valor = "" });

                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_proyecto);
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
        public ResponseGeneric<List<DropDownList>> Get_Tipo_P(string id_ins)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "instancia", Tipo = "String", Valor = id_ins });


                List<DropDownList> Lista = new List<DropDownList>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_tipo_proyecto_dropdown);
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

        public ResponseGeneric<List<DropDownList>> Get_Criticidad()
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                
                List<DropDownList> Lista = new List<DropDownList>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_criticidad_proyecto_dropdown);
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

        public ResponseGeneric<List<DropDownList>> Get_Tipo_Ejecucion()
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();

                List<DropDownList> Lista = new List<DropDownList>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_tipo_ejecucion_proyecto_dropdown);
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

        public ResponseGeneric<List<DropDownList>> Get_Estatus_Proyecto()
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();

                List<DropDownList> Lista = new List<DropDownList>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_estatus_proyecto);
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
    }
}
