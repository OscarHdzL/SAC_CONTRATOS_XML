$(document).ready(function () {
    $("#tbl_Criterio").DataTable();
    get_tipo_criterio();
    Get_lista_criterios();
})

function get_tipo_criterio() {
    $.get($("#EndPointAQ").val() + "Convocatoria/Get/TipoCriterio", function (data, status) {
        var Body = "<option value='' disabled selected>Seleccione una opción</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            Body = Body + "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
        }
        $('#cmb_criterioseval, #cmb_criterioseval_upd').html(Body);
    }, 'json');
}

function Get_lista_criterios() {
    var id_solicitud = $("#_SOLICITUD").val();
    $.get($("#EndPointAQ").val() + "Convocatoria/Get/Criterio/" + id_solicitud, function (data, status) {
        var listado = [];
        for (var i = 0; i <= data.length - 1; i++) {
            var fila = [];
            fila.push(i + 1);
            fila.push(data[i].folio);
            fila.push(data[i].tipo_criterio);
            fila.push(data[i].criterio);
            fila.push(data[i].evaluacion);
            fila.push("<button class='btn btn-primary' title='Editar' onclick=\"EditarCriterio('" + data[i].id + "','" + data[i].tbl_tipocriterio_id + "','" + data[i].criterio + "','" + data[i].evaluacion + "');\"><i class='fa fa-edit'></i></button> " +
                "<button class='btn btn-danger' title='Eliminar' onclick=\"DeleteCriterio('" + data[i].id + "');\"> <i class='fa fa-trash'></i></button> ");
            listado.push(fila);
        }

        var table = $('#tbl_Criterio').DataTable();

        table.destroy();

        $('#tbl_Criterio').DataTable({
            data: listado,
            columns: [
                { title: "No." },
                { title: "Folio conv." },
                { title: "Tipo criterio." },
                { title: "Criterio eval." },
                { title: "Evaluacion." },
                { title: "Acciones" }
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

$('.btn_add_criterio_btn').click(function () {
    Add_conv_criterio();
})

function Validar_criterio() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#cmb_criterioseval').val() == '' || $('#cmb_criterioseval').val() == null) {
        Response.Texto = 'Debe seleccionar un tipo de evaluación.';
        Response.Bit = true;
        return Response;
    }
    if ($('#input_criterioseval').val() == '') {
        Response.Texto = 'Debe ingresar un criterio de evaluación.';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadenab($('#input_criterioseval').val(), '') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Criterio de evaluación"';
        Response.Bit = true;
        return Response;
    }
    if ($('#input_Evaluacion').val() == '') {
        Response.Texto = 'Debe ingresar una evaluación.';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadenab($('#input_Evaluacion').val(), '') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Evaluación"';
        Response.Bit = true;
        return Response;
    }
    Response.Texto = '';
    Response.Bit = false;

    return Response;
}

function Add_conv_criterio() {
    var Validacion = Validar_criterio();

    if (Validacion.Bit) {
        return ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }

    var tbl_con_cri = tbl_convocatoria_criterio;

    tbl_con_cri.p_tbl_convocatoria_id = $('#id_convocatoria').val();
    tbl_con_cri.p_tbl_tipocriterio_id = $('#cmb_criterioseval').val();
    tbl_con_cri.p_criterio = $('#input_criterioseval').val();
    tbl_con_cri.p_evaluacion = $('#input_Evaluacion').val();


    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(tbl_con_cri),
        type: 'post',

        success: function (data) {
            var objresponse = JSON.parse(data);
            if (objresponse.cod == "success") {
                SuccessSA("Operación exitosa", "El registro se guardado correctamente");
                Get_lista_criterios();
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
        url: $("#EndPointAQ").val() + "Convocatoria/Add/Criterio"
    })
}

function EditarCriterio(id, tipo_c, criterio, eval) {
    $('.clr').val('');
    $('#id_conv_criterio').val(id);

    $('#cmb_criterioseval_upd').val(tipo_c);
    $('#input_criterioseval_upd').val(criterio);
    $('#input_Evaluacion_upd').val(eval);

    $('#Modal_Criterio_Upd').modal({ backdrop: 'static', keyboard: false });
    $('#Modal_Criterio_Upd').modal('show');
}

function cerrar_modal_criterio() {
    $('#Modal_Criterio_Upd').modal('hide');
}

function Validar_criterio_upd() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#cmb_criterioseval_upd').val() == '' || $('#cmb_criterioseval_upd').val() == null) {
        Response.Texto = 'Debe seleccionar un tipo de evaluación.';
        Response.Bit = true;
        return Response;
    }
    if ($('#input_criterioseval_upd').val() == '') {
        Response.Texto = 'Debe ingresar un criterio de evaluación.';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadenab($('#input_criterioseval_upd').val(), '') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Criterio de evaluación"';
        Response.Bit = true;
        return Response;
    }
    if ($('#input_Evaluacion_upd').val() == '') {
        Response.Texto = 'Debe ingresar una evaluación.';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadenab($('#input_Evaluacion_upd').val(), '') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Evaluación"';
        Response.Bit = true;
        return Response;
    }
    Response.Texto = '';
    Response.Bit = false;

    return Response;
}

function Update_conv_criterio() {
    var Validacion = Validar_criterio_upd();

    if (Validacion.Bit) {
        return ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }

    var tbl_con_cri_upd = tbl_convocatoria_criterio;
    tbl_con_cri_upd.p_id = $('#id_conv_criterio').val();
    tbl_con_cri_upd.p_tbl_convocatoria_id = $('#id_convocatoria').val();
    tbl_con_cri_upd.p_tbl_tipocriterio_id = $('#cmb_criterioseval_upd').val();
    tbl_con_cri_upd.p_criterio = $('#input_criterioseval_upd').val();
    tbl_con_cri_upd.p_evaluacion = $('#input_Evaluacion_upd').val();


    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(tbl_con_cri_upd),
        type: 'put',

        success: function (data) {
            var objresponse = JSON.parse(data);
            if (objresponse.cod == "success") {
                function _confirmar() {
                    return cerrar_modal_criterio();
                }
                var si = eval(_confirmar);
                SuccessSAAction("Operación exitosa", "El registro se guardado correctamente", si);
                Get_lista_criterios();
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
        url: $("#EndPointAQ").val() + "Convocatoria/Update/Criterio"
    })
}

$('#btn_upd_criterio').click(function () {
    Update_conv_criterio();
})

function DeleteCriterio(item) {
    function Confirmacion_() {
        return eliminar_criterio(item);
    }
    var AccionSi = eval(Confirmacion_);
    WarningSA('', '¿En verdad desea eliminar el registro?', 'Si, Continuar', 'No, Cancelar', AccionSi, '');
}

function eliminar_criterio(item) {
    $.ajax({
        url: $("#EndPointAQ").val() + "Convocatoria/Delete/Criterio/" + item,
        dataType: 'text',
        cache: false,
        contentType: false,
        processData: false,
        type: "DELETE",
        success: function (data) {
            var objresponse = JSON.parse(data);
            if (objresponse.cod == "success") {
                SuccessSA('', 'El registro se eliminó exitosamente');
                Get_lista_criterios();
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

var tbl_convocatoria_criterio = {
    p_opt: 0,
    p_id: "00000000-0000-0000-0000-000000000000",
    p_tbl_convocatoria_id: "00000000-0000-0000-0000-000000000000",
    p_tbl_tipocriterio_id: "00000000-0000-0000-0000-000000000000",
    p_tbl_estatus_obligacion_id: "6e858887-6232-11ea-8324-00155d1b3502",
    p_criterio: "",
    p_evaluacion: ""
}