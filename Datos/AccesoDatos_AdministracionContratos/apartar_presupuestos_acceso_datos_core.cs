using Conexion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Contrato;
using Modelos.Modelos.Dependencia;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.IO;
using Utilidades.Log4Net;

namespace AccesoDatos_AdminContratos
{
    public class apartar_presupuestos_acceso_datos_core
    {
        public BDParametros GeneracionParametros = new BDParametros();
        private readonly ILoggerManager _logger;
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string sp_apartar_presupuesto_area = "sp_apartar_presupuesto_area";
        string sp_origen_recurso = "sp_origen_recurso";

        public apartar_presupuestos_acceso_datos_core()
        {
            _logger = new LoggerManager();
        }
        public ResponseGeneric<CrudresponseIdentificador> add_origen_recurso(origen_recurso_add origen)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = origen.p_opt_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_sigla_tipo_origen", Tipo = "String", Valor = origen.p_sigla_tipo_origen.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_solicitud_id", Tipo = "String", Valor = origen.p_tbl_solicitud_id.ToString() });
                //ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_apartar", Tipo = "String", Valor = origen.p_monto_apartar.ToString() });
               

                CrudresponseIdentificador Lista = new CrudresponseIdentificador();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_origen_recurso);
                            Lista = conexion.Query<CrudresponseIdentificador>().FromSql<CrudresponseIdentificador>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<CrudresponseIdentificador>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return new ResponseGeneric<CrudresponseIdentificador>(ex);
            }
        }

        public ResponseGeneric<CrudresponseIdentificador> add_apartar_presupuesto_area(apartar_presupuesto_area_add presupuesto)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt_id ", Tipo = "String", Valor = presupuesto.p_opt_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_origen_recurso_id", Tipo = "String", Valor = presupuesto.p_tbl_origen_recurso_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_capitulo_gastos_area", Tipo = "String", Valor = presupuesto.p_tbl_capitulo_gastos_area.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_apartar", Tipo = "String", Valor = presupuesto.p_monto_apartar.ToString() });


                CrudresponseIdentificador Lista = new CrudresponseIdentificador();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_origen_recurso);
                            Lista = conexion.Query<CrudresponseIdentificador>().FromSql<CrudresponseIdentificador>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<CrudresponseIdentificador>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return new ResponseGeneric<CrudresponseIdentificador>(ex);
            }
        }


    }
}
