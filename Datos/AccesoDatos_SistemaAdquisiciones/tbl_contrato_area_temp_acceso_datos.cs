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
    public class tbl_contrato_area_temp_acceso_datos
    {
        public BDParametros GeneracionParametros = new BDParametros();
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string StoreProcedure = "sp_tbl_contrato_area_temp";

        public ResponseGeneric<List<Crudresponse>> Guardar(tbl_contrato_area_add contrato_area)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = contrato_area.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = contrato_area.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_id", Tipo = "String", Valor = contrato_area.p_tbl_contrato_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_area_id", Tipo = "String", Valor = contrato_area.p_tbl_area_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_partida_id", Tipo = "String", Valor = contrato_area.p_tbl_partida_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_ejercicio_id", Tipo = "String", Valor = contrato_area.p_tbl_ejercicio_id.ToString() });
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


    }
}