using Conexion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Contrato;
using Modelos.Modelos.Area;
using Modelos.Modelos.ServidoresPublicos;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.IO;
using Utilidades.Log4Net;
using Modelos.Modelos.VerificacionContrato;

namespace AccesoDatos_AdministracionDeContratos
{
    public class sp_cfdi_contrato_datos
    {
        public BDParametros GeneracionParametros = new BDParametros();
        private readonly ILoggerManager _logger;
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string sp_config_contrato = "sp_config_contrato";
        string StoreProcedure = "sp_get_tiposcontrato";
        string sp_get_partidas_montos_area = "sp_get_partidas_montos_area";
        string sp_get_servpubdependencia = "sp_get_servpubdependencia";
        string sp_getproveedores_dependencia = "sp_getproveedores_dependencia";
        string sp_get_adicionalescontrato = "sp_get_adicionalescontrato"; 
        string sp_get_contratoporId = "sp_get_contratoporId";
        string sp_responsables_contrato = "sp_responsables_contrato";
        string sp_add_contrato_proveedor = "sp_add_contrato_proveedor";
        string sp_get_proveedores_por_contrato = "sp_get_proveedores_por_contrato";
        string sp_comprometer_presupuesto_area = "sp_comprometer_presupuesto_area"; 
        string sp_json_contrato = "sp_json_contrato"; 
        string sp_gte_json_contrato = "sp_gte_json_contrato";
        string sp_get_contrProveedor = "sp_get_contrProveedor";
        string sp_get_responsable_contrato = "sp_get_responsable_contrato";
        string sp_upd_token_contrato = "sp_upd_token_contrato";
        string sp_get_contrato_tipocontrato = "sp_get_contrato_tipocontrato";

        string sp_asociar_estructura_contrato = "sp_asociar_estructura_contrato";

        public sp_cfdi_contrato_datos()
        {
            _logger = new LoggerManager();
        }

