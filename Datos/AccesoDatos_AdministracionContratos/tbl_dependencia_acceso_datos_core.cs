using Conexion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Dependencia;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.IO;
using Utilidades.Log4Net;

namespace AccesoDatos_AdministracionDeContratos
{
    public class tbl_dependencia_acceso_datos_core
    {
        public BDParametros GeneracionParametros = new BDParametros();
        private readonly ILoggerManager _logger;
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string sp_dependencia = "sp_dependencia";
        string sp_get_dependencia = "sp_get_dependencia_core";
        string sp_get_ciudad_dropdown = "sp_getciudad";
        string sp_get_tbl_estado = "sp_get_tbl_estado";
        string sp_get_tbl_ciudad_estado = "sp_get_tbl_ciudad_estado";
        string sp_get_Ejercicios = "sp_get_ejercicios";
        string sp_partida_dependencia_list = "sp_partida_dependencia_list";
        string sp_partida_dependencia = "sp_partida_dependencia";
        string sp_partida_dependencia_asignacion_c = "sp_partida_dependencia_asignacion_c";
        string sp_get_suma_c_p_areas_dep = "sp_get_suma_c_p_areas_dep";

        string sp_get_dependencia_x_permiso = "sp_get_dependencia_x_permiso_usuario";


        public tbl_dependencia_acceso_datos_core()
        {
            _logger = new LoggerManager();
        }
        public ResponseGeneric<List<tbl_dependencia>> Get(String dependencia)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "dependencia", Tipo = "String", Valor = dependencia});

                List<tbl_dependencia> Lista = new List<tbl_dependencia>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_dependencia);
                            Lista = conexion.Query<tbl_dependencia>().FromSql<tbl_dependencia>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_dependencia>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_dependencia>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_dependencia_x_permiso>> GetXPermisoUsuario(String dependencia,String idusuario)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "dependencia", Tipo = "String", Valor = dependencia });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "idusuario", Tipo = "String", Valor = idusuario });

                List<tbl_dependencia_x_permiso> Lista = new List<tbl_dependencia_x_permiso>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_dependencia_x_permiso);
                            Lista = conexion.Query<tbl_dependencia_x_permiso>().FromSql<tbl_dependencia_x_permiso>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_dependencia_x_permiso>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_dependencia_x_permiso>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_partida_list>> Get_Lista_Partidas(string id_ejercicio, string id_instancia, string id_dependencia)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "id_ejercicio", Tipo = "String", Valor = id_ejercicio });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "id_instancia", Tipo = "String", Valor = id_instancia });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "id_dependencia", Tipo = "String", Valor = id_dependencia });

                List<tbl_partida_list> Lista = new List<tbl_partida_list>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_partida_dependencia_list);
                            Lista = conexion.Query<tbl_partida_list>().FromSql<tbl_partida_list>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_partida_list>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_partida_list>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Add(tbl_dependencia tbl_dependencia)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "int", Valor = "2" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = tbl_dependencia.id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_dependencia", Tipo = "String", Valor = tbl_dependencia.dependencia.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_ciudad_id", Tipo = "String", Valor = tbl_dependencia.tbl_ciudad_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_instancia_id", Tipo = "String", Valor = tbl_dependencia.tbl_instancia_id.ToString() });

                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_dependencia);
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

        public ResponseGeneric<List<Crudresponse>> Add_Partida(tbl_partida _tbl_partida)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "int", Valor = "2" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_dependencia_id", Tipo = "String", Valor = _tbl_partida.tbl_dependencia_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_partida_id", Tipo = "String", Valor = _tbl_partida.tbl_partida_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_ejercicio_id", Tipo = "String", Valor = _tbl_partida.tbl_ejercicio_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id_propietario", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_planeado", Tipo = "Decimal", Valor = "0" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_asignado", Tipo = "Decimal", Valor = "0" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_ejercido", Tipo = "Decimal", Valor = "0" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_devengado", Tipo = "Decimal", Valor = "0" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_estatus_partida", Tipo = "Tinyint", Valor = "1" });

                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_partida_dependencia);
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
        public ResponseGeneric<List<CrudresponseIdentificador>> Update_Partida(tbl_partida_upd _tbl_partida)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = "3" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = _tbl_partida.tbl_partida_dependencia_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_dependencia_id", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_partida_id", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_ejercicio_id", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id_propietario", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_planeado", Tipo = "String", Valor = "0" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_asignado", Tipo = "String", Valor = _tbl_partida.monto_asignado.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_ejercido", Tipo = "String", Valor = "0" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_devengado", Tipo = "String", Valor = "0" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_estatus_partida", Tipo = "String", Valor = "1" });

                List<CrudresponseIdentificador> Lista = new List<CrudresponseIdentificador>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_partida_dependencia_asignacion_c);
                            Lista = conexion.Query<CrudresponseIdentificador>().FromSql<CrudresponseIdentificador>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<CrudresponseIdentificador>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("upd", ex);
                return new ResponseGeneric<List<CrudresponseIdentificador>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> delete_partida(tbl_partida _tbl_partida)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "int", Valor = "4" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = _tbl_partida.id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_dependencia_id", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_partida_id", Tipo = "String", Valor = ""});
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_ejercicio_id", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id_propietario", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_planeado", Tipo = "Decimal", Valor = "0" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_asignado", Tipo = "Decimal", Valor = "0" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_ejercido", Tipo = "Decimal", Valor = "0" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_devengado", Tipo = "Decimal", Valor = "0" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_estatus_partida", Tipo = "Tinyint", Valor = "1" });

                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_partida_dependencia);
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
        public ResponseGeneric<List<Crudresponse>> update(tbl_dependencia tbl_dependencia)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "int", Valor = "3" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = tbl_dependencia.id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_dependencia", Tipo = "String", Valor = tbl_dependencia.dependencia.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_ciudad_id", Tipo = "String", Valor = tbl_dependencia.tbl_ciudad_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_instancia_id", Tipo = "String", Valor = tbl_dependencia.tbl_instancia_id.ToString() });

                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_dependencia);
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
        public ResponseGeneric<List<Crudresponse>> delete(tbl_dependencia tbl_dependencia)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "int", Valor = "4" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = tbl_dependencia.id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_dependencia", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_ciudad_id", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_instancia_id", Tipo = "String", Valor = "" });

                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_dependencia);
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

        public ResponseGeneric<List<DropDownList>> FillDropC()
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
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_ciudad_dropdown);
                            Lista = conexion.Query<DropDownList>().FromSql<DropDownList>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<DropDownList>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("fill", ex);
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }
        public ResponseGeneric<List<DropDownList>> get_tbl_estado()
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
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_tbl_estado);
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
        public ResponseGeneric<List<DropDownList>> get_tbl_estado_ciudad(Guid tbl_estado_id)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tbl_estado_id", Tipo = "String", Valor = tbl_estado_id.ToString() });


                List<DropDownList> Lista = new List<DropDownList>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_tbl_ciudad_estado);
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
        public ResponseGeneric<List<DropDownList>> get_Ejercicios()
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
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_Ejercicios);
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

        public ResponseGeneric<List<suma_c_areas_padre>> Get_suma_areas_p(String tb_partida_dependencia)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_partida_dependencia_id", Tipo = "String", Valor = tb_partida_dependencia.ToString() });
                List<suma_c_areas_padre> Lista = new List<suma_c_areas_padre>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_suma_c_p_areas_dep);
                            Lista = conexion.Query<suma_c_areas_padre>().FromSql<suma_c_areas_padre>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<suma_c_areas_padre>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<suma_c_areas_padre>>(ex);
            }
        }
    }

}
