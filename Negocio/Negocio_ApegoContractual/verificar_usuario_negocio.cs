using AccesoDatos;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solucion_Negocio
{
    public class verificar_usuario_negocio : CRUD_verificar_usuario<tbl_usuario_verifica, verificacion_usuario>
    {
        private verificar_usuario_acceso_datos _Usuarios = new verificar_usuario_acceso_datos();

        public ResponseGeneric<List<tbl_usuario_verifica>> Consultar(tbl_usuario_verifica entidad, verificacion_usuario usuario)
        {
            try
            {
                return _Usuarios.Consultar(entidad, usuario);
            }
            catch (Exception ex)
            {

                return new ResponseGeneric<List<tbl_usuario_verifica>>(ex);
            }
        }

    }
}
