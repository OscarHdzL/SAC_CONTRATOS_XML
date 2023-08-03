
$(document).ready(function () {
    $("#tbl_obligaciones").DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        }
    });
    Get_TipoObligacion();
    obtenerListado_obligaciones($("#_SOLICITUD").val());
})

function Get_TipoObligacion() {
    $.get($('#EndPointAQ').val() + "Convocatoria/Get/TipoObligaciones", function (data, status) {
        var body = "<option value=''>Seleccione...</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            body = body + "<option value='" + data[i].id + "'>" + data[i].tipo_obligacion + "</option>";
        }
        $('#cmb_obligacion, #cmb_obligacion_upd').html(body);
    });
}

function Validar_rec() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#input_Obligacion').val() == "") {
        Response.Texto = 'Debe ingresar un requerimiento.';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadenab($('#input_Obligacion').val(), '') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Obligación"';
        Response.Bit = true;
        return Response;
    }
    if ($('#cmb_obligacion').val() == "") {
        Response.Texto = 'Debe seleccionar un tipo de obligación.';
        Response.Bit = true;
        return Response;
    }
    Response.Texto = '';
    Response.Bit = false;
    return Response;
}

$(".btn_Add_Oblig").click(function () {
    add_requerimientos();
})

function add_requerimientos() {
    var validacion = Validar_rec();
    if (validacion.Bit) {
        return ErrorSA('Error en los datos de entrada', validacion.Texto);
    }
    var obj_obligacion = tbl_obligacion;

    obj_obligacion.p_tbl_convocatoria_id = $("#id_convocatoria").val();
    obj_obligacion.p_tbl_tipo_obligacion_id = $("#cmb_obligacion").val();
    obj_obligacion.p_obligacion = $("#input_Obligacion").val();

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(obj_obligacion),
        type: 'post',

        success: function (data) {
            var objresponse = JSON.parse(data);
            if (objresponse.cod == "success") {
                SuccessSA("Operación exitosa", "El registro se guardado correctamente");
                $(".clear").val("");
                obtenerListado_obligaciones($("#_SOLICITUD").val());
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
        url: $("#EndPointAQ").val() + "Convocatoria/Add/Obligacion"

    });
}

function obtenerListado_obligaciones(id_solicitud) {

    $.get($("#EndPointAQ").val() + "Convocatoria/Get/Obligaciones/" + id_solicitud, function (data, status) {
        var listado = [];
        for (var i = 0; i <= data.length - 1; i++) {
            var fila = [];

            fila.push(i + 1);
            fila.push(data[i].folio);
            fila.push(data[i].obligacion);
            fila.push(data[i].tipo_obligacion);
            fila.push('<a onclick="btnEditCO(\'' + data[i].id + '\',\'' + data[i].obligacion + '\',\'' + data[i].tbl_tipo_obligacion_id + '\')" class="fa fa-pencil-square-o btn btn-primary" title="Modificar registro"> </a> '
                + '<a onclick="btnDeleteCO(\'' + data[i].id + '\')" class="fa fa-trash-o btn btn-danger" title="Eliminar registro"> </a>');
            listado.push(fila);
        }

        var table = $('#tbl_obligaciones').DataTable();

        table.destroy();

        $('#tbl_obligaciones').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            data: listado,
            columns: [
                { title: "No." },
                { title: "Folio convocatoria" },
                { title: "Requerimiento" },
                { title: "Tipo de Requerimiento" },
                { title: "Acciones" }
            ],
            columnDefs: [
                {
                    targets: -1,
                    className: 'dt-body-center'
                }]
        });
    });
}

function btnDeleteCO(item) {
    function Confirmacion_() {
        return eliminar(item);
    }
    var AccionSi = eval(Confirmacion_);
    WarningSA('', '¿En verdad desea eliminar el registro?', 'Si, Continuar', 'No, Cancelar', AccionSi, '');
}

function eliminar(item) {
    $.ajax({
        url: $("#EndPointAQ").val() + "Convocatoria/Delete/" + item,
        dataType: 'text',
        cache: false,
        contentType: false,
        processData: false,
        type: "DELETE",
        success: function (data) {
            SuccessSA('', 'El registro se eliminó exitosamente');
            obtenerListado_obligaciones($("#_SOLICITUD").val());
        },
        error: function () {
            ErrorSA('', "Ocurrio un error.")
        }
    });
}

function btnEditCO(item,obligacion,tipo_obl) {
    $("#conv_obligacion").val(item);
    $("#input_Obligacion_upd").val(obligacion);
    $("#cmb_obligacion_upd").val(tipo_obl);
    $('#Modal_Tipo_Obligacion_Upd').modal({ backdrop: 'static', keyboard: false });
    $('#Modal_Tipo_Obligacion_Upd').modal('show');
}

function cerrar_modal_upd() {
    $('#Modal_Tipo_Obligacion_Upd').modal('hide');
    $(".clear").val("");
}

var tbl_obligacion = {
    p_opt: 0,
    p_id: '00000000-0000-0000-0000-000000000000',
    p_tbl_convocatoria_id: '00000000-0000-0000-0000-000000000000',
    p_tbl_tipo_obligacion_id: '00000000-0000-0000-0000-000000000000',
    p_tbl_estatus_obligacion_id: '6e858887-6232-11ea-8324-00155d1b3502', //id por default
    p_obligacion: "",
    p_inclusion : "2020-01-01"              
}  

function Validar2() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#input_Obligacion_upd').val() == "") {
        Response.Texto = 'Debe ingresar un requerimiento.';
        Response.Bit = true;
        return Response;
    }
    if ($('#cmb_obligacion_upd').val() == "") {
        Response.Texto = 'Debe seleccionar un tipo de obligación.';
        Response.Bit = true;
        return Response;
    }
    Response.Texto = '';
    Response.Bit = false;
    return Response;
}

$("#btn_upd_tipo_obl").click(function () {
    Update_requerimientos()
})

function Update_requerimientos() {

    var validacion = Validar2();
    if (validacion.Bit) {
        return ErrorSA('Error en los datos de entrada', validacion.Texto);
    }
    var obj_obligacion_upd = tbl_obligacion;
    obj_obligacion_upd.p_id = $("#conv_obligacion").val();
    obj_obligacion_upd.p_tbl_convocatoria_id = $("#id_convocatoria").val();
    obj_obligacion_upd.p_tbl_tipo_obligacion_id = $("#cmb_obligacion_upd").val();
    obj_obligacion_upd.p_obligacion = $("#input_Obligacion_upd").val();


    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(obj_obligacion_upd),
        type: 'put',

        success: function (data) {
            var objresponse = JSON.parse(data);
            if (objresponse.cod == "success") {
                function si() {
                    return cerrar_modal_upd();
                }
                var AccionSi = eval(si);
                SuccessSAAction("Operación exitosa", "El registro se guardado correctamente", AccionSi);
                
                obtenerListado_obligaciones($("#_SOLICITUD").val());
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
        url: $("#EndPointAQ").val() + "Convocatoria/Update/Obligacion"

    });
}