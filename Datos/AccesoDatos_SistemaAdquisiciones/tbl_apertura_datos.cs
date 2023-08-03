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
    public class tbl_apertura_datos : crud_apertura
    {
        public BDParametros GeneracionParametros = new BDParametros();
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string sp_apertura = "sp_apertura";
        string sp_get_municipios_por_estado = "sp_get_municipios_por_estado";
        string sp_declarar_desierta_solicitud = "sp_declarar_desierta_solicitud";

        public ResponseGeneric<List<Crudresponse>> Add(tbl_apertura _tbl_apertura)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "int", Valor = "2" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = _tbl_apertura.id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_solicitud_id", Tipo = "String", Valor = _tbl_apertura.tbl_solicitud_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_municipio_id", Tipo = "String", Valor = _tbl_apertura.tbl_municipio_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_apertura_id", Tipo = "String", Valor = _tbl_apertura.tbl_tipo_apertura_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha", Tipo = "String", Valor = _tbl_apertura.fecha.ToString("yyyy-MM-ddTHH:mm:ss") });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_hora", Tipo = "String", Valor = _tbl_apertura.hora.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_direccion", Tipo = "String", Valor = _tbl_apertura.direccion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_comentario", Tipo = "String", Valor = _tbl_apertura.comentario.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_declaracion_desierta", Tipo = "String", Valor = _tbl_apertura.declaracion_desierta.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_token", Tipo = "String", Valor = _tbl_apertura.token.ToString() });

                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_apertura);
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

        public ResponseGeneric<List<DropDownList>> Get_Municipio(Guid id_estado)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_estado_id", Tipo = "String", Valor = id_estado.ToString() });

                List<DropDownList> Lista = new List<DropDownList>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_municipios_por_estado);
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


        

        public ResponseGeneric<Crudresponse> DeclararDesierta(Guid solicitud)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "solicitud", Tipo = "String", Valor = solicitud.ToString() });
                
                Crudresponse Lista = new Crudresponse();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_declarar_desierta_solicitud);
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