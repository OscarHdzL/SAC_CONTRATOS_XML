//*****************F****************
$(document).ready(function () {
	//Existe();

    callTipoProgramacion();
   
});
//*****************TF****************
function AddProgramacion_() {
	console.log('---tyr---');
	var evaluacion = validateProgramacion();
	console.log('---thor---');
	if (evaluacion.Bit) {
		Swal.fire({
			type: 'error',
			title: 'Hay un error en los datos de entrada',
			text: evaluacion.Texto
		});
		return;
	}
	$.ajax({

		dataType: 'json',
		cache: false,
		contentType: 'application/json',
		processData: false,
		data: JSON.stringify(evaluacion.objeto),
		type: 'post',

		success: function (data) {


			if (data[0].cod == '00000000-0000-0000-0000-000000000000') {
				$('#HD_fecha').val(evaluacion.objeto.FECHA_EVENTO);
				ValidarAvanceFase();
				Swal.fire({
					type: 'error',
					title: data[0].msg,

				}).then(function (isConfirm) {
					if (isConfirm) {

						GetHistorialActas('#tbl_Progra');
						
						//*****************F****************
						//Existe();
						//*****************TF****************
					}

				});
			}
			else {

				GetHistorialActas('#tbl_Progra');
				Swal.fire({
					type: 'success',
					title: data[0].msg
				});
				ValidarAvanceFase();
			}



		},
		error: function (data) {


			Swal.fire({
				type: 'error',
				title: data.responseJSON.Message
			})
		},
		type: 'POST',
		url: $('#EndPointAQ').val() + 'Eventos/Agendar'
	});
}
function validateProgramacion() {
	var OBJ = ProgramacionClass;
	var Response = { Texto: '', Bit: true, objeto: null };
	//TipoProgramación
	if ($('.cmb_programacion').val() == '') {
		Response.Texto = 'Debé seleccionar un tipo de programación';
		Response.Bit = true;
		return Response;
	}
	//Fecha Programación
	if ($('.txt_fecha_pro').val() == '') {
		Response.Texto = 'Debé selccionar una fecha';
		Response.Bit = true;
		return Response;
	}
	//Fecha Hora
	if ($('.txt_hora_pro').val() == '') {
		Response.Texto = 'Debé selccionar una Hora';
		Response.Bit = true;
		return Response;
	}
	//Fecha Direccion
	if ($('.textarea_direccion').val() == '') {
		Response.Texto = 'Debé ingresar una dirección';
		Response.Bit = true;
		return Response;
	}
	if (ValidaCadena($('.textarea_direccion').val(), 'Dirección') != '') {
		Swal.fire({
			type: 'error',
			title: 'Hay un error en los datos de entrada',
			text: 'El campo "Dirección" no puede contener caracteres especiales'
		})
		return;
	}
	//Fecha Estado
	if ($('.cmb_estado').val() == '') {
		Response.Texto = 'Debé seleccionar un Estado';
		Response.Bit = true;
		return Response;
	}
	//Fecha municipio
	if ($('.cmb_municipio').val() == '') {
		Response.Texto = 'Debé seleccionar un Municipio';
		Response.Bit = true;
		return Response;
	}

	OBJ.p_tbl_tipo_programacion_id = $('.cmb_programacion').val();
	OBJ.p_Fecha_Evento = $('.txt_fecha_pro').val();
	OBJ.p_Direccion = $('.textarea_direccion').val();
	OBJ.p_tbl_ciudad_id = $('.cmb_municipio').val();
	OBJ.p_action = 1;






	Response.Texto = '';
	Response.Bit = false;
	Response.objeto = OBJ;
	return Response;
}

