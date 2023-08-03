using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio_AdminContratos.XMLCFDI
{
    public class DatosXml
    {
        // Datos del CFDI Global
        public string folio { get; set; }
        public string serie { get; set; }
        public DateTime fecha { get; set; }
        public string formaDePago { get; set; }
        public string condicionesDePago { get; set; }
        public decimal total { get; set; }
        public string moneda { get; set; }
        public decimal subTotal { get; set; }
        public string metodoDePago { get; set; }
        public string tipoDeComprobante { get; set; }
        public string LugarExpedicion { get; set; }

        // Datos del Emisor
        public string rfcEmisor { get; set; }
        public string nombreEmisor { get; set; }
        public string calleEmisor { get; set; }
        public string noInteriorEmisor { get; set; }
        public string noExteriorEmisor { get; set; }
        public string coloniaEmisor { get; set; }
        public string municipioEmisor { get; set; }
        public string estadoEmisor { get; set; }
        public string paisEmisor { get; set; }
        public string codigoPostalEmisor { get; set; }
        public string RegimenEmisor { get; set; }

        // Datos del Receptor
        public string rfcReceptor { get; set; }
        public string nombreReceptor { get; set; }
        public string calleReceptor { get; set; }
        public string noInteriorReceptor { get; set; }
        public string noExteriorReceptor { get; set; }
        public string coloniaReceptor { get; set; }
        public string municipioReceptor { get; set; }
        public string estadoReceptor { get; set; }
        public string paisReceptor { get; set; }
        public string codigoPostalReceptor { get; set; }

        //Conceptos

        public List<Concepto> Conceptos { get; set; } = new List<Concepto>();
        
    }

    public class Concepto {
        public decimal importe { get; set; }
        public decimal valorUnitario { get; set; }
        public string descripcion { get; set; }
        public string noIdentificacion { get; set; }
        public string unidad { get; set; }
        public decimal cantidad { get; set; }

    }
}
