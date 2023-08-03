
///objeto_contrato_temp


/////////////////////////////////////////////////////////////// DOCUMENT READY ///////////////////////////////////////////////////////////////////////////////////
function INIT_datosgenerales() {

    ///////////////////////////////////////////// SETTIMEOUT 1SEG
    setTimeout(function () {

        datepickerDatosGenerales();
        Eventos_datosGenerales();
    }, 1000);
}
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////// DOM EVENTS ///////////////////////////////////////////////////////////////////////////////////


function Eventos_datosGenerales() {
    $(".valDatGrales").click(function () {
        validacion_datosgenerales();
    });
}
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



function datepickerDatosGenerales() {
    $('#txt_fecha_registro_contratos, #txt_fecha_inicio_contratos, #txt_fecha_final_contratos').datetimepicker(
        {
            format: 'YYYY-MM-DD'
        }
    );
    let current_datetime = new Date()
    var yyyy = current_datetime.getFullYear();
    var mm = (current_datetime.getMonth() + 1).toString().length <= 1 ? "0" + (current_datetime.getMonth() + 1) : (current_datetime.getMonth() + 1);
    var dd = (current_datetime.getDate()).toString().length <= 1 ? "0" + (current_datetime.getDate()).toString() : (current_datetime.getDate()).toString();
    var formated_ = yyyy.toString() + "-" + mm.toString() + "-" + dd.toString();
    $('#txt_fecha_registro_contratos, #txt_fecha_inicio_contratos, #txt_fecha_final_contratos').val(formated_);
}


function GetproyectosPorDependencia() {
    var uri = $('#EndPointAdmon').val() + 'Proyectos/Get_Lista/' + $('#HDidDependencia').val();
    $.get(uri, function (data, status) {
        $('#cmb_proyecto_contratos').html('');
        $('#cmb_proyecto_contratos').html("<option value=''>Seleccione...</option>");
        for (var i = 0; i <= data.length - 1; i++) {
            var option_ = "<option value='" + data[i].id + "'>" + data[i].proyecto + "</option>"
            var valor = $('#cmb_proyecto_contratos').html();
            $('#cmb_proyecto_contratos').html(valor + option_);
        }
    }, 'json');
}


function Gettipocontraato() {
    var uri = $('#EndPointAdmon').val() + 'Contratos/tipocontrato';
    $.get(uri, function (data, status) {
        $('#cmb_tipo_contrato_contratos').html('');
        $('#cmb_tipo_contrato_contratos').html("<option value=''>Seleccione...</option>");
        for (var i = 0; i <= data.length - 1; i++) {
            var option_ = "<option value='" + data[i].id + "'>" + data[i].tipo_contrato + "</option>"
            var valor = $('#cmb_tipo_contrato_contratos').html();
            $('#cmb_tipo_contrato_contratos').html(valor + option_);
        }
    }, 'json');
}


function validacion_datosgenerales() {
    var validation = false;
    $("[requerido_val='val']").each(function () {
        var boolean_ = $(this).val() == '';

        if (boolean_) {
            validation = true;
            $('#objeto_contrato_temp').val('');
            $('#a1').val(0);
            Swal.fire({
                type: 'error',
                title: 'Datos incompletos',
                text: 'Todos los campos del formulario son necesarios, favor de completar el formulario e intentarlo de nuevo.'
            });

        }

    });
    if (validation) {
        return;
    }

    $('#objeto_datos_generales').val();
    var obj_validacion = EntidadContrato;

    obj_validacion.p_necesidad = $('#cmb_tipo_contratacion_contratos').val();
    obj_validacion.p_tbl_proyecto_id = $('#cmb_proyecto_contratos').val();
    obj_validacion.p_fecha_Iinicio = $('#txt_fecha_inicio_contratos').val();
    obj_validacion.p_fecha_fin = $('#txt_fecha_final_contratos').val();

    obj_validacion.p_numero = $('#txt_numero_contratos').val();
    obj_validacion.p_nombre = $('#txt_nombre_contrato_contratos').val();
    obj_validacion.p_objeto = $('#txt_objeto_contratos').val();
    obj_validacion.p_fecha_firma = $('#txt_fecha_registro_contratos').val();

    obj_validacion.p_tbl_tipo_contrato_id = $('#cmb_tipo_contrato_contratos').val();
    obj_validacion.p_monto_min_sin_iva = $('#txt_minsiniva_contratos').val().replaceAll(',', '');
    obj_validacion.p_monto_max_sin_iva = $('#txt_maxsiniva_contratos').val().replaceAll(',', '');
    obj_validacion.p_id = "00000000-0000-0000-0000-000000000000";


    //"00000000-0000-0000-0000-000000000000"

    $('#objeto_contrato_temp').val(JSON.stringify(obj_validacion));
    //debugger;
    /////////////////////////////////////////////////////////////////////////////////////////// success process complete
    Swal.fire({
        type: 'success',
        title: 'Datos Validos',
        text: 'La seccion Datos Generales no tiene errores.'
    });
    $('.div_datosgenerales').hide();
    $('.div_fechas').show();
    INIT_Fechas();
    $('#a1').val(1);

}


function isValidDate(dateString) {
    var regEx = /^\d{4}-\d{2}-\d{2}$/;
    if (!dateString.match(regEx)) return false;  // Invalid format
    var d = new Date(dateString);
    var dNum = d.getTime();
    if (!dNum && dNum !== 0) return false; // NaN value, Invalid date
    return d.toISOString().slice(0, 10) === dateString;
}







