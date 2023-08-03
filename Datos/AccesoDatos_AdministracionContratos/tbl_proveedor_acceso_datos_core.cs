using Conexion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Contrato;
using Modelos.Modelos.Dependencia;
using Modelos.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Utilidades.Log4Net;

namespace AccesoDatos_AdminContratos
{
    public class tbl_proveedor_acceso_datos_core
    {
        public BDParametros GeneracionParametros = new BDParametros();
        private readonly ILoggerManager _logger;
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string sp_proveedor = "sp_proveedor";
        string sp_get_proveedor_instancia = "sp_get_proveedor_instancia";
        string sp_get_lista_contratos_proveedor = "sp_get_lista_contratos_proveedor";
        string sp_get_tbl_proveedor_by_id = "sp_get_tbl_proveedor_by_id";
        string sp_get_tipo_interlocutor = "sp_get_tipo_interlocutor";

        string sp_get_info_comercial_interlocutor = "sp_get_info_comercial_interlocutor";
        string sp_get_proveedor_dependencia = "sp_get_proveedor_dependencia";
        public tbl_proveedor_acceso_datos_core()
        {
            _logger = new LoggerManager();
        }
        public ResponseGeneric<List<Crudresponse>> Add(tbl_proveedor_add proveedor)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = proveedor.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = proveedor.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_dependencia_id", Tipo = "String", Valor = proveedor.p_tbl_dependencia_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_numero", Tipo = "String", Valor = proveedor.p_numero.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_razon_social", Tipo = "String", Valor = proveedor.p_razon_social.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_rfc", Tipo = "String", Valor = proveedor.p_rfc.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_domicilio_fiscal", Tipo = "String", Valor = proveedor.p_domicilio_fiscal.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_rep_legal_nombre", Tipo = "String", Valor = proveedor.p_rep_legal_nombre.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_rep_legal_ap_paterno", Tipo = "String", Valor = proveedor.p_rep_legal_ap_paterno.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_rep_legal_ap_materno", Tipo = "String", Valor = proveedor.p_rep_legal_ap_materno.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_eje_cuenta_nombre", Tipo = "String", Valor = proveedor.p_eje_cuenta_nombre.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_eje_cuenta_ap_paterno", Tipo = "String", Valor = proveedor.p_eje_cuenta_ap_paterno.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_eje_cuenta_ap_materno", Tipo = "String", Valor = proveedor.p_eje_cuenta_ap_materno.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_telefono", Tipo = "String", Valor = proveedor.p_telefono.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_extension", Tipo = "String", Valor = proveedor.p_extension.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_correo_electronico", Tipo = "String", Valor = proveedor.p_correo_electronico.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_es_global", Tipo = "String", Valor = proveedor.p_es_global.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tipo_interlocutor", Tipo = "String", Valor = proveedor.p_tipo_interlocutor.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_dependencias_json", Tipo = "JSON", Valor = proveedor.dependencias_adicionales == null ? "NULL" : JsonConvert.SerializeObject(proveedor.dependencias_adicionales) });


                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_proveedor);
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
        public ResponseGeneric<List<Crudresponse>> Delete(tbl_proveedor_add proveedor)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = proveedor.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = proveedor.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_dependencia_id", Tipo = "String", Valor = proveedor.p_tbl_dependencia_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_numero", Tipo = "String", Valor = proveedor.p_numero == null ? "" : proveedor.p_numero.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_razon_social", Tipo = "String", Valor = proveedor.p_razon_social == null ? "" : proveedor.p_razon_social.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_rfc", Tipo = "String", Valor = proveedor.p_rfc == null ? "" : proveedor.p_rfc.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_domicilio_fiscal", Tipo = "String", Valor = proveedor.p_domicilio_fiscal == null ? "" : proveedor.p_domicilio_fiscal.ToString() }) ;
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_rep_legal_nombre", Tipo = "String", Valor = proveedor.p_rep_legal_nombre == null ? "" : proveedor.p_rep_legal_nombre.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_rep_legal_ap_paterno", Tipo = "String", Valor = proveedor.p_rep_legal_ap_paterno == null ? "" : proveedor.p_rep_legal_ap_paterno.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_rep_legal_ap_materno", Tipo = "String", Valor = proveedor.p_rep_legal_ap_materno == null ? "" : proveedor.p_rep_legal_ap_materno.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_eje_cuenta_nombre", Tipo = "String", Valor = proveedor.p_eje_cuenta_nombre == null ? "" : proveedor.p_eje_cuenta_nombre.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_eje_cuenta_ap_paterno", Tipo = "String", Valor = proveedor.p_eje_cuenta_ap_paterno == null ? "" : proveedor.p_eje_cuenta_ap_paterno.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_eje_cuenta_ap_materno", Tipo = "String", Valor = proveedor.p_eje_cuenta_ap_materno == null ? "" : proveedor.p_eje_cuenta_ap_materno.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_telefono", Tipo = "String", Valor = proveedor.p_telefono == null ? "" : proveedor.p_telefono.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_extension", Tipo = "String", Valor = proveedor.p_extension == null ? "" : proveedor.p_extension.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_correo_electronico", Tipo = "String", Valor = proveedor.p_correo_electronico == null ? "" : proveedor.p_correo_electronico.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_es_global", Tipo = "String", Valor =  proveedor.p_es_global.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tipo_interlocutor", Tipo = "String", Valor = proveedor.p_tipo_interlocutor.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_dependencias_json", Tipo = "JSON", Valor = proveedor.dependencias_adicionales == null ? "NULL" : JsonConvert.SerializeObject(proveedor.dependencias_adicionales) });

                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_proveedor);
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

        public ResponseGeneric<List<tbl_proveedor>> Get_lista_proveedores(String id_instancia)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_instancia_id", Tipo = "String", Valor = id_instancia.ToString() });
                List<tbl_proveedor> Lista = new List<tbl_proveedor>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_proveedor_instancia);
                            Lista = conexion.Query<tbl_proveedor>().FromSql<tbl_proveedor>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_proveedor>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_proveedor>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_contrato>> Get_lista_contratos_p(String proveedor)
        {
            try
            {

                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_proveedor_id", Tipo = "String", Valor = proveedor });

                List<tbl_contrato> Lista = new List<tbl_contrato>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_lista_contratos_proveedor);
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
        public ResponseGeneric<List<tbl_proveedor>> Get_lista_proveedores_by_id(String id_iproveedor)
        {
            try
            {

                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_proveedor_id", Tipo = "String", Valor = id_iproveedor.ToString() });
                List<tbl_proveedor> Lista = new List<tbl_proveedor>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_tbl_proveedor_by_id);
                            Lista = conexion.Query<tbl_proveedor>().FromSql<tbl_proveedor>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_proveedor>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_proveedor>>(ex);
            }
        }

        public ResponseGeneric<List<lista_tipo_interlocutor>> Get_lista_interlocutores(String activo)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_activo", Tipo = "String", Valor = activo.ToString() });
                List<lista_tipo_interlocutor> Lista = new List<lista_tipo_interlocutor>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_tipo_interlocutor);
                            Lista = conexion.Query<lista_tipo_interlocutor>().FromSql<lista_tipo_interlocutor>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<lista_tipo_interlocutor>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<lista_tipo_interlocutor>>(ex);
            }
        }


        public ResponseGeneric<List<proveedor_dependencia>> proveedor_dependencias(String proveedor)
        {
            try
            {

                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "interlocutor_id", Tipo = "String", Valor = proveedor });
                List<proveedor_dependencia> Lista = new List<proveedor_dependencia>();
                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_proveedor_dependencia);
                            Lista = conexion.Query<proveedor_dependencia>().FromSql<proveedor_dependencia>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<proveedor_dependencia>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("proveedor_dependencias", ex);
                return new ResponseGeneric<List<proveedor_dependencia>>(ex);
            }
        }


    }
}
