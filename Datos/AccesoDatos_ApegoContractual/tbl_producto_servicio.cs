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
using Utilidades.Log4Net;

namespace AccesoDatos
{
    public class tbl_producto_servicio_datos : crud_tbl_producto_servicio
    {
        public BDParametros GeneracionParametros = new BDParametros();
        private readonly ILoggerManager _logger;
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string sp_tbl_producto_servicio_list = "sp_tbl_producto_servicio_list";
        string sp_tbl_producto_servicio_unitario = "sp_tbl_producto_servicio_unitario";
        string sp_get_tbl_unidad_medida = "sp_get_tbl_unidad_medida";
        string sp_producto_servicio = "sp_producto_servicio";
        string sp_contrato_producto_contrato = "sp_contrato_producto_contrato";
        string sp_get_productos_ubicacion = "sp_get_productos_ubicacion";
        string sp_get_producto_servicio_pe_ubi = "sp_get_producto_servicio_pe_ubi";
        string sp_get_tipo_producto_servicio = "sp_get_tipo_producto_servicio";



        public tbl_producto_servicio_datos()
        {
            _logger = new LoggerManager();
        }



        public ResponseGeneric<List<Crudresponse>> Add(tbl_producto_servicio_add entidad)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "int", Valor = entidad.p_opt.ToString() }) ;
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "int", Valor = entidad.p_id == null ? Guid.NewGuid().ToString() : entidad.p_id.ToString() }); ;
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_dependencia_id", Tipo = "int", Valor = entidad.p_tbl_dependencia_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_producto_servicio", Tipo = "int", Valor = entidad.p_producto_servicio.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_clave_producto", Tipo = "int", Valor = entidad.p_clave_producto.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_elemento", Tipo = "int", Valor = entidad.p_elemento.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_elemento_desc", Tipo = "int", Valor = entidad.p_elemento_desc.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_unidad_medida_id", Tipo = "int", Valor = entidad.p_tbl_unidad_medida_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_activo", Tipo = "int", Valor = entidad.p_activo.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_comentario", Tipo = "int", Valor = entidad.p_comentario.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_id", Tipo = "int", Valor = entidad.p_tbl_tipo_id.ToString() });
                List<Crudresponse> Lista = new List<Crudresponse>();
                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_producto_servicio);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;

                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }




        public ResponseGeneric<List<tbl_producto_servicio>> Consultar(Guid entidad)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tbl_dependencia_id", Tipo = "varchar(36)", Valor = entidad.ToString() });
 


                List<tbl_producto_servicio> Lista = new List<tbl_producto_servicio>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_tbl_producto_servicio_list);
                            Lista = conexion.Query<tbl_producto_servicio>().FromSql<tbl_producto_servicio>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;

                            break;
                    }

                }
                #endregion

                return new ResponseGeneric<List<tbl_producto_servicio>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_producto_servicio>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_producto_servicio_contrato>> ConsultarContrato(Guid entidad)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tbl_contrato_id", Tipo = "varchar(36)", Valor = entidad.ToString() });



                List<tbl_producto_servicio_contrato> Lista = new List<tbl_producto_servicio_contrato>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_contrato_producto_contrato);
                            Lista = conexion.Query<tbl_producto_servicio_contrato>().FromSql<tbl_producto_servicio_contrato>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;

                            break;
                    }

                }
                #endregion

                return new ResponseGeneric<List<tbl_producto_servicio_contrato>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_producto_servicio_contrato>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_unidad_medida>> ConsultarUnidadesMedida()
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
               // ListaEnvioParam.Add(new EntidadParametro { Nombre = "tbl_dependencia_id", Tipo = "varchar(36)", Valor = entidad.ToString() });



                List<tbl_unidad_medida> Lista = new List<tbl_unidad_medida>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_tbl_unidad_medida);
                            Lista = conexion.Query<tbl_unidad_medida>().FromSql<tbl_unidad_medida>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;

                            break;
                    }

                }
                #endregion

                return new ResponseGeneric<List<tbl_unidad_medida>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_unidad_medida>>(ex);
            }
        }
        public ResponseGeneric<List<DropDownList>> ConsultarTipo_Prod_Serv()
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();


                List<DropDownList> Lista = new List<DropDownList>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_tipo_producto_servicio);
                            Lista = conexion.Query<DropDownList>().FromSql<DropDownList>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;

                            break;
                    }

                }
                #endregion

                return new ResponseGeneric<List<DropDownList>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_producto_servicio>> ConsultarUnitario(Guid entidad)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "id", Tipo = "varchar(36)", Valor = entidad.ToString() });



                List<tbl_producto_servicio> Lista = new List<tbl_producto_servicio>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_tbl_producto_servicio_unitario);
                            Lista = conexion.Query<tbl_producto_servicio>().FromSql<tbl_producto_servicio>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;

                            break;
                    }

                }
                #endregion

                return new ResponseGeneric<List<tbl_producto_servicio>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_producto_servicio>>(ex);
            }
        }

        public ResponseGeneric<List<DropDownList>> get_productos_ubicacion(Guid idPE, Guid idUbicacion)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tbl_ubicacion_id_", Tipo = "varchar(36)", Valor = idUbicacion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tbl_plan_entrega_id_", Tipo = "varchar(36)", Valor = idPE.ToString() });



                List<DropDownList> Lista = new List<DropDownList>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_productos_ubicacion);
                            Lista = conexion.Query<DropDownList>().FromSql<DropDownList>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;

                            break;
                    }

                }
                #endregion

                return new ResponseGeneric<List<DropDownList>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }
        public ResponseGeneric<List<producto_servicio_pe>> get_productos_pe_ubicacion(Guid idPE, Guid idUbicacion)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_plan_entrega_id", Tipo = "varchar(36)", Valor = idPE.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_ubicacion_id", Tipo = "varchar(36)", Valor = idUbicacion.ToString() });
                



                List<producto_servicio_pe> Lista = new List<producto_servicio_pe>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_producto_servicio_pe_ubi);
                            Lista = conexion.Query<producto_servicio_pe>().FromSql<producto_servicio_pe>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;

                            break;
                    }

                }
                #endregion

                return new ResponseGeneric<List<producto_servicio_pe>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<producto_servicio_pe>>(ex);
            }
        }


    }
}
