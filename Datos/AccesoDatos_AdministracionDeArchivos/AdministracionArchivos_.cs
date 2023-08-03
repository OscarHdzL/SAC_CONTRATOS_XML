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

namespace AccesoDatos_AdministracionArchivos
{
    public class AdministracionArchivos_
    {
        public BDParametros GeneracionParametros = new BDParametros();
        private readonly ILoggerManager _logger;
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string _sp__documentary_information_add = "_sp__documentary_information_add";
        string _sp__documentary_information_add_entrega = "_sp__documentary_information_add_entrega";
        string _sp__documentary_information_add_entrega_ubicacion = "_sp__documentary_information_add_entrega_ubicacion";
        string _sp__documentary_information_add_entrega_monitoreo = "_sp__documentary_information_add_entrega_monitoreo";
        string _sp__documentary_information_download = "_sp__documentary_information_download";
        string _sp__documentary_information_delete = "_sp__documentary_information_delete";
        String sp_deleted_file_plan_entrega_archivo_global = "sp_deleted_file_plan_entrega_archivo_global";
        string _sp_insert_token_documentary_versions = "_sp_insert_token_documentary_versions";
        string _sp__documentary_information_Render = "_sp__documentary_information_Render";
        string _sp__documentary_information_DocInfo = "_sp__documentary_information_DocInfo";
        string _sp__documentary_information_GetID = "_sp__documentary_information_GetID";
        string _sp__documentary_information_Render_version = "_sp__documentary_information_Render_version";
        string _sp__documentary_information_Remove = "_sp__documentary_information_Remove";
        string _sp__documentary_information_GetListVersions = "_sp__documentary_information_GetListVersions"; 
        string _sp__documentary_information_update_state = "_sp__documentary_information_update_state";
        String sp_deleted_file_plan_ubicacion_archivo = "sp_deleted_file_plan_entrega_archivo";
        string _sp_documentary_contract_list = "_sp__documentary_list_contract";
        string _sp_documentary_remove_file = "_sp__documentary_remove_file";
        string _sp__documentary_information_download_by_id = "_sp__documentary_information_download_by_id";

        public AdministracionArchivos_()
        {
            _logger = new LoggerManager();
        }


