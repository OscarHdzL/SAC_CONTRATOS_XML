using Conexion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modelos.Interfaz;
using Modelos.Modelos.Contrato;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.IO;
using Modelos.Modelos;

namespace AccesoDatos
{
    public class tbl_tiposcontrato_acceso_datos : crud_tiposcontrato<tbl_tipo_contrato>
    {
        public BDParametros GeneracionParametros = new BDParametros();
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string StoreProcedure = "sp_get_tiposcontrato";

        public ResponseGeneric<List<tbl_tipo_contrato>> Consultar()
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                //ListaEnvioParam.Add(new EntidadParametro { Nombre = "id", Tipo = "Int", Valor = entidad.ToString() });

                List<tbl_tipo_contrato> Lista = new List<tbl_tipo_contrato>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure);
                            Lista = conexion.Query<tbl_tipo_contrato>().FromSql<tbl_tipo_contrato>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_tipo_contrato>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_tipo_contrato>>(ex);
            }
        }


    }
}
