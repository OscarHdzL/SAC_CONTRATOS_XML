using System;
using System.Collections.Generic;
using System.Text;
using AccesoDatos;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Response;
using Utilidades.Log4Net;

namespace Solucion_Negocio
{
	public class tbl_producto_servicio_negocio: crud_tbl_producto_servicio
	{
        private tbl_producto_servicio_datos _ProductoServicios = new tbl_producto_servicio_datos();
        private readonly ILoggerManager _logger;

        public tbl_producto_servicio_negocio()
        {
            _logger = new LoggerManager();
        }
        public ResponseGeneric<List<Crudresponse>> Add(tbl_producto_servicio_add entidad)
        {
            try
            {
                return _ProductoServicios.Add(entidad);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_producto_servicio>> Consultar(Guid id)
        {
            try
            {
                return _ProductoServicios.Consultar(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_producto_servicio>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_producto_servicio_contrato>> ConsultarContrato(Guid id)
        {
            try
            {
                return _ProductoServicios.ConsultarContrato(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_producto_servicio_contrato>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_producto_servicio>> ConsultarUnitario(Guid id)
        {
            try
            {
                return _ProductoServicios.ConsultarUnitario(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_producto_servicio>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_unidad_medida>> ConsultarUnidadesMedida()
        {
            try
            {
                return _ProductoServicios.ConsultarUnidadesMedida();
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_unidad_medida>>(ex);
            }
        }
        public ResponseGeneric<List<DropDownList>> ConsultarTipo_Prod_Serv()
        {
            try
            {
                return _ProductoServicios.ConsultarTipo_Prod_Serv();
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }
        public ResponseGeneric<List<DropDownList>> ProductosUbicacionPE(Guid idPE, Guid idUbicacion)
        {
            try
            {
                return _ProductoServicios.get_productos_ubicacion(idPE, idUbicacion);
            }
            catch (Exception ex)
            {
                _logger.LogError("ProductosUbicacionPE", ex);
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }
        public ResponseGeneric<List<producto_servicio_pe>> Lista_ProductosUbicacionPE(Guid idPE, Guid idUbicacion)
        {
            try
            {
                return _ProductoServicios.get_productos_pe_ubicacion(idPE, idUbicacion);
            }
            catch (Exception ex)
            {
                _logger.LogError("Lista_ProductosUbicacionPE", ex);
                return new ResponseGeneric<List<producto_servicio_pe>>(ex);
            }
        }
    }
}
