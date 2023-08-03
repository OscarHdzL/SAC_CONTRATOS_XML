
$(document).ready(function () {
    $('#_FASE').val('JUSTI');
    console.log($('#_FASE').val());
});

function sendJustificacion(Procede) {

	if ($('#TXTA_justificacion').val() == '' || $('#TXTA_comentario').val() == '') {
		Swal.fire({
			type: 'error',
			title: 'Hay un error en los datos de entrada',
			text: 'Debé ingresar una justificación y un comentario.'
		});
		return;
    }
    
    var JustificacionObj = JustificacionClass;

    if (ValidaCadena($('#TXTA_justificacion').val(), 'Justificación') == '') {
        JustificacionClass.p_justificacion = $('#TXTA_justificacion').val();
    }
    else {
        Swal.fire({
            type: 'error',
            title: 'Hay un error en los datos de entrada',
            text: 'El campo "Justificación" no puede contener caracteres especiales'
        })
        return;
    }
    if (ValidaCadena($('#TXTA_comentario').val(), 'Comentario') == '') {
        JustificacionClass.p_comentarios = $('#TXTA_comentario').val();
    }
    else {
        Swal.fire({
            type: 'error',
            title: 'Hay un error en los datos de entrada',
            text: 'El campo "Comentario" no puede contener caracteres especiales'
        })
        return;
    }
    JustificacionClass.p_opt = 2;
    JustificacionClass.p_procede = Procede;
    JustificacionClass.p_tbl_solicitud_id = $('#_SOLICITUD').val();
	var d = new Date();
	var date = (d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate());
	


	$.ajax({
		 
		dataType: 'json',  
		cache: false,
		contentType: 'application/json',
		processData: false,
		data: JSON.stringify(JustificacionObj),
        type: 'post',

        success: function (data) {


            Swal.fire({
                type: 'success',
                title: 'Solicitud Guardada',
            }).then(function (isConfirm) {
                if (isConfirm) {
                    window.location.href = "/Bandeja";
                }
            });
        },
        error: function () {
            Swal.fire({
                type: 'error',
                title: 'Error al guardar la solicitud'
            })
        },
        type: 'POST',
        //url: '/Request/Justificacion/Add'
        url: $('#EndPointAQ').val() + "SerJustificacion/Add"
	});

}


var JustificacionClass = {
    p_opt: null,
    p_id: '00000000-0000-0000-0000-000000000000',
    p_tbl_solicitud_id: '00000000-0000-0000-0000-000000000000',
    p_justificacion: null,
    p_comentarios: null,
    p_procede: null
}

function LaunchJust(input, num, link) {

    $('#txt_solicitud_hd').val(input);
    $('#_SOLICITUD').val(input);
    $('#modal_justificacion').modal('show');
    $('#NumSol_hd').val(num);
    $('#NumSol_lbl').html(num);

    if (link == undefined || link == null) {

        $('#downloadfilejust').hide();

    } else {

        $('#downloadfilejust').show();
        $("#downloadfilejust").click(function () { getURL(link); });
    }

  
}

function getURL(token_) {
    //Limpiar viewer
    $('#viewer_window_iframe').attr('src', '');
    //


    var RES_ = '';
    var Uri = $('#EndPointFileAQ').val() + 'GeneraUrl/' + token_ + "/10";
    //alert(Uri);
    var URIENC = '';
    $.get(Uri, function (data, status) {
        //alert(data);
        URIENC = data;
        RES_ = $('#EndPointFileAQ').val() + "Viewer/" + URIENC;
        var SCRH = ((screen.height / 4) * 3) - 40;
        $('#viewer_window_iframe').css('height', SCRH);
        $('#viewer_window_iframe').attr('src', RES_);
        modalVisualizacion();
        return RES_;
    });

}

function modalVisualizacion() {
    $('#viewer_window').modal('show');
}

