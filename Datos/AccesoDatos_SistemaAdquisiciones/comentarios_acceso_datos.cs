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
    public class comentarios_acceso_datos
    {
        public BDParametros GeneracionParametros = new BDParametros();
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string sp_get_comentario_solicitud = "sp_get_comentario_solicitud";
        string sp_comentario = "sp_comentario";

        public ResponseGeneric<List<comentarios>> get_comentario_solicitud(String solicitud)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "solicitud", Tipo = "String", Valor = solicitud });

                List<comentarios> Lista = new List<comentarios>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_comentario_solicitud);
                            Lista = conexion.Query<comentarios>().FromSql<comentarios>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<comentarios>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<comentarios>>(ex);
            }
        }


        public ResponseGeneric<Crudresponse> Add(comentario_add comentario)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "int", Valor = comentario.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = comentario.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_solicitud_id", Tipo = "String", Valor = comentario.p_tbl_solicitud_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_usuario_id", Tipo = "String", Valor = comentario.p_tbl_usuario_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_sigla_fase", Tipo = "String", Valor = comentario.p_sigla_fase.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_comentario", Tipo = "String", Valor = comentario.p_comentario.ToString() });
                

                Crudresponse Lista = new Crudresponse();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_comentario);
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