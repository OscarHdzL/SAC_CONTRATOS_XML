using System;

using Modelos.Interfaz;
using Modelos.Modelos;
 

using System.Collections.Generic;
using Conexion;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.IO;
using Modelos.Response;
 

namespace AccesoDatos
{
    public class tbl_tipo_acuerdo_datos 
    {
        public BDParametros GeneracionParametros = new BDParametros();
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string sp_tipoacuerdo = "sp_tipoacuerdo";

        public ResponseGeneric<List<tbl_tipo_acuerdo>> Consultar(Guid entidad)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                List<tbl_tipo_acuerdo> Lista = new List<tbl_tipo_acuerdo>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_tipoacuerdo);
                            Lista = conexion.Query<tbl_tipo_acuerdo>().FromSql<tbl_tipo_acuerdo>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;

                            break;
                    }

                }
                #endregion

                return new ResponseGeneric<List<tbl_tipo_acuerdo>>(Lista);

            }
            catch (Exception ex)
            {

                return new ResponseGeneric<List<tbl_tipo_acuerdo>>(ex);
            }
        }
  





    }
}