        public ResponseGeneric<List<ResponseFileManagerInfo>> GetDocInfo(Guid id_uri)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "id_uri", Tipo = "String", Valor = id_uri.ToString() });



                List<ResponseFileManagerInfo> Lista = new List<ResponseFileManagerInfo>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, _sp__documentary_information_DocInfo);
                            Lista = conexion.Query<ResponseFileManagerInfo>().FromSql<ResponseFileManagerInfo>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<ResponseFileManagerInfo>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<ResponseFileManagerInfo>>(ex);
            }
        }


        public ResponseGeneric<List<ResponseFileManager>> GetBuffer(Guid id_uri)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "id_uri", Tipo = "String", Valor = id_uri.ToString() });
 


                List<ResponseFileManager> Lista = new List<ResponseFileManager>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, _sp__documentary_information_Render);
                            Lista = conexion.Query<ResponseFileManager>().FromSql<ResponseFileManager>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<ResponseFileManager>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<ResponseFileManager>>(ex);
            }
        }

        public ResponseGeneric<List<ResponseFileManager>> GetBufferVersion(Guid id_uri, int version_)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "id_uri", Tipo = "String", Valor = id_uri.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "version", Tipo = "String", Valor = version_.ToString() });


                List<ResponseFileManager> Lista = new List<ResponseFileManager>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, _sp__documentary_information_Render_version);
                            Lista = conexion.Query<ResponseFileManager>().FromSql<ResponseFileManager>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<ResponseFileManager>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<ResponseFileManager>>(ex);
            }
        }


        public ResponseGeneric<List<ResponseFileManager>> Gettoken(String id_uri)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "token", Tipo = "String", Valor = id_uri.ToString() });



                List<ResponseFileManager> Lista = new List<ResponseFileManager>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, _sp__documentary_information_GetID);
                            Lista = conexion.Query<ResponseFileManager>().FromSql<ResponseFileManager>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<ResponseFileManager>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<ResponseFileManager>>(ex);
            }
        }



        public ResponseGeneric<List<ResponseFileManager>> Get(AdministracionArchivos Filemanager)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "id", Tipo = "String", Valor = Filemanager.id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "file_extension", Tipo = "String", Valor = Filemanager.file_extension });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "file_author", Tipo = "String", Valor = Filemanager.file_author });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "file_Descripcion", Tipo = "String", Valor = Filemanager.file_descripcion });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "file_name", Tipo = "String", Valor = Filemanager.file_name });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "versions_title", Tipo = "String", Valor = Filemanager.versions_title });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "versions_description", Tipo = "String", Valor = Filemanager.versions_description });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "versions_author", Tipo = "String", Valor = Filemanager.versions_author });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "versions_size", Tipo = "String", Valor = Filemanager.versions_size });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "minutes", Tipo = "String", Valor = Filemanager.minutes.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tipo_documento_id", Tipo = "String", Valor = Filemanager.file_tbl_tipo_documento_id!=null? Filemanager.file_tbl_tipo_documento_id:"NULL" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "contrato_id", Tipo = "String", Valor = Filemanager.file_tbl_contrato_id != null ? Filemanager.file_tbl_contrato_id : "NULL" });

                List<ResponseFileManager> Lista = new List<ResponseFileManager>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, _sp__documentary_information_add);
                            Lista = conexion.Query<ResponseFileManager>().FromSql<ResponseFileManager>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<ResponseFileManager>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<ResponseFileManager>>(ex);
            }
        }

        //public ResponseGeneric<List<ResponseFileManager>> GetNew(AdministracionArchivos Filemanager, string entrega)
        //{
        //    try
        //    {
        //        #region Parametros
        //        List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "id", Tipo = "String", Valor = Filemanager.id.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "file_extension", Tipo = "String", Valor = Filemanager.file_extension });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "file_author", Tipo = "String", Valor = Filemanager.file_author });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "file_Descripcion", Tipo = "String", Valor = Filemanager.file_descripcion });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "file_name", Tipo = "String", Valor = Filemanager.file_name });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "versions_title", Tipo = "String", Valor = Filemanager.versions_title });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "versions_description", Tipo = "String", Valor = Filemanager.versions_description });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "versions_author", Tipo = "String", Valor = Filemanager.versions_author });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "versions_size", Tipo = "String", Valor = Filemanager.versions_size });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "minutes", Tipo = "String", Valor = Filemanager.minutes.ToString() });

        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "tipo_documento_id", Tipo = "String", Valor = Filemanager.file_tbl_tipo_documento_id != null ? Filemanager.file_tbl_tipo_documento_id : "NULL" });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "contrato_id", Tipo = "String", Valor = Filemanager.file_tbl_contrato_id != null ? Filemanager.file_tbl_contrato_id : "NULL" });
                

        //        List<ResponseFileManager> Lista = new List<ResponseFileManager>();

        //        #endregion

        //        #region ConexionBD
        //        using (Contexto conexion = new Contexto())
        //        {
        //            switch (int.Parse(Configuration["TipoBase"].ToString()))
        //            {
        //                case 2:
        //                    var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, _sp__documentary_information_add);
        //                    Lista = conexion.Query<ResponseFileManager>().FromSql<ResponseFileManager>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
        //                    break;
        //            }

        //        }
        //        #endregion
        //        return new ResponseGeneric<List<ResponseFileManager>>(Lista);

        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Consultar", ex);
        //        return new ResponseGeneric<List<ResponseFileManager>>(ex);
        //    }
        //}

        public ResponseGeneric<List<ResponseFileManager>> GetNewEntrega(AdministracionArchivos Filemanager, string entrega)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "id", Tipo = "String", Valor = Filemanager.id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "file_extension", Tipo = "String", Valor = Filemanager.file_extension });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "file_author", Tipo = "String", Valor = Filemanager.file_author });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "file_Descripcion", Tipo = "String", Valor = Filemanager.file_descripcion });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "file_name", Tipo = "String", Valor = Filemanager.file_name });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "versions_title", Tipo = "String", Valor = Filemanager.versions_title });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "versions_description", Tipo = "String", Valor = Filemanager.versions_description });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "versions_author", Tipo = "String", Valor = Filemanager.versions_author });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "versions_size", Tipo = "String", Valor = Filemanager.versions_size });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "minutes", Tipo = "String", Valor = Filemanager.minutes.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tipo_documento_id", Tipo = "String", Valor = Filemanager.file_tbl_tipo_documento_id != null ? Filemanager.file_tbl_tipo_documento_id : "NULL" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "contrato_id", Tipo = "String", Valor = Filemanager.file_tbl_contrato_id != null ? Filemanager.file_tbl_contrato_id : "NULL" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "entrega_id", Tipo = "String", Valor = entrega.ToString() != null ? entrega.ToString() : "NULL" });


                List<ResponseFileManager> Lista = new List<ResponseFileManager>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, _sp__documentary_information_add_entrega);
                            Lista = conexion.Query<ResponseFileManager>().FromSql<ResponseFileManager>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<ResponseFileManager>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<ResponseFileManager>>(ex);
            }
        }
        public ResponseGeneric<List<ResponseFileManager>> GetNewUbi(AdministracionArchivos Filemanager, string entrega, string ubicacion)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "id", Tipo = "String", Valor = Filemanager.id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "file_extension", Tipo = "String", Valor = Filemanager.file_extension });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "file_author", Tipo = "String", Valor = Filemanager.file_author });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "file_Descripcion", Tipo = "String", Valor = Filemanager.file_descripcion });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "file_name", Tipo = "String", Valor = Filemanager.file_name });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "versions_title", Tipo = "String", Valor = Filemanager.versions_title });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "versions_description", Tipo = "String", Valor = Filemanager.versions_description });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "versions_author", Tipo = "String", Valor = Filemanager.versions_author });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "versions_size", Tipo = "String", Valor = Filemanager.versions_size });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "minutes", Tipo = "String", Valor = Filemanager.minutes.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tipo_documento_id", Tipo = "String", Valor = Filemanager.file_tbl_tipo_documento_id != null ? Filemanager.file_tbl_tipo_documento_id : "NULL" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "contrato_id", Tipo = "String", Valor = Filemanager.file_tbl_contrato_id != null ? Filemanager.file_tbl_contrato_id : "NULL" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "entrega_id", Tipo = "String", Valor = entrega.ToString() != null ? entrega.ToString() : "NULL" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "ubicacion_id", Tipo = "String", Valor = ubicacion.ToString() != null ? ubicacion.ToString() : "NULL" });

                List<ResponseFileManager> Lista = new List<ResponseFileManager>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, _sp__documentary_information_add_entrega_ubicacion);
                            Lista = conexion.Query<ResponseFileManager>().FromSql<ResponseFileManager>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<ResponseFileManager>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<ResponseFileManager>>(ex);
            }
        }

        public ResponseGeneric<List<ResponseFileManager>> GetNewMonitoreo(AdministracionArchivos Filemanager, string entrega, string ubicacion, string monitoreo)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "id", Tipo = "String", Valor = Filemanager.id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "file_extension", Tipo = "String", Valor = Filemanager.file_extension });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "file_author", Tipo = "String", Valor = Filemanager.file_author });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "file_Descripcion", Tipo = "String", Valor = Filemanager.file_descripcion });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "file_name", Tipo = "String", Valor = Filemanager.file_name });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "versions_title", Tipo = "String", Valor = Filemanager.versions_title });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "versions_description", Tipo = "String", Valor = Filemanager.versions_description });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "versions_author", Tipo = "String", Valor = Filemanager.versions_author });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "versions_size", Tipo = "String", Valor = Filemanager.versions_size });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "minutes", Tipo = "String", Valor = Filemanager.minutes.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tipo_documento_id", Tipo = "String", Valor = Filemanager.file_tbl_tipo_documento_id != null ? Filemanager.file_tbl_tipo_documento_id : "NULL" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "contrato_id", Tipo = "String", Valor = Filemanager.file_tbl_contrato_id != null ? Filemanager.file_tbl_contrato_id : "NULL" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "entrega_id", Tipo = "String", Valor = entrega.ToString() != null ? entrega.ToString() : "NULL" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "ubicacion_id", Tipo = "String", Valor = ubicacion.ToString() != null ? ubicacion.ToString() : "NULL" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "monitoreo_id", Tipo = "String", Valor = monitoreo.ToString() != null ? monitoreo.ToString() : "NULL" });

                List<ResponseFileManager> Lista = new List<ResponseFileManager>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, _sp__documentary_information_add_entrega_monitoreo);
                            Lista = conexion.Query<ResponseFileManager>().FromSql<ResponseFileManager>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<ResponseFileManager>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<ResponseFileManager>>(ex);
            }
        }

        public ResponseGeneric<List<Token_confirmacion>> sp_insert_token(string token, string fileManagerId)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "id_archivo", Tipo = "String", Valor = fileManagerId.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "token_insert", Tipo = "String", Valor = token.ToString() });


                List<Token_confirmacion> Lista = new List<Token_confirmacion>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, _sp_insert_token_documentary_versions);
                            Lista = conexion.Query<Token_confirmacion>().FromSql<Token_confirmacion>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Token_confirmacion>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("", ex);
                return new ResponseGeneric<List<Token_confirmacion>>(ex);
            }
        }


        public ResponseGeneric<List<ResponseFileManager>> GetUri(String Token_, int expiration_, String tipo_documento_)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "token", Tipo = "String", Valor = Token_ });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "expiration", Tipo = "String", Valor = expiration_.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tipo_documento_", Tipo = "String", Valor = tipo_documento_ !=null? tipo_documento_:"" });


                List<ResponseFileManager> Lista = new List<ResponseFileManager>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, _sp__documentary_information_download);
                            Lista = conexion.Query<ResponseFileManager>().FromSql<ResponseFileManager>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<ResponseFileManager>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<ResponseFileManager>>(ex);
            }
        }

        public ResponseGeneric<List<ResponseFileManager>> DeleteUri(String id)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "id", Tipo = "String", Valor = id });
                List<ResponseFileManager> Lista = new List<ResponseFileManager>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, _sp__documentary_information_delete);
                            Lista = conexion.Query<ResponseFileManager>().FromSql<ResponseFileManager>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<ResponseFileManager>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return new ResponseGeneric<List<ResponseFileManager>>(ex);
            }
        }

        public ResponseGeneric<List<ResponseFileManager>> GetUriById(String id_document_, int expiration_)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "token", Tipo = "String", Valor = id_document_ });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "expiration", Tipo = "String", Valor = expiration_.ToString() });

                List<ResponseFileManager> Lista = new List<ResponseFileManager>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, _sp__documentary_information_download_by_id);
                            Lista = conexion.Query<ResponseFileManager>().FromSql<ResponseFileManager>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<ResponseFileManager>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<ResponseFileManager>>(ex);
            }
        }

        public ResponseGeneric<List<ResponseFileList>> GetByContract(String idContract_)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "contrato_id", Tipo = "String", Valor = idContract_ });

                List<ResponseFileList> Lista = new List<ResponseFileList>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, _sp_documentary_contract_list);
                            Lista = conexion.Query<ResponseFileList>().FromSql<ResponseFileList>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<ResponseFileList>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<ResponseFileList>>(ex);
            }
        }

        public ResponseGeneric<dynamic> DeleteFileContract(String idFile)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "id", Tipo = "String", Valor = idFile });

                List<ResponseFileList> Lista = new List<ResponseFileList>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, _sp_documentary_remove_file);
                            Lista = conexion.Query<ResponseFileList>().FromSql<ResponseFileList>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<dynamic>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return new ResponseGeneric<dynamic>(ex);
            }
        }


        public ResponseGeneric<List<ResponseFileManager>> Remove(String Token_)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "token", Tipo = "String", Valor = Token_ });
  



                List<ResponseFileManager> Lista = new List<ResponseFileManager>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, _sp__documentary_information_Remove);
                            Lista = conexion.Query<ResponseFileManager>().FromSql<ResponseFileManager>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<ResponseFileManager>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("remove", ex);
                return new ResponseGeneric<List<ResponseFileManager>>(ex);
            }
        }

        public ResponseGeneric<List<DocumentsFileManagerVersion>> documentary_information_GetListVersions(String id_uri)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "Token", Tipo = "String", Valor = id_uri.ToString() });



                List<DocumentsFileManagerVersion> Lista = new List<DocumentsFileManagerVersion>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, _sp__documentary_information_GetListVersions);
                            Lista = conexion.Query<DocumentsFileManagerVersion>().FromSql<DocumentsFileManagerVersion>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<DocumentsFileManagerVersion>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("document", ex);
                return new ResponseGeneric<List<DocumentsFileManagerVersion>>(ex);
            }
        }

        public ResponseGeneric<List<CrudresponseNum>> documentary_information_update_state(String Token, String description_, int tipo)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "Token", Tipo = "String", Valor = Token });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "description_", Tipo = "String", Valor = description_ });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tipo", Tipo = "String", Valor = tipo.ToString() });




                List<CrudresponseNum> Lista = new List<CrudresponseNum>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, _sp__documentary_information_update_state);
                            Lista = conexion.Query<CrudresponseNum>().FromSql<CrudresponseNum>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<CrudresponseNum>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("document", ex);
                return new ResponseGeneric<List<CrudresponseNum>>(ex);
            }
        }


        public ResponseGeneric<dynamic> deleted_file_archivo_monitoreo(string token)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "token", Tipo = "String", Valor = token.ToString() });

                List<Token_eliminar> Lista = new List<Token_eliminar>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_deleted_file_plan_entrega_archivo_global);
                            Lista = conexion.Query<Token_eliminar>().FromSql<Token_eliminar>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }
                }
                #endregion
                return new ResponseGeneric<dynamic>(true);

            }
            catch (Exception ex)
            {
                _logger.LogError("", ex);
                return new ResponseGeneric<dynamic>(ex);
            }
        }

    }
}
