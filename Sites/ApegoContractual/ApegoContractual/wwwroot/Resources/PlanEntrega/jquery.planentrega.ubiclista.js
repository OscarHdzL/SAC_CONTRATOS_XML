$.extend($.fn.dataTable.defaults, {
	responsive: true
});
function Redimension() {
	try {
		var tables = document.getElementsByTagName('table');
		for (var i = 0; i < tables.length; i++) {
			$('#' + tables[i].id + '').resize();
		}
	}
	catch (err) { }
}
$(document).ready(function () {
	LaunchLoader(true);
	$('#tbl_PlanEntrega_ubicaciones').DataTable({
		"language": {
			"url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
		},
		"columnDefs": [
			{ "className": "dt-center", "targets": "_all" },
			{ "width": "100%", "targets": 2 },
		]
    });
	Get_ListaUbicaciones_PE();
	setInterval('Redimension()', 500);
});



function openmodal(IdUbicPE, IdPE) {
	$('.listboos').html('');
	$('.listobligacion').html('');
	$("#btnGuardar").addClass('hidden');

	$('#tbl_ubicaciones_ac_id').val(IdUbicPE);

	$('#ModalProductos').modal('show');

	$.get($("#EndPointAC").val() + "ProdServ/Productos/PE/" + IdPE + "/Ubicacion/" + IdUbicPE, function (data, status) {
		$('.listboos').html('');
		var html_SS = '';
		
		for (var i = 0; i <= data.length - 1; i++) {
			var body = "<li Descripcion_" + data[i].id + "=\"" + data[i].elemento + "\" onclick=\"cumplio(0,'" + IdPE + "','" + data[i].id + "','" + data[i].tbl_plan_entrega_producto_id+"')\" onmouseover=\"GreenContainer('" + data[i].id + "')\" onmouseout=\"NormalContainer('" + data[i].id + "')\" id='contenedor_" + data[i].id + "' class='ctmain list-group-item d-flex justify-content-between align-items-center'>"
				+ data[i].elemento + 
				"<span id='spanon_" + data[i].id + "' style='cursor:pointer;display:none;' onclick = \"cumplio(1,'" + IdPE + "','" + data[i].id + "')\"  class='badge badge-primary badge-pill'> Cumplió</span>" +
				"<span id='spanoff_" + data[i].id + "' style='cursor:pointer;display:none;' onclick=\"cumplio(0,'" + IdPE + "'," + data[i].id + ")\" class='badge badge-primary badge-pill'>No Cumplió</span>" +
				"</li >";
			html_SS = html_SS + body;
		}
		$('.listboos').html(html_SS);
	});
	$('#tbl_ubicaciones_ac_id').val(IdUbicPE);

	//setTimeout(function () {
	//$.get("/Request/PlanEntrega/Ejecucion/Productos/Get/NoCumple/" + Guid, function (data, status) {
	//	for (var i = 0; i <= data.length - 1; i++) {
	//		$('#spanoff_' + data[i]).css('background-color', 'red');
	//		//$('#spanon_' + data[i]).css('background-color', '#777');
	//	}
	//});
	//}, 500);
 

}





function NormalContainer(id) {
	$('#contenedor_' + id).removeClass('list-group-item-success');
 
}
function GreenContainer(id) {
	$('#contenedor_' + id).addClass('list-group-item-success');
}

