$(document).ready(function () {
    $("#tbl_conv_pago").DataTable();
    Get_lista_pagos();
})

$('.btp_Cpago_save').click(function () {
    Add_conv_pago();
})

function Validar_pago() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#input_Cpago').val() == '') {
        Response.Texto = 'Debe ingresar una condición de pago.';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadenab($('#input_Cpago').val(), '') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Condición de pago"';
        Response.Bit = true;
        return Response;
    }
    if ($('#cmb_tipopagoCpago').val() == '' || $('#cmb_tipopagoCpago').val() == null) {
        Response.Texto = 'Debe seleccionar un tipo de pago.';
        Response.Bit = true;
        return Response;
    }
    if ($('#cmb_esquemaCpago').val() == '' || $('#cmb_esquemaCpago').val() == null) {
        Response.Texto = 'Debe seleccionar un esquema de pago.';
        Response.Bit = true;
        return Response;
    }
    if ($('#input_MetodoCpago').val() == '') {
        Response.Texto = 'Debe ingresar un método de pago.';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadenab($('#input_MetodoCpago').val(), '') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Método de pago"';
        Response.Bit = true;
        return Response;
    }
    Response.Texto = '';
    Response.Bit = false;

    return Response;
}

function Add_conv_pago() {
    var Validacion = Validar_pago();

    if (Validacion.Bit) {
        return ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }

    var tbl_con_pago = tbl_convocatoria_pago;

    tbl_con_pago.p_tbl_convocatoria_id = $('#id_convocatoria').val();
    tbl_con_pago.p_condicion_pago = $('#input_Cpago').val();
    tbl_con_pago.p_tipo_pago = $('#cmb_tipopagoCpago').val();
    tbl_con_pago.p_porcentaje_cantidad = $('#cmb_esquemaCpago').val();
    tbl_con_pago.p_metodo_pago = $('#input_MetodoCpago').val();

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(tbl_con_pago),
        type: 'post',

        success: function (data) {
            var objresponse = JSON.parse(data);
            if (objresponse.cod == "success") {
                SuccessSA("Operación exitosa", "El registro se guardado correctamente");
                Get_lista_pagos();
                $('.clr').val('');
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
        url: $("#EndPointAQ").val() + "Convocatoria/Add/Pago"
    })
}