function getURL(token_) {
	var RES_ = '';
	var Uri = $('#EndPointFileAQ').val() + 'GeneraUrl/' + token_ + "/1";
	//alert(Uri);
	var URIENC = '';
	$.get(Uri, function (data, status) {
		//alert(data);
		URIENC = data;
		//window.open($('#EndPointFileAQ').val() + "Viewer/" + URIENC, '_blank');
		RES_ = $('#EndPointFileAQ').val() + "Viewer/" + URIENC;
		var SCRH = ((screen.height / 4) * 3) - 40;

		$('#viewer_window_iframe').css('height', SCRH);
		$('#viewer_window_iframe').attr('src', RES_);
		modalVisualizacion();
	});
	
}

function modalVisualizacion() {
	$('#viewer_window').modal('show');
}

function uploadActa() {
	if ($('#file_programacion').val() == '') {
		return "Imposible guardar acta, debe cargar un archivo";
	}
	var fd = new FormData();
	fd.append('file', $('#file_programacion').prop('files')[0]);

	$.ajax({
		url: $('#EndPointFileAQ').val() + "Upload",
		data: fd,
		processData: false,
		contentType: false,
		type: 'POST',
		success: function (data) {
			var response_ = Updaterow(data);
			
			showfiles(false, '');

			Swal.fire({
				type: 'success',
				title: 'Archivo guardado exitosamente'
			});
			GetHistorialActas('#tbl_Progra');
			return "1";
		},
		error: function (xhr, status, error) {
			showfiles(false, '');
			Swal.fire({
				type: 'success',
				title: 'Archivo guardado exitosamente'
			});
			return "Imposible guardar acta.";
		}
	});
}

function Updaterow(token__) {
	var Uri = $('#EndPointAQ').val() + 'Eventos/Token';
	var fd = new FormData();
	fd.append('p_id_', $('#hide_file_programacion').val());
	fd.append('p_Token_', token__);


	$.ajax({
		url: Uri,
		data: fd,
		processData: false,
		contentType: false,
		type: 'POST',
		success: function (data) {
			return 1;
		},
		error: function (xhr, status, error) {
			return 0;

		}
	});
}

function callTipoProgramacion() {
	var Uri = $('#EndPointAQ').val() + 'Eventos/TipoProgramacion';
	$.get(Uri, function (data, status) {
		var body_ = "<option value=''>Seleccione...</option>";
		for (var i = 0; i <= data.length - 1; i++) {
			body_ = body_ + "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
		}
        $('.cmb_programacion').html(body_);
        //cmb_prog_select();
	}, 'json');

}

function CallEstadoProg() {
	var Uri = $('#EndPointAQ').val() + 'Eventos/Estado';
	$.get(Uri, function (data, status) {
		var body_ = "<option value=''>Seleccione...</option>";
		for (var i = 0; i <= data.length - 1; i++) {
			body_ = body_ + "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
		}
		$('#cmb_estado').html(body_);
	}, 'json');
}


function CallCiudad() {
	var Uri = $('#EndPointAQ').val() + 'Eventos/Estado/' + $('.cmb_estado').val() + '/Ciudad';
	$.get(Uri, function (data, status) {
		var body_ = "<option value=''>Seleccione...</option>";
		for (var i = 0; i <= data.length - 1; i++) {
			body_ = body_ + "<option value='" + data[i].value + "'>"
				+ data[i].text + "</option>";
		}
		$('.cmb_municipio').html(body_);
	}, 'json');
}


var Indicador = "#tbl_Progra";
setTimeout(function () {
	$('.txt_fecha_pro').datetimepicker({
		format: 'YYYY-MM-DD',
		locale: 'es'
	});
	$(".cmb_estado").change(function () {
		CallCiudad();
	});


}, 500);

var ProgramacionClass = {
	p_id:'00000000-0000-0000-0000-000000000000',
	p_tbl_tipo_programacion_id: '00000000-0000-0000-0000-000000000000',
	p_Fecha_Evento: "1700-01-01 00:00:00",
	p_Direccion: "",
	p_tbl_ciudad_id: '00000000-0000-0000-0000-000000000000',
	p_tbl_estatus_programacion_id: '02098a70-5fe6-11ea-8324-00155d1b3502',
	p_Token: '',
	p_inclusion: "1700-01-01 00:00:00",
	p_action: 1,
	p_tbl_solicitud_id: $('#_SOLICITUD').val()
}

