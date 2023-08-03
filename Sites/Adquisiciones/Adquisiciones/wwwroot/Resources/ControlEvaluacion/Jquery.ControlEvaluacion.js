$(function () {
    GetPropuestas();
    ValidarEvalFallo();
    Get_NumSol();
});

function Get_NumSol() {

    $.get($('#EndPointAQ').val() + "SerSolicitud/Get/" + $('#_SOLICITUD').val(), function (data, status) {
        $('.txt_SolicitudControlEvaluacion').val(data.num_solicitud);
    }, 'json');
}



function GetPropuestas() {

    //$.get("/Request/EvaluacionPropuesta/Licitantes/" + $('#idconvocatoria').val(), function (data, status) {
    $.get($('#EndPointAQ').val() + "ControlEvaluacion/Get/Evaluacion/Solicitud/" + $('#_SOLICITUD').val(), function (data, status) {
        var Arreglo_arreglosdot = [];
        for (var i = 0; i <= data.length - 1; i++) {
            var InternoArr = [];

            InternoArr.push('<strong><label style="text-transform: uppercase;">' + data[i].rfc + '</label></strong>');
            InternoArr.push('<textarea id="txt_Analisis_' + data[i].tbl_licitante_id + '" rows="3" class="form-control" >' + data[i].analisis + '</textarea>');
            InternoArr.push('<textarea id="txt_MotivoInc_' + data[i].tbl_licitante_id + '" rows="3" class="form-control" >' + data[i].motivo_incumplimiento + '</textarea>');

            ////////////////////////////////////////////////////////////////////////////////////////////////////

            var ganador_ = data[i].ganador ? 'checked' : '';
            var ECO_ = data[i].remitir_eval_eco ? 'checked' : '';
            var TEC_ = data[i].remitir_eval_tec ? 'checked' : '';
            var Cumplio_ = data[i].no_cumplio ? 'checked' : '';




            InternoArr.push(
                '<input type="radio" name="CEV_' + data[i].tbl_licitante_id + '" id="noCumplioCheck_' + data[i].tbl_licitante_id + '" ' + Cumplio_ +
                ' <label style="font-size:80%"> Incumplimiento </label> <br>'
                +
                '<input type="radio" name="CEV_' + data[i].tbl_licitante_id + '" id="ganadorCheck_' + data[i].tbl_licitante_id + '" ' + ganador_ +
                ' <label style="font-size:80%"> Ganador </label><br>'
                +
                '<input type="checkbox" id="remitirTecCheck_' + data[i].tbl_licitante_id + '" ' + TEC_ +
                ' class="editor-active"> <label style="font-size:80%"> Requiere Analisis Tec. </label><br>'
                +
                '<input type="checkbox" id="remitirEcoCheck_' + data[i].tbl_licitante_id + '"  ' + ECO_ +
                ' class="editor-active"><label style="font-size:80%"> Requiere Analisis Eco. </label>'
            );


            getURL

            //Descarga Archivos y guardado de Registro
            if (data[i].token_tec == null && data[i].token_eco == null) {
                InternoArr.push(
                    '<a onclick="getURL(\'' + data[i].token_tec + '\')" disabled class="fa fa-arrow-circle-o-down btn btn-primary" title="Propuesta técnica"></a>'
                    + '<label style="font-size:80%">&nbsp;Descargar Propuesta Tec</label></br>' +
                    '<a onclick="getURL(\'' + data[i].token_eco + '\')" disabled class="fa fa-arrow-circle-o-down btn btn-primary" title="Propuesta económica"></a>'
                    + '<label style="font-size:80%">&nbsp;Descargar Propuesta Eco</label></br></br>' +
                    '<a onclick="GuardarEv(\'' + data[i].tbl_licitante_id + '\')" class="fa fa-floppy-o btn btn-success" title="Guardar Evaluación"></a>'
                    + '<label style="font-size:80%">&nbsp;Guardar Propuesta</label>'
                );

            }
            else if (data[i].token_tec != null && data[i].token_eco == null) {
                InternoArr.push(
                    '<a onclick="getURL(\'' + data[i].token_tec + '\')"  class="fa fa-arrow-circle-o-down btn btn-primary" title="Propuesta técnica"></a>'
                    + '<label style="font-size:80%">&nbsp;Descargar Propuesta Tec</label></br>' +
                    '<a onclick="getURL(\'' + data[i].token_eco + '\')" disabled class="fa fa-arrow-circle-o-down btn btn-primary" title="Propuesta económica"></a>' 
                    + '<label style="font-size:80%">&nbsp;Descargar Propuesta Eco</label></br></br>' +
                    '<a onclick="GuardarEv(\'' + data[i].tbl_licitante_id + '\')" class="fa fa-floppy-o btn btn-success" title="Guardar Evaluación"></a>'
                    + '<label style="font-size:80%">&nbsp;Guardar Propuesta</label>'
                );

            }
            else if (data[i].token_tec == null && data[i].token_eco != null) {
                InternoArr.push(
                    '<a onclick="getURL(\'' + data[i].token_tec + '\')" disabled class="fa fa-arrow-circle-o-down btn btn-primary" title="Propuesta técnica"></a>'
                    + '<label style="font-size:80%">&nbsp;Descargar Propuesta Tec</label></br>' +
                    '<a onclick="getURL(\'' + data[i].token_eco + '\')" class="fa fa-arrow-circle-o-down btn btn-primary" title="Propuesta económica"></a>'
                    + '<label style="font-size:80%">&nbsp;Descargar Propuesta Eco</label></br></br>' +
                    '<a onclick="GuardarEv(\'' + data[i].tbl_licitante_id + '\')" class="fa fa-floppy-o btn btn-success" title="Guardar Evaluación"></a>'
                    + '<label style="font-size:80%">&nbsp;Guardar Propuesta</label>'
                );

            }
            else if (data[i].token_tec != null && data[i].token_eco != null) {
                InternoArr.push(
                    '<a onclick="getURL(\'' + data[i].token_tec + '\')" class="fa fa-arrow-circle-o-down btn btn-primary" title="Propuesta técnica"></a>'
                    + '<label style="font-size:80%">&nbsp;Descargar Propuesta Tec</label></br>' +
                    '<a onclick="getURL(\'' + data[i].token_eco + '\')" class="fa fa-arrow-circle-o-down btn btn-primary" title="Propuesta económica"></a>'
                    + '<label style="font-size:80%">&nbsp;Descargar Propuesta Eco</label></br></br>' +
                    '<a onclick="GuardarEv(\'' + data[i].tbl_licitante_id + '\')" class="fa fa-floppy-o btn btn-success" title="Guardar Evaluación"></a>'
                    + '<label style="font-size:80%">&nbsp;Guardar Propuesta</label>'
                );

            }






            Arreglo_arreglosdot.push(InternoArr);
        }

        var table = $('#tbl_EvaluacionPropuestas').DataTable();

        table.destroy();

        $('#tbl_EvaluacionPropuestas').DataTable({
            data: Arreglo_arreglosdot,
            pageLength: 5,
            columns: [
                { "width": "10%", title: "RFC" },
                { "width": "25%", title: "Análisis <br> Técnica/Económico" },
                { "width": "25%", title: "Motivo <br> incumplimiento" },

                { "width": "20%", title: "Ganador" },
                { "width": "20%", title: "Acción" }
            ],
            columnDefs: [
                {
                    targets: -1,
                    className: 'dt-body-left'
                }]
        });

    });

    ValidarEvalFallo();
}




