using Conexion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.PreguntasFormulario;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.IO;
using Utilidades.Log4Net;

namespace AccesoDatos
{
    public class tbl_preguntas_formulario_acceso_datos : crud_preguntasformulario
    {
        public BDParametros GeneracionParametros = new BDParametros();
        private readonly ILoggerManager _logger;
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string StoreProcedure_add = "sp_preguntaformulario";
        string StoreProcedure = "sp_get_preguntasdependencia";
        public tbl_preguntas_formulario_acceso_datos()
        {
            _logger = new LoggerManager();
        }
        public ResponseGeneric<List<tbl_pregunta_formulario>> Consultar(String Dependencia)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "dependencia", Tipo = "String", Valor = Dependencia.ToString() });

                List<tbl_pregunta_formulario> Lista = new List<tbl_pregunta_formulario>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure);
                            Lista = conexion.Query<tbl_pregunta_formulario>().FromSql<tbl_pregunta_formulario>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_pregunta_formulario>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_pregunta_formulario>>(ex);
            }
        }


        public ResponseGeneric<List<Crudresponse>> add(tbl_pregunta_formulario_add pregunta)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "Int", Valor = pregunta.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = pregunta.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_dependencia_id", Tipo = "String", Valor = pregunta.p_tbl_dependencia_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_pregunta", Tipo = "String", Valor = pregunta.p_pregunta.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_inclusion", Tipo = "String", Valor = pregunta.p_inclusion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_estatus", Tipo = "String", Valor = pregunta.p_estatus.ToString() });


                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_add);
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

        public ResponseGeneric<List<Crudresponse>> update(tbl_pregunta_formulario_add pregunta)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "Int", Valor = pregunta.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = pregunta.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_dependencia_id", Tipo = "String", Valor = pregunta.p_tbl_dependencia_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_pregunta", Tipo = "String", Valor = pregunta.p_pregunta.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_inclusion", Tipo = "String", Valor = pregunta.p_inclusion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_estatus", Tipo = "String", Valor = pregunta.p_estatus.ToString() });


                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_add);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("update", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }


        public ResponseGeneric<List<Crudresponse>> delete(tbl_pregunta_formulario_add pregunta)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "Int", Valor = NullToString(pregunta.p_opt) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = NullToString(pregunta.p_id) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_dependencia_id", Tipo = "String", Valor = NullToString(pregunta.p_tbl_dependencia_id) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_pregunta", Tipo = "String", Valor = NullToString(pregunta.p_pregunta) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_inclusion", Tipo = "String", Valor = NullToString(pregunta.p_inclusion) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_estatus", Tipo = "String", Valor = NullToString(pregunta.p_estatus) });


                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_add);
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

        public static string NullToString(object Value)
        {

            // Value.ToString() allows for Value being DBNull, but will also convert int, double, etc.
            return Value == null ? "" : Value.ToString();



        }

    }
}
