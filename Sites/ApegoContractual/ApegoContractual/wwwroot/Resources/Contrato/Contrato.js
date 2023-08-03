var MILISEGUNDOS_DIA = 86400000;
var RolUsuario = null;
//$(function () {
//    obtenerListado();
//});

$(document).ready(function () {

    LaunchLoader(true);
    $('#tbl_contrato').DataTable({

        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" },
            //{ "width": "10%", "targets": 0 },
            //{ "width": "30%", "targets": 5 },
            //{ "width": "10%", "targets": 6 },
        ],
    })
    //RolUsuario = $('#HDRol').val();
    RolUsuario = 19;
    if (RolUsuario == 20) {
        $('#btnCargaMasiva').prop('disabled', false);
    }
    else {
        $('#btnCargaMasiva').remove();
    }


    obtenerListado("1");

});



$("#btnCargaMasiva").click(function () {
    $("#modalCargaMasiva").modal("show");
});

$("#archivo").change(function () {
    //Guardar temporalmente los datos del archivo
    //accept = ".csv, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel"
});

$("#btnRealizarCargaMasiva").click(function () {
    //Validar formato de archivo cargado

    if ($("#archivo").val() == "") {
        return ErrorSA('Error', 'No se ha cargado archivo');
    }
    var formData = new FormData();

    var totalFiles = document.getElementById("archivo").files.length;

    for (var i = 0; i < totalFiles; i++) {
        var archivo = document.getElementById("archivo").files[i];
        //var archivo = $("#modalCargaMasiva file").files[0];
        console.log(archivo)
        formData.append("archivo", archivo);
    }
    console.log(formData);

    $.ajax({
        dataType: 'json',
        contentType: false,
        processData: false,
        data: formData,
        type: 'post',
        url: 'Request/Contrato/RealizarCargaMasiva/' + $("#hdnIdInstancia").val() + '/' + $("#hdnIdUsuarioAlta").val(),
        success: function (data) {

            if (data.Bit) {

                SuccessSA();

                $("#modalCargaMasiva").modal("hide");

                window.location.reload();
            }
            else {
                ErrorSA('', data.Excepcion);
            }
        },
        error: function (data) {

            if (!data.Bit) {
                Swal.fire({
                    type: 'error',
                    title: 'Error al realizar la operación',
                    text: data.Excepcion
                });
            };
        }
    });

});

function obtenerListado(estatus) {
    var _localEstatus = localStorage.getItem('estatusContrato');
    if (_localEstatus != null) {
        estatus = _localEstatus;
    }

    var idDependencia = $('#HDidDependencia').val();
    //mostrar loader

    $.get($('#EndPointAC').val() + "SerContrato/Get/ListadoContratos/" + idDependencia, function (data, status) {
        console.log(data)
        var listado = [];

        for (var i = 0; i <= data.length - 1; i++) {
            var fila = [];

            var fechaInicio = data[i].fecha_Iinicio.substring(0, data[i].fecha_Iinicio.indexOf('T'));
            var fechaFin = data[i].fecha_fin.substring(0, data[i].fecha_fin.indexOf('T'));
            var diasRemanente = (new Date(fechaFin) - new Date(fechaInicio)) / MILISEGUNDOS_DIA;

            fila.push(data[i].numero);
            fila.push(data[i].nombre == null ? "No Asignada" : data[i].nombre);
            fila.push(fechaInicio);
            fila.push(fechaFin);
            //fila.push(diasRemanente);

            fila.push(data[i].tipo_contrato == null ? "No asignada" : data[i].tipo_contrato)

            if (RolUsuario == 19) { //RespApego                
                fila.push("<div align='center'><button class='btn btn-primary' title='Ver expediente' onclick=\"verExpediente('" + data[i].id + "');\"><i class='fa fa-files-o'></i></button> <button class='btn btn-success' title='Ver obligaciones' onclick=\"IrMonitoreo('" + data[i].id + "');\"><i class='fa fa-legal'></i></button></div>");
                fila.push("-")
                //fila.push("<button class='btn btn-danger' title='Ir a contrato' onclick=\"IrMonitoreo('" + data[i].ID_CONTRATO_PK + "');\"><i class='fa fa-arrow-circle-right'></i></button>")
            } else if (RolUsuario == 20) {//RespContrato                
                fila.push("<div align='center'><button class='btn btn-secondary' title='Asignar producto(s) a contrato' onclick=\"ConPro('" + data[i].id + "');\"><i class='fa fa-wrench'></i></button> <button class='btn btn-warning' title='Asignación de responsabilidades' onclick=\"asociarObligaciones('" + data[i].id + "');\"><i class='fa fa-link'></i></button> <button class='btn btn-info' title='Asignar responsable de apego contractual' onclick=\"AsignarRespApego('" + data[i].id + "');\"><i class='fa fa-user'></i></button></div>");
                fila.push("<button class='btn btn-danger' title='Ir a plan de entrega' onclick=\"IrMonitoreo('" + data[i].id + "');\"><i class='fa fa-arrow-circle-right'></i></button>")
            } else if (RolUsuario == 21) {
                fila.push("<div align='center'><button class= 'btn btn-default' title = 'Ir a plan monitoreo' onclick =\"IrPlanMonitoreo('" + data[i].id + "');\"><i class='glyphicon glyphicon-indent-left'></i></button></div>");
                fila.push("-")
            } else if (RolUsuario == 22) {
                fila.push("<div align='center'><button class= 'btn btn-default' title = 'Ir a plan de entrega' onclick =\"IrPlanEntrega('" + data[i].id + "');\"><i class='glyphicon glyphicon-indent-right'></i></button></div>");
                fila.push("-")
            } else {
                fila.push("");
                fila.push("")
            }

            listado.push(fila);
        }

        var table = $('#tbl_contrato').DataTable();

        table.destroy();

        $('#tbl_contrato').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            data: listado,
            columns: [
                { title: "No." },
                { title: "Nombre del contrato" },
                { title: "Inicio" },
                { title: "Fin" },
                { title: "Tipo de contrato" },
                { title: "Acciones" },
                { title: "Ir..." },
            ],
            columnDefs: [
                {
                    targets: -1,
                    className: 'dt-body-center'
                }]
        });
        LaunchLoader(false);
    });
}

