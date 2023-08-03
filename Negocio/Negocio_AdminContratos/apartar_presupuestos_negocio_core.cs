using System;
using System.Collections.Generic;
using System.Text;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Response;
using AccesoDatos_AdminContratos;
using System.Linq;
using Utilidades.Log4Net;

namespace Negocio_AdminContratos
{
    public class apartar_presupuestos_negocio_core
    {
        private apartar_presupuestos_acceso_datos_core _apartar_presupuestos = new apartar_presupuestos_acceso_datos_core();
        private readonly ILoggerManager _logger;

        public apartar_presupuestos_negocio_core()
        {
            _logger = new LoggerManager();
        }
        public ResponseGeneric<List<CrudresponseIdentificador>> apartar_presupuesto(origen_recurso_add origen_recurso , List<apartar_presupuesto_area_add> presupuestos)
        {
            try
            {
                List<CrudresponseIdentificador> respuesta = new List<CrudresponseIdentificador>();




                ResponseGeneric<CrudresponseIdentificador> Step1 = _apartar_presupuestos.add_origen_recurso(origen_recurso);

                if (Step1.Response.cod == "success") {

                    respuesta.Add(Step1.Response);

                    foreach (apartar_presupuesto_area_add item in presupuestos) {

                        ResponseGeneric<CrudresponseIdentificador> Step2 = _apartar_presupuestos.add_apartar_presupuesto_area(item);

                        //if (Step2.Response.cod == "success")
                        //{
                        //    respuesta.Add(Step2.Response);
                        //}

                        respuesta.Add(Step2.Response);

                    }



                } else
                {
                    respuesta.Add(Step1.Response);
                    return new ResponseGeneric<List<CrudresponseIdentificador>>(respuesta);
                }


                return new ResponseGeneric<List<CrudresponseIdentificador>>(respuesta);
            }
            catch (Exception ex)
            {
                _logger.LogError("apartar", ex);
                return new ResponseGeneric<List<CrudresponseIdentificador>>(ex);
            }
        }
    }
}
