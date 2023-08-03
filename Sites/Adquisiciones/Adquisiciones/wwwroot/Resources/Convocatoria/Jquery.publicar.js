$(document).ready(function () {
    Validar_pub();
})

function Validar_pub() {
    var id_sol = $("#_SOLICITUD").val();
    $.get($("#EndPointAQ").val() + "Convocatoria/Get/" + id_sol, function (data, status) {
        var obj = JSON.stringify(data);
        if (data.length > 0) {
            for (x = 0; x <= data.length - 1; x++) {
                var folio_pub = data[x].folio_publicacion;
                var tipo_pub = data[x].tipo_publicacion;

                var Sol_index = data[x].num_solicitud;
                var Con_index = data[x].folio;

                if (folio_pub != '' && tipo_pub != '') {
                    $('.first').hide();
                    $('.accions').show();
                    $(".Publica-Container").show();

                    $('.Sol_text').val(Sol_index);
                    $('.Nconv_text').val(Con_index);
                    $('.txt_folio_conv_pub').val(folio_pub).prop('disabled', true);
                    $('.cmb_tipo_publicacion').val(tipo_pub).prop('disabled', true);
                    $('.publicar_conv').prop('disabled', true);
                }
                else {
                    $(".btn-Genera").click(function () {
                        $('.loaderapp').show();
                        $(".btn-Genera").hide();
                        setTimeout(function () {
                            $('.loaderapp').hide();
                            $('.first').hide();
                            $(".Publica-Container").show();
                            $('.accions').show();

                            $('.Sol_text').val(Sol_index);
                            $('.Nconv_text').val(Con_index);
                        }, 3000);
                    });
                }
            }
        }
        else {
        }

    });
    
}

function Validar_conv_pub() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('.txt_folio_conv_pub').val() == "") {
        Response.Texto = 'Debe ingresar un folio de publicación.';
        Response.Bit = true;
        return Response;
    }
    if ($('.cmb_tipo_publicacion').val() == "") {
        Response.Texto = 'Debe seleccionar un tipo de publicación';
        Response.Bit = true;
        return Response;
    }
    Response.Texto = '';
    Response.Bit = false;
    return Response;
}

function Publicar() {
    var Validacion = Validar_conv_pub();

    if (Validacion.Bit) {
        return ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }
    var obj_conv = tbl_convocatoria_upd;

    obj_conv.p_id = $('#id_convocatoria').val();
    obj_conv.p_folio_publicacion = $('.txt_folio_conv_pub').val();
    obj_conv.p_tipo_publicacion = $('.cmb_tipo_publicacion').val();

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(obj_conv),
        type: 'put',

        success: function (data) {
            var objresponse = JSON.parse(data);
            if (objresponse.cod == "success") {
                SuccessSA("Operación exitosa", "El registro se guardado correctamente");
                $('.txt_folio_conv_pub, .cmb_tipo_publicacion, .publicar_conv').prop('disabled', true);
                window.location.href = "/Bandeja";
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
        url: $("#EndPointAQ").val() + "Convocatoria/Update"

    });
}

var tbl_convocatoria_upd = {
    p_opt: 0,
    p_id: '00000000-0000-0000-0000-000000000000',
    p_tbl_solicitud_id: '00000000-0000-0000-0000-000000000000',
    p_tbl_servidor_publico_id: '00000000-0000-0000-0000-000000000000',
    p_folio: '',
    p_procedimiento: '',
    p_fecha_suscripcion: "2020-01-01",
    p_inclusion: "2020-01-01",
    p_folio_publicacion: "",
    p_tipo_publicacion: ""
}