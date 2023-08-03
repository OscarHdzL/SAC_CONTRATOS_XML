﻿
$(function () {
    AutocompleteRfc3();
    GetLicitantesCotizacion();
});

function AutocompleteRfc3() {
    $.get($('#EndPointAQ').val() + "SerLicitante/Get/Proveedores/" + $('#HDidInstancia').val(), function (data, status) {
    var Contenido = [];
        for (var i = 0; i <= data.length - 1; i++) {
            Contenido.push(data[i].rfc);
        }
        $(".txt_RFCs3").autocomplete({
            source: Contenido
        });
    });
}

function validacion_tipo_solicitud() {

    $.get($('#EndPointAQ').val() + "Modalidad/Get/Solicitud/" + $('#_SOLICITUD').val(), function (data, status) {
        if (data.response != null) {
            if (data.response.sigla_licitacion == 'AD') { ///Solicitud de Adjudicacion directa, Se usa sigla del catalogo
                var table = $('#tbl_Licitacion_Cotizacion').DataTable();

                if (table.data().count() == 0) {
                    Guardar_licitante();
                } else if (table.data().count() > 0) {

                    Swal.fire({
                        type: 'error',
                        title: 'Solo se puede agregar un licitante en la adjudicacion directa'
                    });
                    return;
                }
            } else {
                Guardar_licitante();
            }

        }
    });
}


function Guardar_licitante() {

    if ($('.txt_RFCs3').val().length < 12 || $('.txt_RFCs3').val().length > 13) {
        Swal.fire({
            type: 'error',
            title: 'El RFC debe tener entre 12 y 13 caracteres'
        });
        return;
    }
    if (ValidaCadena($('.txt_RFCs3').val(), 'RFC') == '') {
        $('.RFCRL').val($('.txt_RFCs3').val());
        verificacion();
    }
    else {
        Swal.fire({
            type: 'error',
            title: 'Hay un error en los datos de entrada',
            text: 'El campo "RFC" no puede contener caracteres especiales'
        })
        return;
    }
}


//$("#btn_save_licitante3").click(function () {
//    if ($('.txt_RFCs3').val().length < 12 || $('.txt_RFCs3').val().length > 13) {
//        Swal.fire({
//            type: 'error',
//            title: 'El RFC debe tener entre 12 y 13 caracteres'
//        });
//        return;
//    }
//    if (ValidaCadena($('.txt_RFCs3').val(), 'RFC') == '') {
//        $('.RFCRL').val($('.txt_RFCs3').val());
//        verificacion();
//    }
//    else {
//        Swal.fire({
//            type: 'error',
//            title: 'Hay un error en los datos de entrada',
//            text: 'El campo "RFC" no puede contener caracteres especiales'
//        })
//        return;
//    }
//});


$(".newLic2").click(function () {
    sendlicitante();
});


function GetLicitantesCotizacion() {
        
    $.get($('#EndPointAQ').val() + "SerLicitante/Get/Propuestas/Solicitud/" + $('#_SOLICITUD').val() + '/' + $('#_TIPOACTA').val(), function (data, status) {
        var Arreglo_arreglosdot = [];
        for (var i = 0; i <= data.length - 1; i++) {
            var InternoArr = [];

            InternoArr.push(data[i].licitante);
            InternoArr.push(data[i].rep_nombre + ' ' + data[i].rep_paterno + ' ' + data[i].rep_materno);
            InternoArr.push(data[i].rfc);
            if (data[i].es_proveedor) {
                InternoArr.push('Proveedor Licitante');
            }
            else {
                InternoArr.push('Licitante');
            }
            InternoArr.push("<button class='btn btn-danger' onclick=\"DeleteItemlicitante('" + data[i].id + "');\">Eliminar</button>");

            if (data[i].token == null || data[i].token == '') {
                
                InternoArr.push("<button class='btn btn-info' onclick=\"CargarPropuestaLicitante('" + data[i].id + "');\">Adjuntar propuesta</button>");
            }
            else {
                InternoArr.push("<button class='btn btn-success' onclick=\"getURL('" + data[i].token + "');\"> Descargar PPTA </button>");
            }            

            Arreglo_arreglosdot.push(InternoArr);
        }

        var table = $('#tbl_Licitacion_Cotizacion').DataTable();

        table.destroy();

        $('#tbl_Licitacion_Cotizacion').DataTable({
            data: Arreglo_arreglosdot,
            columns: [
                { title: "Razon Soc." },
                { title: "Representante" },
                { title: "RFC" },
                { title: "Estatus" },                
                { title: "Acciones" },
                { title: "Propuesta" }

                //$('#_TIPOACTA').val() === 'tec' ? { title: "Propuesta Técnica" } : { title: "Propuesta Económica" }
            ],
            columnDefs: [
                {
                    targets: -1,
                    className: 'dt-body-center'
                }]
        });

    });
}



