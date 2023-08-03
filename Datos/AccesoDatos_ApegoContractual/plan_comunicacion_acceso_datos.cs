using Conexion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.IO;
using Utilidades.Log4Net;

namespace AccesoDatos
{
    public class plan_comunicacion_acceso_datos : crud_plancomunicacion
    {
        public BDParametros GeneracionParametros = new BDParametros();
        private readonly ILoggerManager _logger;
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string StoreProcedure_Mensaje = "sp_get_pcmensajecomunicacion_contrato";
        string StoreProcedure_Contratante = "sp_get_pccontratante_contrato";
        string StoreProcedure_Proveedor = "sp_get_pcproveedores_contrato";
        string StoreProcedure_Proveedor_id = "sp_get_pcproveedor";
        string StoreProcedure_Proveedor_add = "sp_plancomunicacion_proveedor";


        public plan_comunicacion_acceso_datos()
        {
            _logger = new LoggerManager();
        }


        public ResponseGeneric<List<tbl_pc_mensaje>> Consultar_MensajesComunicacion_contrato(String contrato)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "contrato", Tipo = "String", Valor = contrato.ToString() });

                List<tbl_pc_mensaje> Lista = new List<tbl_pc_mensaje>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_Mensaje);
                            Lista = conexion.Query<tbl_pc_mensaje>().FromSql<tbl_pc_mensaje>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_pc_mensaje>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_pc_mensaje>>(ex);
            }
        }


        public ResponseGeneric<List<tbl_pc_contratante>> Consultar_Contratante_contrato(String contrato)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "contrato", Tipo = "String", Valor = contrato.ToString() });

                List<tbl_pc_contratante> Lista = new List<tbl_pc_contratante>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_Contratante);
                            Lista = conexion.Query<tbl_pc_contratante>().FromSql<tbl_pc_contratante>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_pc_contratante>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_pc_contratante>>(ex);
            }
        }


        public ResponseGeneric<tbl_pc_proveedor> Consultar_Proveedor(String idProveedor)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "proveedor", Tipo = "String", Valor = idProveedor.ToString() });

                tbl_pc_proveedor Lista = new tbl_pc_proveedor();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_Proveedor_id);
                            Lista = conexion.Query<tbl_pc_proveedor>().FromSql<tbl_pc_proveedor>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<tbl_pc_proveedor>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<tbl_pc_proveedor>(ex);
            }
        }


        public ResponseGeneric<List<tbl_pc_proveedor>> Consultar_Proveedor_contrato(String contrato)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "contrato", Tipo = "String", Valor = contrato.ToString() });

                List<tbl_pc_proveedor> Lista = new List<tbl_pc_proveedor>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_Proveedor);
                            Lista = conexion.Query<tbl_pc_proveedor>().FromSql<tbl_pc_proveedor>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_pc_proveedor>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_pc_proveedor>>(ex);
            }
        }



        public ResponseGeneric<List<Crudresponse>> addProveedor(tbl_pc_proveedor_add proveedor)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "Int", Valor = proveedor.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = proveedor.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_proveedor_id", Tipo = "String", Valor = proveedor.p_tbl_proveedor_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_id", Tipo = "String", Valor = proveedor.p_tbl_contrato_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_acuerdo_id", Tipo = "String", Valor = proveedor.p_tbl_tipo_audiencia_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_inclusion", Tipo = "String", Valor = proveedor.p_inclusion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_activo", Tipo = "String", Valor = proveedor.p_activo.ToString() });

                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_Proveedor_add);
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


        public ResponseGeneric<List<Crudresponse>> deleteProveedor(tbl_pc_proveedor_add proveedor)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "Int", Valor = NullToString(proveedor.p_opt) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = NullToString(proveedor.p_id) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_proveedor_id", Tipo = "String", Valor = NullToString(proveedor.p_tbl_proveedor_id) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_id", Tipo = "String", Valor = NullToString(proveedor.p_tbl_contrato_id) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_acuerdo_id", Tipo = "String", Valor = NullToString(proveedor.p_tbl_tipo_audiencia_id) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_inclusion", Tipo = "String", Valor = NullToString(proveedor.p_inclusion) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_activo", Tipo = "String", Valor = NullToString(proveedor.p_activo) });

                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_Proveedor_add);
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

        //public ResponseGeneric<List<Crudresponse>> update(tbl_acuerdo_add acuerdo)
        //{
        //    try
        //    {
        //        #region Parametros
        //        List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();

        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "Int", Valor = acuerdo.p_opt.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = acuerdo.p_id.ToString() });
        //        //ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_id", Tipo = "String", Valor = acuerdo.p_tbl_contrato_id.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_servidor_resp_id", Tipo = "String", Valor = acuerdo.p_tbl_contrato_servidor_resp_id.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_acuerdo_id", Tipo = "String", Valor = acuerdo.p_tbl_tipo_acuerdo_id.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_acuerdo", Tipo = "String", Valor = acuerdo.p_acuerdo.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_registro", Tipo = "String", Valor = acuerdo.p_fecha_registro.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_compromiso", Tipo = "String", Valor = acuerdo.p_fecha_compromiso.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_cierre", Tipo = "String", Valor = acuerdo.p_fecha_cierre.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_estatus_acuerdo", Tipo = "String", Valor = acuerdo.p_estatus_acuerdo.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_comentario", Tipo = "String", Valor = acuerdo.p_comentario.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_estatus", Tipo = "String", Valor = acuerdo.p_estatus.ToString() });

        //        List<Crudresponse> Lista = new List<Crudresponse>();

        //        #endregion

        //        #region ConexionBD
        //        using (Contexto conexion = new Contexto())
        //        {
        //            switch (int.Parse(Configuration["TipoBase"].ToString()))
        //            {
        //                case 2:
        //                    var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure);
        //                    Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
        //                    break;
        //            }

        //        }
        //        #endregion
        //        return new ResponseGeneric<List<Crudresponse>>(Lista);

        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResponseGeneric<List<Crudresponse>>(ex);
        //    }
        //}

        //public ResponseGeneric<List<Crudresponse>> delete(tbl_acuerdo_add acuerdo)
        //{
        //    try
        //    {
        //        #region Parametros
        //        List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();

        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "Int", Valor = NullToString(acuerdo.p_opt)  });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = NullToString(acuerdo.p_id) });
        //        //ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_id", Tipo = "String", Valor = NullToString(acuerdo.p_tbl_contrato_id) });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_servidor_resp_id", Tipo = "String", Valor = NullToString(acuerdo.p_tbl_contrato_servidor_resp_id) });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_acuerdo_id", Tipo = "String", Valor = NullToString(acuerdo.p_tbl_tipo_acuerdo_id) });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_acuerdo", Tipo = "String", Valor = NullToString(acuerdo.p_acuerdo) });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_registro", Tipo = "String", Valor = NullToString(acuerdo.p_fecha_registro) });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_compromiso", Tipo = "String", Valor = NullToString(acuerdo.p_fecha_compromiso) });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_cierre", Tipo = "String", Valor = NullToString(acuerdo.p_fecha_cierre) });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_estatus_acuerdo", Tipo = "String", Valor = NullToString(acuerdo.p_estatus_acuerdo) });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_comentario", Tipo = "String", Valor = NullToString(acuerdo.p_comentario) });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_estatus", Tipo = "String", Valor = NullToString(acuerdo.p_estatus) });

        //        List<Crudresponse> Lista = new List<Crudresponse>();

        //        #endregion

        //        #region ConexionBD
        //        using (Contexto conexion = new Contexto())
        //        {
        //            switch (int.Parse(Configuration["TipoBase"].ToString()))
        //            {
        //                case 2:
        //                    var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure);
        //                    Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
        //                    break;
        //            }

        //        }
        //        #endregion
        //        return new ResponseGeneric<List<Crudresponse>>(Lista);

        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResponseGeneric<List<Crudresponse>>(ex);
        //    }




        //}

        //public ResponseGeneric<List<DropDownList>> ConsultarTipos()
        //{
        //    try
        //    {
        //        #region Parametros
        //        List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
        //        //ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_contrato_id", Tipo = "String", Valor = contrato.ToString() });

        //        List<DropDownList> Lista = new List<DropDownList>();

        //        #endregion

        //        #region ConexionBD
        //        using (Contexto conexion = new Contexto())
        //        {
        //            switch (int.Parse(Configuration["TipoBase"].ToString()))
        //            {
        //                case 2:
        //                    var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedureTipos);
        //                    Lista = conexion.Query<DropDownList>().FromSql<DropDownList>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
        //                    break;
        //            }

        //        }
        //        #endregion
        //        return new ResponseGeneric<List<DropDownList>>(Lista);

        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResponseGeneric<List<DropDownList>>(ex);
        //    }
        //}

        public static string NullToString(object Value)
        {

            // Value.ToString() allows for Value being DBNull, but will also convert int, double, etc.
            return Value == null ? "" : Value.ToString();



        }
    }
}
