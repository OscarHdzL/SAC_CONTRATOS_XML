using System;
using System.Collections.Generic;
using System.Text;
using AccesoDatos;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Response;

namespace Solucion_Negocio
{
	public class tbl_tipo_acuerdo_negocio : crud_tbl_tipo_acuerdo
    {
        private tbl_producto_servicio_datos _ProductoServicios = new tbl_producto_servicio_datos();
       

        public ResponseGeneric<List<tbl_producto_servicio>> Consultar(Guid id)
        {
            try
            {
                return _ProductoServicios.Consultar(id);
            }
            catch (Exception ex)
            {

                return new ResponseGeneric<List<tbl_producto_servicio>>(ex);
            }
        }
    
    }
}
