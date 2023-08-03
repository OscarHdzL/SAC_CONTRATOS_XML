using AccesoDatos_SistemaAdquisiciones;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio_SistemaAdquisiciones
{
    public class tbl_juntaclaraciones_negocio : crud_juntaclaraciones
    {
        private tbl_juntaclaraciones_acceso_datos _acceso_datos = new tbl_juntaclaraciones_acceso_datos();

        public ResponseGeneric<List<Crudresponse>> Add_Obs(tbl_solicitud_observador _tbl_solicitud_observador)
        {
            try
            {
                if (_tbl_solicitud_observador.id == null || _tbl_solicitud_observador.id == Guid.Empty.ToString() || _tbl_solicitud_observador.id == "")
                {
                    _tbl_solicitud_observador.id = Guid.NewGuid().ToString();
                }
                return _acceso_datos.Add_Obs(_tbl_solicitud_observador);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Add_Junta(tbl_junta_aclaraciones _tbl_junta_aclaraciones)
        {
            try
            {
                if (_tbl_junta_aclaraciones.id == null || _tbl_junta_aclaraciones.id == Guid.Empty.ToString() || _tbl_junta_aclaraciones.id == "")
                {
                    _tbl_junta_aclaraciones.id = Guid.NewGuid().ToString();
                }
                return _acceso_datos.Add_Junta(_tbl_junta_aclaraciones);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_solicitud_observador_list>> Get_Obs(string id_sol, string tipo_acta, string prog)
        {
            try
            {
                return _acceso_datos.Get_Obs(id_sol, tipo_acta, prog);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_solicitud_observador_list>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_junta_aclaraciones_list>> Get_Juntas(string id_sol)
        {
            try
            {
                return _acceso_datos.Get_Juntas(id_sol);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_junta_aclaraciones_list>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Delete_Obs(string id_sol_obs)
        {
            try
            {
                return _acceso_datos.Delete_Obs(id_sol_obs);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Delete_Junta(string id_junta_acl)
        {
            try
            {
                return _acceso_datos.Delete_Junta(id_junta_acl);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
    }
}
