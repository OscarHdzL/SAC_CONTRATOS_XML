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
    public class licitante_negocio
    {
        private licitante_acceso_datos _licitante = new licitante_acceso_datos();
        

        public ResponseGeneric<List<tbl_proveedor>> GetProveedores_instancia(string instancia)
        {
            try
            {
                return _licitante.GetProveedores_instancia(instancia);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_proveedor>>(ex);
            }
        }



        public ResponseGeneric<Crudresponse> AddLicitante(tbl_licitante_add licitante)
        {
            try
            {
                licitante.p_opt = 2;
                return _licitante.AddLicitante(licitante);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<Crudresponse>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> DeleteLicitante(tbl_licitante_add licitante)
        {
            try
            {
                licitante.p_opt = 4;
                return _licitante.DeleteLicitante(licitante);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<Crudresponse> ValidaLicitante(String RFC, String Solicitud)
        {
            try
            {
                tbl_proveedor ProvExistente = new tbl_proveedor();
                ProvExistente = _licitante.GetProveedor_RFC(RFC).Response;
                List<Crudresponse> lista = new List<Crudresponse>();
                Crudresponse response = new Crudresponse();

                if (ProvExistente != null) {

                    tbl_licitante_add lic = new tbl_licitante_add();
                    lic.p_opt = 2;
                    lic.p_id = Guid.NewGuid().ToString();
                    lic.p_tbl_solicitud_id = Solicitud;
                    lic.p_razon_social = ProvExistente.razon_social;
                    lic.p_rep_legal_nombre = ProvExistente.rep_legal_nombre;
                    lic.p_rep_legal_ap_paterno = ProvExistente.rep_legal_ap_paterno;
                    lic.p_rep_legal_ap_materno = ProvExistente.rep_legal_ap_materno;
                    lic.p_rfc = ProvExistente.rfc;
                    lic.p_es_proveedor = 1;
                    response = _licitante.AddLicitante(lic).Response;
                    
                } else {
                    response.cod = "NoExiste";
                    response.msg = "No existe proveedor";
                    //lista.Add(response);
                }
                
                return new ResponseGeneric<Crudresponse>(response);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<Crudresponse>(ex);
            }
        }

        public ResponseGeneric<List<tbl_licitante>> GetLicitantes_Solicitud(String Solicitud)
        {
            try
            {
                return _licitante.GetLicitantes_Solicitud(Solicitud);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_licitante>>(ex);
            }
        }

        ////PROPUESTA LICITANTE
        ///

        public ResponseGeneric<Crudresponse> AddPropuestaLicitante(tbl_licitante_propuesta_add licitante_propuesta)
        {
            try
            {
                licitante_propuesta.p_opt = 2;
                return _licitante.AddPropuestaLicitante(licitante_propuesta);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<Crudresponse>(ex);
            }
        }
        public ResponseGeneric<Crudresponse> AddPropuestaLicitante_C(tbl_licitante_propuesta_add licitante_propuesta)
        {
            try
            {
                licitante_propuesta.p_id = Guid.NewGuid().ToString();
                return _licitante.AddPropuestaLicitante_C(licitante_propuesta);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<Crudresponse>(ex);
            }
        }

        public ResponseGeneric<List<licitante_propuesta>> GetLicitantes_Propuesta_Solicitud(String Solicitud, String TipoPropuesta)
        {
            try
            {
                return _licitante.GetLicitantes_Propuesta_Solicitud(Solicitud, TipoPropuesta);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<licitante_propuesta>>(ex);
            }
        }
    }
}