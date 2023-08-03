$.extend($.fn.dataTable.defaults, {
    responsive: true
});

$(document).ready(function () {
    $('#tbl_PlanMonitoreo').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
    });
    LaunchLoader(true);
    GetPlanesMonitoreoFiles();

});

function ModalUploadFile() {
    $('#ModalCargar_File').modal();
}

function ValidasPlanesEntrega() {
    $.get("/Request/PlanMonitoreo/Confirmados/" + $('#txt_hdd_contrato').val(), function (data, status) {
        for (var i = 0; i <= data.length - 1; i++) {
            $('#conf_' + data[i]).hide();
        }

    });
}


function irEjecutar(id) {
    var route = '/PlanDeMonitoreo/EjecucionListaUbicaciones/' + id;
    window.location.replace(route);
}

//function eval_function(id, eval) {

//	//$.ajaxSetup({
//	//	async: false
//	//});

//	if (validarReporte(id)) {

//		$.post("/Request/Ejecucion/PE/Header/Add/" + id + "/" + eval, function (data, status) {
//			console.log(data);
//			if ((data != '' || data != null) && data) {
//				//alert(true);

//				$('.' + id).removeAttr('onclick');

//				$('.' + id).html('Plan de Entrega sin incumplimiento');



//			}
//			else if ((data != '' || data != null) && !data) {
//				//alert('#btn_ac_' + id);
//				$('.' + id).removeAttr('onclick');
//				$('.' + id).html('Plan de Entrega con incumplimiento');

//				$('.' + id).removeClass('btn-success');
//				$('.' + id).addClass('btn-danger');
//			}
//		});

//		$('#warningincumplidas').modal('hide');

//	} else {

//		ErrorSA('Error', "Se necesita Cargar un reporte");


//	}






//$.ajaxSetup({
//	async: true
//});


//}

function validarReporte(idPlanEntrega) {
    $.get("/Request/PlanEntrega/Reporte/" + idPlanEntrega, function (data, status) {
        if (data) {
            return true;
        }
        else {
            return false;
        }
    });
}

function RedirectDet(Route) {
    window.location.href = "/PlanDeMonitoreo/EjecucionListaUbicaciones/" + Route;
}


function UploadFilesPM(value) {
    $('#ModalInput').val(value);
    $('#ModalCargar_File').modal('show');
}


function GetPlanesMonitoreo() {

    $.get($("#EndPointAC").val() + 'Operaciones/PM/Get/List/' + $('#txt_hdd_contrato').val(), function (data, status) {
        if (data.length > 0) {
            var nombre = '';
            var Arreglo_arreglos = [];
            for (var i = 0; i <= data.length - 1; i++) {

                var Interno = [];
                Interno.push(data[i].identificador);
                Interno.push(data[i].contUbicaciones);
                Interno.push(data[i].pmEstadoDescripcion);

                var dformat = '';

                if (data[i].fecha_ejecucion != null) {
                    var d = new Date(data[i].fecha_ejecucion.replace(
                        /^(\d{4})(\d\d)(\d\d)(\d\d)(\d\d)(\d\d)$/,
                        '$4:$5:$6 $2/$3/$1'
                    ));

                    var cadena = '0';
                    dformat = [(d.getDate()) < 10 ? cadena.concat((d.getDate())) : (d.getDate()),
                    (d.getMonth() + 1) < 10 ? cadena.concat((d.getMonth() + 1)) : (d.getMonth() + 1),
                    d.getFullYear()].join('/') + ' ' +
                        [d.getHours(),
                        d.getMinutes(),
                        d.getSeconds()].join(':');

                }

                Interno.push(data[i].fecha_ejecucion == null ? 'No ejecutado' : dformat);

                if (data[i].ejecutado) {
                    if (data[i].token == null) {
                        Interno.push("<a onclick=\"UploadFilesPM('" + data[i].id + "')\"  class='btn btn-primary'> Cargar archivo </a> ");
                    }
                    else {
                        Interno.push("<a onclick=\"Download1('" + data[i].token + "')\" class='btn btn-primary'> <em class='fa fa-download'></em> Descargar archivo </a> <a onclick=\"DeleteDocument('" + data[i].id + "')\" class='btn btn-danger'>  <em class='fa fa-trash'></em> Eliminar archivo </a> ");
                    }
                }
                else {
                    Interno.push("<button class='btn btn-success' type='button' onclick=\"RedirectDet('" + data[i].id + "')\">Ir a Ubicaciones</button> <button id='conf_" + data[i].Id + "' class='btn btn-info' type='button' onclick=\"ConfirmPMfnc('" + data[i].id + "')\">Confirmar</button>");
                }

                Arreglo_arreglos.push(Interno);
            }

            var table = $('#tbl_PlanMonitoreo').DataTable();

            table.destroy();
            console.log(Arreglo_arreglos);
            $('#tbl_PlanMonitoreo').DataTable({
                "language": {
                    "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
                },
                data: Arreglo_arreglos,
                columns: [
                    { title: "Identificador" },
                    { title: "Cantidad total" },
                    { title: "Descripción" },
                    { title: "Ejecución" },
                    { title: "Acciones" }


                ]
            });
            LaunchLoader(false);

        } else { LaunchLoader(false); }
    });
}

