$(document).ready(function () {
    LaunchLoader(true);
    $("#tbl_unidad_medida").DataTable({
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
    Get_lista_unidad_medida();
})

function Get_lista_unidad_medida() {
    $.get($("#EndPointAdmon").val() + "Catalogos/Get/UnidadesMedida", function (data, status) {
        var listado = [];
        console.log(data);
        for (var i = 0; i <= data.length - 1; i++) {
            var fila = [];
            fila.push(i + 1);
            fila.push(data[i].unidad_medida);
            fila.push(data[i].clave);
            fila.push("<button class='btn btn-primary' title='Editar' onclick=\"Editar_Unidad_Medida('" + data[i].id + "','" + data[i].unidad_medida + "','" + data[i].clave + "');\"><i class='fa fa-edit'></i></button> " +
                "<button class='btn btn-danger' title='Eliminar' onclick=\"DeleteUnidadM('" + data[i].id + "');\"><i class='fa fa-trash'></i></button> ");
            listado.push(fila);
        }

        var table = $('#tbl_unidad_medida').DataTable();

        table.destroy();

        $('#tbl_unidad_medida').DataTable({
            data: listado,
            columns: [
                { title: "No." },
                { title: "Unidad de medida" },
                { title: "Clave" },
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

function Abrir_Modal_Unidad_Medida() {
    $('.clear_txt').val('');
    $('#Modal_Unidad_Medida').modal({ backdrop: 'static', keyboard: false });
    $('#Modal_Unidad_Medida').modal('show');
}

function Cerrar_Modal_Unidad_Medida() {
    $('#Modal_Unidad_Medida').modal('hide');
    $('.clear_txt').val('');
}

$("#Add_UnidadMedida").click(function () {
    Abrir_Modal_Unidad_Medida();
})

$("#btn_guardar_unidad_medida").click(function () {
    Add_Unidad_Medida();
})

function Add_Unidad_Medida() {

    if ($("#txt_unidad_medida_add").val() == "" || $("#txt_clave_add").val()=="") {
        return ErrorSA('Error en los datos de entrada', 'Debe ingresar unidad de medida y clave.');
    }
    tbl_unidad_medida.p_unidad_medida = $("#txt_unidad_medida_add").val();
    tbl_unidad_medida.p_clave = $("#txt_clave_add").val();
    //debugger;
    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(tbl_unidad_medida),
        type: 'post',

        success: function (data) {
            var objresponse = JSON.parse(data);
            console.log(objresponse[0].cod)
            if (objresponse[0].cod == "success") {
                function si() {
                    return Cerrar_Modal_Unidad_Medida();
                }
                var AccionSi = eval(si);
                SuccessSAAction("Operación exitosa", "El registro se guardado correctamente", AccionSi);
                Get_lista_unidad_medida();

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
        url: $("#EndPointAdmon").val() + "Catalogos/Add/UnidadesMedida"

    })
}

function Editar_Unidad_Medida(id, unidad_m,clave) {
    $('.clear_txt_upd').val("");
    $("#id_unidad_m").val("");
    $('#Modal_Unidad_Medida_Upd').modal({ backdrop: 'static', keyboard: false });
    $('#Modal_Unidad_Medida_Upd').modal('show');
    $("#id_unidad_m").val(id);
    $("#txt_unidad_medida_upd").val(unidad_m);
    $("#txt_clave_upd").val(clave);
}

function DeleteUnidadM(item) {
    function Confirmacion_() {
        return eliminar_unidad_medida(item);
    }
    var AccionSi = eval(Confirmacion_);
    WarningSA('', '¿En verdad desea eliminar el registro?', 'Si, Continuar', 'No, Cancelar', AccionSi, '');
}

function eliminar_unidad_medida(item) {
    $.ajax({
        url: $("#EndPointAdmon").val() + "Catalogos/Delete/UnidadesMedida/" + item,
        dataType: 'text',
        cache: false,
        contentType: false,
        processData: false,
        type: "DELETE",
        success: function (data) {
            var objresponse = JSON.parse(data);
            if (objresponse[0].cod == "success") {
                SuccessSA('', 'El registro se eliminó exitosamente');
                Get_lista_unidad_medida();
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

var tbl_unidad_medida = {
    p_id: "00000000-0000-0000-0000-000000000000",
    p_unidad_medida: "",
    p_clave: ""
}