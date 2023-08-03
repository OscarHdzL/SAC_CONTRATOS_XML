
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
    public class tbl_proyectos_negocio
    {
        private tbl_proyectos_acceso_datos _proyectos = new tbl_proyectos_acceso_datos();
        

        public ResponseGeneric<List<DropDownList>> FillDrop(string dependencia)
        {
            try
            {
                return _proyectos.FillDrop(dependencia);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }

    }
}
