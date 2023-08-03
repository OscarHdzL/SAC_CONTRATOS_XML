$(function () {
    setTimeout(function () {
        if ($('#HD_programacion').val() == '') {
            $(".exist").each(function () {
                $(this).hide();
            });
            $(".noexist").each(function () {
                $(this).show();
            });
        }



    }, 100);

    $('.txt_FechaApertura').datetimepicker({
        format: 'YYYY-MM-DD',
        locale: 'es'
    });
    Get_NumSol();
    CallEstado();
});

setTimeout(function () {
    $("#cmb_EstApertura").change(function () {
        GetMunicipiosApertura();
    });



}, 500);


function GoBandeja() {
    window.location.href = "/Bandeja";
}

function AvanzaFaseActas() {

    $.get($('#EndPointAQ').val() + "AvanzarFase/Solicitud/" + $('#_SOLICITUD').val(), function (data, status) {
        if (data.cod == 'success') {
            window.location.href = "/Bandeja";
        } else if (data.cod == 'warning') {
            Swal.fire({
                type: 'error',
                title: 'Hay un error en los datos de entrada',
                text: data.msg
            });
        }
    });
}


function DeclararDesierta() {
    Swal.fire({
        title: 'Atención',
        text: "¿Desea declarar desierta la solicitud?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Confirmar '
    }).then((result) => {
        if (result.value) {
            $.get($('#EndPointAQ').val() + "Apertura/DeclararDesierta/" + $('#_SOLICITUD').val(), function (data, status) {
                if (data.cod == 'success') {
                    window.location.href = "/Bandeja";
                } else if (data.cod == 'warning') {
                    Swal.fire({
                        type: 'error',
                        title: 'Se presentó un error',
                        text: data.msg
                    });
                }
            });        
        }
        else {
            return;
        }
    })
    
}

function Get_NumSol() {
    
    $.get($('#EndPointAQ').val() + "SerSolicitud/Get/" + $('#_SOLICITUD').val(), function (data, status) {   
        $('.txt_SolicitudApertura').val(data.num_solicitud);
    }, 'json');
}

function CallEstado() {
    var Uri = $('#EndPointAQ').val() + 'Eventos/Estado';
    $.get(Uri, function (data, status) {
        var body_ = "<option value=''>Seleccione...</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            body_ = body_ + "<option value='" + data[i].value + "'>"
                + data[i].text + "</option>";
        }
        $('#cmb_EstApertura').html(body_);
    }, 'json');
}

function GetMunicipiosApertura() {
    $('.cmb_MunicipioApertura').html('');
    var Estado = $('#cmb_EstApertura').val();    
    
    $.get($('#EndPointAQ').val() + "Apertura/Municipio/" + Estado, function (data, status) {
        var Body = "<option value=''>Seleccione...</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            Body = Body + "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
        }
        $('.cmb_MunicipioApertura').html(Body);
    }, 'json');
}


function validateApertura(objeto) {

    var Validaciones = true;
    var Response = '';


    Validaciones = objeto.tbl_solicitud_id != '';
    if (!Validaciones) {
        Response = 'Debe ingresar una solicitud';
        return { Texto: Response, Bit: false }
    }

    Validaciones = objeto.fecha != '';
    if (!Validaciones) {
        Response = 'Debe seleccionar una fecha';
        return { Texto: Response, Bit: false }
    }

    Validaciones = objeto.hora != '';
    if (!Validaciones) {
        Response = 'Debe seleccionar una Hora';
        return { Texto: Response, Bit: false }
    }

    Validaciones = objeto.direccion != '';
    if (!Validaciones) {
        Response = 'Debe ingresar una dirección';
        return { Texto: Response, Bit: false }
    }
    else if (Validaciones) {
        if (ValidaCadena(objeto.direccion, 'Direccion') != '') {
            Swal.fire({
                type: 'error',
                title: 'Hay un error en los datos de entrada',
                text: 'El campo "Direccion" no puede contener caracteres especiales'
            })
            return;
        }
    }


    Validaciones = objeto.tbl_municipio_id != '';
    if (!Validaciones) {
        Response = 'Debe seleccionar un Municipio';
        return { Texto: Response, Bit: false }
    }

    Validaciones = objeto.comentario != ''
    if (ValidaCadena(objeto.comentario, 'comentario') != '') {
        Swal.fire({
            type: 'error',
            title: 'Hay un error en los datos de entrada',
            text: 'El campo "Comentarios" no puede contener caracteres especiales'
        })
        return;
    }


    return { Texto: '', Bit: true }
}


