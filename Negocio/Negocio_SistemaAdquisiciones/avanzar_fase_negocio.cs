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
    public class avanzar_fase_negocio
    {
        private avanzar_fase_acceso_datos _avanzar = new avanzar_fase_acceso_datos();
        //private tbl_tiposcontrato_acceso_datos _TiposContratos = new tbl_tiposcontrato_acceso_datos();

        public ResponseGeneric<Crudresponse> avanzar_fase(String solicitud)
        {
            try
            {
                return _avanzar.avanzar_fase(solicitud);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<Crudresponse>(ex);
            }
        }



    }
}