$(function () {
    LaunchLoader(true);
});

$(document).ready(function () {
	$('#tbl_observadores').DataTable({});
	GetCatalogs('Modalidad/Catalogos/modalidad', '#drop_modalidad');
	GetCatalogs('Modalidad/Catalogos/licitacion', '#drop_tipolicitacion');
	FillSolicitud();
	GetDocumentos();
});
var con = $('#EndPointAQ').val() + "SerSolicitud/";

function ViewDocto(token_) {
	var RES_ = '';
	var Uri = $('#EndPointFileAQ').val() + 'GeneraUrl/' + token_ + "/10";
	//alert(Uri);
	var URIENC = '';
	$.get(Uri, function (data, status) {
		//alert(data);
		URIENC = data;
		RES_ = $('#EndPointFileAQ').val() + "Viewer/" + URIENC;
		var SCRH = ((screen.height / 4) * 3) - 40;
		$('#viewer_window_iframe').css('height', SCRH);
		$('#viewer_window_iframe').attr('src', RES_);
		$('#viewer_window').modal('show');
		return RES_;
	});

}

$("#Cerra15").click(function () {
	window.location.href = "/Bandeja";
});

function FillSolicitud() {
	var Uri = $('#EndPointAQ').val() + "SerSolicitud/Get/" + $('#_SOLICITUD').val();
	$.get(Uri, function (data, status) {
		$('#txt_numsol').val(data.num_solicitud);
		$('#drop_TipoSolicitud').html('<option>' + data.tipo_solicitud + '</option>');
		$('#drop_TipoContrato').html('<option>' + data.tipo_contrato + '</option>');
		$('#txt_FechaSolicitud').val(data.fecha_solicitud);
		$('#txt_elaboro').val(data.elaboro);
		$('#drop_dependencia').html('<option>' + data.dependencia + '</option>');
		$('#drop_area').html('<option>'+data.area+'</option>');
		$('#drop_Proyecto').html('<option>' + data.proyecto + '</option>');

		$('#txt_descripcion').val(data.descripcion);
		$('#txt_montosolicitud').val(data.monto_solicitud);
		$('#txt_montoautorizado').val(data.monto_autorizado);
		$('#txt_comentarios').val(data.comentarios);
		$('#txt_comentarios_suf').val(data.comentario_suficiencia);
		$('#txt_nom_bien_servicio').val(data.nombre_bien_servicio);

		if (data.visita_sitio == true) {
			$('#cbx_visitaS').prop("checked", true);
		} else if (data.visita_sitio == false) {
			$('#cbx_visitaS').prop("checked", false);
		}

		//token_solicitante

		//$("#Download_file").click(function () { getURL(data[0].token_solicitante) });

		//$('#viewer_window_iframe').attr('src', getURL(data[0].token_solicitante));

 
		
	},'json');
}
function GetDocumentos() {
	$.get(con + "Get_Documentos_Solicitud/" + $('#_SOLICITUD').val(), function (data, status) {
		var Arreglo_arreglosdot = [];
		for (var i = 0; i <= data.length - 1; i++) {
			var InternoArr = [];
			InternoArr.push(data[i].nombre_documento);
			InternoArr.push(data[i].tipo_documento);
			InternoArr.push("<button class='btn btn-primary' onclick=\"ViewDocto('" + data[i].token + "');\"><span class='glyphicon glyphicon-eye-open'></span></button>");
			Arreglo_arreglosdot.push(InternoArr);
		}

		var table = $('#tbl_adjuntos').DataTable();

		table.destroy();

		$('#tbl_adjuntos').DataTable({
			"language": {
				"url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
			},
			"pageLength": 5,
			"lengthMenu": [5],
			data: Arreglo_arreglosdot,
			columns: [
				{ title: "Nom. documento" },
				{ title: "Tipo de documento" },
				{ title: "Acción" }
			],
			columnDefs: [
				{
					targets: -1,
					className: 'dt-body-center'
				}]
		});
		//getSolicitud();
        LaunchLoader(false);
	});
}
function validar_eventos() {
	var Uri = $('#EndPointAQ').val() + "Modalidad/Validar/" + $('#_SOLICITUD').val();
	$.get(Uri, function (data, status) {
		if (data == '1') {
			var x = UploadJustificacion();
		 
		}
		else {

			Swal.fire({
				type: 'error',
				title: 'Datos incompletos',
				text: 'Debe dar de alta todos los eventos'
			})
		}
	});
}



