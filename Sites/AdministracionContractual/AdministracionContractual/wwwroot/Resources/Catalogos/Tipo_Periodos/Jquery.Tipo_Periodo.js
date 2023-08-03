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
    Get_lista_tipo_periodo();
})

function Get_lista_tipo_periodo() {
        //var id_instancia = $("#HDidInstancia").val();
    $.get($("#EndPointApego").val() + "SerObligacion/Get/Periodos", function (data, status) {
            var listado = [];
            console.log(data);
            for (var i = 0; i <= data.length - 1; i++) {
                var fila = [];
                fila.push(i + 1);
                fila.push(data[i].periodo);
                fila.push("<button class='btn btn-primary' title='Editar' onclick=\"Editar_Tipo_Periodo('" + data[i].id + "','" + data[i].periodo + "');\"><i class='fa fa-edit'></i></button> " +
                    "<button class='btn btn-danger' title='Eliminar' onclick=\"DeleteTipoPeriodo('" + data[i].id + "');\"><i class='fa fa-trash'></i></button> ");
                listado.push(fila);
            }

            var table = $('#tbl_tipo_periodo').DataTable();

            table.destroy();

            $('#tbl_tipo_periodo').DataTable({
                data: listado,
                columns: [
                    { title: "No." },
                    { title: "Tipo de periodo" },
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

$("#btn_guardar_tipo_periodo").click(function () {
    Add_Tipo_Periodo();
})

function Add_Tipo_Periodo() {

    if ($("#txt_tipo_periodo_add").val() == "") {
        return ErrorSA('Error en los datos de entrada', 'Debe ingresar un tipo de periodo.');
    }
    tbl_tipo_periodo.p_periodo = $("#txt_tipo_periodo_add").val();
    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(tbl_tipo_periodo),
        type: 'post',

        success: function (data) {
            var objresponse = JSON.parse(data);
            //console.log(objresponse[0].cod)
            if (objresponse.response[0].cod == "success") {
                function si() {
                    return Cerrar_Modal_Tipo_Periodo();
                }
                var AccionSi = eval(si);
                SuccessSAAction("Operación exitosa", "El registro se guardado correctamente", AccionSi);
                Get_lista_tipo_periodo();

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
        url: $("#EndPointApego").val() + "SerObligacion/Add/Periodos"

    })
}

function Editar_Tipo_Periodo(id, tipo_per) {
    $('.clear_txt_upd').val("");
    $("#id_tipo_periodo").val("");
    $('#Modal_Tipo_Periodo_Upd').modal({ backdrop: 'static', keyboard: false });
    $('#Modal_Tipo_Periodo_Upd').modal('show');
    $("#id_tipo_periodo").val(id);
    $("#txt_tipo_periodo_upd").val(tipo_per);
}

function Cerrar_Modal_Tipo_Periodo() {
    $('#Modal_Tipo_Periodo').modal('hide');
    $('.clear_txt').val('');
}

$("#Add_TipoPeriodo").click(function () {
    Abrir_Modal_Tipo_Periodo();
})

function Abrir_Modal_Tipo_Periodo() {
    $('.clear_txt').val('');
    $('#Modal_Tipo_Periodo').modal({ backdrop: 'static', keyboard: false });
    $('#Modal_Tipo_Periodo').modal('show');
}

function DeleteTipoPeriodo(item) {
    function Confirmacion_() {
        return eliminar_tipo_periodo(item);
    }
    var AccionSi = eval(Confirmacion_);
    WarningSA('', '¿En verdad desea eliminar el registro?', 'Si, Continuar', 'No, Cancelar', AccionSi, '');
}

function eliminar_tipo_periodo(item) {
    $.ajax({
        url: $("#EndPointApego").val() + "SerObligacion/Delete/Periodos/" + item,
        dataType: 'text',
        cache: false,
        contentType: false,
        processData: false,
        type: "DELETE",
        success: function (data) {
            var objresponse = JSON.parse(data);
            if (objresponse[0].cod == "success") {
                SuccessSA('', 'El registro se eliminó exitosamente');
                Get_lista_tipo_periodo();
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
    
var tbl_tipo_periodo = {
    p_id: "00000000-0000-0000-0000-000000000000",
    p_periodo: ""
}