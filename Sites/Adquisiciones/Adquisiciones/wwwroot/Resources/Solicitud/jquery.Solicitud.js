///Globals
var Obj_presupuesto = [];
var rol_solicitante = 'SOL';
var rol_costos = 'COS';
var rol_encargado_legal = 'EL';
var rol_coordinador = 'COR';
var rol_admin_unidad_licitadora = 'AUL';
var rol_tec_unidad_licitante = 'TUL';
var rol_autorizador = 'EST';
var rol_dir_admin = 'SFC';
var rol_integ_precios = 'EXP';

///Globals

$(document).ready(function () {
	$('#tbl_adjuntos').DataTable({
		"language": {
			"url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
		},
		"pageLength": 5,
		"lengthMenu": [5]
	});
});

var archvos = [];

$("#add").click(function () {

	if ($('#carga_docts').val() == '') {
		Swal.fire({
			type: 'error',
			title: 'Hay un error en los datos de entrada',
			text: 'Cargue un documento'
		})
		return;
	}
	else if ($('#tipo_dcto').val() == null) {
		Swal.fire({
			type: 'error',
			title: 'Hay un error en los datos de entrada',
			text: 'Seleccione tipo de documento'
		})
		return;
	} 

	var file_data_aray = { file: $('#carga_docts').prop('files')[0], tipo: $('select[id="tipo_dcto"] option:selected').text(), id_tipo: $('#tipo_dcto').val() };
	archvos.push(file_data_aray);

	get_table_files();
	$('#carga_docts').val('');
	$('#tipo_dcto').val('');

});

function get_table_files() {
		var Arreglo_arreglos = [];
		for (var i = 0; i <= archvos.length - 1; i++) {
			var Interno = [];
			Interno.push(archvos[i].file.name);
			Interno.push(archvos[i].tipo);
			Interno.push("<button class='btn btn-success' onclick=(getviewer(" + i + "))><span class='glyphicon glyphicon-eye-open'></span></button> <button class='btn btn-danger' onclick=(del_archivo_array(" + i + "))><span class='glyphicon glyphicon-trash'></span></button> ");
			Arreglo_arreglos.push(Interno);
		}
		var table = $('#tbl_adjuntos').DataTable();
		table.destroy();
		$('#tbl_adjuntos').DataTable({
			"language": {
				"url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
			},
			"pageLength": 5,
			"lengthMenu": [5],
			data: Arreglo_arreglos,
			columns: [
				{ title: "Nom. documento" },
				{ title: "Tipo de documento" },
				{ title: "Acción" }
			],
			columnDefs: [
				{
					targets: '_all',
					className: 'dt-body-center',
					width: '40%', targets: 0,
					width: '40%', targets: 1,
					width: '20%', targets: 2,
				}]			
		});
}

function del_archivo_array(index) {
	archvos.splice([index], 1);
	get_table_files();
}

function getviewer(position) {
	var data = archvos[position].file;
	const objectURL = URL.createObjectURL(data);
		var SCRH = ((screen.height / 4) * 3) - 40;
		$('#viewer_window_iframe').css('height', SCRH);
		$('#viewer_window_iframe').attr('src', objectURL);
		$('#viewer_window').modal('show');
}

$(".ModalPress").click(function () {
	if ($('#drop_dependencia').val() == ('' || null)) {
		Swal.fire({
			type: 'error',
			title: 'Debé seleccionar una dependencia'
		});
		return;
	}
	else {
		$('#HD_Dep_Id').val($('#drop_dependencia').val());
		$('#ModalPresupuestos').modal({ backdrop: 'static', keyboard: false });
		$('#ModalPresupuestos').modal('show');
		var x = []
		INIT_Areas(x);
	}
});

$(function () {

	getDependencias();
	getTipoSolicitud();
	getTipoContratoSolicitud();
	getdrop_tipo_docto();
	getResponsables_solicitud();

	//inicialización Datetime picker
	$('#txt_FechaSolicitud').datetimepicker({
		format: 'YYYY-MM-DD',
		locale: 'es'
	});

	$('#Partidas_tbl').DataTable({
		"language": {
			"url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
		},
		"columnDefs": [
			{ "className": "dt-center", "targets": "_all" },
			{ "width": "0%", "targets": 0 },
			{ "width": "40%", "targets": 1 },
			{ "width": "30%", "targets": 2 },
			{ "width": "15%", "targets": 3 },
			{ "width": "15%", "targets": 4 },

			{
				"targets": [0],
				"visible": false,
				"searchable": false
			}
		],
	});
	var table = $('#Partidas_tbl').DataTable();
	$('#Partidas_tbl tbody').on('click', 'tr', function () {
		if ($(this).hasClass('selected')) {
			$(this).removeClass('selected');
		}
		else {
			table.$('tr.selected').removeClass('selected');
			$(this).addClass('selected');
		}
	});
	$('#drop_dependencia_Pres').prop('disabled', true);

	//$('#txt_montosolicitud, #txt_montoautorizado').mask("#,##000000000000000.0000", { reverse: true });
	//Se cambio la linea anterior comentada por esta:
	//$('#txt_montosolicitud, #txt_montoautorizado').on({
	//    "focus": function (event) {
	//        $(event.target).select();
	//    },
	//    "keyup": function (event) {
	//        $(event.target).val(function (index, value) {
	//            return value.replace(/\D/g, "")
	//                .replace(/([0-9])([0-9]{2})$/, '$1.$2')
	//                .replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ",");
	//        });
	//    }
	//});
});


