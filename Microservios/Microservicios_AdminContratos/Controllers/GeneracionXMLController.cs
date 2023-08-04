using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens.Saml;
using Modelos.Interfaz;
using Modelos.Modelos;
using Modelos.Modelos.Area;
using Modelos.Modelos.VerificacionContrato;
using Negocio_AdminContratos;
using Negocio_AdminContratos.XMLCFDI;
using Newtonsoft.Json;
using Utilidades.Log4Net;

namespace Servicios_AdminitracionContratos.Controllers
{
        [Produces("application/json")]
    [Route("GeneracionXMLController")]
    [EnableCors("CorsPolicy")]
    public class GeneracionXMLController : ControllerBase
    {
        #region Instancias

        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        #endregion
        public GeneracionXMLController(IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = new LoggerManager();
        }
        private GenerarXML_CFDI_Negocio Negocio = new GenerarXML_CFDI_Negocio();


        [HttpGet("Listar")]
        public IActionResult Listar()
        {
            return Ok(Negocio.ListarCFDI());
        }


        [HttpGet("ListarByContrato/{idContrato}")]
        public IActionResult ListarByContrato(string idContrato)
        {
            return Ok(Negocio.ListarCFDIByContrato(idContrato));
        }

        [HttpGet("ListarById/{id}")]
        public IActionResult ListarById(string id)
        {
            return Ok(Negocio.ListarCFDIById(id));
        }   