function GetCatalogs(value, jqueryId) {
	var Uri = $('#EndPointAQ').val() + value;
	$.get(Uri, function (data, status) {
			var body = '';
			for (var i = 0; i <= data.length - 1; i++) {
				body = body + "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
				
			}
			body = '<option value="" selected disabled> Seleccione...</option>' + body;
			$(jqueryId).html(body);
	},'json');

}

var ModalidadClass = {
	p_opt: 2,
	p_id: "00000000-0000-0000-0000-000000000000",
	p_tbl_solicitud_id: "8e464dcb-62fe-11ea-8324-00155d1b3502",
	p_tbl_tipo_modalidad_id: "7c4b3b6a-5fd6-11ea-8324-00155d1b3502",
	p_tbl_tipo_licitacion_id: "97412f87-5fd5-11ea-8324-00155d1b3502",
	p_falta_documentacion: 1,
	p_requiere_justificacion: 1,
	p_token: "",
	p_estatus: 1,
	p_visita_sitio: 1,
	p_aclaraciones: 1
};

 

function ShowFiles() {
	if ($('#cbx_justifica').prop('checked') && !$('#file_adjuntardoc').is(":visible")) {
		$('.file_HD').show();
	}
	else if (!$('#cbx_justifica').prop('checked') && $('#file_adjuntardoc').is(":visible")) {
		$('.file_HD').hide();
	}
}

	
function Validate() {
	if (($('#drop_modalidad').val() == null) || ($('#drop_tipolicitacion').val() == null)) {
		return false;
	}
	else {
		return true;
	}
}


function PostModalidad(token__) {


	var validacion = Validate();

	if (!validacion) {
		Swal.fire({
			type: 'error',	
			title: 'Datos de entrada erroneos',
			text: 'Debé completar todos los Campos del formulario'
		});
		
		return;
	}



	var Uri = $('#EndPointAQ').val() + "Modalidad/Asignar";
	var objeto = ModalidadClass;
	ModalidadClass.p_aclaraciones = $('#cbx_aclaraciones').prop('checked') == true ? 1 : 0;
	ModalidadClass.p_estatus = 1;
	ModalidadClass.p_falta_documentacion = $('#cbx_documentacion').prop('checked') == true ? 1 : 0;
	ModalidadClass.p_id = '00000000-0000-0000-0000-000000000000';
	ModalidadClass.p_opt = 2;
	ModalidadClass.p_requiere_justificacion = $('#cbx_justifica').prop('checked') == true ? 1 : 0;
	ModalidadClass.p_tbl_solicitud_id = $('#_SOLICITUD').val();
	ModalidadClass.p_tbl_tipo_licitacion_id = $('#drop_tipolicitacion').val();
	ModalidadClass.p_tbl_tipo_modalidad_id = $('#drop_modalidad').val();
	ModalidadClass.p_token = token__;
	ModalidadClass.p_visita_sitio = $('#cbx_visitaS').prop('checked') == true ? 1 : 0;

	$.ajax({

		dataType: 'json',
		cache: false,
		contentType: 'application/json',
		processData: false,
		data: JSON.stringify(ModalidadClass),
		type: 'post',

		success: function (data) {


			if (data[0].cod == 'success') {
				Swal.fire({
					type: 'success',
					title: 'Exito',
					text: 'La modalidad se asignó con exito.'
				})
			}

			window.location.href = "/Bandeja";
			return "1";
		},
		error: function (data) {

			Swal.fire({
				type: 'error',
				title: 'Error',
				text: 'Hubo un problema al Gurdar la solicitud'
			})

		},
		type: 'POST',
		url: Uri
	});

	


}
 

function UploadJustificacion() {

	var token = '';


	if ($('#cbx_justifica').prop('checked')) {

		if ($('#file_adjuntardoc').val() == "" || $('#file_adjuntardoc').val() == undefined) {
			return 0;
		}

		var fd = new FormData();
		fd.append('file', $('#file_adjuntardoc').prop('files')[0]);

		$.ajax({

			url: $('#EndPointFileAQ').val() + "Upload",
			data: fd,
			processData: false,
			contentType: false,
			type: 'POST',
			success: function (data) {


				return PostModalidad(data);


			},
			error: function (xhr, status, error) {
				return '';
			}
		});
	} else {
		//El token se mandara vacio
		PostModalidad('');
	}

	
}
 




//$(function () {
//	GetObservadores();
//});

