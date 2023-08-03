using AccesoDatos;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Response;
using Utilidades.Log4Net;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Solucion_Negocio
{
    public class tbl_acuerdo_negocio : crud_acuerdos
    {
        private readonly ILoggerManager _logger;
        private tbl_acuerdo_acceso_datos _acuerdos = new tbl_acuerdo_acceso_datos();

        public tbl_acuerdo_negocio()
        {
            _logger = new LoggerManager();
        }

        public ResponseGeneric<List<tbl_acuerdos_lista>> Consultar(String contrato)
        {
            try
            {
                return _acuerdos.Consultar(contrato);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_acuerdos_lista>>(ex);
            }
        }
        public ResponseGeneric<tbl_acuerdos_lista> ConsultarAcuerdo(String Acuerdo)
        {
            try
            {
                return _acuerdos.ConsultarAcuerdo(Acuerdo);
            }
            catch (Exception ex)
            {
                _logger.LogError("ConsultarAcuerdo", ex);
                return new ResponseGeneric<tbl_acuerdos_lista>(ex);
            }
        }

        public ResponseGeneric<tbl_acuerdos_lista> ConsultarAcuerdoRC(String Acuerdo, String Contrato)
        {
            try
            {
                return _acuerdos.ConsultarAcuerdoRC(Acuerdo, Contrato);
            }
            catch (Exception ex)
            {
                _logger.LogError("ConsultarAcuerdo", ex);
                return new ResponseGeneric<tbl_acuerdos_lista>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> add(tbl_acuerdo_add Acuerdo)
        {
            try
            {
                Acuerdo.p_id = Guid.NewGuid().ToString();
                Acuerdo.p_opt = 2;
                //Acuerdo.p_inclusion = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                Acuerdo.p_estatus = 1;
                return _acuerdos.add(Acuerdo);
            }
            catch (Exception ex)
            {
                _logger.LogError("Add", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> update(tbl_acuerdo_add Acuerdo)
        {
            try
            {
                Acuerdo.p_opt = 3;
                Acuerdo.p_estatus = 1;
                return _acuerdos.update(Acuerdo);
            }
            catch (Exception ex)
            {
                _logger.LogError("Update", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> delete(tbl_acuerdo_add Acuerdo)
        {
            try
            {
                Acuerdo.p_opt = 4;
                Acuerdo.p_estatus = 0;
                return _acuerdos.delete(Acuerdo);
            }
            catch (Exception ex)
            {
                _logger.LogError("Delete", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<DropDownList>> ConsultarTipos()
        {
            try
            {
                return _acuerdos.ConsultarTipos();
            }
            catch (Exception ex)
            {
                _logger.LogError("ConsultarTipos", ex);
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Add_tipo_acuerdo(tbl_tipo_acuerdo_add tbl_tipo_acuerdo_add)
        {
            try
            {
                if (tbl_tipo_acuerdo_add.p_opt == 2)
                {
                    tbl_tipo_acuerdo_add.p_id = Guid.NewGuid();
                }
                return _acuerdos.Add_tipo_acuerdo(tbl_tipo_acuerdo_add);
            }
            catch (Exception ex)
            {
                _logger.LogError("Add_tipo_acuerdo", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Delete_tipo_acuerdo(tbl_tipo_acuerdo_add tbl_tipo_acuerdo_add)
        {
            try
            {
                return _acuerdos.Delete_tipo_acuerdo(tbl_tipo_acuerdo_add);
            }
            catch (Exception ex)
            {
                _logger.LogError("Delete_tipo_acuerdo", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        //public Response Eliminar(TBL_SANCIONES entidad)
        //{
        //    try
        //    {
        //        return _Sanciones.Eliminar(entidad);
        //    }
        //    catch (Exception ex)
        //    {

        //        return new Response(ex);
        //    }
        //}

        //public Response Guardar(TBL_SANCIONES entidad)
        //{
        //    try
        //    {
        //        return _Sanciones.Guardar(entidad);
        //    }
        //    catch (Exception ex)
        //    {

        //        return new Response(ex);
        //    }
        //}

        //public Response Modificar(TBL_SANCIONES entidad)
        //{
        //    try
        //    {
        //        return _Sanciones.Modificar(entidad);
        //    }
        //    catch (Exception ex)
        //    {

        //        return new Response(ex);
        //    }
        //}
    }
}
