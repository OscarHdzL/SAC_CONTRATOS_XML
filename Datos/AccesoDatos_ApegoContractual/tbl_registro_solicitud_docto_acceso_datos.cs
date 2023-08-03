using Conexion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.RegSolDoc;
using Modelos.Modelos.ResponsablesApego;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.IO;
using Utilidades.Log4Net;

namespace AccesoDatos
{
    public class tbl_registro_solicitud_docto_acceso_datos : crud_regsoldoc
    {
        public BDParametros GeneracionParametros = new BDParametros();
        private readonly ILoggerManager _logger;
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string StoreProcedure_RegSolDoc = "sp_get_listaregsoldoc_contrato";
        string StoreProcedure_RegSolDoc_Edit = "sp_get_regsoldoc";
        string StoreProcedure = "sp_regsoldoc";
        string StoreProcedure_RegSolDoc_Expediente = "sp_get_regsoldoc_expediente";

        public tbl_registro_solicitud_docto_acceso_datos()
        {
            _logger = new LoggerManager();
        }

        public ResponseGeneric<List<tbl_registro_solicitud_docto_list>> Consultar_RegSolDoc(String contrato)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "contrato", Tipo = "String", Valor = contrato });

                List<tbl_registro_solicitud_docto_list> Lista = new List<tbl_registro_solicitud_docto_list>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_RegSolDoc);
                            Lista = conexion.Query<tbl_registro_solicitud_docto_list>().FromSql<tbl_registro_solicitud_docto_list>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_registro_solicitud_docto_list>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_registro_solicitud_docto_list>>(ex);
            }
        }


        public ResponseGeneric<tbl_contrato_solicitud_docto> GetSolicitud_RegSolDoc(String contrato, String solicitud)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "contrato", Tipo = "String", Valor = contrato });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "solicitud", Tipo = "String", Valor = solicitud });

                tbl_contrato_solicitud_docto Lista = new tbl_contrato_solicitud_docto();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_RegSolDoc_Edit);
                            Lista = conexion.Query<tbl_contrato_solicitud_docto>().FromSql<tbl_contrato_solicitud_docto>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<tbl_contrato_solicitud_docto>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("get", ex);
                return new ResponseGeneric<tbl_contrato_solicitud_docto>(ex);
            }
        }


        public ResponseGeneric<List<tbl_contrato_solicitud_docto_expediente>> GetSolicitud_Expediente(String contrato)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "contrato", Tipo = "String", Valor = contrato });
                

                List<tbl_contrato_solicitud_docto_expediente> Lista = new List<tbl_contrato_solicitud_docto_expediente>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_RegSolDoc_Expediente);
                            Lista = conexion.Query<tbl_contrato_solicitud_docto_expediente>().FromSql<tbl_contrato_solicitud_docto_expediente>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_contrato_solicitud_docto_expediente>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("get", ex);
                return new ResponseGeneric<List<tbl_contrato_solicitud_docto_expediente>>(ex);
            }
        }


        public ResponseGeneric<List<Crudresponse>> add(tbl_contrato_solicitud_docto_add regsoldoc)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "Int", Valor = regsoldoc.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = regsoldoc.p_id.ToString() });
                //ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_id", Tipo = "String", Valor = regsoldoc.p_tbl_contrato_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_nombre_documento", Tipo = "String", Valor = regsoldoc.p_nombre_documento.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_solicitud", Tipo = "String", Valor = regsoldoc.p_fecha_solicitud.ToString() });//Convert.ToDateTime(regsoldoc.p_fecha_solicitud).ToShortDateString()});
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_entrega", Tipo = "String", Valor = regsoldoc.p_fecha_entrega.ToString() }); //Convert.ToDateTime(regsoldoc.p_fecha_entrega).ToShortDateString()});
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tipo_entregable", Tipo = "String", Valor = regsoldoc.p_tipo_entregable.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_observacion", Tipo = "String", Valor = regsoldoc.p_observacion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_correo_solicitud", Tipo = "String", Valor = regsoldoc.p_correo_solicitud.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_inclusion", Tipo = "DateTime", Valor = regsoldoc.p_inclusion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_estatus", Tipo = "String", Valor = regsoldoc.p_estatus.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_servidor_resp_id", Tipo = "String", Valor = regsoldoc.p_tbl_contrato_servidor_resp_id.ToString()});


                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure);
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


    }
}
