using System;
using System.Collections.Generic;
using System.Text;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Contrato;
using Modelos.Response;
using AccesoDatos_AdminContratos;
using Utilidades.Log4Net;

namespace Negocio_AdminContratos
{
    public class tbl_proveedor_negocio
    {
        private tbl_proveedor_acceso_datos_core _tbl_Proveedor = new tbl_proveedor_acceso_datos_core();
        private readonly ILoggerManager _logger;

        public tbl_proveedor_negocio()
        {
            _logger = new LoggerManager();
        }
        public ResponseGeneric<List<Crudresponse>> Add(tbl_proveedor_add proveedor_add)
        {
            try
            {
                if (proveedor_add.p_opt == 2) {
                    proveedor_add.p_id = Guid.NewGuid();
                }
                return _tbl_Proveedor.Add(proveedor_add);
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<Crudresponse>> Delete(tbl_proveedor_add proveedor_add)
        {
            try
            {
                 return _tbl_Proveedor.Delete(proveedor_add);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }
        public ResponseGeneric<List<lista_proveedores_estructura>> Get_lista_proveedores_contrato(String id_instancia)
        {
            try
            {
                List<lista_proveedores_estructura> _lista_proveedores_estructura = new List<lista_proveedores_estructura>();
                ResponseGeneric<List<tbl_proveedor>> lista_p = _tbl_Proveedor.Get_lista_proveedores(id_instancia);
                List<tbl_proveedor> nueva_lista = lista_p.Response;
                foreach (tbl_proveedor _Proveedor in nueva_lista) {
                    lista_proveedores_estructura lista_ = new lista_proveedores_estructura();
                    lista_._tbl_proveedor = new tbl_proveedor();
                    lista_._tbl_proveedor = _Proveedor;
                    ResponseGeneric<List<tbl_contrato>> lista_c_p = _tbl_Proveedor.Get_lista_contratos_p(_Proveedor.id.ToString());
                    List<tbl_contrato> listacontratos = lista_c_p.Response;
                    lista_._tbl_contrato = listacontratos;
                    _lista_proveedores_estructura.Add(lista_);
                }
                return new ResponseGeneric<List<lista_proveedores_estructura>>(_lista_proveedores_estructura);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<lista_proveedores_estructura>>(ex);
            }
        }
        public ResponseGeneric<List<tbl_proveedor>> Get_proveedor_id(string id_proveedor)
        {
            try
            {
                return _tbl_Proveedor.Get_lista_proveedores_by_id(id_proveedor);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_proveedor>>(ex);
            }
        }

        public ResponseGeneric<List<lista_tipo_interlocutor>> Get_lista_tipo_interlocutor(string activo)
        {

            try
            {
                return _tbl_Proveedor.Get_lista_interlocutores(activo);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<lista_tipo_interlocutor>>(ex);
            }
            //try
            //{
            //    List<lista_tipo_interlocutor> _lista_tipo_interlocutor = new List<lista_tipo_interlocutor>();
            //    ResponseGeneric<List<lista_tipo_interlocutor>> _lista_tipo_interlocutor = _tbl_Proveedor.Get_lista_interlocutores(activo);
            //    //List<tbl_proveedor> nueva_lista = lista_t_i.Response;
            //    //foreach (tbl_proveedor _Interlocutor in nueva_lista)
            //    //{
            //    //    lista_tipo_interlocutor lista_ = new lista_tipo_interlocutor();
            //    //    lista_._tbl_Proveedor = new tbl_proveedor();
            //    //    lista_._tbl_Proveedor = _Interlocutor;
            //    //    ResponseGeneric<List<tbl_contrato>> lista_c_p = _tbl_Proveedor.Get_lista_contratos_p(_Interlocutor.id.ToString());
            //    //    List<tbl_contrato> listacontratos = lista_c_p.Response;
            //    //    lista_._tbl_Proveedor = listacontratos;
            //    //    _lista_tipo_interlocutor.Add(lista_);
            //    //}
            //    return new ResponseGeneric<List<lista_tipo_interlocutor>>(_lista_tipo_interlocutor);
            //}
            //catch (Exception ex)
            //{
            //    return new ResponseGeneric<List<lista_tipo_interlocutor>>(ex);
            //}
        }

        public ResponseGeneric<List<proveedor_dependencia>> proveedor_dependencias(String proveedor)
        {
            try
            {
                return _tbl_Proveedor.proveedor_dependencias(proveedor);
            }
            catch (Exception ex)
            {
                _logger.LogError("proveedor_dependencias", ex);
                return new ResponseGeneric<List<proveedor_dependencia>>(ex);
            }
        }


    }
}
