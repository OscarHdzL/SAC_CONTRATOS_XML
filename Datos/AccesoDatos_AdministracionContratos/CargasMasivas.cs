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

namespace AccesoDatos_AdminContratos
{
    public class Cargas_Masivas_AD
    {
        public Crudresponse ResponseFunction { get; set; }

        public BDParametros GeneracionParametros = new BDParametros();
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();

        Dictionary<String, String> StoreProcedures__ = new Dictionary<String, String>();

        public Cargas_Masivas_AD (Dictionary<String, String> StoreProceduresDictionary, String Function_constructor)
        {
            StoreProcedures__.Add("area", "sp_area");
            StoreProcedures__.Add("subordinada", "sp_area_subordinada");
            StoreProcedures__.Add("subarea", "sp_subareas");
            StoreProcedures__.Add("proveedor", "sp_proveedor");

            ResponseFunction = MasiveSettings(StoreProceduresDictionary, StoreProcedures__[Function_constructor]);
        }

        public Crudresponse MasiveSettings(Dictionary<String, String> parameters, String Function)
        {
            try
            {

                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                foreach (KeyValuePair<String, String> entry in parameters)
                {
                    ListaEnvioParam.Add(new EntidadParametro { Nombre = entry.Key, Tipo = "String", Valor = entry.Value == null ? "" : entry.Value });
                }
                List<Crudresponse> Lista = new List<Crudresponse>();
         
     
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, Function);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            String code = Lista[0].cod;
                            if (code != "success")
                                {
                                code = code + " -";
                            }
                            break;
                    }

                }
                return Lista[0];
            }
            catch (Exception ex)
            {
                Crudresponse Response_ = new Crudresponse();
                Response_.cod = 500.ToString();
                Response_.msg = ex.Message;
                return Response_;
            }
        }


    }
}