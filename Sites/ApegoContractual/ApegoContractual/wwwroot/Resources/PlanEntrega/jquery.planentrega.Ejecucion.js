$.extend($.fn.dataTable.defaults, {
	responsive: true
});

$(document).ready(function () {
	LaunchLoader(true);
	$('#tbl_PlanEntrega').DataTable({
		"language": {
			"url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
		}
	});
	GetPlanesEntrega();

});



function irEjecutar(id) {
	var route = '/PlanDeEntrega/EjecucionListaUbicaciones/' + id;

	window.location.replace(route);
}



function CountIncumplidas(tbl_PlanEntrega_ac_id) {
	$('#action_confirm').unbind("click");
	$('#bodymodal').html('');
	$('#warningincumplidas').modal('show');
	$('#txt_PE').val(tbl_PlanEntrega_ac_id);
	var conteo = 0;
	var cadena = '<p>Este Plan de Entrega cuenta con las siguientes obligaciones incumplidas:</p> </br><ul>';
	var empty = '<p>¿Seguro quieres confirmar el plan de entrega?</p>';
	$.get($("#EndPointAC").val() + "Confirmar/PE/Incumplidas/1/" + tbl_PlanEntrega_ac_id + "/" + '00000000-0000-0000-0000-000000000000', function (data, status) {
		if (data.length <= 0) {
			//$('#txt_value').val('1');
			$('#bodymodal').html(empty);

			$("#action_confirm").click(function () {
				eval_function(tbl_PlanEntrega_ac_id, 1);
			});
			return;
		}
		$('#txt_value').val('0');
		for (var i = 0; i <= data.length - 1; i++) {
			cadena = cadena + '<li>' + data[i].obligacion + '</li>';
		}
		cadena = cadena + '</ul>';
		cadena = cadena + '<p>¿Seguro quieres confirmar el plan de entrega?</p>';
		$('#bodymodal').html(cadena);
		$("#action_confirm").click(function () {
			eval_function(tbl_PlanEntrega_ac_id);
		});

	});


}

function eval_function(id) {

	$.ajaxSetup({
		async: false
	});
	var validacion = validarReporte(id);

	if (validacion) {

		$.post($("#EndPointAC").val() + "Confirmar/PE/" + id, function (data, status) {
			console.log(data);
			if ((data != '' || data != null) && data) {
				SuccessSA("Operación exitosa", "El registro se guardado correctamente")
				GetPlanesEntrega();
			}
			else if ((data != '' || data != null) && !data) {
				ErrorSA('', "Ocurrio un error.")
				GetPlanesEntrega();
			}
		});
		$('#warningincumplidas').modal('hide');
	} else {
		ErrorSA('Error', "Se necesita cargar un reporte");
	}
	$.ajaxSetup({
		async: true
	});

	
}


function validarReporte(idPlanEntrega) {

	var validacion = null;

	$.get($("#EndPointAC").val() + "Operaciones/PE/Get/token/PlanEntrega/" + idPlanEntrega, function (data, status) {
		var Token = data.response;
		for (var i = 0; i <= Token.length - 1; i++) {
			if (Token[i].token != "") {
				validacion = true;
			}
			else {
				validacion = false;
			}
		}
	});
	return validacion;
}



