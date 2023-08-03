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
    public class tbl_contrato_producto_negocio : crud_tbl_contrato_producto
    {
        private tbl_contrato_productos_acceso_datos _area = new tbl_contrato_productos_acceso_datos();
        private readonly ILoggerManager _logger;

        public tbl_contrato_producto_negocio()
        {
            _logger = new LoggerManager();
        }

        public ResponseGeneric<List<DropDownList>> FillDrop(string dependencia)
        {
            try
            {
                return _area.FillDrop(dependencia);
            }
            catch (Exception ex)
            {
                _logger.LogError("Filldrop", ex);
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_contrato_producto_list>> GetListContrato(Guid id)
        {
            try
            {
                return _area.GetListContrato(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetListContrato", ex);
                return new ResponseGeneric<List<tbl_contrato_producto_list>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_contrato_producto_list>> GetUnitario(Guid id)
        {
            try
            {
                return _area.Getunitario(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetUnitario", ex);
                return new ResponseGeneric<List<tbl_contrato_producto_list>>(ex);
            }
        }
        public ResponseGeneric<List<DropDownList>> GetListDependencia(Guid id)
        {
            try
            {
                return _area.GetListDependencia(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetListDependencia", ex);
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> update(tbl_contrato_producto_add tbl_contrato_producto_add)
        {
            try
            {
                return _area.Update(tbl_contrato_producto_add);
            }
            catch (Exception ex)
            {
                _logger.LogError("update", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

    }
}
