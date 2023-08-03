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
    public class tbl_partida_area_acceso_datos 
    {
        public BDParametros GeneracionParametros = new BDParametros();
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string StoreProcedure = "sp_get_partidas_area_dropdown";
        string StoreProcedure_Monto = "sp_get_montoseleccionado_area_partida";

        public ResponseGeneric<List<DropDownList>> FillDrop(String area, String ejercicio)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "area", Tipo = "String", Valor = area.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "ejercicio", Tipo = "String", Valor = ejercicio.ToString() });

                List<DropDownList> Lista = new List<DropDownList>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure);
                            Lista = conexion.Query<DropDownList>().FromSql<DropDownList>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<DropDownList>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }

        public ResponseGeneric<monto_seleccionado_area_partida> MontoSeleccionado_area_partida(String area, String partida)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "area", Tipo = "String", Valor = area.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "partida", Tipo = "String", Valor = partida.ToString() });

                monto_seleccionado_area_partida Lista = new monto_seleccionado_area_partida();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_Monto);
                            Lista = conexion.Query<monto_seleccionado_area_partida>().FromSql<monto_seleccionado_area_partida>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<monto_seleccionado_area_partida>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<monto_seleccionado_area_partida>(ex);
            }
        }


    }
}