function GetPlanesEntrega() {

    $.get($("#EndPointAC").val() + "Operaciones/PE/Get/lista/tipo/contrato/id/" + $('#txt_hdd_contrato').val() + "/" + $('#HDidUsuario').val(), function (data, status) {
    //$.get("https://localhost:44359/PlanEntrega/Get/lista/tipo/contrato/id/" + $('#txt_hdd_contrato').val() + "/" + $('#HDidUsuario').val(), function (data, status) {
		var nombre = '';
		var Arreglo_arreglos = [];
		for (var i = 0; i <= data.length - 1; i++) {
			var Interno = [];
			var fechaEjecucion = data[i].header.p_fecha_ejecucion.substring(0, data[i].header.p_fecha_ejecucion.indexOf('T'));

			Interno.push(data[i].header.p_identificador);
			Interno.push(data[i].header.p_periodo);
			
			/*Interno.push(data[i].header.p_descripcion);*/
			Interno.push("<div align='center'><button class= 'btn btn-default' title = 'ver descripcion' onclick=\"Mostrar_Plan_Descripcion('" + data[i].header.p_id + "','" + data[i].header.p_descripcion + "');\"><i class='glyphicon glyphicon-comment'></i></button></div>");
			Interno.push(fechaEjecucion);
			Interno.push(data[i].ubicacionesProductos.map(u => u.productos.map(p => p.p_cantidad).reduce((prev, curr) => prev + curr, 0))
													 .reduce((prev, curr) => prev + curr, 0));
			//Interno.push(data[i].header.p_tipo_entrega);
            //a82039db-4161-11ea-9fcf-00155d1b3502 Resp Ubicacion
			//820aa780-37e8-11ea-82d7-00155d1b3502 Ejec PE
			console.log($('#HDidRol').val());
			if ($('#HDidRol').val() == '820aa780-37e8-11ea-82d7-00155d1b3502') {//Ejecutor del PE
				if (data[i].header.p_cumplio_pe == false && data[i].header.p_ejecucion == false) { //No ejcutado
					Interno.push("<button class='btn btn-info' title='Monitorear contrato' onclick=\"irEjecutar('" + data[i].header.p_id + "');\"><i class='fa fa-arrow-circle-right'></i></button>");
						//+ (data[i].token != "" ? "<button onclick=\"DescargarReporte('" + data[i].token + "')\" class='btn btn-link'>Descargar reporte</button>" : "  <button id='btnFile_" + data[i].header.p_id + "' onclick=\"ModalAdjuntarReporte('" + data[i].header.p_id + "')\" class='btn btn-link'><i class='fa fa-file'>&nbsp;&nbsp;Adjuntar </i></button>")
					 //   + "  <button id='_" + data[i].header.p_id + "' onclick=\"CountIncumplidas('" + data[i].header.p_id + "')\" class=' " + data[i].header.p_id + " btn btn-success'> Confirmar</button>");
				} else if (data[i].header.p_cumplio_pe == false && data[i].header.p_ejecucion == true) { //No se cumplió
					Interno.push("<button class='btn btn-info' title='Monitorear contrato' onclick=\"irEjecutar('" + data[i].header.p_id + "');\"><i class='fa fa-arrow-circle-right'></i></button> ");
						//+ (data[i].token != "" ? "<button onclick=\"DescargarReporte('" + data[i].token + "')\" class='btn btn-link'>Descargar reporte</button>" : "  <button id='btnFile_" + data[i].header.p_id + "' onclick=\"ModalAdjuntarReporte('" + data[i].header.p_id + "')\" class='btn btn-link'><i class='fa fa-file'>&nbsp;&nbsp;Adjuntar </i></button>")
						//+" <button id='_" + data[i].header.p_id + "' class=' " + data[i].header.p_id + " btn btn-danger'> Plan de Entrega con incumplimiento</button>");
				} else if (data[i].header.p_cumplio_pe == true && data[i].header.p_ejecucion == true) { //cumplió
					Interno.push("<button class='btn btn-info' title='Monitorear contrato' onclick=\"irEjecutar('" + data[i].header.p_id + "');\"><i class='fa fa-arrow-circle-right'></i></button> ");
					//Interno.push("<button class='btn btn-info' title='Ver detalle'><i class='fa fa-eye'></i></button> ");
                }
            } else {
				if (data[i].header.p_cumplio_pe == false && data[i].header.p_ejecucion == false) { //No ejcutado
					var botonEliminar = "";
					if (data[i].ubicacionesProductos.length > 0) {
						botonEliminar = "  <button id='_" + data[i].header.p_id + "' onclick=\"IrBorrarPosiciones('" + data[i].header.p_id + "')\" class=' " + data[i].header.p_id + " btn btn-danger'> Borrar Productos</button>";
					} else {
						botonEliminar = "  <button id='_" + data[i].header.p_id + "' onclick=\"EliminarPlan('" + data[i].header.p_id + "')\" class=' " + data[i].header.p_id + " btn btn-danger'> Borrar Plan</button>";
					}

					Interno.push(("<button onclick=\"ModalAdjuntarReporteGlobal('" + data[i].header.p_id + "')\" style='height: 34px;' class='btn btn-info btn-lg'><i  class='glyphicon glyphicon-folder-open'></i></button></div>")
						+ "  <button id='_" + data[i].header.p_id + "' onclick=\"CountIncumplidas('" + data[i].header.p_id + "')\" class=' " + data[i].header.p_id + " btn btn-success'> Confirmar</button>"
						+ "  <button id='_" + data[i].header.p_id + "' onclick=\"IrEditarPlan('" + data[i].header.p_id + "')\" class=' " + data[i].header.p_id + " btn btn-primary'> Editar</button>"
						+ "  <button id='_" + data[i].header.p_id + "' onclick=\"EliminarPlan('" + data[i].header.p_id + "')\" class=' " + data[i].header.p_id + " btn btn-danger'> Borrar Plan</button>"
						+ botonEliminar 
					);

					//ORGINAL
					//Interno.push((data[i].token != "" ? "<button onclick=\"DescargarReporte('" + data[i].token + "')\" class='btn btn-link'>Descargar reporte</button> <a onclick=\"DeleteDocument('" + data[i].header.p_id + "')\" class='btn btn-link'>  <em class='fa fa-trash'></em> Eliminar archivo </a>" : "  <button id='btnFile_" + data[i].header.p_id + "' onclick=\"ModalAdjuntarReporte('" + data[i].header.p_id + "')\" class='btn btn-info btn-lg'><i class='glyphicon glyphicon-folder-open'></i></button></div>")
					//	+ "  <button id='_" + data[i].header.p_id + "' onclick=\"CountIncumplidas('" + data[i].header.p_id + "')\" class=' " + data[i].header.p_id + " btn btn-success'> Confirmar</button>"
					//	+ "  <button id='_" + data[i].header.p_id + "' onclick=\"IrEditarPlan('" + data[i].header.p_id + "')\" class=' " + data[i].header.p_id + " btn btn-primary'> Editar</button>"
					//	+ botonEliminar
					//);  

					//DeletePosiciones
				} else if (data[i].header.p_cumplio_pe == false && data[i].header.p_ejecucion == true) { //No se cumplió
					Interno.push(("<button onclick=\"ModalAdjuntarReporteGlobal('" + data[i].header.p_id + "')\" style='height: 34px;' class='btn btn-info btn-lg'><i  class='glyphicon glyphicon-folder-open'></i></button></div>")
						+ "  <button id='_" + data[i].header.p_id + "' onclick=\"CountIncumplidas('" + data[i].header.p_id + "')\" class=' " + data[i].header.p_id + " btn btn-success'> Confirmar</button>"
						+ "  <button id='_" + data[i].header.p_id + "' onclick=\"IrEditarPlan('" + data[i].header.p_id + "')\" class=' " + data[i].header.p_id + " btn btn-primary'> Editar</button>"
						+ "  <button id='_" + data[i].header.p_id + "' onclick=\"EliminarPlan('" + data[i].header.p_id + "')\" class=' " + data[i].header.p_id + " btn btn-danger'> Borrar Plan</button>"
					);
					//Interno.push(
					//	//"<button class='btn btn-info' title='Monitorear contrato' onclick=\"irEjecutar('" + data[i].header.p_id + "');\"><i class='fa fa-arrow-circle-right'></i></button> " +
					//	(data[i].token != "" ? "<button onclick=\"DescargarReporte('" + data[i].token + "')\" class='btn btn-link'>Descargar reporte</button>  <a onclick=\"DeleteDocument('" + data[i].token + "')\" class='btn btn-link'>  <em class='fa fa-trash'></em> Eliminar archivo </a>" : "  <button id='btnFile_" + data[i].header.p_id + "' onclick=\"ModalAdjuntarReporte('" + data[i].header.p_id + "')\" class='btn btn-link'><i class='fa fa-file'>&nbsp;&nbsp;Adjuntar </i></button>")
					//	+ " <button id='_" + data[i].header.p_id + "' class=' " + data[i].header.p_id + " btn btn-danger'> Plan de Entrega con incumplimiento</button>");
				} else if (data[i].header.p_cumplio_pe == true && data[i].header.p_ejecucion == true) {
					Interno.push(("<button onclick=\"ModalAdjuntarReporteGlobal('" + data[i].header.p_id + "')\" style='height: 34px;' class='btn btn-info btn-lg'><i  class='glyphicon glyphicon-folder-open'></i></button></div>")
						+ "  <button id='_" + data[i].header.p_id + "' onclick=\"CountIncumplidas('" + data[i].header.p_id + "')\" class=' " + data[i].header.p_id + " btn btn-success'> Confirmar</button>"
						+ "  <button id='_" + data[i].header.p_id + "' onclick=\"IrEditarPlan('" + data[i].header.p_id + "')\" class=' " + data[i].header.p_id + " btn btn-primary'> Editar</button>"
						+ "  <button id='_" + data[i].header.p_id + "' onclick=\"EliminarPlan('" + data[i].header.p_id + "')\" class=' " + data[i].header.p_id + " btn btn-danger'> Borrar Plan</button>"
					);
				}
            }

			Arreglo_arreglos.push(Interno);
			
		}

		
        var table = $('#tbl_PlanEntrega').DataTable();

		table.destroy();
		console.log("Eddy",Arreglo_arreglos);
        $('#tbl_PlanEntrega').DataTable({
            
			"language": {
				"url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
			},
			data: Arreglo_arreglos,
			columns: [
				{ title: "Identificador" },
				{ title: "Periodo" },
				{ title: "Descripción" },
				{ title: "Ejecución" },
				{ title: "Cantidad total" },
				//{ title: "Tipo" },
				//{ title: "Responsable" },
				{ title: "Acciones" }

			]
		});
		LaunchLoader(false);


	});


}

