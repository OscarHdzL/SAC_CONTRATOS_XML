
$(document).ready(function () {
    $('#tbl_comentarios').DataTable();
});
$('#ListarComentarios3').click(function () {
    $('#ModalComentarios').modal('show');
    //$('#drop_faseC').prop('disabled', true);
    var IdSol = $('#_SOLICITUD').val();
    GetListaComentarios(IdSol);
})

$('#ListarComentarios1, #ListarComentarios2, #ComentariosEvP').click(function () {


    //*************************************ACCESOS VALIDOS PARA GENERAR UN COMENTRIO****************************************//
    if (location.href.toLowerCase().indexOf('solicitud/complementa') > 0
        || location.href.toLowerCase().indexOf('solicitud/aprobadas') > 0
        || location.href.toLowerCase().indexOf('solicitud/rechazadas') > 0
        || location.href.toLowerCase().indexOf('modalidad/index') > 0
        || location.href.toLowerCase().indexOf('setconvocatoria/formulario') > 0
        || location.href.toLowerCase().indexOf('solicitud/poraprobar') > 0
        || location.href.toLowerCase().indexOf('proposiciones/evaluacioneconomica') > 0
        || location.href.toLowerCase().indexOf('proposiciones/evaluaciontecnica') > 0
        || location.href.toLowerCase().indexOf('visitasitio/actavisitasitio') > 0
        || location.href.toLowerCase().indexOf('juntaaclaraciones/actajuntaaclaraciones') > 0
        || location.href.toLowerCase().indexOf('actaapertura/actapresentacionaperturaeconomica') > 0
        || location.href.toLowerCase().indexOf('actaapertura/actapresentacionaperturatecnica') > 0
        || location.href.toLowerCase().indexOf('cotizaciones/cotizaciones') > 0
        || location.href.toLowerCase().indexOf('fallo/fallo') > 0
        || location.href.toLowerCase().indexOf('controlevaluacion/controlevaluacion') > 0
    ) {
        $('#ModalComentarios').modal('show');

        //Si es el para el registro de convocatoria
        if (location.href.toLowerCase().indexOf('setconvocatoria/formulario') > 0) {
            //$('#drop_faseC').prop('disabled', false);
            var IdSol = $('#_SOLICITUD').val();
            GetListaComentarios(IdSol);
        }
        else if (location.href.toLowerCase().indexOf('proposiciones/evaluacioneconomica') > 0
            || location.href.toLowerCase().indexOf('proposiciones/evaluaciontecnica') > 0) {
            $('#txt_Comentario1').prop('disabled', false);
            var IdSol = $('#_SOLICITUD').val();
            GetListaComentarios(IdSol);
            //$('#drop_faseC').prop('disabled', true);
        }
        else if (location.href.toLowerCase().indexOf('visitasitio/actavisitasitio') > 0
            || location.href.toLowerCase().indexOf('adquisiciones/actajuntaaclaraciones') > 0
            || location.href.toLowerCase().indexOf('actaapertura/actapresentacionaperturaeconomica') > 0
            || location.href.toLowerCase().indexOf('actaapertura/actapresentacionaperturatecnica') > 0
            || location.href.toLowerCase().indexOf('cotizaciones/cotizaciones') > 0
            || location.href.toLowerCase().indexOf('fallo/fallo') > 0
            || location.href.toLowerCase().indexOf('controlevaluacion/controlevaluacion') > 0) {

            $('#txt_Comentario1').prop('disabled', false);
            var IdSol = $('#_SOLICITUD').val();
            GetListaComentarios(IdSol);
            //$('#drop_faseC').prop('disabled', true);

        }
        else {
            $('#txt_Comentario1').prop('disabled', false);
            var IdSol = $('#_SOLICITUD').val();
            GetListaComentarios(IdSol);
            //$('#drop_faseC').prop('disabled', true);
        }
    }
    else {
        Swal.fire({
            type: 'error',
            title: 'Oops...',
            text: 'No hay mensajes para mostar!'
        });
    }
})

$('#cerrar').click(function () {
    if ((location.href.toLowerCase().indexOf('solicitud/complementa') < 0)
        && (location.href.toLowerCase().indexOf('solicitud') > 0)
        && (location.href.toLowerCase().indexOf('solicitud/aprobadas') < 0)
        && (location.href.toLowerCase().indexOf('solicitud/poraprobar') < 0)
        && location.href.toLowerCase().indexOf('solicitud/rechazadas') < 0) {
        window.location.href = "/Bandeja";
    }
    $('#txt_Comentario1').val('');
    //$('#drop_faseC').val('');
})

