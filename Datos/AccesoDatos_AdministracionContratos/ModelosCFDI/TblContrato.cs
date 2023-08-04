using System;
using System.Collections.Generic;

namespace AccesoDatos_AdminContratos.ModelosCFDI
{
    public partial class TblContrato
    {
        public string Id { get; set; }
        public string TblTipoContratoId { get; set; }
        public string TblPrioridadId { get; set; }
        public string TblEstatusContratoId { get; set; }
        public string TblProyectoId { get; set; }
        public string TblProcedimientoId { get; set; }
        public string Numero { get; set; }
        public string Objeto { get; set; }
        public DateTime FechaFirma { get; set; }
        public DateTime FechaIinicio { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime FechaFormalizacion { get; set; }
        public sbyte Ampliacion { get; set; }
        public sbyte RequiereRenovacion { get; set; }
        public sbyte Satisfactorio { get; set; }
        public double? PorcMaxPenalizacion { get; set; }
        public double? PorcMaxDeductivas { get; set; }
        public sbyte PresentoGarantia { get; set; }
        public double? PorcGarantia { get; set; }
        public double? MontoGarantia { get; set; }
        public sbyte EsAdministradora { get; set; }
        public sbyte? Activo { get; set; }
        public string Token { get; set; }
        public string Nombre { get; set; }
        public double? MontoMaxSinIva { get; set; }
        public double? MontoMinSinIva { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public string Necesidad { get; set; }
        public string TblDependenciaId { get; set; }
        public string EstructuraAsignadaId { get; set; }
        public int? TipoEstructura { get; set; }
    }
}
