using AccesoDatos;
using AccesoDatos_SistemaAdquisiciones;
using Microsoft.Extensions.Configuration;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Contrato;
using Modelos.Response;
using Newtonsoft.Json;
using SistemaDeAdquisiciones.Util.EnvioSmtp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Negocio_SistemaAdquisiciones
{
    public class cotizaciones_negocio
    {

        private Cotizaciones_datos _datos = new Cotizaciones_datos();
        public IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        public ResponseGeneric<List<Crudresponse>> Add(List<string> proveedores, List<string> archivos_adjuntos, string solicitud)
        {
            try
            {

                string links = "";
                foreach (string link in archivos_adjuntos)
                {                    
                    links = links + "<p>" + link + "</p>";
                }

                //tbl_proveedor consultauser = new tbl_proveedor();

                //List<string> lista_proveedores = new List<string>();
                foreach (string item in proveedores)
                {
                     var consultauser = _datos.obtener_datos_proveedr(item);


                    List<String> Correo = new List<String>();
                    Correo.Add(consultauser.Response.correo_electronico);
                    //Correo.Add("alejandro.rocha@people-media.com.mx");

                    string Body = $"<p>Se ha solicitado la cotizacion con los siguientes adjuntos:</p><br/>";

                    Body += links;
                    Body += $"<br/><p>Este correo ha sido enviado automáticamente por el Sistema SACPRO, no responder este mensaje.</p>";

                    string Asunto = "Solicitud de cotizacion a proveedores.";

                    string SMTPHost = Configuration["SMTPHost"].ToString();
                    string remitenteSMTP = Configuration["remitenteSMTP"].ToString();
                    string SMTPPass = Configuration["SMTPPass"].ToString();
                    int Port = int.Parse(Configuration["Port"].ToString());

                    //EnvioCorreos.SendEmail("webmail.pmsoluciones.mx", "sentrega@pmsoluciones.mx", "Ventana1010", 465, Correo, Asunto, Body);
                    EnvioCorreos.SendEmail(SMTPHost, remitenteSMTP, SMTPPass, Port, Correo, Asunto, Body);

                }

                var sol_actual = _datos.update_sol_metodo("Solic_cotizacion", solicitud,"null");


                return sol_actual;

            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }
        }


        public ResponseGeneric<List<tbl_cotizacion_solicitud>> Get_documentos_cotizacion(string id_solicitud)
        {
            try
            {
                return _datos.Get_documentos_cotizacion(id_solicitud);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<tbl_cotizacion_solicitud>>(ex);
            }

        }

        public ResponseGeneric<List<Crudresponse>> Add_cotizacion(tbl_cotizacion_sol_crud cotizacion_sol)
        {
            try
            {
                if (cotizacion_sol.p_id == null || cotizacion_sol.p_id == "")
                {
                    cotizacion_sol.p_id = Guid.NewGuid().ToString();
                }
                cotizacion_sol.p_opt = 2;
                return _datos.crud_cotizacion(cotizacion_sol);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }

        }

        public ResponseGeneric<List<Crudresponse>> Delete_cotizacion(string id_cotizacion_solic)
        {
            try
            {
                tbl_cotizacion_sol_crud cotizacion_sol = new tbl_cotizacion_sol_crud();
                cotizacion_sol.p_id = id_cotizacion_solic;
                cotizacion_sol.p_opt = 4;
                cotizacion_sol.p_tbl_proveedor_id = "";
                cotizacion_sol.p_tbl_solicitud_documento_adjunto_id = "";
                cotizacion_sol.p_tbl_tipo_documento_id = "";
                cotizacion_sol.p_tbl_solicitud_id = "";
                cotizacion_sol.p_nom_documento = "";
                return _datos.crud_cotizacion(cotizacion_sol);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<List<Crudresponse>>(ex);
            }

        }
        //public ResponseGeneric<tbl_proveedor> obtener_datos_proveedr(string id_prov)
        //{
        //    try
        //    {
        //        return _datos.obtener_datos_proveedr(id_prov);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResponseGeneric<tbl_proveedor>(ex);
        //    }

        //}


    }
}
