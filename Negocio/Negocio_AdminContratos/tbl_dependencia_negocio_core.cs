using System;
using System.Collections.Generic;
using System.Text;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Dependencia;
using Modelos.Response;
using AccesoDatos_AdministracionDeContratos;
using Utilidades.Log4Net;

namespace NegocioAdministracionContratos
{
    public class tbl_dependencia_negocio_core : crud_dependencia
    {
        private tbl_dependencia_acceso_datos_core _tbl_dependencia_acceso_datos_core = new tbl_dependencia_acceso_datos_core();
        private readonly ILoggerManager _logger;

        public tbl_dependencia_negocio_core()
        {
            _logger = new LoggerManager();
        }
        public ResponseGeneric<List<tbl_dependencia>> Get(string dependencia)
        {
            try
            {
                return _tbl_dependencia_acceso_datos_core.Get(dependencia);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_dependencia>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_dependencia_x_permiso>> GetXPermisoUsuario(String dependencia, String idusuario)
        {
            try
            {
                return _tbl_dependencia_acceso_datos_core.GetXPermisoUsuario(dependencia, idusuario);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetXPermisoUsuario", ex);
                return new ResponseGeneric<List<tbl_dependencia_x_permiso>>(ex);
            }
        }

        

        public ResponseGeneric<List<tbl_partida_list>> Get_Lista_Partidas(string id_ejercicio, string id_instancia, string id_dependencia)
        {
            try
            {
                return _tbl_dependencia_acceso_datos_core.Get_Lista_Partidas(id_ejercicio, id_instancia, id_dependencia);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_partida_list>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Add(tbl_dependencia dependencia)
        {
            try
            {
                if (dependencia.id == null || dependencia.id == Guid.Empty.ToString() || dependencia.id == "")
                {
                    dependencia.id = Guid.NewGuid().ToString();
                }
                return _tbl_dependencia_acceso_datos_core.Add(dependencia);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Add_Partida(tbl_partida _tbl_partida)
        {
            try
            {
                return _tbl_dependencia_acceso_datos_core.Add_Partida(_tbl_partida);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<CrudresponseIdentificador>> Update_Partida(List<tbl_partida_upd> _tbl_partida)
        {
            try
            {
                List<CrudresponseIdentificador> respuesta = new List<CrudresponseIdentificador>();
                foreach (tbl_partida_upd lista in _tbl_partida) {
                    var monto_total = _tbl_dependencia_acceso_datos_core.Get_suma_areas_p(lista.tbl_partida_dependencia_id.ToString()).Response[0].monto_total;

                    if (lista.monto_asignado >= monto_total)
                    {
                        tbl_partida_upd nuevo_reg = new tbl_partida_upd();
                        nuevo_reg.tbl_partida_dependencia_id = lista.tbl_partida_dependencia_id;
                        nuevo_reg.monto_asignado = lista.monto_asignado;
                        ResponseGeneric<List<CrudresponseIdentificador>> Step1 = _tbl_dependencia_acceso_datos_core.Update_Partida(nuevo_reg);
                        respuesta.Add(Step1.Response[0]);
                    }
                    else 
                    {
                        CrudresponseIdentificador respuesta_conpuesta = new CrudresponseIdentificador();
                        respuesta_conpuesta.cod = "error";
                        respuesta_conpuesta.msg = "Monto inferior al comprometido";
                        respuesta_conpuesta.id = lista.tbl_partida_dependencia_id;
                        respuesta.Add(respuesta_conpuesta);
                    }
                    
                }
                return new ResponseGeneric<List<CrudresponseIdentificador>>(respuesta);
            }
            catch (Exception ex)
            {
                _logger.LogError("upd", ex);
                return new ResponseGeneric<List<CrudresponseIdentificador>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> update(tbl_dependencia dependencia)
        {
            try
            {
                return _tbl_dependencia_acceso_datos_core.update(dependencia);
            }
            catch (Exception ex)
            {
                _logger.LogError("upd", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> delete(tbl_dependencia dependencia)
        {
            try
            {               
                return _tbl_dependencia_acceso_datos_core.delete(dependencia);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> delete_partida(tbl_partida _tbl_partida)
        {
            try
            {
                return _tbl_dependencia_acceso_datos_core.delete_partida(_tbl_partida);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<DropDownList>> FillDropC()
        {
            try
            {
                return _tbl_dependencia_acceso_datos_core.FillDropC();
            }
            catch (Exception ex)
            {
                _logger.LogError("fill", ex);
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }
        public ResponseGeneric<List<DropDownList>> get_tbl_estado()
        {
            try
            {

                return _tbl_dependencia_acceso_datos_core.get_tbl_estado();
            }
            catch (Exception ex)
            {
                _logger.LogError("get", ex);
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }

        public ResponseGeneric<List<DropDownList>> get_tbl_estado_ciudad(Guid id)
        {
            try
            {

                return _tbl_dependencia_acceso_datos_core.get_tbl_estado_ciudad(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("get", ex);
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }
        public ResponseGeneric<List<DropDownList>> get_Ejercicios()
        {
            try
            {

                return _tbl_dependencia_acceso_datos_core.get_Ejercicios();
            }
            catch (Exception ex)
            {
                _logger.LogError("get", ex);
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }
        public ResponseGeneric<List<DropDownList>> FillDrop(string instancia)
        {
            throw new NotImplementedException();
        }
    }
}
