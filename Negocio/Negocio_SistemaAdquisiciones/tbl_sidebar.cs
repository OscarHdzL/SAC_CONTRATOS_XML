using AccesoDatos_SistemaAdquisiciones;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio_SistemaAdquisiciones
{
    public class vs_siderbar_negocio : crud_siderbar
    {
        private vs_siderbar_acces_odatos _sidebar = new vs_siderbar_acces_odatos();

        public ResponseGeneric<List<vs_siderbar>> Consultar(string rol)
        {
            try
            {
                return _sidebar.Consultar(rol);
            }
            catch (Exception ex)
            {

                return new ResponseGeneric<List<vs_siderbar>>(ex);
            }
        }

    }
}
