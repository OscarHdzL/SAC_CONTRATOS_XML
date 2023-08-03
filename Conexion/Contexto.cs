using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modelos.Modelos;
using Modelos.Modelos.Contrato;
using Modelos.Modelos.RegSolDoc;
using Modelos.Modelos.ResponsablesApego;
using Modelos.Modelos.ServidoresPublicos;
using Modelos.Modelos.Responsabilidades;
using Modelos.Modelos.PreguntasFormulario;
using Modelos.Modelos.VerificacionContrato;
using Modelos.Modelos.GestionExpediente;
using Modelos.Modelos.EsquemaPago;
using Modelos.Modelos.Area;
using Modelos.Modelos.Dependencia;
using Modelos.Modelos.Proyectos;
using Modelos.Modelos.Verificacion;

namespace Conexion
{
    public class Contexto : DbContext
    {
         
        public virtual DbQuery<DocumentsFileManager> DocumentsFileManager { get; set; }

        public virtual DbQuery<DocumentsFileManagerVersion> DocumentsFileManagerVersion { get; set; }
        public virtual DbQuery<SolicitudMesaValidacion> SolicitudMesaValidacion { get; set; }
        public virtual DbQuery<ResponseFileManagerInfoVersions> ResponseFileManagerInfoVersions { get; set; }
        public virtual DbQuery<sp_config_contrato_> sp_config_contrato_ { get; set; }
        public virtual DbQuery<ContratoPresupuesto> ContratoPresupuesto { get; set; }
        public virtual DbQuery<sp_config_contrato> sp_config_contrato { get; set; }
        public virtual DbQuery<ModalidadSolParcial> ModalidadSolParcial { get; set; }
        public virtual DbQuery<tbl_agendados> tbl_agendados { get; set; }
        public virtual DbQuery<CrudresponseNum> CrudresponseNum { get; set; }
        public virtual DbQuery<ResponseFileManagerInfo> ResponseFileManagerInfo { get; set; }
        public virtual DbQuery<ResponseFileManager> ResponseFileManager { get; set; }
        public virtual DbQuery<tbl_obligacion> tbl_obligacion { get; set; }
        public virtual DbQuery<tbl_unidad_medida> tbl_unidad_medida { get; set; }
        public virtual DbQuery<tbl_periodo> tbl_periodo { get; set; }
        public virtual DbQuery<tbl_tipo_obligacion> tbl_tipo_obligacion { get; set; }
        public virtual DbQuery<DropDownList> DropDownList { get; set; }
        public virtual DbQuery<tbl_producto_servicio> tbl_producto_servicio { get; set; }
        public virtual DbQuery<tbl_usuario> tbl_usuario { get; set; }
        public virtual DbQuery<tbl_usuario_verifica> tbl_usuario_verifica { get; set; }
        public virtual DbQuery<tbl_sanciones> TBL_SANCIONES { get; set; }
        public virtual DbQuery<tbl_servidor_publico> tbl_servidorespublicos { get; set; }
        public virtual DbQuery<tbl_contrato> tbl_contratos { get; set; }
        public virtual DbQuery<tbl_contrato_list> tbl_contratos_listado { get; set; }
        public virtual DbQuery<tbl_tipo_contrato> tbl_tipos_contrato{ get; set; }
        public virtual DbQuery<tbl_contrato_vista> tbl_contrato_vista { get; set; }
        public virtual DbQuery<tbl_responsable_apego_contrato_regsoldoc> tbl_responsables_apego_contrato_regsoldoc { get; set; }
        public virtual DbQuery<tbl_registro_solicitud_docto_list> tbl_registro_solicitud_docto_list { get; set; }
        public virtual DbQuery<Crudresponse> Crudresponse { get; set; }
        public virtual DbQuery<tbl_contrato_solicitud_docto> tbl_registro_solicitud_docto { get; set; }
        public virtual DbQuery<tbl_contrato_solicitud_docto_add> tbl_registro_solicitud_docto_add { get; set; }
        public virtual DbQuery<tbl_acuerdos_lista> tbl_acuerdos_lista { get; set; }
        public virtual DbQuery<tbl_tipo_aplicacion> tbl_tipo_aplicacion { get; set; }
        public virtual DbQuery<tbl_responsable_apego_contrato_responsabilidad> tbl_responsable_apego_contrato_responsabilidad { get; set; }
        public virtual DbQuery<tbl_obligacion_unitario> tbl_obligacion_unitario { get; set; }
        public virtual DbQuery<tbl_matriz_riesgo> tbl_matriz_riesgo { get; set; }
        public virtual DbQuery<tbl_contrato_solicitud_docto_expediente> tbl_registro_solicitud_docto_expediente { get; set; }
        public virtual DbQuery<tbl_contrato_producto> tbl_contrato_producto { get; set; }
        public virtual DbQuery<tbl_contrato_producto_list> tbl_contrato_producto_list { get; set; }
        public virtual DbQuery<tbl_responsabilidad> tbl_responsabilidad { get; set; }
        public virtual DbQuery<tbl_tipo_respuesta> tbl_tipo_respuesta { get; set; }
        public virtual DbQuery<tbl_nivel_riesgo> tbl_nivel_riesgo { get; set; }
        public virtual DbQuery<tbl_ubicacion_input> tbl_ubicacion_input { get; set; }
        public virtual DbQuery<tbl_ubicacion_output> tbl_ubicacion_output { get; set; }
        public virtual DbQuery<tbl_ubicacion_add> tbl_ubicacion_add { get; set; }
        public virtual DbQuery<tbl_gestion_expediente_contrato_add> tbl_gestion_expediente_contrato_add { get; set; }
        public virtual DbQuery<sp_get_vs_servidor_publico_contrato> sp_get_vs_servidor_publico_contrato { get; set; }
        public virtual DbQuery<CrudresponseIdentificador> CrudresponseIdentificador { get; set; }
        public virtual DbQuery<tbl_pregunta_formulario> tbl_pregunta_formulario { get; set; }
        public virtual DbQuery<tbl_pregunta_formulario_add> tbl_pregunta_formulario_add { get; set; }
        public virtual DbQuery<tbl_verificacion_contrato> tbl_verificacion_contrato { get; set; }
        public virtual DbQuery<tbl_verificacion_contrato_add> tbl_verificacion_contrato_add { get; set; }
        public virtual DbQuery<tbl_contrato_servidor_resp_esquemapago> tbl_contrato_servidor_resp_esquemapago { get; set; }
        public virtual DbQuery<monto_seleccionado_area_partida> tbl_esquema_pago { get; set; }
        public virtual DbQuery<tbl_proveedores_esquemapago> tbl_proveedores_esquemapago { get; set; }
        public virtual DbQuery<tbl_instancia> tbl_instancia { get; set; }
        public virtual DbQuery<tbl_tipo_audiencia_> tbl_tipo_audiencia { get; set; }
        public virtual DbQuery<tbl_canal> tbl_canal { get; set; }
        public virtual DbQuery<tbl_pc_proveedor> tbl_pc_proveedor { get; set; }
        public virtual DbQuery<tbl_producto_servicio_contrato> tbl_producto_servicio_contrato { get; set; }
        public virtual DbQuery<tbl_contrato_add> tbl__contrato_add { get; set; }
        public virtual DbQuery<tbl_proveedor> tbl_proveedor { get; set; }
        public virtual DbQuery<vs_plan_entrega> vs_plan_entrega { get; set; }
        public virtual DbQuery<tbl_plan_monitoreo_estado> tbl_plan_monitoreo_estado { get; set; }
        public virtual DbQuery<tbl_plan_monitoreo_lista> tbl_plan_monitoreo_lista { get; set; }
        public virtual DbQuery<plan_entrega_ubicacion> plan_entrega_ubicacion { get; set; }
        public virtual DbQuery<tbl_obligacion_cls> tbl_obligacion_cls { get; set; }
        public virtual DbQuery<conteoitems> conteoitems { get; set; }
        public virtual DbQuery<sp_get_vs_servidor_publico_> sp_get_vs_servidor_publico_ { get; set; }
        public virtual DbQuery<tbl_esquema_pago_new> tbl_esquema_pago_new { get; set; }
        public virtual DbQuery<sp_tbl_ArchivosMonitoreo> sp_tbl_ArchivosMonitoreo { get; set; }
        public virtual DbQuery<tbl_areas_lista> tbl_areas_lista { get; set; }
        public virtual DbQuery<tbl_subareas_lista> tbl_subareas_lista { get; set; }
        public virtual DbQuery<tbl_areasubordinada_lista> tbl_areasubordinada_lista { get; set; }
        public virtual DbQuery<tbl_subarea> tbl_subarea { get; set; }
        public virtual DbQuery<tbl_area_subordinada> tbl_area_subordinada { get; set; }

