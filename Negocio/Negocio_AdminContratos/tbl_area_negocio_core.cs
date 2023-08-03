using System;
using System.Collections.Generic;
using System.Text;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Area;
using Modelos.Response;
using AccesoDatos_AdministracionDeContratos;
using Utilidades.Log4Net;

namespace NegocioAdministracionContratos
{
    public class tbl_area_negocio_core : crud_areas
    {
        private tbl_area_acceso_datos_core _tbl_area_acceso_datos_core = new tbl_area_acceso_datos_core();
        private readonly ILoggerManager _logger;

        public tbl_area_negocio_core()
        {
            _logger = new LoggerManager();
        }
        public ResponseGeneric<List<tbl_lista_areas>> Get(string dependencia, string su)
        {
            try
            {
                return _tbl_area_acceso_datos_core.Get(dependencia, su);
            }
            catch (Exception ex)
            {
                _logger.LogError("get", ex);
                return new ResponseGeneric<List<tbl_lista_areas>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_lista_areas>> Get_Sub(string Area)
        {
            try
            {
                return _tbl_area_acceso_datos_core.Get_Sub(Area);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_lista_areas>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Add(tbl_area area)
        {
            try
            {
                if (area.id == null || area.id == Guid.Empty.ToString() || area.id == "")
                {
                    area.id = Guid.NewGuid().ToString();
                }
                return _tbl_area_acceso_datos_core.Add(area);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> update(tbl_area area)
        {
            try
            {
                return _tbl_area_acceso_datos_core.update(area);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> delete(tbl_area area)
        {
            try
            {               
                return _tbl_area_acceso_datos_core.delete(area);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<DropDownList>> FillDrop(string dependencia)
        {
            try
            {
                return _tbl_area_acceso_datos_core.FillDrop(dependencia);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }

        /*Modificación FSD*/
        public ResponseGeneric<List<tbl_areas_lista>> Get_areas(string p_id, string su)
        {
            try
            {
                return _tbl_area_acceso_datos_core.Get_areas(p_id, su);
            }
            catch (Exception ex)
            {
                _logger.LogError("get", ex);
                return new ResponseGeneric<List<tbl_areas_lista>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_subareas_lista>> Get_subareas(string p_id)
        {
            try
            {
                return _tbl_area_acceso_datos_core.Get_subareas(p_id);
            }
            catch (Exception ex)
            {
                _logger.LogError("get", ex);
                return new ResponseGeneric<List<tbl_subareas_lista>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_areasubordinada_lista>> Get_areas_sub(string p_id)
        {
            try
            {
                return _tbl_area_acceso_datos_core.Get_areas_sub(p_id);
            }
            catch (Exception ex)
            {
                _logger.LogError("get", ex);
                return new ResponseGeneric<List<tbl_areasubordinada_lista>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Add_subareas(tbl_subarea _Subarea)
        {
            try
            {
                _Subarea.p_opt = 2;
                _Subarea.p_id = Guid.NewGuid();

                return _tbl_area_acceso_datos_core.add_subarea(_Subarea);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Upd_subareas(tbl_subarea _Subarea)
        {
            try
            {
                _Subarea.p_opt = 3;
                return _tbl_area_acceso_datos_core.add_subarea(_Subarea);
            }
            catch (Exception ex)
            {
                _logger.LogError("upd", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Delete_subareas(tbl_subarea _Subarea)
        {
            try
            {
                _Subarea.p_opt = 4;
                return _tbl_area_acceso_datos_core.add_subarea(_Subarea);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Add_area_sub(tbl_area_subordinada _Subordinada)
        {
            try
            {
                _Subordinada.p_opt = 2;
                _Subordinada.p_id = Guid.NewGuid();

                return _tbl_area_acceso_datos_core.add_area_subordinada(_Subordinada);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Upd_area_sub(tbl_area_subordinada _Subordinada)
        {
            try
            {
                _Subordinada.p_opt = 3;
                return _tbl_area_acceso_datos_core.add_area_subordinada(_Subordinada);
            }
            catch (Exception ex)
            {
                _logger.LogError("upd", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Delete_area_sub(tbl_area_subordinada _Subordinada)
        {
            try
            {
                _Subordinada.p_opt = 4;
                return _tbl_area_acceso_datos_core.add_area_subordinada(_Subordinada);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        /*Modificación FSD*/
    }
}
