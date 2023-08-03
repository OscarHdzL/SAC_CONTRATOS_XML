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
    public class tbl_plan_cumplimiento_acceso_datos 
    {
        public BDParametros GeneracionParametros = new BDParametros();
        private readonly ILoggerManager _logger;
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string sp_tbl_plan_cumplimiento = "sp_tbl_plan_cumplimiento";
        string sp_confirmar_pm = "sp_confirmar_pm";
        string sp_cerrar_contrato = "sp_cerrar_contrato";

        public tbl_plan_cumplimiento_acceso_datos()
        {
            _logger = new LoggerManager();
        }

        public ResponseGeneric<List<Crudresponse>> tbl_plan_cumplimiento(string opcion, Guid id, Guid tbl_link_obligacion_id, Guid tbl_plan_entrega_producto_id,String tipo)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "opcion", Tipo = "String", Valor = opcion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "id", Tipo = "String", Valor = id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tbl_link_obligacion_id", Tipo = "String", Valor = tbl_link_obligacion_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tbl_plan_entrega_producto_id", Tipo = "String", Valor = tbl_plan_entrega_producto_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tipo", Tipo = "String", Valor = tipo });


                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_tbl_plan_cumplimiento);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("tbl_plan_cumplimiento", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }


        public ResponseGeneric<Crudresponse> ConfirmarPM(Guid tbl_plan_monitoreo_id)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_plan_monitoreo", Tipo = "String", Valor = tbl_plan_monitoreo_id.ToString() });
                


                Crudresponse Lista = new Crudresponse();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_confirmar_pm);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<Crudresponse>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("ConfirmarPM", ex);
                return new ResponseGeneric<Crudresponse>(ex);
            }
        }


        public ResponseGeneric<Crudresponse> CerrarContrato(Guid tbl_contrato_id)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_id", Tipo = "String", Valor = tbl_contrato_id.ToString() });



                Crudresponse Lista = new Crudresponse();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_cerrar_contrato);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<Crudresponse>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("CerrarContrato", ex);
                return new ResponseGeneric<Crudresponse>(ex);
            }
        }


    }
}
