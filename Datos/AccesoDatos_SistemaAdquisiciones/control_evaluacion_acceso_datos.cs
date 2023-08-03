using Conexion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.IO;

namespace AccesoDatos_SistemaAdquisiciones
{
    public class control_evaluacion_acceso_datos
    {
        public BDParametros GeneracionParametros = new BDParametros();
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string sp_get_grid_evaluacion_propuestas_solicitud = "sp_get_grid_evaluacion_propuestas_solicitud";
        string sp_evaluacion_propuesta = "sp_evaluacion_propuesta";
        


        public ResponseGeneric<List<grid_evaluacion_propuestas_solicitud>> Get_Evaluacion_Propuesta(String Solicitud)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "solicitud", Tipo = "String", Valor = Solicitud });
                

                List<grid_evaluacion_propuestas_solicitud> Lista = new List<grid_evaluacion_propuestas_solicitud>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_grid_evaluacion_propuestas_solicitud);
                            Lista = conexion.Query<grid_evaluacion_propuestas_solicitud>().FromSql<grid_evaluacion_propuestas_solicitud>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<grid_evaluacion_propuestas_solicitud>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<grid_evaluacion_propuestas_solicitud>>(ex);
            }
        }

        public ResponseGeneric<Crudresponse> Add(evaluacion_propuesta_add evaluacion)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "int", Valor = evaluacion.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = evaluacion.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_licitante_id", Tipo = "String", Valor = evaluacion.p_tbl_licitante_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_analisis", Tipo = "String", Valor = evaluacion.p_analisis.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_no_cumplio", Tipo = "String", Valor = evaluacion.p_no_cumplio.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_motivo_incumplimiento", Tipo = "String", Valor = evaluacion.p_motivo_incumplimiento.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_remitir_eval_tec", Tipo = "String", Valor = evaluacion.p_remitir_eval_tec.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_remitir_eval_eco", Tipo = "String", Valor = evaluacion.p_remitir_eval_eco.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_ganador", Tipo = "String", Valor = evaluacion.p_ganador.ToString() });

                Crudresponse Lista = new Crudresponse();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_evaluacion_propuesta);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<Crudresponse>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<Crudresponse>(ex);
            }
        }

    }
}
