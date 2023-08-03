using AccesoDatos_SistemaAdquisiciones;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Contrato;
using Modelos.Response;
using System;
using System.Collections.Generic;
using
System.Text;

namespace Negocio_SistemaAdquisiciones
{
    public class tbl_solicitud_justificacion_negocio
    {
        private tbl_solicitud_justificacion_acceso_datos _solicitud_justificacion = new tbl_solicitud_justificacion_acceso_datos();

        public ResponseGeneric<List<Crudresponse>> Guardar(tbl_solicitud_justificacion solicitud_justificacion)
        {
            try
            {
                solicitud_justificacion.p_opt = 2;
                solicitud_justificacion.p_id = Guid.NewGuid();

                return _solicitud_justificacion.Guardar(solicitud_justificacion);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }



    }
}