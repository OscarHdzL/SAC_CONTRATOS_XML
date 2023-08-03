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
    public class sp_confirmar_pe_acceso_datos
    {
        public BDParametros GeneracionParametros = new BDParametros();
        private readonly ILoggerManager _logger;
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string StoreProcedure_confirmar_pe = "sp_confirmar_pe";
        string StoreProcedure_sp_get_pe_incumplidas = "sp_get_pe_incumplidas";

        string StoreProcedure_eliminar_productos_plan_entrega = "sp_eliminar_productos_plan_entrega";
        string StoreProcedure_eliminar_ubicacion_plan_entrega = "sp_eliminar_ubicacion_plan_entrega";
        string StoreProcedure_eliminar_plan_entrega = "sp_eliminar_plan_entrega";

        public sp_confirmar_pe_acceso_datos()
        {
            _logger = new LoggerManager();
        }

        public ResponseGeneric<Crudresponse> confirmar_pe(String input)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_plan_entrega_id", Tipo = "String", Valor = input.ToString() });



                Crudresponse Lista = new Crudresponse();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_confirmar_pe);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<Crudresponse>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<Crudresponse>(ex);
            }
        }
        public ResponseGeneric<List<tbl_obligacion_cls_PE>> get_obligacion_Incumplidas(int p_opt, Guid tbl_plan_entrega_id_, Guid plan_entrega_producto_id)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_plan_entrega_id", Tipo = "String", Valor = tbl_plan_entrega_id_.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_plan_entrega_producto_id", Tipo = "String", Valor = plan_entrega_producto_id.ToString() });
                List<tbl_obligacion_cls_PE> Lista = new List<tbl_obligacion_cls_PE>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_sp_get_pe_incumplidas);
                            Lista = conexion.Query<tbl_obligacion_cls_PE>().FromSql<tbl_obligacion_cls_PE>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_obligacion_cls_PE>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_obligacion_cls_PE>>(ex);
            }
        }

        #region eliminar elementos plan entrega
        public ResponseGeneric<Crudresponse> Eliminar_producto_plan_entrega(String tbl_plan_entrega_id, String plan_entrega_producto_id, String tbl_ubicacion_plan_entrega_id, String tbl_contrato_producto_id)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_plan_entrega_id", Tipo = "String", Valor = tbl_plan_entrega_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_plan_entrega_producto_id", Tipo = "String", Valor = plan_entrega_producto_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_ubicacion_plan_entrega_id", Tipo = "String", Valor = tbl_ubicacion_plan_entrega_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_producto_id", Tipo = "String", Valor = tbl_contrato_producto_id.ToString() });

                Crudresponse Lista = new Crudresponse();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_eliminar_productos_plan_entrega);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<Crudresponse>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("eliminar", ex);
                return new ResponseGeneric<Crudresponse>(ex);
            }
        }

        public ResponseGeneric<Crudresponse> Eliminar_ubicacion_plan_entrega(String tbl_plan_entrega_id, String tbl_ubicacion_id)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_plan_entrega_id", Tipo = "String", Valor = tbl_plan_entrega_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_ubicacion_id", Tipo = "String", Valor = tbl_ubicacion_id.ToString() });

                Crudresponse Lista = new Crudresponse();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_eliminar_ubicacion_plan_entrega);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<Crudresponse>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("eliminar", ex);
                return new ResponseGeneric<Crudresponse>(ex);
            }
        }

        public ResponseGeneric<Crudresponse> Eliminar_plan_entrega(String tbl_plan_entrega_id)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_plan_entrega_id", Tipo = "String", Valor = tbl_plan_entrega_id.ToString() });
                Crudresponse Lista = new Crudresponse();
                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_eliminar_plan_entrega);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<Crudresponse>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("eliminar", ex);
                return new ResponseGeneric<Crudresponse>(ex);
            }
        }

        #endregion



    }

}
