using Conexion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.ServidoresPublicos;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.IO;
using Utilidades.Log4Net;

namespace AccesoDatos
{
    public class tbl_servidorespublicos_acceso_datos : crud_servidorespublicos
    {
        public BDParametros GeneracionParametros = new BDParametros();
        private readonly ILoggerManager _logger;
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string StoreProcedure = "sp_get_servpubdependencia";
        string StoreProcedureRolApego = "sp_get_servpubdependenciarolapego";
        string StoreProcedureRolPEentrega = "sp_get_servpubdependenciarolpentrega";
        string StoreProcedureRolPMonitoreo = "sp_get_servpubdependenciarolpmonitoreo";
        string StoreProcedureServPub = "sp_get_servidorPublico";
        string sp_get_ejecutores = "sp_get_ejecutores";
        string sp_get_responsablescontrato = "sp_get_responsablescontrato";
        string sp_get_responsablesUbicacion = "sp_get_responsablesUbicacion";
        string sp_get_tbl_estado = "sp_get_tbl_estado";
        string sp_get_tbl_ciudad_estado = "sp_get_tbl_ciudad_estado";
        string sp_get_responsable_pe_contrato = "sp_get_responsable_pe_contrato";
        string sp_get_vs_servidor_publico = "sp_get_vs_servidor_publico";


        public tbl_servidorespublicos_acceso_datos()
        {
            _logger = new LoggerManager();
        }

        public ResponseGeneric<List<sp_get_vs_servidor_publico_contrato>> get_vs_servidor_publico(sp_get_vs_servidor_publico_input obj)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "id", Tipo = "String", Valor = obj.id.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tipo", Tipo = "String", Valor = obj.tipo.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "Escontrato", Tipo = "String", Valor = Convert.ToInt32( obj.Escontrato ).ToString() });