function cumplio(estatus, id, prod, plan_entrega_producto_id) {

		LaunchLoader(true);


	$('#tbl_ProdServ_ac_id').val(prod);

	$(".ctmain").each(function () {
		$(this).removeClass('list-group-item-success');
	});
	$('#contenedor_' + prod).addClass("ctmain list-group-item-success");

		//ctmain
	if (estatus == '1') {
		$('#spanoff_' + prod).css('background-color', '#777');
		$('#spanon_' + prod).css('background-color', 'green');

		
 
	}
	else {
		$('#spanon_' + prod).css('background-color', '#777');
		$('#spanoff_' + prod).css('background-color', 'red');
		noCumplio(prod, id, plan_entrega_producto_id);
		$('#tbl_PlanEntrega_ac_id').val(id);
	 
	}
	var contenido_ = $('#contenedor_' + prod).attr('Descripcion_' + prod);
	$('.tituloModal').html("<label style='color: white;font-weight: 900;'>Producto: &nbsp;&nbsp;</label>" + contenido_);



}
function execute(obligacion,link_obligacion, plan_entrega_producto_id) {

	var bit = 0;




	$('#tbl_Obligaciones_ac_id').val(obligacion);
	if ($('#bag_c_' + obligacion).hasClass("push-Off") && !$('#bag_c_' + obligacion).hasClass("push-NotOk")) {
		$('#bag_c_' + obligacion).addClass("push-NotOk");
		bit = 0;
	}
	else {
		$('#bag_c_' + obligacion).removeClass("push-NotOk");
		$('#bag_c_' + obligacion).addClass("push-Off");
		bit = 1;
	}

	var idObligacion = $('#tbl_Obligaciones_ac_id').val();
	var idproducto = $('#tbl_ProdServ_ac_id').val();
	var idplan = $('#tbl_PlanEntrega_ac_id').val();


	$.post($("#EndPointAC").val() + "Plan/PE/" + link_obligacion + "/" + plan_entrega_producto_id, function (data, status) {

	});

}

function execute_btn_save() {
	

	var select_numbers = document.getElementsByName('select').length;
	var _link_obligacion;
	var _plan_entrega_producto_id;
	var _option;
	var _conact_link_option;

	for (var i = 0; i <= select_numbers - 1; i++) {
		_link_obligacion = (document.getElementsByName('select')[i].id).split('|')[1];
		_plan_entrega_producto_id = (document.getElementsByName('select')[i].id).split('|')[2];
		_option = document.getElementsByName('select')[i].value;

		$.post($("#EndPointAC").val() + "Plan/PE/" + _option + "/" + _link_obligacion + "/" + _plan_entrega_producto_id, function (data, status) {

			if ((data != null) && (data != '')) {
				console.log(data);
			}

		});

		function Confirmacion() {
			return true;
		}
		var AccionSi = eval(Confirmacion);
		//LaunchLoader(true);
		SuccessSAAction("Operación exitosa", "Se actualizó correctamente", AccionSi);


		//setTimeout(function () {
		//	$.post($("#EndPointAC").val() + "Plan/PE/" + _option + "/" + _link_obligacion + "/" + _plan_entrega_producto_id, function (data, status) {

		//		if ((data != null) && (data != '')) {
		//			console.log(data);
		//		}

		//	});
		//}, 500);
	}


	//var bit = 0;
	////$('#tbl_Obligaciones_ac_id').val(obligacion);
	//if ($('#bag_c_' + obligacion).hasClass("push-Off") && !$('#bag_c_' + obligacion).hasClass("push-NotOk")) {
	//	$('#bag_c_' + obligacion).addClass("push-NotOk");
	//	bit = 0;
	//}
	//else {
	//	$('#bag_c_' + obligacion).removeClass("push-NotOk");
	//	$('#bag_c_' + obligacion).addClass("push-Off");
	//	bit = 1;
	//}

	//var idObligacion = $('#tbl_Obligaciones_ac_id').val();
	//var idproducto = $('#tbl_ProdServ_ac_id').val();
	//var idplan = $('#tbl_PlanEntrega_ac_id').val();


	//$.post($("#EndPointAC").val() + "Plan/PE/" + link_obligacion + "/" + plan_entrega_producto_id, function (data, status) {

	//});

}

