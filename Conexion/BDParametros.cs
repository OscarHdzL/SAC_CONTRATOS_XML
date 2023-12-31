﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.SqlServer.Server;
using MySql.Data.MySqlClient;


namespace Conexion
{
    public class BDParametros
    {


        public EntidadModelado<SqlParameter> ParametrosSqlServer(List<EntidadParametro> ListParam, string Query)
        {

            EntidadModelado<SqlParameter> ParametrosSQLServer = new EntidadModelado<SqlParameter>();

            ParametrosSQLServer.Query = "EXEC " + Query + " ";
            int count = 0;
            foreach (var item in ListParam)
            {
                SqlParameter Parametro = new SqlParameter();
                if (item.Valor.Equals("NULL"))
                {
                    if (count == 0)
                    {
                        ParametrosSQLServer.Query += "NULL";
                    }
                    else
                    {
                        ParametrosSQLServer.Query += ",NULL";
                    }
                }
                else
                {
                    ParametrosSQLServer.Query += count == 0 ? "" + "@" + item.Nombre : "," + "@" + item.Nombre;
                    Parametro.ParameterName = item.Nombre;
                    Parametro.SqlDbType = TipoDato(item.Tipo);
                    Parametro.Value = item.Valor;
                    ParametrosSQLServer.ListaParametros.Add(Parametro);
                }
                count++;
            }
            return ParametrosSQLServer;
        }

        public EntidadModelado<MySqlParameter> ParametrosMySQL(List<EntidadParametro> ListParam, string Query)
        {

            EntidadModelado<MySqlParameter> ParametrosmySQL = new EntidadModelado<MySqlParameter>();
            ParametrosmySQL.Query = "CALL " + Query + "(";
            int count = 0;
            foreach (var item in ListParam)
            {
                MySqlParameter Parametro = new MySqlParameter();
                if (item.Valor.Equals("NULL"))
                {
                    if (count == 0)
                    {
                        ParametrosmySQL.Query += "NULL";
                    }
                    else
                    {
                        ParametrosmySQL.Query += ",NULL";
                    }
                }
                else
                {
                    ParametrosmySQL.Query += count == 0 ? "" + "@" + item.Nombre : "," + "@" + item.Nombre;
                    Parametro.ParameterName = item.Nombre;
                    Parametro.MySqlDbType = TipoDatoMySQL(item.Tipo);
                    Parametro.Value = item.Valor;
                    ParametrosmySQL.ListaParametros.Add(Parametro);
                }
                count++;
            }
            ParametrosmySQL.Query += ")";

            return ParametrosmySQL;
        }

        public SqlDbType TipoDato(String Tipo)
        {

            switch (Tipo)
            {
                case "String":
                    return SqlDbType.NChar;
       
                case "Int":
                    return SqlDbType.Int;
                case "Boolean":
                    return SqlDbType.Bit;
                case "Decimal":
                    return SqlDbType.Decimal;
                case "DateTime":
                    return SqlDbType.DateTime;

                default:
                    return SqlDbType.NChar;

            }
        }
        public MySqlDbType TipoDatoMySQL(String Tipo)
        {

            switch (Tipo)
            {
                case "String":
                    return MySqlDbType.VarChar;
                case "Int":
                    return MySqlDbType.Int32;
                case "Guid":
                    return MySqlDbType.Guid;
                case "Boolean":
                    return MySqlDbType.Bit;
                case "Decimal":
                    return MySqlDbType.Decimal;
                case "DateTime":
                    return MySqlDbType.DateTime;
                case "Date":
                    return MySqlDbType.Date;
                case "JSON":
                    return MySqlDbType.JSON;

                default:
                    return MySqlDbType.VarChar;

            }
        }
    }

    public class EntidadModelado<T>
    {

        public EntidadModelado()
        {

            ListaParametros = new List<T>();
        }
        public string Query { get; set; }
        public List<T> ListaParametros { get; set; }
    }

    public class EntidadParametro
    {

        public string Nombre { get; set; }
        public string Valor { get; set; }
        public string Tipo { get; set; }
    }
}
