using System;
using System.Collections.Generic;
using System.Text;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Contrato;
using Modelos.Response;
using AccesoDatos_AdminContratos;
using Utilidades.Log4Net;

namespace Negocio_AdminContratos
{
    public class catalogos_negocio_core
    {
        private catalogos_acceso_datos_core _catalogos = new catalogos_acceso_datos_core();
        private readonly ILoggerManager _logger;

        public catalogos_negocio_core()
        {
            _logger = new LoggerManager();
        }
        public ResponseGeneric<List<Crudresponse>> Add_tipo_riesgo(tbl_tipo_riesgo_add tipo_riesgo_add)
        {
            try
            {
                if (tipo_riesgo_add.p_opt == 2)
                {
                    tipo_riesgo_add.p_id = Guid.NewGuid();
                }
                return _catalogos.Add_tipo_riesgo(tipo_riesgo_add);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Delete_tipo_riesgo(tbl_tipo_riesgo_add _tbl_tipo_riesgo)
        {
            try
            {
                return _catalogos.Delete_tipo_riesgo(_tbl_tipo_riesgo);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Add_tipo_documento(tbl_tipo_documento_add tipo_documento_add)
        {
            try
            {
                if (tipo_documento_add.p_opt == 2)
                {
                    tipo_documento_add.p_id = Guid.NewGuid();
                }
                return _catalogos.Add_tipo_documento(tipo_documento_add);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Delete_tipo_documento(tbl_tipo_documento_add _tipo_documento_add)
        {
            try
            {
                return _catalogos.Delete_tipo_documento(_tipo_documento_add);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Add_tipo_proyecto(tbl_tipo_proyecto_add tipo_proyecto_add)
        {
            try
            {
                if (tipo_proyecto_add.p_opt == 2)
                {
                    tipo_proyecto_add.p_id = Guid.NewGuid();
                }
                return _catalogos.Add_tipo_proyecto(tipo_proyecto_add);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Delete_tipo_proyecto(tbl_tipo_proyecto_add _tipo_proyecto_add)
        {
            try
            {
                return _catalogos.Delete_tipo_proyecto(_tipo_proyecto_add);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_tipo_riesgo>> Get_tipo_riesgo(string id_insatancia)
        {
            try
            {
                return _catalogos.Get_lista_tipo_riesgo(id_insatancia);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_tipo_riesgo>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_tipo_documento>> Get_tipo_documento(string id_insatancia)
        {
            try
            {
                return _catalogos.Get_lista_tipo_documento(id_insatancia);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_tipo_documento>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_tipo_proyecto>> Get_tipo_proyecto(string id_insatancia)
        {
            try
            {
                return _catalogos.Get_lista_tipo_proyecto(id_insatancia);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_tipo_proyecto>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_tipo_ejecucion>> Get_tipo_ejecucion(string id_insatancia)
        {
            try
            {
                return _catalogos.Get_lista_tipo_ejecucion(id_insatancia);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_tipo_ejecucion>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Add_tipo_ejecucion(tbl_tipo_ejecucion_add tipo_ejecucion_add)
        {
            try
            {
                if (tipo_ejecucion_add.p_opt == 2)
                {
                    tipo_ejecucion_add.p_id = Guid.NewGuid();
                }
                return _catalogos.Add_tipo_ejecucion(tipo_ejecucion_add);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Delete_tipo_ejecucion(tbl_tipo_ejecucion_add _tbl_tipo_ejecucion)
        {
            try
            {
                return _catalogos.Delete_tipo_ejecucion(_tbl_tipo_ejecucion);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }


        public ResponseGeneric<List<Crudresponse>> Add_tipo_contrato(tbl_tipo_contrato_add tipo_contrato_add)
        {
            try
            {
                if (tipo_contrato_add.p_opt == 2)
                {
                    tipo_contrato_add.p_id = Guid.NewGuid();
                }
                return _catalogos.Add_tipo_contrato(tipo_contrato_add);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Delete_tipo_contrato(tbl_tipo_contrato_add _tipo_contrato_add)
        {
            try
            {
                return _catalogos.Delete_tipo_contrato(_tipo_contrato_add);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_tipo_contrato>> Get_tipo_contrato(string id_instancia)
        {
            try
            {
                return _catalogos.Get_lista_tipo_contrato(id_instancia);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_tipo_contrato>>(ex);
            }
        }


        public ResponseGeneric<List<tbl_tipo_prioridad>> Get_tipo_prioridad(string id_insatancia)
        {
            try
            {
                return _catalogos.Get_lista_tipo_prioridad(id_insatancia);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_tipo_prioridad>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Add_tipo_prioridad(tbl_tipo_prioridad_add tipo_prioridad_add)
        {
            try
            {
                if (tipo_prioridad_add.p_opt == 2)
                {
                    tipo_prioridad_add.p_id = Guid.NewGuid();
                }
                return _catalogos.Add_tipo_prioridad(tipo_prioridad_add);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Delete_tipo_prioridad(tbl_tipo_prioridad_add _tbl_tipo_prioridad)
        {
            try
            {
                return _catalogos.Delete_tipo_prioridad(_tbl_tipo_prioridad);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }



        public ResponseGeneric<List<tbl_unidad_medida>> Get_lista_unidad_medida()
        {
            try
            {
                return _catalogos.Get_lista_unidad_medida();
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_unidad_medida>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Add_unidad_medida(tbl_unidad_medida_add unidad_medida_add)
        {
            try
            {
                if (unidad_medida_add.p_opt == 2)
                {
                    unidad_medida_add.p_id = Guid.NewGuid();
                }
                return _catalogos.Add_unidad_medida(unidad_medida_add);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Delete_unidad_medida(tbl_unidad_medida_add unidad_medida_add)
        {
            try
            {
                return _catalogos.Delete_unidad_medida(unidad_medida_add);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<lista_tipo_interlocutor>> Get_tipo_interlocutor()
        {
            try
            {
                return _catalogos.Get_lista_tipo_interlocutor();
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<lista_tipo_interlocutor>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Add_tipo_interlocutor(tbl_tipo_interlocutor_add tipo_interlocutor_add)
        {
            try
            {
                if (tipo_interlocutor_add.p_opt == 2)
                {
                    tipo_interlocutor_add.id = Guid.NewGuid();
                }
                return _catalogos.Add_tipo_interlocutor(tipo_interlocutor_add);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Delete_tipo_interlocutor(tbl_tipo_interlocutor_add _tbl_tipo_interlocutor)
        {
            try
            {
                return _catalogos.Delete_tipo_interlocutor(_tbl_tipo_interlocutor);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }


        public ResponseGeneric<List<tbl_procedimiento>> Get_procedimiento()
        {
            try
            {
                return _catalogos.Get_lista_procedimiento();
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_procedimiento>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Add_procedimiento(tbl_procedimiento_add procedimiento_add)
        {
            try
            {
                if (procedimiento_add.p_opt == 2)
                {
                    procedimiento_add.p_id = Guid.NewGuid();
                }
                return _catalogos.Add_procedimiento(procedimiento_add);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Delete_procedimiento(tbl_procedimiento_add procedimiento_add)
        {
            try
            {
                return _catalogos.Delete_procedimiento(procedimiento_add);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }



    }
}