function GetPlanesMonitoreoFiles() {

    $.get($("#EndPointAC").val() + 'Operaciones/PM/Get/List/' + $('#txt_hdd_contrato').val(), function (data, status) {
        if (data.length > 0) {
            var nombre = '';
            var Arreglo_arreglos = [];
            for (var i = 0; i <= data.length - 1; i++) {

                var Interno = [];
                Interno.push(data[i].identificador);
                Interno.push(data[i].contUbicaciones);
                Interno.push(data[i].pmEstadoDescripcion);

                var dformat = '';

                if (data[i].fecha_ejecucion != null) {
                    var d = new Date(data[i].fecha_ejecucion.replace(
                        /^(\d{4})(\d\d)(\d\d)(\d\d)(\d\d)(\d\d)$/,
                        '$4:$5:$6 $2/$3/$1'
                    ));

                    var cadena = '0';
                    dformat = [(d.getDate()) < 10 ? cadena.concat((d.getDate())) : (d.getDate()),
                    (d.getMonth() + 1) < 10 ? cadena.concat((d.getMonth() + 1)) : (d.getMonth() + 1),
                    d.getFullYear()].join('/') + ' ' +
                        [d.getHours(),
                        d.getMinutes(),
                        d.getSeconds()].join(':');

                }

                Interno.push(data[i].fecha_ejecucion == null ? 'No ejecutado' : dformat);

                if (data[i].ejecutado) {       
                        Interno.push("<button onclick=\"ModalAdjuntarFileMonitoreo('" + data[i].id + "')\" style='height: 34px;' class='btn btn-info btn-lg'><i  class='glyphicon glyphicon-folder-open'></i></button></div>");                   
                }
                else {
                    Interno.push("<button class='btn btn-success' type='button' onclick=\"RedirectDet('" + data[i].id + "')\">Ir a Ubicaciones</button> <button id='conf_" + data[i].Id + "' class='btn btn-info' type='button' onclick=\"ConfirmPMfnc('" + data[i].id + "')\">Confirmar</button>");
                }

                Arreglo_arreglos.push(Interno);
            }

            var table = $('#tbl_PlanMonitoreo').DataTable();

            table.destroy();
            console.log(Arreglo_arreglos);
            $('#tbl_PlanMonitoreo').DataTable({
                "language": {
                    "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
                },
                data: Arreglo_arreglos,
                columns: [
                    { title: "Identificador" },
                    { title: "Cantidad total" },
                    { title: "Descripción" },
                    { title: "Ejecución" },
                    { title: "Acciones" }


                ]
            });
            LaunchLoader(false);

        } else { LaunchLoader(false); }
    });
}

function GetPlanesMonitoreoGlobal() {
    var monitoreo = $('#idPlanMonitoreoFile').val();
    $.get($("#EndPointAC").val() + "Operaciones/PM/Get/lista/monitoreo/id/" + monitoreo, function (data, status) {
        
        var nombre = '';
        var Arreglo_arreglos_file = [];
        for (var i = 0; i <= data.length - 1; i++) {
            var Interno = [];
            Interno.push(data[i].fa);
            Interno.push(("<button onclick=\"Download1('" + data[i].ft + "')\" class='glyphicon glyphicon-eye-open'></button ><button onclick=\"DeleteDocument('" + data[i].ft + "')\" class='glyphicon glyphicon-trash' style='margin: 10px'></button> "))

            Arreglo_arreglos_file.push(Interno);

        }

        var table = $('#tbl_PlanMonitoreo_Global').DataTable();
        table.destroy();
        $('#tbl_PlanMonitoreo_Global').DataTable({

            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            data: Arreglo_arreglos_file,
            columns: [
                { title: "Documento" }
                , { title: "Acciones" }
            ],

        });
        LaunchLoader(false);



    });


}

function ModalAdjuntarFileMonitoreo(idPlanMonitoreo) {
    $('#idPlanMonitoreoFile').val(idPlanMonitoreo);
    $('#FileReporte1').val('');
    $('#ModalCargarFileMonitoreo').modal('show');
    $('#lista_doctos').html('');

    GetPlanesMonitoreoGlobal(idPlanMonitoreo);
}


function Download1(item) {
    getURL(item)
    modalVisualizacion();
    
}

DeleteDocument = (token) => WarningSA("Atención", "Usted está a punto de eliminar el documento cargado ¿Desea continuar?", "Si", "No", () => confirmDeleteDocument(token), "");

confirmDeleteDocument = (token) => $.get($('#EndPointFileAC').val() + 'DeleteUrl/monitoreo/id/' + token, function (data, status) { GetPlanesMonitoreoGlobal(); });

function cerrarModal() {
    $('#ModalCargar_File').modal('hide');
}

