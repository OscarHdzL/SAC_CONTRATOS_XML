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
    public class control_evaluacion_negocio
    {
        private control_evaluacion_acceso_datos _evaluacion = new control_evaluacion_acceso_datos();

        public ResponseGeneric<List<grid_evaluacion_propuestas_solicitud>> Get_Evaluacion_Propuesta(String Solicitud)
        {
            try
            {
                return _evaluacion.Get_Evaluacion_Propuesta(Solicitud);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<grid_evaluacion_propuestas_solicitud>>(ex);
            }
        }

        public ResponseGeneric<Crudresponse> Add(evaluacion_propuesta_add evaluacion)
        {
            try
            {
                evaluacion.p_opt = 2;
                return _evaluacion.Add(evaluacion);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<Crudresponse>(ex);
            }
        }



    }
}