        public virtual DbQuery<dependencia_estructura> dependencia_estructura { get; set; }
        public virtual DbQuery<area_estructura> area_estructura { get; set; }
        public virtual DbQuery<subarea_estructura> subarea_estructura { get; set; }
        public virtual DbQuery<areasubordinada_estructura> areasubordinada_estructura { get; set; }
        public virtual DbQuery<lista_capitulos_gastos> lista_capitulos_gastos { get; set; }
        public virtual DbQuery<lista_capitulos_gastos_area> lista_capitulos_gastos_area { get; set; }
        public virtual DbQuery<lista_capitulos_gastos_subarea> lista_capitulos_gastos_subarea { get; set; }
        public virtual DbQuery<lista_capitulos_gastos_area_subordinada> lista_capitulos_gastos_area_subordinada { get; set; }
        public virtual DbQuery<monto_repartido_cg> monto_repartido_cg { get; set; }
        public virtual DbQuery<capitulo_gasto_existente> capitulo_gasto_existente { get; set; }
        public virtual DbQuery<lista_info_capitulo_gasto> lista_info_capitulo_gasto { get; set; }
        //public virtual DbQuery<sp_plan_monitoreo_struc> sp_plan_monitoreo_struc { get; set; }
        //public virtual DbQuery<sp_plan_monitoreo_input> sp_plan_monitoreo_input { get; set; }
        //public virtual DbQuery<sp_plan_monitoreo_ubicacion> sp_plan_monitoreo_ubicacion { get; set; }