        public ResponseGeneric<List<Crudresponse>> config(sp_config_contrato contrato)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt ", Tipo = "String", Valor = contrato.p_opt == null ? "" : contrato.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id ", Tipo = "String", Valor = contrato.p_id == null ? "" : contrato.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_contrato_id ", Tipo = "String", Valor = contrato.p_tbl_tipo_contrato_id == null ? "" : contrato.p_tbl_tipo_contrato_id.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_prioridad_id ", Tipo = "String", Valor = contrato.p_tbl_prioridad_id == null ? "" : contrato.p_tbl_prioridad_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_estatus_contrato_id ", Tipo = "String", Valor = contrato.p_tbl_estatus_contrato_id == null ? "" : contrato.p_tbl_estatus_contrato_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_proyecto_id ", Tipo = "String", Valor = contrato.p_tbl_proyecto_id == null ? "" : contrato.p_tbl_proyecto_id.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_procedimiento_id ", Tipo = "String", Valor = contrato.p_tbl_procedimiento_id == null ? "" : contrato.p_tbl_procedimiento_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_numero ", Tipo = "String", Valor = contrato.p_numero == null ? "" : contrato.p_numero.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_objeto ", Tipo = "String", Valor = contrato.p_objeto == null ? "" : contrato.p_objeto.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_firma  ", Tipo = "String", Valor = (contrato.p_fecha_firma == null || contrato.p_fecha_firma.ToString().Contains("01/01/0001")) ? "1800-01-01 00:00:00" : contrato.p_fecha_firma.ToString("yyyy-MM-dd") });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_Iinicio ", Tipo = "String", Valor = (contrato.p_fecha_Iinicio == null || contrato.p_fecha_Iinicio.ToString().Contains("01/01/0001")) ? "1800-01-01 00:00:00" : contrato.p_fecha_Iinicio.ToString("yyyy-MM-dd") });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_fin  ", Tipo = "String", Valor = (contrato.p_fecha_fin == null || contrato.p_fecha_fin.ToString().Contains("01/01/0001")) ? "1800-01-01 00:00:00" : contrato.p_fecha_fin.ToString("yyyy-MM-dd") });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_formalizacion ", Tipo = "String", Valor = (contrato.p_fecha_formalizacion == null || contrato.p_fecha_formalizacion.ToString().Contains("01/01/0001")) ? "1800-01-01 00:00:00" : contrato.p_fecha_formalizacion.ToString("yyyy-MM-dd") });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_ampliacion  ", Tipo = "String", Valor = contrato.p_ampliacion == 0 ? "0" : contrato.p_ampliacion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_requiere_renovacion  ", Tipo = "String", Valor = contrato.p_requiere_renovacion == 0 ? "0" : contrato.p_requiere_renovacion.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_satisfactorio  ", Tipo = "String", Valor = contrato.p_satisfactorio == 0 ? "0" : contrato.p_satisfactorio.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_porc_max_penalizacion  ", Tipo = "String", Valor = contrato.p_porc_max_penalizacion == 0 ? "0" : contrato.p_porc_max_penalizacion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_porc_max_deductivas  ", Tipo = "String", Valor = contrato.p_porc_max_deductivas == 0 ? "0" : contrato.p_porc_max_deductivas.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_presento_garantia  ", Tipo = "String", Valor = contrato.p_presento_garantia == 0 ? "0" : contrato.p_presento_garantia.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_porc_garantia  ", Tipo = "String", Valor = contrato.p_porc_garantia == 0 ? "0" : contrato.p_porc_garantia.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_garantia ", Tipo = "String", Valor = contrato.p_monto_garantia == 0 ? "0" : contrato.p_monto_garantia.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_es_administradora ", Tipo = "String", Valor = contrato.p_es_administradora == 0 ? "0" : contrato.p_es_administradora.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_activo  ", Tipo = "String", Valor = contrato.p_activo == 0 ? "0" : contrato.p_activo.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_token  ", Tipo = "String", Valor = contrato.p_token == null ? "" : contrato.p_token.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_nombre  ", Tipo = "String", Valor = contrato.p_nombre == null ? "" : contrato.p_nombre.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_max_sin_iva  ", Tipo = "String", Valor = contrato.p_monto_max_sin_iva < 0 ? "0" : contrato.p_monto_max_sin_iva.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_min_sin_iva  ", Tipo = "String", Valor = contrato.p_monto_min_sin_iva < 0 ? "0" : contrato.p_monto_min_sin_iva.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_registro", Tipo = "String", Valor = (contrato.p_fecha_registro == null || contrato.p_fecha_registro.ToString().Contains("01/01/0001")) ? "1800-01-01 00:00:00" : contrato.p_fecha_registro.ToString("yyyy-MM-dd") });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_necesidad", Tipo = "String", Valor = contrato.p_necesidad == null ? "" : contrato.p_necesidad.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_dependencia_id ", Tipo = "String", Valor = contrato.p_tbl_dependencia_id == null ? "" : contrato.p_tbl_dependencia_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_estructura_asignado_id ", Tipo = "String", Valor = contrato.p_estructura_asignado_id == null ? "" : contrato.p_estructura_asignado_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tipo_estructura ", Tipo = "Int", Valor = contrato.p_tipo_estructura.ToString() });


                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_config_contrato);
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


        public ResponseGeneric<List<Crudresponse>> operacion_cfdi_contrato(DatosXmlRequest datos)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt ", Tipo = "String", Valor = datos.opt == null ? 0 : datos.opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id ", Tipo = "String", Valor = datos.id == null ? "" : datos.id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_id ", Tipo = "String", Valor = datos.tbl_contrato_id == null ? "" : datos.tbl_contrato_id.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_folio ", Tipo = "String", Valor = datos.folio == null ? "" : datos.folio.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_serie ", Tipo = "String", Valor = datos.serie == null ? "" : datos.serie.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha ", Tipo = "String", Valor = datos.fecha == null ? "" : datos.fecha.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_formaDePago ", Tipo = "String", Valor = datos.formaDePago == null ? "" : datos.formaDePago.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_total ", Tipo = "String", Valor = datos.total == 0 ? "0" : datos.total.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_subTotal ", Tipo = "String", Valor = datos.subTotal == 0 ? "0" : datos.subTotal.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_moneda ", Tipo = "String", Valor = datos.moneda == null ? "" : datos.moneda.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_condicionesDePago  ", Tipo = "String", Valor = datos.condicionesDePago == null ? "" : datos.condicionesDePago.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_metodoDePago ", Tipo = "String", Valor = datos.metodoDePago == null ? "" : datos.metodoDePago.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tipoDeComprobante  ", Tipo = "String", Valor = datos.tipoDeComprobante == null ? "" : datos.tipoDeComprobante.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_lugarExpedicion ", Tipo = "String", Valor = datos.LugarExpedicion == null ? "" : datos.LugarExpedicion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_rfcEmisor  ", Tipo = "String", Valor = datos.rfcEmisor == null ? "" : datos.rfcEmisor.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_nombreEmisor  ", Tipo = "String", Valor = datos.rfcEmisor == null ? "" : datos.rfcEmisor.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_calleEmisor  ", Tipo = "String", Valor = datos.calleEmisor == null ? "" : datos.calleEmisor.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_noInteriorEmisor  ", Tipo = "String", Valor = datos.noInteriorEmisor == null ? "" : datos.noInteriorEmisor.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_noExteriorEmisor  ", Tipo = "String", Valor = datos.noExteriorEmisor == null ? "" : datos.noExteriorEmisor.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_coloniaEmisor  ", Tipo = "String", Valor = datos.coloniaEmisor == null ? "" : datos.coloniaEmisor.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_municipioEmisor  ", Tipo = "String", Valor = datos.municipioEmisor == null ? "" : datos.municipioEmisor.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_estadoEmisor ", Tipo = "String", Valor = datos.estadoEmisor == null ? "" : datos.estadoEmisor.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_codigoPostalEmisor ", Tipo = "String", Valor = datos.codigoPostalEmisor == null ? "" : datos.codigoPostalEmisor.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_regimenEmisor ", Tipo = "String", Valor = datos.RegimenEmisor == null ? "" : datos.RegimenEmisor.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_rfcReceptor ", Tipo = "String", Valor = datos.rfcReceptor == null ? "" : datos.rfcReceptor.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_nombreReceptor ", Tipo = "String", Valor = datos.nombreReceptor == null ? "" : datos.nombreReceptor.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_calleReceptor ", Tipo = "String", Valor = datos.calleReceptor == null ? "" : datos.calleReceptor.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_noInteriorReceptor ", Tipo = "String", Valor = datos.noInteriorReceptor == null ? "" : datos.noInteriorReceptor.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_noExteriorReceptor ", Tipo = "String", Valor = datos.noExteriorReceptor == null ? "" : datos.noExteriorReceptor.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_coloniaReceptor ", Tipo = "String", Valor = datos.coloniaReceptor == null ? "" : datos.coloniaReceptor.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_municipioReceptor ", Tipo = "String", Valor = datos.municipioReceptor == null ? "" : datos.municipioReceptor.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_estadoReceptor ", Tipo = "String", Valor = datos.estadoReceptor == null ? "" : datos.estadoReceptor.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_paisReceptor ", Tipo = "String", Valor = datos.paisReceptor == null ? "" : datos.paisReceptor.ToString() });


                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_codigoPostalReceptor ", Valor = datos.codigoPostalReceptor == null ? "" : datos.codigoPostalReceptor.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_conceptos ", Tipo = "String", Valor = datos.conceptos_cadena == null ? "" : datos.conceptos_cadena.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_traslados ", Tipo = "String", Valor = datos.traslados_cadena == null ? "" : datos.traslados_cadena.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_retenciones ", Tipo = "String", Valor = datos.retenciones_cadena == null ? "" : datos.retenciones_cadena.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_cadenaXML ", Tipo = "String", Valor = datos.rfcEmisor == null ? "" : datos.rfcEmisor.ToString() });


                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_config_contrato);
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



        //public ResponseGeneric<List<tbl_tipo_contrato>> ConsultartipoContrato()
        //{
        //    try
        //    {
        //        #region Parametros
        //        List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
        //        //ListaEnvioParam.Add(new EntidadParametro { Nombre = "id", Tipo = "Int", Valor = entidad.ToString() });

        //        List<tbl_tipo_contrato> Lista = new List<tbl_tipo_contrato>();

        //        #endregion

        //        #region ConexionBD
        //        using (Contexto conexion = new Contexto())
        //        {
        //            switch (int.Parse(Configuration["TipoBase"].ToString()))
        //            {
        //                case 2:
        //                    var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure);
        //                    Lista = conexion.Query<tbl_tipo_contrato>().FromSql<tbl_tipo_contrato>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
        //                    break;
        //            }

        //        }
        //        #endregion
        //        return new ResponseGeneric<List<tbl_tipo_contrato>>(Lista);

        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Consultar", ex);
        //        return new ResponseGeneric<List<tbl_tipo_contrato>>(ex);
        //    }
        //}

        //public ResponseGeneric<List<ContratoPresupuesto>> get_partidas_montos_area(Guid iddep)
        //{
        //    try
        //    {
        //        #region Parametros
        //        List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_dependencia_id", Tipo = "String", Valor = iddep.ToString() });

        //        List<ContratoPresupuesto> Lista = new List<ContratoPresupuesto>();

        //        #endregion

        //        #region ConexionBD
        //        using (Contexto conexion = new Contexto())
        //        {
        //            switch (int.Parse(Configuration["TipoBase"].ToString()))
        //            {
        //                case 2:
        //                    var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_partidas_montos_area);
        //                    Lista = conexion.Query<ContratoPresupuesto>().FromSql<ContratoPresupuesto>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
        //                    break;
        //            }

        //        }
        //        #endregion
        //        return new ResponseGeneric<List<ContratoPresupuesto>>(Lista);

        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Consultar", ex);
        //        return new ResponseGeneric<List<ContratoPresupuesto>>(ex);
        //    }
        //}

        //public ResponseGeneric<List<tbl_servidor_publico>> ConsultarServPub(Guid dependencia)
        //{
        //    try
        //    {
        //        #region Parametros
        //        List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "servidor_dependencia", Tipo = "String", Valor = dependencia.ToString() });

        //        List<tbl_servidor_publico> Lista = new List<tbl_servidor_publico>();

        //        #endregion

        //        #region ConexionBD
        //        using (Contexto conexion = new Contexto())
        //        {
        //            switch (int.Parse(Configuration["TipoBase"].ToString()))
        //            {
        //                case 2:
        //                    var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_servpubdependencia);
        //                    Lista = conexion.Query<tbl_servidor_publico>().FromSql<tbl_servidor_publico>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
        //                    break;
        //            }

        //        }
        //        #endregion
        //        return new ResponseGeneric<List<tbl_servidor_publico>>(Lista);

        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Consultar", ex);
        //        return new ResponseGeneric<List<tbl_servidor_publico>>(ex);
        //    }
        //}

        //public ResponseGeneric<List<tbl_proveedor>> proveedoresdep(Guid idproveedor)
        //{
        //    try
        //    {
        //        #region Parametros
        //        List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "idproveedor", Tipo = "String", Valor = idproveedor.ToString() });
        //        List<tbl_proveedor> Lista = new List<tbl_proveedor>();
        //        #endregion

        //        #region ConexionBD
        //        using (Contexto conexion = new Contexto())
        //        {
        //            switch (int.Parse(Configuration["TipoBase"].ToString()))
        //            {
        //                case 2:
        //                    var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_getproveedores_dependencia);
        //                    Lista = conexion.Query<tbl_proveedor>().FromSql<tbl_proveedor>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
        //                    break;
        //            }

        //        }
        //        #endregion
        //        return new ResponseGeneric<List<tbl_proveedor>>(Lista);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Consultar", ex);
        //        return new ResponseGeneric<List<tbl_proveedor>>(ex);
        //    }
        //}

        //public ResponseGeneric<List<DropDownList>> get_adicionalescontrato(String opt)
        //{
        //    try
        //    {
        //        #region Parametros
        //        List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "opt ", Tipo = "String", Valor = opt });
        //        List<DropDownList> Lista = new List<DropDownList>();
        //        #endregion

        //        #region ConexionBD
        //        using (Contexto conexion = new Contexto())
        //        {
        //            switch (int.Parse(Configuration["TipoBase"].ToString()))
        //            {
        //                case 2:
        //                    var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_adicionalescontrato);
        //                    Lista = conexion.Query<DropDownList>().FromSql<DropDownList>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
        //                    break;
        //            }

        //        }
        //        #endregion
        //        return new ResponseGeneric<List<DropDownList>>(Lista);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Consultar", ex);
        //        return new ResponseGeneric<List<DropDownList>>(ex);
        //    }
        //}

        //public ResponseGeneric<List<sp_config_contrato_>> get_contratoporId(Guid idcontrato)
        //{
        //    try
        //    {
        //        #region Parametros
        //        List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "idcontrato ", Tipo = "String", Valor = idcontrato.ToString() });
        //        List<sp_config_contrato_> Lista = new List<sp_config_contrato_>();
        //        #endregion

        //        #region ConexionBD
        //        using (Contexto conexion = new Contexto())
        //        {
        //            switch (int.Parse(Configuration["TipoBase"].ToString()))
        //            {
        //                case 2:
        //                    var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_contratoporId);
        //                    Lista = conexion.Query<sp_config_contrato_>().FromSql<sp_config_contrato_>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
        //                    break;
        //            }

        //        }
        //        #endregion
        //        return new ResponseGeneric<List<sp_config_contrato_>>(Lista);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Consultar", ex);
        //        return new ResponseGeneric<List<sp_config_contrato_>>(ex);
        //    }
        //}

        //public ResponseGeneric<List<Crudresponse>> responsables_contrato(Guid p_tbl_servidor_publico_id, Guid p_tbl_contrato_id, int p_responsable, String opt)
        //{
        //    try
        //    {
        //        #region Parametros
        //        List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_servidor_publico_id ", Tipo = "String", Valor = p_tbl_servidor_publico_id.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_id ", Tipo = "String", Valor = p_tbl_contrato_id.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_responsable ", Tipo = "String", Valor = p_responsable.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "opt ", Tipo = "String", Valor = opt });


        //        List<Crudresponse> Lista = new List<Crudresponse>();
        //        #endregion

        //        #region ConexionBD
        //        using (Contexto conexion = new Contexto())
        //        {
        //            switch (int.Parse(Configuration["TipoBase"].ToString()))
        //            {
        //                case 2:
        //                    var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_responsables_contrato);
        //                    Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
        //                    break;
        //            }

        //        }
        //        #endregion
        //        return new ResponseGeneric<List<Crudresponse>>(Lista);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Consultar", ex);
        //        return new ResponseGeneric<List<Crudresponse>>(ex);
        //    }
        //}

        //public ResponseGeneric<List<Crudresponse>> add_contrato_proveedor(Guid p_tbl_proveedor_id, Guid p_tbl_contrato_id,String opt)
        //{
        //    try
        //    {
        //        #region Parametros
        //        List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_id ", Tipo = "String", Valor = p_tbl_contrato_id.ToString() });

        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_proveedor_id ", Tipo = "String", Valor = p_tbl_proveedor_id.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "opt ", Tipo = "String", Valor = opt });



        //        List<Crudresponse> Lista = new List<Crudresponse>();
        //        #endregion

        //        #region ConexionBD
        //        using (Contexto conexion = new Contexto())
        //        {
        //            switch (int.Parse(Configuration["TipoBase"].ToString()))
        //            {
        //                case 2:
        //                    var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_add_contrato_proveedor);
        //                    Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
        //                    break;
        //            }

        //        }
        //        #endregion
        //        return new ResponseGeneric<List<Crudresponse>>(Lista);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("add", ex);
        //        return new ResponseGeneric<List<Crudresponse>>(ex);
        //    }
        //}

        //public ResponseGeneric<List<DropDownList>> get_proveedores_por_contrato(Guid idcontrato)
        //{
        //    try
        //    {
        //        #region Parametros
        //        List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "idcontrato ", Tipo = "String", Valor = idcontrato.ToString() });
        //        List<DropDownList> Lista = new List<DropDownList>();
        //        #endregion
        //        #region ConexionBD
        //        using (Contexto conexion = new Contexto())
        //        {
        //            switch (int.Parse(Configuration["TipoBase"].ToString()))
        //            {
        //                case 2:
        //                    var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_proveedores_por_contrato);
        //                    Lista = conexion.Query<DropDownList>().FromSql<DropDownList>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
        //                    break;
        //            }
        //        }
        //        #endregion
        //        return new ResponseGeneric<List<DropDownList>>(Lista);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Consultar", ex);
        //        return new ResponseGeneric<List<DropDownList>>(ex);
        //    }
        //}

        //public ResponseGeneric<List<Crudresponse>> comprometer_presupuesto_area(comprometer_presupuesto_area_input obj)
        //{
        //    try
        //    {
        //        #region Parametros
        //        List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();

        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt ", Tipo = "String", Valor = obj.p_opt.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_id ", Tipo = "String", Valor = obj.p_tbl_contrato_id.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_dependencia_id ", Tipo = "String", Valor = obj.p_tbl_dependencia_id.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_capitulo_gasto_id ", Tipo = "String", Valor = obj.p_tbl_capitulo_gasto_id.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_ejercicio_id ", Tipo = "String", Valor = obj.p_tbl_ejercicio_id.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_area_id ", Tipo = "String", Valor = obj.p_tbl_area_id.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_a_comprometer ", Tipo = "String", Valor = obj.p_monto_a_comprometer.ToString() });

        //        List<Crudresponse> Lista = new List<Crudresponse>();
        //        #endregion
        //        #region ConexionBD
        //        using (Contexto conexion = new Contexto())
        //        {
        //            switch (int.Parse(Configuration["TipoBase"].ToString()))
        //            {
        //                case 2:
        //                    var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_comprometer_presupuesto_area);
        //                    Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
        //                    break;
        //            }
        //        }
        //        #endregion
        //        return new ResponseGeneric<List<Crudresponse>>(Lista);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Consultar", ex);
        //        return new ResponseGeneric<List<Crudresponse>>(ex);
        //    }
        //}

        //public ResponseGeneric<List<tbl_contrato_list>> get_lista_contratos(string iddep)
        //{
        //    try
        //    {
        //        #region Parametros
        //        List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "dependencia", Tipo = "String", Valor = iddep.ToString() });

        //        List<tbl_contrato_list> Lista = new List<tbl_contrato_list>();

        //        #endregion

        //        #region ConexionBD
        //        using (Contexto conexion = new Contexto())
        //        {
        //            switch (int.Parse(Configuration["TipoBase"].ToString()))
        //            {
        //                case 2:
        //                    var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_contrato_tipocontrato);
        //                    Lista = conexion.Query<tbl_contrato_list>().FromSql<tbl_contrato_list>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
        //                    break;
        //            }

        //        }
        //        #endregion
        //        return new ResponseGeneric<List<tbl_contrato_list>>(Lista);

        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Consultar", ex);
        //        return new ResponseGeneric<List<tbl_contrato_list>>(ex);
        //    }
        //}

        //public ResponseGeneric<List<CrudresponseNum>> get_sp_json_contrato(Guid p_tbl_contrato_id, String p_json_)
        //{
        //    try
        //    {
        //        #region Parametros
        //        List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_id", Tipo = "String", Valor = p_tbl_contrato_id.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_json_", Tipo = "String", Valor = p_json_ });



        //        List<CrudresponseNum> Lista = new List<CrudresponseNum>();

        //        #endregion

        //        #region ConexionBD
        //        using (Contexto conexion = new Contexto())
        //        {
        //            switch (int.Parse(Configuration["TipoBase"].ToString()))
        //            {
        //                case 2:
        //                    var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_json_contrato);
        //                    Lista = conexion.Query<CrudresponseNum>().FromSql<CrudresponseNum>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
        //                    break;
        //            }
        //        }
        //        #endregion
        //        return new ResponseGeneric<List<CrudresponseNum>>(Lista);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Consultar", ex);
        //        return new ResponseGeneric<List<CrudresponseNum>>(ex);
        //    }
        //}

        //public ResponseGeneric<List<CrudresponseNum>> sp_get_json_contrato(Guid p_tbl_contrato_id)
        //{
        //    try
        //    {
        //        #region Parametros
        //        List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_id", Tipo = "String", Valor = p_tbl_contrato_id.ToString() });




        //        List<CrudresponseNum> Lista = new List<CrudresponseNum>();

        //        #endregion

        //        #region ConexionBD
        //        using (Contexto conexion = new Contexto())
        //        {
        //            switch (int.Parse(Configuration["TipoBase"].ToString()))
        //            {
        //                case 2:
        //                    var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_gte_json_contrato);
        //                    Lista = conexion.Query<CrudresponseNum>().FromSql<CrudresponseNum>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
        //                    break;
        //            }
        //        }
        //        #endregion
        //        return new ResponseGeneric<List<CrudresponseNum>>(Lista);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Consultar", ex);
        //        return new ResponseGeneric<List<CrudresponseNum>>(ex);
        //    }
        //}


        //public ResponseGeneric<List<CrudresponseNum>> get_contrProveedor(Guid p_tbl_contrato_id)
        //{
        //    try
        //    {
        //        #region Parametros
        //        List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_id", Tipo = "String", Valor = p_tbl_contrato_id.ToString() });




        //        List<CrudresponseNum> Lista = new List<CrudresponseNum>();

        //        #endregion

        //        #region ConexionBD
        //        using (Contexto conexion = new Contexto())
        //        {
        //            switch (int.Parse(Configuration["TipoBase"].ToString()))
        //            {
        //                case 2:
        //                    var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_contrProveedor);
        //                    Lista = conexion.Query<CrudresponseNum>().FromSql<CrudresponseNum>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
        //                    break;
        //            }
        //        }
        //        #endregion
        //        return new ResponseGeneric<List<CrudresponseNum>>(Lista);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Consultar", ex);
        //        return new ResponseGeneric<List<CrudresponseNum>>(ex);
        //    }
        //}

        //public ResponseGeneric<List<Crudresponse>> sp_get_responsable_contrato_(Guid p_tbl_contrato_id)
        //{
        //    try
        //    {
        //        #region Parametros
        //        List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_id", Tipo = "String", Valor = p_tbl_contrato_id.ToString() });




        //        List<Crudresponse> Lista = new List<Crudresponse>();

        //        #endregion

        //        #region ConexionBD
        //        using (Contexto conexion = new Contexto())
        //        {
        //            switch (int.Parse(Configuration["TipoBase"].ToString()))
        //            {
        //                case 2:
        //                    var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_responsable_contrato);
        //                    Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
        //                    break;
        //            }
        //        }
        //        #endregion
        //        return new ResponseGeneric<List<Crudresponse>>(Lista);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Consultar", ex);
        //        return new ResponseGeneric<List<Crudresponse>>(ex);
        //    }
        //}

        //public ResponseGeneric<List<CrudresponseNum>> upd_token_contrato(Guid p_id, String p_token)
        //{
        //    try
        //    {
        //        #region Parametros
        //        List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_token", Tipo = "String", Valor = p_token });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = p_id.ToString() });




        //        List<CrudresponseNum> Lista = new List<CrudresponseNum>();

        //        #endregion

        //        #region ConexionBD
        //        using (Contexto conexion = new Contexto())
        //        {
        //            switch (int.Parse(Configuration["TipoBase"].ToString()))
        //            {
        //                case 2:
        //                    var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_upd_token_contrato);
        //                    Lista = conexion.Query<CrudresponseNum>().FromSql<CrudresponseNum>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
        //                    break;
        //            }
        //        }
        //        #endregion
        //        return new ResponseGeneric<List<CrudresponseNum>>(Lista);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("upd", ex);
        //        return new ResponseGeneric<List<CrudresponseNum>>(ex);
        //    }
        //}



    }
}
