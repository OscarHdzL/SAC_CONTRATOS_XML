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
    public class MesaValidacion_Negocio
    {
        private MesaValidacion_acceso_datos _avanzar = new MesaValidacion_acceso_datos();
        //private tbl_tiposcontrato_acceso_datos _TiposContratos = new tbl_tiposcontrato_acceso_datos();

        public ResponseGeneric<CrudresponseNum> MesaValidacion_(String solicitud, String Token)
        {
            try
            {
                return _avanzar.Mesa_Validacion(solicitud, Token);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<CrudresponseNum>(ex);
            }
        }

        public ResponseGeneric<CrudresponseNum> get_num_sol(Guid solicitud)
        {
            try
            {
                return _avanzar.get_num_sol(solicitud);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<CrudresponseNum>(ex);
            }
        }
        public ResponseGeneric<SolicitudMesaValidacion> get_mesa(Guid solicitud)
        {
            try
            {
                return _avanzar.get_mesa(solicitud);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<SolicitudMesaValidacion>(ex);
            }
        }
        public ResponseGeneric<List<DocumentsFileManager>> get_mesa_solicitante(Guid solicitud)
        {
            try
            {
                return _avanzar.get_mesa_solicitante(solicitud);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<DocumentsFileManager>>(ex);
            }
        }
    }
}