$(document).ready(function () {
    $("#tbl_docval").DataTable();
    get_tipo_doc();
    Get_lista_documentos();
})

function get_tipo_doc() {
    $.get($("#EndPointAQ").val() + "Convocatoria/Get/TipoDoc/" + $('#HDidInstancia').val(), function (data, status) {
        var Body = "<option value='' disabled selected>Seleccione una opción</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            Body = Body + "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
        }
        $('#cmb_docval, #cmb_docval_upd').html(Body);
    }, 'json');
}

function Get_lista_documentos() {
    var id_solicitud = $("#_SOLICITUD").val();
    $.get($("#EndPointAQ").val() + "Convocatoria/Get/Documento/" + id_solicitud, function (data, status) {
        var listado = [];
        for (var i = 0; i <= data.length - 1; i++) {
            var fila = [];
            fila.push(i + 1);
            fila.push(data[i].folio);
            fila.push(data[i].tipo_documento);
            fila.push(data[i].justificacion);
            fila.push("<button class='btn btn-primary' title='Editar' onclick=\"EditarDoc_('" + data[i].id + "','" + data[i].tbl_tipo_documento_id + "','" + data[i].justificacion + "');\"><i class='fa fa-edit'></i></button> " +
                "<button class='btn btn-danger' title='Eliminar' onclick=\"DeleteDoc_('" + data[i].id + "');\"> <i class='fa fa-trash'></i></button> ");
            listado.push(fila);
        }

        var table = $('#tbl_docval').DataTable();

        table.destroy();

        $('#tbl_docval').DataTable({
            data: listado,
            columns: [
                { title: "No." },
                { title: "Folio conv." },
                { title: "Tipo de doc." },
                { title: "Justificación." },
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

$('.btn_Add_DocVal').click(function () {
    Add_conv_documento();
})

function Validar_doc_() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#cmb_docval').val() == null) {
        Response.Texto = 'Debe seleccionar un tipo de doc.';
        Response.Bit = true;
        return Response;
    }
    if ($('#input_docval').val() == '') {
        Response.Texto = 'Debe ingresar una justificación.';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadenab($('#input_docval').val(), '') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Justificación"';
        Response.Bit = true;
        return Response;
    }
    Response.Texto = '';
    Response.Bit = false;

    return Response;
}

function Add_conv_documento() {
    var Validacion = Validar_doc_();

    if (Validacion.Bit) {
        return ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }

    var tbl_con_doc = tbl_convocatoria_doc;

    tbl_con_doc.p_tbl_convocatoria_id = $('#id_convocatoria').val();
    tbl_con_doc.p_tbl_tipo_documento_id = $('#cmb_docval').val();
    tbl_con_doc.p_justificacion = $('#input_docval').val();
    console.log(JSON.stringify(tbl_con_doc));
    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(tbl_con_doc),
        type: 'post',

        success: function (data) {
            var objresponse = JSON.parse(data);
            console.log(objresponse.cod)
            if (objresponse.cod == "success") {
                SuccessSA("Operación exitosa", "El registro se guardado correctamente");
                Get_lista_documentos();
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
        url: $("#EndPointAQ").val() + "Convocatoria/Add/Documento"
    })
}

function EditarDoc_(id, tipo_doc, justificacion) {
    $('.clr').val('');
    $('#id_conv_doc').val(id);

    $('#cmb_docval_upd').val(tipo_doc);
    $('#input_docval_upd').val(justificacion);

    $('#Modal_Doc_Upd').modal({ backdrop: 'static', keyboard: false });
    $('#Modal_Doc_Upd').modal('show');
}

function cerrar_modal_doc() {
    $('#Modal_Doc_Upd').modal('hide');
}

function Validar_doc_upd() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#cmb_docval_upd').val() == null) {
        Response.Texto = 'Debe seleccionar un tipo de doc.';
        Response.Bit = true;
        return Response;
    }
    if ($('#input_docval_upd').val() == '') {
        Response.Texto = 'Debe ingresar una justificación.';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadenab($('#input_docval_upd').val(), '') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Justificación"';
        Response.Bit = true;
        return Response;
    }
    Response.Texto = '';
    Response.Bit = false;

    return Response;
}

function Update_conv_doc() {
    var Validacion = Validar_doc_upd();

    if (Validacion.Bit) {
        return ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }
    var tbl_con_doc_upd = tbl_convocatoria_doc;

    tbl_con_doc_upd.p_id = $('#id_conv_doc').val();
    tbl_con_doc_upd.p_tbl_convocatoria_id = $('#id_convocatoria').val();
    tbl_con_doc_upd.p_tbl_tipo_documento_id = $('#cmb_docval_upd').val();
    tbl_con_doc_upd.p_justificacion = $('#input_docval_upd').val();


    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(tbl_con_doc_upd),
        type: 'put',

        success: function (data) {
            var objresponse = JSON.parse(data);
            console.log(objresponse.cod)
            if (objresponse.cod == "success") {
                function _confirmar() {
                    return cerrar_modal_doc();
                }
                var si = eval(_confirmar);
                SuccessSAAction("Operación exitosa", "El registro se guardado correctamente", si);
                Get_lista_documentos();
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
        url: $("#EndPointAQ").val() + "Convocatoria/Update/Documento"
    })
}

$('#btn_upd_doc').click(function () {
    Update_conv_doc();
})

function DeleteDoc_(item) {
    function Confirmacion_() {
        return eliminar_doc(item);
    }
    var AccionSi = eval(Confirmacion_);
    WarningSA('', '¿En verdad desea eliminar el registro?', 'Si, Continuar', 'No, Cancelar', AccionSi, '');
}

function eliminar_doc(item) {
    $.ajax({
        url: $("#EndPointAQ").val() + "Convocatoria/Delete/Documento/" + item,
        dataType: 'text',
        cache: false,
        contentType: false,
        processData: false,
        type: "DELETE",
        success: function (data) {
            var objresponse = JSON.parse(data);
            if (objresponse.cod == "success") {
                SuccessSA('', 'El registro se eliminó exitosamente');
                Get_lista_documentos();
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

var tbl_convocatoria_doc = {
    p_opt: 0,
    p_id: "00000000-0000-0000-0000-000000000000",
    p_tbl_convocatoria_id: "00000000-0000-0000-0000-000000000000",
    p_tbl_tipo_documento_id: "00000000-0000-0000-0000-000000000000",
    p_justificacion: ""
}