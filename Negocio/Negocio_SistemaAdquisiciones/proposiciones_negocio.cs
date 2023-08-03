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
    public class proposiciones_negocio
    {
        private Proposiciones_acceso_datos _proposiciones = new Proposiciones_acceso_datos();
       
        public ResponseGeneric<List<proposiciones>> get_proposiciones_tec(String solicitud)
        {
            try
            {
                return _proposiciones.get_proposiciones_tec(solicitud);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<proposiciones>>(ex);
            }
        }

        public ResponseGeneric<List<proposiciones>> get_proposiciones_eco(String solicitud)
        {
            try
            {
                return _proposiciones.get_proposiciones_eco(solicitud);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<proposiciones>>(ex);
            }
        }

        public ResponseGeneric<List<proposiciones_evaluadas>> get_proposiciones_evaluadas_tipo(String solicitud, String tipo)
        {
            try
            {
                return _proposiciones.get_proposiciones_evaluadas_tipo(solicitud, tipo);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<proposiciones_evaluadas>>(ex);
            }
        }

        public ResponseGeneric<Crudresponse> add_proposicion(proposicion_tec_eco_add proposicion)
        {
            try
            {
                proposicion.p_opt = 2;
                return _proposiciones.add_proposicion(proposicion);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<Crudresponse>(ex);
            }
        }

        public ResponseGeneric<Crudresponse> update_proposicion(proposicion_tec_eco_add proposicion)
        {
            try
            {
                proposicion.p_opt = 3;
                return _proposiciones.update_proposicion(proposicion);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<Crudresponse>(ex);
            }
        }


    }
}