using Conexion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Contrato;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.IO;
using Utilidades.Log4Net;

namespace AccesoDatos
{
    public class tbl_contrato_acceso_datos : crud_contrato
    {
        public BDParametros GeneracionParametros = new BDParametros();
        private readonly ILoggerManager _logger;
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string StoreProcedure_Contrato = "sp_get_contrato";
        string StoreProcedure_ContratoTipo = "sp_get_contrato_tipocontrato";
        string StoreProcedure_ContratoVista = "sp_get_vista_contrato";
        string StoreProcedure_ContratoCargaM = "sp_contrato";
        string sp_get_acuerdo_pe = "sp_get_acuerdo_pe";
        string sp_get_acuerdo_rc = "sp_get_acuerdo_rc";

        string StoreProcedure_ContratoXRol = "sp_get_contrato_x_rol";

        public tbl_contrato_acceso_datos()
        {
            _logger = new LoggerManager();
        }
        public ResponseGeneric<List<tbl_contrato>> Consultar(String dependencia)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "dependencia", Tipo = "String", Valor = dependencia.ToString() });

                List<tbl_contrato> Lista = new List<tbl_contrato>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_Contrato);
                            Lista = conexion.Query<tbl_contrato>().FromSql<tbl_contrato>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_contrato>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_contrato>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_contrato_list>> ConsultarLista(string dependencia)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "dependencia", Tipo = "String", Valor = dependencia.ToString() });

                List<tbl_contrato_list> Lista = new List<tbl_contrato_list>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_ContratoTipo);
                            Lista = conexion.Query<tbl_contrato_list>().FromSql<tbl_contrato_list>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_contrato_list>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("ConsultarLista", ex);
                return new ResponseGeneric<List<tbl_contrato_list>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_contrato_list>> ConsultarListaXRol(string rol, string dependencia, string usuario)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "rol", Tipo = "String", Valor = rol.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "dependencia", Tipo = "String", Valor = dependencia.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "usuario", Tipo = "String", Valor = usuario.ToString() });

                List<tbl_contrato_list> Lista = new List<tbl_contrato_list>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_ContratoXRol);
                            Lista = conexion.Query<tbl_contrato_list>().FromSql<tbl_contrato_list>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_contrato_list>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("ConsultarListaRol", ex);
                return new ResponseGeneric<List<tbl_contrato_list>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_contrato_vista>> ConsultarVista(String contrato)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = contrato.ToString() });

                List<tbl_contrato_vista> Lista = new List<tbl_contrato_vista>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_ContratoVista);
                            Lista = conexion.Query<tbl_contrato_vista>().FromSql<tbl_contrato_vista>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_contrato_vista>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("ConsultarVista", ex);
                return new ResponseGeneric<List<tbl_contrato_vista>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_acuerdo_pe>> ConsultarAcuerdosPE(String usuario, String contrato)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "id_usuario", Tipo = "String", Valor = usuario.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "id_contrato", Tipo = "String", Valor = contrato.ToString() });

                List<tbl_acuerdo_pe> Lista = new List<tbl_acuerdo_pe>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_acuerdo_pe);
                            Lista = conexion.Query<tbl_acuerdo_pe>().FromSql<tbl_acuerdo_pe>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_acuerdo_pe>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_acuerdo_pe>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_acuerdo_pe>> ConsultarAcuerdosRC(String contrato)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "id_contrato", Tipo = "String", Valor = contrato.ToString() });

                List<tbl_acuerdo_pe> Lista = new List<tbl_acuerdo_pe>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_acuerdo_rc);
                            Lista = conexion.Query<tbl_acuerdo_pe>().FromSql<tbl_acuerdo_pe>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_acuerdo_pe>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_acuerdo_pe>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> CargaMasivaContrato(tbl_contrato_add contrato)
        {
            try
            {
                #region Parametros
               List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                //ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = contrato.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = contrato.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_contrato_id", Tipo = "String", Valor = contrato.p_tbl_tipo_contrato_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_prioridad_id", Tipo = "String", Valor = contrato.p_tbl_prioridad_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_estatus_contrato_id", Tipo = "String", Valor = contrato.p_tbl_estatus_contrato_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_proyecto_id", Tipo = "String", Valor = contrato.p_tbl_proyecto_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_procedimiento_id", Tipo = "String", Valor = contrato.p_tbl_procedimiento_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_numero", Tipo = "String", Valor = contrato.p_numero.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_objeto", Tipo = "String", Valor = contrato.p_objeto.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_firma", Tipo = "String", Valor = contrato.p_fecha_firma.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_Iinicio", Tipo = "String", Valor = contrato.p_fecha_Iinicio.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_fin", Tipo = "String", Valor = contrato.p_fecha_fin.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_formalizacion", Tipo = "String", Valor = contrato.p_fecha_formalizacion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_ampliacion", Tipo = "String", Valor = contrato.p_ampliacion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_requiere_renovacion", Tipo = "String", Valor = contrato.p_requiere_renovacion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_satisfactorio", Tipo = "String", Valor = contrato.p_satisfactorio.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_porc_max_penalizacion", Tipo = "String", Valor = contrato.p_porc_max_penalizacion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_porc_max_deductivas", Tipo = "String", Valor = contrato.p_porc_max_deductivas.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_presento_garantia", Tipo = "String", Valor = contrato.p_presento_garantia.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_porc_garantia", Tipo = "String", Valor = contrato.p_porc_garantia.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_garantia", Tipo = "String", Valor = contrato.p_monto_garantia.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_es_administradora", Tipo = "String", Valor = contrato.p_es_administradora.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_activo", Tipo = "String", Valor = contrato.p_activo.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_token", Tipo = "String", Valor = contrato.p_token.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_nombre", Tipo = "String", Valor = contrato.p_nombre.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_max_sin_iva", Tipo = "String", Valor = contrato.p_monto_max_sin_iva.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_min_sin_iva", Tipo = "String", Valor = contrato.p_monto_min_sin_iva.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_registro", Tipo = "String", Valor = contrato.p_fecha_registro.ToString() });





                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_ContratoCargaM);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("CargaMasivaContrato", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }






        public ResponseGeneric<List<tbl_tipo_contrato>> ConsultarTiposContrato()
        {
            throw new NotImplementedException();
        }
    }
}
