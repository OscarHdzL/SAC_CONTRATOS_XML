
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
    public class responsablessolicitud_negocio 
    {
        private responsablessolicitud_acceso_datos _resp = new responsablessolicitud_acceso_datos();
        //private tbl_tiposcontrato_acceso_datos _TiposContratos = new tbl_tiposcontrato_acceso_datos();

        public ResponseGeneric<List<DropDownList>> FillDrop(String sigla_rol, String instancia)
        {
            try
            {
                return _resp.FillDrop(sigla_rol, instancia);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Guardar(Responsables_Solicitud responsable)
        {
            try
            {
                return _resp.Guardar(responsable);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }


    }
}
