$(document).ready(function () {
	$('#tbl_observadores').DataTable({});

});

$(function () {
	GetObservadores();
});

$(".btn-save-observador").click(function () {
	if ($('.textobservadores').val() == '') {
		Swal.fire({
			type: 'error',
			title: 'Debé Ingresar un observador'
		});
		return;
	}
	if (ValidaCadena($('.textobservadores').val(), 'Observador') != '') {
		Swal.fire({
			type: 'error',
			title: 'Hay un error en los datos de entrada',
			text: 'El campo "Observador" no puede contener caracteres especiales'
		})
		return;
	}
	sendObservador();
});

function GetObservadores() {

	$.get("/Request/Observador/Get/Convocatoria/" + $('#idconvocatoria').val() + "/" + $('#txt_tipo').val() + "/" + $('#HD_programacion').val(), function (data, status) {
		var Arreglo_arreglosdot = [];
		for (var i = 0; i <= data.length - 1; i++) {
			var InternoArr = [];

			InternoArr.push(data[i].tbl_convocatoria.Folio);
			InternoArr.push(data[i].Observador);
			//if (data[i].tipo == 'eco') {
			//	InternoArr.push(data[i].tipo == 'eco' ? 'Económico' : 'Técnico');
			//}
			//else if (data[i].tipo == 'tec') {
			//	InternoArr.push(data[i].tipo == 'eco' ? 'Económico' : 'Técnico');
			//}
			//else {
			//	InternoArr.push('-');
			//}
			//InternoArr.push(data[i].tipo == 'eco' ? 'Económico' : 'Técnico');

			InternoArr.push("<button class='btn btn-danger' onclick=\"DeleteItemObservador('" + data[i].id + "');\">Eliminar</button>");

			Arreglo_arreglosdot.push(InternoArr);
		}

		var table = $('#tbl_observadores').DataTable();

		table.destroy();

		$('#tbl_observadores').DataTable({
			data: Arreglo_arreglosdot,
			columns: [
				{ title: "Folio Conv." },
				{ title: "Observador" },
				//{ title: "Tipo" },

				{ title: "Acciones" }
			],
			columnDefs: [
				{
					targets: -1,
					className: 'dt-body-center'
				}]
		});

	});
}

function DeleteItemObservador(item) {
	$.post("/Request/Observador/delete/" + item, function (data, status) {
		if (status == 'success') {
			GetObservadores();
		}

	});
}

function sendObservador() {

	var evaluacion = ObservadoresConvClass;

	evaluacion.tbl_convocatoria_id = $('#idconvocatoria').val();
	evaluacion.Observador = $('.textobservadores').val();
	evaluacion.tipo = $('#txt_tipo').val();
	evaluacion.Eventos = $('#HD_programacion').val();




	$.ajax({

		dataType: 'json',  // what to expect back from the PHP script, if anything
		cache: false,
		contentType: 'application/json',
		processData: false,
		data: JSON.stringify(evaluacion),
		type: 'post',

		success: function (data) {


			GetObservadores();


		},
		error: function (data) {
			console.log(data);
			Swal.fire({
				type: 'error',
				title: 'Error al guardar funcionario',
				text: data.responseJSON.Message
			})
		},
		processData: false,
		type: 'POST',
		url: '/Request/Observador/add'
	});


}

var ObservadoresConvClass = {
	id: 0,
	tbl_convocatoria_id: '',
	Observador: '',
	tipo: '',
	inclusion: '2019-09-18',
	Eventos: 0
}
