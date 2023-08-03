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
    public class Modalidad_datos
    {
        public BDParametros GeneracionParametros = new BDParametros();
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();

        String sp_modalidad = "sp_modalidad"; 
        String sp_get_modalidad_catalogos = "sp_get_modalidad_catalogos";
        String sp_get_modalidad_solicitud = "sp_get_modalidad_solicitud";
        String sp_modalidadProgra_validar = "sp_modalidadProgra_validar";
        String sp_modalidad_get_parcial = "sp_modalidad_get_parcial";


        public ResponseGeneric<List<ModalidadSolParcial>> modalidad_get_parcial(Guid p_tbl_solicitud_id)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_solicitud_id", Tipo = "String", Valor = p_tbl_solicitud_id.ToString() });

                List<ModalidadSolParcial> Lista = new List<ModalidadSolParcial>();
                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_modalidad_get_parcial);
                            Lista = conexion.Query<ModalidadSolParcial>().FromSql<ModalidadSolParcial>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<ModalidadSolParcial>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<ModalidadSolParcial>>(ex);
            }
        }



        public ResponseGeneric<List<CrudresponseNum>> validar(Guid p_tbl_solicitud_id)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_solicitud_id", Tipo = "String", Valor = p_tbl_solicitud_id.ToString() });
 
                List<CrudresponseNum> Lista = new List<CrudresponseNum>();
                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_modalidadProgra_validar);
                            Lista = conexion.Query<CrudresponseNum>().FromSql<CrudresponseNum>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<CrudresponseNum>>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<CrudresponseNum>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> add(sp_modalidad obj)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = obj.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = obj.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_solicitud_id", Tipo = "String", Valor = obj.p_tbl_solicitud_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_modalidad_id", Tipo = "String", Valor = obj.p_tbl_tipo_modalidad_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_licitacion_id", Tipo = "String", Valor = obj.p_tbl_tipo_licitacion_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_falta_documentacion", Tipo = "String", Valor = obj.p_falta_documentacion ? "1" : "0" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_requiere_justificacion", Tipo = "String", Valor = obj.p_requiere_justificacion ? "1" : "0" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_token", Tipo = "String", Valor = obj.p_token.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_estatus", Tipo = "String", Valor = obj.p_estatus ? "1" : "0" });
               // ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_inclusion", Tipo = "String", Valor = obj.p_inclusion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_visita_sitio", Tipo = "String", Valor = obj.p_visita_sitio ? "1" : "0" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_aclaraciones", Tipo = "String", Valor = obj.p_aclaraciones ? "1" : "0" });
   


                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_modalidad);
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

        public ResponseGeneric<List<DropDownList>> get_modalidad_catalogos(String opcion)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tipo", Tipo = "String", Valor = opcion });
                List<DropDownList> Lista = new List<DropDownList>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_modalidad_catalogos);
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

        

        public ResponseGeneric<tbl_modalidad> get_modalidad_solicitud(String solicitud)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "solicitud", Tipo = "String", Valor = solicitud.ToString() });
                tbl_modalidad Lista = new tbl_modalidad();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_modalidad_solicitud);
                            Lista = conexion.Query<tbl_modalidad>().FromSql<tbl_modalidad>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<tbl_modalidad>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<tbl_modalidad>(ex);
            }
        }


    }
}
