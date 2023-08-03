$(document).ready(function () {
    LaunchLoader(true);
    $("#tbl_tipo_sancion").DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        "columnDefs": [
            { "width": "5%", "targets": 0 },
            { "width": "80%", "targets": 1 },
            { "width": "15%", "targets": 2 },
        ],
    });
    Get_lista_tipo_sancion();
})

function Get_lista_tipo_sancion() {
    $.get($("#EndPointApego").val() + "SerSancion/Get", function (data, status) {
        var listado = [];
        console.log(data);
        for (var i = 0; i <= data.length - 1; i++) {
            var fila = [];
            fila.push(i + 1);
            fila.push(data[i].sansion);
            fila.push("<button class='btn btn-primary' title='Editar' onclick=\"Editar_Tipo_Sancion('" + data[i].id + "','" + data[i].sansion + "');\"><i class='fa fa-edit'></i></button> " +
                "<button class='btn btn-danger' title='Eliminar' onclick=\"DeleteTipoSancion('" + data[i].id + "');\"><i class='fa fa-trash'></i></button> ");
            listado.push(fila);
        }

        var table = $('#tbl_tipo_sancion').DataTable();

        table.destroy();

        $('#tbl_tipo_sancion').DataTable({
            data: listado,
            columns: [
                { title: "No." },
                { title: "Tipo de sansion" },
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

$("#btn_guardar_tipo_sancion").click(function () {
    Add_Tipo_Sancion();
})

function Add_Tipo_Sancion() {

    if ($("#txt_tipo_sancion_add").val() == "") {
        return ErrorSA('Error en los datos de entrada', 'Debe ingresar un tipo de periodo.');
    }
    tbl_tipo_sancion.p_sancion = $("#txt_tipo_sancion_add").val();
    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(tbl_tipo_sancion),
        type: 'post',

        success: function (data) {
            var objresponse = JSON.parse(data);
            //console.log(objresponse[0].cod)
            if (objresponse.response[0].cod == "success") {
                function si() {
                    return Cerrar_Modal_Tipo_Sancion();
                }
                var AccionSi = eval(si);
                SuccessSAAction("Operación exitosa", "El registro se guardado correctamente", AccionSi);
                Get_lista_tipo_sancion();

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
        url: $("#EndPointApego").val() + "SerSancion/Add/Sanciones"

    })
}

function Editar_Tipo_Sancion(id, tipo_sanc) {
    $('.clear_txt_upd').val("");
    $("#id_tipo_sancion").val("");
    $('#Modal_Tipo_Sancion_Upd').modal({ backdrop: 'static', keyboard: false });
    $('#Modal_Tipo_Sancion_Upd').modal('show');
    $("#id_tipo_sancion").val(id);
    $("#txt_tipo_sancion_upd").val(tipo_sanc);
}

$("#Add_TipoSancion").click(function () {
    Abrir_Modal_Tipo_Sancion();
})

function Abrir_Modal_Tipo_Sancion() {
    $('.clear_txt').val('');
    $('#Modal_Tipo_Sancion').modal({ backdrop: 'static', keyboard: false });
    $('#Modal_Tipo_Sancion').modal('show');
}

function Cerrar_Modal_Tipo_Sancion() {
    $('#Modal_Tipo_Sancion').modal('hide');
    $('.clear_txt').val('');
}

function DeleteTipoSancion(item) {
    function Confirmacion_() {
        return eliminar_tipo_sancion(item);
    }
    var AccionSi = eval(Confirmacion_);
    WarningSA('', '¿En verdad desea eliminar el registro?', 'Si, Continuar', 'No, Cancelar', AccionSi, '');
}

function eliminar_tipo_sancion(item) {
    $.ajax({
        url: $("#EndPointApego").val() + "SerSancion/Delete/Sanciones/" + item,
        dataType: 'text',
        cache: false,
        contentType: false,
        processData: false,
        type: "DELETE",
        success: function (data) {
            var objresponse = JSON.parse(data);
            if (objresponse[0].cod == "success") {
                SuccessSA('', 'El registro se eliminó exitosamente');
                Get_lista_tipo_sancion();
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

var tbl_tipo_sancion = {
    p_id: "00000000-0000-0000-0000-000000000000",
    p_sancion: ""
}