//$(".btn-save-observador").click(function () {
//	if ($('.textobservadores').val() == '') {
//		Swal.fire({
//			type: 'error',
//			title: 'Debé Ingresar un observador'
//		});
//		return;
//	}
//	if (ValidaCadena($('.textobservadores').val(), 'Observador') != '') {
//		Swal.fire({
//			type: 'error',
//			title: 'Hay un error en los datos de entrada',
//			text: 'El campo "Observador" no puede contener caracteres especiales'
//		})
//		return;
//	}
//	sendObservador();
//});

//function GetObservadores() {

//	$.get("/Request/Observador/Get/Convocatoria/" + $('#idconvocatoria').val() + "/" + $('#txt_tipo').val() + "/" + $('#HD_programacion').val(), function (data, status) {
//		var Arreglo_arreglosdot = [];
//		for (var i = 0; i <= data.length - 1; i++) {
//			var InternoArr = [];

//			InternoArr.push(data[i].tbl_convocatoria.Folio);
//			InternoArr.push(data[i].Observador);
//			if (data[i].tipo == 'eco') {
//				InternoArr.push(data[i].tipo == 'eco' ? 'Económico' : 'Técnico');
//			}
//			else if (data[i].tipo == 'tec') {
//				InternoArr.push(data[i].tipo == 'eco' ? 'Económico' : 'Técnico');
//			}
//			else {
//				InternoArr.push('-');
//			}
//			//InternoArr.push(data[i].tipo == 'eco' ? 'Económico' : 'Técnico');

//			InternoArr.push("<button class='btn btn-danger' onclick=\"DeleteItemObservador('" + data[i].id + "');\">Eliminar</button>");

//			Arreglo_arreglosdot.push(InternoArr);
//		}

//		var table = $('#tbl_observadores').DataTable();

//		table.destroy();

//		$('#tbl_observadores').DataTable({
//			data: Arreglo_arreglosdot,
//			columns: [
//				{ title: "Folio Conv." },
//				{ title: "Observador" },
//				{ title: "Tipo" },

//				{ title: "Acciones" }
//			],
//			columnDefs: [
//				{
//					targets: -1,
//					className: 'dt-body-center'
//				}]
//		});

//	});
//}

//function DeleteItemObservador(item) {
//	$.post("/Request/Observador/delete/" + item, function (data, status) {
//		if (status == 'success') {
//			GetObservadores();
//		}

//	});
//}

//function sendObservador() {

//	var evaluacion = ObservadoresConvClass;

//	evaluacion.tbl_convocatoria_id = $('#idconvocatoria').val();
//	evaluacion.Observador = $('.textobservadores').val();
//	evaluacion.tipo = $('#txt_tipo').val();
//	evaluacion.Eventos = $('#HD_programacion').val();




//	$.ajax({

//		dataType: 'json',  // what to expect back from the PHP script, if anything
//		cache: false,
//		contentType: 'application/json',
//		processData: false,
//		data: JSON.stringify(evaluacion),
//		type: 'post',

//		success: function (data) {


//			GetObservadores();


//		},
//		error: function (data) {
//			console.log(data);
//			Swal.fire({
//				type: 'error',
//				title: 'Error al guardar funcionario',
//				text: data.responseJSON.Message
//			})
//		},
//		processData: false,
//		type: 'POST',
//		url: '/Request/Observador/add'
//	});


//}

//var ObservadoresConvClass = {
//	id: 0,
//	tbl_convocatoria_id: '',
//	Observador: '',
//	tipo: '',
//	inclusion: '2019-09-18',
//	Eventos: 0
//}

//Quitar acentos




$("#drop_tipolicitacion").change(function () {

	var LICITACION_PUBLICA = 'licitacionpublica';

	var seleccionado = $("#drop_tipolicitacion option:selected").text();
	var seleccionado_ = (seleccionado.replace(/ /g, '').toLowerCase());
	var valor = removeAccents(seleccionado_);

	if (valor == LICITACION_PUBLICA) {
		$("#cbx_visitaS").prop("checked", true);
		$("#cbx_aclaraciones").prop("checked", true);

		$("#cbx_visitaS").prop("disabled", true);
		$("#cbx_aclaraciones").prop("disabled", true);

	} else {
		$("#cbx_visitaS").prop("checked", false);
		$("#cbx_aclaraciones").prop("checked", false);

		$("#cbx_visitaS").prop("disabled", false);
		$("#cbx_aclaraciones").prop("disabled", false);

	}

});


const removeAccents = (str) => {
	return str.normalize("NFD").replace(/[\u0300-\u036f]/g, "");
}