function Validar_Comentario() {

    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('#txt_Comentario1').val() == '') {
        Response.Texto = 'Debe agregar un comentario';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#txt_Comentario1').val(), 'Comentario') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Comentario"';
        Response.Bit = true;
        return Response;
    }
    Response.Texto = '';
    Response.Bit = false;
    return Response;
}

function AddComentario() {
    AddCM();
}


function AddCM() {
    var val = Validar_Comentario();
    //**********************************************COMIENZA LAS VALIDACIONES*********************************************//
    if (val.Bit) {
        Swal.fire({
            type: 'error',
            title: 'Hay un error en los datos de entrada',
            text: val.Texto
        });
        return;
    }

    var OBJ = Comentario;
    var d = new Date();
    var date = (d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate());

    //*************************************************ASIGNACION DE VALORES**********************************************//
    OBJ.p_opt = 2;
    OBJ.p_id = '00000000-0000-0000-0000-000000000000';
    OBJ.p_tbl_usuario_id = $('#HDidUsuario').val();
    OBJ.p_comentario = btoa($('#txt_Comentario1').val());
    


    //Se ejecuta cuando el comentario se redacta en el registro de convocatoria
    if (location.href.toLowerCase().indexOf('setconvocatoria/formulario') > 0) {
        //if ($('#drop_faseC').val() == '') {
        //    Swal.fire({
        //        type: 'error',
        //        title: 'Hay un error en los datos de entrada',
        //        text: 'Debe seleccionar en que fase se genera este comentario'
        //    });
        //    return;
        //}
        //else {
            OBJ.p_tbl_solicitud_id = $('#_SOLICITUD').val();
            OBJ.p_sigla_fase = $('#_FASE').val();
        //}
    }

    //Se ejecuta cuando el comentario se redacta desde la creacion de la solicitud
    else if ((location.href.toLowerCase().indexOf('solicitud/complementa') < 0)
        && (location.href.toLowerCase().indexOf('solicitud') > 0)
        && (location.href.toLowerCase().indexOf('solicitud/aprobadas') < 0)
        && (location.href.toLowerCase().indexOf('solicitud/poraprobar') < 0)
        && location.href.toLowerCase().indexOf('solicitud/rechazadas') < 0) {
        OBJ.p_sigla_fase = $('#_FASE').val();
        OBJ.p_tbl_solicitud_id = $('#_SOLICITUD').val();

    }

    //Se ejecuta cuando el comentario se redacta en la seccion de modalidad
    else if (location.href.toLowerCase().indexOf('modalidad/index') > 0) {
        OBJ.p_tbl_solicitud_id = $('#_SOLICITUD').val();
        OBJ.p_sigla_fase = $('#_FASE').val();
    }

    else if (location.href.toLowerCase().indexOf('bandeja') > 0) {
        OBJ.p_tbl_solicitud_id = $('#_SOLICITUD').val();
        OBJ.p_sigla_fase = $('#_FASE').val();
    }

    else if (location.href.toLowerCase().indexOf('proposiciones/evaluacioneconomica') > 0
        || location.href.toLowerCase().indexOf('proposiciones/evaluaciontecnica') > 0) {
        OBJ.p_sigla_fase = $('#_FASE').val();
        OBJ.p_tbl_solicitud_id = $('#_SOLICITUD').val();
    }
    else if (location.href.toLowerCase().indexOf('visitasitio/actavisitasitio') > 0
        || location.href.toLowerCase().indexOf('adquisiciones/actajuntaaclaraciones') > 0
        || location.href.toLowerCase().indexOf('actaapertura/actapresentacionaperturaeconomica') > 0
        || location.href.toLowerCase().indexOf('actaapertura/actapresentacionaperturatecnica') > 0
        || location.href.toLowerCase().indexOf('cotizaciones/cotizaciones') > 0
        || location.href.toLowerCase().indexOf('fallo/fallo') > 0
        || location.href.toLowerCase().indexOf('controlevaluacion/controlevaluacion') > 0) {
        OBJ.p_sigla_fase = $('#_FASE').val();
        OBJ.p_tbl_solicitud_id = $('#_SOLICITUD').val();
    }
    //Se ejecuta cuando el comentario se redacta en cuaquier parte o fase de la solicitud
    else {
        OBJ.p_sigla_fase = $('#_FASE').val();
        OBJ.p_tbl_solicitud_id = $('#_SOLICITUD').val();
    }
    //*******************************************TERMINA ASIGNACION DE VALORES********************************************//
    //**********************************************TERMINA LAS VALIDACIONES**********************************************//
    //var form_data = new FormData();
    //form_data.append('Comentario', JSON.stringify(OBJ));

    $.ajax({

        dataType: 'json',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ),
        type: 'post',

        success: function (data) {
            if (data.cod = 'success') {
                Swal.fire({
                    type: 'success',
                    title: 'Comentario agregado',
                }).then(function (isConfirm) {
                    if (isConfirm) {
                        if ((location.href.toLowerCase().indexOf('solicitud/complementa') < 0)
                            && (location.href.toLowerCase().indexOf('solicitud') > 0)
                            && (location.href.toLowerCase().indexOf('solicitud/aprobadas') < 0)
                            && (location.href.toLowerCase().indexOf('solicitud/poraprobar') < 0)
                            && location.href.toLowerCase().indexOf('solicitud/rechazadas') < 0) {
                            //GetListaComentarios($('#idsol').val());
                            window.location.href = "/Bandeja";
                            $('#txt_Comentario1').val('');
                        }
                        if (location.href.toLowerCase().indexOf('proposiciones/evaluacioneconomica') > 0
                            || location.href.toLowerCase().indexOf('proposiciones/evaluaciontecnica') > 0) {
                            $('#ModalComentario').modal('hide');
                            var IdSol = $('#_SOLICITUD').val();
                            GetListaComentarios(IdSol);
                            $('#txt_Comentario1').val('');
                        }
                        if (location.href.toLowerCase().indexOf('visitasitio/actavisitasitio') > 0
                            || location.href.toLowerCase().indexOf('adquisiciones/actajuntaaclaraciones') > 0
                            || location.href.toLowerCase().indexOf('actaapertura/actapresentacionaperturaeconomica') > 0
                            || location.href.toLowerCase().indexOf('actaapertura/actapresentacionaperturatecnica') > 0
                            || location.href.toLowerCase().indexOf('cotizaciones/cotizaciones') > 0
                            || location.href.toLowerCase().indexOf('fallo/fallo') > 0
                            || location.href.toLowerCase().indexOf('controlevaluacion/controlevaluacion') > 0) {
                            $('#ModalComentario').modal('hide');
                            var IdSol = $('#_SOLICITUD').val();
                            GetListaComentarios(IdSol);
                            $('#txt_Comentario1').val('');
                        }
                        if (location.href.toLowerCase().indexOf('bandeja') > 0) {
                            $('#ModalComentario').modal('hide');
                            var IdSol = $('#_SOLICITUD').val();
                            GetListaComentarios(IdSol);
                            $('#txt_Comentario1').val('');
                        }
                        if (location.href.toLowerCase().indexOf('setconvocatoria/formulario') > 0) {
                            $('#ModalComentario').modal('hide');
                            GetListaComentarios($('#_SOLICITUD').val());
                            $('#txt_Comentario1').val('');
                            //$('#drop_faseC').val('');
                        }
                        else {
                            $('#ModalComentario').modal('hide');
                            GetListaComentarios($('#_SOLICITUD').val());
                            $('#txt_Comentario1').val('');
                        }
                    }
                });
            }
        },
        error: function () {
            Swal.fire({
                type: 'error',
                title: 'Error al guardar el comentario'
            })
        },
        processData: false,
        type: 'POST',
        url: $('#EndPointAQ').val() +'Comentario/Add'
    })
}

