$(document).ready(function () {
    LaunchLoader(true);
    $("#tbl_procedimiento").DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        "columnDefs": [
            { "width": "5%", "targets": 0 },
            { "width": "65%", "targets": 1 },
            { "width": "15%", "targets": 2 },
            { "width": "15%", "targets": 3 },

        ],
    });
    Get_lista_procedimiento();
})

function Get_lista_procedimiento() {
    $.get($("#EndPointAdmon").val() + "Catalogos/Get/Procedimiento", function (data, status) {
        var listado = [];
        console.log(data);
        for (var i = 0; i <= data.length - 1; i++) {
            var fila = [];
            fila.push(i + 1);
            fila.push(data[i].procedimiento);
            fila.push(data[i].sigla);
            fila.push("<button class='btn btn-primary' title='Editar' onclick=\"Editar_Procedimiento('" + data[i].id + "','" + data[i].procedimiento + "','" + data[i].sigla + "');\"><i class='fa fa-edit'></i></button> " +
                "<button class='btn btn-danger' title='Eliminar' onclick=\"DeleteProcedimientoM('" + data[i].id + "');\"><i class='fa fa-trash'></i></button> ");
            listado.push(fila);
        }

        var table = $('#tbl_procedimiento').DataTable();

        table.destroy();

        $('#tbl_procedimiento').DataTable({
            data: listado,
            columns: [
                { title: "No." },
                { title: "Procedimiento" },
                { title: "Sigla" },
                { title: "Acciones" },
            ],
            destroy: true,
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            columnDefs: [
                {
                    targets: -1,
                    className: 'dt-body-center'
                }]
        });
        LaunchLoader(false);
    });
}

function Abrir_Modal_Procedimiento() {
    $('.clear_txt').val('');
    $('#Modal_Procedimiento').modal({ backdrop: 'static', keyboard: false });
    $('#Modal_Procedimiento').modal('show');
}

function Cerrar_Modal_Procedimiento() {
    $('#Modal_Procedimiento').modal('hide');
    $('.clear_txt').val('');
}

$("#Add_Procedimiento").click(function () {
    Abrir_Modal_Procedimiento();
})

$("#btn_guardar_procedimiento").click(function () {
    Add_Procedimiento();
})

function Add_Procedimiento() {

    if ($("#txt_procedimiento_add").val() == "" || $("#txt_sigla_add").val() == "") {
        return ErrorSA('Error en los datos de entrada', 'Debe ingresar procedimiento y sigla.');
    }
    tbl_procedimiento.p_procedimiento = $("#txt_procedimiento_add").val();
    tbl_procedimiento.p_sigla = $("#txt_sigla_add").val();
    //debugger;
    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(tbl_procedimiento),
        type: 'post',

        success: function (data) {
            var objresponse = JSON.parse(data);
            console.log(objresponse[0].cod)
            if (objresponse[0].cod == "success") {
                function si() {
                    return Cerrar_Modal_Procedimiento();
                }
                var AccionSi = eval(si);
                SuccessSAAction("Operación exitosa", "El registro se guardado correctamente", AccionSi);
                Get_lista_procedimiento();

            }
            else if (objresponse[0].cod == "warning") {
                ErrorSA("", objresponse[0].msg);
            }
            else {
                ErrorSA("", "Ocurrio un error.");
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(XMLHttpRequest.responseText);
            if (XMLHttpRequest.responseText != null) {
                var errorResponse = JSON.parse(XMLHttpRequest.responseText);
                console.log(errorResponse);
                if (errorResponse.length > 0) {
                    if (errorResponse[0].msg != null) {
                        ErrorSA('Error', errorResponse[0].msg);
                    } else {
                        ErrorSA('Error', "Ocurrio un error.");
                    }
                } else {
                    ErrorSA('Error', "Ocurrio un error.");
                }

            } else {
                ErrorSA('Error', "Ocurrio un error.");
            }
        },
        processData: false,
        type: 'POST',
        url: $("#EndPointAdmon").val() + "Catalogos/Add/Procedimiento"

    })
}

function Editar_Procedimiento(id, procedimiento, sigla) {
    $('.clear_txt_upd').val("");
    $("#id_procedimiento").val("");
    $('#Modal_Procedimiento_Upd').modal({ backdrop: 'static', keyboard: false });
    $('#Modal_Procedimiento_Upd').modal('show');
    $("#id_procedimiento").val(id);
    $("#txt_procedimiento_upd").val(procedimiento);
    $("#txt_sigla_upd").val(sigla);
}

function DeleteProcedimientoM(item) {
    function Confirmacion_() {
        return eliminar_procedimiento(item);
    }
    var AccionSi = eval(Confirmacion_);
    WarningSA('', '¿En verdad desea eliminar el registro?', 'Si, Continuar', 'No, Cancelar', AccionSi, '');
}

function eliminar_procedimiento(item) {
    $.ajax({
        url: $("#EndPointAdmon").val() + "Catalogos/Delete/Procedimiento/" + item,
        dataType: 'text',
        cache: false,
        contentType: false,
        processData: false,
        type: "DELETE",
        success: function (data) {
            var objresponse = JSON.parse(data);
            if (objresponse[0].cod == "success") {
                SuccessSA('', 'El registro se eliminó exitosamente');
                Get_lista_procedimiento();
            }
            else {
                ErrorSA("", "Ocurrió un error al eliminar, intente nuevamente.");
            }

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(XMLHttpRequest.responseText);
            if (XMLHttpRequest.responseText != null) {
                var errorResponse = JSON.parse(XMLHttpRequest.responseText);
                if (errorResponse.msg != null) {
                    ErrorSA('Error', errorResponse.msg);
                } else {
                    ErrorSA('Error', "Ocurrio un error.");
                }

            } else {
                ErrorSA('Error', "Ocurrio un error.");
            }
        },
    });
}

var tbl_procedimiento = {
    p_id: "00000000-0000-0000-0000-000000000000",
    p_procedimiento: "",
    p_sigla: ""
}