function noCumplio(tbl_prodServ_ac_id, id, plan_entrega_producto_id) {
	$("#btnGuardar").removeClass('hidden');
	var ejecutado;

	var planEntrega = $('#tbl_PlanEntregaDetalle_ac_id').val();

	$.get($("#EndPointAC").val() + "Operaciones/PE/Get/lista/tipo/contrato/id/" + $('#idContrato').val() + "/" + $('#HDidUsuario').val(), function (data, status) {
		if (data[0].header.p_ejecucion == true) {
			$("#btnGuardar").addClass('hidden');
			ejecutado = true;
		}
	});


	setTimeout(function () {
		$.get($("#EndPointAC").val() + "Operaciones/PE/Get/lista/Obligaciones/PlanEntrega/" + id + '/Producto/' + tbl_prodServ_ac_id, function (data, status) {

			var htmlbody = '';
			var Lista = data.response;
			var disabled = '';
			console.log(Lista);
			if (ejecutado == true) {
				disabled = 'disabled';
			}
			for (var i = 0; i <= Lista.length - 1; i++) {
				var prioridad = Lista[i].tbl_tipo_prioridad_nombre == null ? 'Sin prioridad' : Lista[i].tbl_tipo_prioridad_nombre;
				var body_int = "<li id='oblicont_" + Lista[i].id + "' onmouseover=\"GreenContainer_obli('" + Lista[i].id + "')\" onmouseout=\"NormalContainer_obli('" + Lista[i].id + "')\" class='ctmain list-group-item d-flex justify-content-between align-items-center' style='padding-bottom: 25px;'>"
					+ prioridad + ' <br>  ' + Lista[i].obligacion +
					"<select name='select' id='" + Lista[i].id + "|" + Lista[i].tbl_link_obligaciones_id + "|" + plan_entrega_producto_id + "' class='push-Off badge badge-primary badge-pill' " + disabled +">" +
					"<option value = '1'>Si Cumplió</option>" +
					"<option value = '2' selected>No cumplió</option>" +
					"</select>" +
					"</li>";
				htmlbody = htmlbody + body_int;

			}
			$('.listobligacion').html(htmlbody);

		});
		LaunchLoader(false);
	}, 500);


	setTimeout(function () {
		$.get($("#EndPointAC").val() + "Confirmar/PE/Incumplidas/2/" + id + "/" + plan_entrega_producto_id, function (data, status) {
			if (data.length > 0) {
				for (var i = 0; i <= data.length - 1; i++) {
					document.getElementById(data[i].id + "|" + data[i].tbl_link_obligaciones_id + "|" + plan_entrega_producto_id).value = "1";
				}

			}

		});
		LaunchLoader(false);
	}, 500);

	

}

function NormalContainer_obli(id) {
	$('#oblicont_' + id).removeClass('list-group-item-success');

}
function GreenContainer_obli(id) {
	$('#oblicont_' + id).addClass('list-group-item-success');
}






function AddHeader() {
	var route = "/Request/Ejecucion/PE/Header/Add/";
	$.post(route + $('#txt_ubicaciones_pe').val(), function (data, status) {
		if ((data != null) && (data != '')) {
			$('#txt_header').val(data);
			$('#btnMain').remove();
		}
	});
}



function AddDetail() {
	var route = "/Request/Ejecucion/PE/Header/Add/";
	$.post(route + $('#txt_ubicaciones_pe').val(), function (data, status) {
		if ((data != null) && (data != '')) {
			$('#txt_header').val(data);
			$('#btnMain').remove();
		}
	});
}



function ModalCargarReporteUbicacion(idUbicacion,idPlanEntregaDetalle) {
	$('#idPlanEntregaDetalle').val(idPlanEntregaDetalle);
	$('#idUbicacion').val(idUbicacion);
	$('#FileReporte').val('');
	$('#ModalCargarReporteUbicacion').modal('show');
}

function CerrarModalCargarReporteUbicacion() {
	$('#idPlanEntregaDetalle').val('');
	$('#idUbicacion').val('');
	$('#FileReporte').val('');
	$('#ModalCargarReporteUbicacion').modal('hide');
	//location.reload();
}


