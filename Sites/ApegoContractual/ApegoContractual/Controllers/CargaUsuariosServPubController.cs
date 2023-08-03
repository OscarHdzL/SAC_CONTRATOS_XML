using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ApegoContractual.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApegoContractual.Controllers
{
    public class CargaUsuariosServPubController : Controller
    {
        private int NUMERO_COLUMNAS = 9;
        private int LONGITUD_NOMBRE= 120;
        private int LONGITUD_APELLIDO = 60;
        private int LONGITUD_EMAIL = 500;
        private int LONGITUD_RFC = 13;

        private int INDICE_COL_CORREO = 0;
        private int INDICE_COL_NOMBRE = 1;
        private int INDICE_COL_APPATERNO = 2;
        private int INDICE_COL_APMATERNO = 3;
        private int INDICE_COL_RFC = 4;
        private int INDICE_COL_TELEFONO = 5;
        private int INDICE_COL_EXTENSION= 6;
        private int INDICE_COL_ID_ROL = 7;
        private int INDICE_COL_ID_AREA = 8;

        private string ID_ESTATUS_AUTENTICACION_PASSWORD_NO_ACTUALIZADO = "32f70c3e-37f1-11ea-82d7-00155d1b3502";

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult RealizarCargaMasiva(IFormFile archivo)
        {
            DateTime fechaActual = DateTime.Now;
            List<Usuario> lstUsuarios = new List<Usuario>();

            Usuario usuario;
            bool siguienteFila;

            List<string[]> lstRegistrosExitosos = new List<string[]>();
            List<RegistroFallido> lstRegistrosFallidos = new List<RegistroFallido>();

            string strCorreo, strNombre, strApPaterno, strApMaterno, strRFC, strTelefono, strExtension, strIdRol, strIdArea;
            string strValidacionLongitud = "La información de la celda '{0}' debe ser menor o igual a {1} caracteres.";
            string strNombreCompleto = "{0} {1} {2}";

            string jsonUsuarios = string.Empty;
            string[] fila;
            string strURIUsuarios = "http://107.178.210.37:8081/EndPoint/Usuarios/Add";
            
            HttpClient cliente;
            StringContent contenido;
            HttpResponseMessage resultado;

            using (StreamReader reader = new StreamReader(archivo.OpenReadStream(), Encoding.GetEncoding("iso-8859-1"), true))
            {
                while (!reader.EndOfStream)
                {
                    fila = reader.ReadLine().Split(',');

                    if (fila.Length == NUMERO_COLUMNAS)
                    {
                        //strIdArea = "f166d600-37c8-11ea-82d7-00155d1b3502";

                        usuario = new Usuario()
                        {
                            p_opt = 0,
                            p_activo = 1,
                            p_super_usuario = 1,
                            p_tbl_dependencia_id = HttpContext.Session.GetString("IdDependencia")
                        };

                        siguienteFila = false;

                        //Columnas de contratos
                        strCorreo = fila[INDICE_COL_CORREO].ToString();
                        strNombre = fila[INDICE_COL_NOMBRE].ToString();
                        strApPaterno = fila[INDICE_COL_APPATERNO].ToString();
                        strApMaterno = fila[INDICE_COL_APMATERNO].ToString();
                        strRFC = fila[INDICE_COL_RFC].ToString();
                        strTelefono = fila[INDICE_COL_TELEFONO].ToString();
                        strExtension = fila[INDICE_COL_EXTENSION].ToString();
                        strIdRol = fila[INDICE_COL_ID_ROL].ToString();
                        strIdArea = fila[INDICE_COL_ID_AREA].ToString();

                        //Validar que haya información en los campos requeridos
                        for (int columna = 0; columna < NUMERO_COLUMNAS; columna++)
                        {
                            if (String.IsNullOrEmpty(fila[columna].ToString()))
                            {
                                siguienteFila = true;

                                lstRegistrosFallidos.Add(new RegistroFallido()
                                {
                                    Nombre = String.Format(strNombreCompleto, strNombre, strApPaterno, strApMaterno),
                                    Correo = strCorreo,
                                    MsgError = "Las celdas del archivo deben contener información."
                                });

                                break;
                            }
                        }

                        if (siguienteFila)
                        {
                            continue;
                        }

                        //Validar longitud de campos string
                        if (strCorreo.Length > LONGITUD_EMAIL)
                        {
                            siguienteFila = true;

                            lstRegistrosFallidos.Add(new RegistroFallido()
                            {
                                Nombre = String.Format(strNombreCompleto, strNombre, strApPaterno, strApMaterno),
                                Correo = strCorreo,
                                MsgError = String.Format(strValidacionLongitud, "CORREO NUEVO USUARIO", LONGITUD_EMAIL)
                            });

                            continue;
                        }

                        if (strNombre.Length > LONGITUD_NOMBRE)
                        {
                            siguienteFila = true;

                            lstRegistrosFallidos.Add(new RegistroFallido()
                            {
                                Nombre = String.Format(strNombreCompleto, strNombre, strApPaterno, strApMaterno),
                                Correo = strCorreo,
                                MsgError = String.Format(strValidacionLongitud, "NOMBRE", LONGITUD_NOMBRE)
                            });

                            continue;
                        }

                        if (strApPaterno.Length > LONGITUD_APELLIDO)
                        {
                            siguienteFila = true;

                            lstRegistrosFallidos.Add(new RegistroFallido()
                            {
                                Nombre = String.Format(strNombreCompleto, strNombre, strApPaterno, strApMaterno),
                                Correo = strCorreo,
                                MsgError = String.Format(strValidacionLongitud, "APELLIDO PATERNO", LONGITUD_APELLIDO)
                            });

                            continue;
                        }

                        if (strApMaterno.Length > LONGITUD_APELLIDO)
                        {
                            siguienteFila = true;

                            lstRegistrosFallidos.Add(new RegistroFallido()
                            {
                                Nombre = String.Format(strNombreCompleto, strNombre, strApPaterno, strApMaterno),
                                Correo = strCorreo,
                                MsgError = String.Format(strValidacionLongitud, "APELLIDO MATERNO", LONGITUD_APELLIDO)
                            });

                            continue;
                        }

                        if (strRFC.Length > LONGITUD_RFC)
                        {
                            siguienteFila = true;

                            lstRegistrosFallidos.Add(new RegistroFallido()
                            {
                                Nombre = String.Format(strNombreCompleto, strNombre, strApPaterno, strApMaterno),
                                Correo = strCorreo,
                                MsgError = String.Format(strValidacionLongitud, "RFC", LONGITUD_RFC)
                            });

                            continue;
                        }

                        usuario.p_email = strCorreo;
                        usuario.p_nombre = strNombre;
                        usuario.p_ap_paterno = strApPaterno;
                        usuario.p_ap_materno = strApMaterno;
                        usuario.p_rfc = strRFC;
                        usuario.p_telefono = strTelefono;
                        usuario.p_extension = strExtension;
                        usuario.p_tbl_rol_id = strIdRol;
                        usuario.p_tbl_area_id = strIdArea;
                        usuario.p_tbl_estatus_autenticacion_id = ID_ESTATUS_AUTENTICACION_PASSWORD_NO_ACTUALIZADO;
                        usuario.p_tbl_area_id = strIdArea;

                        string[] p_usuario = strCorreo.Split('@');
                        usuario.p_usuario = p_usuario[0];

                        //lstUsuarios.Add(usuario);

                        cliente = new HttpClient();
                        jsonUsuarios = JsonConvert.SerializeObject(usuario);
                        contenido = new StringContent(jsonUsuarios, Encoding.UTF8, "application/json");

                        resultado = cliente.PostAsync(strURIUsuarios, contenido).Result;
                        List<Respuesta> respuesta = JsonConvert.DeserializeObject<List<Respuesta>>(resultado.Content.ReadAsStringAsync().Result);

                        if (resultado.IsSuccessStatusCode && respuesta[0].cod == "success")
                        {
                            lstRegistrosExitosos.Add(fila);
                        }
                        else
                        {
                            lstRegistrosFallidos.Add(new RegistroFallido()
                            {
                                Nombre = String.Format(strNombreCompleto, strNombre, strApPaterno, strApMaterno),
                                Correo = strCorreo,
                                MsgError = respuesta[0].msg
                            });
                        }
                    }
                }

                return Json(new { Exitoso = true, Excepcion = string.Empty, Contenido = new { lstRegistrosExitosos, lstRegistrosFallidos } });
            }
        }

        private bool ValidarFecha(string strFecha)
        {
            DateTime fechaValidada;

            if (DateTime.TryParse(strFecha, out fechaValidada))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}