function Get_lista_pagos() {
    var id_solicitud = $("#_SOLICITUD").val();
    $.get($("#EndPointAQ").val() + "Convocatoria/Get/Pago/" + id_solicitud, function (data, status) {
        var listado = [];
        for (var i = 0; i <= data.length - 1; i++) {
            var fila = [];
            fila.push(i + 1);
            fila.push(data[i].folio);
            fila.push(data[i].condicion_pago);
            fila.push(data[i].metodo_pago);
            fila.push(data[i].tipo_pago);
            fila.push(data[i].porcentaje_cantidad);
            fila.push("<button class='btn btn-primary' title='Editar' onclick=\"EditarPago('" + data[i].id + "','" + data[i].condicion_pago + "','" + data[i].metodo_pago + "','" + data[i].tipo_pago + "','" + data[i].porcentaje_cantidad + "');\"><i class='fa fa-edit'></i></button> " +
                "<button class='btn btn-danger' title='Eliminar' onclick=\"DeletePago('" + data[i].id + "');\"> <i class='fa fa-trash'></i></button> ");
            listado.push(fila);
        }

        var table = $('#tbl_conv_pago').DataTable();

        table.destroy();

        $('#tbl_conv_pago').DataTable({
            data: listado,
            columns: [
                { title: "No." },
                { title: "Folio conv." },
                { title: "Condición pago." },
                { title: "Metodo pago." },
                { title: "Tipo pago." },
                { title: "Esquema pago." },
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
    });
}

function EditarPago(id, condicion, metodo, tipo, esquema) {
    $('.clr').val('');
    $('#id_conv_pago').val(id);

    $('#input_Cpago_upd').val(condicion);
    $('#cmb_tipopagoCpago_upd').val(tipo);
    $('#cmb_esquemaCpago_upd').val(esquema);
    $('#input_MetodoCpago_upd').val(metodo);

    $('#Modal_Pago_Upd').modal({ backdrop: 'static', keyboard: false });
    $('#Modal_Pago_Upd').modal('show');
}

function cerrar_modal_pago() {
    $('#Modal_Pago_Upd').modal('hide');
}

function Validar_pago_upd() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#input_Cpago_upd').val() == '') {
        Response.Texto = 'Debe ingresar una condición de pago.';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadenab($('#input_Cpago_upd').val(), '') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Condición de pago"';
        Response.Bit = true;
        return Response;
    }
    if ($('#cmb_tipopagoCpago_upd').val() == '' || $('#cmb_tipopagoCpago_upd').val() == null) {
        Response.Texto = 'Debe seleccionar un tipo de pago.';
        Response.Bit = true;
        return Response;
    }
    if ($('#cmb_esquemaCpago_upd').val() == '' || $('#cmb_esquemaCpago_upd').val() == null) {
        Response.Texto = 'Debe seleccionar un esquema de pago.';
        Response.Bit = true;
        return Response;
    }
    if ($('#input_MetodoCpago_upd').val() == '') {
        Response.Texto = 'Debe ingresar un método de pago.';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadenab($('#input_MetodoCpago_upd').val(), '') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Método de pago"';
        Response.Bit = true;
        return Response;
    }
    Response.Texto = '';
    Response.Bit = false;

    return Response;
}

function Update_conv_pago() {
    var Validacion = Validar_pago_upd();

    if (Validacion.Bit) {
        return ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }

    var tbl_con_pago_upd = tbl_convocatoria_pago;

    tbl_con_pago_upd.p_id = $('#id_conv_pago').val();
    tbl_con_pago_upd.p_tbl_convocatoria_id = $('#id_convocatoria').val();
    tbl_con_pago_upd.p_condicion_pago = $('#input_Cpago_upd').val();
    tbl_con_pago_upd.p_tipo_pago = $('#cmb_tipopagoCpago_upd').val();
    tbl_con_pago_upd.p_porcentaje_cantidad = $('#cmb_esquemaCpago_upd').val();
    tbl_con_pago_upd.p_metodo_pago = $('#input_MetodoCpago_upd').val();

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(tbl_con_pago_upd),
        type: 'put',

        success: function (data) {
            var objresponse = JSON.parse(data);
            if (objresponse.cod == "success") {
                function _confirmar() {
                    return cerrar_modal_pago();
                }
                var si = eval(_confirmar);
                SuccessSAAction("Operación exitosa", "El registro se guardado correctamente", si);
                Get_lista_pagos();
                $('.clr').val('');
            }
            else {
                ErrorSA("", "Ocurrio un error.");
            }
        },
        error: function () {
            ErrorSA('', "Ocurrio un error.")
        },
        processData: false,
        type: 'PUT',
        url: $("#EndPointAQ").val() + "Convocatoria/Update/Pago"
    })
}

$('#btn_upd_pago').click(function () {
    Update_conv_pago();
})

function DeletePago(item) {
    function Confirmacion_() {
        return eliminar_pago(item);
    }
    var AccionSi = eval(Confirmacion_);
    WarningSA('', '¿En verdad desea eliminar el registro?', 'Si, Continuar', 'No, Cancelar', AccionSi, '');
}

function eliminar_pago(item) {
    $.ajax({
        url: $("#EndPointAQ").val() + "Convocatoria/Delete/Pago/" + item,
        dataType: 'text',
        cache: false,
        contentType: false,
        processData: false,
        type: "DELETE",
        success: function (data) {
            var objresponse = JSON.parse(data);
            if (objresponse.cod == "success") {
                SuccessSA('', 'El registro se eliminó exitosamente');
                Get_lista_pagos();
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

var tbl_convocatoria_pago = {
    p_opt: 0,
    p_id: "00000000-0000-0000-0000-000000000000",
    p_tbl_convocatoria_id: "00000000-0000-0000-0000-000000000000",
    p_tbl_estatus_obligacion_id: "6e858887-6232-11ea-8324-00155d1b3502",
    p_condicion_pago: "",
    p_metodo_pago: "",
    p_tipo_pago: "",
    p_porcentaje_cantidad: ""
}