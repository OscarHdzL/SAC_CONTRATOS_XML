using AccesoDatos;
using Modelos.Interfaz;
using Modelos.Modelos;

using Modelos.Modelos.PreguntasFormulario;
using Modelos.Response;

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Utilidades.Log4Net;

namespace Solucion_Negocio
{
    public class tbl_pregunta_formulario_negocio : crud_preguntasformulario
    {
        private tbl_preguntas_formulario_acceso_datos _preguntas = new tbl_preguntas_formulario_acceso_datos();
        private readonly ILoggerManager _logger;

        public tbl_pregunta_formulario_negocio()
        {
            _logger = new LoggerManager();
        }
        public ResponseGeneric<List<Crudresponse>> add(tbl_pregunta_formulario_add pregunta)
        {
            try
            {
                pregunta.p_id = Guid.NewGuid().ToString();
                pregunta.p_opt = 2;
                pregunta.p_inclusion = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");   
                pregunta.p_estatus = 1;
                return _preguntas.add(pregunta);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> update(tbl_pregunta_formulario_add pregunta)
        {
            try
            {
                
                pregunta.p_opt = 3;
                pregunta.p_estatus = 1;
                return _preguntas.update(pregunta);
            }
            catch (Exception ex)
            {
                _logger.LogError("update", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }


        public ResponseGeneric<List<Crudresponse>> delete(tbl_pregunta_formulario_add pregunta)
        {
            try
            {
                pregunta.p_opt = 4;
                pregunta.p_estatus = 0;
                return _preguntas.delete(pregunta);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_pregunta_formulario>> Consultar(string Dependencia)
        {
            throw new NotImplementedException();
        }
    }
}
