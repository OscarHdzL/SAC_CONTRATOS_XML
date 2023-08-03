using AccesoDatos;
using Modelos.Interfaz;
using Modelos.Modelos;

using Modelos.Modelos.GestionExpediente;
using Modelos.Response;

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Utilidades.Log4Net;

namespace Solucion_Negocio
{
    public class tbl_gestion_expediente_contrato_negocio : crud_expedientecontrato
    {
        private tbl_gestion_expediente_contrato_acceso_datos _expediente = new tbl_gestion_expediente_contrato_acceso_datos();
        private readonly ILoggerManager _logger;

        public tbl_gestion_expediente_contrato_negocio()
        {
            _logger = new LoggerManager();
        }

        public ResponseGeneric<List<Crudresponse>> add(tbl_gestion_expediente_contrato_add expediente)
        {
            try
            {

                if (expediente.p_id == null || expediente.p_id == Guid.Empty.ToString())
                {
                    expediente.p_id = Guid.NewGuid().ToString();
                    expediente.p_opt = 2;
                }
                else
                { //Actualiza por que ya existe un id
                    expediente.p_opt = 3;
                }
                expediente.p_inclusion = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                expediente.p_estatus = 1;
                return _expediente.add(expediente);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

    }
}
