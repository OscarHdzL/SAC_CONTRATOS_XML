$(document).ready(function () {
    $("#tbl_penalizaciones").DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" },
            { "width": "15%", "targets": 6 }
        ],
    });
    Get_lista_penalizaciones();
})

$('#txt_monto_garantia').keyup(function (event) {
    if (event.which >= 37 && event.which <= 40) {
        event.preventDefault();
    }
    $(this).val(function (index, value) {
        return value
            .replace(/\D/g, "")
            .replace(/([0-9])([0-9]{2})$/, '$1.$2')
            .replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ",")
            ;
    });
});

function Validar_pen() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#input_Penalizacion').val() == '') {
        Response.Texto = 'Debe ingresar una penalización.';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadenab($('#input_Penalizacion').val(), '') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Penalización"';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_porcentaje_pena').val() == '') {
        Response.Texto = 'Debe ingresar un % de penalización.';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_porcentaje_deduc').val() == '') {
        Response.Texto = 'Debe ingresar un % de deductiva.';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_porcentaje_garantia').val() == '') {
        Response.Texto = 'Debe ingresar un % de garantía.';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_monto_garantia').val() == '') {
        Response.Texto = 'Debe ingresar un monto de garantía';
        Response.Bit = true;
        return Response;
    }
    Response.Texto = '';
    Response.Bit = false;

    return Response;
}

function Add_conv_penalizacion() {
    var Validacion = Validar_pen();

    if (Validacion.Bit) {
        return ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }

    var tbl_con_pen = tbl_convocatoria_penalizacion;

    tbl_con_pen.p_tbl_convocatoria_id = $('#id_convocatoria').val();
    tbl_con_pen.p_penalizacion = $('#input_Penalizacion').val();
    tbl_con_pen.p_porcentaje_penalizacion = $('#txt_porcentaje_pena').val();
    tbl_con_pen.p_porcentaje_deductiva = $('#txt_porcentaje_deduc').val();
    tbl_con_pen.p_porcentaje_garantia = $('#txt_porcentaje_garantia').val();
    tbl_con_pen.p_monto_garantia = $('#txt_monto_garantia').val().replace(/,/g, "");

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(tbl_con_pen),
        type: 'post',

        success: function (data) {
            var objresponse = JSON.parse(data);
            if (objresponse.cod == "success") {
                SuccessSA("Operación exitosa", "El registro se guardado correctamente");
                Get_lista_penalizaciones();
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
        url: $("#EndPointAQ").val() + "Convocatoria/Add/Penalizacion"
    })
}

$('.btn_Add_Pena').click(function () {
    Add_conv_penalizacion();
})