function GetHistorialActas(identificador) {
	//var URI = "/Request/Programacion/Sol/Eventos/" + $('#HDSOL_Agenda').val() + "/" + $('.cmb_programacion').val();
    var URI = $('#EndPointAQ').val() + "Eventos/Agendados/" + $('#_SOLICITUD').val() + "/TipoEvento/" + $('#cmb_programacion').val();

	$.get(URI, function (data, status) {
		var Arreglo_arreglos = [];
		for (var i = 0; i <= data.length - 1; i++) {
			var Interno = [];
			Interno.push(data[i].estado);
            Interno.push(data[i].ciudad);
            var incl = (data[i].inclusion).split('T');
            Interno.push(incl[0]);
			Interno.push(data[i].estatus);
			if (data[i].token == '') {
				Interno.push("<button class='btn btn-info' onclick=\"showfiles(true, '" + data[i].iD_EVENTO_PROGRAMADO + "');\"> Adjuntar Actas </button> ");
			}
			else {
				Interno.push("<button class='btn btn-success' onclick=\"getURL('" + data[i].token + "');\"> Descargar PPTA </button>");
			}
			if (data[i].estatus == 'PROGRAMADO' ) {
				Interno.push("<button class='btn btn-danger' onclick=\"DeleteItem('" + data[i].iD_EVENTO_PROGRAMADO + "');\"> Terminar </button> ");
			}
			else {
				Interno.push("");
			}
			Arreglo_arreglos.push(Interno);
		}
		var table = $(identificador).DataTable();
		table.destroy();
		console.log(Arreglo_arreglos);
		$(identificador).DataTable({
			data: Arreglo_arreglos,
			columns: [
				{ title: "Estado" },
				{ title: "Municipio" },
				{ title: "Fecha" },
				{ title: "Estatus" },
				{ title: "Actas" },
				{ title: "Controles" }

			]
		});
	});
}

//function Existe() {
//	var Sol = $('#HDSOL_Agenda').val();
//	$.get("/Request/Programacion/Existe/" + Sol, function (data, status) {
//		if (data) {
//			$('.btnOk').prop('disabled', false);
//		}
//		else {
//			$('.btnOk').prop('disabled', true);
//		}
//	});
//}
//$('#CerrarP').click(function () {
//    location.reload();
//    //if (location.href.toLowerCase().indexOf('fallo/fallo') > 0) {
//    //    location.reload();
//    //} else {
//    //    window.location.href = "/Bandeja";
//    //}
//})
//*****************TF****************
//function GetMunicipios() {
//	$('.cmb_municipio').html('');
//	var Estado = $('.cmb_estado').val();
//	$.get("/Request/Programacion/Estado/" + Estado + "/Municipios/Get", function (data, status) {
//		var Body = "<option value=''>Seleccione...</option>";
//		for (var i = 0; i <= data.length - 1; i++) {
//			Body = Body + "<option value='" + data[i].ID_MUNICIPIO_PK + "'>" + data[i].NOMBRE_MUNICIPIO + "</option>";
//		}
//		$('.cmb_municipio').html(Body);
//	}, 'json');
//}
//$('.closeaction').click(function () {
//	if (location.href.toLowerCase().indexOf('adquisiciones/fallo') > 0) {
//		location.reload();
//	}
//})


function openDialog() {
	$('#ProgramacionEventos').modal('show');
	callTipoProgramacion();
    CallEstadoProg();
}

function cmb_prog_select() {
    $.get($('#EndPointAQ').val()+ "Eventos/Get/Tipo_Prog/" + $('#Sigla_Programacion').val(), function (data, status) {
		$('.cmb_programacion').val(data[0].value);
		GetHistorialActas(Indicador);
    });
}

