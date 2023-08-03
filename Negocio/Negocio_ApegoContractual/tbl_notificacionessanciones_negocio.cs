using System;
using System.Collections.Generic;
using System.Text;
using AccesoDatos;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Response;
using Utilidades.Log4Net;

namespace Solucion_Negocio
{
	public class tbl_notificacionessanciones_negocio: crud_tbl_notificacionessanciones
	{
        private tbl_notificacionessanciones_datos _NotificacionesSanciones = new tbl_notificacionessanciones_datos();
        private readonly ILoggerManager _logger;
        public tbl_notificacionessanciones_negocio()
        {
            _logger = new LoggerManager();
        }
        public ResponseGeneric<List<tbl_notificacionsanciones>> Consultar(Guid idContrato, string periodo)
        {
            try
            {
                return _NotificacionesSanciones.Consultar(idContrato, periodo);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_notificacionsanciones>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_plan_por_obligacion>> ConsultarPO(Guid idOblig, string tipo, string periodo)
        {
            try
            {
                return _NotificacionesSanciones.ConsultarPO(idOblig, tipo, periodo);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_plan_por_obligacion>>(ex);
            }
        }
    }
}
