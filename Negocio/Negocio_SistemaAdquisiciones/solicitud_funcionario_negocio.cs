using AccesoDatos_SistemaAdquisiciones;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Contrato;
using Modelos.Response;
using System;
using System.Collections.Generic;
using
System.Text;

namespace Negocio_SistemaAdquisiciones
{
    public class solicitud_funcionario_negocio 
    {
        private solicitud_funcionarios_acceso_datos _funcionarios = new solicitud_funcionarios_acceso_datos();

        public ResponseGeneric<List<DropDownList>> GetServidores(String instancia)
        {
            try
            {
                return _funcionarios.GetServidores(instancia);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }

        public ResponseGeneric<List<solicitud_funcionario>> GetFuncionariosSolicitud(String solicitud, String tipo_acta, String programacion)
        {
            try
            {
                return _funcionarios.GetFuncionariosSolicitud(solicitud, tipo_acta, programacion);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<solicitud_funcionario>>(ex);
            }
        }

        public ResponseGeneric<Crudresponse> Add(solicitud_funcionario_add funcionario)
        {
            try
            {
                funcionario.p_opt = 2;
                funcionario.p_id = Guid.NewGuid().ToString();

                return _funcionarios.Add(funcionario);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<Crudresponse>(ex);
            }
        }

        public ResponseGeneric<Crudresponse> Delete(solicitud_funcionario_add funcionario)
        {
            try
            {
                return _funcionarios.Delete(funcionario);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<Crudresponse>(ex);
            }
        }


    }
}