        [HttpPost("Generar")]
        public IActionResult GenerarXML( [FromBody] DatosXmlRequest datos_facturacion_general)
        {

                try
                {

                Comprobante objCompXSD = new Comprobante();
                // Datos del CFDI Global
                //objCompXSD.version = "3.2";
                objCompXSD.folio = datos_facturacion_general.folio;
                objCompXSD.serie = datos_facturacion_general.serie;
                objCompXSD.fecha = datos_facturacion_general.fecha;
                objCompXSD.formaDePago = datos_facturacion_general.formaDePago;
                objCompXSD.total = datos_facturacion_general.total;
                objCompXSD.condicionesDePago = datos_facturacion_general.condicionesDePago;
                objCompXSD.subTotal = datos_facturacion_general.subTotal;
                objCompXSD.Moneda = datos_facturacion_general.moneda;
                objCompXSD.metodoDePago = datos_facturacion_general.metodoDePago;
                //objCompXSD.tipoDeComprobante = new ComprobanteTipoDeComprobante();
                objCompXSD.tipoDeComprobante = ComprobanteTipoDeComprobante.ingreso;
                objCompXSD.LugarExpedicion = datos_facturacion_general.LugarExpedicion;

                // Datos del Emisor
                objCompXSD.Emisor = new ComprobanteEmisor();
                objCompXSD.Emisor.rfc = datos_facturacion_general.rfcEmisor;
                objCompXSD.Emisor.nombre = datos_facturacion_general.nombreEmisor;
                // Datos del Domicilio del Emisor
                objCompXSD.Emisor.DomicilioFiscal = new t_UbicacionFiscal();
                objCompXSD.Emisor.DomicilioFiscal.calle = datos_facturacion_general.calleEmisor;
                objCompXSD.Emisor.DomicilioFiscal.noInterior = datos_facturacion_general.calleEmisor;
                objCompXSD.Emisor.DomicilioFiscal.noExterior = datos_facturacion_general.noExteriorEmisor;
                objCompXSD.Emisor.DomicilioFiscal.colonia = datos_facturacion_general.coloniaEmisor;
                objCompXSD.Emisor.DomicilioFiscal.municipio = datos_facturacion_general.municipioEmisor;
                objCompXSD.Emisor.DomicilioFiscal.estado = datos_facturacion_general.estadoEmisor;
                objCompXSD.Emisor.DomicilioFiscal.pais = datos_facturacion_general.paisEmisor;
                objCompXSD.Emisor.DomicilioFiscal.codigoPostal = datos_facturacion_general.codigoPostalEmisor;
                // Regimen del Emisor
                objCompXSD.Emisor.RegimenFiscal = new ComprobanteEmisorRegimenFiscal[1];
                objCompXSD.Emisor.RegimenFiscal[0] = new ComprobanteEmisorRegimenFiscal();


                objCompXSD.Emisor.RegimenFiscal[0].Regimen = datos_facturacion_general.RegimenEmisor;

                // Datos del Receptor
                objCompXSD.Receptor = new ComprobanteReceptor();
                objCompXSD.Receptor.rfc = datos_facturacion_general.rfcReceptor;
                objCompXSD.Receptor.nombre = datos_facturacion_general.nombreReceptor;

                //// Datos del Domicilio del Receptor
                objCompXSD.Receptor.Domicilio = new t_Ubicacion();
                objCompXSD.Receptor.Domicilio.calle = datos_facturacion_general.calleReceptor;
                objCompXSD.Receptor.Domicilio.noExterior = datos_facturacion_general.noExteriorReceptor;
                objCompXSD.Receptor.Domicilio.noInterior = datos_facturacion_general.noInteriorReceptor;
                objCompXSD.Receptor.Domicilio.colonia = datos_facturacion_general.coloniaReceptor;
                objCompXSD.Receptor.Domicilio.municipio = datos_facturacion_general.municipioReceptor;
                objCompXSD.Receptor.Domicilio.estado = datos_facturacion_general.estadoReceptor;
                objCompXSD.Receptor.Domicilio.pais = datos_facturacion_general.paisReceptor;
                objCompXSD.Receptor.Domicilio.codigoPostal = datos_facturacion_general.codigoPostalReceptor;
                // Conceptops



                objCompXSD.Conceptos = new ComprobanteConcepto[datos_facturacion_general.Conceptos.Count()]; // Numero de Filas

                decimal totalImpuestosTrasladados = 0;
                decimal totalImporteConceptos = 0;
                for (int i = 0; i < datos_facturacion_general.Conceptos.Count(); i++)
                {
                    objCompXSD.Conceptos[i] = new ComprobanteConcepto(); // Instancia de la Fila
                    objCompXSD.Conceptos[i].importe = datos_facturacion_general.Conceptos[i].importe;
                    objCompXSD.Conceptos[i].valorUnitario = datos_facturacion_general.Conceptos[i].valorUnitario;
                    objCompXSD.Conceptos[i].descripcion = datos_facturacion_general.Conceptos[i].descripcion;
                    objCompXSD.Conceptos[i].noIdentificacion = datos_facturacion_general.Conceptos[i].noIdentificacion;
                    objCompXSD.Conceptos[i].unidad = datos_facturacion_general.Conceptos[i].unidad;
                    objCompXSD.Conceptos[i].cantidad = datos_facturacion_general.Conceptos[i].cantidad;
                    totalImporteConceptos = totalImporteConceptos + (datos_facturacion_general.Conceptos[i].valorUnitario * datos_facturacion_general.Conceptos[i].cantidad);
                    //objCompXSD.Conceptos[i].importe = (datos_facturacion_general.Conceptos[i].valorUnitario * datos_facturacion_general.Conceptos[i].cantidad);

                }


                // [0] Debe aumentar para el siguiente Concepto
                // Impuestos
                objCompXSD.Impuestos = new ComprobanteImpuestos();
                objCompXSD.Impuestos.totalImpuestosTrasladadosSpecified = true; // Estatus si aparece
                objCompXSD.Impuestos.totalImpuestosTrasladados = (totalImporteConceptos * Convert.ToDecimal("0.16"));



                // Impuestos Traslados
                objCompXSD.Impuestos.Traslados = new ComprobanteImpuestosTraslado[1];
                objCompXSD.Impuestos.Traslados[0] = new ComprobanteImpuestosTraslado();
                objCompXSD.Impuestos.Traslados[0].importe = objCompXSD.Impuestos.totalImpuestosTrasladados;
                objCompXSD.Impuestos.Traslados[0].tasa = 16;
                objCompXSD.Impuestos.Traslados[0].impuesto = ComprobanteImpuestosTrasladoImpuesto.IVA;


                //objCompXSD.Impuestos.Retenciones = new ComprobanteImpuestosRetencion[1];
                //objCompXSD.Impuestos.Retenciones[0] = new ComprobanteImpuestosRetencion();
                //objCompXSD.Impuestos.Retenciones[0].impuesto = ComprobanteImpuestosRetencionImpuesto.IVA;
                ////objCompXSD.Impuestos.Retenciones[0].impuesto = ComprobanteImpuestosRetencionImpuesto.ISR;
                //objCompXSD.Impuestos.Retenciones[0].importe = 1000;


                // Complemento
                // objCompXSD.Complemento = new ComprobanteComplemento();
                // Creas los namespaces requeridos
                XmlSerializerNamespaces xmlNameSpace = new XmlSerializerNamespaces();

                xmlNameSpace.Add("cfdi", "http://www.sat.gob.mx/cfd/3");
                xmlNameSpace.Add("tfd", "http://www.sat.gob.mx/TimbreFiscalDigital");


                xmlNameSpace.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
                //xmlNameSpace.Add("schemaLocation", "http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv32.xsd");
                // Creas una instancia de XMLSerializer con el tipo de dato Comprobante
                XmlSerializer xmlSerialize = new XmlSerializer(typeof(Comprobante));


                string xmlCadena = String.Empty;

                using (StringWriter stringWriter = new System.IO.StringWriter())
                {
                    xmlSerialize.Serialize(stringWriter, objCompXSD, xmlNameSpace);
                    xmlCadena = stringWriter.ToString();
                }

                datos_facturacion_general.xml_cadena = xmlCadena;
                datos_facturacion_general.conceptos_cadena = JsonConvert.SerializeObject(datos_facturacion_general.Conceptos);

                return Ok(Negocio.guardarCFDI(datos_facturacion_general));




                //var stream = new MemoryStream();
                //xmlSerialize.Serialize(stream, objCompXSD, xmlNameSpace);
                //stream.Position = 0;
                //return File(stream, "application/xml", "MyXml.xml");


            }



            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
           
        }