function DeleteItemlicitante(item) {
    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        type: 'delete',

        success: function (data) {
            var obj = JSON.parse(data);
            if (obj[0].cod == 'success') {
                SuccessSA("Operación exitosa", "El registro se eliminado correctamente");
                GetLicitantesCotizacion();
            }
            else {
                ErrorSA("Error", obj[0].msg);
            }
        },
        error: function () {

            ErrorSA('Error', "Ocurrio un error")
        },
        url: $('#EndPointAQ').val() + "SerLicitante/delete/" + item
    });

};



function CargarPropuestaLicitante(item) {

    $('#idLicitanteModal').val(item);
    $('#ModalCargaPropuesta').modal('show');

};


function verificacion() {
    $.get($('#EndPointAQ').val() + "SerLicitante/Valida/" + $('.txt_RFCs3 ').val() + "/" + $('#_SOLICITUD').val(), function (data, status) {

        if (data == undefined) {
            Swal.fire({
                type: 'error',
                title: 'Error',
                text: "Hay un error en la comunicación de los servicios"
            });
        }
        else if (data.cod == "NoExiste") {
            $('#altalicitante2').modal('show');
        }
        else if (data.cod == "success") {
            GetLicitantesCotizacion();
        }
        else if (data.cod == "warning") {
            Swal.fire({
                type: 'error',
                title: 'Error',
                text: data.msg
            });
        }
    });
}


function tamanotabla() {

    var table = $('#tbl_Licitacion_Cotizacion').DataTable();

    if (!table.data().count()) {
        alert('Empty table');
    }
}

$('.btn-save-propuesta').click(function () {
    sendPropuesta();
});

function validarfile() {

    var Validaciones = true;
    var Response = '';

    Validaciones = $('#FilePropuesta').val() != '';
    if (!Validaciones) {
        Response = 'Debe seleccionar un archivo';
        return { Texto: Response, Bit: false }
    }

    return { Texto: '', Bit: true }
}

function sendPropuesta() {

    var evaluacion = validarfile();
    if (!evaluacion.Bit) {
        Swal.fire({
            type: 'error',
            title: 'Hay un error en los datos de entrada',
            text: evaluacion.Texto
        });
        return;
    }





            var LicitantePropuestaInstancia = LicitantePropuestaClass;

            LicitantePropuestaInstancia.p_opt = 2;
            LicitantePropuestaInstancia.p_id = '00000000-0000-0000-0000-000000000000';
            LicitantePropuestaInstancia.p_tbl_licitante_id = $('#idLicitanteModal').val();
            LicitantePropuestaInstancia.p_sigla = $('#_TIPOACTA').val();
    
            var form_data_file = new FormData();
            var file_data = $('#FilePropuesta').prop('files')[0];

            form_data_file.append('file', file_data);


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

                    var token = data.replace(/[ '"]+/g, '');

                    LicitantePropuestaInstancia.p_token = token;
                },
                error: function (data) {
                    var objresponse = JSON.parse(data);
                    ErrorSA('', objresponse);
                }
            });
       
            ///////////fin Carga de archivo °°°°°°°°°°°°°°°°°°°°°°° Actualizacion JC

            var form_data = new FormData();
            form_data.append('LicitantePropuestaObj', JSON.stringify(LicitantePropuestaInstancia));


            $.ajax({

                dataType: 'text', 
                cache: false,
                contentType: false,
                processData: false,
                data: form_data,
                type: 'post',

                success: function (data) {
                    var objresponse = JSON.parse(data);
                    if (objresponse.cod == 'success') {

                        Swal.fire({
                            type: 'success',
                            title: 'Guardado exitoso',

                        })

                        $('#ModalCargaPropuesta').modal('hide');
                        $('#FilePropuesta').val('');
                        GetLicitantesCotizacion();
                    }
                },
                error: function (data) {
                    var objresponse = JSON.parse(data);
                    Swal.fire({
                        type: 'error',
                        title: 'Error al guardar propuesta',
                        text: objresponse.msg
                    })
                },
                processData: false,
                type: 'POST',
                url: $('#EndPointAQ').val() + "SerLicitante/Propuesta_Cotizacion/Add"        
            });
      
    


};

function sendlicitante() {


    var evaluacion = validateLicitante();
    if (evaluacion.Bit) {
        Swal.fire({
            type: 'error',
            title: 'Hay un error en los datos de entrada',
            text: evaluacion.Texto
        });
        return;
    }



    $.ajax({

        dataType: 'json',  // what to expect back from the PHP script, if anything
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(evaluacion.objeto),
        type: 'post',

        success: function (data) {
            if (!data.Bit) {
                $('.txt_RFCs3').val('');
                //$('.txt_RFCs').val('');
                $('.NombreRL').val('');
                $('.PaternoRL').val('');
                $('.MaternoRL').val('');
                $('.RazonSocialRL').val('');
                $('.RFCRL').val('');

                GetLicitantesCotizacion();

                $('#altalicitante2').modal('hide');
            }
        },
        error: function (data) {
            alert(data);
            Swal.fire({
                type: 'error',
                title: 'Error al guardar Licitación',
                text: data
            })
        },
        processData: false,
        type: 'POST',
        url: $('#EndPointAQ').val() + "SerLicitante/Add"
    });

}


