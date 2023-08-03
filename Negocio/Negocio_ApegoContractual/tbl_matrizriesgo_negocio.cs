using AccesoDatos;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Contrato;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Utilidades.Log4Net;

namespace Solucion_Negocio
{
    public class tbl_matriz_riesgo_negocio : crud_tbl_matriz_riesgo
    {
        private tbl_matriz_riesgo_acceso_datos tbl_matriz_riesgo_acceso_datos = new tbl_matriz_riesgo_acceso_datos();
        private readonly ILoggerManager _logger;

        public tbl_matriz_riesgo_negocio()
        {
            _logger = new LoggerManager();
        }

        public ResponseGeneric<List<tbl_matriz_riesgo>> GetObligacion(Guid Obligacion)
        {
            try
            {
                return tbl_matriz_riesgo_acceso_datos.matrizriegos_obligacion(Obligacion);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetObligacion", ex);
                return new ResponseGeneric<List<tbl_matriz_riesgo>>(ex);
            }
        }


        public ResponseGeneric<List<Crudresponse>> Add(tbl_matriz_riesgo_add matriz)
        {
            try
            {
                return tbl_matriz_riesgo_acceso_datos.Add(matriz);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_tipo_respuesta>> GetTipoRespuesta()
        {
            try
            {
                return tbl_matriz_riesgo_acceso_datos.GetTipoRespuesta();
            }
            catch (Exception ex)
            {
                _logger.LogError("GetTipoRespuesta", ex);
                return new ResponseGeneric<List<tbl_tipo_respuesta>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_nivel_riesgo>> GetNivelRiesgo()
        {
            try
            {
                return tbl_matriz_riesgo_acceso_datos.GetNivelRiesgo();
            }
            catch (Exception ex)
            {
                _logger.LogError("GetNivelRiesgo", ex);
                return new ResponseGeneric<List<tbl_nivel_riesgo>>(ex);
            }
        }
    }
}
