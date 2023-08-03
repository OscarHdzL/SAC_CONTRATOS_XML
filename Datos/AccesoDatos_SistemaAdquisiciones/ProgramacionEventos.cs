using System;

using Modelos.Interfaz;
using Modelos.Modelos;
using System.Collections.Generic;
using Conexion;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.IO;
using Modelos.Response;

namespace AccesoDatos
{
    public class ProgramacionEventos
    {
        public BDParametros GeneracionParametros = new BDParametros();
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string sp_programacion = "sp_programacion";
        string sp_tbl_tipo_programacion = "sp_tbl_tipo_programacion";
        string sp_getciudad_ = "sp_getciudad";
        string sp_getestado_ = "sp_getestado"; 
        string sp_eventos_agendados = "sp_eventos_agendados";
        string sp_get_programacion_solicitud_tipo = "sp_get_programacion_solicitud_tipo";





        public ResponseGeneric<List<Crudresponse>> add(ProgramacionEventosEntidad obj)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = obj.p_id.ToString() });
         
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_programacion_id", Tipo = "String", Valor = obj.p_tbl_tipo_programacion_id.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_Fecha_Evento", Tipo = "String", Valor = obj.p_Fecha_Evento.ToString("yyyy-MM-dd")});
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_Direccion", Tipo = "String", Valor = obj.p_direccion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_ciudad_id", Tipo = "String", Valor = obj.p_tbl_ciudad_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_estatus_programacion_id", Tipo = "String", Valor = obj.p_tbl_estatus_programacion_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_Token", Tipo = "String", Valor = obj.p_token == null ? "" : obj.p_token.ToString()});
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_inclusion", Tipo = "String", Valor = obj.p_inclusion.ToString("yyyy-MM-dd") });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_action", Tipo = "String", Valor = obj.p_action.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_solicitud_id", Tipo = "String", Valor = obj.p_tbl_solicitud_id.ToString() });
                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_programacion);
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
        public ResponseGeneric<List<DropDownList>> get_sp_tbl_tipo_programacion()
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
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_tbl_tipo_programacion);
                            Lista = conexion.Query<DropDownList>().FromSql<DropDownList>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<DropDownList>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }
        public ResponseGeneric<List<DropDownList>> get_sp_getciudad(Guid id)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "id_", Tipo = "String", Valor = id.ToString() });

                List<DropDownList> Lista = new List<DropDownList>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_getciudad_);
                            Lista = conexion.Query<DropDownList>().FromSql<DropDownList>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<DropDownList>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }

        public ResponseGeneric<List<DropDownList>> tipo_pro_x_sigla(string sigla)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_sigla", Tipo = "String", Valor = sigla });

                List<DropDownList> Lista = new List<DropDownList>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, "sp_get_tipo_programacion_dropdown");
                            Lista = conexion.Query<DropDownList>().FromSql<DropDownList>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<DropDownList>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }
        public ResponseGeneric<List<DropDownList>> get_sp_getestado()
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
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_getestado_);
                            Lista = conexion.Query<DropDownList>().FromSql<DropDownList>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<DropDownList>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_agendados>> get_sp_eventos_agendados(Guid id,Guid tipo)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_solicitud_id", Tipo = "String", Valor = id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_programacion", Tipo = "String", Valor = tipo.ToString() });


                List<tbl_agendados> Lista = new List<tbl_agendados>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_eventos_agendados);
                            Lista = conexion.Query<tbl_agendados>().FromSql<tbl_agendados>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_agendados>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_agendados>>(ex);
            }
        }

        


            public ResponseGeneric<tbl_programacion> get_ultimo_evento_activo(String idSolicitud, String tipo)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "solicitud", Tipo = "String", Valor = idSolicitud.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tipo_programacion", Tipo = "String", Valor = tipo.ToString() });


                tbl_programacion Lista = new tbl_programacion();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_programacion_solicitud_tipo);
                            Lista = conexion.Query<tbl_programacion>().FromSql<tbl_programacion>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<tbl_programacion>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<tbl_programacion>(ex);
            }
        }



    }
}
