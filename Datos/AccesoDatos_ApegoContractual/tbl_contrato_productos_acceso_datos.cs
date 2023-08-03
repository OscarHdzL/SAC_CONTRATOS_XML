using Conexion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.ResponsablesApego;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.IO;
using Utilidades.Log4Net;

namespace AccesoDatos
{
    public class tbl_contrato_productos_acceso_datos : crud_contratoproductos
    {
        public BDParametros GeneracionParametros = new BDParametros();
        private readonly ILoggerManager _logger;
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string sp_get_productos_contrato_dropdown = "sp_get_productos_contrato_dropdown";
        string sp_tbl_contrato_producto = "sp_tbl_contrato_producto";
        string sp_get_productosdependencia = "sp_get_productosdependencia";
        string sp_contrato_producto = "sp_contrato_producto"; 
        string sp_tbl_contrato_producto_id = "sp_tbl_contrato_producto_id";


        public tbl_contrato_productos_acceso_datos()
        {
            _logger = new LoggerManager();
        }

        public ResponseGeneric<List<DropDownList>> FillDrop(String contrato)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "contrato", Tipo = "String", Valor = contrato});

                List<DropDownList> Lista = new List<DropDownList>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_productos_contrato_dropdown);
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

        public ResponseGeneric<List<tbl_contrato_producto_list>> GetListContrato(Guid contrato)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tbl_contrato_id", Tipo = "String", Valor = contrato.ToString() });

                List<tbl_contrato_producto_list> Lista = new List<tbl_contrato_producto_list>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_tbl_contrato_producto);
                            Lista = conexion.Query<tbl_contrato_producto_list>().FromSql<tbl_contrato_producto_list>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_contrato_producto_list>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("GetListContrato", ex);
                return new ResponseGeneric<List<tbl_contrato_producto_list>>(ex);
            }
        }


        public ResponseGeneric<List<tbl_contrato_producto_list>> Getunitario(Guid tbl_contrato_producto)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tbl_contrato_producto", Tipo = "String", Valor = tbl_contrato_producto.ToString() });

                List<tbl_contrato_producto_list> Lista = new List<tbl_contrato_producto_list>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_tbl_contrato_producto_id);
                            Lista = conexion.Query<tbl_contrato_producto_list>().FromSql<tbl_contrato_producto_list>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_contrato_producto_list>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("GetUnitario", ex);
                return new ResponseGeneric<List<tbl_contrato_producto_list>>(ex);
            }
        }



        public ResponseGeneric<List<DropDownList>> GetListDependencia(Guid tbl_dependencia_id)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tbl_dependencia_id", Tipo = "String", Valor = tbl_dependencia_id.ToString() });

                List<DropDownList> Lista = new List<DropDownList>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_productosdependencia);
                            Lista = conexion.Query<DropDownList>().FromSql<DropDownList>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<DropDownList>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("GetListDependencia", ex);
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Update(tbl_contrato_producto_add obj)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor =  obj.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = obj.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_id", Tipo = "String", Valor = obj.p_tbl_contrato_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_producto_servicio_id", Tipo = "String", Valor = obj.p_tbl_producto_servicio_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_cantidad_minima", Tipo = "String", Valor = obj.p_cantidad_minima.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_cantidad_maxima", Tipo = "String", Valor = obj.p_cantidad_maxima.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_minimo", Tipo = "String", Valor = obj.p_monto_minimo.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto_maximo", Tipo = "String", Valor = obj.p_monto_maximo.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_descripcion", Tipo = "String", Valor = obj.p_descripcion == null ? "" : obj.p_descripcion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_estatus", Tipo = "String", Valor = obj.p_estatus.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_unitario", Tipo = "String", Valor = obj.p_unitario.ToString() });




                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_contrato_producto);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Update", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }




    }
}
