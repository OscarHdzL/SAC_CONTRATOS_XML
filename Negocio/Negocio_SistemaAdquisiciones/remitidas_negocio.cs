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
    public class remitidas_negocio
    {
        private Remitidas_acceso_datos _remitidas = new Remitidas_acceso_datos();
        //private tbl_tiposcontrato_acceso_datos _TiposContratos = new tbl_tiposcontrato_acceso_datos();

        public ResponseGeneric<List<remitidas>> get_remitidas_tec(String rol_usuario)
        {
            try
            {
                return _remitidas.get_remitidas_tec(rol_usuario);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<remitidas>>(ex);
            }
        }

        public ResponseGeneric<List<remitidas>> get_remitidas_eco(String rol_usuario)
        {
            try
            {
                return _remitidas.get_remitidas_eco(rol_usuario);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<remitidas>>(ex);
            }
        }



    }
}