function SendFileUbicacion() {
	LaunchLoader(true);
	var obj_Arcivo_Ubi_PE = SendDocPE;
	var idPlanEntregaDetalle = $('#idPlanEntregaDetalle').val();

	obj_Arcivo_Ubi_PE.tbl_plan_entrega_id_ = idPlanEntregaDetalle;
	obj_Arcivo_Ubi_PE.tbl_Ubicacion_id_ = $('#idUbicacion').val();

	var countFiles = $('#FileReporte').prop('files').length;
	if (countFiles == 0) {
		ErrorSA('Error', "No se ha cargado ningún documento. ");
		return;
	} else if (countFiles > 0) {

		var form_data_file = new FormData();
		var file_ = $('#FileReporte').prop('files')[0];

		form_data_file.append('file', file_);


		$.ajax({
			url: $("#EndPointFileAC").val() + 'Upload/',
			dataType: 'text',
			cache: false,
			contentType: false,
			processData: false,
			data: form_data_file,
			type: 'POST',
			async: false,
			success: function (data) {
				var token = data.replace(/[ '"]+/g, '');
				obj_Arcivo_Ubi_PE.token_ = token;
				console.log(token);
			},
			error: function (data) {
				var objresponse = JSON.parse(data);
				ErrorSA('', objresponse);
			}
		});
	}

	console.log(JSON.stringify(obj_Arcivo_Ubi_PE));

	$.ajax({
		dataType: 'text',
		cache: false,
		contentType: 'application/json',
		processData: false,
		data: JSON.stringify(obj_Arcivo_Ubi_PE),
		type: 'post',

		success: function (data) {
			var objresponse = JSON.parse(data);
			console.log(objresponse[0].cod)
			if (objresponse[0].cod == "success") {
				function Confirmacion() {
					return CerrarModalCargarReporteUbicacion();
				}
				var AccionSi = eval(Confirmacion);
				LaunchLoader(true);
				SuccessSAAction("Operación exitosa", "El archivo se guardo correctamente", AccionSi);
				Get_ListaUbicaciones_PE();
			}
			else {
				ErrorSA("", "Ocurrio un error.");
			}
		},
		error: function () {
			ErrorSA('', "Ocurrio un error.")
		},
		processData: false,
		type: 'POST',
		url: $("#EndPointAC").val() + "Operaciones/PE/add/Doc/PE"

	});
};

function Get_ListaUbicaciones_PE() {
	var idPE = $("#txt_ubicaciones_pe").val();
	$.get($("#EndPointAC").val() + "UbicacionesCatalog/Get/lista/Ubiacaiones/PE/" + idPE + "/" + $('#HDidUsuario').val(), function (data, status){
		console.log(data);
		var Arreglo_arreglos = [];
		for (var i = 0; i <= data.length - 1; i++) {

			var ListaUb_PE = [];

			ListaUb_PE.push("<a style='cursor: pointer' onclick=\"openmodal('" + data[i].ubicacion.tbl_ubicacion_id + "','" + idPE + "')\">" + data[i].ubicacion.tbl_ubicacion_unidad) + "</a>";
			ListaUb_PE.push(data[i].ubicacion.tbl_ubicacion_clave);
			//ListaUb_PE.push("<textarea style='width:100% !important;border-style: none;background-color: transparent;width: 1148px;margin: 0px;height: 46px' disabled >" + data[i].ubicacion.tbl_ubicacion_direccion + "</textarea>");
			ListaUb_PE.push(data[i].ubicacion.tbl_ubicacion_direccion);
			ListaUb_PE.push(data[i].ubicacion.tbl_contrato_servidor_resp_str);
			ListaUb_PE.push(data[i].ubicacion.tbl_ubicacion_telefono);
			ListaUb_PE.push("<div align='center'><button class= 'btn btn-default' title = 'ver detalles' onclick=\"Mostrar_Detalle_Actividades('" + data[i].id + "','" + data[i].ubicacion.tbl_ubicacion_detalle_actividad + "');\"><i class='glyphicon glyphicon-comment'></i></button></div>");

			ListaUb_PE.push(("<button onclick=\"ModalAdjuntarReporteUbiGlobal('" + data[i].ubicacion.tbl_ubicacion_id + "','" + data[i].ubicacion.tbl_ubicacion_clave + "')\" style='height: 34px;' class='btn btn-info btn-lg'><i class='glyphicon glyphicon-folder-open'></i></button></div>")
				//+ "  <button id='_" + data[i].header.p_id + "' onclick=\"CountIncumplidas('" + data[i].header.p_id + "')\" class=' " + data[i].header.p_id + " btn btn-success'> Confirmar</button>"
				//+ "  <button id='_" + data[i].header.p_id + "' onclick=\"IrEditarPlan('" + data[i].header.p_id + "')\" class=' " + data[i].header.p_id + " btn btn-primary'> Editar</button>"
				//+ botonEliminar
			);
			
			Arreglo_arreglos.push(ListaUb_PE);
		}

		var table = $('#tbl_PlanEntrega_ubicaciones').DataTable();

		table.destroy();
		console.log(Arreglo_arreglos);
		$('#tbl_PlanEntrega_ubicaciones').DataTable({

			"language": {
				"url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
			},
			data: Arreglo_arreglos,
			columns: [
				{ title: "Unidad" },
				{ title: "Clave" },
				{ title: "Dirección" },
				{ title: "Responsable" },
				{ title: "Teléfono" },
				{ title: "Ver" },
				{ title: "Archivo" }
				

			]
		});
		LaunchLoader(false);
	});

}

function Get_ListaUbicaciones_PE_Files(idPlanUbicacionEntrega, claveubi) {
	var clave = claveubi;
	var idUB = idPlanUbicacionEntrega;
	localStorage.setItem('key', clave);
	$.get($("#EndPointAC").val() + "UbicacionesCatalog/Get/lista/Ubicaciones/PE/" + idUB + "/" + clave , function (data, status) {
		console.log(data);
		var Arreglo_arreglos = [];
		for (var i = 0; i <= data.length - 1; i++) {

			var ListaUb_PE = [];
		
			ListaUb_PE.push(data[i].fa);
			ListaUb_PE.push(("<button onclick=\"DescargarReporte_ubicacion('" + data[i].ft + "')\" class='glyphicon glyphicon-eye-open'></button ><button onclick=\"alertDeteleReporteUbi('" + data[i].ft + "')\" class='glyphicon glyphicon-trash' style='margin: 10px'></button> "))
			Arreglo_arreglos.push(ListaUb_PE);
		}

		var table = $('#tbl_PlanEntrega_Global_Ubi').DataTable();

		table.destroy();
		//console.log(Arreglo_arreglos);
		$('#tbl_PlanEntrega_Global_Ubi').DataTable({

			"language": {
				"url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
			},
			data: Arreglo_arreglos,
			columns: [
				{ title: "Documento" },
				{ title: "Acciones" }
			]
		});
		LaunchLoader(false);
	});

}

function ModalAdjuntarReporteUbiGlobal(idPlanUbicacionEntrega, claveubi) {
	$('#idPlanEntregaUbiFile').val(idPlanUbicacionEntrega);
	$('#idUbicacion').val(idUbicacion);
	$('#FileReporte1').val('');
	$('#ModalCargarReporteUbiGlobal').modal('show');
	$('#lista_doctos').html('');

	Get_ListaUbicaciones_PE_Files(idPlanUbicacionEntrega, claveubi);
}



function CerrarModalCargarReportePlanGlobal() {
	$('#idPlanEntrega').val('');
	$('#FileReporte').val('');
	$('#ModalCargarReporteUbiGlobal').modal('hide');
	$('#lista_doctos').html('');
}
function Mostrar_Detalle_Actividades(id, detalles) {
	$('.clear_txt_upd').val("");
	$("#id_detalles").val("");
	$('#Modal_Show_Details').modal({ backdrop: 'static', keyboard: false });
	$('#Modal_Show_Details').modal('show');
	$("#id_detalles").val(id);
	$("#txt_show_details").val(detalles);
}

function DescargarReporte_ubicacion(item) {
	getURL(item)
	modalVisualizacion();
}

alertDeteleReporteUbicacion = (id) =>
	WarningSA("Atención", "Usted está a punto de eliminar el report cargado ¿Desea continuar?", "Si", "No", () => deleteReporteUbicacion(id), "");

deleteReporteUbicacion = (id) => {
	const idPE = $("#txt_ubicaciones_pe").val();
	
	$.get($("#EndPointAC").val() + "UbicacionesCatalog/Delete/ReporteUbicacion/PE/" + idPE + "/" + id, function (data, status) {
		Get_ListaUbicaciones_PE(idPlanUbicacionEntrega);
	});
}

alertDeteleReporteUbi = (token) =>
	WarningSA("Atención", "Usted está a punto de eliminar el documento cargado ¿Desea continuar?", "Si", "No", () => deleteReporteUbi(token), "");

deleteReporteUbi = (token) => {
	var idPlanUbicacion = $('#idPlanEntregaUbiFile').val();
	var claveubi = localStorage.getItem('key');
	//const idPE = $("#txt_ubicaciones_pe").val();
	$.get($("#EndPointAC").val() + "UbicacionesCatalog/Deleted/FileUbicacion/id/" + token, function (data, status) {
		Get_ListaUbicaciones_PE_Files(idPlanUbicacion, claveubi);
	});
}

var ReporteClass = {
	id: '00000000-0000-0000-0000-000000000000',
	tbl_PlanEntregaDetalle_ac_id: '00000000-0000-0000-0000-000000000000',
	tbl_ubicaciones_ac_id: '00000000-0000-0000-0000-000000000000',
	tokenArchivo: '00000000-0000-0000-0000-000000000000',
	RepPE: true,
	RepPM: false,
	Estatus: true,
	Inclusion: '2000-01-01'
}

var SendDocPE = {
	id_: "00000000-0000-0000-0000-000000000000",
	tbl_plan_entrega_id_: "00000000-0000-0000-0000-000000000000",
	tbl_Ubicacion_id_: "00000000-0000-0000-0000-000000000000",
	tbl_producto_servicio_id_: "00000000-0000-0000-0000-000000000000",
	token_: ""
};

function tempFileUbi() {

	var entrega = "00000000-0000-0000-0000-000000000000";
	var idPlanUbicacion = $('#idPlanEntregaUbiFile').val();

	LaunchLoader(true);
	var obj_ArcivoPE = SendDocPE;


	var countFiles = $('#FileReporte2').prop('files').length;
	if (countFiles == 0) {
		ErrorSA('Error', "No se ha cargado ningún documento. ");
		LaunchLoader(false);
		return;
	}
	var form_data_file = new FormData();
	var file_ = $('#FileReporte2').prop('files')[0];

	form_data_file.append('file', file_);

	$.ajax({
		url: $("#EndPointFileAC").val() + 'Upload/'+entrega+'/'+idPlanUbicacion,
		dataType: 'text',
		cache: false,
		contentType: false,
		processData: false,
		data: form_data_file,
		type: 'POST',
		async: false,
		success: function (data) {
			var token = data.replace(/[ '"]+/g, '');
			obj_ArcivoPE.token_ = token;
			console.log(token);
			LaunchLoader(false);

		},
		error: function (data) {
			var objresponse = JSON.parse(data);
			ErrorSA('', objresponse);
			LaunchLoader(false);
		}
	});

	console.log(JSON.stringify(obj_ArcivoPE));

	var htmlOriginal = $('#lista_doctos').html();

	var html = htmlOriginal + "<div class='col-lg-8 mb-3' name='nuevo_documento' id='" + obj_ArcivoPE.token_ + "'><div class='col-md-5'><input type='text' class='form-control' id='archivo_" + obj_ArcivoPE.token_ + "' placeholder='Renombrar archivo' /></div>" +
		"<div class='col-md-5'>" + file_.name + " </div>" +
	"<div class='col-md-2'><i class= 'fa fa-eye text-primary'  onclick=\"DescargarReporte('" + obj_ArcivoPE.token_ + "')\" aria-hidden='true'></i> &nbsp; &nbsp;	<i class='fa fa-trash text-danger' onclick=\"BorrarArchivocargadotemp('" + obj_ArcivoPE.token_ + "')\" aria-hidden='true'></i></div></div>"

	$('#lista_doctos').html(html);
	document.getElementById("FileReporte2").value = "";
	var claveubi = localStorage.getItem('key');
	Get_ListaUbicaciones_PE_Files(idPlanUbicacion, claveubi);

}
