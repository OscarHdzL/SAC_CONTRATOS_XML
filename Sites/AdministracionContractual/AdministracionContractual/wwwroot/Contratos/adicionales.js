



function INIT_adicionales() {
    calladicionales('procedimiento');
    calladicionales('prioridad');
    calladicionales('estatus');
    $('#MG').prop('disabled', true);

}

function calladicionales(value) {
    var uri = $('#EndPointAdmon').val() + 'Contratos/adicionalescontrato/' + value;
    $.get(uri, function (data) {
        var body__ = "<option value=''>Seleccione...</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            body__ = body__ + "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
        }
        $('#cmb_' + value).html(body__);
    });
}


function validadatosAdiciones() {
    var cont = 0;
    $(".Requerido_").each(function () {
        if ($(this).val() == '') {

            Swal.fire({
                type: 'error',
                title: 'Error de entrada de datos',
                text: 'Todos los valores son obligatorios',
            });
            cont = 0;
            $('#adicionales_').val('');
            $('#a6').val(0);
            return false;
        }
        else {
            $('#a6').val(1);
            cont = 1;
            Swal.fire({
                type: 'success',
                title: 'Datos correctos',
                text: 'Los datos se guardarón de forma correcta.',
            });

        }

        var objadicionales = {
            PMP: $('#PMP').val(),
            PMD: $('#PMD').val(),
            PG: $('#PG').val(),
            MG: $('#MG').val(),
            Proc: $('#cmb_procedimiento').val(),
            Priori: $('#cmb_prioridad').val(),
            Estatus: $('#cmb_estatus').val()
        };
        $('#adicionales_').val(JSON.stringify(objadicionales));


    });

    $('.div_deductivas').hide();
    $('.div_areas').hide();
    $('.formalizacion_div').show();


}

$('#PG').keyup(function () {
    if (baseline.length > 0) {
        //var conteo_var = 0;
        //for (var i = 0; i <= baseline.length - 1; i++) {
        //    conteo_var = conteo_var + parseInt(baseline[i].monto_por_ejercer);
        //}
        var porcentaje = $('#PG').val();
        console.log('porcentaje:  ' + porcentaje);

        var monto = $('#txt_maxsiniva_contratos').val().replaceAll(',', '');
        console.log('monto maximo sin iva:  ' + monto);
        var MG = parseFloat((monto * porcentaje) / 100);
        console.log('monto de la garantía: ' + MG);

        var formateado = formatNumber(MG.toFixed(2));
        console.log('monto de la garantía con formato: ' + formateado);
        $('#MG').val(formateado);

    } else {
        function limpiar() {
            return $('#PG').val('');
        }
        var si = eval(limpiar);
        ErrorSAAction('Error', 'Necesita cargar presupuesto al contrato en la seccion de "Áreas"', si);
    }

})
