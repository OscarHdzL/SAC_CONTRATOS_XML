using Conexion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Dependencia;
using Modelos.Modelos.Usuarios;
using Modelos.Response;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;

namespace AccesoDatos_SistemaAdquisiciones
{
    public class tbl_usuarios_acceso_datos : CRUD<tbl_usuario>
    {
        public BDParametros GeneracionParametros = new BDParametros();
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        string StoreProcedure = "sp_get_usuario";

        public ResponseGeneric<List<tbl_usuario>> Consultar(tbl_usuario entidad)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "usuario_email", Tipo = "String", Valor = entidad.usuario });



                List<tbl_usuario> Lista = new List<tbl_usuario>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure);
                            Lista = conexion.Query<tbl_usuario>().FromSql<tbl_usuario>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }

                }
                #endregion

                return new ResponseGeneric<List<tbl_usuario>>(Lista);

            }
            catch (Exception ex)
            {

                return new ResponseGeneric<List<tbl_usuario>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Guardar(tbl_usuario_add entidad)
        {
            try
            {
                string sp_ejecutar = "sp_persona_usuario";

                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();

                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_opt", Tipo = "Int", Valor = "2" });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id_persona", Tipo = "String", Valor = entidad.p_id_persona == null ? "NULL" : entidad.p_id_persona });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_nombre", Tipo = "String", Valor = entidad.p_nombre == null ? "NULL" : entidad.p_nombre });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_ap_paterno", Tipo = "String", Valor = entidad.p_ap_paterno == null ? "NULL" : entidad.p_ap_paterno });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_ap_materno", Tipo = "String", Valor = entidad.p_ap_materno == null ? "NULL" : entidad.p_ap_materno });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_email", Tipo = "String", Valor = entidad.p_email == null ? "NULL" : entidad.p_email });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_rfc", Tipo = "String", Valor = entidad.p_rfc == null ? "NULL" : entidad.p_rfc });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_telefono", Tipo = "String", Valor = entidad.p_telefono == null ? "NULL" : entidad.p_telefono });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_extencion", Tipo = "String", Valor = entidad.p_extencion == null ? "NULL" : entidad.p_extencion });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_dependencia_id", Tipo = "String", Valor = entidad.p_tbl_dependencia_id == null ? "NULL" : entidad.p_tbl_dependencia_id });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_id_usuario", Tipo = "String", Valor = entidad.p_id_usuario == null ? "" : entidad.p_id_usuario });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_usuario", Tipo = "String", Valor = entidad.p_usuario == null ? "NULL" : entidad.p_usuario });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_password", Tipo = "String", Valor = entidad.p_password == null ? "" : entidad.p_password });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_salto", Tipo = "String", Valor = entidad.p_salto == null ? "" : entidad.p_salto });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_activo", Tipo = "String", Valor = entidad.p_activo.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_super_usuario", Tipo = "String", Valor = entidad.p_super_usuario.ToString() });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_estatus_autenticacion_id", Tipo = "String", Valor = entidad.p_tbl_estatus_autenticacion_id == null ? "" : entidad.p_tbl_estatus_autenticacion_id });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_rol_id", Tipo = "String", Valor = entidad.p_tbl_rol_id == null ? "NULL" : entidad.p_tbl_rol_id });
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_tbl_area_id  ", Tipo = "String", Valor = entidad.p_tbl_area_id == null ? "NULL" : entidad.p_tbl_area_id });

                List<Crudresponse> Lista = new List<Crudresponse>();

                #endregion

                using (Contexto conexion = new Contexto())
                {
                    var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, sp_ejecutar);
                    Lista = conexion.Query<Crudresponse>().FromSql<Crudresponse>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                }

                return new ResponseGeneric<List<Crudresponse>>(Lista);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<DropDownList>> ObtenerPassword(string usuario)
        {
            try
            {
                #region Parametros
                List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
                ListaEnvioParam.Add(new EntidadParametro { Nombre = "p_usuario", Tipo = "String", Valor = usuario });

                List<DropDownList> Lista = new List<DropDownList>();

                #endregion

                #region ConexionBD
                using (Contexto conexion = new Contexto())
                {
                    switch (int.Parse(Configuration["TipoBase"].ToString()))
                    {
                        case 2:
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, "sp_get_password_usuario");
                            Lista = conexion.Query<DropDownList>().FromSql<DropDownList>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).ToListAsync().Result;
                            break;
                    }
                }

                //MySqlConnection conexion = new MySqlConnection(_config.GetSection("RutaFisica").GetSection("conn").Value);
                //MySqlCommand cmd = new MySqlCommand("sp_get_password_usuario");
                //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Parameters.Add(new MySqlParameter("p_usuario", usuario));
                //cmd.Connection = conexion;
                //conexion.Open();
                //var reader = cmd.ExecuteReader();
                //reader.Read();
                //var resp = reader[1].ToString();
                //cmd.Connection.Close();
                #endregion

                return new ResponseGeneric<List<DropDownList>>(Lista);

            }
            catch (Exception ex)
            {

                return new ResponseGeneric<List<DropDownList>>(ex);
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
                            var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, "sp_get_instancia_hexa");
                            Lista = conexion.Query<tbl_instancia_contrato_get>().FromSql<tbl_instancia_contrato_get>(resulMySQL.Query, resulMySQL.ListaParametros.ToArray()).FirstOrDefaultAsync().Result;
                            break;
                    }

                }
                #endregion
                return new ResponseGeneric<tbl_instancia_contrato_get>(Lista);

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<tbl_instancia_contrato_get>(ex);
            }
        }

        //public Response Eliminar(TBL_USUARIOS entidad)
        //{
        //    try
        //    {
        //        #region Parametros
        //        List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "tipo", Tipo = "Int", Valor = "4" });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "IDUsuario", Tipo = "Int", Valor = entidad.IDUsuario == null ? "NULL" : entidad.IDUsuario.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "IDInstancia", Tipo = "Int", Valor = entidad.IDInstancia == null ? "NULL" : entidad.IDInstancia.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "IDTipoUsuario", Tipo = "Int", Valor = entidad.IDTipoUsuario == null ? "NULL" : entidad.IDTipoUsuario.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "IDGenero", Tipo = "Int", Valor = entidad.IDGenero == null ? "NULL" : entidad.IDGenero.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "Nombre", Tipo = "String", Valor = entidad.Nombre == null ? "NULL" : entidad.Nombre.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "ApellidoP", Tipo = "String", Valor = entidad.ApellidoP == null ? "NULL" : entidad.ApellidoP.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "ApellidoM", Tipo = "String", Valor = entidad.ApellidoM == null ? "NULL" : entidad.ApellidoM.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "Nacionalidad", Tipo = "String", Valor = entidad.Nacionalidad == null ? "NULL" : entidad.Nacionalidad.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "AnioNacimiento", Tipo = "Int", Valor = entidad.AnioNacimiento == null ? "NULL" : entidad.AnioNacimiento.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "Calle", Tipo = "String", Valor = entidad.Calle == null ? "NULL" : entidad.Calle.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "Numero", Tipo = "String", Valor = entidad.Numero == null ? "NULL" : entidad.Numero.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "Municipio", Tipo = "String", Valor = entidad.Municipio == null ? "NULL" : entidad.Municipio.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "CodigoPostal", Tipo = "Int", Valor = entidad.CodigoPostal == null ? "NULL" : entidad.CodigoPostal.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "Telefono", Tipo = "String", Valor = entidad.Telefono == null ? "NULL" : entidad.Telefono.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "Email", Tipo = "String", Valor = entidad.Email == null ? "NULL" : entidad.Email.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "Contrasenia", Tipo = "String", Valor = entidad.Contrasenia == null ? "NULL" : entidad.Contrasenia.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "Activo", Tipo = "Boolean", Valor = entidad.Activo == null ? "NULL" : entidad.Activo == false ? "0" : "1" });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "IDDependencia", Tipo = "Int", Valor = entidad.IDDependencia == null ? "NULL" : entidad.IDDependencia.ToString() });
        //        int result = 0;

        //        #endregion

        //        #region ConexionBD
        //        using (Contexto conexion = new Contexto())
        //        {
        //            switch (int.Parse(Configuration["TipoBase"].ToString()))
        //            {
        //                case 1:
        //                    var resultSQL = GeneracionParametros.ParametrosSqlServer(ListaEnvioParam, StoreProcedure);
        //                    result = conexion.Database.ExecuteSqlCommand(resultSQL.Query, resultSQL.ListaParametros.ToArray());
        //                    break;
        //                case 2:
        //                    var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure);
        //                    result = conexion.Database.ExecuteSqlCommand(resulMySQL.Query, resulMySQL.ListaParametros.ToArray());
        //                    break;
        //            }

        //        }
        //        #endregion
        //        #region Resultado
        //        if (result == 1)
        //        {
        //            return new Response();
        //        }
        //        else
        //        {
        //            return new Response("Error Peticion");
        //        }
        //        #endregion
        //    }
        //    catch (Exception ex)
        //    {

        //        return new Response(ex);
        //    }
        //}

        //public Response Guardar(TBL_USUARIOS entidad)
        //{
        //    try
        //    {
        //        #region Parametros
        //        List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "Tipo", Tipo = "Int", Valor = "2" });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "IDUsuario", Tipo = "Int", Valor = entidad.IDUsuario == null ? "NULL" : entidad.IDUsuario.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "IDInstancia", Tipo = "Int", Valor = entidad.IDInstancia == null ? "NULL" : entidad.IDInstancia.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "IDTipoUsuario", Tipo = "Int", Valor = entidad.IDTipoUsuario == null ? "NULL" : entidad.IDTipoUsuario.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "IDGenero", Tipo = "Int", Valor = entidad.IDGenero == null ? "NULL" : entidad.IDGenero.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "Nombre", Tipo = "String", Valor = entidad.Nombre == null ? "NULL" : entidad.Nombre.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "ApellidoP", Tipo = "String", Valor = entidad.ApellidoP == null ? "NULL" : entidad.ApellidoP.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "ApellidoM", Tipo = "String", Valor = entidad.ApellidoM == null ? "NULL" : entidad.ApellidoM.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "Nacionalidad", Tipo = "String", Valor = entidad.Nacionalidad == null ? "NULL" : entidad.Nacionalidad.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "AnioNacimiento", Tipo = "Int", Valor = entidad.AnioNacimiento == null ? "NULL" : entidad.AnioNacimiento.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "Calle", Tipo = "String", Valor = entidad.Calle == null ? "NULL" : entidad.Calle.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "Numero", Tipo = "String", Valor = entidad.Numero == null ? "NULL" : entidad.Numero.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "Municipio", Tipo = "String", Valor = entidad.Municipio == null ? "NULL" : entidad.Municipio.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "CodigoPostal", Tipo = "Int", Valor = entidad.CodigoPostal == null ? "NULL" : entidad.CodigoPostal.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "Telefono", Tipo = "String", Valor = entidad.Telefono == null ? "NULL" : entidad.Telefono.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "Email", Tipo = "String", Valor = entidad.Email == null ? "NULL" : entidad.Email.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "Contrasenia", Tipo = "String", Valor = entidad.Contrasenia == null ? "NULL" : entidad.Contrasenia.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "Activo", Tipo = "Boolean", Valor = entidad.Activo == null ? "NULL" : entidad.Activo == false ? "0" : "1" });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "IDDependencia", Tipo = "Int", Valor = entidad.IDDependencia == null ? "NULL" : entidad.IDDependencia.ToString() });
        //        int result = 0;

        //        #endregion

        //        #region ConexionBD
        //        using (Contexto conexion = new Contexto())
        //        {
        //            switch (int.Parse(Configuration["TipoBase"].ToString()))
        //            {
        //                case 1:
        //                    var resultSQL = GeneracionParametros.ParametrosSqlServer(ListaEnvioParam, StoreProcedure);
        //                    result = conexion.Database.ExecuteSqlCommand(resultSQL.Query, resultSQL.ListaParametros.ToArray());
        //                    break;
        //                case 2:
        //                    var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure);
        //                    result = conexion.Database.ExecuteSqlCommand(resulMySQL.Query, resulMySQL.ListaParametros.ToArray());
        //                    break;
        //            }

        //        }
        //        #endregion
        //        #region Resultado
        //        if (result == 1)
        //        {
        //            return new Response();
        //        }
        //        else
        //        {
        //            return new Response("Error Peticion");
        //        }
        //        #endregion
        //    }
        //    catch (Exception ex)
        //    {

        //        return new Response(ex);
        //    }
        //}

        //public Response Modificar(TBL_USUARIOS entidad)
        //{
        //    try
        //    {
        //        #region Parametros
        //        List<EntidadParametro> ListaEnvioParam = new List<EntidadParametro>();
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "Tipo", Tipo = "Int", Valor = "3" });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "IDUsuario", Tipo = "Int", Valor = entidad.IDUsuario == null ? "NULL" : entidad.IDUsuario.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "IDInstancia", Tipo = "Int", Valor = entidad.IDInstancia == null ? "NULL" : entidad.IDInstancia.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "IDTipoUsuario", Tipo = "Int", Valor = entidad.IDTipoUsuario == null ? "NULL" : entidad.IDTipoUsuario.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "IDGenero", Tipo = "Int", Valor = entidad.IDGenero == null ? "NULL" : entidad.IDGenero.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "Nombre", Tipo = "String", Valor = entidad.Nombre == null ? "NULL" : entidad.Nombre.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "ApellidoP", Tipo = "String", Valor = entidad.ApellidoP == null ? "NULL" : entidad.ApellidoP.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "ApellidoM", Tipo = "String", Valor = entidad.ApellidoM == null ? "NULL" : entidad.ApellidoM.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "Nacionalidad", Tipo = "String", Valor = entidad.Nacionalidad == null ? "NULL" : entidad.Nacionalidad.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "AnioNacimiento", Tipo = "Int", Valor = entidad.AnioNacimiento == null ? "NULL" : entidad.AnioNacimiento.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "Calle", Tipo = "String", Valor = entidad.Calle == null ? "NULL" : entidad.Calle.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "Numero", Tipo = "String", Valor = entidad.Numero == null ? "NULL" : entidad.Numero.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "Municipio", Tipo = "String", Valor = entidad.Municipio == null ? "NULL" : entidad.Municipio.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "CodigoPostal", Tipo = "Int", Valor = entidad.CodigoPostal == null ? "NULL" : entidad.CodigoPostal.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "Telefono", Tipo = "String", Valor = entidad.Telefono == null ? "NULL" : entidad.Telefono.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "Email", Tipo = "String", Valor = entidad.Email == null ? "NULL" : entidad.Email.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "Contrasenia", Tipo = "String", Valor = entidad.Contrasenia == null ? "NULL" : entidad.Contrasenia.ToString() });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "Activo", Tipo = "Int", Valor = entidad.Activo == null ? "NULL" : entidad.Activo == false ? "0" : "1" });
        //        ListaEnvioParam.Add(new EntidadParametro { Nombre = "IDDependencia", Tipo = "Int", Valor = entidad.IDDependencia == null ? "NULL" : entidad.IDDependencia.ToString() });
        //        int result = 0;

        //        #endregion

        //        #region ConexionBD
        //        using (Contexto conexion = new Contexto())
        //        {
        //            switch (int.Parse(Configuration["TipoBase"].ToString()))
        //            {
        //                case 1:
        //                    var resultSQL = GeneracionParametros.ParametrosSqlServer(ListaEnvioParam, StoreProcedure);
        //                    result = conexion.Database.ExecuteSqlCommand(resultSQL.Query, resultSQL.ListaParametros.ToArray());
        //                    break;
        //                case 2:
        //                    var resulMySQL = GeneracionParametros.ParametrosMySQL(ListaEnvioParam, StoreProcedure);
        //                    result = conexion.Database.ExecuteSqlCommand(resulMySQL.Query, resulMySQL.ListaParametros.ToArray());
        //                    break;
        //            }

        //        }
        //        #endregion
        //        #region Resultado
        //        if (result == 1)
        //        {
        //            return new Response();
        //        }
        //        else
        //        {
        //            return new Response("Error Peticion");
        //        }
        //        #endregion

        //    }
        //    catch (Exception ex)
        //    {

        //        return new Response(ex);
        //    }
        //}
    }
}