function SelectDrop(input, valor) {
    return input == valor ? "selected" : "";
}

function AsignarRespApego(idContrato) {
    LaunchLoader(true);

    $('#idContrato').val(idContrato);



    $.ajaxSetup({
        async: false
    });

    $.get("/Request/Dependencia/Contrato/" + idContrato, function (data, status) {

        //Se asigna la dependencia 
        $("#idDependencia").val(data);
        DropResponsablesApego();
    });

    $('#modalRespApegoContrato').modal('show');

    getResponsableApego();

    $.ajaxSetup({
        async: true
    });

    //setTimeout(function () {
    //    getResponsableApego();
    //}, 500);  
    LaunchLoader(false);
}

function guardarResponsableApego() {

    var Validacion = ValidarRespApego();
    if (Validacion.Bit) {

        ErrorSA('Error en los datos de entrada', Validacion.Texto);

    }
    else {
        var OBJ_RespApegoForm = RespApegoContratoClass;
        var d = new Date();
        var date = (d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate());
        OBJ_RespApegoForm.id = '00000000-0000-0000-0000-000000000000';
        OBJ_RespApegoForm.ID_SERVIDOR_PUBLICO_FK = $('#DropResponsableApego').val();
        OBJ_RespApegoForm.ID_CONTRATO_FK = $('#idContrato').val();
        OBJ_RespApegoForm.ESTATUS = true;
        OBJ_RespApegoForm.INCLUSION = date;

        var form_data = new FormData();
        form_data.append('RespApegoContractualForm', JSON.stringify(OBJ_RespApegoForm));

        $.ajax({
            dataType: 'text',
            cache: false,
            contentType: false,
            processData: false,
            data: form_data,
            type: 'post',

            success: function (data) {
                var objresponse = JSON.parse(data);
                if (!objresponse.Bit) {

                    function confirmar() {
                        return closeModalRC();
                    }
                    var Conf = eval(confirmar);
                    SuccessSAAction("Operación exitosa", "El registro se guardado correctamente", Conf);

                    $('#DropResponsableApego').html("");
                    $('#idDependencia').val("");
                    $('#idContrato').val("");
                }
                else {
                    ErrorSA("", objresponse.Descripcion);
                }
            },
            error: function () {
                var objresponse = JSON.parse(data);
                ErrorSA('', objresponse.Descripcion)
            },
            processData: false,
            type: 'POST',
            url: '/Request/Contrato/AddRespApegoContrato'

        })
    }


}

function closeModalRC() {
    $('#modalRespApegoContrato').modal('hide');
}

function CancelarAsignarRespApego() {
    $('#idDependencia').val("");
    $('#idContrato').val("");
    $('#modalRespApegoContrato').modal('hide');
    $('#DropResponsableApego').html("");
}

function ValidarRespApego() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#DropResponsableApego').val() == '' || $('#DropResponsableApego').val() == '0') {
        Response.Texto = 'Debe seleccionar un responsable';
        Response.Bit = true;
        return Response;
    }

    Response.Texto = '';
    Response.Bit = false;
    return Response;
}

