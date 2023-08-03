using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.IO;
using ApegoContractual.Models;

namespace ApegoContractual.Controllers
{
    public class ContratoController : Controller
    {
        //private int NUMERO_FILA_INICIAL = 1;
        private int NUMERO_COLUMNAS = 23;
        //private int NUMERO_COLUMNAS_PARTIDAS = 6;
        //private int VERDADERO = 1;
        //private int FALSO = 0;
        //private int INVALIDO = -1;
        private int LONGITUD_NUMERO = 100;
        private int LONGITUD_OBJETO = 2000;
        private int LONGITUD_NOMBRE = 400;
        //private int LONGITUD_NUMPARTIDA = 10;
        //private int LONGITUD_DESCPARTIDA = 250;
        //private int ID_ALERTAMIENTO = 1;
        private int INDICE_COL_PROYECTO = 0;
        private int INDICE_COL_PRIORIDAD = 1;
        private int INDICE_COL_PROCEDIMIENTO = 2;
        private int INDICE_COL_ESTATUSCONTRATO = 3;
        private int INDICE_COL_TIPOCONTRATO = 4;
        private int INDICE_COL_NUMERO = 5;
        private int INDICE_COL_OBJETO = 6;
        private int INDICE_COL_FECHAFIRMA = 7;
        private int INDICE_COL_FECHAINICIO = 8;
        private int INDICE_COL_FECHAFIN = 9;
        private int INDICE_COL_FECHAFORMALIZACION = 10;
        private int INDICE_COL_ESAMPLIACION = 11;
        private int INDICE_COL_REQUIERERENOVACION = 12;
        private int INDICE_COL_ESSATISFACTORIO = 13;
        private int INDICE_COL_PRESENTOGARANTIA = 14;
        private int INDICE_COL_ESADMINISTRADORA = 15;
        private int INDICE_COL_PORMAXPENALIZACION = 16;
        private int INDICE_COL_PORCMAXDEDUCTIVAS = 17;
        private int INDICE_COL_PORCGARANTIA = 18;
        private int INDICE_COL_MONTOGARANTIA = 19;
        private int INDICE_COL_NOMBRE = 20;
        private int INDICE_COL_MONTOMAXSINIVA = 21;
        private int INDICE_COL_MONTOMINSINIVA = 22;
        //private int INDICE_COL_NUMPARTIDA = 16;
        //private int INDICE_COL_DESCPARTIDA = 17;
        //private int INDICE_COL_MONTO = 18;
        //private int INDICE_COL_AREA = 19;
        //private int INDICE_COL_IDPARTIDA = 20;
        //private int INDICE_COL_EJERCICIO = 21;

        public IActionResult Index()
        {
            HttpContext.Session.SetString("IdContrato", "");
            return View();
        }

        public ActionResult Responsables()
        {
            return View();
        }
        public ActionResult AsignacionResponsabilidades(Guid id)
        {
            return View(id);
        }
        public ActionResult ContratoProducto(Guid id)
        {        
            return View(id);
        }