function GetPlanesEntregaGlobal() {

	$.get($("#EndPointAC").val() + "Operaciones/PE/Get/lista/entrega/id/" + $('#idPlanEntregaFile').val(), function (data, status) {
		//$.get("https://localhost:44359/PlanEntrega/Get/lista/tipo/contrato/id/" + $('#txt_hdd_contrato').val() + "/" + $('#HDidUsuario').val(), function (data, status) {
		var nombre = '';
		var Arreglo_arreglos_file = [];
		for (var i = 0; i <= data.length - 1; i++) {
			var Interno = [];
			Interno.push(data[i].fa);
			Interno.push(("<button onclick=\"DescargarReporte('" + data[i].ft + "')\" class='glyphicon glyphicon-eye-open'></button ><button onclick=\"DeleteDocumentArchivo('" + data[i].ft + "')\" class='glyphicon glyphicon-trash' style='margin: 10px'></button> "))


			Arreglo_arreglos_file.push(Interno);

		}

		//var prueba = Arreglo_arreglos[0][1];
		var table = $('#tbl_PlanEntrega_Global').DataTable();
		table.destroy();
		$('#tbl_PlanEntrega_Global').DataTable({

			"language": {
				"url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
			},
			data: Arreglo_arreglos_file,
			columns: [
				{ title: "Documento" }
				,{ title: "Acciones" }
			],

		});
		LaunchLoader(false);



	});


}

