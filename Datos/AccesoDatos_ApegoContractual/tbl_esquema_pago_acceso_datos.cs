using Conexion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.EsquemaPago;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.IO;
using Utilidades.Log4Net;

namespace AccesoDatos
{
    public class tbl_esquema_pago_acceso_datos : crud_esquemapago
    {
        public BDParametros GeneracionParametros = new BDParametros();
        private readonly ILoggerManager _logger;
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string StoreProcedure = "sp_get_esquemapago_contrato";
        string StoreProcedure_proveedores = "sp_get_proveedores_contrato";
        string StoreProcedure_instancia = "sp_get_instancia";
        string StoreProcedure_add = "sp_esquema_pago";
        string StoreProcedure_byId = "sp_get_esquemapago";
        string StoreProcedure_planes_entrega = "sp_get_planes_entrega_x_contrato";
        string sp_get_interlocutor_cuentas_x_pagar_dependencia = "sp_get_interlocutor_cuentas_x_pagar_dependencia";
        string sp_get_info_esquema_correo = "sp_get_info_esquema_correo";

        string sp_get_plan_esquema = "sp_get_plan_esquema";

        public tbl_esquema_pago_acceso_datos()
        {
            _logger = new LoggerManager();
        }

        public ResponseGeneric<List<tbl_esquema_pago_new>> ConsultarEsquemasPago(String contrato)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "contrato", Tipo = "String", Valor = contrato.ToString() });

                List<tbl_esquema_pago_new> Lista = new List<tbl_esquema_pago_new>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure);
                            Lista = conexion.Query<tbl_esquema_pago_new>().FromSql<tbl_esquema_pago_new>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_esquema_pago_new>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("ConsultarEsquemasPago", ex);
                return new ResponseGeneric<List<tbl_esquema_pago_new>>(ex);
            }
        }

        public ResponseGeneric<tbl_esquema_pago_new> ConsultarEsquemaPago_esquemacontrato(String esquema, String contrato)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "esquema", Tipo = "String", Valor = esquema.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "contrato", Tipo = "String", Valor = contrato.ToString() });

                tbl_esquema_pago_new Lista = new tbl_esquema_pago_new();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_byId);
                            Lista = conexion.Query<tbl_esquema_pago_new>().FromSql<tbl_esquema_pago_new>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<tbl_esquema_pago_new>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("ConsultarEsquemaPago_esquemacontrato", ex);
                return new ResponseGeneric<tbl_esquema_pago_new>(ex);
            }
        }


        public ResponseGeneric<List<tbl_proveedores_esquemapago>> ConsultarProveedores_Contrato(String contrato)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "contrato", Tipo = "String", Valor = contrato.ToString() });

                List<tbl_proveedores_esquemapago> Lista = new List<tbl_proveedores_esquemapago>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_proveedores);
                            Lista = conexion.Query<tbl_proveedores_esquemapago>().FromSql<tbl_proveedores_esquemapago>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_proveedores_esquemapago>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("ConsultarProveedores_Contrato", ex);
                return new ResponseGeneric<List<tbl_proveedores_esquemapago>>(ex);
            }
        }


        public ResponseGeneric<tbl_instancia> ConsultarInstancia(String instancia)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "instancia", Tipo = "String", Valor = instancia.ToString() });

                tbl_instancia Lista = new tbl_instancia();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_instancia);
                            Lista = conexion.Query<tbl_instancia>().FromSql<tbl_instancia>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync().Result;
                            break;
                    }
                }
                #endregion
                return new ResponseGeneric<tbl_instancia>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("ConsultarInstancia", ex);
                return new ResponseGeneric<tbl_instancia>(ex);
            }
        }




        public ResponseGeneric<List<Crudresponse>> add(tbl_esquema_pago_add esquema)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "Int", Valor = esquema.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = esquema.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_servidor_resp_id", Tipo = "String", Valor = esquema.p_tbl_contrato_servidor_resp_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_proveedor_id", Tipo = "String", Valor = esquema.p_tbl_contrato_proveedor_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_pago", Tipo = "String", Valor = esquema.p_fecha_pago.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto", Tipo = "String", Valor = esquema.p_monto!=null? esquema.p_monto.ToString().Replace(",",""):"" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_iva", Tipo = "String", Valor = esquema.p_monto_iva!=null? esquema.p_monto_iva.ToString().Replace(",",""):"" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_total", Tipo = "String", Valor = esquema.p_total!=null? esquema.p_total.ToString().Replace(",",""):"" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_estado_plan_entrega", Tipo = "String", Valor = esquema.p_estado_plan_entrega!= null?esquema.p_estado_plan_entrega.ToString():"" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tiene_firma", Tipo = "String", Valor = esquema.p_tiene_firma.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_observacion", Tipo = "String", Valor = esquema.p_observacion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_token_fac", Tipo = "String", Valor = esquema.p_token_fac.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_inclusion", Tipo = "String", Valor = esquema.p_inclusion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_estatus", Tipo = "String", Valor = esquema.p_estatus.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_plan_entrega_id", Tipo = "String", Valor = esquema.p_tbl_plan_entrega_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_notificacion_proveedor_id", Tipo = "String", Valor = esquema.p_notificacion_proveedor_id.ToString() });

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





        public ResponseGeneric<List<Crudresponse>> update(tbl_esquema_pago_add esquema)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "Int", Valor = esquema.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = esquema.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_servidor_resp_id", Tipo = "String", Valor = esquema.p_tbl_contrato_servidor_resp_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_proveedor_id", Tipo = "String", Valor = esquema.p_tbl_contrato_proveedor_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_pago", Tipo = "String", Valor = esquema.p_fecha_pago.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto", Tipo = "String", Valor = esquema.p_monto.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_iva", Tipo = "String", Valor = esquema.p_monto_iva.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_total", Tipo = "String", Valor = esquema.p_total.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_estado_plan_entrega", Tipo = "String", Valor = esquema.p_estado_plan_entrega.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tiene_firma", Tipo = "String", Valor = esquema.p_tiene_firma.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_observacion", Tipo = "String", Valor = esquema.p_observacion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_token_fac", Tipo = "String", Valor = esquema.p_token_fac.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_inclusion", Tipo = "String", Valor = esquema.p_inclusion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_estatus", Tipo = "String", Valor = esquema.p_estatus.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_plan_entrega_id", Tipo = "String", Valor = esquema.p_tbl_plan_entrega_id.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_notificacion_proveedor_id", Tipo = "String", Valor = esquema.p_notificacion_proveedor_id.ToString() });
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


        public ResponseGeneric<List<Crudresponse>> delete(tbl_esquema_pago_add esquema)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "Int", Valor = NullToString(esquema.p_opt) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = NullToString(esquema.p_id) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_servidor_resp_id", Tipo = "String", Valor = NullToString(esquema.p_tbl_contrato_servidor_resp_id) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_proveedor_id", Tipo = "String", Valor = NullToString(esquema.p_tbl_contrato_proveedor_id) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_pago", Tipo = "String", Valor = NullToString(esquema.p_fecha_pago) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto", Tipo = "String", Valor = NullToString(esquema.p_monto) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_iva", Tipo = "String", Valor = NullToString(esquema.p_monto_iva) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_total", Tipo = "String", Valor = NullToString(esquema.p_total) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_estado_plan_entrega", Tipo = "String", Valor = NullToString(esquema.p_estado_plan_entrega) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tiene_firma", Tipo = "String", Valor = NullToString(esquema.p_tiene_firma) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_observacion", Tipo = "String", Valor = NullToString(esquema.p_observacion) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_token_fac", Tipo = "String", Valor = NullToString(esquema.p_token_fac) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_inclusion", Tipo = "String", Valor = NullToString(esquema.p_inclusion) });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_estatus", Tipo = "String", Valor = NullToString(esquema.p_estatus) });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_plan_entrega_id", Tipo = "String", Valor = NullToString(esquema.p_tbl_plan_entrega_id) });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_notificacion_proveedor_id", Tipo = "String", Valor = NullToString(esquema.p_notificacion_proveedor_id) });

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
        public ResponseGeneric<List<tbl_planes_sin_esquema>> ConsultarPlanes_Sin_Esquema(String contrato)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_contrato_id", Tipo = "String", Valor = contrato.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_sin_esquema_pago", Tipo = "Int", Valor = "1" });

                List<tbl_planes_sin_esquema> Lista = new List<tbl_planes_sin_esquema>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_planes_entrega);
                            Lista = conexion.Query<tbl_planes_sin_esquema>().FromSql<tbl_planes_sin_esquema>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_planes_sin_esquema>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("ConsultarPlanes_Sin_Esquema", ex);
                return new ResponseGeneric<List<tbl_planes_sin_esquema>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_planes_sin_esquema>> ConsultarPlan_Del_Esquema(String idEsquema)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_esquema_id", Tipo = "String", Valor = idEsquema });

                List<tbl_planes_sin_esquema> Lista = new List<tbl_planes_sin_esquema>();
                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_plan_esquema);
                            Lista = conexion.Query<tbl_planes_sin_esquema>().FromSql<tbl_planes_sin_esquema>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_planes_sin_esquema>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("ConsultarPlan_Del_Esquema", ex);
                return new ResponseGeneric<List<tbl_planes_sin_esquema>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_proveedor>> get_cuentas_pagar(Guid id_dependencia)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_dependencia", Tipo = "String", Valor = id_dependencia.ToString() });
                List<tbl_proveedor> Lista = new List<tbl_proveedor>();
                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_interlocutor_cuentas_x_pagar_dependencia);
                            Lista = conexion.Query<tbl_proveedor>().FromSql<tbl_proveedor>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_proveedor>>(Lista);
            }
            catch (Exception ex)
            {
                _logger.LogError("get_cuentas_pagar", ex);
                return new ResponseGeneric<List<tbl_proveedor>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_esquema_pago_info_correo>> InfoEsquemaCorreo(string id_esquema)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = id_esquema });
                
                List<tbl_esquema_pago_info_correo> Lista = new List<tbl_esquema_pago_info_correo>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_info_esquema_correo);
                            Lista = conexion.Query<tbl_esquema_pago_info_correo>().FromSql<tbl_esquema_pago_info_correo>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_esquema_pago_info_correo>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("InfoEsquemaCorreo", ex);
                return new ResponseGeneric<List<tbl_esquema_pago_info_correo>>(ex);
            }
        }


        public static string NullToString(object Value)
        {

            // Value.ToString() allows for Value being DBNull, but will also convert int, double, etc.
            return Value == null ? "" : Value.ToString();



        }
    }
}
