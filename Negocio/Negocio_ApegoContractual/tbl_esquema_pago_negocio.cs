using AccesoDatos;
using Conexion;
using Microsoft.Extensions.Configuration;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.EsquemaPago;
using Modelos.Modelos.PreguntasFormulario;
using Modelos.Response;

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Utilidades.Log4Net;
using Solucion_Negocio.Util.EnvioSmtp;

namespace Solucion_Negocio
{
    public class tbl_esquema_pago_negocio : crud_esquemapago
    {
        private tbl_esquema_pago_acceso_datos _esquemas = new tbl_esquema_pago_acceso_datos();
        private readonly ILoggerManager _logger;
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();

        public tbl_esquema_pago_negocio()
        {
            _logger = new LoggerManager();
        }

        public ResponseGeneric<List<tbl_esquema_pago_new>> ConsultarEsquemasPago(String contrato)
        {
            try
            {
                return _esquemas.ConsultarEsquemasPago(contrato);
            }
            catch (Exception ex)
            {
                _logger.LogError("ConsultarEsquemasPago", ex);
                return new ResponseGeneric<List<tbl_esquema_pago_new>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_proveedores_esquemapago>> ConsultarProveedores_Contrato(String contrato)
        {
            try
            {
                return _esquemas.ConsultarProveedores_Contrato(contrato);
            }
            catch (Exception ex)
            {
                _logger.LogError("ConsultarProveedores_Contrato", ex);
                return new ResponseGeneric<List<tbl_proveedores_esquemapago>>(ex);
            }
        }


        public ResponseGeneric<tbl_esquema_pago_new> ConsultarEsquemaPago_esquemacontrato(String esquema, String contrato)
        {
            try
            {
                return _esquemas.ConsultarEsquemaPago_esquemacontrato(esquema, contrato);
            }
            catch (Exception ex)
            {
                _logger.LogError("ConsultarEsquemaPago_esquemacontrato", ex);
                return new ResponseGeneric<tbl_esquema_pago_new>(ex);
            }
        }

        public ResponseGeneric<tbl_instancia> ConsultarInstancia(String instancia)
        {
            try
            {
                return _esquemas.ConsultarInstancia(instancia);
            }
            catch (Exception ex)
            {
                _logger.LogError("ConsultarInstancia", ex);
                return new ResponseGeneric<tbl_instancia>(ex);
            }
        }


        public ResponseGeneric<List<Crudresponse>> add(tbl_esquema_pago_add Esquema)
        {
            try
            {
                if (Esquema.p_id == null || Esquema.p_id == Guid.Empty.ToString())
                {
                    Esquema.p_id = Guid.NewGuid().ToString();
                    Esquema.p_opt = 2;
                     

                }
                else
                { //Actualiza por que ya existe un id
                    Esquema.p_opt = 3;
                }

                Esquema.p_inclusion = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                Esquema.p_estatus = 1;
                var response = _esquemas.add(Esquema);

                if (response.Response[0].cod == "success")
                {
                    try
                    {
                        var info = _esquemas.InfoEsquemaCorreo(Esquema.p_id);
                        List<String> Correo = new List<String>();
                        Correo.Add(info.Response[0].notificacion_correo);//obtener correo del proveeodr

                        string Body = $"<p>Se ha dado de alta un nuevo esquema de pago para el responsable <strong> {info.Response[0].nombre_responsable} </strong>con los siguientes datos:</p>";

                        Body += $"<p>Proveedor:\t{info.Response[0].razon_social}</p>";
                        Body += $"<p>Fecha de pago:\t{info.Response[0].fecha_pago}</p>";
                        Body += $"<p>Total:\t{info.Response[0].total}</p>";
                        Body += $"<p>Este correo ha sido enviado automáticamente por el Sistema SACPRO, no responder este mensaje.</p>";

                        string Asunto = "Alta de esquema de pago.";

                        string SMTPHost = Configuration["SMTPHost"].ToString();
                        string remitenteSMTP = Configuration["remitenteSMTP"].ToString();
                        string SMTPPass = Configuration["SMTPPass"].ToString();
                        int Port = int.Parse(Configuration["Port"].ToString());
                        EnvioCorreos correos = new EnvioCorreos();
                        correos.SendEmail(SMTPHost, remitenteSMTP, SMTPPass, Port, Correo, Asunto, Body);
                    }
                    catch (Exception ex) {
                        _logger.LogError("Envío de correos esquema de pago", ex);
                    }
                    
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("add", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<Crudresponse>> update(tbl_esquema_pago_add Esquema)
        {
            try
            {
                Esquema.p_opt = 3;
                Esquema.p_estatus = 1;

                return _esquemas.update(Esquema);
            }
            catch (Exception ex)
            {
                _logger.LogError("update", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }



        public ResponseGeneric<List<Crudresponse>> delete(string Esquema)
        {
            try
            {
                tbl_esquema_pago_add objEsquema = new tbl_esquema_pago_add();
                objEsquema.p_id = Esquema;
                objEsquema.p_fecha_pago = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                objEsquema.p_inclusion = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
                objEsquema.p_opt = 4;
                objEsquema.p_estatus = 0;

                return _esquemas.delete(objEsquema);
            }
            catch (Exception ex)
            {
                _logger.LogError("delete", ex);
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_planes_sin_esquema>> ConsultarPlanes_Sin_Esquema(String contrato)
        {
            try
            {
                return _esquemas.ConsultarPlanes_Sin_Esquema(contrato);
            }
            catch (Exception ex)
            {
                _logger.LogError("ConsultarPlanes_Sin_Esquema", ex);
                return new ResponseGeneric<List<tbl_planes_sin_esquema>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_planes_sin_esquema>> ConsultarPlan_Del_Esquema(String idEsquema)
        {
            try
            {
                return _esquemas.ConsultarPlan_Del_Esquema(idEsquema);
            }
            catch (Exception ex)
            {
                _logger.LogError("ConsultarPlan_Del_Esquema", ex);
                return new ResponseGeneric<List<tbl_planes_sin_esquema>>(ex);
            }
        }

        public ResponseGeneric<List<tbl_proveedor>> get_cuentas_pagar(Guid id_dependencia)
        {
            try
            {
                return _esquemas.get_cuentas_pagar(id_dependencia);
            }
            catch (Exception ex)
            {
                _logger.LogError("get_cuentas_pagar", ex);
                return new ResponseGeneric<List<tbl_proveedor>>(ex);
            }
        }

    }
}