function Mostrar_Plan_Descripcion(id, descripcion) {
	$('.clear_txt_upd').val("");
	$("#id_descripcion").val("");
	$('#Modal_Show_Description').modal({ backdrop: 'static', keyboard: false });
	$('#Modal_Show_Description').modal('show');
	$("#id_descripcion").val(id);
	$("#txt_show_description").val(descripcion);
}

DeleteDocument = (id) => WarningSA("Atención", "Usted está a punto de eliminar el documento cargado ¿Desea continuar?", "Si", "No", () => confirmDeleteDocument(id), "");

confirmDeleteDocument = (id) => $.get($('#EndPointAC').val() + 'Operaciones/PE/DeletedFile/contrato/id/' + id, function (data, status) { GetPlanesEntrega(); });

DeleteDocumentArchivo = (token) => WarningSA("Atención", "Usted está a punto de eliminar el documento cargado ¿Desea continuar?", "Si", "No", () => confirmDeleteDocumentFile(token), "");

confirmDeleteDocumentFile = (token) => $.get($('#EndPointAC').val() + 'Operaciones/PE/DeletedFile/entrada/id/' + token, function (data, status) { GetPlanesEntregaGlobal(); });


function ModalAdjuntarReporte(idPlanEntrega) {
	$('#idPlanEntrega').val(idPlanEntrega);
	$('#FileReporte').val('');
	$('#ModalCargarReportePlan').modal('show');
	$('#lista_doctos').html('');

}

