using AccesoDatos;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Utilidades.Log4Net;

namespace Solucion_Negocio
{
    public class vs_siderbar_negocio : crud_siderbar
    {
        private vs_siderbar_acces_odatos _sidebar = new vs_siderbar_acces_odatos();
        private readonly ILoggerManager _logger;

        public vs_siderbar_negocio()
        {
            _logger = new LoggerManager();
        }

        public ResponseGeneric<List<vs_siderbar>> Consultar(string rol)
        {
            try
            {
                return _sidebar.Consultar(rol);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<vs_siderbar>>(ex);
            }
        }

    }
}