        [HttpPut("Actualizar")]
        public IActionResult ActualizarXML([FromBody] DatosXmlRequest datos_facturacion_general)
        {

            try
            {

                Comprobante objCompXSD = new Comprobante();
                // Datos del CFDI Global
                //objCompXSD.version = "3.2";
                objCompXSD.folio = datos_facturacion_general.folio;
                objCompXSD.serie = datos_facturacion_general.serie;
                objCompXSD.fecha = datos_facturacion_general.fecha;
                objCompXSD.formaDePago = datos_facturacion_general.formaDePago;
                objCompXSD.total = datos_facturacion_general.total;
                objCompXSD.condicionesDePago = datos_facturacion_general.condicionesDePago;
                objCompXSD.subTotal = datos_facturacion_general.subTotal;
                objCompXSD.Moneda = datos_facturacion_general.moneda;
                objCompXSD.metodoDePago = datos_facturacion_general.metodoDePago;
                //objCompXSD.tipoDeComprobante = new ComprobanteTipoDeComprobante();
                objCompXSD.tipoDeComprobante = ComprobanteTipoDeComprobante.ingreso;
                objCompXSD.LugarExpedicion = datos_facturacion_general.LugarExpedicion;

                // Datos del Emisor
                objCompXSD.Emisor = new ComprobanteEmisor();
                objCompXSD.Emisor.rfc = datos_facturacion_general.rfcEmisor;
                objCompXSD.Emisor.nombre = datos_facturacion_general.nombreEmisor;
                // Datos del Domicilio del Emisor
                objCompXSD.Emisor.DomicilioFiscal = new t_UbicacionFiscal();
                objCompXSD.Emisor.DomicilioFiscal.calle = datos_facturacion_general.calleEmisor;
                objCompXSD.Emisor.DomicilioFiscal.noInterior = datos_facturacion_general.calleEmisor;
                objCompXSD.Emisor.DomicilioFiscal.noExterior = datos_facturacion_general.noExteriorEmisor;
                objCompXSD.Emisor.DomicilioFiscal.colonia = datos_facturacion_general.coloniaEmisor;
                objCompXSD.Emisor.DomicilioFiscal.municipio = datos_facturacion_general.municipioEmisor;
                objCompXSD.Emisor.DomicilioFiscal.estado = datos_facturacion_general.estadoEmisor;
                objCompXSD.Emisor.DomicilioFiscal.pais = datos_facturacion_general.paisEmisor;
                objCompXSD.Emisor.DomicilioFiscal.codigoPostal = datos_facturacion_general.codigoPostalEmisor;
                // Regimen del Emisor
                objCompXSD.Emisor.RegimenFiscal = new ComprobanteEmisorRegimenFiscal[1];
                objCompXSD.Emisor.RegimenFiscal[0] = new ComprobanteEmisorRegimenFiscal();


                objCompXSD.Emisor.RegimenFiscal[0].Regimen = datos_facturacion_general.RegimenEmisor;

                // Datos del Receptor
                objCompXSD.Receptor = new ComprobanteReceptor();
                objCompXSD.Receptor.rfc = datos_facturacion_general.rfcReceptor;
                objCompXSD.Receptor.nombre = datos_facturacion_general.nombreReceptor;

                //// Datos del Domicilio del Receptor
                objCompXSD.Receptor.Domicilio = new t_Ubicacion();
                objCompXSD.Receptor.Domicilio.calle = datos_facturacion_general.calleReceptor;
                objCompXSD.Receptor.Domicilio.noExterior = datos_facturacion_general.noExteriorReceptor;
                objCompXSD.Receptor.Domicilio.noInterior = datos_facturacion_general.noInteriorReceptor;
                objCompXSD.Receptor.Domicilio.colonia = datos_facturacion_general.coloniaReceptor;
                objCompXSD.Receptor.Domicilio.municipio = datos_facturacion_general.municipioReceptor;
                objCompXSD.Receptor.Domicilio.estado = datos_facturacion_general.estadoReceptor;
                objCompXSD.Receptor.Domicilio.pais = datos_facturacion_general.paisReceptor;
                objCompXSD.Receptor.Domicilio.codigoPostal = datos_facturacion_general.codigoPostalReceptor;
                // Conceptops



                objCompXSD.Conceptos = new ComprobanteConcepto[datos_facturacion_general.Conceptos.Count()]; // Numero de Filas

                decimal totalImpuestosTrasladados = 0;
                decimal totalImporteConceptos = 0;
                for (int i = 0; i < datos_facturacion_general.Conceptos.Count(); i++)
                {
                    objCompXSD.Conceptos[i] = new ComprobanteConcepto(); // Instancia de la Fila
                    objCompXSD.Conceptos[i].importe = datos_facturacion_general.Conceptos[i].importe;
                    objCompXSD.Conceptos[i].valorUnitario = datos_facturacion_general.Conceptos[i].valorUnitario;
                    objCompXSD.Conceptos[i].descripcion = datos_facturacion_general.Conceptos[i].descripcion;
                    objCompXSD.Conceptos[i].noIdentificacion = datos_facturacion_general.Conceptos[i].noIdentificacion;
                    objCompXSD.Conceptos[i].unidad = datos_facturacion_general.Conceptos[i].unidad;
                    objCompXSD.Conceptos[i].cantidad = datos_facturacion_general.Conceptos[i].cantidad;
                    totalImporteConceptos = totalImporteConceptos + (datos_facturacion_general.Conceptos[i].valorUnitario * datos_facturacion_general.Conceptos[i].cantidad);
                    //objCompXSD.Conceptos[i].importe = (datos_facturacion_general.Conceptos[i].valorUnitario * datos_facturacion_general.Conceptos[i].cantidad);

                }


                // [0] Debe aumentar para el siguiente Concepto
                // Impuestos
                objCompXSD.Impuestos = new ComprobanteImpuestos();
                objCompXSD.Impuestos.totalImpuestosTrasladadosSpecified = true; // Estatus si aparece
                objCompXSD.Impuestos.totalImpuestosTrasladados = (totalImporteConceptos * Convert.ToDecimal("0.16"));



                // Impuestos Traslados
                objCompXSD.Impuestos.Traslados = new ComprobanteImpuestosTraslado[1];
                objCompXSD.Impuestos.Traslados[0] = new ComprobanteImpuestosTraslado();
                objCompXSD.Impuestos.Traslados[0].importe = objCompXSD.Impuestos.totalImpuestosTrasladados;
                objCompXSD.Impuestos.Traslados[0].tasa = 16;
                objCompXSD.Impuestos.Traslados[0].impuesto = ComprobanteImpuestosTrasladoImpuesto.IVA;


                //objCompXSD.Impuestos.Retenciones = new ComprobanteImpuestosRetencion[1];
                //objCompXSD.Impuestos.Retenciones[0] = new ComprobanteImpuestosRetencion();
                //objCompXSD.Impuestos.Retenciones[0].impuesto = ComprobanteImpuestosRetencionImpuesto.IVA;
                ////objCompXSD.Impuestos.Retenciones[0].impuesto = ComprobanteImpuestosRetencionImpuesto.ISR;
                //objCompXSD.Impuestos.Retenciones[0].importe = 1000;


                // Complemento
                // objCompXSD.Complemento = new ComprobanteComplemento();
                // Creas los namespaces requeridos
                XmlSerializerNamespaces xmlNameSpace = new XmlSerializerNamespaces();

                xmlNameSpace.Add("cfdi", "http://www.sat.gob.mx/cfd/3");
                xmlNameSpace.Add("tfd", "http://www.sat.gob.mx/TimbreFiscalDigital");


                xmlNameSpace.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
                //xmlNameSpace.Add("schemaLocation", "http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv32.xsd");
                // Creas una instancia de XMLSerializer con el tipo de dato Comprobante
                XmlSerializer xmlSerialize = new XmlSerializer(typeof(Comprobante));


                string xmlCadena = String.Empty;

                using (StringWriter stringWriter = new System.IO.StringWriter())
                {
                    xmlSerialize.Serialize(stringWriter, objCompXSD, xmlNameSpace);
                    xmlCadena = stringWriter.ToString();
                }

                datos_facturacion_general.xml_cadena = xmlCadena;
                datos_facturacion_general.conceptos_cadena = JsonConvert.SerializeObject(datos_facturacion_general.Conceptos);

                return Ok(Negocio.actualizarCFDI(datos_facturacion_general));




                //var stream = new MemoryStream();
                //xmlSerialize.Serialize(stream, objCompXSD, xmlNameSpace);
                //stream.Position = 0;
                //return File(stream, "application/xml", "MyXml.xml");


            }



            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }


