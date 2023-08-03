using System;
using System.Collections.Generic;
using System.Text;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Proyectos;
using Modelos.Response;
using AccesoDatos_AdministracionDeContratos;
using Utilidades.Log4Net;

namespace NegocioAdministracionContratos
{
    public class tbl_proyectos_negocio : crud_proyectos
    {
        private tbl_proyectos_acceso_datos _tbl_proyectos_acceso_datos = new tbl_proyectos_acceso_datos();
        private readonly ILoggerManager _logger;

        public tbl_proyectos_negocio()
        {
            _logger = new LoggerManager();
        }

        public ResponseGeneric<List<tbl_lista_proyectos>> Get_Lista(String id_dep)
        {
            try
            {
                return _tbl_proyectos_acceso_datos.Get_Lista(id_dep);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_lista_proyectos>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_proyectos>> Get_Proyecto(string id_proyecto)
        {
            try
            {
                return _tbl_proyectos_acceso_datos.Get_Proyecto(id_proyecto);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_proyectos>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Add(tbl_proyectos _tbl_proyectos)
        {
            try
            {
                if (_tbl_proyectos.id == null || _tbl_proyectos.id == Guid.Empty.ToString() || _tbl_proyectos.id == "")
                {
                    _tbl_proyectos.id = Guid.NewGuid().ToString();
                }
                return _tbl_proyectos_acceso_datos.Add(_tbl_proyectos);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> update(tbl_proyectos _tbl_proyectos)
        {
            try
            {
                return _tbl_proyectos_acceso_datos.update(_tbl_proyectos);
            }
            catch (Exception ex)
            {
                _logger.LogError("upd", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> delete(tbl_proyectos _tbl_proyectos)
        {
            try
            {
                return _tbl_proyectos_acceso_datos.delete(_tbl_proyectos);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<DropDownList>> Get_Tipo_P(string id_ins)
        {
            try
            {
                return _tbl_proyectos_acceso_datos.Get_Tipo_P(id_ins);
            }
            catch (Exception ex)
            {
                _logger.LogError("get", ex);
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }
        public ResponseGeneric<List<DropDownList>> Get_Criticidad()
        {
            try
            {
                return _tbl_proyectos_acceso_datos.Get_Criticidad();
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }
        public ResponseGeneric<List<DropDownList>> Get_Tipo_Ejecucion()
        {
            try
            {
                return _tbl_proyectos_acceso_datos.Get_Tipo_Ejecucion();
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }
        public ResponseGeneric<List<DropDownList>> Get_Estatus_Proyecto()
        {
            try
            {
                return _tbl_proyectos_acceso_datos.Get_Estatus_Proyecto();
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }

    }
}
