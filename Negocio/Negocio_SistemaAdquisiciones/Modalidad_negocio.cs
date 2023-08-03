
using AccesoDatos;
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
    public class Modalidad_negocio
    {
        private AccesoDatos.Modalidad_datos _modalidad = new AccesoDatos.Modalidad_datos();

        public ResponseGeneric<List<Crudresponse>> add(sp_modalidad sp_modalidad_input)
        {
            try
            {
                sp_modalidad_input.p_id = Guid.NewGuid();
                return _modalidad.add(sp_modalidad_input);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<ModalidadSolParcial>> SolParcial(Guid id)
        {
            try
            {
                return _modalidad.modalidad_get_parcial(id);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<ModalidadSolParcial>>(ex);
            }
        }
        public ResponseGeneric<List<CrudresponseNum>> Validar(Guid p_tbl_solicitud_id)
        {
            try
            {
                return _modalidad.validar(p_tbl_solicitud_id);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<CrudresponseNum>>(ex);
            }
        }



        public ResponseGeneric<List<DropDownList>> get_modalidad_catalogos(String tipo)
        {
            try
            {
                return _modalidad.get_modalidad_catalogos(tipo);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }

        public ResponseGeneric<tbl_modalidad> get_modalidad_solicitud(String solicitud)
        {
            try
            {
                return _modalidad.get_modalidad_solicitud(solicitud);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<tbl_modalidad>(ex);
            }
        }

    }
}