        [HttpPost("Previsualizar")]
        public IActionResult PrevisualizarXML([FromBody] DatosXmlRequest datos_facturacion_general)
        {

            try
            {

                Comprobante objCompXSD = new Comprobante();

                // Datos del CFDI Global
                //objCompXSD.version = "3.2";
                objCompXSD.folio = datos_facturacion_general.folio;
                objCompXSD.serie = datos_facturacion_general.serie;
                objCompXSD.fecha = datos_facturacion_general.fecha;
                objCompXSD.formaDePago = datos_facturacion_general.formaDePago;
                objCompXSD.total = datos_facturacion_general.total;
                objCompXSD.condicionesDePago = datos_facturacion_general.condicionesDePago;
                objCompXSD.subTotal = datos_facturacion_general.subTotal;
                objCompXSD.Moneda = datos_facturacion_general.moneda;
                objCompXSD.metodoDePago = datos_facturacion_general.metodoDePago;
                //objCompXSD.tipoDeComprobante = new ComprobanteTipoDeComprobante();
                objCompXSD.tipoDeComprobante = ComprobanteTipoDeComprobante.ingreso;
                objCompXSD.LugarExpedicion = datos_facturacion_general.LugarExpedicion;

                // Datos del Emisor
                objCompXSD.Emisor = new ComprobanteEmisor();
                objCompXSD.Emisor.rfc = datos_facturacion_general.rfcEmisor;
                objCompXSD.Emisor.nombre = datos_facturacion_general.nombreEmisor;
                // Datos del Domicilio del Emisor
                objCompXSD.Emisor.DomicilioFiscal = new t_UbicacionFiscal();
                objCompXSD.Emisor.DomicilioFiscal.calle = datos_facturacion_general.calleEmisor;
                objCompXSD.Emisor.DomicilioFiscal.noInterior = datos_facturacion_general.calleEmisor;
                objCompXSD.Emisor.DomicilioFiscal.noExterior = datos_facturacion_general.noExteriorEmisor;
                objCompXSD.Emisor.DomicilioFiscal.colonia = datos_facturacion_general.coloniaEmisor;
                objCompXSD.Emisor.DomicilioFiscal.municipio = datos_facturacion_general.municipioEmisor;
                objCompXSD.Emisor.DomicilioFiscal.estado = datos_facturacion_general.estadoEmisor;
                objCompXSD.Emisor.DomicilioFiscal.pais = datos_facturacion_general.paisEmisor;
                objCompXSD.Emisor.DomicilioFiscal.codigoPostal = datos_facturacion_general.codigoPostalEmisor;
                // Regimen del Emisor
                objCompXSD.Emisor.RegimenFiscal = new ComprobanteEmisorRegimenFiscal[1];
                objCompXSD.Emisor.RegimenFiscal[0] = new ComprobanteEmisorRegimenFiscal();


                objCompXSD.Emisor.RegimenFiscal[0].Regimen = datos_facturacion_general.RegimenEmisor;

                // Datos del Receptor
                objCompXSD.Receptor = new ComprobanteReceptor();
                objCompXSD.Receptor.rfc = datos_facturacion_general.rfcReceptor;
                objCompXSD.Receptor.nombre = datos_facturacion_general.nombreReceptor;

                //// Datos del Domicilio del Receptor
                objCompXSD.Receptor.Domicilio = new t_Ubicacion();
                objCompXSD.Receptor.Domicilio.calle = datos_facturacion_general.calleReceptor;
                objCompXSD.Receptor.Domicilio.noExterior = datos_facturacion_general.noExteriorReceptor;
                objCompXSD.Receptor.Domicilio.noInterior = datos_facturacion_general.noInteriorReceptor;
                objCompXSD.Receptor.Domicilio.colonia = datos_facturacion_general.coloniaReceptor;
                objCompXSD.Receptor.Domicilio.municipio = datos_facturacion_general.municipioReceptor;
                objCompXSD.Receptor.Domicilio.estado = datos_facturacion_general.estadoReceptor;
                objCompXSD.Receptor.Domicilio.pais = datos_facturacion_general.paisReceptor;
                objCompXSD.Receptor.Domicilio.codigoPostal = datos_facturacion_general.codigoPostalReceptor;
                // Conceptops



                objCompXSD.Conceptos = new ComprobanteConcepto[datos_facturacion_general.Conceptos.Count()]; // Numero de Filas

                decimal totalImpuestosTrasladados = 0;
                decimal totalImporteConceptos = 0;
                for (int i = 0; i < datos_facturacion_general.Conceptos.Count(); i++)
                {
                    objCompXSD.Conceptos[i] = new ComprobanteConcepto(); // Instancia de la Fila
                    objCompXSD.Conceptos[i].importe = datos_facturacion_general.Conceptos[i].importe;
                    objCompXSD.Conceptos[i].valorUnitario = datos_facturacion_general.Conceptos[i].valorUnitario;
                    objCompXSD.Conceptos[i].descripcion = datos_facturacion_general.Conceptos[i].descripcion;
                    objCompXSD.Conceptos[i].noIdentificacion = datos_facturacion_general.Conceptos[i].noIdentificacion;
                    objCompXSD.Conceptos[i].unidad = datos_facturacion_general.Conceptos[i].unidad;
                    objCompXSD.Conceptos[i].cantidad = datos_facturacion_general.Conceptos[i].cantidad;
                    totalImporteConceptos = totalImporteConceptos + (datos_facturacion_general.Conceptos[i].valorUnitario * datos_facturacion_general.Conceptos[i].cantidad);
                    //objCompXSD.Conceptos[i].importe = (datos_facturacion_general.Conceptos[i].valorUnitario * datos_facturacion_general.Conceptos[i].cantidad);

                }
                 

                // [0] Debe aumentar para el siguiente Concepto
                // Impuestos
                objCompXSD.Impuestos = new ComprobanteImpuestos();
                objCompXSD.Impuestos.totalImpuestosTrasladadosSpecified = true; // Estatus si aparece
                objCompXSD.Impuestos.totalImpuestosTrasladados = (totalImporteConceptos * Convert.ToDecimal("0.16"));



                // Impuestos Traslados
                objCompXSD.Impuestos.Traslados = new ComprobanteImpuestosTraslado[1];
                objCompXSD.Impuestos.Traslados[0] = new ComprobanteImpuestosTraslado();
                objCompXSD.Impuestos.Traslados[0].importe = objCompXSD.Impuestos.totalImpuestosTrasladados;
                objCompXSD.Impuestos.Traslados[0].tasa = 16;
                objCompXSD.Impuestos.Traslados[0].impuesto = ComprobanteImpuestosTrasladoImpuesto.IVA;


                //objCompXSD.Impuestos.Retenciones = new ComprobanteImpuestosRetencion[1];
                //objCompXSD.Impuestos.Retenciones[0] = new ComprobanteImpuestosRetencion();
                //objCompXSD.Impuestos.Retenciones[0].impuesto = ComprobanteImpuestosRetencionImpuesto.IVA;
                ////objCompXSD.Impuestos.Retenciones[0].impuesto = ComprobanteImpuestosRetencionImpuesto.ISR;
                //objCompXSD.Impuestos.Retenciones[0].importe = 1000;


                // Complemento
                // objCompXSD.Complemento = new ComprobanteComplemento();
                // Creas los namespaces requeridos
                XmlSerializerNamespaces xmlNameSpace = new XmlSerializerNamespaces();

                xmlNameSpace.Add("cfdi", "http://www.sat.gob.mx/cfd/3");
                xmlNameSpace.Add("tfd", "http://www.sat.gob.mx/TimbreFiscalDigital");


                xmlNameSpace.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
                //xmlNameSpace.Add("schemaLocation", "http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv32.xsd");
                // Creas una instancia de XMLSerializer con el tipo de dato Comprobante
                XmlSerializer xmlSerialize = new XmlSerializer(typeof(Comprobante));


                string xmlCadena = String.Empty;

                using (StringWriter stringWriter = new System.IO.StringWriter())
                {
                    xmlSerialize.Serialize(stringWriter, objCompXSD, xmlNameSpace);
                    xmlCadena = stringWriter.ToString();
                }

                return Ok(xmlCadena);

            }



            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


    }
}