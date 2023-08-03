$(document).ready(function () {
    LaunchLoader(true);
    $("#tbl_tipo_contrato").DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        "columnDefs": [
            { "width": "5%", "targets": 0 },
            { "width": "80%", "targets": 1 },
            { "width": "15%", "targets": 2 },
        ],
    });
    Get_lista_tipo_contrato();
})

function Get_lista_tipo_contrato() {
    var id_instancia = $("#HDidInstancia").val();
    $.get($("#EndPointAdmon").val() + "Catalogos/Get/TContrato/" + id_instancia, function (data, status) {
        var listado = [];
        console.log(data);
        for (var i = 0; i <= data.length - 1; i++) {
            var fila = [];
            fila.push(i + 1);
            fila.push(data[i].tipo_contrato);
            fila.push("<button class='btn btn-primary' title='Editar' onclick=\"Editar_Tipo_Con('" + data[i].id + "','" + data[i].tipo_contrato + "');\"><i class='fa fa-edit'></i></button> " +
                "<button class='btn btn-danger' title='Eliminar' onclick=\"DeleteTipoDoc('" + data[i].id + "');\"><i class='fa fa-trash'></i></button> ");
            listado.push(fila);
        }

        var table = $('#tbl_tipo_contrato').DataTable();

        table.destroy();

        $('#tbl_tipo_contrato').DataTable({
            data: listado,
            columns: [
                { title: "No." },
                { title: "Tipo de contrato" },
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

function Abrir_Modal_Tipo_Contrato() {
    $('.clear_txt').val('');
    $('#Modal_Tipo_Contrato').modal({ backdrop: 'static', keyboard: false });
    $('#Modal_Tipo_Contrato').modal('show');
}

function Cerrar_Modal_Tipo_Contrato() {
    $('#Modal_Tipo_Contrato').modal('hide');
    $('.clear_txt').val('');
}

$("#Add_TipoContrato").click(function () {
    Abrir_Modal_Tipo_Contrato();
})

$("#btn_guardar_tipo_con").click(function () {
    Add_Tipo_Contrato();
})

function Add_Tipo_Contrato() {

    if ($("#txt_tipo_contrato_add").val() == "") {
        return ErrorSA('Error en los datos de entrada', 'Debe ingresar un tipo de documento.');
    }
    tbl_tipo_contrato.p_tipo_contrato = $("#txt_tipo_contrato_add").val();
    tbl_tipo_contrato.p_tbl_instancia_id = $("#HDidInstancia").val();
    //debugger;
    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(tbl_tipo_contrato),
        type: 'post',

        success: function (data) {
            var objresponse = JSON.parse(data);
            console.log(objresponse[0].cod)
            if (objresponse[0].cod == "success") {
                function si() {
                    return Cerrar_Modal_Tipo_Contrato();
                }
                var AccionSi = eval(si);
                SuccessSAAction("Operación exitosa", "El registro se guardado correctamente", AccionSi);
                Get_lista_tipo_contrato();

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
        url: $("#EndPointAdmon").val() + "Catalogos/Add/TContrato"

    })
}

function Editar_Tipo_Con(id, tipo_doc) {
    $('.clear_txt_upd').val("");
    $("#id_tipo_doc").val("");
    $('#Modal_Tipo_Contrato_Upd').modal({ backdrop: 'static', keyboard: false });
    $('#Modal_Tipo_Contrato_Upd').modal('show');
    $("#id_tipo_con").val(id);
    $("#txt_tipo_contrato_upd").val(tipo_doc);
}

function DeleteTipoDoc(item) {
    function Confirmacion_() {
        return eliminar_tipo_doc(item);
    }
    var AccionSi = eval(Confirmacion_);
    WarningSA('', '¿En verdad desea eliminar el registro?', 'Si, Continuar', 'No, Cancelar', AccionSi, '');
}

function eliminar_tipo_doc(item) {
    $.ajax({
        url: $("#EndPointAdmon").val() + "Catalogos/Delete/TContrato/" + item,
        dataType: 'text',
        cache: false,
        contentType: false,
        processData: false,
        type: "DELETE",
        success: function (data) {
            var objresponse = JSON.parse(data);
            if (objresponse[0].cod == "success") {
                SuccessSA('', 'El registro se eliminó exitosamente');
                Get_lista_tipo_contrato();
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

var tbl_tipo_contrato = {
    p_id: "00000000-0000-0000-0000-000000000000",
    p_tipo_contrato: "",
    p_tbl_instancia_id: ""
}