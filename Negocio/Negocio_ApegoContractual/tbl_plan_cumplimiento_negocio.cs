using AccesoDatos;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Contrato;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Utilidades.Log4Net;

namespace Solucion_Negocio
{
    public class tbl_plan_cumplimiento_negocio 
    {
        private tbl_plan_cumplimiento_acceso_datos _tbl_plan_cumplimiento_acceso_datos = new tbl_plan_cumplimiento_acceso_datos();
        private readonly ILoggerManager _logger;


        public ResponseGeneric<List<Crudresponse>> tbl_plan_cumplimiento(string opcion, Guid id, Guid tbl_link_obligacion_id, Guid tbl_plan_entrega_producto_id, String tipo)
        {

            return _tbl_plan_cumplimiento_acceso_datos.tbl_plan_cumplimiento(opcion, id, tbl_link_obligacion_id, tbl_plan_entrega_producto_id, tipo);
        }

        public ResponseGeneric<Crudresponse> ConfirmarPM(Guid tbl_plan_monitoreo_id)
        {
            return _tbl_plan_cumplimiento_acceso_datos.ConfirmarPM(tbl_plan_monitoreo_id);
        }

        public ResponseGeneric<Crudresponse> CerrarContrato(Guid tbl_contrato_id)
        {
            return _tbl_plan_cumplimiento_acceso_datos.CerrarContrato(tbl_contrato_id);
        }

    }
}
