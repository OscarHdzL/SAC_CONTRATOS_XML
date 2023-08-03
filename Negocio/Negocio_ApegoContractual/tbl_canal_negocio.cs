using AccesoDatos;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Response;
using System;
using Utilidades.Log4Net;
using System.Collections.Generic;
using System.Text;

namespace Solucion_Negocio
{
    public class tbl_canal_negocio : crud_canal
    {
        private tbl_canal_acceso_datos _Canal = new tbl_canal_acceso_datos();
        private readonly ILoggerManager _logger;


        public ResponseGeneric<List<tbl_canal>> Consultar()
        {
            try
            {
                return _Canal.Consultar();
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_canal>>(ex);
            }
        }
    }
}