function getdrop_tipo_docto() {
	var instancia = $('#HDidInstancia').val();
	$.get($('#EndPointAQ').val() + "SerSolicitud/Get_lista_tipo_documento/" + instancia,	
	//$.get("https://localhost:44359/solicitud/Get_lista_tipo_documento/" + instancia,	
		function (data, status) {
			$('#tipo_dcto').html('<option value="" selected disabled >Seleccione... </option>');
			for (var i = 0; i <= data.length - 1; i++) {
				var item = "<option value='" + data[i].id + "'>" + data[i].tipo_documento + "</option>";
				$('#tipo_dcto').append(item);
			}
		});
}


//$("#btnGuardar").click(function () {
//    swal('Éxito!', 'Proceso exitoso', 'success');
//})

$(".btnGoHome").click(function () {
	window.location.href = "Home";
});

function ModalComentario() {
	$('#ModalComentarios').modal('show');
}

$(".btnDoOk").click(function () {
	btnDoOk();
});

function btnDoOk() {
	Swal.fire({
		type: 'question',
		title: 'Confirmación de guardado de solicitud!',
		confirmButtonText: 'Guardar solicitud',
		confirmButtonClass: 'btn btn-success btn-lg'
	}).then((result) => {
		if (result.value) {
			SendRequest();
			//console.log('guardado');
		}
	})
}

//function sol_suf() {
//	console.log('suficiencia');
//	Swal.fire({
//		title: 'Suficiencia guardada',
//		type: 'success'
//	}).then((result) => {
//		if (result.value) {
//			btnDoOk(2);
//		}
//	})
//}
//function sol_est_mercado() {
//	console.log('mercado');
//	Swal.fire({
//		title: 'Estudio de mercado guardado',
//		type: 'success'
//	}).then((result) => {
//		if (result.value) {
//			btnDoOk(3);
//		}
//	})
//}

$("#drop_dependencia").change(function () {
	var Instancia = $('#HDidInstancia').val();
	var Dependencia = $('#drop_dependencia').val();
	//Cargamos Areas
	$.get($('#EndPointAQ').val() + "SerAreas/Get/Dropdown/" + Dependencia,
		function (data, status) {
			$('#drop_area').html('<option value="">Seleccione... </option>');
			for (var i = 0; i <= data.length - 1; i++) {
				var item = "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
				$('#drop_area').append(item);
			}
		});
	//Cargamos proyectos
	$.get($('#EndPointAQ').val() + "SerProyectos/Get/Dropdown/" + Dependencia,
		function (data, status) {
			$('#drop_Proyecto').html('<option  value="">Seleccione... </option>');
			for (var i = 0; i <= data.length - 1; i++) {
				var item = "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
				$('#drop_Proyecto').append(item);
			}
		});
});