        public virtual DbQuery<vs_siderbar> vs_siderbars { get; set; }
        public virtual DbQuery<tbl_ubicaciones_planmonitoreo> tbl_ubicaciones_planmonitoreo { get; set; }
        public virtual DbQuery<sp_productos_ubicacion_monitoreo> sp_productos_ubicacion_monitoreo { get; set; }
        public virtual DbQuery<sp_obligaciones_ubicacion_producto_planmonitoreo> sp_obligaciones_ubicacion_producto_planmonitoreo { get; set; }
        public virtual DbQuery<sp_obligaciones_nocumple> sp_obligaciones_nocumple { get; set; }
        public virtual DbQuery<sp_obligaciones_incumplidas> sp_obligaciones_incumplidas { get; set; }
        public virtual DbQuery<tbl_obligacion_cls_PE> tbl_obligacion_cls_PE { get; set; }
        public virtual DbQuery<tbl_notificacionsanciones> tbl_notificacionsanciones { get; set; }
        public virtual DbQuery<vs_plan_entrega_ejec> vs_plan_entrega_ejec { get; set; }
        public virtual DbQuery<producto_servicio_pe> producto_servicio_pe { get; set; }
        public virtual DbQuery<Token_confirmacion> Token_confirmacion { get; set; }
        public virtual DbQuery<Token_eliminar> Token_eliminar { get; set; }
        public virtual DbQuery<File_name> File_name { get; set; }

        public virtual DbQuery<tbl_area> tbl_area { get; set; }
        public virtual DbQuery<tbl_lista_areas> tbl_lista_areas { get; set; }
        public virtual DbQuery<tbl_dependencia> tbl_dependencia { get; set; }
        public virtual DbQuery<token_ubicacion_PE> token_ubicacion_PE { get; set; }
        public virtual DbQuery<verificar_oblig> verificar_oblig { get; set; }
        public virtual DbQuery<sp_tbl_archivosPE> sp_tbl_archivosPE { get; set; }
        /**/
        public virtual DbQuery<tbl_proveedor_add> tbl_proveedor_add { get; set; }
        public virtual DbQuery<sp_get_vs_servidor_publico_contrato_c> sp_get_vs_servidor_publico_contrato_c { get; set; }
        public virtual DbQuery<tbl_plan_por_obligacion> tbl_plan_por_obligacion { get; set; }
        public virtual DbQuery<tbl_usuarios> tbl_usuarios { get; set; }
        public virtual DbQuery<tbl_tipo_documento> tbl_tipo_documento { get; set; }
        public virtual DbQuery<tbl_tipo_proyecto> tbl_tipo_proyecto { get; set; }
        public virtual DbQuery<tbl_tipo_riesgo> tbl_tipo_riesgo { get; set; }
        public virtual DbQuery<tbl_tipo_riesgo_add> tbl_tipo_riesgo_add { get; set; }
        public virtual DbQuery<tbl_tipo_documento_add> tbl_tipo_documento_add { get; set; }
        public virtual DbQuery<tbl_tipo_proyecto_add> tbl_tipo_proyecto_add { get; set; }
        public virtual DbQuery<tbl_partida_list> tbl_partida_list { get; set; }
        public virtual DbQuery<estructura_tbl_dependencia> estructura_tbl_dependencia { get; set; }
        public virtual DbQuery<estructura_tbl_area> estructura_tbl_area { get; set; }
        public virtual DbQuery<monto_seleccionado_area_partida> monto_seleccionado_area_partida { get; set; }
        public virtual DbQuery<lista_cap_gastos_dep> lista_cap_gastos_dep { get; set; }