function GetListaComentarios(idSolicitud) {
    $.get($('#EndPointAQ').val() + "Comentario/Get/Solicitud/" + idSolicitud, function (data, status) {
        var Arreglo_arreglos = [];
        for (var i = 0; i <= data.length - 1; i++) {
            var Interno = [];

            Interno.push(data[i].num_solicitud);
            Interno.push(data[i].nombre_usuario);
            Interno.push(atob(data[i].comentario));
            var fecha = (data[i].inclusion).split('T');
            Interno.push(fecha[0]);
            Interno.push(data[i].fase);

            Arreglo_arreglos.push(Interno);
        }

        var table = $('#tbl_comentarios').DataTable();

        table.destroy();
        console.log(Arreglo_arreglos);
        $('#tbl_comentarios').DataTable({
            data: Arreglo_arreglos,
            columns: [
                { title: "Solicitud" },
                { title: "Usuario" },
                { title: "Comentario" },
                { title: "Fecha" },
                { title: "Fase" }
            ]
        });

    });
}
var Comentario = {
    p_opt: null,
    p_id: null,
    p_tbl_solicitud_id: null,
    p_tbl_usuario_id: null,
    p_sigla_fase: null,
    p_comentario: null
}

$('.modal-child').on('show.bs.modal', function () {
    var modalParent = $(this).attr('data-modal-parent');
    $(modalParent).css('opacity', 0);
});

$('.modal-child').on('hidden.bs.modal', function () {
    var modalParent = $(this).attr('data-modal-parent');
    $(modalParent).css('opacity', 1);
});