using AccesoDatos;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Response;
using System;
using System.Collections.Generic;
using System.Text;
using Utilidades.Log4Net;

namespace Solucion_Negocio
{
    public class tbl_plan_comunicacion_negocio : crud_plancomunicacion
    {
        private plan_comunicacion_acceso_datos _Plan = new plan_comunicacion_acceso_datos();
        private readonly ILoggerManager _logger;

        public tbl_plan_comunicacion_negocio()
        {
            _logger = new LoggerManager();
        }

        public ResponseGeneric<List<tbl_pc_proveedor>> Consultar_Proveedor_contrato(String Contrato)
        {
            try
            {
                return _Plan.Consultar_Proveedor_contrato(Contrato);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_pc_proveedor>>(ex);
            }
        }

        public ResponseGeneric<tbl_pc_proveedor> Consultar_Proveedor(String idProveedor)
        {
            try
            {
                return _Plan.Consultar_Proveedor(idProveedor);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<tbl_pc_proveedor>(ex);
            }
        }

        public ResponseGeneric<List<tbl_pc_contratante>> Consultar_Contratante_contrato(String Contrato)
        {
            try
            {
                return _Plan.Consultar_Contratante_contrato(Contrato);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_pc_contratante>>(ex);
            }

        }

        


            public ResponseGeneric<List<tbl_pc_mensaje>> Consultar_MensajesComunicacion_contrato(String Contrato)
        {
            try
            {
                return _Plan.Consultar_MensajesComunicacion_contrato(Contrato);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);
                return new ResponseGeneric<List<tbl_pc_mensaje>>(ex);
            }

        }



        public ResponseGeneric<List<Crudresponse>> addProveedor(tbl_pc_proveedor_add Proveedor)
        {
            try
            {

                if (Proveedor.p_id == null || Proveedor.p_id == Guid.Empty.ToString())
                {
                    Proveedor.p_id = Guid.NewGuid().ToString();
                    Proveedor.p_opt = 2;
                }
                else
                { //Actualiza por que ya existe un id
                    Proveedor.p_opt = 3;
                }

                Proveedor.p_inclusion = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                Proveedor.p_activo = 1;

                return _Plan.addProveedor(Proveedor);
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar", ex);

                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }


        public ResponseGeneric<List<Crudresponse>> deleteProveedor(String idProveedor)
        {
            try
            {
                tbl_pc_proveedor_add prov = new tbl_pc_proveedor_add();
                prov.p_opt = 4;
                prov.p_id = idProveedor;
                prov.p_tbl_proveedor_id = Guid.NewGuid().ToString();
                prov.p_tbl_contrato_id = Guid.NewGuid().ToString();
                prov.p_tbl_tipo_audiencia_id = Guid.NewGuid().ToString();
                prov.p_inclusion= DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                prov.p_activo = 0;


                return _Plan.deleteProveedor(prov);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

    }
}