function DescargarDoc(tokenDocto) {

    $.get("/Request/EvaluacionPropuesta/Licitantes/Propuesta/" + tokenDocto, function (data, status) {

        window.open(data, '_blank');

    });

}


function ValidarEvalFallo() {

    $.get($('#EndPointAQ').val() + "ControlEvaluacion/Get/Evaluacion/Solicitud/" + $('#_SOLICITUD').val(), function (data, status) {
        var contador_licitantes = 0;
        var contador_evaluaciones = 0;

        if (data != undefined) {

            contador_licitantes = data.length;

            for(var i = 0; i <= data.length - 1; i++) {
                if (data[i].remitir_eval_tec == false && data[i].remitir_eval_eco == false) {

                    if ((data[i].ganador == true && data[i].no_cumplio == false) || (data[i].ganador == false && data[i].no_cumplio == true)) {
                        contador_evaluaciones++;
                    }

                }

            }

            if ((contador_licitantes == contador_evaluaciones) && (contador_licitantes > 0) && (contador_licitantes > 0)) {
                $("#btn_AvanzaFase").prop('disabled', false);
            } else {
                $("#btn_AvanzaFase").prop('disabled', true);
            }

            
        }


    });
}


$("#btn_AvanzaFase").click(function () {
    AvanzaFaseActas();
});


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


function GuardarEv(item) {

    Add(item);

}

function Add(item) {
    var val = Valida(item)
    if (val.Bit) {
        Swal.fire({
            type: 'error',
            title: 'Hay un error en los datos de entrada',
            text: val.Texto
        });
        return;
    }

    var OBJ = EvaluacionPropuestaClass;

    var d = new Date();
    var date = (d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate());

    OBJ.p_opt = 2;
    OBJ.p_id = '00000000-0000-0000-0000-000000000000';
    OBJ.p_tbl_licitante_id = item;
    OBJ.p_analisis = $('#txt_Analisis_' + item + '').val();
    OBJ.p_no_cumplio = $('#noCumplioCheck_' + item + '').prop('checked') ? 1 : 0;
    OBJ.p_motivo_incumplimiento = $('#txt_MotivoInc_' + item + '').val();
    OBJ.p_remitir_eval_tec = $('#remitirTecCheck_' + item + '').prop('checked') ? 1 : 0;
    OBJ.p_remitir_eval_eco = $('#remitirEcoCheck_' + item + '').prop('checked') ? 1 : 0;
    OBJ.p_ganador = $('#ganadorCheck_' + item + '').prop('checked') ? 1 : 0;
    

    //form_data.append('Evaluacion', JSON.stringify(OBJ));

    $.ajax({

        dataType: 'json',  // what to expect back from the PHP script, if anything
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ),
        type: 'post',

        success: function (data) {
            //var obj = JSON.parse(data);

            if (data.cod == 'success') {
                Swal.fire({
                    type: 'success',
                    title: 'Guardado exitoso',

                })

                GetPropuestas();
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
        url: $('#EndPointAQ').val() + "ControlEvaluacion/Add" 
    });

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


function Valida(item) {
    var Response = { Texto: '', Bit: true, objeto: null };

    if ($('#txt_Analisis_' + item + '').val() == '') {
        Response.Texto = 'Debe agregar un "Análisis"';
        Response.Bit = true;
        return Response;
    }

    Response.Texto = '';
    Response.Bit = false;
    return Response;
}



var EvaluacionPropuestaClass = {
    p_opt: 2,
    p_id: '00000000-0000-0000-0000-000000000000',
    p_tbl_licitante_id: '00000000-0000-0000-0000-000000000000',
    p_analisis: '',
    p_no_cumplio: '',
    p_motivo_incumplimiento: '',
    p_remitir_eval_tec: '',
    p_remitir_eval_eco: '',
    p_ganador: ''
}


function GoBandeja() {
    window.location.href = "/Bandeja";
}