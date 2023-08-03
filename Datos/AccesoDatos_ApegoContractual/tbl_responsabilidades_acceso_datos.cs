using Conexion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Responsabilidades;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.IO;
using Utilidades.Log4Net;

namespace AccesoDatos
{
    public class tbl_responsabilidades_acceso_datos : crud_responsabilidades
    {
        public BDParametros GeneracionParametros = new BDParametros();
        private readonly ILoggerManager _logger;
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string StoreProcedure = "sp_get_responsabilidad";
        string StoreProcedure_update_email = "sp_persona_update_email";

        public tbl_responsabilidades_acceso_datos()
        {
            _logger = new LoggerManager();
        }
        public ResponseGeneric<List<tbl_responsabilidad>> Consultar()
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                //ListaEnvioParam.Add(new EntidadParametro { Nombre = "id", Tipo = "Int", Valor = entidad.ToString() });

                List<tbl_responsabilidad> Lista = new List<tbl_responsabilidad>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure);
                            Lista = conexion.Query<tbl_responsabilidad>().FromSql<tbl_responsabilidad>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_responsabilidad>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_responsabilidad>>(ex);
            }
        }



        
            public ResponseGeneric<List<Crudresponse>> Update_Email(Guid idPersona, String Correo)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_persona", Tipo = "Int", Valor = idPersona.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_email", Tipo = "Int", Valor = Correo.ToString() });

                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure_update_email);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("upd", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        //public ResponseGeneric<List<tbl_responsabilidad>> ConsultarResponsabilidad()
        //{
        //    try
        //    {
        //        #region Parametros
        //        List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
        //        //ListaEnvioParam.Add(new EntidadParametro { Nombre = "id", Tipo = "Int", Valor = entidad.ToString() });

        //        List<tbl_responsabilidad> Lista = new List<tbl_responsabilidad>();

        //        #endregion

        //        #region ConexionBD
        //        using (Contexto conexion = new Contexto())
        //        {
        //            switch (int.Parse(Configuration["TipoBase"].ToString()))
        //            {
        //                case 2:
        //                    var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure);
        //                    Lista = conexion.Query<tbl_responsabilidad>().FromSql<tbl_responsabilidad>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
        //                    break;
        //            }

        //        }
        //        #endregion
        //        return new ResponseGeneric<List<tbl_responsabilidad>>(Lista);

        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResponseGeneric<List<tbl_responsabilidad>>(ex);
        //    }
        //}


    }
}
