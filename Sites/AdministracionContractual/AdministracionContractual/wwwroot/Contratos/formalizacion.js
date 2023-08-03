
function validacion_completa() {
    if (!validacion_datosgenerales_formalizacion()) {
        $('#a1').val(0);
        $('.tcre_1').removeClass('primary');
        $('.tcre_1').addClass('danger');
    } else if (validacion_datosgenerales_formalizacion) {
        $('#a1').val(1);
        $('.tcre_1').removeClass('danger');
        $('.tcre_1').addClass('primary');
    }
    if (!validacion_fechas_formalizacion()) {
        $('.tcre_2').removeClass('primary');
        $('.tcre_2').addClass('danger');
    } else if (validacion_fechas_formalizacion()) {
        $('.tcre_2').removeClass('danger');
        $('.tcre_2').addClass('primary');
    }
    if (!validate_Areas_formalizacion()) {
        $('.tcre_3').removeClass('primary');
        $('.tcre_3').addClass('danger');
    } else if (validate_Areas_formalizacion()) {
        $('.tcre_3').removeClass('danger');
        $('.tcre_3').addClass('primary');
    }

    if (!validate_firmantes_formalizacion()) {
        $('.tcre_4').removeClass('primary');
        $('.tcre_4').addClass('danger');
    } else if (validate_firmantes_formalizacion()) {
        $('.tcre_4').removeClass('danger');
        $('.tcre_4').addClass('primary');
    }


    if (!validate_proveedores_formalizacion()) {
        $('.tcre_5').removeClass('primary');
        $('.tcre_5').addClass('danger');
    } else if (validate_proveedores_formalizacion()) {
        $('.tcre_5').removeClass('danger');
        $('.tcre_5').addClass('primary');
    }

    if (!validadatosAdiciones__formalizaciones()) {
        $('.tcre_6').removeClass('primary');
        $('.tcre_6').addClass('danger');
    } else if (validadatosAdiciones__formalizaciones()) {
        $('.tcre_6').removeClass('danger');
        $('.tcre_6').addClass('primary');
    }

    if (listFiles.length == 0) {
        $('.tcre_7').removeClass('primary');
        $('.tcre_7').addClass('danger');
    }
    else {
        $('.tcre_7').removeClass('danger');
        $('.tcre_7').addClass('primary');
    }

    if (
        $('.tcre_1').hasClass('primary') &&
        $('.tcre_2').hasClass('primary') &&
        $('.tcre_3').hasClass('primary') &&
        $('.tcre_4').hasClass('primary') &&
        $('.tcre_5').hasClass('primary') &&
        $('.tcre_6').hasClass('primary') &&
        $('.tcre_7').hasClass('primary')
    ) {
        save_contrato();
    }



}

function validacion_completa_update() {
    if (!validacion_datosgenerales_formalizacion()) {
        $('#a1').val(0);
        $('.tcre_1').removeClass('primary');
        $('.tcre_1').addClass('danger');
    } else if (validacion_datosgenerales_formalizacion) {
        $('#a1').val(1);
        $('.tcre_1').removeClass('danger');
        $('.tcre_1').addClass('primary');
    }
    if (!validacion_fechas_formalizacion()) {
        $('.tcre_2').removeClass('primary');
        $('.tcre_2').addClass('danger');
    } else if (validacion_fechas_formalizacion()) {
        $('.tcre_2').removeClass('danger');
        $('.tcre_2').addClass('primary');
    }
    if (!validate_Areas_formalizacion()) {
        $('.tcre_3').removeClass('primary');
        $('.tcre_3').addClass('danger');
    } else if (validate_Areas_formalizacion()) {
        $('.tcre_3').removeClass('danger');
        $('.tcre_3').addClass('primary');
    }

    if (!validate_firmantes_formalizacion()) {
        $('.tcre_4').removeClass('primary');
        $('.tcre_4').addClass('danger');
    } else if (validate_firmantes_formalizacion()) {
        $('.tcre_4').removeClass('danger');
        $('.tcre_4').addClass('primary');
    }


    if (!validate_proveedores_formalizacion()) {
        $('.tcre_5').removeClass('primary');
        $('.tcre_5').addClass('danger');
    } else if (validate_proveedores_formalizacion()) {
        $('.tcre_5').removeClass('danger');
        $('.tcre_5').addClass('primary');
    }

    if (!validadatosAdiciones__formalizaciones()) {
        $('.tcre_6').removeClass('primary');
        $('.tcre_6').addClass('danger');
    } else if (validadatosAdiciones__formalizaciones()) {
        $('.tcre_6').removeClass('danger');
        $('.tcre_6').addClass('primary');
    }

    //if ($('#doc_formalizacion').val() == '') {
    //    $('.tcre_7').removeClass('primary');
    //    $('.tcre_7').addClass('danger');
    //}
    //else {
    //    $('.tcre_7').removeClass('danger');
    $('.tcre_7').addClass('primary');
    //}

    if (
        $('.tcre_1').hasClass('primary') &&
        $('.tcre_2').hasClass('primary') &&
        $('.tcre_3').hasClass('primary') &&
        $('.tcre_4').hasClass('primary') &&
        $('.tcre_5').hasClass('primary') &&
        $('.tcre_6').hasClass('primary') &&
        $('.tcre_7').hasClass('primary')
    ) {
        $('#estatus_act').val(1);
    }
    else {
        $('#estatus_act').val(0);
    }



}




function validacion_datosgenerales_formalizacion() {
    var validation = true;
    $("[requerido_val='val']").each(function () {
        if ($(this).val() == '' || $(this).val() == null) {
            validation = false;
        }
    });
    return validation;
}

function validacion_fechas_formalizacion() {
    var validation = true;
    $("[requerido='val']").each(function () {
        if ($(this).val() == '') {
            validation = false;
        }
    });
    return validation;
}


function validate_Areas_formalizacion() {
    var banderaErrores = 0;
    if (parseInt($('#mtae').html()) > 0) {
        //return true;
    }
    else {
        banderaErrores = banderaErrores + 1;
        //return false;
    }
    if ($("#dependencias_usuario").val() == '') {
        banderaErrores = banderaErrores + 1;
    }
    if ($("#areaAsignadaContrato").val() == '') {
        banderaErrores = banderaErrores + 1;
    }
    if ($("#nivelAreaAsignadaContrato").val() == '') {
        banderaErrores = banderaErrores + 1;
    }
    if (banderaErrores == 0) {
        return true;
    } else {
        return false;
    }
}



function validate_firmantes_formalizacion() {
    var obj = JSON.parse($('#firmantes_hd').val());

    if ($('#responsables_hd').val() != "" && obj.length <= 0) {
        return false;
    }
    else if ($('#responsables_hd').val() == "" && obj.length <= 0) {
        return false;
    }
    else {
        return true;
    }
}

function validate_proveedores_formalizacion() {
    var obj = JSON.parse($('#proveedores__').val());

    if (obj.length <= 0) {
        return false;
    }
    else {
        return true;
    }
}



function validadatosAdiciones__formalizaciones() {
    var nev = true;
    $(".Requerido_").each(function () {
        if ($(this).val() == '') {
            nev = false;
        }
    });
    return nev;
}


