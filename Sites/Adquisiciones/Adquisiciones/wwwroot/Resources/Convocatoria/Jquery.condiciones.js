$(document).ready(function () {
    $("#tbl_condiciones").DataTable();
    Get_lista_condiciones();
})

function Validar_con() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#input_condicion').val() == '') {
        Response.Texto = 'Debe ingresar una condición.';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadenab($('#input_condicion').val(), '') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Condición"';
        Response.Bit = true;
        return Response;
    }
    if ($('#cmb_condicion').val() == '') {
        Response.Texto = 'Debe selecciona una aopción.';
        Response.Bit = true;
        return Response;
    }
    Response.Texto = '';
    Response.Bit = false;

    return Response;
}

function Add_conv_condicion() {
    var Validacion = Validar_con();

    if (Validacion.Bit) {
        return ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }

    var tbl_con_condicion = tbl_convocatoria_condicion;

    tbl_con_condicion.p_tbl_convocatoria_id = $('#id_convocatoria').val();
    tbl_con_condicion.p_condicion = $('#input_condicion').val();
    tbl_con_condicion.p_periodo = $('#cmb_condicion').val();

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(tbl_con_condicion),
        type: 'post',

        success: function (data) {
            var objresponse = JSON.parse(data);
            if (objresponse.cod == "success") {
                SuccessSA("Operación exitosa", "El registro se guardado correctamente");
                Get_lista_condiciones();
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
        url: $("#EndPointAQ").val() + "Convocatoria/Add/Condicion"
    })
}

$('.btn_Add_Condiciones').click(function () {
    Add_conv_condicion();
})

function Get_lista_condiciones() {
    var id_solicitud = $("#_SOLICITUD").val();
    $.get($("#EndPointAQ").val() + "Convocatoria/Get/Condicion/" + id_solicitud, function (data, status) {
        var listado = [];
        for (var i = 0; i <= data.length - 1; i++) {
            var fila = [];
            fila.push(i + 1);
            fila.push(data[i].folio);
            fila.push(data[i].periodo);
            fila.push(data[i].condicion);
            fila.push("<button class='btn btn-primary' title='Editar' onclick=\"EditarCon('" + data[i].id + "','" + data[i].condicion + "','" + data[i].periodo + "');\"><i class='fa fa-edit'></i></button> " +
                "<button class='btn btn-danger' title='Eliminar' onclick=\"DeleteCon('" + data[i].id + "');\"> <i class='fa fa-trash'></i></button> ");
            listado.push(fila);
        }

        var table = $('#tbl_condiciones').DataTable();

        table.destroy();

        $('#tbl_condiciones').DataTable({
            data: listado,
            columns: [
                { title: "No." },
                { title: "Folio conv." },
                { title: "Periodo" },
                { title: "Condicion" },
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

function EditarCon(id, condicion, periodo) {
    $('.clr').val('');
    $('#id_conv_cond').val(id);
    $('#input_condicion_upd').val(condicion);
    $('#cmb_condicion_upd').val(periodo);
    $('#Modal_Condicion_Upd').modal({ backdrop: 'static', keyboard: false });
    $('#Modal_Condicion_Upd').modal('show');
}

function cerrar_modal_con() {
    $('#Modal_Condicion_Upd').modal('hide');
}

function Validar_con_upd() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#input_condicion_upd').val() == '') {
        Response.Texto = 'Debe ingresar una condición.';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadenab($('#input_condicion_upd').val(), '') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Condición"';
        Response.Bit = true;
        return Response;
    }
    if ($('#cmb_condicion_upd').val() == '') {
        Response.Texto = 'Debe selecciona una aopción.';
        Response.Bit = true;
        return Response;
    }
    Response.Texto = '';
    Response.Bit = false;

    return Response;
}

function Update_conv_condicion() {
    var Validacion = Validar_con_upd();

    if (Validacion.Bit) {
        return ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }

    var tbl_con_condicion_upd = tbl_convocatoria_condicion;

    tbl_con_condicion_upd.p_id = $('#id_conv_cond').val();
    tbl_con_condicion_upd.p_tbl_convocatoria_id = $('#id_convocatoria').val();
    tbl_con_condicion_upd.p_condicion = $('#input_condicion_upd').val();
    tbl_con_condicion_upd.p_periodo = $('#cmb_condicion_upd').val();

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(tbl_con_condicion_upd),
        type: 'put',

        success: function (data) {
            var objresponse = JSON.parse(data);
            //console.log(objresponse.cod)
            if (objresponse.cod == "success") {
                function conf() {
                    return cerrar_modal_con();
                }
                var si = eval(conf);
                SuccessSAAction("Operación exitosa", "El registro se guardado correctamente", si);
                Get_lista_condiciones();
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
        url: $("#EndPointAQ").val() + "Convocatoria/Update/Condicion"
    })
}

$('#btn_upd_cond').click(function () {
    Update_conv_condicion();
})

function DeleteCon(item) {
    function Confirmacion_() {
        return eliminar_cond(item);
    }
    var AccionSi = eval(Confirmacion_);
    WarningSA('', '¿En verdad desea eliminar el registro?', 'Si, Continuar', 'No, Cancelar', AccionSi, '');
}

function eliminar_cond(item) {
    $.ajax({
        url: $("#EndPointAQ").val() + "Convocatoria/Delete/Condicion/" + item,
        dataType: 'text',
        cache: false,
        contentType: false,
        processData: false,
        type: "DELETE",
        success: function (data) {
            var objresponse = JSON.parse(data);
            if (objresponse.cod == "success") {
                SuccessSA('', 'El registro se eliminó exitosamente');
                Get_lista_condiciones();
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

var tbl_convocatoria_condicion = {
    p_opt: 0,
    p_id: "00000000-0000-0000-0000-000000000000",
    p_tbl_convocatoria_id: "00000000-0000-0000-0000-000000000000",
    p_tbl_estatus_obligacion_id: "6e858887-6232-11ea-8324-00155d1b3502",
    p_periodo: "",
    p_condicion: ""
}