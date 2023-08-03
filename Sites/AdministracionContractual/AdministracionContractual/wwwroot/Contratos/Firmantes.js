

function INIT_Firmantes() {
	call_firmantes_catalog();
	setTimeout(function () {
	}, 1000);
	//setInterval('estutusalive()', 100);
	var firmantes = [];
	$('#firmantes_hd').val(JSON.stringify(firmantes));
}



function check_firmante(value) {
	if ($('#fir_' + value).hasClass('check-fir')) {
		$('#fir_' + value).removeClass('check-fir');
		var firmantes = JSON.parse($('#firmantes_hd').val());
		var index = firmantes.indexOf(value);
		firmantes.splice(index, 1);
		$('#firmantes_hd').val(JSON.stringify(firmantes));
	}
	else {
		var firmantes = JSON.parse($('#firmantes_hd').val());
		firmantes.push(value);
		$('#firmantes_hd').val(JSON.stringify(firmantes));
		$('#fir_' + value).addClass('check-fir');
	}

}

function check_responsable(value) {
	$(".responsable_cl").each(function () {
		$(this).removeClass('check-fir');
	});

	if ($('#res_' + value).hasClass('check-fir')) {
		$('#res_' + value).removeClass('check-fir');
	}
	else {
		$('#res_' + value).addClass('check-fir');
		$('#responsables_hd').val('#res_' + value);
		$(".paginate_button").each(function () {
			$(this).click(function () {
				estutusalive();
			});
		});

	}

}


function estutusalive() {
	if ($('#responsables_hd').val() != '') {
		$(".responsable_cl").each(function () {
			$(this).removeClass('check-fir');
		});
		$($('#responsables_hd').val()).addClass('check-fir');
	}
}


function validate_firmantes__() {
	if ($('#responsables_hd').val() != "" || obj.length <= 0) {

		Swal.fire({
			type: 'success',
			title: 'Datos Validos',
			text: 'Los datos son validos',

		});

		$('#a4').val(1);
		$('.div_proveedores').show();
		$('.div_firmantes').hide();
		INIT_Proveedores();
	}
	else {
		$('#a4').val(0);
		Swal.fire({
			type: 'error',
			title: 'Datos Invalidos',
			text: 'Debe seleccionar firmantes y un responsable',

		})
	}
}




function call_firmantes_catalog() {
	var uri = $('#EndPointAdmon').val() + 'Contratos/ConsultarServPub/' + $('#dependenciaContrato').val();
	var Arreglo_arreglosdot = [];
	$.get(uri, function (data, status) {
		for (var i = 0; i <= data.length - 1; i++) {
			var InternoArr = [];
			var nomcomp = data[i].nombre + ' ' + data[i].ap_paterno + ' ' + data[i].ap_materno;
			var rol_ = data[i].rol;
			var rfc_ = data[i].rfc;
			var id_ = data[i].id;

			if (data[i].tbl_rol_id == '820538fc-37e8-11ea-82d7-00155d1b3502' || data[i].tbl_rol_id == '8da8843f-a5ed-11ea-8a36-00155d1b3502' || data[i].tbl_rol_id == '63585dfe-5f2d-11ea-8324-00155d1b3502' || data[i].tbl_rol_id == 'd7032c28-864b-11ea-b826-00155d1b3502') {
				InternoArr.push(nomcomp);
				InternoArr.push(rfc_);
				InternoArr.push(rol_);
				InternoArr.push("<div style='text-align: center;font-size: 200%;'><i id='fir_" + id_ + "' onclick=\"check_firmante('" + id_ + "')\" class='fa fa-fw fa-check-circle firmante'></i></div>");
				if (data[i].tbl_rol_id == '820538fc-37e8-11ea-82d7-00155d1b3502') {
					InternoArr.push("<div style='text-align: center;font-size: 200%;'><i id='res_" + id_ + "' onclick=\"check_responsable('" + id_ + "')\" class='fa fa-fw fa-check-circle responsable_cl'></i></div>");
				} else {
					InternoArr.push("<div style='text-align: center;font-size: 200%;'></div>");
				}

				Arreglo_arreglosdot.push(InternoArr);

			}





		}

		var table = $('#firmantes_tbl').DataTable();

		table.destroy();

		$('#firmantes_tbl').DataTable({
			data: Arreglo_arreglosdot,
			columns: [
				{ title: "Razon Soc." },
				{ title: "RFC" },
				{ title: "ROL" },
				{ title: "Seleccionar firmante" },
				{ title: "Es responsable" }

			]
		});
	});
}

