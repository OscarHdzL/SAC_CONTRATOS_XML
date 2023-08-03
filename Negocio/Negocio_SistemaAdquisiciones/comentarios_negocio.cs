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
    public class comentarios_negocio
    {
        private comentarios_acceso_datos _comentario = new comentarios_acceso_datos();

        public ResponseGeneric<List<comentarios>> get_comentarios_solicitud(String solicitud)
        {
            try
            {
                return _comentario.get_comentario_solicitud(solicitud);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<comentarios>>(ex);
            }
        }

        public ResponseGeneric<Crudresponse> Add(comentario_add comentario)
        {
            try
            {
                comentario.p_opt = 2;
                return _comentario.Add(comentario);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<Crudresponse>(ex);
            }
        }



    }
}