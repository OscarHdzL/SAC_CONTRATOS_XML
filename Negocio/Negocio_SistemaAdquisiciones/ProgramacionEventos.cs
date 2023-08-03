
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
    public class ProgramacionEventosNegocio
    {
        private AccesoDatos.ProgramacionEventos _ProgramacionEventos = new AccesoDatos.ProgramacionEventos();
        
        public ResponseGeneric<List<Crudresponse>> add(ProgramacionEventosEntidad ProgramacionEventosEntidad_)
        {
            try
            {
                return _ProgramacionEventos.add(ProgramacionEventosEntidad_);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
    
        public ResponseGeneric<List<DropDownList>> get_sp_tbl_tipo_programacion()
        {
            try
            {
                return _ProgramacionEventos.get_sp_tbl_tipo_programacion();
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }
        public ResponseGeneric<List<DropDownList>> get_sp_getciudad(Guid id)
        {
            try
            {
                return _ProgramacionEventos.get_sp_getciudad(id);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }
        public ResponseGeneric<List<DropDownList>> tipo_pro_x_sigla(string sigla)
        {
            try
            {
                return _ProgramacionEventos.tipo_pro_x_sigla(sigla);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }
        public ResponseGeneric<List<DropDownList>> get_sp_getestado()
        {
            try
            {
                return _ProgramacionEventos.get_sp_getestado();
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_agendados>> get_sp_eventos_agendados(Guid id,Guid tipo)
        {
            try
            {
                return _ProgramacionEventos.get_sp_eventos_agendados(id, tipo);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_agendados>>(ex);
            }
        }
        public ResponseGeneric<tbl_programacion> get_ultimo_evento_activo(String solicitud, String tipo)
        {
            try
            {
                return _ProgramacionEventos.get_ultimo_evento_activo(solicitud, tipo);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<tbl_programacion>(ex);
            }
        }

    }
}
