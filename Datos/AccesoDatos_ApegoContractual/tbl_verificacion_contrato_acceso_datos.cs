using Conexion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.PreguntasFormulario;
using Modelos.Modelos.Verificacion;
using Modelos.Modelos.VerificacionContrato;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.IO;
using Utilidades.Log4Net;

namespace AccesoDatos
{
    public class tbl_verificacion_contrato_acceso_datos : crud_verificacioncontrato
    {
        public BDParametros GeneracionParametros = new BDParametros();
        private readonly ILoggerManager _logger;
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string StoreProcedure = "sp_get_verificacioncontrato";
        string StoreProcedure_add = "sp_verificacioncontrato";
        string StoreProcedure_add_V2 = "sp_verificacioncontrato_v2";
        string sp_get_verificacion_x_contrato = "sp_get_verificacion_x_contrato";

        public tbl_verificacion_contrato_acceso_datos()
        {
            _logger = new LoggerManager();
        }

        public ResponseGeneric<List<lista_verificados>> Consultar(string Dependencia, string Contrato)
        {
            throw new NotImplementedException();
        }

        public ResponseGeneric<tbl_verificacion_contrato> ConsultarVerficacionPregunta(String Contrato, String Pregunta)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "contrato", Tipo = "String", Valor = Contrato.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "pregunta", Tipo = "String", Valor = Pregunta.ToString() });

                tbl_verificacion_contrato Lista = new tbl_verificacion_contrato();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure);
                            Lista = conexion.Query<tbl_verificacion_contrato>().FromSql<tbl_verificacion_contrato>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<tbl_verificacion_contrato>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<tbl_verificacion_contrato>(ex);
            }
        }

        public ResponseGeneric<List<lista_verificados>> Consultar_SinContrato(string Dependencia)
        {
            throw new NotImplementedException();
        }

        public ResponseGeneric<List<Crudresponse>> add(tbl_verificacion_contrato_add verificacion)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "Int", Valor = verificacion.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = verificacion.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_id", Tipo = "String", Valor = verificacion.p_tbl_contrato_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_usuario_id", Tipo = "String", Valor = verificacion.p_tbl_usuario_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_pregunta_formulario_id", Tipo = "String", Valor = verificacion.p_tbl_pregunta_formulario_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_estatus_verificacion_id", Tipo = "String", Valor = verificacion.p_tbl_estatus_verificacion_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_inclusion", Tipo = "String", Valor = verificacion.p_inclusion.ToString() });

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
        public ResponseGeneric<List<Crudresponse>> addV2(tbl_verificacion_contrato_add verificacion)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "Int", Valor = verificacion.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = verificacion.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_id", Tipo = "String", Valor = verificacion.p_tbl_contrato_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_usuario_id", Tipo = "String", Valor = verificacion.p_tbl_usuario_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_pregunta_formulario_id", Tipo = "String", Valor = verificacion.p_tbl_pregunta_formulario_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_estatus_verificacion_id", Tipo = "String", Valor = verificacion.p_tbl_estatus_verificacion_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_inclusion", Tipo = "String", Valor = verificacion.p_inclusion.ToString() });
                //ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_verificacion", Tipo = "String", Valor = verificacion.p_fecha_verificacion.ToString("yyyy-MM-ddTHH:mm:ss") });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_verificacion", Tipo = "String", Valor = verificacion.p_fecha_verificacion!=null? verificacion.p_fecha_verificacion.Value.ToString("yyyy-MM-ddTHH:mm:ss"): "" });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_pregunta_personalizada", Tipo = "String", Valor = verificacion.p_pregunta_personalizada == null? "": verificacion.p_pregunta_personalizada.ToString() });

                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_add_V2);
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

        public ResponseGeneric<List<Crudresponse>> DeleteV2(tbl_verificacion_contrato_add verificacion)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "Int", Valor = verificacion.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = verificacion.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_id", Tipo = "String", Valor = verificacion.p_tbl_contrato_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_usuario_id", Tipo = "String", Valor = verificacion.p_tbl_usuario_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_pregunta_formulario_id", Tipo = "String", Valor = verificacion.p_tbl_pregunta_formulario_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_estatus_verificacion_id", Tipo = "String", Valor = verificacion.p_tbl_estatus_verificacion_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_inclusion", Tipo = "String", Valor = verificacion.p_inclusion.ToString() });
                //ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_verificacion", Tipo = "String", Valor = verificacion.p_fecha_verificacion.ToString("yyyy-MM-ddTHH:mm:ss") });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_verificacion", Tipo = "String", Valor = verificacion.p_fecha_verificacion != null ? verificacion.p_fecha_verificacion.Value.ToString("yyyy-MM-ddTHH:mm:ss") : "" });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_pregunta_personalizada", Tipo = "String", Valor = verificacion.p_pregunta_personalizada == null ? "" : verificacion.p_pregunta_personalizada.ToString() });

                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_add_V2);
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

        public ResponseGeneric<List<Crudresponse>> delete(tbl_verificacion_contrato_add verificacion)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "Int", Valor = NullToString(verificacion.p_opt) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = NullToString(verificacion.p_id) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_id", Tipo = "String", Valor = NullToString(verificacion.p_tbl_contrato_id) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_usuario_id", Tipo = "String", Valor = NullToString(verificacion.p_tbl_usuario_id) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_pregunta_formulario_id", Tipo = "String", Valor = NullToString(verificacion.p_tbl_pregunta_formulario_id) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_estatus_verificacion_id", Tipo = "String", Valor = NullToString(verificacion.p_tbl_estatus_verificacion_id) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_inclusion", Tipo = "String", Valor = NullToString(verificacion.p_inclusion) });

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

        public ResponseGeneric<List<lista_verificion_x_contrato>> GetVerificacionxContrato(String contrato)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_idcontrato", Tipo = "String", Valor = contrato });              
                List<lista_verificion_x_contrato> Lista = new List<lista_verificion_x_contrato>();
                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_verificacion_x_contrato);
                            Lista = conexion.Query<lista_verificion_x_contrato>().FromSql<lista_verificion_x_contrato>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }
                }
                #endregion
                return new ResponseGeneric<List<lista_verificion_x_contrato>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("GetVerificacionxContrato", ex);
                return new ResponseGeneric<List<lista_verificion_x_contrato>>(ex);
            }
        }


    }
}
