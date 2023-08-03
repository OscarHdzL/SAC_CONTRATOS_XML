using AccesoDatos;
using AccesoDatos_SistemaAdquisiciones;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Contrato;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.Text;



namespace Negocio_SistemaAdquisiciones
{
    public class tbl_convocatoria_negocio
    {
        private tbl_convocatoria_acceso_datos _convocatoria = new tbl_convocatoria_acceso_datos();
        public ResponseGeneric<List<tbl_convocatoria>> Get_convocatoria(String id_solicitud)
        {
            try
            {
                return _convocatoria.get_convocatoria_by_solicitud(id_solicitud);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_convocatoria>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Add(tbl_convocatoria_add tbl_Convocatoria)
        {
            try
            {
                tbl_Convocatoria.p_opt = 2;
                tbl_Convocatoria.p_id = Guid.NewGuid();
                tbl_Convocatoria.p_inclusion = DateTime.Now;

                return _convocatoria.add(tbl_Convocatoria);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Update(tbl_convocatoria_add tbl_Convocatoria)
        {
            try
            {
                tbl_Convocatoria.p_opt = 3;
                tbl_Convocatoria.p_fecha_suscripcion = DateTime.Now;
                tbl_Convocatoria.p_inclusion = DateTime.Now;

                return _convocatoria.add(tbl_Convocatoria);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<DropDownList>> Get_Areas(String id_dependencia)
        {
            try
            {
                return _convocatoria.get_areas_by_dep(id_dependencia);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }
        public ResponseGeneric<List<DropDownList>> Get_SerPub(String id_area)
        {
            try
            {
                return _convocatoria.get_servidor_by_dep(id_area);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_tipo_obligacion>> GetTipooblig()
        {
            try
            {
                return _convocatoria.Get_tipo_obligaciones();
            }
            catch (Exception ex)
            {

                return new ResponseGeneric<List<tbl_tipo_obligacion>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Add_obligacion_conv(tbl_obligacion_conv_add tbl_Obligacion_Conv)
        {
            try
            {
                tbl_Obligacion_Conv.p_opt = 2;
                tbl_Obligacion_Conv.p_id = Guid.NewGuid();
                tbl_Obligacion_Conv.p_inclusion = DateTime.Now;

                return _convocatoria.add_obligacion(tbl_Obligacion_Conv);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Update_obligacion_conv(tbl_obligacion_conv_add tbl_Obligacion_Conv)
        {
            try
            {
                tbl_Obligacion_Conv.p_opt = 3;
                return _convocatoria.add_obligacion(tbl_Obligacion_Conv);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Delete_obligacion_conv(tbl_obligacion_conv_add tbl_Obligacion_Conv)
        {
            try
            {
                tbl_Obligacion_Conv.p_opt = 5;
                return _convocatoria.add_obligacion(tbl_Obligacion_Conv);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_convocatoria_obligaciones>> Get_convocatoria_obl(String id_solicitud)
        {
            try
            {
                return _convocatoria.get_convocatoria_obl(id_solicitud);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_convocatoria_obligaciones>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Add_responsable(tbl_responsable_convocatoria tbl_Responsable_)
        {
            try
            {
                tbl_Responsable_.p_opt = 2;
                tbl_Responsable_.p_id = Guid.NewGuid();

                return _convocatoria.add_responsable(tbl_Responsable_);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Update_responsable(tbl_responsable_convocatoria tbl_Responsable_)
        {
            try
            {
                tbl_Responsable_.p_opt = 3;
                return _convocatoria.add_responsable(tbl_Responsable_);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Delete_responsable(tbl_responsable_convocatoria tbl_Responsable_)
        {
            try
            {
                tbl_Responsable_.p_opt = 4;
                return _convocatoria.add_responsable(tbl_Responsable_);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_responsable_convocatoria_lista>> Get_convocatoria_lista_resp(String id_solicitud)
        {
            try
            {
                return _convocatoria.get_convocatoria_responsable(id_solicitud);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_responsable_convocatoria_lista>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Add_penalizacion(tbl_convocatoria_penalizacion tbl_Penalizacion)
        {
            try
            {
                tbl_Penalizacion.p_opt = 2;
                tbl_Penalizacion.p_id = Guid.NewGuid();

                return _convocatoria.add_penalizacion(tbl_Penalizacion);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Update_penalizacion(tbl_convocatoria_penalizacion tbl_Penalizacion)
        {
            try
            {
                tbl_Penalizacion.p_opt = 3;
                return _convocatoria.add_penalizacion(tbl_Penalizacion);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Delete_penalizacion(tbl_convocatoria_penalizacion tbl_Penalizacion)
        {
            try
            {
                tbl_Penalizacion.p_opt = 4;
                return _convocatoria.add_penalizacion(tbl_Penalizacion);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_convocatoria_penalizacion_lista>> Get_convocatoria_lista_penalizacion(String id_solicitud)
        {
            try
            {
                return _convocatoria.get_convocatoria_penalizacion(id_solicitud);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_convocatoria_penalizacion_lista>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Add_condicion(tbl_convocatoria_condicion _Condicion)
        {
            try
            {
                _Condicion.p_opt = 2;
                _Condicion.p_id = Guid.NewGuid();

                return _convocatoria.add_condicion(_Condicion);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Update_condicion(tbl_convocatoria_condicion _Condicion)
        {
            try
            {
                _Condicion.p_opt = 3;
                return _convocatoria.add_condicion(_Condicion);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Delete_condicion(tbl_convocatoria_condicion _Condicion)
        {
            try
            {
                _Condicion.p_opt = 4;
                return _convocatoria.add_condicion(_Condicion);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_convocatoria_condicion_lista>> Get_convocatoria_lista_condicion(String id_solicitud)
        {
            try
            {
                return _convocatoria.get_convocatoria_condicion(id_solicitud);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_convocatoria_condicion_lista>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Add_pago(tbl_convocatoria_pago _Pago)
        {
            try
            {
                _Pago.p_opt = 2;
                _Pago.p_id = Guid.NewGuid();

                return _convocatoria.add_pago(_Pago);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Update_pago(tbl_convocatoria_pago _Pago)
        {
            try
            {
                _Pago.p_opt = 3;
                return _convocatoria.add_pago(_Pago);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Delete_pago(tbl_convocatoria_pago _Pago)
        {
            try
            {
                _Pago.p_opt = 4;
                return _convocatoria.add_pago(_Pago);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_convocatoria_pago_lista>> Get_convocatoria_lista_pago(String id_solicitud)
        {
            try
            {
                return _convocatoria.get_convocatoria_pagos(id_solicitud);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_convocatoria_pago_lista>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Add_criterio(tbl_convocatoria_criterio _Criterio)
        {
            try
            {
                _Criterio.p_opt = 2;
                _Criterio.p_id = Guid.NewGuid();

                return _convocatoria.add_criterio(_Criterio);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Update_criterio(tbl_convocatoria_criterio _Criterio)
        {
            try
            {
                _Criterio.p_opt = 3;
                return _convocatoria.add_criterio(_Criterio);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Delete_criterio(tbl_convocatoria_criterio _Criterio)
        {
            try
            {
                _Criterio.p_opt = 4;
                return _convocatoria.add_criterio(_Criterio);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_convocatoria_criterio_lista>> Get_convocatoria_lista_criterios(String id_solicitud)
        {
            try
            {
                return _convocatoria.get_convocatoria_criterios(id_solicitud);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_convocatoria_criterio_lista>>(ex);
            }
        }
        public ResponseGeneric<List<DropDownList>> Get_convocatoria_tipo_criterios()
        {
            try
            {
                return _convocatoria.get_tipo_criterios();
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Add_Doc(tbl_documento_validacion tbl_Documento_)
        {
            try
            {
                tbl_Documento_.p_opt = 2;
                tbl_Documento_.p_id = Guid.NewGuid();

                return _convocatoria.add_documento(tbl_Documento_);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Update_Doc(tbl_documento_validacion tbl_Documento_)
        {
            try
            {
                tbl_Documento_.p_opt = 3;
                return _convocatoria.add_documento(tbl_Documento_);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Delete_Doc(tbl_documento_validacion tbl_Documento_)
        {
            try
            {
                tbl_Documento_.p_opt = 4;
                return _convocatoria.add_documento(tbl_Documento_);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_documento_validacion_lista>> Get_convocatoria_lista_documento(String id_solicitud)
        {
            try
            {
                return _convocatoria.get_convocatoria_documentos(id_solicitud);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_documento_validacion_lista>>(ex);
            }
        }
        public ResponseGeneric<List<DropDownList>> Get_convocatoria_tipo_documento(String id_instancia)
        {
            try
            {
                return _convocatoria.get_tipo_documento(id_instancia);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }
    }
}
