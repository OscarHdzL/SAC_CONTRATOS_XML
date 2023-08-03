using Conexion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Contrato;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.IO;
using Utilidades.Log4Net;

namespace AccesoDatos
{
    public class tbl_dependencia_acceso_datos : crud_areas
    {
        public BDParametros GeneracionParametros = new BDParametros();
        private readonly ILoggerManager _logger;
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string StoreProcedure_instancia = "sp_get_dependenciainstancia_dropdown";
        string StoreProcedure_instancia_contrato = "sp_get_dependenciainstancia_dropdown_contrato";

        public tbl_dependencia_acceso_datos()
        {
            _logger = new LoggerManager();
        }
        public ResponseGeneric<List<DropDownList>> FillDrop(String instancia)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "instancia", Tipo = "String", Valor = instancia.ToString() });

                List<DropDownList> Lista = new List<DropDownList>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_instancia_contrato);
                            Lista = conexion.Query<DropDownList>().FromSql<DropDownList>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<DropDownList>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("FillDrop", ex);
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }

    }
}
