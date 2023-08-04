using System;
using System.Collections.Generic;

namespace AccesoDatos_AdminContratos.ModelosCFDI
{
    public partial class TblCfdiContratos
    {
        public string Id { get; set; }
        public string TblContratoId { get; set; }
        public string Folio { get; set; }
        public string Serie { get; set; }
        public DateTime Fecha { get; set; }
        public string FormaDePago { get; set; }
        public decimal Total { get; set; }
        public decimal SubTotal { get; set; }
        public string Moneda { get; set; }
        public string CondicionesDePago { get; set; }
        public string MetodoDePago { get; set; }
        public string TipoDeComprobante { get; set; }
        public string LugarExpedicion { get; set; }
        public string RfcEmisor { get; set; }
        public string NombreEmisor { get; set; }
        public string CalleEmisor { get; set; }
        public string NoInteriorEmisor { get; set; }
        public string NoExteriorEmisor { get; set; }
        public string ColoniaEmisor { get; set; }
        public string MunicipioEmisor { get; set; }
        public string EstadoEmisor { get; set; }
        public string PaisEmisor { get; set; }
        public string CodigoPostalEmisor { get; set; }
        public string RegimenEmisor { get; set; }
        public string RfcReceptor { get; set; }
        public string NombreReceptor { get; set; }
        public string CalleReceptor { get; set; }
        public string NoInteriorReceptor { get; set; }
        public string NoExteriorReceptor { get; set; }
        public string ColoniaReceptor { get; set; }
        public string MunicipioReceptor { get; set; }
        public string EstadoReceptor { get; set; }
        public string PaisReceptor { get; set; }
        public string CodigoPostalReceptor { get; set; }
        public string Conceptos { get; set; }
        public string Traslados { get; set; }
        public string Retenciones { get; set; }
        public string CadenaXml { get; set; }
        public sbyte Activo { get; set; }
    }
}
