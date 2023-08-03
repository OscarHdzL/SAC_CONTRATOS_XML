$(document).ready(function () {
    $('#tbl_ListaEvaluados').DataTable({
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" },
            { "width": "10%", "targets": 0 },
            //{ "width": "10%", "targets": 1 },
            { "width": "35%", "targets": 1 },
            { "width": "35%", "targets": 2 },
            { "width": "10%", "targets": 3 },
           
        ]
    });
    var idSol = $('#_SOLICITUD').val();
    var tipo = $('#TipoP').val();
    GetProposionesEva(idSol, tipo);
});
function ListarPT() {

    var idSol = $('#_SOLICITUD').val();
    var tipo = $('#TipoP').val();
    GetProposionesEva(idSol, tipo);

    var table = $('#tbl_ListaEvaluados').DataTable();
    if (!table.data().any()) {
        Swal.fire({
            type: 'info',
            title: 'No se encontraron registros'
        });
    }
    else {
        $('#ModalTablaEvaluados').modal('show');
    }
}
function Editar(item) {
    if ($('.text').prop('disabled', true)) {

        $('#txt2_analisis_' + item + '').prop('disabled', false);
        $('#txt3_justificacion_' + item + '').prop('disabled', false);
    }
    else {
        $('.text').prop('disabled', true)
    }

}
function Valida(item) {
    var Response = { Texto: '', Bit: true, objeto: null };

    if ($('#txt2_analisis_' + item + '').val() == '') {
        Response.Texto = 'Debe agregar un comentario en el campo "Análisis"';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#txt2_analisis_' + item + '').val(), 'Análisis') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Análisis"';
        Response.Bit = true;
        return Response;
    }
    if ($('#txt3_justificacion_' + item + '').val() == '') {
        Response.Texto = 'Debe agregar una justificación';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#txt3_justificacion_' + item + '').val(), 'Justificación') != '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Justificación"';
        Response.Bit = true;
        return Response;
    }
    Response.Texto = '';
    Response.Bit = false;
    return Response;
}
function AddEditado(idProp, idLic, est) {
    AddM(idProp, idLic, est);
}

function AddM(idProp, idLic, est) {
    var val = Valida(idLic)
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

    OBJ.p_opt = 3;
    OBJ.p_id = idProp;
    OBJ.p_tbl_licitante_id = idLic;
    OBJ.p_cumplimiento = est;
    OBJ.p_analisis = btoa($('#txt2_analisis_' + idLic + '').val());
    OBJ.p_justificacion = btoa($('#txt3_justificacion_' + idLic + '').val());
    OBJ.p_tipo_proposicion = $('#TipoP').val();



    $.ajax({

        dataType: 'json',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ),
        type: 'post',

        success: function (data) {
            if (data.cod=='success') {
                Swal.fire({
                    type: 'success',
                    title: 'Cambios guardados'
                })

                $('.text').prop('disabled', true);

                var idSol = $('#_SOLICITUD').val();
                var tipo = $('#TipoP').val();
                GetProposionesEva(idSol, tipo);
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
        url: $('#EndPointAQ').val() + 'Proposiciones/Evaluadas/Update'
    })

}
function GetProposionesEva(idSol, Tipo) {

    $.get($('#EndPointAQ').val() + 'Proposiciones/Get/Evaluadas/Solicitud/' + idSol + "/Tipo/" + Tipo, function (data, status) {
        var Arreglo_arreglos = [];
        for (var i = 0; i <= data.length - 1; i++) {
            var Interno = [];

            Interno.push(data[i].licitante);
            //Interno.push(data[i].cumplimiento == true ? 'Aceptada' : 'Rechazada');
            Interno.push('<textarea id="txt2_analisis_' + data[i].tbl_licitante_id + '" rows="2" class="form-control text" style=" resize: none" disabled>' + atob(data[i].analisis) + '</textarea>');
            Interno.push('<textarea id="txt3_justificacion_' + data[i].tbl_licitante_id + '" rows="2" class="form-control text" style=" resize: none" disabled>' + atob(data[i].justificacion) + '</textarea>');
            //Interno.push(data[i].inclusion);
            Interno.push('<a onclick="Editar(\'' + data[i].tbl_licitante_id + '\')" class="fa fa-edit btn btn-success" title="Modificar registro"></a> <a onclick="AddEditado(\'' + data[i].tbl_proposicion_tecnica_economica_id + '\', \'' + data[i].tbl_licitante_id + '\',\'' + (data[i].cumplimiento ? 1 : 0) + '\')"   class="fa fa-floppy-o btn btn-primary" title="Guardar cambios"></a>');
            Arreglo_arreglos.push(Interno);
        }

        var table = $('#tbl_ListaEvaluados').DataTable();

        table.destroy();
        console.log(Arreglo_arreglos);
        $('#tbl_ListaEvaluados').DataTable({
            data: Arreglo_arreglos,
            columns: [
                { title: "Licitante" },
                //{ title: "Estatus" },
                { title: "Analisis" },
                { title: "Justificación" },
                { title: "Acciones" }
            ]
        });

    });

}