function DropResponsablesApego() {
    var idDependencia = $('#HDidDependencia').val();


    //alert('idDependencia: ' + idDependencia + " / " + 'idContrato: ' + idContrato);

    var url = $("#EndPointAC").val() + "SerServidorPublico/Get/sigla/RAC/dependencia/" + idDependencia;
    $.get(url, function (data, status) {

        var Body = "<option value='0'>Selecciona una opción</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            var nombreServPub = data[i].NOMBRE + ' ' + data[i].AP_PATERNO + ' ' + data[i].AP_MATERNO;
            Body = Body + "<option value='" + data[i].ID_SERVIDOR_PUBLICO_PK + "'>" + nombreServPub + "</option>";
        }
        $('#DropResponsableApego').html(Body);
        //if (idRespSelect != null && idRespSelect != '00000000-0000-0000-0000-000000000000') { 
        //    $("#DropResponsableApego option[value='" + idRespSelect + "']").prop('selected', true);
        //}
    });

    //$.get("/Request/ResponsableApego/Contrato/get/" + idContrato, function (RespApego, status) {
    //    idRespSelect = RespApego.ID_SERVIDOR_PUBLICO_FK;

    //    if (idRespSelect != null && idRespSelect != '00000000-0000-0000-0000-000000000000') {
    //        console.log(idRespSelect);
    //        //idRespSelect = RespApego.ID_SERVIDOR_PUBLICO_FK;
    //        $('#modalRespApegoContrato').modal('show');
    //        $("#DropResponsableApego option[value='" + idRespSelect + "']").prop('selected', true);
    //    }

    //});

}

function getResponsableApego() {
    var idContrato = $('#idContrato').val();
    var idRespSelect = null;
    $.get("/Request/ResponsableApego/Contrato/get/" + idContrato, function (RespApego, status) {
        idRespSelect = RespApego.ID_SERVIDOR_PUBLICO_FK;

        if (idRespSelect != null && idRespSelect != '00000000-0000-0000-0000-000000000000') {
            console.log(idRespSelect);
            //idRespSelect = RespApego.ID_SERVIDOR_PUBLICO_FK;
            $("#DropResponsableApego option[value='" + idRespSelect + "']").prop('selected', true);
        }

    });
}

function verDetalle(id) {
    $.get("/Request/Contrato/GetById/" + id, function (data, status) {

        $("#lblIdContrato").html(data.ID_CONTRATO_PK);
        $("#lblNumero").html(data.NUMERO);
        $("#lblObjeto").html(data.OBJETO);
    });

    $("#modalVerDetalle").modal('show');
}

function verExpediente(id) {
    $.get("/EnableSession/Index/" + id, function (data, status) {
        //RolUsuario = $('#HDRol').val();
        if (RolUsuario == 19) {
            window.location.href = "/GestionExpedientes/ExpedientesContrato/" + id;
        }
    });
}

//JC
function IrPlanMonitoreo(id) {
    $.get("/EnableSession/Index/" + id, function (data, status) {
        window.location.href = "/PlanDeMonitoreo/Lista/" + id
    });
}
function IrPlanEntrega(id) {
    $.get("/EnableSession/Index/" + id, function (data, status) {
        window.location.href = "/PlanDeEntrega/Lista/" + id
    });
}


//JC

function asociarObligaciones(id) {
    window.location.href = "/Contrato/AsignacionResponsabilidades/" + id;
}
function ConPro(id) {
    window.location.href = "/Contrato/ContratoProducto/" + id;
}
function IrMonitoreo(id) {
    $.get("/EnableSession/Index/" + id, function (data, status) {
        //RolUsuario = $('#HDRol').val();
        if (RolUsuario == 19) {
            return window.location.href = "/DesgloseContrato/Index/" + id;
        }
        //else if (RolUsuario == 20) {
        //    return window.location.href = "/PlanDeEntrega/Lista/" + id;
        //}
        //else if (RolUsuario == 21) {
        //    return window.location.href = "/PlanDeMonitoreo/Lista/" + id;
        //}
        //else {
        //    return window.location.href = "/PlanDeEntrega/Lista/" + id;
        //}
    });
}
function Obligaciones(id) {
    window.location.href = "/DesgloseContrato/Index/" + id;
}

var RespApegoContratoClass = {
    id: null,
    ID_SERVIDOR_PUBLICO_FK: null,
    ID_CONTRATO_FK: null,
    ESTATUS: null,
    INCLUSION: null
}

function filtrar_contrato(estatus) {
    localStorage.setItem('estatusContrato', estatus);
    obtenerListado(estatus);
}