//Carga el archivo para el plan de monitoreo
$('.CargarArchivo').click(function () {

    var obj_Doc_PM = SendDocPM;
    var countFiles = $("#file_PM_gral").prop('files').length;

    obj_Doc_PM.tbl_plan_moniotoreo_id_ = $('#ModalInput').val();
    if (countFiles == 0) {
        ErrorSA('Error', "No se ha cargado ningún documento. ");
        return;
    } else if (countFiles > 0) {

        var form_data = new FormData();
        var file_data = $('#file_PM_gral').prop('files')[0];
        form_data.append('file', file_data);

        $.ajax({
            url: $("#EndPointFileAC").val() + 'Upload/',
            dataType: 'text',
            cache: false,
            contentType: false,
            processData: false,
            data: form_data,
            type: 'POST',
            async: false,
            success: function (data) {
                var token = data.replace(/[ '"]+/g, '');
                obj_Doc_PM.token_ = token;
                console.log(token);
            },
            error: function (data) {
                var objresponse = JSON.parse(data);
                ErrorSA('', objresponse);
            }
        });

    }

    console.log(JSON.stringify(obj_Doc_PM));

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(obj_Doc_PM),
        type: 'post',

        success: function (data) {
            var objresponse = JSON.parse(data);
            console.log(objresponse[0].cod)
            if (objresponse[0].cod == "success") {
                function Confirmacion() {
                    return cerrarModal();
                }
                var AccionSi = eval(Confirmacion);
                SuccessSAAction("Operación exitosa", "El archivo se guardo correctamente", AccionSi);
                GetPlanesMonitoreo();
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
        url: $("#EndPointAC").val() + 'Operaciones/PM/add/Doc/PM'

    });
})

function CerrarModalCargarReportePlanGlobal() {
    $('#idPlanMonitoreoFile').val('');
    $('#FileReporte').val('');
    $('#ModalCargarFileMonitoreo').modal('hide');
    $('#lista_doctos').html('');
}

var SendDocPM = {
    id_: "00000000-0000-0000-0000-000000000000",
    tbl_plan_moniotoreo_id_: "00000000-0000-0000-0000-000000000000",
    tbl_Ubicacion_id_: "00000000-0000-0000-0000-000000000000",
    tbl_obligacion_id_: "00000000-0000-0000-0000-000000000000",
    token_: ""
};
//function ModalAdjuntarReporte(idPlanEntrega) {
//	$('#idPlanEntrega').val(idPlanEntrega);
//	$('#FileReporte').val('');
//	$('#ModalCargarReportePlan').modal('show');
//}

//function CerrarModalCargarReportePlan() {
//	$('#idPlanEntrega').val('');
//	$('#FileReporte').val('');
//	$('#ModalCargarReportePlan').modal('hide');

//}


//function SendFile() {

//	var countFiles = $('#FileReporte').prop('files').length;

//	if (countFiles == 0) {
//		ErrorSA('Error', "No se han cargado documentos");
//		return;
//	} else if (countFiles > 0) {

//		var form_data = new FormData();
//		var file_data = $('#FileReporte').prop('files')[0];

//		var idPlanEntregaDetalle = $('#idPlanEntrega').val();

//		form_data.append('IdPlanEntregaDetalle', idPlanEntregaDetalle);
//		form_data.append('ReportePE_File', file_data);

//		$.ajax({

//			dataType: 'json',
//			cache: false,
//			contentType: false,
//			processData: false,
//			data: form_data,
//			type: 'post',

//			success: function (data) {
//				if (data) {

//					function Confirmacion() {
//						return CerrarModalCargarReportePlan();
//					}
//					var AccionSi = eval(Confirmacion);

//					SuccessSAAction("Operación exitosa", "El archivo se guardo correctamente", AccionSi);

//					GetPlanesEntrega();


//				}
//			},
//			error: function (data) {
//				alert(data);
//			},
//			processData: false,
//			type: 'POST',
//			url: '/Request/ReportePlanEntrega/add'
//		});


//	}
//};


//function DescargarReporte(tokenDocto) {

//	$.get("/Request/PlanEntrega/Reporte/" + tokenDocto, function (data, status) {

//		window.open(data, '_blank');

//	});

//}

function tempFileMon() {
    var Monitoreo = $('#idPlanMonitoreoFile').val();
    var entrega = "00000000-0000-0000-0000-000000000000";
    var ubicacion = "00000000-0000-0000-0000-000000000000";
    LaunchLoader(true);
    var obj_Doc_PM = SendDocPM;;

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
        url: $("#EndPointFileAC").val() + 'Upload/' + entrega + '/' + ubicacion + '/' + Monitoreo,
        dataType: 'text',
        cache: false,
        contentType: false,
        processData: false,
        data: form_data_file,
        type: 'POST',
        async: false,
        success: function (data) {
            var token = data.replace(/[ '"]+/g, '');
            obj_Doc_PM.token_ = token;
            console.log(token);
            LaunchLoader(false);

        },
        error: function (data) {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse);
            LaunchLoader(false);
        }
    });

    console.log(JSON.stringify(obj_Doc_PM));

    document.getElementById("FileReporte1").value = "";
    GetPlanesMonitoreoGlobal();

}

















