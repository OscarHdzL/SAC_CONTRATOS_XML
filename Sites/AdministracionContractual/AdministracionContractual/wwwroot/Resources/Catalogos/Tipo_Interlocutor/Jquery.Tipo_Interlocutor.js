$(document).ready(function () {
    LaunchLoader(true);
    $("#tbl_tipo_interlocutor").DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        destroy: true,
        "columnDefs": [
            { "width": "5%", "targets": 0 },
            { "width": "80%", "targets": 1 },
            { "width": "15%", "targets": 2 },
        ],
    });
    Get_lista_tipo_interlocutor();
})

function Get_lista_tipo_interlocutor() {
    $.get($("#EndPointAdmon").val() + "Catalogos/Get/TInterlocutor/", function (data, status) {
        var listado = [];
        console.log(data);
        for (var i = 0; i <= data.length - 1; i++) {
            var fila = [];
            fila.push(i + 1);
            fila.push(data[i].nombre);
            if (data[i].comercial == true) {
                fila.push("<i class='glyphicon glyphicon-check'></i>");
            } else {
                fila.push("<i class='glyphicon glyphicon-unchecked'></i>");
            }

            if (data[i].id == 'fd975710-caae-49e3-8c9e-7a2ddc0ac948' || data[i].id == 'fd975710-caae-49e3-8c9e-7a2ddc0ac958') {
                fila.push("<button class='btn btn-primary' title='Editar' onclick=\"Editar_Tipo_Int('" + data[i].id + "','" + data[i].nombre + "','" + data[i].comercial + "');\"><i class='fa fa-edit'></i></button> ");
            } else {
                fila.push("<button class='btn btn-primary' title='Editar' onclick=\"Editar_Tipo_Int('" + data[i].id + "','" + data[i].nombre + "','" + data[i].comercial + "');\"><i class='fa fa-edit'></i></button> " +
                    "<button class='btn btn-danger' title='Eliminar' onclick=\"DeleteTipoInt('" + data[i].id + "');\"><i class='fa fa-trash'></i></button> ");
            }
          
            listado.push(fila);
        }

        var table = $('#tbl_tipo_interlocutor').DataTable();

        table.destroy();

        $('#tbl_tipo_interlocutor').DataTable({
            data: listado,
            columns: [
                { title: "No." },
                { title: "Tipo de interlocutor" },
                { title: "Comercial" },
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

function Abrir_Modal_Tipo_interlocutor() {
    $('.clear_txt').val('');
    $("#check_es_comercial_add").prop("checked", false);

    $('#Modal_Tipo_interlocutor').modal({ backdrop: 'static', keyboard: false });
    $('#Modal_Tipo_interlocutor').modal('show');
}

function Cerrar_Modal_Tipo_interlocutor() {
    $('#Modal_Tipo_interlocutor').modal('hide');
    $('.clear_txt').val('');
}

$("#Add_TipoInterlocutor").click(function () {
    Abrir_Modal_Tipo_interlocutor();
})

$("#btn_guardar_tipo_int").click(function () {
    Add_Tipo_interlocutor();
})

function Add_Tipo_interlocutor() {

    if ($("#txt_tipo_interlocutor_add").val() == "") {
        return ErrorSA('Error en los datos de entrada', 'Debe ingresar un tipo de interlocutor.');
    }
    tbl_tipo_interlocutor.nombre = $("#txt_tipo_interlocutor_add").val();
    if ($('#check_es_comercial_add').is(":checked") == false) {
        tbl_tipo_interlocutor.comercial = 0;
    } else {
        tbl_tipo_interlocutor.comercial = 1;
    }

    //debugger;
    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(tbl_tipo_interlocutor),
        type: 'post',

        success: function (data) {
            var objresponse = JSON.parse(data);
            console.log(objresponse[0].cod)
            if (objresponse[0].cod == "success") {
                function si() {
                    return Cerrar_Modal_Tipo_interlocutor();
                }
                var AccionSi = eval(si);
                SuccessSAAction("Operación exitosa", "El registro se guardado correctamente", AccionSi);
                Get_lista_tipo_interlocutor();

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
        url: $("#EndPointAdmon").val() + "Catalogos/Add/TInterlocutor"

    })
}

function Editar_Tipo_Int(id, tipo_doc, comercial) {
    $('.clear_txt_upd').val("");
    $("#id_tipo_int").val("");
    $("#check_es_comercial_upd").prop("checked", false);
    $('#Modal_Tipo_interlocutor_Upd').modal({ backdrop: 'static', keyboard: false });
    $('#Modal_Tipo_interlocutor_Upd').modal('show');
    $("#id_tipo_int").val(id);
    $("#txt_tipo_interlocutor_upd").val(tipo_doc);
    if (comercial == "true") {
        $("#check_es_comercial_upd").prop("checked", true);
    }
}

function DeleteTipoInt(item) {
    function Confirmacion_() {
        return eliminar_tipo_int(item);
    }
    var AccionSi = eval(Confirmacion_);
    WarningSA('', '¿En verdad desea eliminar el registro?', 'Si, Continuar', 'No, Cancelar', AccionSi, '');
}

function eliminar_tipo_int(item) {
    $.ajax({
        url: $("#EndPointAdmon").val() + "Catalogos/Delete/TInterlocutor/" + item,
        dataType: 'text',
        cache: false,
        contentType: false,
        processData: false,
        type: "DELETE",
        success: function (data) {
            var objresponse = JSON.parse(data);
            if (objresponse[0].cod == "success") {
                SuccessSA('', 'El registro se eliminó exitosamente');
                Get_lista_tipo_interlocutor();
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

var tbl_tipo_interlocutor = {
    p_id: "00000000-0000-0000-0000-000000000000",
    nombre: "",
    comercial: ""
}