                List<sp_get_vs_servidor_publico_contrato> Lista = new List<sp_get_vs_servidor_publico_contrato>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_vs_servidor_publico);
                            Lista = conexion.Query<sp_get_vs_servidor_publico_contrato>().FromSql<sp_get_vs_servidor_publico_contrato>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<sp_get_vs_servidor_publico_contrato>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<sp_get_vs_servidor_publico_contrato>>(ex);
            }
        }


        public ResponseGeneric<List<sp_get_vs_servidor_publico_>> get_vs_servidor_publico_dep(sp_get_vs_servidor_publico_input obj)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "id", Tipo = "String", Valor = obj.id.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tipo", Tipo = "String", Valor = obj.tipo.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "Escontrato", Tipo = "String", Valor = Convert.ToInt32(obj.Escontrato).ToString() });

                List<sp_get_vs_servidor_publico_> Lista = new List<sp_get_vs_servidor_publico_>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_vs_servidor_publico);
                            Lista = conexion.Query<sp_get_vs_servidor_publico_>().FromSql<sp_get_vs_servidor_publico_>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<sp_get_vs_servidor_publico_>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<sp_get_vs_servidor_publico_>>(ex);
            }
        }


        public ResponseGeneric<List<sp_get_vs_servidor_publico_contrato_c>> get_vs_servidor_publico_por_contrato(sp_get_vs_servidor_publico_input obj)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "id", Tipo = "String", Valor = obj.id.ToString() });

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tipo", Tipo = "String", Valor = obj.tipo.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "Escontrato", Tipo = "String", Valor = Convert.ToInt32(obj.Escontrato).ToString() });

                List<sp_get_vs_servidor_publico_contrato_c> Lista = new List<sp_get_vs_servidor_publico_contrato_c>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_vs_servidor_publico);
                            Lista = conexion.Query<sp_get_vs_servidor_publico_contrato_c>().FromSql<sp_get_vs_servidor_publico_contrato_c>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<sp_get_vs_servidor_publico_contrato_c>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<sp_get_vs_servidor_publico_contrato_c>>(ex);
            }
        }





        public ResponseGeneric<List<DropDownList>> get_responsable_pe_contrato(Guid p_tbl_contrato_id)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_contrato_id", Tipo = "String", Valor = p_tbl_contrato_id.ToString() });


                List<DropDownList> Lista = new List<DropDownList>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_responsable_pe_contrato);
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



        //get_ResponsablesUbicaciones
        public ResponseGeneric<List<DropDownList>> get_tbl_estado()
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
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_tbl_estado);
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
        public ResponseGeneric<List<DropDownList>> get_tbl_estado_ciudad(Guid tbl_estado_id)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tbl_estado_id", Tipo = "String", Valor = tbl_estado_id.ToString() });


                List<DropDownList> Lista = new List<DropDownList>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_tbl_ciudad_estado);
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


        public ResponseGeneric<List<DropDownList>> get_ejecutores_dependencia(Guid dependencia)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tbl_dependencia_id", Tipo = "String", Valor = dependencia.ToString() });

                List<DropDownList> Lista = new List<DropDownList>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_ejecutores);
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
        public ResponseGeneric<List<DropDownList>> get_responsablescontrato(Guid dependencia)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tbl_dependencia_id", Tipo = "String", Valor = dependencia.ToString() });

                List<DropDownList> Lista = new List<DropDownList>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_responsablescontrato);
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
        public ResponseGeneric<List<DropDownList>> get_ResponsablesUbicaciones(Guid dependencia)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "tbl_dependencia_id", Tipo = "String", Valor = dependencia.ToString() });

                List<DropDownList> Lista = new List<DropDownList>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_responsablesUbicacion);
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


        public ResponseGeneric<List<tbl_servidor_publico>> Consultar(String dependencia)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "servidor_dependencia", Tipo = "String", Valor = dependencia.ToString() });

                List<tbl_servidor_publico> Lista = new List<tbl_servidor_publico>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure);
                            Lista = conexion.Query<tbl_servidor_publico>().FromSql<tbl_servidor_publico>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_servidor_publico>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_servidor_publico>>(ex);
            }
        }



        public ResponseGeneric<List<DropDownList>> FillDrop(String dependencia)
        {
            throw new NotImplementedException();
        }

        public ResponseGeneric<List<tbl_servidor_publico>> ConsultarByRol(String dependencia)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "servidor_dependencia", Tipo = "String", Valor = dependencia.ToString() });

                List<tbl_servidor_publico> Lista = new List<tbl_servidor_publico>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedureRolApego);
                            Lista = conexion.Query<tbl_servidor_publico>().FromSql<tbl_servidor_publico>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_servidor_publico>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_servidor_publico>>(ex);
            }
        }
     

            public ResponseGeneric<List<tbl_servidor_publico>> ConsultarByRolEjecutorPEntrega(string Dependencia)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "dependencia", Tipo = "String", Valor = Dependencia.ToString() });

                List<tbl_servidor_publico> Lista = new List<tbl_servidor_publico>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedureRolPEentrega);
                            Lista = conexion.Query<tbl_servidor_publico>().FromSql<tbl_servidor_publico>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_servidor_publico>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_servidor_publico>>(ex);
            }
        }


        public ResponseGeneric<List<tbl_servidor_publico>> ConsultarByRolEjecutorPMonitoreo(string Dependencia)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "dependencia", Tipo = "String", Valor = Dependencia.ToString() });

                List<tbl_servidor_publico> Lista = new List<tbl_servidor_publico>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedureRolPMonitoreo);
                            Lista = conexion.Query<tbl_servidor_publico>().FromSql<tbl_servidor_publico>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<tbl_servidor_publico>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_servidor_publico>>(ex);
            }
        }

        public ResponseGeneric<tbl_servidor_publico> ConsultarServidor(string idServidor)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "servidor", Tipo = "String", Valor = idServidor.ToString() });

                tbl_servidor_publico Lista = new tbl_servidor_publico();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedureServPub);
                            Lista = conexion.Query<tbl_servidor_publico>().FromSql<tbl_servidor_publico>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<tbl_servidor_publico>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<tbl_servidor_publico>(ex);
            }
        }


    }
}
