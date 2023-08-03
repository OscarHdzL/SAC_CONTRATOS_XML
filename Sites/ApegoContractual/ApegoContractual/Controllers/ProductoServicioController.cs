using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Data;
using Microsoft.AspNetCore.Http;
using ApegoContractual.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace ApegoContractual.Controllers
{
    public class ProductoServicioController : Controller
    {
        private int NUMERO_COLUMNAS = 8;
        private int LONGITUD_1000 = 1000;
        private int LONGITUD_500 = 500;
        private int LONGITUD_200 = 200;
        private int LONGITUD_30 = 30;

        private int INDICE_COL_NUMERO = 0;
        private int INDICE_COL_ID = 1;
        private int INDICE_COL_CLAVE = 2;
        private int INDICE_COL_ELEMENTO = 3;
        private int INDICE_COL_DESCRIPCION = 4;
        private int INDICE_COL_ID_UNIDAD_MEDIDA = 5;
        private int INDICE_COL_TIPO = 6;
        private int INDICE_COL_COMENTARIOS = 7;

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RealizarCargaMasiva(IFormFile archivo)
        {
            List<string[]> values = new List<string[]>();
            string strNumProducto, strIdProducto, strClave, strElemento, strDescripcion, strTipo, strIdUnidadMedida, strComentarios;
            string strValidacionLongitud = "La información de la celda '{0}' debe ser menor o igual a {1} caracteres.";
            string jsonProducto = string.Empty;
            string[] fila;
            string strURIProductos = "http://107.178.210.37:8081/EndPoint/ProdServ/Add";
            Guid guidOutput;
            HttpClient cliente;
            StringContent contenido;
            HttpResponseMessage resultado;

            ProductoServicio producto;
            List<ProductoServicio> lstProductoServicio = new List<ProductoServicio>();

            using (StreamReader reader = new StreamReader(archivo.OpenReadStream(), Encoding.GetEncoding("iso-8859-1"), true))
            {
                while (!reader.EndOfStream)
                {
                    fila = reader.ReadLine().Split(',');

                    if(fila.Length == NUMERO_COLUMNAS)
                    {
                        //Inicializar registro de producto
                        producto = new ProductoServicio()
                        {
                            p_opt = "0",
                            p_id = "null",
                            p_activo = "1",
                            p_tbl_dependencia_id = HttpContext.Session.GetString("IdDependencia")
                        };

                        //Columnas de productos
                        strNumProducto = fila[INDICE_COL_NUMERO].ToString();
                        strIdProducto = fila[INDICE_COL_ID].ToString();
                        strClave = fila[INDICE_COL_CLAVE].ToString();
                        strElemento = fila[INDICE_COL_ELEMENTO].ToString();
                        strDescripcion = fila[INDICE_COL_DESCRIPCION].ToString();
                        strIdUnidadMedida = fila[INDICE_COL_ID_UNIDAD_MEDIDA].ToString();
                        strTipo = fila[INDICE_COL_TIPO].ToString();
                        strComentarios = fila[INDICE_COL_COMENTARIOS].ToString();

                        //Validar que haya información en los campos requeridos
                        for (int columna = 0; columna < NUMERO_COLUMNAS; columna++)
                        {
                            if (String.IsNullOrEmpty(fila[columna].ToString()))
                            {
                                return Json( new { Exitoso = false, Excepcion = "Las celdas del archivo deben contener información."});
                            }
                        }

                        //Validar longitud de campos string
                        if (strIdProducto.Length > LONGITUD_1000)
                        {
                            return Json(new { Exitoso = false, Excepcion = String.Format(strValidacionLongitud, "ID PRODUCTO", LONGITUD_1000) });
                        }

                        if (strClave.Length > LONGITUD_30)
                        {
                            return Json(new { Exitoso = false, Excepcion = String.Format(strValidacionLongitud, "CLAVE DE PRODUCTO", LONGITUD_30) });
                        }

                        if (strElemento.Length > LONGITUD_200)
                        {
                            return Json(new { Exitoso = false, Excepcion = String.Format(strValidacionLongitud, "ELEMENTO", LONGITUD_200) });
                        }

                        if (strDescripcion.Length > LONGITUD_1000)
                        {
                            return Json(new { Exitoso = false, Excepcion = String.Format(strValidacionLongitud, "DESCRIPCION DE ELEMENTO", LONGITUD_1000) });
                        }

                        if (strComentarios.Length > LONGITUD_500)
                        {
                            return Json(new { Exitoso = false, Excepcion = String.Format(strValidacionLongitud, "COMENTARIOS", LONGITUD_500) });
                        }

                        //Validar llaves de catálogos
                        if (!Guid.TryParse(strTipo, out guidOutput) || !Guid.TryParse(strIdUnidadMedida, out guidOutput))
                        {
                            return Json(new { Exitoso = false, Excepcion = "La información de las celdas 'TIPO' y 'UNIDAD DE MEDIDA' no corresponden a identificadores únicos." });
                        }

                        producto.p_producto_servicio = strIdProducto;
                        producto.p_clave_producto = strClave;
                        producto.p_elemento = strElemento;
                        producto.p_elemento_desc = strDescripcion;
                        producto.p_tbl_tipo_id = strTipo;
                        producto.p_tbl_unidad_medida_id = strIdUnidadMedida;
                        producto.p_comentario = strComentarios;

                        //lstProductoServicio.Add(producto);

                        cliente = new HttpClient();
                        jsonProducto = JsonConvert.SerializeObject(producto);
                        contenido = new StringContent(jsonProducto, Encoding.UTF8, "application/json");

                        resultado = cliente.PostAsync(strURIProductos, contenido).Result;
                        var respuesta = resultado.Content.ReadAsStringAsync();
                    }
                }

                return Json(new { Exitoso = true, Excepcion = string.Empty, Contenido = string.Empty });
            }
        }
    }
}