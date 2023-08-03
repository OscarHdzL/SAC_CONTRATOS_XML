function add_sufuciencia(sigla, coment) {
    var con = $('#EndPointAQ').val() + "SerSolicitud/";
    var OBJ_Form = {};

    if (sigla !== 'SFCSL') {
        OBJ_Form.p_opt = 2;
        OBJ_Form.p_id = null;
        OBJ_Form.p_tbl_solicitud_id = $('#id_solicitud').val();
        OBJ_Form.p_fecha_autorizacion = $('#fecha_autorizacion').val();
        OBJ_Form.p_folio_autorizacion = $('#foli_autrz').val();
        OBJ_Form.p_autorizo = $('#autorizo').val();
        OBJ_Form.p_tbl_fuente_financiamiento_id = $('#fuen_financ').val();
        OBJ_Form.p_comentarios = coment;
        OBJ_Form.p_sigla = sigla;
    } else if (sigla == 'SFCSL') {
        var f_aut = '';
        let date = new Date();
        let day = date.getDate();
        let month = date.getMonth() + 1;
        let year = date.getFullYear();
        if (month < 10) {
            f_aut = (year + '-0' + month + '-' + day);
        } else {
            f_aut = (year + '-' + month + '-' + day);
        }
        OBJ_Form.p_opt = 2;
        OBJ_Form.p_id = null;
        OBJ_Form.p_tbl_solicitud_id = $('#_SOLICITUD').val();
        OBJ_Form.p_fecha_autorizacion = f_aut.toString();
        OBJ_Form.p_folio_autorizacion = '';
        OBJ_Form.p_autorizo = '';
        OBJ_Form.p_tbl_fuente_financiamiento_id = '';
        OBJ_Form.p_comentarios = coment;
        OBJ_Form.p_sigla = sigla;
    }
    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ_Form),
        type: 'post',

        success: function (data) {
            var objresponse = JSON.parse(data);
            if (objresponse != null) {
                Swal.fire({
                    type: 'success',
                    title: 'Solicitud actualizada',
                }).then(function (isConfirm) {
                    if (isConfirm) {
                        window.location.href = "/Bandeja";
                    }
                });
            }
        },
        error: function () {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse.Descripcion)
        },
        url: (con + "add_suficiencia")

    })
}