using AccesoDatos;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solucion_Negocio
{
    public class tbl_tipo_audiencia_negocio : crud_audiencia
    {
        private tbl_tipo_audiencia_acceso_datos _Audiencia = new tbl_tipo_audiencia_acceso_datos();
        private readonly ILoggerManager _logger;

        public ResponseGeneric<List<tbl_tipo_audiencia_>> Consultar()
        {
            try
            {
                return _Audiencia.Consultar();
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_tipo_audiencia_>>(ex);
            }
        }
    }
}
