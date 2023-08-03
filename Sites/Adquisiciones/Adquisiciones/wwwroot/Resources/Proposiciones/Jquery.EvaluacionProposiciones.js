
$(document).ready(function () {
    $('#tbl_Proposiciones').DataTable({

        "columnDefs": [
            { "className": "dt-center", "targets": "_all" }
        ],

    });
    var id = $('#_SOLICITUD').val();
    var tipo = $('#TipoP').val();
    GetListaLicitantes(id, tipo);

});

function GuardarEv(item) {
    Add(item);
}

function Valida1(item) {
    var Response = { Texto: '', Bit: true, objeto: null };

    if ($('#txt_analisis_' + item + '').val() == '') {
        Response.Texto = 'Debe agregar un comentario en el campo "Análisis"';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#txt_analisis_' + item).val(), 'Análisis') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Análisis"';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt_justificacion_' + item + '').val() == '') {
        Response.Texto = 'Debe agregar una justificación';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#txt_justificacion_' + item).val(), 'Justificación') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Justificación"';
        Response.Bit = true;
        return Response;
    }
    Response.Texto = '';
    Response.Bit = false;
    return Response;
}

function Add(item) {
    var val = Valida1(item)
    if (val.Bit) {
        Swal.fire({
            type: 'error',
            title: 'Hay un error en los datos de entrada',
            text: val.Texto
        });
        return;
    }

    var OBJ = ProposicionesClass;

    var d = new Date();
    var date = (d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate());


    OBJ.p_opt = 2;
    OBJ.p_id = '00000000-0000-0000-0000-000000000000';
    OBJ.p_tbl_licitante_id = item;
    OBJ.p_cumplimiento = 0;
    OBJ.p_analisis = btoa($('#txt_analisis_' + item + '').val());
    OBJ.p_justificacion = btoa($('#txt_justificacion_' + item + '').val());
    OBJ.p_tipo_proposicion = $('#TipoP').val();


    $.ajax({

        dataType: 'json',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ),
        type: 'post',

        success: function (data) {
            if (data.cod == 'success') {
                Swal.fire({
                    type: 'success',
                    title: 'Registro agregado'
                })

                var id = $('#_SOLICITUD').val();
                var tipo = $('#TipoP').val();
                GetListaLicitantes(id, tipo);

                var idLic = $('#_SOLICITUD').val();
                var tipo = $('#TipoP').val();
                GetProposionesEva(idLic, tipo);
            }
        },
        error: function () {
            Swal.fire({
                type: 'error',
                title: 'Error al guardar el registro'
            })
        },
        processData: false,
        type: 'POST',
        url: $('#EndPointAQ').val() + 'Proposiciones/Evaluadas/Add'
    })

}

$('#CancelarE').click(function () {
    if (location.href.toLowerCase().indexOf('proposiciones/evaluaciontecnica') > 0) {
        window.location.href = "/BandejaRemitida/Tecnica";
    }
    if (location.href.toLowerCase().indexOf('proposiciones/evaluacioneconomica') > 0) {
        window.location.href = "/BandejaRemitida/Economica";
    }

})


function GetListaLicitantes(idSol, Tipo) {

    $.get($('#EndPointAQ').val() + 'Proposiciones/Get/Solicitud/' + idSol + "/Tipo/" + Tipo, function (data, status) {
        var Arreglo_arreglos = [];
        for (var i = 0; i <= data.length - 1; i++) {
            var Interno = [];

            Interno.push(data[i].rfc);

            //Interno.push('<label">Aceptada</label> <input type="checkbox" id="cumplimientoCheck_' + data[i].idLicitante + '" class="editor-active" >');
            Interno.push('<textarea id="txt_analisis_' + data[i].tbl_licitante_id + '" rows="2" class="form-control" style=" resize: none" ></textarea>');
            Interno.push('<textarea id="txt_justificacion_' + data[i].tbl_licitante_id + '" rows="2" class="form-control" style=" resize: none" ></textarea>');
            Interno.push('<a onclick="GuardarEv(\'' + data[i].tbl_licitante_id + '\')" class="fa fa-floppy-o btn btn-success" title="Guardar Evaluación"></a> <a onclick="getURL(\'' + data[i].token_propuesta + '\')"   class="fa fa-arrow-circle-o-down btn btn-primary" title="Descargar archivo"></a>');
            //Interno.push('<a onclick="DescargarFM(\'' + data[i].idDocumento + '\',\'' + data[i].idCotizacion + '\')" id="Descargarfm_' + data[i].idCotizacion + '"   class="fa fa-arrow-circle-o-down btn btn-primary" title="Descargar archivo"></a>');
            Arreglo_arreglos.push(Interno);
        }

        var table = $('#tbl_Proposiciones').DataTable();

        table.destroy();
        console.log(Arreglo_arreglos);
        $('#tbl_Proposiciones').DataTable({
            data: Arreglo_arreglos,
            columns: [
                { title: "RFC" },
                //{ title: "Estatus" },
                { title: "Análisis " },
                { title: "Justificación" },
                { title: "Acciones" }

            ]
        });

    });

}

var ProposicionesClass = {
    p_opt: null,
    p_id: null,
    p_tbl_licitante_id: null,
    p_cumplimiento: null,
    p_analisis: null,
    p_justificacion: null,
    p_tipo_proposicion: null
}

//Esto es solo para el modal

$('.modal-child').on('show.bs.modal', function () {
    var modalParent = $(this).attr('data-modal-parent');
    $(modalParent).css('opacity', 0);
});

$('.modal-child').on('hidden.bs.modal', function () {
    var modalParent = $(this).attr('data-modal-parent');
    $(modalParent).css('opacity', 1);
});



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
