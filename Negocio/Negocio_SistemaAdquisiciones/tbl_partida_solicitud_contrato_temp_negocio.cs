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
    public class tbl_partida_solicitud_contrato_temp_negocio
    {
        private tbl_partida_solicitud_contrato_temp_acceso_datos _partida_solicitud = new tbl_partida_solicitud_contrato_temp_acceso_datos();

        public ResponseGeneric<List<Crudresponse>> Guardar(tbl_partida_solicitud_contrato_temp_add partida_solicitud)
        {
            try
            {

                return _partida_solicitud.Guardar(partida_solicitud);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }



    }
}