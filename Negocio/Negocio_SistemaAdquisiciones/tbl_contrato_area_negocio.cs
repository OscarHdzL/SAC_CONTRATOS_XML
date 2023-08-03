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
    public class tbl_contrato_area_negocio
    {
        private tbl_contrato_area_temp_acceso_datos _contrato_area = new tbl_contrato_area_temp_acceso_datos();

        public ResponseGeneric<List<Crudresponse>> Guardar(tbl_contrato_area_add contrato_area)
        {
            try
            {

                return _contrato_area.Guardar(contrato_area);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }



    }
}