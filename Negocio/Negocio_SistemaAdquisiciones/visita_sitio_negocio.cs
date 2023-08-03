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
    public class visita_sitio_negocio
    {
        private visita_sitio_acceso_datos _control = new visita_sitio_acceso_datos();

        public ResponseGeneric<Crudresponse> Add(tbl_control_evento_add control)
        {
            try
            {
                
                control.p_opt = 2;
                control.p_id = Guid.NewGuid().ToString();

                return _control.Add(control);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<Crudresponse>(ex);
            }
        }



    }
}