        public ActionResult RealizarCargaMasiva(IFormFile archivo)
        {
            DateTime fechaFirma, fechaInicio, fechaFin, fechaFormalizacion;
            DateTime fechaActual = DateTime.Now;
            Nullable<sbyte> esAmpliacion, requiereRenovacion, esSatisfactorio, presentoGarantia, esAdministradora;
            string idPrioridad, idProcedimiento, idEstatusContrato, idTipoContrato;
            string idProyecto, idContrato, idArea;
            //bool esMontoDecimal, esEjercicioNumerico, esIdPartidaNumerico;

            List<Nullable<sbyte>> lstBinarios;
            List<bool> lstBinariosFecha;
            List<Partida> lstPartidas = new List<Partida>();
            List<Contrato> lstContratos = new List<Contrato>();
            List<Contrato> lstContratosUnicos;

            List<string[]> values = new List<string[]>();

            string strNumero, strObjeto, strFechaFirma, strFechaInicio, strFechaFin, strFechaFormalizacion;
            //string strNumPartida, strDescPartida, strMonto, strIdPartida, strEjercicio;
            string strPorcMaxPenalizacion, strPorcMaxDeductivas, strPorcGarantia, strMontoGarantia, strNombre, strMontoMaxSinIVA, strMontoMinSinIVA;

            string strValidacionLongitud = "La información de la celda '{0}' debe ser menor o igual a {1} caracteres.";
            string jsonContrato = string.Empty;
            string[] fila;
            string strURIContratos = "http://107.178.210.37:8081/EndPoint/SerContrato/CargaMasiva";
            Guid guidOutput;
            HttpClient cliente;
            StringContent contenido;
            HttpResponseMessage resultado;

            Contrato contrato;

            //using(StreamReader reader = new StreamReader("C:\\Users\\arochac\\Desktop\\Ejemplo.csv"))
            using (StreamReader reader = new StreamReader(archivo.OpenReadStream(), Encoding.GetEncoding("iso-8859-1"), true))
            {
                while (!reader.EndOfStream)
                {
                    fila = reader.ReadLine().Split(',');

                    if (fila.Length == NUMERO_COLUMNAS)
                    {
                        //Inicializar registro de producto
                        //contrato = new Contrato()
                        //{
                        //    p_opt = "0",
                        //    p_id = "null",
                        //    p_activo = "1",
                        //    p_tbl_dependencia_id = "442a01d4-3639-11ea-82d7-00155d1b3502" //hardcode
                        //};

                        contrato = new Contrato()
                        {
                            p_id = Guid.Empty.ToString(),
                            p_activo = 1,
                            p_token = Guid.NewGuid().ToString(),
                            p_fecha_registro = DateTime.Now.ToString("yyyy-MM-dd")
                        };

                        //Columnas de contratos
                        strNumero = fila[INDICE_COL_NUMERO].ToString();
                        strObjeto = fila[INDICE_COL_OBJETO].ToString();
                        strFechaFirma = fila[INDICE_COL_FECHAFIRMA].ToString();
                        strFechaInicio = fila[INDICE_COL_FECHAINICIO].ToString();
                        strFechaFin = fila[INDICE_COL_FECHAFIN].ToString();
                        strFechaFormalizacion = fila[INDICE_COL_FECHAFORMALIZACION].ToString();

                        strPorcMaxPenalizacion = fila[INDICE_COL_PORMAXPENALIZACION].ToString(); 
                        strPorcMaxDeductivas = fila[INDICE_COL_PORCMAXDEDUCTIVAS].ToString(); 
                        strPorcGarantia = fila[INDICE_COL_PORCGARANTIA].ToString(); 
                        strMontoGarantia = fila[INDICE_COL_MONTOGARANTIA].ToString();
                        strNombre = fila[INDICE_COL_NOMBRE].ToString();
                        strMontoMaxSinIVA = fila[INDICE_COL_MONTOMAXSINIVA].ToString();
                        strMontoMinSinIVA = fila[INDICE_COL_MONTOMINSINIVA].ToString();

                        //Columnas de partidas
                        //strNumPartida = fila[INDICE_COL_NUMPARTIDA].ToString();
                        //strDescPartida = fila[INDICE_COL_DESCPARTIDA].ToString();
                        //strMonto = fila[INDICE_COL_MONTO].ToString();
                        //strIdPartida = fila[INDICE_COL_IDPARTIDA].ToString();
                        //strEjercicio = fila[INDICE_COL_EJERCICIO].ToString();

                        //Validar que haya información en los campos requeridos
                        for (int columna = 0; columna < NUMERO_COLUMNAS; columna++)
                        {
                            if (String.IsNullOrEmpty(fila[columna].ToString()))
                            {
                                return Json(new { Exitoso = false, Excepcion = "Las celdas del archivo deben contener información." });
                            }
                        }

                        //Validar llaves de catálogos
                        idProyecto = fila[INDICE_COL_PROYECTO].ToString();
                        idPrioridad = fila[INDICE_COL_PRIORIDAD].ToString();
                        idProcedimiento = fila[INDICE_COL_PROCEDIMIENTO].ToString(); ;
                        idEstatusContrato = fila[INDICE_COL_ESTATUSCONTRATO].ToString(); ;
                        idTipoContrato = fila[INDICE_COL_TIPOCONTRATO].ToString(); ;
                        //idArea = fila[INDICE_COL_AREA].ToString(); ;

                        //Validar longitud de campos string
                        if (strNumero.Length > LONGITUD_NUMERO)
                        {
                            return Json(new { Exitoso = false, Excepcion = String.Format(strValidacionLongitud, "ID PRODUCTO", LONGITUD_NUMERO) });
                        }

                        if (strObjeto.Length > LONGITUD_OBJETO)
                        {
                            return Json(new { Exitoso = false, Excepcion = String.Format(strValidacionLongitud, "CLAVE DE PRODUCTO", LONGITUD_OBJETO) });
                        }

                        if (strNombre.Length > LONGITUD_NOMBRE)
                        {
                            return Json(new { Exitoso = false, Excepcion = String.Format(strValidacionLongitud, "NOMBRE", LONGITUD_NOMBRE) });
                        }

                        //if (strNumPartida.Length > LONGITUD_NUMPARTIDA)
                        //{
                        //    return Json(new { Exitoso = false, Excepcion = String.Format(strValidacionLongitud, "ELEMENTO", LONGITUD_NUMPARTIDA) });
                        //}

                        //if (strDescPartida.Length > LONGITUD_DESCPARTIDA)
                        //{
                        //    return Json(new { Exitoso = false, Excepcion = String.Format(strValidacionLongitud, "DESCRIPCION DE ELEMENTO", LONGITUD_DESCPARTIDA) });
                        //}

                        //Validar campos numéricos de partidas
                        //esMontoDecimal = float.TryParse(strMonto, out float f);
                        //esEjercicioNumerico = int.TryParse(strEjercicio, out int n);
                        //esIdPartidaNumerico = int.TryParse(strIdPartida, out int p);

                        //if (!esMontoDecimal)
                        //{
                        //    return Json(new { Exitoso = false, Excepcion = "La información de la columna 'MONTO' debe ser un valor numérico decimal." });
                        //}
                        //if (!esEjercicioNumerico)
                        //{
                        //    return Json(new { Exitoso = false, Excepcion = "La información de la columna 'EJERCICIO' debe ser un valor numérico entero." });
                        //}
                        //if (!esIdPartidaNumerico)
                        //{
                        //    return Json(new { Exitoso = false, Excepcion = "La información de la columna 'ID DE PARTIDA' debe ser un valor numérico entero." });
                        //}

                        lstBinariosFecha = new List<bool>()
                        {
                            ValidarFecha(strFechaFirma),
                            ValidarFecha(strFechaInicio),
                            ValidarFecha(strFechaFin),
                            ValidarFecha(strFechaFormalizacion)
                        };

                        //Validar formato de fechas
                        if (lstBinariosFecha.Contains(false))
                        {
                            return Json(new { Exitoso = false, Excepcion = "La información de las celdas 'FECHA FIRMA', 'FECHA INICIO', 'FECHA FIN' y 'FECHA FORMALIZACION' es incorrecta." });
                        }

                        //Aplicar reglas de negocio a fechas
                        fechaFirma = DateTime.Parse(strFechaFirma);
                        fechaInicio = DateTime.Parse(strFechaInicio);
                        fechaFin = DateTime.Parse(strFechaFin);
                        fechaFormalizacion = DateTime.Parse(strFechaFormalizacion);

                        if (DateTime.Compare(fechaInicio, fechaFin) > 0)
                        {
                            return Json(new { Exitoso = false, Excepcion = "La información del campo 'FECHA INICIO' debe ser menor a la información del campo 'FECHA FIN'." });
                        }

                        if (DateTime.Compare(fechaActual, fechaFin) > 0)
                        {
                            return Json(new { Exitoso = false, Excepcion = "La información del campo 'FECHA FIN' debe ser mayor a la fecha actual." });
                        }

                        //Validar booleanos
                        esAmpliacion = ValidarBinarios(fila[INDICE_COL_ESAMPLIACION].ToString());
                        requiereRenovacion = ValidarBinarios(fila[INDICE_COL_REQUIERERENOVACION].ToString());
                        esSatisfactorio = ValidarBinarios(fila[INDICE_COL_ESSATISFACTORIO].ToString());
                        presentoGarantia = ValidarBinarios(fila[INDICE_COL_PRESENTOGARANTIA].ToString());
                        esAdministradora = ValidarBinarios(fila[INDICE_COL_ESADMINISTRADORA].ToString());

                        lstBinarios = new List<Nullable<sbyte>>()
                        {
                            esAmpliacion, requiereRenovacion, esSatisfactorio, presentoGarantia, esAdministradora
                        };

                        if (lstBinarios.Contains(null))
                        {
                            return Json(new { Exitoso = false, Excepcion = "La información de las celdas 'AMPLIACION', 'REQUIERE RENOVACION', 'SATISFACTORIO', 'PRESENTO GARANTIA', 'ES ADMINISTRADORA' es incorrecta." });
                        }

                        contrato.p_tbl_tipo_contrato_id = idTipoContrato;
                        contrato.p_tbl_prioridad_id = idPrioridad;
                        contrato.p_tbl_estatus_contrato_id = idEstatusContrato;
                        contrato.p_tbl_proyecto_id = idProyecto;
                        contrato.p_tbl_procedimiento_id = idProcedimiento;
                        contrato.p_numero = strNumero;
                        contrato.p_objeto = strObjeto;
                        contrato.p_fecha_firma = strFechaFirma;
                        contrato.p_fecha_Iinicio = strFechaInicio;
                        contrato.p_fecha_fin = strFechaFin;
                        contrato.p_fecha_formalizacion = strFechaFormalizacion;
                        contrato.p_ampliacion = esAmpliacion.GetValueOrDefault(0);
                        contrato.p_requiere_renovacion = requiereRenovacion.GetValueOrDefault(0);
                        contrato.p_satisfactorio = esSatisfactorio.GetValueOrDefault(0);
                        contrato.p_porc_max_penalizacion = strPorcMaxPenalizacion;
                        contrato.p_porc_max_deductivas = strPorcMaxDeductivas;
                        contrato.p_presento_garantia = presentoGarantia.GetValueOrDefault(0);
                        contrato.p_porc_garantia = strPorcGarantia;
                        contrato.p_monto_garantia = strMontoGarantia;
                        contrato.p_es_administradora = esAdministradora.GetValueOrDefault(0);
                        contrato.p_nombre = strNombre;
                        contrato.p_monto_max_sin_iva = strMontoMaxSinIVA;
                        contrato.p_monto_min_sin_iva = strMontoMinSinIVA;

                        lstContratos.Add(contrato);

                        //lstContratos.Add(new Contrato()
                        //{
                        //    //IdContratoTemp = idContrato,
                        //    Tbl_proyecto_id = idProyecto,
                        //    Tbl_prioridad_id = idPrioridad,
                        //    Tbl_procedimiento_id = idProcedimiento,
                        //    Tbl_estatus_contrato_id = idEstatusContrato,
                        //    Tbl_tipo_contrato_id = idTipoContrato,
                        //    Numero = strNumero,
                        //    Objeto = strObjeto,
                        //    Fecha_firma = strFechaFirma,
                        //    Fecha_inicio = strFechaInicio,
                        //    Fecha_fin = strFechaFin,
                        //    Fecha_formalizacion = strFechaFormalizacion,
                        //    Ampliacion = esAmpliacion.ToString(),
                        //    Requiere_renovacion = requiereRenovacion.ToString(),
                        //    Satisfactorio = esSatisfactorio.ToString(),
                        //    Presento_garantia = presentoGarantia.ToString(),
                        //    Es_administradora = esAdministradora.ToString(),
                        //    Porc_max_penalizacion = strPorcMaxPenalizacion,
                        //    Porc_max_deductivas = strPorcMaxDeductivas,
                        //    Porc_garantia = strPorcGarantia,
                        //    Monto_garantia = strMontoGarantia,
                        //    Activo = strActivo,
                        //    Token = strToken,
                        //    Nombre = strNombre,
                        //    Monto_max_sin_iva = strMontoMaxSinIVA,
                        //    Monto_min_sin_iva = strMontoMinSinIVA,
                        //    Fecha_registro = strFechaRegistro
                        //});

                        //Crear lista de partidas
                        //lstPartidas.Add(new Partida()
                        //{
                        //    NumContrato = strNumero,
                        //    NumPartida = strNumPartida,
                        //    DescPartida = strDescPartida,
                        //    Monto = strMonto,
                        //    IdArea = idArea,
                        //    IdPartida = strIdPartida,
                        //    Ejercicio = strEjercicio
                        //});
                    }
                }

                lstContratosUnicos = lstContratos.GroupBy(x => x.p_numero).Select(y => y.First()).ToList();

                //foreach (Contrato item in lstContratosUnicos)
                //{
                //    item.p_id = Guid.NewGuid().ToString();

                //    //idContrato = Guid.NewGuid().ToString();

                //    //Guardar partidas del contrato, ciclar lista de partidas y obtener mediante ID de contrato generado previamente
                //    //foreach (Partida partida in lstPartidas.Where(x => x.NumContrato == item.Numero).ToList())
                //    //{
                //    //    var partidaVM = new
                //    //    {
                //    //        numeros = partida.NumPartida,
                //    //        descripciones = partida.DescPartida,
                //    //        montos = partida.Monto,
                //    //        modelArea = new
                //    //        {
                //    //            IdArea = partida.IdArea,
                //    //            IdPartida = partida.IdPartida,
                //    //            IdContratoTemp = idContrato,
                //    //            IdEjercicio = partida.Ejercicio
                //    //        }
                //    //    };

                //    //    string jsonPartida = JsonConvert.SerializeObject(partidaVM);

                //    //    cliente = new HttpClient();

                //    //    contenido = new StringContent(jsonPartida, Encoding.UTF8, "application/json");

                //    //    var areaVM = new
                //    //    {
                //    //        contrato = new { IdArea = partida.IdArea },
                //    //        IdPartida = partida.IdPartida,
                //    //        IdContratoTemp = idContrato,
                //    //        idEjercicio = partida.Ejercicio
                //    //    };

                //    //    string jsonArea = JsonConvert.SerializeObject(areaVM);

                //    //    contenido = new StringContent(jsonArea, Encoding.UTF8, "application/json");
                //    //}

                //    //contrato = new Contrato()
                //    //{
                //    //    IdContratoTemp = idContrato.ToString(),
                //    //    IdProyecto = item.IdProyecto,
                //    //    IdPrioridad = item.IdPrioridad,
                //    //    IdProcedimiento = item.IdProcedimiento,
                //    //    IdEstatusContrato = item.IdEstatusContrato,
                //    //    Id_TipoContrato = item.Id_TipoContrato,
                //    //    Numero = item.Numero,
                //    //    Objeto = item.Objeto,
                //    //    FechaFirma = item.FechaFirma,
                //    //    FechaInicio = item.FechaInicio,
                //    //    FechaFin = item.FechaFin,
                //    //    FechaFormalizacion = item.FechaFormalizacion,
                //    //    Ampliacion = item.Ampliacion,
                //    //    RequiereRenovacion = item.RequiereRenovacion,
                //    //    Satisfactorio = item.Satisfactorio,
                //    //    PresentoGarantia = item.PresentoGarantia,
                //    //    EsAdministradora = item.EsAdministradora,
                //    //    IdAlertamiento = ID_ALERTAMIENTO.ToString()
                //    //};

                //    //contrato = new Contrato()
                //    //{
                //    //    Id = idContrato.ToString(),
                //    //    Tbl_proyecto_id = item.Tbl_proyecto_id,
                //    //    Tbl_prioridad_id = item.Tbl_prioridad_id,
                //    //    Tbl_procedimiento_id = item.Tbl_procedimiento_id,
                //    //    Tbl_estatus_contrato_id = item.Tbl_estatus_contrato_id,
                //    //    Tbl_tipo_contrato_id = item.Tbl_tipo_contrato_id,
                //    //    Numero = item.Numero,
                //    //    Objeto = item.Objeto,
                //    //    Fecha_firma = item.Fecha_firma,
                //    //    Fecha_inicio = item.Fecha_inicio,
                //    //    Fecha_fin = item.Fecha_fin,
                //    //    Fecha_formalizacion = item.Fecha_formalizacion,
                //    //    Ampliacion = item.Ampliacion,
                //    //    Requiere_renovacion = item.Requiere_renovacion,
                //    //    Satisfactorio = item.Satisfactorio,
                //    //    Presento_garantia = item.Presento_garantia,
                //    //    Es_administradora = item.Es_administradora,
                //    //    Porc_max_penalizacion = item.Porc_max_penalizacion,
                //    //    Porc_max_deductivas = item.Porc_max_deductivas,
                //    //    Porc_garantia = item.Porc_garantia,
                //    //    Monto_garantia = item.Monto_garantia,
                //    //    Activo = item.Activo,
                //    //    Token = item.Token,
                //    //    Nombre = item.Nombre,
                //    //    Monto_max_sin_iva = item.Monto_max_sin_iva,
                //    //    Monto_min_sin_iva = item.Monto_min_sin_iva,
                //    //    Fecha_registro = item.Fecha_registro
                //    //};

                //    ////string url = strURIContratos;
                //    //cliente = new HttpClient();

                //    //jsonContrato = JsonConvert.SerializeObject(contrato);

                //    //contenido = new StringContent(jsonContrato, Encoding.UTF8, "application/json");
                //    //resultado = cliente.PostAsync(strURIContratos, contenido).Result;
                //}

                cliente = new HttpClient();
                jsonContrato = JsonConvert.SerializeObject(lstContratosUnicos);
                contenido = new StringContent(jsonContrato, Encoding.UTF8, "application/json");

                resultado = cliente.PostAsync(strURIContratos, contenido).Result;

                return Json(new { Exitoso = true, Excepcion = string.Empty, Contenido = string.Empty });
                //return Json(resultado);
            }
        }

        private Nullable<sbyte> ValidarBinarios(string cadena)
        {
            string cadenaFormateada = FormatearCadena(cadena);

            if (cadenaFormateada == "si")
            {
                return 1;
                //return VERDADERO;
            }
            else if (cadenaFormateada == "no")
            {
                return 0;
                //return FALSO;
            }
            else
            {
                return null;
                //return INVALIDO;
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

        private string FormatearCadena(string cadena)
        {
            string cadenaFormateada = string.Empty;

            cadenaFormateada = cadena
                                    .ToLower()
                                    .Replace('á', 'a')
                                    .Replace('é', 'e')
                                    .Replace('í', 'i')
                                    .Replace('ó', 'o')
                                    .Replace('ú', 'u')
                                    .Replace(" ", "")
                                    .Trim();

            return cadenaFormateada;
        }
    }
}