function ValidaObject(obj) {
	var Validaciones = true;
	var Response = '';
	/*  NumSolicitud */
	Validaciones = SolicitudClass.p_num_solicitud != '';
	if (!Validaciones) {
		Response = 'Debe ingresa un Número de Solicitud';
		return { Texto: Response, Bit: false }
	}
	else if (Validaciones) {
		if (ValidaCadena(SolicitudClass.p_num_solicitud, 'Solicitud') != '') {
			Response: 'El campo "Solicitud" no puede contener caracteres especiales'
			return { Texto: Response, Bit: false }
		}
	}
	/*  Tipo Solicitud */
	Validaciones = SolicitudClass.p_tbl_tipo_solicitud_id != '';
	if (!Validaciones) {
		Response = 'Debe seleccionar un tipo de solicitud';
		return { Texto: Response, Bit: false }
	}
	/*  Tipo Contrato */
	Validaciones = SolicitudClass.p_tbl_tipo_contrato_id != '';
	if (!Validaciones) {
		Response = 'Debe seleccionar un tipo de contrato';
		return { Texto: Response, Bit: false }
	}
	/*  Fecha Solicitud */
	Validaciones = SolicitudClass.p_fecha_solicitud != '';
	if (!Validaciones) {
		Response = 'Fecha de solictud invalida';
		return { Texto: Response, Bit: false }
	}
	/*  Elaboro */
	Validaciones = SolicitudClass.p_elaboro != '';
	if (!Validaciones) {
		Response = 'El campo "Elaboró" no puede ser vacío';
		return { Texto: Response, Bit: false }
	}
	else if (Validaciones) {
		if (ValidaCadena(SolicitudClass.p_elaboro, 'Elaboró') != '') {
			Response = 'El campo "Elaboró" no puede contener caracteres especiales'
			return { Texto: Response, Bit: false }
		}
	}
	/*  Dependencia */
	Validaciones = SolicitudClass.p_tbl_dependencia_id != '';
	if (!Validaciones) {
		Response = 'Debe seleccionar una dependencia';
		return { Texto: Response, Bit: false }
	}
	/*  Area */
	Validaciones = SolicitudClass.p_tbl_area_id != '';
	if (!Validaciones) {
		Response = 'Debe seleccionar una Area';
		return { Texto: Response, Bit: false }
	}
	/*  Proyecto */
	Validaciones = SolicitudClass.p_tbl_proyecto_id != '';
	if (!Validaciones) {
		Response = 'Debe seleccionar un Proyecto';
		return { Texto: Response, Bit: false }
	}
	/*  descripción */
	Validaciones = SolicitudClass.p_descripcion != '';
	if (!Validaciones) {
		Response = 'El campo "Descripción" no puede ser vacío';
		return { Texto: Response, Bit: false }
	}
	var Documento = archvos.length;;
	Validaciones = Documento != 0;
	if (!Validaciones) {
		Response = 'Debe cargar al menos un documento'
		return { Texto: Response, Bit: false }
	}
	else if (Validaciones) {
		if (ValidaCadena(SolicitudClass.p_descripcion, 'Descripción') != '') {
			Response = 'El campo "Descripción" no puede contener caracteres especiales'
			return { Texto: Response, Bit: false }
		}
	}
	/*  monto solicitud */
	Validaciones = SolicitudClass.p_monto_solicitud != '';
	if (!Validaciones) {
		Response = 'El campo "Monto Solicitud" no puede ser vacío. Debe asignarle presupuesto a la solicitud.';
		return { Texto: Response, Bit: false }
	}

	//Validaciones = SolicitudClass.p_json_pres != '';
	//if (!Validaciones) {
	//	Response = 'El campo "Monto Solicitud" no puede ser vacío';
	//	return { Texto: Response, Bit: false }
	//}

	if (Validaciones) {

		if (ValidaCadena(SolicitudClass.p_num_solicitud, 'Solicitud') != '') {
			Response = 'El campo "Solicitud" no puede contener caracteres especiales'
			return { Texto: Response, Bit: false }
		}
		if (ValidaCadena(SolicitudClass.p_elaboro, 'Elaboro') != '') {
			Response = 'El campo "Elaboro" no puede contener caracteres especiales'
			return { Texto: Response, Bit: false }
		}
		if (ValidaCadena(SolicitudClass.p_descripcion, 'Solicitud') != '') {
			Response = 'El campo "Descripcion" no puede contener caracteres especiales'
			return { Texto: Response, Bit: false }
		}

		return { Texto: '', Bit: true }
	}

}

function ValidaCombos() {
	//DropVal
	var val = 0;
	$(".DropVal").each(function () {
		if ($(this).val() == ('' || null)) {
			val++;
		}
	});
	return val;
}

function archivos_to_bd(token, tipo_docto, nom_docto) {

	var OBJ_Form = {};
	OBJ_Form.id = null;
	OBJ_Form.token = token;
	OBJ_Form.tbl_tipo_documento_id = tipo_docto;
	OBJ_Form.tbl_solicitud_id = $('#id_Solicitud').val();
	OBJ_Form.nom_documento = nom_docto;

	$.ajax({
		dataType: 'text',
		cache: false,
		contentType: 'application/json',
		processData: false,
		data: JSON.stringify(OBJ_Form),
		type: 'post',
		url: ($('#EndPointAQ').val() + "SerSolicitud/Add_dcto_adj")
		//url: ("https://localhost:44359/solicitud/Add_dcto_adj")

	})
}