function ModalAdjuntarReporteGlobal(idPlanEntrega) {
	$('#idPlanEntregaFile').val(idPlanEntrega);
	$('#FileReporte1').val('');
	$('#ModalCargarReporteGlobal').modal('show');
	$('#lista_doctos').html('');
	
	GetPlanesEntregaGlobal();
}

function CerrarModalCargarReportePlanGlobal() {
	$('#idPlanEntrega').val('');
	$('#FileReporte').val('');
	$('#ModalCargarReporteGlobal').modal('hide');
	$('#lista_doctos').html('');
}

function CerrarModalCargarReportePlan() {
	$('#idPlanEntrega').val('');
	$('#FileReporte').val('');
	$('#ModalCargarReportePlan').modal('hide');
	$('#lista_doctos').html('');


}

function EliminarPlan(tbl_plan_entrega_id) {
	var modelo =
	{
		tbl_plan_entrega_id: tbl_plan_entrega_id
	};
	$.ajax({
		dataType: 'text',
		cache: false,
		contentType: 'application/json',
		processData: false,
		data: JSON.stringify(modelo),
		type: 'post',
		success: function (data) {
			console.log(data);
			var objresponse = JSON.parse(data);
			if (objresponse.cod == "success") {
				ExitoEliminarProducto(objresponse.msg);
			} else {
				ErrorEliminarProducto(objresponse.msg);
			}
		},
		error: function (XMLHttpRequest, textStatus, errorThrown) {
			console.log(XMLHttpRequest.responseText);
			if (XMLHttpRequest.responseText != null) {
				var errorResponse = JSON.parse(XMLHttpRequest.responseText);
				if (errorResponse.msg != null) {
					ErrorSA('Error', errorResponse.msg);
				} else {
					ErrorSA('Error', "Ocurrio un error al eliminar la ubicación.");
				}

			} else {
				ErrorSA('Error', "Ocurrio un error al eliminar la ubicación.");
			}
		},
		processData: false,
		type: 'POST',
		url: $("#EndPointAC").val() + "Confirmar/PE/EliminarPlan"

	});
}
function ErrorEliminarProducto(mensaje) {
	Swal.fire({
		allowOutsideClick: false,
		type: 'warning',
		title: 'Atención.!',
		text: mensaje,
		showCancelButton: true,
		confirmButtonColor: '#3085d6',
		cancelButtonColor: '#d33',
		confirmButtonText: 'Continuar',
		cancelButtonText: 'Cancelar',
	}).then((result) => {
		if (result.value) {
			// SuccessSA('', 'Picaste si');
		}
		else {
			// SuccessSA('Cancelado', 'Picaste no')
		}
	});
}

