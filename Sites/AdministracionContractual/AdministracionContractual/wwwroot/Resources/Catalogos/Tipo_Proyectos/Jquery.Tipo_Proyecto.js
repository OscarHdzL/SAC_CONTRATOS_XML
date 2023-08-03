$(document).ready(function () {
    LaunchLoader(true);
    $("#tbl_tipo_proyecto").DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        "columnDefs": [
            { "width": "5%", "targets": 0 },
            { "width": "80%", "targets": 1 },
            { "width": "15%", "targets": 2 },
        ],
    });
    Get_lista_tipo_proyecto();
})

function Get_lista_tipo_proyecto() {
    var id_instancia = $("#HDidInstancia").val();
    $.get($("#EndPointAdmon").val() + "Catalogos/Get/TProyecto/" + id_instancia, function (data, status) {
        var listado = [];
        console.log(data);
        for (var i = 0; i <= data.length - 1; i++) {
            var fila = [];
            fila.push(i + 1);
            fila.push(data[i].tipo_proyecto);
            fila.push("<button class='btn btn-primary' title='Editar' onclick=\"Editar_Tipo_Proyecto('" + data[i].id + "','" + data[i].tipo_proyecto + "');\"><i class='fa fa-edit'></i></button> " +
                "<button class='btn btn-danger' title='Eliminar' onclick=\"DeleteTipoProyecto('" + data[i].id + "');\"><i class='fa fa-trash'></i></button> ");
            listado.push(fila);
        }

        var table = $('#tbl_tipo_proyecto').DataTable();

        table.destroy();

        $('#tbl_tipo_proyecto').DataTable({
            data: listado,
            columns: [
                { title: "No." },
                { title: "Tipo de proyecto" },
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



$("#Add_TipoProyecto").click(function () {
    Abrir_Modal_Tipo_Proyecto();
})

$("#btn_guardar_tipo_proyecto").click(function () {
    Add_Tipo_Proyecto();
})

function Add_Tipo_Proyecto() {

    if ($("#txt_tipo_proyecto_add").val() == "") {
        return ErrorSA('Error en los datos de entrada', 'Debe ingresar un tipo de proyecto.');
    }
    tbl_tipo_proyecto.p_tipo_proyecto = $("#txt_tipo_proyecto_add").val();
    tbl_tipo_proyecto.p_tbl_instancia_id = $("#HDidInstancia").val();

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(tbl_tipo_proyecto),
        type: 'post',

        success: function (data) {
            var objresponse = JSON.parse(data);
            console.log(objresponse[0].cod)
            if (objresponse[0].cod == "success") {
                function si() {
                    return Cerrar_Modal_Tipo_Proyecto();
                }
                var AccionSi = eval(si);
                SuccessSAAction("Operación exitosa", "El registro se guardado correctamente", AccionSi);
                Get_lista_tipo_proyecto();

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
        url: $("#EndPointAdmon").val() + "Catalogos/Add/TProyecto"

    })
}

function Editar_Tipo_Proyecto(id, tipo_doc) {
    $('.clear_txt_upd').val("");
    $("#id_tipo_proyecto").val("");
    $('#Modal_Tipo_Proyecto_Upd').modal({ backdrop: 'static', keyboard: false });
    $('#Modal_Tipo_Proyecto_Upd').modal('show');
    $("#id_tipo_proyecto").val(id);
    $("#txt_tipo_proyecto_upd").val(tipo_doc);
}

function DeleteTipoProyecto(item) {
    function Confirmacion_() {
        return eliminar_tipo_proyecto(item);
    }
    var AccionSi = eval(Confirmacion_);
    WarningSA('', '¿En verdad desea eliminar el registro?', 'Si, Continuar', 'No, Cancelar', AccionSi, '');
}

function eliminar_tipo_proyecto(item) {
    $.ajax({
        url: $("#EndPointAdmon").val() + "Catalogos/Delete/TProyecto/" + item,
        dataType: 'text',
        cache: false,
        contentType: false,
        processData: false,
        type: "DELETE",
        success: function (data) {
            var objresponse = JSON.parse(data);
            if (objresponse[0].cod == "success") {
                SuccessSA('', 'El registro se eliminó exitosamente');
                Get_lista_tipo_proyecto();
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

function Abrir_Modal_Tipo_Proyecto() {
    $('.clear_txt').val('');
    $('#Modal_Tipo_Proyecto').modal({ backdrop: 'static', keyboard: false });
    $('#Modal_Tipo_Proyecto').modal('show');
}

function Cerrar_Modal_Tipo_Proyecto() {
    $('#Modal_Tipo_Proyecto').modal('hide');
    $('.clear_txt').val('');
}

var tbl_tipo_proyecto = {
    p_id: "00000000-0000-0000-0000-000000000000",
    p_tipo_proyecto: "",
    p_tbl_instancia_id: ""
}