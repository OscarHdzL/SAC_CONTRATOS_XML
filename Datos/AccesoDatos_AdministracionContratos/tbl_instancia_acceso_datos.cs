using Conexion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Dependencia;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.IO;
using Utilidades.Log4Net;

namespace AccesoDatos_AdminContratos
{
    public class tbl_instancia_acceso_datos
    {
        public BDParametros GeneracionParametros = new BDParametros();
        private readonly ILoggerManager _logger;
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string sp_get_inst = "sp_get_instancia_dropdown";
        string sp_instancia = "sp_instancia";
        string sp_get_instancia = "sp_get_instancia_hexa";

        public tbl_instancia_acceso_datos()
        {
            _logger = new LoggerManager();
        }

        public ResponseGeneric<List<DropDownList>> FillDropC()
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
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_inst);
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


        public ResponseGeneric<List<Crudresponse>> Eject (tbl_instancia_contrato ins)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "int", Valor = ins.opt.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id", Tipo = "String", Valor = ins.id.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_nombre", Tipo = "String", Valor = ins.nombre.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_nombre_corto", Tipo = "String", Valor = ins.nombre_corto.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_copyright", Tipo = "String", Valor = ins.copyright.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_gob_fed", Tipo = "String", Valor = ins.gob_fed.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_token_min", Tipo = "String", Valor = ins.token_logo_mini.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_token_ini", Tipo = "String", Valor = ins.token_logo_inicio.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_hex_col_header", Tipo = "String", Valor = ins.hex_col_header.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_hex_col_sidebar", Tipo = "String", Valor = ins.hex_col_sidebar.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_hex_background", Tipo = "String", Valor = ins.hex_background.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_hex_textcolor", Tipo = "String", Valor = ins.hex_textcolor.ToString() });


                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_instancia);
                            Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<List<Crudresponse>>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("ejet", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<tbl_instancia_contrato_get> Get(String Instancia)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "instancia", Tipo = "String", Valor = Instancia });

                tbl_instancia_contrato_get Lista = new tbl_instancia_contrato_get();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_get_instancia);
                            Lista = conexion.Query<tbl_instancia_contrato_get>().FromSql<tbl_instancia_contrato_get>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<tbl_instancia_contrato_get>(Lista);

            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<tbl_instancia_contrato_get>(ex);
            }
        }
    }
}
