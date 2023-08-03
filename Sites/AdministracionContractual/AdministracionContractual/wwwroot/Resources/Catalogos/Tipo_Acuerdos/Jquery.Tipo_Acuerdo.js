$(document).ready(function () {
    LaunchLoader(true);
    $("#tbl_tipo_periodo").DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        "columnDefs": [
            { "width": "5%", "targets": 0 },
            { "width": "80%", "targets": 1 },
            { "width": "15%", "targets": 2 },
        ],
    });
    Get_lista_tipo_acuerdo();
})

function Get_lista_tipo_acuerdo() {
    $.get($("#EndPointApego").val() + "SerAcuerdos/Get/DropDown/Tipos", function (data, status) {
        var listado = [];
        console.log(data);
        for (var i = 0; i <= data.length - 1; i++) {
            var fila = [];
            fila.push(i + 1);
            fila.push(data[i].text);
            fila.push("<button class='btn btn-primary' title='Editar' onclick=\"Editar_Tipo_Acuerdo('" + data[i].value + "','" + data[i].text + "');\"><i class='fa fa-edit'></i></button> " +
                "<button class='btn btn-danger' title='Eliminar' onclick=\"DeleteTipoAcuerdo('" + data[i].value + "');\"><i class='fa fa-trash'></i></button> ");
            listado.push(fila);
        }

        var table = $('#tbl_tipo_acuerdo').DataTable();

        table.destroy();

        $('#tbl_tipo_acuerdo').DataTable({
            data: listado,
            columns: [
                { title: "No." },
                { title: "Tipo de acuerdo" },
                { title: "Acciones" },
            ],
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

$("#btn_guardar_tipo_acuerdo").click(function () {
    Add_Tipo_Acuerdo();
})

function Add_Tipo_Acuerdo() {

    if ($("#txt_tipo_acuerdo_add").val() == "") {
        return ErrorSA('Error en los datos de entrada', 'Debe ingresar un tipo de periodo.');
    }
    tbl_tipo_acuerdo.p_tipo_acuerdo = $("#txt_tipo_acuerdo_add").val();
    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(tbl_tipo_acuerdo),
        type: 'post',

        success: function (data) {
            var objresponse = JSON.parse(data);
            //console.log(objresponse[0].cod)
            if (objresponse.response[0].cod == "success") {
                function si() {
                    return Cerrar_Modal_Tipo_Acuerdo();
                }
                var AccionSi = eval(si);
                SuccessSAAction("Operación exitosa", "El registro se guardado correctamente", AccionSi);
                Get_lista_tipo_acuerdo();

            }
            else if (objresponse[0].cod == "warning") {
                ErrorSA("", objresponse[0].msg);
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
        url: $("#EndPointApego").val() + "SerAcuerdos/Add/Acuerdos"

    })
}

function Editar_Tipo_Acuerdo(id, tipo_acu) {
    $('.clear_txt_upd').val("");
    $("#id_tipo_acuerdo").val("");
    $('#Modal_Tipo_Acuerdo_Upd').modal({ backdrop: 'static', keyboard: false });
    $('#Modal_Tipo_Acuerdo_Upd').modal('show');
    $("#id_tipo_acuerdo").val(id);
    $("#txt_tipo_acuerdo_upd").val(tipo_acu);
}

function Cerrar_Modal_Tipo_Acuerdo() {
    $('#Modal_Tipo_Acuerdo').modal('hide');
    $('.clear_txt').val('');
}

$("#Add_TipoAcuerdo").click(function () {
    Abrir_Modal_Tipo_Acuerdo();
})

function Abrir_Modal_Tipo_Acuerdo() {
    $('.clear_txt').val('');
    $('#Modal_Tipo_Acuerdo').modal({ backdrop: 'static', keyboard: false });
    $('#Modal_Tipo_Acuerdo').modal('show');
}

function DeleteTipoAcuerdo(item) {
    function Confirmacion_() {
        return eliminar_tipo_acuerdo(item);
    }
    var AccionSi = eval(Confirmacion_);
    WarningSA('', '¿En verdad desea eliminar el registro?', 'Si, Continuar', 'No, Cancelar', AccionSi, '');
}

function eliminar_tipo_acuerdo(item) {
    $.ajax({
        url: $("#EndPointApego").val() + "SerAcuerdos/Delete/Acuerdos/" + item,
        dataType: 'text',
        cache: false,
        contentType: false,
        processData: false,
        type: "DELETE",
        success: function (data) {
            var objresponse = JSON.parse(data);
            if (objresponse[0].cod == "success") {
                SuccessSA('', 'El registro se eliminó exitosamente');
                Get_lista_tipo_acuerdo();
            }
            else {
                ErrorSA("", "Ocurrió un error al eliminar, intente nuevamente.");
            }

        },
        error: function (data) {
            ErrorSA("", "Ocurrió un error al eliminar, intente nuevamente.");
        }
    });
}

var tbl_tipo_acuerdo = {
    p_id: "00000000-0000-0000-0000-000000000000",
    p_tipo_acuerdo: ""
}