$(document).ready(function () {
    LaunchLoader(true);
    $("#tbl_tipo_riesgo").DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        "columnDefs": [
            { "width": "5%", "targets": 0 },
            { "width": "80%", "targets": 1 },
            { "width": "15%", "targets": 2 },
        ],
    });
    Get_Lista_Tipo_riesgos();
})

function Get_Lista_Tipo_riesgos() {
    var id_instancia = $("#HDidInstancia").val();
    $.get($("#EndPointAdmon").val() + "Catalogos/Get/TRiesgo/" + id_instancia, function (data, status) {
        var listado = [];
        console.log(data);
        for (var i = 0; i <= data.length - 1; i++) {
            var fila = [];
            fila.push(i + 1);
            fila.push(data[i].tipo_riesgo);
            fila.push("<button class='btn btn-primary' title='Editar' onclick=\"Editar_Tipo_Riesgo('" + data[i].id + "','" + data[i].tipo_riesgo + "');\"><i class='fa fa-edit'></i></button> " +
                "<button class='btn btn-danger' title='Eliminar' onclick=\"DeleteTipoRiesgo('" + data[i].id + "');\"><i class='fa fa-trash'></i></button> ");
            listado.push(fila);
        }

        var table = $('#tbl_tipo_riesgo').DataTable();

        table.destroy();

        $('#tbl_tipo_riesgo').DataTable({
            data: listado,
            columns: [
                { title: "No." },
                { title: "Tipo de riesgo" },
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

function Abrir_Modal_Tipo_Riesgo() {
    $('.clear_txt').val('');
    $('#Modal_Tipo_Riesgo').modal({ backdrop: 'static', keyboard: false });
    $('#Modal_Tipo_Riesgo').modal('show');
}

function Cerrar_Modal_Tipo_Riesgo() {
    $('#Modal_Tipo_Riesgo').modal('hide');
    $('.clear_txt').val('');
}

$("#Add_TipoRiesgo").click(function () {
    Abrir_Modal_Tipo_Riesgo();
})

$("#btn_guardar_tipo_riesgo").click(function () {
    Add_Tipo_Riesgo();
})

function Add_Tipo_Riesgo() {

    if ($("#txt_tipo_riesgo_add").val() == "") {
        return ErrorSA('Error en los datos de entrada', 'Debe ingresar un tipo de riesgo.');
    }
    tbl_tipo_riesgo.p_tipo_riesgo = $("#txt_tipo_riesgo_add").val();
    tbl_tipo_riesgo.p_tbl_instancia_id = $("#HDidInstancia").val();

    console.log(JSON.stringify(tbl_tipo_riesgo));

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(tbl_tipo_riesgo),
        type: 'post',

        success: function (data) {
            var objresponse = JSON.parse(data);
            console.log(objresponse[0].cod)
            if (objresponse[0].cod == "success") {
                function si() {
                    return Cerrar_Modal_Tipo_Riesgo();
                }
                var AccionSi = eval(si);
                SuccessSAAction("Operación exitosa", "El registro se guardado correctamente", AccionSi);
                Get_Lista_Tipo_riesgos();

            }
            else if (objresponse[0].cod == "warning") {
                ErrorSA("", objresponse[0].msg);
            }
            else {
                ErrorSA("", "Ocurrio un error.");
            }
        },
        error: function () {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse.Descripcion)
        },
        processData: false,
        type: 'POST',
        url: $("#EndPointAdmon").val() + "Catalogos/Add/TRiesgo"

    })
}

function Editar_Tipo_Riesgo(id, tipo_riesgo) {
    $('.clear_txt_upd').val("");
    $("#id_tipo_riesgo").val("");
    $('#Modal_Tipo_Riesgo_Modificar').modal({ backdrop: 'static', keyboard: false });
    $('#Modal_Tipo_Riesgo_Modificar').modal('show');
    $("#id_tipo_riesgo").val(id);
    $("#txt_tipo_riesgo_upd").val(tipo_riesgo);
}

function DeleteTipoRiesgo(item) {
    function Confirmacion() {
        return eliminar_tipo_riesgo(item);
    }
    var AccionSi = eval(Confirmacion);
    WarningSA('', '¿En verdad desea eliminar el registro?', 'Si, Continuar', 'No, Cancelar', AccionSi, '');
}

function eliminar_tipo_riesgo(item) {
    $.ajax({
        url: $("#EndPointAdmon").val() + "Catalogos/Delete/TRiesgo/" + item,
        dataType: 'text',
        cache: false,
        contentType: false,
        processData: false,
        type: "DELETE",
        success: function (data) {
            var objresponse = JSON.parse(data);
            if (objresponse[0].cod == "success") {
                SuccessSA('', 'El registro se eliminó exitosamente');
                Get_Lista_Tipo_riesgos();
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

var tbl_tipo_riesgo = {
    p_id: "00000000-0000-0000-0000-000000000000",
    p_tipo_riesgo: "",
    p_tbl_instancia_id: ""
}