function CallModalProgramacion() {
	CallEstadoProg();
	cmb_prog_select();
	$('#ProgramacionEventos').modal('show');
	//$('.cmb_programacion').val(type);
	$('.cmb_programacion').prop('disabled', true);
	
}
function DeleteItem(item) {
	var Uri = $('#EndPointAQ').val() + 'Eventos/Estatus';
	var fd = new FormData();
	fd.append('p_id_', item);

	$.ajax({
		url: Uri,
		data: fd,
		processData: false,
		contentType: false,
		type: 'POST',
		success: function (data) {
			GetHistorialActas('#tbl_Progra');
            ValidarAvanceFase();
			return 1;
		},
		error: function (xhr, status, error) {
			return 0;

		}
	});
}
function showfiles(bit, id) {

	$("#file_programacion").replaceWith($("#file_programacion").val('').clone(true));
	$('#hide_file_programacion').val(id);
	if (bit) {
		$(".R1_body").hide();
		$(".R2_body").hide();
		$(".R1_file").show();
		$(".R2_file").show();
	}
	else {
		$(".R1_body").show();
		$(".R2_body").show();
		$(".R1_file").hide();
		$(".R2_file").hide();
	}
}


//function SendFile_Progra(id) {
//	var file_data = $('#file_programacion').prop('files')[0];
//	var form_data = new FormData();
//	form_data.append('PrograFile', file_data);

//	$.ajax({

//		dataType: 'text',
//		cache: false,
//		contentType: false,
//		processData: false,
//		data: form_data,
//		type: 'post',

//		success: function (data) {
//			Swal.fire({
//				type: 'success',
//				title: 'Archivo guardado exitosamente'
//			});
//			showfiles(false, id);
//			GetHistorialActas(Indicador);
//		},

//		error: function () {
//			Swal.fire({
//				type: 'error',
//				title: 'Ocurrió un error al cargar el archivo'
//			});
//		},
//		processData: false,
//		type: 'POST',
//		url: '/Request/Programacion/Files/' + id
//	});
//}
//function GetLink(Token) {
//	$.get("/Request/Cotizacion/Dowload/" + Token, function (data, status) {
//		window.open(data, '_blank');
//	});
//}


$(".btn_file_programacion").click(function () {
	uploadActa();
	GetHistorialActas('#tbl_Progra');
});

function refresh_tbls() {
	Getfuncionarios();
	GetObservadores();
	//getFecha();
}
function getFecha() {
	//$.get("/Request/ActaVisita/GetFecha/" + $('#HDSOL_Agenda').val(), function (data, status) {
	//	$('.fecha_event').html(data);
	//});

    //ººººººººººººººººº   Validar
    $.get($('#EndPointAQ').val() + 'Eventos/Get/Solicitud/' + $('#_SOLICITUD').val() + '/SiglaTipoEvento/' + $('#Sigla_Programacion').val(), function (data, status) {
        var fecha = (data.fecha_Evento).split('T');
        var date = fecha[0].split('-');
        var anio = date[0];
        var mes = date[1];
        var dia = date[2];
        var fecha_full = dia + '/' + mes + '/' + anio;

		$('#HD_programacion').val(data.id);
		refresh_tbls();


		$('.fecha_event').html(fecha_full);
		$('#txt_FechaFallo').val(fecha_full);
		$('#txtDireccion').val(data.direccion);
		$('#txtEstado').val(data.estado);
		$('#txtMunicipio').val(data.ciudad);
		
		
    });
}

function ValidarAvanceFase() {
    $.get($('#EndPointAQ').val() + "Eventos/Get/Solicitud/" + $('#_SOLICITUD').val() + "/SiglaTipoEvento/" + $('#Sigla_Programacion').val(), function (data, status) {
		if (data == undefined || null) {
			$(".exist").each(function () {
				$(this).hide();
			});
			$(".noexist").each(function () {
				$(this).show();
			});
		}
		else {
			//$('#HD_programacion').val(data);
			$(".noexist").each(function () {
				$(this).hide();
			});
			$(".exist").each(function () {
				$(this).show();
				getFecha();
			});
		}
		
	});
}


