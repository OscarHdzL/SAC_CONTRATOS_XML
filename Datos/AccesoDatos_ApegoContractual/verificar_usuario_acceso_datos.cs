using Conexion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.IO;

namespace AccesoDatos
{
    public class verificar_usuario_acceso_datos : CRUD_verificar_usuario<tbl_usuario_verifica, verificacion_usuario>
    {
        public BDParametros GeneracionParametros = new BDParametros();
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string StoreProcedure = "sp_valida_usuario";

        public ResponseGeneric<List<tbl_usuario_verifica>> Consultar(tbl_usuario_verifica entidad, verificacion_usuario usuario)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "usuario_email", Tipo = "String", Valor = usuario.Email });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "usuario_password", Tipo = "String", Valor = usuario.Password });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "usuario_salto", Tipo = "String", Valor = usuario.Salto });

                List<tbl_usuario_verifica> Lista = new List<tbl_usuario_verifica>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure);
                            Lista = conexion.Query<tbl_usuario_verifica>().FromSql<tbl_usuario_verifica>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion

                return new ResponseGeneric<List<tbl_usuario_verifica>>(Lista);

            }
            catch (Exception ex)
            {

                return new ResponseGeneric<List<tbl_usuario_verifica>>(ex);
            }
        }


    }
}
