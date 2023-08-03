$(document).ready(function () {
    LaunchLoader(true);
    $("#tbl_tipo_obligacion").DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        "columnDefs": [
            { "width": "5%", "targets": 0 },
            { "width": "80%", "targets": 1 },
            { "width": "15%", "targets": 2 },
        ],
    });
    Get_lista_tipo_obligacion();
})

function Get_lista_tipo_obligacion() {
    //var id_instancia = $("#HDidInstancia").val();
    $.get($("#EndPointApego").val() + "SerObligacion/Get/Tipooblig", function (data, status) {
        var listado = [];
        console.log(data);
        for (var i = 0; i <= data.length - 1; i++) {
            var fila = [];
            fila.push(i + 1);
            fila.push(data[i].tipo_obligacion);
            fila.push("<button class='btn btn-primary' title='Editar' onclick=\"Editar_Tipo_Obligacion('" + data[i].id + "','" + data[i].tipo_obligacion + "');\"><i class='fa fa-edit'></i></button> " +
                "<button class='btn btn-danger' title='Eliminar' onclick=\"DeleteTipoObligacion('" + data[i].id + "');\"><i class='fa fa-trash'></i></button> ");
            listado.push(fila);
        }

        var table = $('#tbl_tipo_obligacion').DataTable();

        table.destroy();

        $('#tbl_tipo_obligacion').DataTable({
            data: listado,
            columns: [
                { title: "No." },
                { title: "Tipo de obligacion" },
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

$("#btn_guardar_tipo_obligacion").click(function () {
    Add_Tipo_Obligacion();
})

function Add_Tipo_Obligacion() {

    if ($("#txt_tipo_obligacion_add").val() == "") {
        return ErrorSA('Error en los datos de entrada', 'Debe ingresar un tipo de periodo.');
    }
    tbl_tipo_obligacion.p_tipo_obligacion = $("#txt_tipo_obligacion_add").val();
    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(tbl_tipo_obligacion),
        type: 'post',

        success: function (data) {
            var objresponse = JSON.parse(data);
            //console.log(objresponse[0].cod)
            if (objresponse.response[0].cod == "success") {
                function si() {
                    return Cerrar_Modal_Tipo_Obligacion();
                }
                var AccionSi = eval(si);
                SuccessSAAction("Operación exitosa", "El registro se guardado correctamente", AccionSi);
                Get_lista_tipo_obligacion();

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
        url: $("#EndPointApego").val() + "SerObligacion/Add/Obligaciones"

    })
}

function Editar_Tipo_Obligacion(id, tipo_obli) {
    $('.clear_txt_upd').val("");
    $("#id_tipo_obligacion").val("");
    $('#Modal_Tipo_Obligacion_Upd').modal({ backdrop: 'static', keyboard: false });
    $('#Modal_Tipo_Obligacion_Upd').modal('show');
    $("#id_tipo_obligacion").val(id);
    $("#txt_tipo_obligacion_upd").val(tipo_obli);
}

function DeleteTipoObligacion(item) {
    function Confirmacion_() {
        return eliminar_tipo_obligacion(item);
    }
    var AccionSi = eval(Confirmacion_);
    WarningSA('', '¿En verdad desea eliminar el registro?', 'Si, Continuar', 'No, Cancelar', AccionSi, '');
}

function eliminar_tipo_obligacion(item) {
    $.ajax({
        url: $("#EndPointApego").val() + "SerObligacion/Delete/Obligaciones/" + item,
        dataType: 'text',
        cache: false,
        contentType: false,
        processData: false,
        type: "DELETE",
        success: function (data) {
            var objresponse = JSON.parse(data);
            if (objresponse[0].cod == "success") {
                SuccessSA('', 'El registro se eliminó exitosamente');
                Get_lista_tipo_obligacion();
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

$("#Add_TipoObligacion").click(function () {
    Abrir_Modal_Tipo_Obligacion();
})
function Abrir_Modal_Tipo_Obligacion() {
    $('.clear_txt').val('');
    $('#Modal_Tipo_Obligacion').modal({ backdrop: 'static', keyboard: false });
    $('#Modal_Tipo_Obligacion').modal('show');
}
function Cerrar_Modal_Tipo_Obligacion() {
    $('#Modal_Tipo_Obligacion').modal('hide');
    $('.clear_txt').val('');
}

var tbl_tipo_obligacion = {
    p_id: "00000000-0000-0000-0000-000000000000",
    p_tipo_obligacion: ""
}