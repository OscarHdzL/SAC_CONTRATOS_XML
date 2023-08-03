using System;
using System.Collections.Generic;
using System.Text;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Dependencia;
using Modelos.Response;
using AccesoDatos_AdminContratos;
using Utilidades.Log4Net;

namespace Negocio_AdminContratos
{
    public class tbl_instancia_negocio : crud_instancia
    {
        private tbl_instancia_acceso_datos _acceso_datos = new tbl_instancia_acceso_datos();
        private readonly ILoggerManager _logger;

        public tbl_instancia_negocio()
        {
            _logger = new LoggerManager();
        }
        public ResponseGeneric<List<DropDownList>> FillDropC()
        {
            try
            {
                return _acceso_datos.FillDropC();
            }
            catch (Exception ex)
            {
                _logger.LogError("fill", ex);
                return new ResponseGeneric<List<DropDownList>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> Add(tbl_instancia_contrato ins)
        {
            try
            {
                if (ins.id == null || ins.id == Guid.Empty.ToString() || ins.id == "")
                {
                    ins.id = Guid.NewGuid().ToString();
                }
                ins.opt = 2;
                return _acceso_datos.Eject(ins);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> update(tbl_instancia_contrato ins)
        {
            try
            {
                ins.opt = 3;
                return _acceso_datos.Eject(ins);
            }
            catch (Exception ex)
            {
                _logger.LogError("upd", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> delete(tbl_instancia_contrato ins)
        {
            try
            {
                ins.opt = 4;
                return _acceso_datos.Eject(ins);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<tbl_instancia_contrato_get> Get(string Instancia)
        {
            try
            {
                return _acceso_datos.Get(Instancia);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<tbl_instancia_contrato_get>(ex);
            }
        }
    }
}