function validateLicitante() {
    var OBJ = LicitanteConvClass;
    var Response = { Texto: '', Bit: true, objeto: null };
    //Nombre
    if ($('.NombreRL').val() == '') {
        Response.Texto = 'Debé ingresar el nombre del representante legal';
        Response.Bit = true;
        return Response;
    }
    if (ValidaCadena($('.NombreRL').val(), 'Nombre') != '') {
        Swal.fire({
            type: 'error',
            title: 'Hay un error en los datos de entrada',
            text: 'El campo "Nombre" no puede contener caracteres especiales'
        })
        return;
    }
    //paterno
    if ($('.PaternoRL').val() == '') {
        Response.Texto = 'Debé ingresar el apellido Paterno del representante legal';
        Response.Bit = true;
        return Response;
    }
    if (ValidaCadena($('.PaternoRL').val(), 'Apellido Paterno') != '') {
        Swal.fire({
            type: 'error',
            title: 'Hay un error en los datos de entrada',
            text: 'El campo "Apellido Paterno" no puede contener caracteres especiales'
        })
        return;
    }
    //materno
    //if ($('.MaternoRL').val() == '') {
    //    Response.Texto = 'Debé ingresar el apellido Paterno del representante legal';
    //    Response.Bit = true;
    //    return Response;
    //}
    //if (ValidaCadena($('.MaternoRL').val(), 'Apellido Materno') != '') {
    //    Swal.fire({
    //        type: 'error',
    //        title: 'Hay un error en los datos de entrada',
    //        text: 'El campo "Apellido Materno" no puede contener caracteres especiales'
    //    })
    //    return;
    //}
    //Razón Social
    if ($('.RazonSocialRL').val() == '') {
        Response.Texto = 'Debé ingresar una razón social';
        Response.Bit = true;
        return Response;
    }
    if (ValidaCadena($('.RazonSocialRL').val(), 'Razón Social') != '') {
        Swal.fire({
            type: 'error',
            title: 'Hay un error en los datos de entrada',
            text: 'El campo "Razón Social" no puede contener caracteres especiales'
        })
        return;
    }
    //RFC
    if ($('.RFCRL').val() == '' || $('.RFCRL').val().length < 12) {
        Response.Texto = 'Debé ingresar un RFC a 12 o 13 posiciones';
        Response.Bit = true;
        return Response;
    }


    OBJ.p_opt = 2;
    OBJ.p_tbl_solicitud_id = $('#_SOLICITUD').val();
    OBJ.p_razon_social = $('.RazonSocialRL').val();
    OBJ.p_rep_legal_nombre = $('.NombreRL').val();
    OBJ.p_rep_legal_ap_paterno = $('.PaternoRL').val();
    OBJ.p_rep_legal_ap_materno = $('.MaternoRL').val();
    OBJ.p_rfc = $('.RFCRL').val();
    OBJ.p_es_proveedor = 0;


    Response.Texto = '';
    Response.Bit = false;
    Response.objeto = OBJ;
    return Response;
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

var LicitanteConvClass = {
    p_opt: null,
    p_id: '00000000-0000-0000-0000-000000000000',
    p_tbl_solicitud_id: ' 00000000-0000-0000-0000-000000000000',
    p_razon_social: '',
    p_rep_legal_nombre: '',
    p_rep_legal_ap_paterno: '',
    p_rep_legal_ap_materno: '',
    p_rfc: '',
    p_es_proveedor: ''
}

var LicitantePropuestaClass = {
    p_opt: 2,
    p_id: null,
    p_tbl_licitante_id: null,
    p_sigla: null,
    p_token: null
}


function AvanzaFaseActas() {

    $.get($('#EndPointAQ').val() + "SerLicitante/Get/Propuestas/Solicitud/" + $('#_SOLICITUD').val() + '/' + $('#_TIPOACTA').val(), function (data2, status) {

        var invalido = false;
        for (var i = 0; i <= data2.length - 1; i++) {
            if (data2[i].token == '' || (data2[i].token == null)) {

                invalido = true;
            }
        }

        if (invalido) {
            Swal.fire({
                type: 'error',
                title: 'Hay un error en los datos de entrada',
                text: 'No se han cargado propuestas en todos los licitantes'
            });
        } else {

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

        


    });
}

function validarAvance() {

    
   
}