        public virtual DbQuery<tbl_proyectos> tbl_proyectos { get; set; }
        public virtual DbQuery<tbl_lista_proyectos> tbl_lista_proyectos { get; set; }
        public virtual DbQuery<tbl_modalidad> tbl_modalidad { get; set; }
        public virtual DbQuery<tbl_partida_upd> tbl_partida_upd { get; set; }

        public virtual DbQuery<tbl_acuerdo_pe> tbl_acuerdo_pe { get; set; }
        public virtual DbQuery<elementos_bandeja> elementos_bandeja { get; set; }
        public virtual DbQuery<lista_cap_gastos_areas> lista_cap_gastos_areas { get; set; }
        public virtual DbQuery<tbl_solicitud> tbl_solicitud { get; set; }
        public virtual DbQuery<contador_solicitud> contador_solicitud { get; set; }

        public virtual DbQuery<existe_partida> existe_partida { get; set; }
        public virtual DbQuery<tbl_usuarios_list> tbl_usuarios_list { get; set; }
        public virtual DbQuery<suma_c_areas_padre> suma_c_areas_padre { get; set; }
        public virtual DbQuery<monto_asignacion> monto_asignacion { get; set; }
        public virtual DbQuery<tbl_convocatoria> tbl_convocatoria { get; set; }
        public virtual DbQuery<tbl_licitante> tbl_licitante { get; set; }
        public virtual DbQuery<tbl_convocatoria_add> tbl_convocatoria_add { get; set; }
        public virtual DbQuery<solicitud_funcionario> solicitud_funcionario { get; set; }
        public virtual DbQuery<tbl_obligacion_conv_add> tbl_obligacion_conv_add { get; set; }
        public virtual DbQuery<tbl_convocatoria_obligaciones> tbl_convocatoria_obligaciones { get; set; }
        public virtual DbQuery<tbl_solicitud_observador> tbl_solicitud_observador { get; set; }
        public virtual DbQuery<tbl_solicitud_observador_list> tbl_solicitud_observador_list { get; set; }
        public virtual DbQuery<tbl_junta_aclaraciones> tbl_junta_aclaraciones { get; set; }
        public virtual DbQuery<tbl_junta_aclaraciones_list> tbl_junta_aclaraciones_list { get; set; }
        public virtual DbQuery<tbl_programacion> tbl_programacion { get; set; }
        public virtual DbQuery<licitante_propuesta> licitante_propuesta { get; set; }

        public virtual DbQuery<tbl_responsable_convocatoria> tbl_responsable_convocatoria { get; set; }
        public virtual DbQuery<tbl_responsable_convocatoria_lista> tbl_responsable_convocatoria_lista { get; set; }
        public virtual DbQuery<tbl_convocatoria_penalizacion> tbl_convocatoria_penalizacion { get; set; }
        public virtual DbQuery<tbl_convocatoria_penalizacion_lista> tbl_convocatoria_penalizacion_lista { get; set; }
        public virtual DbQuery<tbl_convocatoria_condicion> tbl_convocatoria_condicion { get; set; }
        public virtual DbQuery<tbl_convocatoria_condicion_lista> tbl_convocatoria_condicion_lista { get; set; }
        public virtual DbQuery<grid_evaluacion_propuestas_solicitud> grid_evaluacion_propuestas_solicitud { get; set; }
        public virtual DbQuery<tbl_convocatoria_pago> tbl_convocatoria_pago { get; set; }
        public virtual DbQuery<tbl_convocatoria_pago_lista> tbl_convocatoria_pago_lista { get; set; }
        public virtual DbQuery<tbl_convocatoria_criterio> tbl_convocatoria_criterio { get; set; }
        public virtual DbQuery<tbl_convocatoria_criterio_lista> tbl_convocatoria_criterio_lista { get; set; }
        public virtual DbQuery<remitidas> remitidas { get; set; }
        public virtual DbQuery<proposiciones> proposiciones { get; set; }
        public virtual DbQuery<tbl_documento_validacion> tbl_documento_validacion { get; set; }
        public virtual DbQuery<tbl_documento_validacion_lista> tbl_documento_validacion_lista { get; set; }
        public virtual DbQuery<proposiciones_evaluadas> proposiciones_evaluadas { get; set; }

