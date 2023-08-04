using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AccesoDatos_AdminContratos.ModelosCFDI
{
    public partial class saciiContext : DbContext
    {
        public saciiContext()
        {
        }

        public saciiContext(DbContextOptions<saciiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblCfdiContratos> TblCfdiContratos { get; set; }
        public virtual DbSet<TblContrato> TblContrato { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=10.4.15.160;user id=root;password=D3vPMS.2019;database=sac-ii;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblCfdiContratos>(entity =>
            {
                entity.ToTable("tbl_cfdi_contratos");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.Activo)
                    .HasColumnName("activo")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.CadenaXml)
                    .IsRequired()
                    .HasColumnName("cadenaXML")
                    .HasColumnType("text");

                entity.Property(e => e.CalleEmisor)
                    .IsRequired()
                    .HasColumnName("calleEmisor")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.CalleReceptor)
                    .IsRequired()
                    .HasColumnName("calleReceptor")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.CodigoPostalEmisor)
                    .IsRequired()
                    .HasColumnName("codigoPostalEmisor")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.CodigoPostalReceptor)
                    .IsRequired()
                    .HasColumnName("codigoPostalReceptor")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.ColoniaEmisor)
                    .IsRequired()
                    .HasColumnName("coloniaEmisor")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.ColoniaReceptor)
                    .IsRequired()
                    .HasColumnName("coloniaReceptor")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Conceptos)
                    .IsRequired()
                    .HasColumnName("conceptos")
                    .HasColumnType("text");

                entity.Property(e => e.CondicionesDePago)
                    .IsRequired()
                    .HasColumnName("condicionesDePago")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.EstadoEmisor)
                    .IsRequired()
                    .HasColumnName("estadoEmisor")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.EstadoReceptor)
                    .IsRequired()
                    .HasColumnName("estadoReceptor")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("datetime");

                entity.Property(e => e.Folio)
                    .IsRequired()
                    .HasColumnName("folio")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.FormaDePago)
                    .IsRequired()
                    .HasColumnName("formaDePago")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.LugarExpedicion)
                    .IsRequired()
                    .HasColumnName("lugarExpedicion")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.MetodoDePago)
                    .IsRequired()
                    .HasColumnName("metodoDePago")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Moneda)
                    .IsRequired()
                    .HasColumnName("moneda")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.MunicipioEmisor)
                    .IsRequired()
                    .HasColumnName("municipioEmisor")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.MunicipioReceptor)
                    .IsRequired()
                    .HasColumnName("municipioReceptor")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.NoExteriorEmisor)
                    .IsRequired()
                    .HasColumnName("noExteriorEmisor")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.NoExteriorReceptor)
                    .IsRequired()
                    .HasColumnName("noExteriorReceptor")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.NoInteriorEmisor)
                    .IsRequired()
                    .HasColumnName("noInteriorEmisor")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.NoInteriorReceptor)
                    .IsRequired()
                    .HasColumnName("noInteriorReceptor")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.NombreEmisor)
                    .IsRequired()
                    .HasColumnName("nombreEmisor")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.NombreReceptor)
                    .IsRequired()
                    .HasColumnName("nombreReceptor")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.PaisEmisor)
                    .IsRequired()
                    .HasColumnName("paisEmisor")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.PaisReceptor)
                    .IsRequired()
                    .HasColumnName("paisReceptor")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.RegimenEmisor)
                    .IsRequired()
                    .HasColumnName("regimenEmisor")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Retenciones)
                    .HasColumnName("retenciones")
                    .HasColumnType("text");

                entity.Property(e => e.RfcEmisor)
                    .IsRequired()
                    .HasColumnName("rfcEmisor")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.RfcReceptor)
                    .IsRequired()
                    .HasColumnName("rfcReceptor")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Serie)
                    .IsRequired()
                    .HasColumnName("serie")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.SubTotal)
                    .HasColumnName("subTotal")
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValueSql("'0.00'");

                entity.Property(e => e.TblContratoId)
                    .IsRequired()
                    .HasColumnName("tbl_contrato_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.TipoDeComprobante)
                    .IsRequired()
                    .HasColumnName("tipoDeComprobante")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Total)
                    .HasColumnName("total")
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValueSql("'0.00'");

                entity.Property(e => e.Traslados)
                    .HasColumnName("traslados")
                    .HasColumnType("text");
            });

            modelBuilder.Entity<TblContrato>(entity =>
            {
                entity.ToTable("tbl_contrato");

                entity.HasIndex(e => e.TblDependenciaId)
                    .HasName("fk_tbl_contrato_vs_tbl_dependencia");

                entity.HasIndex(e => e.TblEstatusContratoId)
                    .HasName("fk_tbl_contrato_vs_tbl_estatus_contrato_idx");

                entity.HasIndex(e => e.TblPrioridadId)
                    .HasName("fk_tbl_contrato_vs_tbl_prioridad_idx");

                entity.HasIndex(e => e.TblProcedimientoId)
                    .HasName("fk_tbl_contrato_vs_tbl_procedimiento_idx");

                entity.HasIndex(e => e.TblProyectoId)
                    .HasName("fk_tbl_contrato_vs_tbl_proyecto_idx");

                entity.HasIndex(e => e.TblTipoContratoId)
                    .HasName("fk_tbl_contrato_vs_tbl_tipo_contrato_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.Activo)
                    .HasColumnName("activo")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.Ampliacion)
                    .HasColumnName("ampliacion")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.EsAdministradora)
                    .HasColumnName("es_administradora")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.EstructuraAsignadaId)
                    .HasColumnName("estructura_asignada_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.FechaFin)
                    .HasColumnName("fecha_fin")
                    .HasColumnType("date");

                entity.Property(e => e.FechaFirma)
                    .HasColumnName("fecha_firma")
                    .HasColumnType("date");

                entity.Property(e => e.FechaFormalizacion)
                    .HasColumnName("fecha_formalizacion")
                    .HasColumnType("date");

                entity.Property(e => e.FechaIinicio)
                    .HasColumnName("fecha_Iinicio")
                    .HasColumnType("date");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnName("fecha_registro")
                    .HasColumnType("date");

                entity.Property(e => e.MontoGarantia).HasColumnName("monto_garantia");

                entity.Property(e => e.MontoMaxSinIva).HasColumnName("monto_max_sin_iva");

                entity.Property(e => e.MontoMinSinIva).HasColumnName("monto_min_sin_iva");

                entity.Property(e => e.Necesidad)
                    .HasColumnName("necesidad")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasColumnType("varchar(400)");

                entity.Property(e => e.Numero)
                    .IsRequired()
                    .HasColumnName("numero")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Objeto)
                    .IsRequired()
                    .HasColumnName("objeto")
                    .HasColumnType("varchar(4000)");

                entity.Property(e => e.PorcGarantia).HasColumnName("porc_garantia");

                entity.Property(e => e.PorcMaxDeductivas).HasColumnName("porc_max_deductivas");

                entity.Property(e => e.PorcMaxPenalizacion).HasColumnName("porc_max_penalizacion");

                entity.Property(e => e.PresentoGarantia)
                    .HasColumnName("presento_garantia")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.RequiereRenovacion)
                    .HasColumnName("requiere_renovacion")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.Satisfactorio)
                    .HasColumnName("satisfactorio")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.TblDependenciaId)
                    .HasColumnName("tbl_dependencia_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.TblEstatusContratoId)
                    .IsRequired()
                    .HasColumnName("tbl_estatus_contrato_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.TblPrioridadId)
                    .IsRequired()
                    .HasColumnName("tbl_prioridad_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.TblProcedimientoId)
                    .IsRequired()
                    .HasColumnName("tbl_procedimiento_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.TblProyectoId)
                    .IsRequired()
                    .HasColumnName("tbl_proyecto_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.TblTipoContratoId)
                    .IsRequired()
                    .HasColumnName("tbl_tipo_contrato_id")
                    .HasColumnType("varchar(36)");

                entity.Property(e => e.TipoEstructura)
                    .HasColumnName("tipo_estructura")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Token)
                    .HasColumnName("token")
                    .HasColumnType("varchar(64)");
            });
        }
    }
}
