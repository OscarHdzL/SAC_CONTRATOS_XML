

function INIT_Proveedores() {
	call_Proveedores_catalog();
	var proveedores__ = [];
	$('#proveedores__').val(JSON.stringify(proveedores__));
}



function call_Proveedores_catalog() {
	//consultamos los proveedores que ya tienen un contrato
	$.get($("#EndPointAdmon").val() + 'Contratos/GET/Json/Proveedores/' + $('#contrato_identifier').val(), function (provRegistrados, status) {
		var tmp = [];
		if (provRegistrados.length == 1) {
			tmp.push(provRegistrados[0]);
		}
		else {
			for (var i = 0; i <= provRegistrados.length - 1; i++) {
				tmp.push(provRegistrados[i]);
			}
		}

		var uri = $('#EndPointAdmon').val() + 'Contratos/ConsultarProveedor/' + $('#dependenciaContrato').val();
		var Arreglo_arreglosdot = [];
		$.get(uri, function (data, status) {
			for (var n = 0; n <= provRegistrados.length - 1; n++) {
				for (var i = 0; i <= data.length - 1; i++) {
					var InternoArr = [];
					var id_ = data[i].id;
					var razon_social = data[i].razon_social;
					var domicilio_fiscal = data[i].domicilio_fiscal;
					var correo_electronico = data[i].correo_electronico;
					var rfc = data[i].rfc;

					if (provRegistrados[n] == data[i].id) {
						InternoArr.push(razon_social);
						InternoArr.push(rfc);
						InternoArr.push(domicilio_fiscal);
						InternoArr.push(correo_electronico);
						InternoArr.push("<div style='text-align: center;font-size: 200%;'><i id='fir_" + id_ + "' onclick=\"check_proveedor('" + id_ + "')\" class='fa fa-fw fa-check-circle firmante'></i></div>");

						Arreglo_arreglosdot.push(InternoArr);
					}



				}
			}
			var table = $('#Proveedores').DataTable();

			table.destroy();

			$('#Proveedores').DataTable({
				data: Arreglo_arreglosdot,
				columns: [
					{ title: "Razon Soc." },
					{ title: "RFC" },
					{ title: "Domicilio Fiscal" },
					{ title: "Email" },
					{ title: "Seleccionar" }

				]
			});


			Get_lista_tipo_interlocutor();
		});
	});


}


function check_proveedor(value) {
	if ($('#fir_' + value).hasClass('check-fir')) {
		$('#fir_' + value).removeClass('check-fir');
		var firmantes = JSON.parse($('#proveedores__').val());
		var index = firmantes.indexOf(value);
		firmantes.splice(index, 1);
		$('#proveedores__').val(JSON.stringify(firmantes));
	}
	else {
		var firmantes = JSON.parse($('#proveedores__').val());
		firmantes.push(value);
		$('#proveedores__').val(JSON.stringify(firmantes));
		$('#fir_' + value).addClass('check-fir');
	}

}


function validate_proveedores__() {
	if ($('#proveedores__').val() != '') {
		Swal.fire({
			type: 'success',
			title: 'Datos Correctos',
			text: 'Los datos son correctos'
		});
		$('#a5').val(1);
		$('.div_deductivas').show();
		$('.div_proveedores').hide();
		INIT_adicionales();
	}
	else {
		Swal.fire({
			icon: 'success',
			title: 'Datos Correctos',
			text: 'Los datos son correctos'
		});
	}
}

function Get_lista_tipo_interlocutor() {
	$.get($("#EndPointAdmon").val() + "Proveedor/Get/lista_tipo_interlocutor/1", function (data, status) {
		var htmllistfiltro = "<option value='0' selected>Filtrar por tipo de interlocutor</option>";

		for (var i = 0; i <= data.length - 1; i++) {

			htmllistfiltro += "<option value = '" + data[i].id + "'>" + data[i].nombre + "</option>"
		}
		$('#txt_tipo_interlocutor_list').html(htmllistfiltro);
	});
}

function call_Proveedores_catalog_filtro(tbl_tipo_interlocutor_id) {
	if (tbl_tipo_interlocutor_id != 0) {
		var uri = $('#EndPointAdmon').val() + 'Contratos/ConsultarProveedor/' + $('#dependenciaContrato').val();
		var Arreglo_arreglosdot = [];
		$.get(uri, function (data, status) {
			for (var i = 0; i <= data.length - 1; i++) {
				if (data[i].tbl_tipo_interlocutor_id === tbl_tipo_interlocutor_id) {
					var InternoArr = [];
					var id_ = data[i].id;
					var razon_social = data[i].razon_social;
					var domicilio_fiscal = data[i].domicilio_fiscal;
					var correo_electronico = data[i].correo_electronico;
					var rfc = data[i].rfc;

					InternoArr.push(razon_social);
					InternoArr.push(rfc);
					InternoArr.push(domicilio_fiscal);
					InternoArr.push(correo_electronico);
					InternoArr.push("<div style='text-align: center;font-size: 200%;'><i id='fir_" + id_ + "' onclick=\"check_proveedor('" + id_ + "')\" class='fa fa-fw fa-check-circle firmante'></i></div>");

					Arreglo_arreglosdot.push(InternoArr);
				}

				var table = $('#Proveedores').DataTable();

				table.destroy();

				$('#Proveedores').DataTable({
					data: Arreglo_arreglosdot,
					columns: [
						{ title: "Razon Soc." },
						{ title: "RFC" },
						{ title: "Domicilio Fiscal" },
						{ title: "Email" },
						{ title: "Seleccionar" }

					]
				});
			}
		});
	} else {
		call_Proveedores_catalog();
	}

}