function Get_lista_penalizaciones() {
    var id_solicitud = $("#_SOLICITUD").val();
    $.get($("#EndPointAQ").val() + "Convocatoria/Get/Penalizacion/" + id_solicitud, function (data, status) {
        var listado = [];
        for (var i = 0; i <= data.length - 1; i++) {
            var fila = [];
            fila.push(data[i].folio);
            fila.push(data[i].penalizacion);
            fila.push(data[i].porcentaje_penalizacion + ' ' + '%');
            fila.push(data[i].porcentaje_deductiva + ' ' + '%');
            fila.push(data[i].porcentaje_garantia + ' ' + '%');
            fila.push('$' + ' ' + data[i].monto_garantia.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,'));
            fila.push("<button class='btn btn-primary' title='Editar' onclick=\"EditarPen('" + data[i].id + "','" + data[i].penalizacion + "','" + data[i].porcentaje_penalizacion + "','" + data[i].porcentaje_deductiva + "','" + data[i].porcentaje_garantia + "','" + data[i].monto_garantia.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,') +"');\"><i class='fa fa-edit'></i></button> " +
                "<button class='btn btn-danger' title='Eliminar' onclick=\"DeletePen('" + data[i].id + "');\"> <i class='fa fa-trash'></i></button> ");
            listado.push(fila);
        }

        var table = $('#tbl_penalizaciones').DataTable();

        table.destroy();

        $('#tbl_penalizaciones').DataTable({
            data: listado,
            columns: [
                { title: "Folio conv." },
                { title: "Penalización" },
                { title: "% de penalizacion" },
                { title: "% de deductiva" },
                { title: "% de garantia" },
                { title: "Monto de garantía" },
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

function EditarPen(id, pena, por_pena, por_ded, por_ga, mon_ga) {
    $('.clr').val('');  
    $('#id_conv_pena').val(id);
    $('#input_Penalizacion_upd').val(pena);
    $('#txt_porcentaje_pena_upd').val(por_pena);
    $('#txt_porcentaje_deduc_upd').val(por_ded);
    $('#txt_porcentaje_garantia_upd').val(por_ga);
    $('#txt_monto_garantia_upd').val(mon_ga);
    $('#Modal_Penalizaciones_Upd').modal({ backdrop: 'static', keyboard: false });
    $('#Modal_Penalizaciones_Upd').modal('show');
}

function cerrar_modal() {
    $('#Modal_Penalizaciones_Upd').modal('hide');
}

function Validar_upd() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#input_Penalizacion_upd').val() == '') {
        Response.Texto = 'Debe ingresar una penalización.';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadenab($('#input_Penalizacion_upd').val(), '') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Penalización"';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_porcentaje_pena_upd').val() == '') {
        Response.Texto = 'Debe ingresar un % de penalización.';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_porcentaje_deduc_upd').val() == '') {
        Response.Texto = 'Debe ingresar un % de deductiva.';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_porcentaje_garantia_upd').val() == '') {
        Response.Texto = 'Debe ingresar un % de garantía.';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_monto_garantia_upd').val() == '') {
        Response.Texto = 'Debe ingresar un monto de garantía';
        Response.Bit = true;
        return Response;
    }
    Response.Texto = '';
    Response.Bit = false;

    return Response;
}

function Upd_conv_penalizacion() {
    var Validacion = Validar_upd();

    if (Validacion.Bit) {
        return ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }

    var tbl_con_pen_upd = tbl_convocatoria_penalizacion;
    tbl_con_pen_upd.p_id = $('#id_conv_pena').val();
    tbl_con_pen_upd.p_tbl_convocatoria_id = $('#id_convocatoria').val();
    tbl_con_pen_upd.p_penalizacion = $('#input_Penalizacion_upd').val();
    tbl_con_pen_upd.p_porcentaje_penalizacion = $('#txt_porcentaje_pena_upd').val();
    tbl_con_pen_upd.p_porcentaje_deductiva = $('#txt_porcentaje_deduc_upd').val();
    tbl_con_pen_upd.p_porcentaje_garantia = $('#txt_porcentaje_garantia_upd').val();
    tbl_con_pen_upd.p_monto_garantia = $('#txt_monto_garantia_upd').val().replace(/,/g, "");

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(tbl_con_pen_upd),
        type: 'put',

        success: function (data) {
            var objresponse = JSON.parse(data);
            if (objresponse.cod == "success") {
                function conf() {
                    return cerrar_modal();
                }
                var si = eval(conf);
                SuccessSAAction("Operación exitosa", "El registro se guardado correctamente", si);
                Get_lista_penalizaciones();
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
        url: $("#EndPointAQ").val() + "Convocatoria/Update/Penalizacion"
    })
}

$('#btn_upd_pena').click(function () {
    Upd_conv_penalizacion();
})

function DeletePen(item) {
    function Confirmacion_() {
        return eliminar_penc(item);
    }
    var AccionSi = eval(Confirmacion_);
    WarningSA('', '¿En verdad desea eliminar el registro?', 'Si, Continuar', 'No, Cancelar', AccionSi, '');
}

function eliminar_penc(item) {
    $.ajax({
        url: $("#EndPointAQ").val() + "Convocatoria/Delete/Penalizacion/" + item,
        dataType: 'text',
        cache: false,
        contentType: false,
        processData: false,
        type: "DELETE",
        success: function (data) {
            var objresponse = JSON.parse(data);
            if (objresponse.cod == "success") {
                SuccessSA('', 'El registro se eliminó exitosamente');
                Get_lista_penalizaciones();
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

var tbl_convocatoria_penalizacion = {
	p_opt: 0,
	p_id: "00000000-0000-0000-0000-000000000000",
	p_tbl_convocatoria_id: "00000000-0000-0000-0000-000000000000",
    p_tbl_estatus_obligacion_id: "6e858887-6232-11ea-8324-00155d1b3502",
	p_penalizacion: "",
	p_porcentaje_penalizacion: 0,
	p_porcentaje_deductiva: 0,
	p_porcentaje_garantia: 0,
	p_monto_garantia: ""
}