function SendRequest() {
	LaunchLoader(true);
	var validate = ValidaCombos();
	if (validate > 0) {
		Swal.fire({
			type: 'error',
			title: 'Hay un error en los datos de entrada',
			text: 'Debe seleccionar todos los responsables de la solicitud'
		})
		LaunchLoader(false);
		return;
	}

	else {
		var d = new Date();
		var date = (d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate());

		//var Rol1 = { id: 0, TBLENT_USUARIO_ID: $('#drop_Solicitante').val(), tbl_Solicitud_id: '00000000-0000-0000-0000-000000000000', tblent_CatRol_id: 9, inclusion: date };


		//*Como p_tbl_rol_id se enviaran Siglas para obtenere id en SP. 
		var Rol1 = { p_opt: 2, p_id: '00000000-0000-0000-0000-000000000000', p_tbl_rol_usuario_id: $('#HDidRolUsuario').val(), p_tbl_solicitud_id: '00000000-0000-0000-0000-000000000000', /*p_tbl_rol_id: rol_admin_unidad_licitadora,*/ p_inclusion: date }; //SOLICITANTE
		var Rol2 = { p_opt: 2, p_id: '00000000-0000-0000-0000-000000000000', p_tbl_rol_usuario_id: $('#drop_AdminU').val(), p_tbl_solicitud_id: '00000000-0000-0000-0000-000000000000', /*p_tbl_rol_id: rol_admin_unidad_licitadora,*/ p_inclusion: date };
		var Rol3 = { p_opt: 2, p_id: '00000000-0000-0000-0000-000000000000', p_tbl_rol_usuario_id: $('#drop_TecnicoU').val(), p_tbl_solicitud_id: '00000000-0000-0000-0000-000000000000', /*p_tbl_rol_id: rol_tec_unidad_licitante,*/ p_inclusion: date };
		var Rol4 = { p_opt: 2, p_id: '00000000-0000-0000-0000-000000000000', p_tbl_rol_usuario_id: $('#drop_EncargadoLegal').val(), p_tbl_solicitud_id: '00000000-0000-0000-0000-000000000000', /*p_tbl_rol_id: rol_encargado_legal,*/ p_inclusion: date };
		var Rol5 = { p_opt: 2, p_id: '00000000-0000-0000-0000-000000000000', p_tbl_rol_usuario_id: $('#drop_Coordinador').val(), p_tbl_solicitud_id: '00000000-0000-0000-0000-000000000000', /*p_tbl_rol_id: rol_coordinador,*/ p_inclusion: date };
		var Rol6 = { p_opt: 2, p_id: '00000000-0000-0000-0000-000000000000', p_tbl_rol_usuario_id: $('#drop_Costos').val(), p_tbl_solicitud_id: '00000000-0000-0000-0000-000000000000', /*p_tbl_rol_id: rol_costos,*/ p_inclusion: date };

		var Rol7 = { p_opt: 2, p_id: '00000000-0000-0000-0000-000000000000', p_tbl_rol_usuario_id: $('#drop_autorizador').val(), p_tbl_solicitud_id: '00000000-0000-0000-0000-000000000000', /*p_tbl_rol_id: rol_costos,*/ p_inclusion: date };
		var Rol8 = { p_opt: 2, p_id: '00000000-0000-0000-0000-000000000000', p_tbl_rol_usuario_id: $('#drop_dir_admin').val(), p_tbl_solicitud_id: '00000000-0000-0000-0000-000000000000', /*p_tbl_rol_id: rol_costos,*/ p_inclusion: date };
		var Rol9 = { p_opt: 2, p_id: '00000000-0000-0000-0000-000000000000', p_tbl_rol_usuario_id: $('#drop_integ_prec').val(), p_tbl_solicitud_id: '00000000-0000-0000-0000-000000000000', /*p_tbl_rol_id: rol_costos,*/ p_inclusion: date };

		var objetArray = [Rol1, Rol2, Rol3, Rol4, Rol5, Rol6, Rol7, Rol8, Rol9];
	}

	var SolicitudIntancia = SolicitudClass;

	SolicitudIntancia.p_opt = 2;
	SolicitudIntancia.p_id = $('#id_Solicitud').val();
	SolicitudIntancia.p_num_solicitud = $('#txt_numsol').val();
	SolicitudIntancia.p_tbl_tipo_solicitud_id = $('#drop_TipoSolicitud').val();
	SolicitudIntancia.p_tbl_tipo_contrato_id = $('#drop_TipoContrato').val();
	SolicitudIntancia.p_fecha_solicitud = $('#txt_FechaSolicitud').val();
	SolicitudIntancia.p_elaboro = $('#txt_elaboro').val();
	SolicitudIntancia.p_tbl_dependencia_id = $('#drop_dependencia').val();
	SolicitudIntancia.p_tbl_area_id = $('#drop_area').val();
	SolicitudIntancia.p_tbl_proyecto_id = $('#drop_Proyecto').val();
	SolicitudIntancia.p_descripcion = $('#txt_descripcion').val();



	//Quitamos las comas del monto
	//var montoSoli = $('#txt_montosolicitud').val();
	//montoSoli = montoSoli.replace(/,/g, "");
	//montoSoli = montoSoli.replace(/./g, ",");
	SolicitudIntancia.p_monto_solicitud = $('#txt_montosolicitud').val();
	//fin de los cambios realizados en montos(formato)

	SolicitudIntancia.p_monto_autorizado = 0.0;
	SolicitudIntancia.p_comentarios = $('#txt_comentarios').val();
	SolicitudIntancia.p_tbl_estatus_solicitud_id = '00000000-0000-0000-0000-000000000000';
	SolicitudIntancia.p_inclusion = $('#txt_FechaSolicitud').val();
	// ---------------- Settings GUID
	SolicitudIntancia.p_token_autorizacion = '00000000-0000-0000-0000-000000000000';
	SolicitudIntancia.p_token_solicitante = '00000000-0000-0000-0000-000000000000'; //pendiente de realizar carga de archivos.


	var objVAL = ValidaObject(SolicitudIntancia);

	if (!objVAL.Bit) {
		Swal.fire({
			type: 'error',
			title: 'Hay un error en los datos de entrada',
			text: objVAL.Texto
		})
		LaunchLoader(false);
		return;
	}
	//LaunchLoader(true);

//SE COMENTA HASTA QUE FINALICE LO DE PRESUPUESTOS EN CORE 
//================================================================================================================================

	//if ($('#Partidas_tbl').DataTable().rows().count() <= 0) {
	//	Swal.fire({
	//		type: 'error',
	//		title: 'Hay un error en los datos de entrada',
	//		text: 'Debe ingresar Presúpuestos'
	//	});
	//	return;
	//}

	/////Recuperar presupuesto

	//var obj = JSON.parse($('#HD_Objeto').val());

	//var d = new Date();
	//var anioactual = d.getFullYear();

	//var listaPartidas = [];
	//var listaAreasContrato = [];

	
	//for (var i = 0; i <= obj.length - 1; i++) {

	//	if (obj[i] != null) {
	//		///Se llenan los objetos para el nuevo microservicio PartidasAreasRequesController
	//		var objPartidas = PartidasTempClass;

	//		objPartidas.p_opt = 2;
	//		objPartidas.p_id = '00000000-0000-0000-0000-000000000000';
	//		objPartidas.p_tbl_partida_id = obj[i].idCapitulo;
	//		objPartidas.p_tbl_area_id = obj[i].idArea;
	//		objPartidas.p_id_propietario = '00000000-0000-0000-0000-000000000000';
	//		objPartidas.p_tbl_ejercicio_id = anioactual;
	//		objPartidas.p_monto_ejercido = obj[i].MontoEjercer;
	//		objPartidas.p_numero = obj[i].NumeroPartida;
	//		objPartidas.p_descripcion = obj[i].descripciones;

	//		var String_ = JSON.stringify(objPartidas);
	//		var anonimo = JSON.parse(String_);


	//		listaPartidas.push(anonimo);

	//		var objAreasContrato = AreasContratoTempClass;

	//		objAreasContrato.p_opt = 2;
	//		objAreasContrato.p_id = '00000000-0000-0000-0000-000000000000';
	//		objAreasContrato.p_tbl_area_id = obj[i].idArea;
	//		objAreasContrato.p_monto_ejercido = obj[i].MontoEjercer;
	//		objAreasContrato.p_tbl_partida_id = obj[i].idCapitulo;
	//		objAreasContrato.p_tbl_ejercicio_id = anioactual;
	//		objAreasContrato.p_tbl_contrato_id = '00000000-0000-0000-0000-000000000000';


	//		var String_2 = JSON.stringify(objAreasContrato);
	//		var anonimo2 = JSON.parse(String_2);

	//		listaAreasContrato.push(anonimo2);

	//		/////////////////////////////////
	//	}
	//}

//================================================================================================================================
	///////////Carga de archivo

	//var file_data = $('#FileSolicitante').prop('files')[0];

	for (var i = 0; i <= archvos.length - 1; i++) {
		var form_data_file = new FormData();
		var file_data = archvos[i].file;
		var tipo_docto = archvos[i].id_tipo;
		var nom_docto = archvos[i].file.name;
		form_data_file.append('file', file_data);
		$.ajax({
			url: $("#EndPointFileAQ").val() + 'Upload/',
			dataType: 'text',
			cache: false,
			contentType: false,
			processData: false,
			data: form_data_file,
			type: 'POST',
			async: false,
			success: function (data) {
				var token = data.replace(/[ '"]+/g, '');
				archivos_to_bd(token, tipo_docto, nom_docto);
				//SolicitudIntancia.p_token_solicitante = token;

			},
			error: function (data) {
				var objresponse = JSON.parse(data);
				ErrorSA('', objresponse);
			}
		});

	}



	
//=============================================================================================================================

	SolicitudIntancia.p_json_pres = '';
	SolicitudIntancia.p_nombre_bien_servicio = $('#txt_nom_bien_servicio').val();
	var i = $('#requiere_visita_s').is(":checked") ? 1 : 0;
	var f = $('#requiere_mesa_val').is(":checked") ? 1 : 0;
	SolicitudIntancia.p_visitasitio = i;
	SolicitudIntancia.p_mesa_validacion = f;
	//var file_data = $('#FileSolicitante').prop('files')[0];
	var form_data = new FormData();

	//form_data.append('Solicitante', file_data);

	form_data.append('SolicitudFront', JSON.stringify(SolicitudIntancia));
	form_data.append('Responsables', JSON.stringify(objetArray));
	form_data.append('json_pres', JSON.stringify(baseline));

//SE COMENTA HASTA QUE FINALICE LO DE PRESUPUESTOS EN CORE 
//================================================================================================================================
	//form_data.append('PartidasTemp', JSON.stringify(listaPartidas));
	//form_data.append('AreasContratoTemp', JSON.stringify(listaAreasContrato));

//================================================================================================================================


	//console.log('Solicitud: ' + JSON.stringify(SolicitudIntancia));
	//console.log('Responsables: ' + JSON.stringify(objetArray));
	//console.log('json_pres', JSON.stringify(baseline));
	//console.log('Partidas: ' + JSON.stringify(listaPartidas));
	//console.log('AreasContrato: ' + JSON.stringify(listaAreasContrato));


	console.log(form_data);

	$.ajax({

		dataType: 'text',  // what to expect back from the PHP script, if anything
		cache: false,
		contentType: false,
		processData: false,
		data: form_data,
		type: 'post',


		success: function (data) {
			var objresponse = JSON.parse(data);
			if (objresponse != null) {

				LaunchLoader(false);
				//el indice 0 tiene el id de la solicitud
				$('#_SOLICITUD').val(objresponse[0].id);
				Swal.fire({
					title: 'Solicitud guardada',
					text: "¿Desea agregar un comentario?",
					type: 'success',
					showCancelButton: true,
					confirmButtonColor: '#3085d6',
					cancelButtonColor: '#d33',
					confirmButtonText: 'Generar comentario'
				}).then((result) => {
					if (result.value) {
						;
						$('#ModalComentario').modal('show');
						$('#drop_faseC').prop('disabled', true);
					}
					else {
						window.location.href = "../Bandeja";
					}
				})
			}
			else {
				Swal.fire({
					type: 'error',
					title: 'Error al guardar la solicitud',
					text: objresponse.Excepcion
				})
				LaunchLoader(false);
			}
		},

		error: function () {
			Swal.fire({
				type: 'error',
				title: 'Error al guardar la solicitud'
			})
			LaunchLoader(false);
		},
		processData: false,
		type: 'POST',
		url: $('#EndPointAQ').val() + "SerSolicitud/Add"
		//url: "https://localhost:44359/solicitud/Add"
		
	});

}





$("#drop_dependencia_Pres").change(function () {
	var Instancia = $('#HiddenInstancia').val();
	var Dependencia = $('#drop_dependencia_Pres').val();
	//Cargamos Areas
	$.get("Request/Area/" + Instancia + '/' + Dependencia,
		function (data, status) {
			$('#drop_area_press').html('<option value="">Seleccione... </option>');
			for (var i = 0; i <= data.length - 1; i++) {
				var item = "<option value='" + data[i].Value + "'>" + data[i].Text + "</option>";
				$('#drop_area_press').append(item);
			}
		});

});


$("#drop_area_press").change(function () {
	//$.get($('#EndPointJavaScript').val() + "ListaPartidas?idArea=" + $('#drop_area_press').val(),

	var fecha = new Date();
	var ejercicio = fecha.getFullYear();

	$.get($('#EndPointAQ').val() + "SerPartidaArea/Get/Dropdown/" + $('#drop_area_press').val() + '/' + ejercicio,
		function (data, status) {
			$('#drop_capitulo_press').html('<option value="" disabled selected>Seleccione... </option>');
			for (var i = 0; i <= data.length - 1; i++) {
				var item = "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
				$('#drop_capitulo_press').append(item);
			}
		});
});



$("#drop_capitulo_press").change(function () {
	//$.get($('#EndPointJavaScript').val() + "MontoSeleccionado?idPartida=" + $('#drop_capitulo_press').val() + "&idAmbito=" + $('#drop_area_press').val(),
	$.get($('#EndPointAQ').val() + "SerPartidaArea/Get/MontoSeleccionado/" + $('#drop_area_press').val() + '/' + $('#drop_capitulo_press').val(),
		function (data, status) {
			$('#MontoDisponible').val(data.disponible);
		});
});

$("#MontoEjercer").keyup(function () {
	$("#Montocomprometido").val($("#MontoEjercer").val());
});

function AddPres() {
	var Mensaje = '';
	if ($('#drop_dependencia_Pres').val() == ('' || null)) {
		Mensaje = 'Debé seleccionar una dependencia';
	}
	if ($('#drop_area_press').val() == ('' || null)) {
		Mensaje = 'Debé seleccionar un area';
	}
	if ($('#drop_capitulo_press').val() == ('' || null)) {
		Mensaje = 'Debé ingresar un capítulo de gasto';
	}
	if ($('#desc_pres').val() == '') {
		Mensaje = 'Debé ingresar una Descripción de gasto';
	}
	if ($('#NumeroPartida').val() == '') {
		Mensaje = 'Debé ingresar un número de partida';
	}
	if ($('#MontoEjercer').val() == '' || $('#MontoEjercer').val() == '0') {
		Mensaje = 'Debé ingresar un monto para ejercer';
	}
	if (parseInt($('#MontoDisponible').val()) < parseInt($('#Montocomprometido').val())) {
		Mensaje = 'No cuenta con fondos suficientes';
	}
	var montotmp = ValidarMonto();
	montotmp = montotmp + parseInt($('#MontoEjercer').val());
	if (montotmp > parseInt($('#MontoDisponible').val())) {
		Mensaje = 'No cuenta con fondos suficientes, Validé la lista de partidas';
	}
	if (Mensaje != '') {
		Swal.fire({
			type: 'error',
			title: 'Hay un error en los datos de entrada',
			text: Mensaje
		});
		return;
	}

	//Se valida que no se haya registrado el presupuesto
	var contenido_tabla = $('#Partidas_tbl').DataTable().data().toArray();
	if (contenido_tabla.length != undefined) {
		for (var i = 0; i < contenido_tabla.length; i++) {
			if (contenido_tabla[0][i] == ('id_' + $('#drop_area_press').val() + '_' + $('#drop_capitulo_press').val())) {
				ErrorSA('', 'Ya se registró el area y el capitulo de gasto anteriormente');
				return;
			}
		}
	}




	//Obj_presupuesto
	var contador = parseInt($('#HD_contador').val());
	contador++;
	$('#HD_contador').val(contador);
	var obj_tmp = null;

	obj_tmp = {
		id: contador,
		Dependencia: $('#drop_dependencia_Pres option:selected').text(),
		idDependencia: $('#drop_dependencia_Pres').val(),
		Capitulo: $('#drop_capitulo_press option:selected').text(),
		idCapitulo: $('#drop_capitulo_press').val(),
		Area: $('#drop_area_press option:selected').text(),
		idArea: $('#drop_area_press').val(),
		MontoEjercer: $('#MontoEjercer').val(),
		NumeroPartida: $('#NumeroPartida').val(),
		descripciones: $('#desc_pres').val()

	};
	Obj_presupuesto.push(obj_tmp);

	$('#HD_Objeto').val(JSON.stringify(Obj_presupuesto));

	var Monto = ValidarMonto();
	$('#Monto').val(Monto);




	var t = $('#Partidas_tbl').DataTable();


	t.row.add([
		'id_' + $('#drop_area_press').val() + '_' + $('#drop_capitulo_press').val(),
		$('#drop_dependencia_Pres option:selected').text(),
		$('#drop_capitulo_press option:selected').text(),
		$('#MontoEjercer').val(),
		'<td><button class="btn btn-danger" onclick="Eliminarow(' + $('#HD_contador').val() + ')">Eliminar</button></td>'
	]).draw(false);

}

function Eliminarow(value) {
	var obj = JSON.parse($('#HD_Objeto').val());
	var binary = delete obj[(parseInt(value) - 1)];
	if (binary) {
		$('#HD_Objeto').val(JSON.stringify(obj));
		Obj_presupuesto = JSON.parse($('#HD_Objeto').val());
	}
	var table = $('#Partidas_tbl').DataTable();
	table.row('.selected').remove().draw(false);
	var Monto = ValidarMonto();
	$('#Monto').val(Monto);
}
function ValidarMonto() {
	if ($('#HD_Objeto').val() == "" || $('#HD_Objeto').val() == "0" || $('#HD_Objeto').val() == "0.0") {
		return 0;
	}
	var r = JSON.parse($('#HD_Objeto').val());
	var Monto = 0;
	for (var i = 0; i <= r.length - 1; i++) {
		if (r[i] != null) {
			Monto = parseInt(Monto) + parseInt(r[i].MontoEjercer);
		}
	}
	return Monto;
}
$('#GuardarPS').click(function () {
	$('#txt_montosolicitud').val(ValidarMonto());
})

//	******************************************* MICROSERVICIOS NUEVOS *******************************************


function getDependencias() {
	var instancia = $('#HDidInstancia').val();

	$.get($('#EndPointAQ').val() + "SerDependencia/Get/Dropdown/" + instancia,
		function (data, status) {
			$('#drop_dependencia').html('<option value="" selected disabled >Seleccione... </option>');
			for (var i = 0; i <= data.length - 1; i++) {
				var item = "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
				$('#drop_dependencia').append(item);
			}
		});

}




function getTipoSolicitud() {

	$.get($('#EndPointAQ').val() + "SerSolicitud/Get/TipoSolicitud/Dropdown/",
		function (data, status) {
			$('#drop_TipoSolicitud').html('<option value="" selected disabled >Seleccione... </option>');
			for (var i = 0; i <= data.length - 1; i++) {
				var item = "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
				$('#drop_TipoSolicitud').append(item);
			}
		});

}


function getTipoContratoSolicitud() {
	var instancia = $('#HDidInstancia').val();

	$.get($('#EndPointAQ').val() + "SerSolicitud/Get/TipoContratoSolicitud/Dropdown/" + instancia,
		function (data, status) {
			$('#drop_TipoContrato').html('<option value="" selected disabled >Seleccione... </option>');
			for (var i = 0; i <= data.length - 1; i++) {
				var item = "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
				$('#drop_TipoContrato').append(item);
			}
		});

}


function getResponsables_solicitud() {

	var instancia = $('#HDidInstancia').val();



	$.get($('#EndPointAQ').val() + "SerRespSolic/Get/Dropdown/" + rol_costos + '/' + instancia,
		function (data, status) {
			$('#drop_Costos').html('<option value="" selected disabled >Seleccione... </option>');
			for (var i = 0; i <= data.length - 1; i++) {
				var item = "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
				$('#drop_Costos').append(item);
			}
		});

	$.get($('#EndPointAQ').val() + "SerRespSolic/Get/Dropdown/" + rol_admin_unidad_licitadora + '/' + instancia,
		function (data, status) {
			$('#drop_AdminU').html('<option value="" selected disabled >Seleccione... </option>');
			for (var i = 0; i <= data.length - 1; i++) {
				var item = "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
				$('#drop_AdminU').append(item);
			}
		});



	$.get($('#EndPointAQ').val() + "SerRespSolic/Get/Dropdown/" + rol_tec_unidad_licitante + '/' + instancia,
		function (data, status) {
			$('#drop_TecnicoU').html('<option value="" selected disabled >Seleccione... </option>');
			for (var i = 0; i <= data.length - 1; i++) {
				var item = "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
				$('#drop_TecnicoU').append(item);
			}
		});


	$.get($('#EndPointAQ').val() + "SerRespSolic/Get/Dropdown/" + rol_encargado_legal + '/' + instancia,
		function (data, status) {
			$('#drop_EncargadoLegal').html('<option value="" selected disabled >Seleccione... </option>');
			for (var i = 0; i <= data.length - 1; i++) {
				var item = "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
				$('#drop_EncargadoLegal').append(item);
			}
		});

	$.get($('#EndPointAQ').val() + "SerRespSolic/Get/Dropdown/" + rol_coordinador + '/' + instancia,
		function (data, status) {
			$('#drop_Coordinador').html('<option value="" selected disabled >Seleccione... </option>');
			for (var i = 0; i <= data.length - 1; i++) {
				var item = "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
				$('#drop_Coordinador').append(item);
			}
		});
	$.get($('#EndPointAQ').val() + "SerRespSolic/Get/Dropdown/" + rol_autorizador + '/' + instancia,
		function (data, status) {
			$('#drop_autorizador').html('<option value="" selected disabled >Seleccione... </option>');
			for (var i = 0; i <= data.length - 1; i++) {
				var item = "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
				$('#drop_autorizador').append(item);
			}
		});
	$.get($('#EndPointAQ').val() + "SerRespSolic/Get/Dropdown/" + rol_dir_admin + '/' + instancia,
		function (data, status) {
			$('#drop_dir_admin').html('<option value="" selected disabled >Seleccione... </option>');
			for (var i = 0; i <= data.length - 1; i++) {
				var item = "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
				$('#drop_dir_admin').append(item);
			}
		});
	$.get($('#EndPointAQ').val() + "SerRespSolic/Get/Dropdown/" + rol_integ_precios + '/' + instancia,
		function (data, status) {
			$('#drop_integ_prec').html('<option value="" selected disabled >Seleccione... </option>');
			for (var i = 0; i <= data.length - 1; i++) {
				var item = "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
				$('#drop_integ_prec').append(item);
			}
		});
}



function filterFloat(evt, input) {
	// Backspace = 8, Enter = 13, ‘0′ = 48, ‘9′ = 57, ‘.’ = 46, ‘-’ = 43
	var key = window.Event ? evt.which : evt.keyCode;
	var chark = String.fromCharCode(key);
	var tempValue = input.value + chark;
	if (key >= 48 && key <= 57) {
		if (filter(tempValue) === false) {
			return false;
		} else {
			return true;
		}
	} else {
		if (key == 8 || key == 13 || key == 0) {
			return true;
		} else if (key == 46) {
			if (filter(tempValue) === false) {
				return false;
			} else {
				return true;
			}
		} else {
			return false;
		}
	}
}
function filter(__val__) {
	var preg = /^([0-9]+\.?[0-9]{0,2})$/;
	if (preg.test(__val__) === true) {
		return true;
	} else {
		return false;
	}

}