        public virtual DbQuery<tbl_fallo> tbl_fallo { get; set; }
        public virtual DbQuery<tbl_firmantes> tbl_firmantes { get; set; }
        public virtual DbQuery<tbl_Responsable> tbl_Responsable { get; set; }
        public virtual DbQuery<tbl_Proveedores> tbl_Proveedores { get; set; }
        public virtual DbQuery<comentarios> comentarios { get; set; }
        public virtual DbQuery<tbl_instancia_contrato> tbl_instancia_contrato { get; set; }
        public virtual DbQuery<tbl_instancia_contrato_get> tbl_instancia_contrato_get { get; set; }
        public virtual DbQuery<validar_ubicacion_ligada> validar_ubicacion_ligada { get; set; }
        public virtual DbQuery<json_presupuesto_sol> json_presupuesto_sol { get; set; }
        public virtual DbQuery<tbl_solicitud_documento_adjunto> tbl_solicitud_documento_adjunto { get; set; }
        public virtual DbQuery<tbl_documento_adjunto_solicitud> tbl_documento_adjunto_solicitud { get; set; }
        public virtual DbQuery<tbl_solicitud_estudio_mercado> tbl_solicitud_estudio_mercado { get; set; }
        public virtual DbQuery<tbl_suficiencia_add> tbl_suficiencia_add { get; set; }
        public virtual DbQuery<tbl_solicitud_suficiencia> tbl_solicitud_suficiencia { get; set; }
        public virtual DbQuery<tbl_estudio_mercado> tbl_estudio_mercado { get; set; }
        public virtual DbQuery<tbl_tipo_dictamen> tbl_tipo_dictamen { get; set; }
        public virtual DbQuery<tbl_dictamen> tbl_dictamen { get; set; }
        public virtual DbQuery<tbl_cotizacion_solicitud> tbl_cotizacion_solicitud { get; set; }
        public virtual DbQuery<tbl_cotizacion_sol_crud> tbl_cotizacion_sol_crud { get; set; }        
        public virtual DbQuery<tbl_planes_sin_esquema> tbl_planes_sin_esquema { get; set; }
        public virtual DbQuery<ResponseFileList> ResponseFileList { get; set; }
        public virtual DbQuery<tbl_rol_usuario_response> tbl_rol_usuario_response { get; set; }
        public virtual DbQuery<tbl_tipo_ejecucion> tbl_tipo_ejecucion { get; set; }
        public virtual DbQuery<tbl_tipo_prioridad> tbl_tipo_prioridad { get; set; }
        public virtual DbQuery<lista_tipo_interlocutor> lista_tipo_interlocutor { get; set; }
        public virtual DbQuery<tbl_obligacion_producto> tbl_obligacion_producto { get; set; }
        public virtual DbQuery<vs_plan_entrega_detalle_producto> vs_plan_entrega_detalle_producto { get; set; }
        public virtual DbQuery<tbl_procedimiento_add> tbl_procedimiento_add { get; set; }
        public virtual DbQuery<tbl_procedimiento> tbl_procedimiento { get; set; }
        public virtual DbQuery<info_interlocutor_comercial> info_interlocutor_comercial { get; set; }
        public virtual DbQuery<dependencias_usuario> dependencias_usuario { get; set; }
        public virtual DbQuery<proveedor_dependencia> proveedor_dependencia { get; set; }
        public virtual DbQuery<tbl_dependencia_x_permiso> tbl_dependencia_x_permiso { get; set; }
        public virtual DbQuery<lista_verificion_x_contrato> lista_verificion_x_contrato { get; set; }
        public virtual DbQuery<ReporteSancionesConsulta> ReporteSancionesConsulta { get; set; }
        public virtual DbQuery<tbl_esquema_pago_info_correo> tbl_esquema_pago_info_correo { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
            int TipoBase = int.Parse(Configuration["TipoBase"].ToString());
            switch (TipoBase)
            {
                //Conexion SQL Server
                case 1:
                    optionBuilder.UseSqlServer(Configuration.GetConnectionString("ConnectionDBSQLServer"));
                    break;
                //Conexion MySql
                case 2:
                    optionBuilder.UseMySql(Configuration.GetConnectionString("ConnectionDBMySQL"));
                    break;
                case 3:
                    optionBuilder.UseNpgsql(Configuration.GetConnectionString("ConnectionDBPostGreSQL"));
                    break;
            }
        }
    }
}
