
$(function () {
	setTimeout(function () {
		if ($('#HD_programacion').val() == '') {
			$(".exist").each(function () {
				$(this).hide();
			});
			$(".noexist").each(function () {
				$(this).show();
			});
		}


	}, 100);

	getFecha();
});

function getFecha() {
	//$.get("/Request/ActaVisita/GetFecha/" + $('#_SOLICITUD').val(), function (data, status) {
	//	$('.fecha_event').html(data);
	//});


	$.get($('#EndPointAQ').val() + 'Eventos/Get/Solicitud/' + $('#_SOLICITUD').val() + '/SiglaTipoEvento/VS', function (data, status) {

		
		//var zero = '0';
		//var convertedStartDate = new Date(data.fecha_Evento);
		//var month = convertedStartDate.getMonth() + 1;
		//var day = convertedStartDate.getDate();
		//var year = convertedStartDate.getFullYear();


		//var fullday = day < 10 ? zero.concat(day) : day;
		//var fullmonth = month < 10 ? zero.concat(month) : month;

		//var shortStartDate = fullday + "/" + fullmonth + "/" + year;
		var fecha = (data.fecha_Evento).split('T');
		var date = fecha[0].split('-');
		var anio = date[0];
		var mes = date[1];
		var dia = date[2];

		var fecha_full = dia + '/' + mes + '/' + anio;

		$('.fecha_event').html(fecha_full);

		$('#HD_programacion').val(data.id);
		



	});
}

function GoBandeja() {
	window.location.href = "/Bandeja";
}


function AvanzaFaseActas() {

	$.get($('#EndPointAQ').val() +  "AvanzarFase/Solicitud/" + $('#_SOLICITUD').val(), function (data, status) {
		if (data.cod == 'success') {
			window.location.href = "/Bandeja";
		} else if (data.cod == 'warning') {
			Swal.fire({
				type: 'error',
				title: 'Hay un error en los datos de entrada',
				text: data.msg
			});
		}
	});
}

function AddActa(Estatus) {

	if ($('#cargaComentario').val() == '') {
		Swal.fire({
			type: 'error',
			title: 'Debé adjuntar un archivo'
		});
		return;
	}


	var Param = ActaVisitaClass;

	console.log(Param);

	Param.p_estatus = Estatus;
	Param.p_tbl_solicitud_id = $('#_SOLICITUD').val();
	Param.p_tbl_tipo_programacion_id = $('#Sigla_Programacion').val();
	//Param.p_token = 
	
	if ($('#cargaComentario').prop('files') != undefined) {


		var form_data_file = new FormData();
		var file_data = $('#cargaComentario').prop('files')[0];

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
				Param.p_token = token;

			},
			error: function (data) {
				var objresponse = JSON.parse(data);
				ErrorSA('', objresponse);
			}
		});



	}

///////////fin Carga de archivo



	var form_data = new FormData();
	form_data.append('data', JSON.stringify(Param));


	$.ajax({

		dataType: 'text',  // what to expect back from the PHP script, if anything
		cache: false,
		contentType: false,
		processData: false,
		data: form_data,
		type: 'post',

		success: function (data) {
			var obj = JSON.parse(data);

			if (obj.cod == 'success') { 
			Swal.fire({
				type: 'success',
				title: 'Acta actualizada con exito'

			});
			setTimeout(function () {
				if (Estatus != 'cerrado') {
					AddProgramacion_();
				}
				else {
					window.location.replace("/Bandeja");
				}
			}, 1000);
			}

		},

		error: function () {

		},
		processData: false,
		type: 'POST',
		url: $('#EndPointAQ').val() + 'VisitaSitio/Add'
	});
}


var ActaVisitaClass = {
	p_opt: 2,
	p_id: '00000000-0000-0000-0000-000000000000',
	p_tbl_solicitud_id: '00000000-0000-0000-0000-000000000000',
	p_token: '',
	p_tbl_tipo_programacion_id: 'VS',
	p_estatus: ''
}


function trigger() {

	$(".btn-add-event2").click(function () {
		AddActa('reagenda');
	});
}

