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
    public class elementos_bandeja_negocio { 
        private elementos_bandeja_acceso_datos _elementos = new elementos_bandeja_acceso_datos();
        public ResponseGeneric<List<elementos_bandeja>> getElementos(string usuario)
        {
            try
            {
                return _elementos.getElementos(usuario);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<elementos_bandeja>>(ex);
            }
        }



    }
}