function ExitoEliminarProducto(mensaje) {
	Swal.fire({
		allowOutsideClick: false,
		type: 'success',
		title: 'Éxito.!',
		text: mensaje,
		showCancelButton: true,
		confirmButtonColor: '#3085d6',
		cancelButtonColor: '#d33',
		confirmButtonText: 'Continuar',
		cancelButtonText: 'Cancelar',
	}).then((result) => {
		if (result.value) {
			// SuccessSA('', 'Picaste si');
			Recargar();
		}
		else {
			// SuccessSA('Cancelado', 'Picaste no')
			Recargar();
		}
	});
}

function Recargar() {
	location.reload();
}

function tempFile() {
	var idPlanEntregaFile = $('#idPlanEntregaFile').val();
	var entrega = idPlanEntregaFile;
	//contrato = idPlanEntregaFile
	LaunchLoader(true);
	var obj_ArcivoPE = SendDocPE;
	

	//obj_ArcivoPE.tbl_plan_entrega_id_ = idPlanEntregaDetalle;

	var countFiles = $('#FileReporte1').prop('files').length;
	if (countFiles == 0) {
		ErrorSA('Error', "No se ha cargado ningún documento. ");
		LaunchLoader(false);
		return;
	}
	var form_data_file = new FormData();
	var file_ = $('#FileReporte1').prop('files')[0];

	form_data_file.append('file', file_);

	$.ajax({
		url: $("#EndPointFileAC").val() + 'Upload/' + entrega,
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

	var html = htmlOriginal + "<div class='col-lg-8 mb-3' name='nuevo_documento' id='" + obj_ArcivoPE.token_ +"'><div class='col-md-5'><input type='text' class='form-control' id='archivo_" + obj_ArcivoPE.token_ + "' placeholder='Renombrar archivo' /></div>" +
		"<div class='col-md-5'>" + file_.name + " </div>" +
		"<div class='col-md-2'><i class= 'fa fa-eye text-primary'  onclick=\"DescargarReporte('" + obj_ArcivoPE.token_ + "')\" aria-hidden='true'></i> &nbsp; &nbsp;	<i class='fa fa-trash text-danger' onclick=\"BorrarArchivocargadotemp('" + obj_ArcivoPE.token_ + "')\" aria-hidden='true'></i></div></div>"

	$('#lista_doctos').html(html);
	document.getElementById("FileReporte1").value = "";
	GetPlanesEntregaGlobal();

}

function tempFileAnt() {
	//var idPlanEntregaFile = $('#idPlanEntregaFile').val();
	//var entrega = idPlanEntregaFile;
	//contrato = idPlanEntregaFile
	LaunchLoader(true);
	var obj_ArcivoPE = SendDocPE;


	//obj_ArcivoPE.tbl_plan_entrega_id_ = idPlanEntregaDetalle;

	var countFiles = $('#FileReporte').prop('files').length;
	if (countFiles == 0) {
		ErrorSA('Error', "No se ha cargado ningún documento. ");
		LaunchLoader(false);
		return;
	}
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
	document.getElementById("FileReporte").value = "";
	GetPlanesEntrega();

}

function SendFile() {
	LaunchLoader(true);


	//var countFiles = $('#FileReporte').prop('files').length;


	var countFiles = $("[name='nuevo_documento']").length;

	if (countFiles == 0) {
		ErrorSA('Error', "No se ha cargado ningún documento. ");
		LaunchLoader(false);
		return;
	} else if (countFiles > 0) {

		for (var i = 0; i <= countFiles - 1; i++) {

			if ($("[name='nuevo_documento']")[i].id =! "") {
				var obj_ArcivoPE = SendDocPE;
				var idPlanEntregaDetalle = $('#idPlanEntrega').val();

				obj_ArcivoPE.tbl_plan_entrega_id_ = idPlanEntregaDetalle;

				var form_data_file = new FormData();
				var file_ = $('#FileReporte').prop('files')[0];

				//obj_ArcivoPE.token_ = $("[name='nuevo_documento']")[i].id;

				form_data_file.append('file', file_);


				$.ajax({
					dataType: 'text',
					cache: false,
					contentType: 'application/json',
					processData: false,
					data: JSON.stringify(obj_ArcivoPE),
					type: 'post',

					success: function (data) {
						var token = data.replace(/[ '"]+/g, '');
						obj_ArcivoPE.token_ = token;
						var objresponse = JSON.parse(data);
						console.log(objresponse[0].cod)
						if (objresponse[0].cod == "success") {
							function Confirmacion() {
								//return CerrarModalCargarReportePlan();
							}
							var AccionSi = eval(Confirmacion);
							LaunchLoader(false);
							SuccessSAAction("Operación exitosa", "El archivo se guardo correctamente", AccionSi);
							GetPlanesEntrega();
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
				CerrarModalCargarReportePlan();
			}
		

		}

	
		//$.ajax({
		//	url: $("#EndPointFileAC").val() + 'Upload/',
		//	dataType: 'text',
		//	cache: false,
		//	contentType: false,
		//	processData: false,
		//	data: form_data_file,
		//	type: 'POST',
		//	async: false,
		//	success: function (data) {
		//		var token = data.replace(/[ '"]+/g, '');
		//		obj_ArcivoPE.token_ = token;
		//		console.log(token);
		//	},
		//	error: function (data) {
		//		var objresponse = JSON.parse(data);
		//		ErrorSA('', objresponse);
		//	}
		//});
	}

	//console.log(JSON.stringify(obj_ArcivoPE));

	//$.ajax({
	//	dataType: 'text',
	//	cache: false,
	//	contentType: 'application/json',
	//	processData: false,
	//	data: JSON.stringify(obj_ArcivoPE),
	//	type: 'post',

	//	success: function (data) {
	//		var objresponse = JSON.parse(data);
	//		console.log(objresponse[0].cod)
	//		if (objresponse[0].cod == "success") {
	//			function Confirmacion() {
	//				return CerrarModalCargarReportePlan();
	//			}
	//			var AccionSi = eval(Confirmacion);
	//			LaunchLoader(false	);
	//			SuccessSAAction("Operación exitosa", "El archivo se guardo correctamente", AccionSi);
	//			GetPlanesEntrega();
	//		}
	//		else {
	//			ErrorSA("", "Ocurrio un error.");
	//		}
	//	},
	//	error: function () {
	//		ErrorSA('', "Ocurrio un error.")
	//	},
	//	processData: false,
	//	type: 'POST',
	//	url: $("#EndPointAC").val() + "Operaciones/PE/add/Doc/PE"

	//});

	
};

function BorrarArchivocargadotemp(token) {
	//$('#' + token).html('');
	document.getElementById(token).innerHTML = '';
}

function DescargarReporte(item) {
	getURL(item)
	modalVisualizacion();
}

var SendDocPE = {
	id_: "00000000-0000-0000-0000-000000000000",
	tbl_plan_entrega_id_: "00000000-0000-0000-0000-000000000000",
	tbl_Ubicacion_id_: "00000000-0000-0000-0000-000000000000",
	tbl_producto_servicio_id_: "00000000-0000-0000-0000-000000000000",
	token_: ""
};

function IrEditarPlan(idPlan) {
	console.log(idPlan);
	console.log("/PlanDeEntrega/Edit/?id=" + $("#idContrato").val() + "&idPlan=" + idPlan);
	return window.location.href = "/PlanDeEntrega/Edit/?id=" + $("#idContrato").val() + "&idPlan=" + idPlan;
}

function IrBorrarPosiciones(idPlan) {
	console.log(idPlan);
	console.log("/PlanDeEntrega/DeletePosiciones/?id=" + $("#idContrato").val() + "&idPlan=" + idPlan);
	return window.location.href = "/PlanDeEntrega/DeletePosiciones/?id=" + $("#idContrato").val() + "&idPlan=" + idPlan;
}