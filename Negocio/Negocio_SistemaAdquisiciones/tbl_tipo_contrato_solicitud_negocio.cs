
using AccesoDatos_SistemaAdquisiciones;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Contrato;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.Text;


namespace Negocio_SistemaAdquisiciones
{
    public class tbl_tipo_contrato_solicitud_negocio
    {
        private tbl_tipo_contrato_solicitud_acceso_datos _tipo_contrato_solicitud = new tbl_tipo_contrato_solicitud_acceso_datos();
        

        public ResponseGeneric<List<DropDownList>> FillDrop(string instancia)
        {
            try
            {
                return _tipo_contrato_solicitud.FillDrop(instancia);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }


       

    }
}