$('.btn-save-Apertura').click(function () {
    SendRequest();
});

function validarfile() {

    var Validaciones = true;
    var Response = '';

    Validaciones = $('#FileActaApertura').val() != '';
    if (!Validaciones) {
        Response = 'Debe seleccionar un archivo';
        return { Texto: Response, Bit: false }
    }

    return { Texto: '', Bit: true }
}
function SendRequest() {

    var evaluacion = validarfile();
    if (!evaluacion.Bit) {
        Swal.fire({
            type: 'error',
            title: 'Hay un error en los datos de entrada',
            text: evaluacion.Texto
        });
        return;
    }
    
    var file_data = $('#FileActaApertura').prop('files')[0];
    var form_data_file = new FormData();
    form_data_file.append('file', file_data);
    var token = '';  


    var AperturaInstancia = AperturaClass;
    AperturaInstancia.id = '00000000-0000-0000-0000-000000000000';
    AperturaInstancia.tbl_solicitud_id = $('#_SOLICITUD').val();
    AperturaInstancia.tbl_municipio_id = $('.cmb_MunicipioApertura').val();
    AperturaInstancia.tbl_tipo_apertura_id = $('#_TIPOACTA').val();
    AperturaInstancia.fecha = $('.txt_FechaApertura').val();
    AperturaInstancia.hora = $('.txt_HoraApertura').val();
    AperturaInstancia.direccion = $('.textarea_DireccionApertura').val();
    AperturaInstancia.comentario = $('.textarea_Comentarios').val();
    var i = $('#cbx_DeclaracionDesierta').is(":checked") ? 1 : 0;
    AperturaInstancia.declaracion_desierta = i; 

    var objVAL = validateApertura(AperturaInstancia);

    if (!objVAL.Bit) {
        Swal.fire({
            type: 'error',
            title: 'Hay un error en los datos de entrada',
            text: objVAL.Texto
        })
        return;
    }

    $.ajax({
        url: $("#EndPointFileAQ").val() + 'Upload/',
        dataType: 'text',
        cache: false,
        contentType: false,
        processData: false,
        data: form_data_file,
        type: 'POST',
        async: false,
        success: function (data) {
            token = data.replace(/[ '"]+/g, '');

        },
        error: function (data) {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse);
        }
    });

    AperturaInstancia.token = token;


    $.ajax({

        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(AperturaInstancia),
        type: 'post',

        success: function (data) {
            var data_b = $.parseJSON(data);
            var objresponse = JSON.parse(data);
            if (!objresponse.Bit) {
                SuccessSA("Operación exitosa", data_b[0].msg);

                $('#FileActaApertura').val(null);
                $('.txt_FechaApertura').val('');
                $('.textarea_Comentarios').val('');
                $('.txt_HoraApertura').val('');
                $('.textarea_DireccionApertura').val('');
                $('.cmb_EstadoApertura').val('');
                $('.cmb_MunicipioApertura').html('');
                $('#cbx_DeclaracionDesierta').prop("checked", false);                
                $('.icheckbox_minimal-blue').removeClass("checked");                
            }
            else {
                ErrorSA("", objresponse.Descripcion);
            }
        },
        error: function () {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse.Descripcion)
        },
        url: $('#EndPointAQ').val() + "Apertura/Add"
    });



};


var AperturaClass = {
    id: null,
    tbl_solicitud_id: null,
    tbl_municipio_id: null,
    tbl_tipo_apertura_id: null,
    fecha: null,
    hora: null,
    direccion: null,
    comentario: null,    
    declaracion_desierta: null,    
    token: null
};