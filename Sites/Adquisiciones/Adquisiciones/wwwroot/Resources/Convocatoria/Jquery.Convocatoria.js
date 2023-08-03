$(document).ready(function () {
    LaunchLoader(true);
    $('#datetimepicker_conv').datetimepicker({
        format: 'YYYY-MM-DD',
        locale: 'es'
    });
    validar_convocatoria();
});

function get_solicitud(id) {
    $.get($("#EndPointAQ").val() + "SerSolicitud/Get/" + id, function (data, status) {
        $("#txt_num_sol").val(data.num_solicitud);
        $("#txt_area").val(data.area);
        $("#txt_descripcion").val(data.descripcion);
        $("#txt_tipo_licitacion").val(data.tipo_licitacion);
        $(".input_con, .btn_con_save, .btn_con_cancel").prop("disabled", false);
        $("#txt_num_sol").prop("disabled", true);
    });
}

function validar_convocatoria() {
    var id_sol = $("#_SOLICITUD").val();
    
    $.get($("#EndPointAQ").val() + "Convocatoria/Get/" + id_sol, function (data, status) {
        var obj = JSON.stringify(data); 
        if (data.length > 0) {
            for (x = 0; x <= data.length - 1; x++) {

                $("#id_convocatoria").val(data[x].id);

                var fechaProg = data[x].fecha_suscripcion.substring(0, data[x].fecha_suscripcion.indexOf('T'));
                $("#txt_folio_con").val(data[x].folio);
                $("#txt_num_sol").val(data[x].num_solicitud);
                $("#txt_folio_proc").val(data[x].procedimiento);
                $("#txt_tipo_licitacion").val(data[x].tipo_licitacion);
                $("#txt_area").val(data[x].area);
                $("#txt_fecha").val(fechaProg);
                $("#txt_descripcion").val(data[x].descripcion);
                $(".input_con, .btn_con_save, .btn_con_cancel").prop("disabled", true);
            }
            $('.tabspanel').show();
        }
        else {
            get_solicitud(id_sol);
        }
        
    });
}

function Validar() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#txt_folio_con').val() == "") {
        Response.Texto = 'Debe ingresar un folio.';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_folio_proc').val() == "") {
        Response.Texto = 'Debe ingresar un folio de procedimiento';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_fecha').val() == "") {
        Response.Texto = 'Debe seleccionar le fecha de creación.';
        Response.Bit = true;
        return Response;
    }
    Response.Texto = '';
    Response.Bit = false;
    return Response;
}

function add_convocatoria() {
    var validacion = Validar();
    if (validacion.Bit) {
        return ErrorSA('Error en los datos de entrada', validacion.Texto);
    }
    var id_sol = $("#_SOLICITUD").val();
    var obj_convocatoria = tbl_convocatoria;

    obj_convocatoria.p_tbl_solicitud_id = id_sol;
    obj_convocatoria.p_tbl_servidor_publico_id = $("#HDidUsuario").val();
    obj_convocatoria.p_folio = $("#txt_folio_con").val();
    obj_convocatoria.p_procedimiento = $("#txt_folio_proc").val();
    obj_convocatoria.p_fecha_suscripcion = $("#txt_fecha").val();

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(obj_convocatoria),
        type: 'post',

        success: function (data) {
            var objresponse = JSON.parse(data); 
            if (objresponse.cod == "success") {
                SuccessSA("Operación exitosa", "El registro se guardado correctamente");
                validar_convocatoria();
                $('.tabspanel').show();
                Validar_pub();
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
        url: $("#EndPointAQ").val() + "Convocatoria/Add"

    });
}

$(".btn_con_save").click(function () {
    add_convocatoria();
})

var tbl_convocatoria = {
    p_opt: 0,
    p_id: '00000000-0000-0000-0000-000000000000',
    p_tbl_solicitud_id: "",
    p_tbl_servidor_publico_id: "",
    p_folio: "",
    p_procedimiento: "",
    p_fecha_suscripcion: "",
    p_inclusion: "2020-01-01",
    p_folio_publicacion: "",
    p_tipo_publicacion: ""
}