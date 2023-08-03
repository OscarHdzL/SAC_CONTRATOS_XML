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
    public class tbl_obligacion_acceso_datos : crud_tbl_obligacion
    {
        public BDParametros GeneracionParametros = new BDParametros();
        private readonly ILoggerManager _logger;
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string StoreProcedure = "sp_get_obligacionesContrato";
        string StoreProcedurePeriodos = "sp_getperiodo";
        string StoreProcedureAddPeriodos = "sp_tipo_periodo";
        string StoreProcedureAreasResp = "sp_getarea_obligacion";
        string StoreProcedureServiResp = "sp_getresponsable_obligacion";
        string StoreProcedureAdd = "sp_obligacion";
        string StoreProcedureTipoOblig = "sp_get_tiposobligacion";
        string StoreProcedureAddObligaciones = "sp_tipo_obligacion";
        string sp_obligacion_area = "sp_obligacion_area";
        string sp_obligacion_responsable = "sp_obligacion_responsable";
        string sp_obligacion_link = "sp_obligacion_link";
        string sp_tipo_aplicacion = "sp_tipo_aplicacion";
        string sp_get_obligacion_id = "sp_get_obligacion_id";
        string StoreProcedureVerificarOblig = "sp_validar_obligacion_pe";

        string StoreProcedureObligacionesxProducto = "sp_get_obligacionesContrato_x_producto";
        string StoreProcedureReporteSanciones = "sp_reporte_sanciones";

        public tbl_obligacion_acceso_datos()
        {
            _logger = new LoggerManager();
        }

        public ResponseGeneric<List<tbl_obligacion>> Consultar(String entidad)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tbl_contrato_id", Tipo = "String", Valor = entidad.ToString() });
 


                List<tbl_obligacion> Lista = new List<tbl_obligacion>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure);
                            Lista = conexion.Query<tbl_obligacion>().FromSql<tbl_obligacion>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;

                            break;
                    }

                }
                #endregion

                return new ResponseGeneric<List<tbl_obligacion>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_obligacion>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_obligacion_unitario>> ConsultarId(Guid tbl_obligacion_id)
        {
            try
            {
                #region Parametros
                
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tbl_obligacion_id", Tipo = "String", Valor = tbl_obligacion_id.ToString() });



                List<tbl_obligacion_unitario> Lista = new List<tbl_obligacion_unitario>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_obligacion_id);
                            Lista = conexion.Query<tbl_obligacion_unitario>().FromSql<tbl_obligacion_unitario>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;

                            break;
                    }

                }
                #endregion

                return new ResponseGeneric<List<tbl_obligacion_unitario>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_obligacion_unitario>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_obligacion_detalle>> ConsultarDetalle(String entidad)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tbl_contrato_id", Tipo = "String", Valor = entidad.ToString() });
                List<tbl_obligacion> Lista = new List<tbl_obligacion>();
                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure);
                            Lista = conexion.Query<tbl_obligacion>().FromSql<tbl_obligacion>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;

                            break;
                    }

                }
                #endregion
                List<tbl_obligacion_detalle> ListaDet = new List<tbl_obligacion_detalle>();
                List<tbl_obligacion> Result = (List<tbl_obligacion>)Lista;
                foreach (tbl_obligacion item in Lista) {
                    tbl_obligacion_detalle detalle = new tbl_obligacion_detalle();
                    detalle.Obligacion = item;
                    tbl_obligacion_acceso_datos _Obligaciones = new tbl_obligacion_acceso_datos();
                    detalle.Areas = _Obligaciones.GetAreasResponsables(item.tbl_obligacion_id);
                    detalle.Responsables = _Obligaciones.GetServidoresResponsables(item.tbl_obligacion_id);
                    ListaDet.Add(detalle);
                }
                return new ResponseGeneric<List<tbl_obligacion_detalle>>(ListaDet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_obligacion_detalle>>(ex);
            }
        }


        public ResponseGeneric<List<tbl_obligacion_producto>> ConsultarObligacionProductoDetalle(String contrato, String producto)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_id", Tipo = "String", Valor = contrato.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_producto_id", Tipo = "String", Valor = producto.ToString() });

                List<tbl_obligacion_producto> Lista = new List<tbl_obligacion_producto>();
                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedureObligacionesxProducto);
                            Lista = conexion.Query<tbl_obligacion_producto>().FromSql<tbl_obligacion_producto>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;

                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_obligacion_producto>>(Lista);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_obligacion_producto>>(ex);
            }
        }

        public ResponseGeneric<List<ReporteSancionesConsulta>> ReporteSanciones(String contrato, String fecha_filtro)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_contrato_id", Tipo = "String", Valor = contrato });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_fecha_filtro", Tipo = "String", Valor = fecha_filtro });
                List<ReporteSancionesConsulta> Lista = new List<ReporteSancionesConsulta>();
                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedureReporteSanciones);
                            Lista = conexion.Query<ReporteSancionesConsulta>().FromSql<ReporteSancionesConsulta>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;

                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<ReporteSancionesConsulta>>(Lista);
            }
            catch (Exception ex)
            {
                _logger.LogError("ReporteSanciones", ex);
                return new ResponseGeneric<List<ReporteSancionesConsulta>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_periodo>> GetPeriodo()
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                List<tbl_periodo> Lista = new List<tbl_periodo>();
                #endregion
                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedurePeriodos);
                            Lista = conexion.Query<tbl_periodo>().FromSql<tbl_periodo>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }
                }
                #endregion
                return new ResponseGeneric<List<tbl_periodo>>(Lista);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_periodo>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Add_tipo_periodo(tbl_tipo_periodo_add tbl_tipo_periodo_add)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = tbl_tipo_periodo_add.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = tbl_tipo_periodo_add.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_periodo", Tipo = "String", Valor = tbl_tipo_periodo_add.p_periodo.ToString() });
                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedureAddPeriodos);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Add_tipo_periodo", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Delete_tipo_periodo(tbl_tipo_periodo_add tbl_tipo_periodo_add)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = tbl_tipo_periodo_add.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = tbl_tipo_periodo_add.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_periodo", Tipo = "String", Valor = tbl_tipo_periodo_add.p_periodo == null ? "" : tbl_tipo_periodo_add.p_periodo.ToString() });
                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedureAddPeriodos);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Delete_tipo_periodo", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_tipo_obligacion>> GetTipooblig()
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                List<tbl_tipo_obligacion> Lista = new List<tbl_tipo_obligacion>();
                #endregion
                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedureTipoOblig);
                            Lista = conexion.Query<tbl_tipo_obligacion>().FromSql<tbl_tipo_obligacion>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }
                }
                #endregion
                return new ResponseGeneric<List<tbl_tipo_obligacion>>(Lista);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_tipo_obligacion>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Add_tipo_obligacion(tbl_tipo_obligacion_add tbl_tipo_obligacion_add)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = tbl_tipo_obligacion_add.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = tbl_tipo_obligacion_add.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_obligacion", Tipo = "String", Valor = tbl_tipo_obligacion_add.p_tipo_obligacion.ToString() });
                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedureAddObligaciones);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Add_tipo_obligacion", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Delete_tipo_obligacion(tbl_tipo_obligacion_add tbl_tipo_obligacion_add)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "String", Valor = tbl_tipo_obligacion_add.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = tbl_tipo_obligacion_add.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tipo_obligacion", Tipo = "String", Valor = tbl_tipo_obligacion_add.p_tipo_obligacion == null ? "" : tbl_tipo_obligacion_add.p_tipo_obligacion.ToString() });
                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedureAddObligaciones);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Delete_tipo_obligacion", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<verificar_oblig> VerificarObligacion(Guid idObligacion)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "obligacion", Tipo = "String", Valor = idObligacion.ToString() });

                verificar_oblig valor = new verificar_oblig();
                #endregion
                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedureVerificarOblig);
                            valor = conexion.Query<verificar_oblig>().FromSql<verificar_oblig>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync().Result;
                            break;
                    }
                }
                #endregion
                return new ResponseGeneric<verificar_oblig>(valor);
            }
            catch (Exception ex)
            {
                _logger.LogError("", ex);
                return new ResponseGeneric<verificar_oblig>(ex);
            }
        }

        public List<DropDownList> GetAreasResponsables(String idObligacion)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "idObligacion", Tipo = "String", Valor = idObligacion.ToString() });


                List<DropDownList> Lista = new List<DropDownList>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedureAreasResp);
                            Lista = conexion.Query<DropDownList>().FromSql<DropDownList>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;

                            break;
                    }

                }
                #endregion

                return Lista;                                                          

            }
            catch (Exception ex)
            {
                _logger.LogError("", ex);
                return new List<DropDownList>();
            }
        }
        public List<DropDownList> GetServidoresResponsables(String idObligacion)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "idObligacion", Tipo = "String", Valor = idObligacion });
                List<DropDownList> Lista = new List<DropDownList>();
                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedureServiResp);
                            Lista = conexion.Query<DropDownList>().FromSql<DropDownList>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;

                            break;
                    }

                }
                #endregion
                return new List<DropDownList>(Lista);
            }
            catch (Exception ex)
            {
                _logger.LogError("", ex);
                return new List<DropDownList>();
            }
        }
        public ResponseGeneric<List<Crudresponse>> Add(tbl_obligacion_input input)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "int", Valor = input.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = input.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_clausula", Tipo = "String ", Valor = input.p_clausula.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_nivel_servicio", Tipo = "int", Valor = input.p_nivel_servicio.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_forma_aplicacion", Tipo = "String", Valor = input.p_forma_aplicacion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_comentarios", Tipo = "String", Valor = input.p_comentarios.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_obligacion", Tipo = "String", Valor = input.p_obligacion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto", Tipo = "double", Valor = input.p_monto.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_porcentaje", Tipo = "double", Valor = input.p_porcentaje.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_prioridad_id", Tipo = "String", Valor = input.p_tbl_tipo_prioridad_id!=null? input.p_tbl_tipo_prioridad_id.ToString():"NULL" });

                List<Crudresponse> Lista = new List<Crudresponse>();
                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedureAdd);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;

                            break;
                    }

                }
                #endregion

                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> AddObligacionAreas(Guid obligacion, String Areas, int opcion)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "Int", Valor = opcion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id_obligacion_id", Tipo = "String", Valor = obligacion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_str_areas", Tipo = "String", Valor = Areas });


                List<Crudresponse> Lista = new List<Crudresponse>();
                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_obligacion_area);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;

                            break;
                    }

                }
                #endregion

                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> AddObligacioResponsables(Guid obligacion, String Responsables, int opcion)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "Int", Valor = opcion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id_obligacion_id", Tipo = "String", Valor = obligacion.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_str_areas", Tipo = "String", Valor = Responsables });

                List<Crudresponse> Lista = new List<Crudresponse>();
                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_obligacion_responsable);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;

                            break;
                    }

                }
                #endregion

                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> AddObligacioLink(tbl_link_obligacion_input tbl_link_obligacion_input)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "Int", Valor = tbl_link_obligacion_input.p_opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = tbl_link_obligacion_input.p_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_obligacion_id", Tipo = "String", Valor = tbl_link_obligacion_input.p_tbl_obligacion_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_id", Tipo = "String", Valor = tbl_link_obligacion_input.p_tbl_contrato_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_obligacion_id", Tipo = "String", Valor = tbl_link_obligacion_input.p_tbl_tipo_obligacion_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_sancion_obligacion_id", Tipo = "String", Valor = tbl_link_obligacion_input.p_tbl_sancion_obligacion_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_periodo_id", Tipo = "String", Valor = tbl_link_obligacion_input.p_tbl_periodo_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_producto_servicio_id", Tipo = "String", Valor = tbl_link_obligacion_input.p_tbl_producto_servicio_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_estatus", Tipo = "String", Valor = tbl_link_obligacion_input.p_estatus.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_tipo_aplicacion_id", Tipo = "String", Valor = tbl_link_obligacion_input.p_tbl_tipo_aplicacion_id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_str_areas", Tipo = "String", Valor = tbl_link_obligacion_input.p_str_areas == null ? "" : tbl_link_obligacion_input.p_str_areas });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_str_responsables", Tipo = "String", Valor = tbl_link_obligacion_input.p_str_responsables == null ? "" : tbl_link_obligacion_input.p_str_responsables });



                List<Crudresponse> Lista = new List<Crudresponse>();
                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_obligacion_link);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;

                            break;
                    }

                }
                #endregion

                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Delete(Guid input)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "int", Valor = "4" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = input.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_clausula", Tipo = "String ", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_nivel_servicio", Tipo = "int", Valor = "0" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_forma_aplicacion", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_comentarios", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_obligacion", Tipo = "String", Valor = "" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_monto", Tipo = "double", Valor = "0" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_porcentaje", Tipo = "int", Valor = "0" });


                List<Crudresponse> Lista = new List<Crudresponse>();
                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedureAdd);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;

                            break;
                    }

                }
                #endregion

                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_tipo_aplicacion>> Get_tipo_aplicacion()
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                List<tbl_tipo_aplicacion> Lista = new List<tbl_tipo_aplicacion>();
                #endregion
                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_tipo_aplicacion);
                            Lista = conexion.Query<tbl_tipo_aplicacion>().FromSql<tbl_tipo_aplicacion>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }
                }
                #endregion
                return new ResponseGeneric<List<tbl_tipo_aplicacion>>(Lista);
            }
            catch (Exception ex)
            {
                _logger.LogError("get", ex);
                return new ResponseGeneric<List<tbl_tipo_aplicacion>>(ex);
            }
        }
    }
}
