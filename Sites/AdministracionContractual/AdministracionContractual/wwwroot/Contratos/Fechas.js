
function INIT_Fechas() {
    GetFechasProyecto();
    datepickerFechas();
    setTimeout(function () {
        Eventos_fechas();
    }, 1000);
}


function Eventos_fechas() {
    $(".valFechar_").click(function () {
        validacion_fechas();
    });
}



function GetFechasProyecto() {
    var obj = JSON.parse($('#objeto_contrato_temp').val());
    var uri = $('#EndPointAdmon').val() + 'Proyectos/Get_Proyecto/' + obj.p_tbl_proyecto_id;
    $.get(uri, function (data, status) {
        $('#lbl_proyecto').html(data[0].proyecto);
        $('#txt_fecha_inicio_proyecto_contratos').val(data[0].fecha_incio.replace('T00:00:00', ''));
        $('#txt_fecha_fin_proyecto_contratos').val(data[0].fecha_fin.replace('T00:00:00', ''));
    }, 'json');
}



function datepickerFechas() {
    $('#txt_fecha_inicio_contrato_contratos, #txt_fecha_fin_contrato_contratos').datetimepicker(
        {
            format: 'YYYY-MM-DD'
        }
    );
    let current_datetime = new Date()
    var yyyy = current_datetime.getFullYear();
    var mm = (current_datetime.getMonth() + 1).toString().length <= 1 ? "0" + (current_datetime.getMonth() + 1) : (current_datetime.getMonth() + 1);
    var dd = (current_datetime.getDate()).toString().length <= 1 ? "0" + (current_datetime.getDate()).toString() : (current_datetime.getDate()).toString();
    var formated_ = yyyy.toString() + "-" + mm.toString() + "-" + dd.toString();
    $('#txt_fecha_inicio_contrato_contratos, #txt_fecha_fin_contrato_contratos').val(formated_);
}

function validacion_fechas() {
    var validation = false;
    $("[requerido='val']").each(function () {
        var boolean_ = $(this).val() == '';

        if (boolean_) {
            $('#a2').val(0);
            validation = true;
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
    Swal.fire({
        type: 'success',
        title: 'Datos Validos',
        text: 'La seccion Fechas no tiene errores.'
    });
    $('.div_fechas').hide();
    $('.div_areas').show();
    INIT_Areas();
    $('#a2').val(1);

}


