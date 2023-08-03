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
    public class tbl_partida_solicitud_contrato_temp_acceso_datos
    {
        public BDParametros GeneracionParametros = new BDParametros();
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string StoreProcedure = "sp_tbl_partida_solicitud_contrato_temp";

        public ResponseGeneric<List<Crudresponse>> Guardar(tbl_partida_solicitud_contrato_temp_add contrato_area)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = contrato_area.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = contrato_area.p_id.ToString() });                
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_area_id", Tipo = "String", Valor = contrato_area.p_tbl_area_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_partida_id", Tipo = "String", Valor = contrato_area.p_tbl_partida_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_ejercicio_id", Tipo = "String", Valor = contrato_area.p_tbl_ejercicio_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id_propietario", Tipo = "String", Valor = contrato_area.p_id_propietario.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_numero", Tipo = "String", Valor = contrato_area.p_numero.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_descripcion", Tipo = "String", Valor = contrato_area.p_descripcion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_ejercido", Tipo = "String", Valor = contrato_area.p_monto_ejercido.ToString() });


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
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Update_sol(sp_solicitud_en _tbl_solicitud)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "int", Valor = "5" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = _tbl_solicitud.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_num_solicitud", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_solicitud_id", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_contrato_id", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_solicitud", Tipo = "String", Valor = "0001-01-01T00:00:00" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_elaboro", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_area_id", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_proyecto_id", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_descripcion", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_solicitud", Tipo = "String", Valor = "0.0" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_autorizado", Tipo = "String", Valor = _tbl_solicitud.p_monto_autorizado.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_comentarios", Tipo = "String", Valor = _tbl_solicitud.p_comentarios.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_token_solicitante", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_token_autorizacion", Tipo = "String", Valor = _tbl_solicitud.p_token_autorizacion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_estatus_solicitud_id", Tipo = "String", Valor = _tbl_solicitud.p_tbl_estatus_solicitud_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_inclusion", Tipo = "String", Valor = "0001-01-01T00:00:00" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_json_pres", Tipo = "String", Valor = _tbl_solicitud.p_json_pres.ToString() });

                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, "sp_solicitud");
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

    }
}