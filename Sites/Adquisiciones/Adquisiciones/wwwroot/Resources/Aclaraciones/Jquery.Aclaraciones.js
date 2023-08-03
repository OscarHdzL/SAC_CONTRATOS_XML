var con = $('#EndPointAQ').val();

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
        $('.icheckbox_minimal-blue').hide();

    }, 100);

    //inicialización Datetime picker
    $('.txt_FechaAclaracion').datetimepicker({
        format: 'YYYY-MM-DD',
        locale: 'es'
    });
    //Llena tabla historico
    GetJuntasAclaraciones();
    //Llena numero de junta
    //var resp = GetNumeroJunta();

});


//Entidad
var JuntaAclaracionClass = {
    id: null,
    tbl_solicitud_id: null,
    numero_junta: null,
    aclaracion: null,
    fecha_aclaracion: null,
    req_junta: null
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


//Agregar
$(".btn-save-aclaracion").click(function () {


    var ObjValido = validateAclaracion();

    if (ObjValido.Bit) {
        Swal.fire({
            type: 'error',
            title: 'Hay un error en los datos de entrada',
            text: ObjValido.Texto
        });
        return;
    }



    $.ajax({

        dataType: 'json',  // what to expect back from the PHP script, if anything
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(ObjValido.objeto),
        type: 'post',

        success: function (data) {
            if (!data.Bit) {

                GetJuntasAclaraciones();
                //GetNumeroJunta();
                $('#txt_Aclaracion').val('');

                $('.txt_FechaAclaracion').val('');

                $('#cbx_ReqJunta').prop('checked', false);
                $('.icheckbox_minimal-blue').removeClass("checked");

                //$('#cbx_ReqJunta').prop('checked',true);
                //$('.icheckbox_minimal-blue').addClass("checked");

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
        url: con + "JuntaAclaracion/Add_Junta"
    });




});

function GetNumeroJunta(data) {

    //$.get(con + "JuntaAclaracion/Get_Juntas/" + $('#idSolicitud_Aclaraciones').val(), function (data, status) {
        for (var i = 0; i <= data.length - 1; i++) {
            var conteo = data.length;
        }
    if (conteo == undefined) {
            //comienza en uno
            $('#txt_NumJunta').val(1);

        }
        else {
            //Junta actual + 1
            $('#txt_NumJunta').val(conteo + 1);
        }
   // });

}


//Validaciones campos
function validateAclaracion() {



    var Response = { Texto: '', Bit: true, objeto: null };
    //Nombre
    if ($('.txt_FechaAclaracion').val() == '') {
        Response.Texto = 'Debe ingresar fecha';
        Response.Bit = true;
        return Response;
    }



    if ($('#txt_NumJunta').val() == '') {
        Response.Texto = 'No se cargó el número de junta';
        Response.Bit = true;
        return Response;
    }

    if ($('#txt_Aclaracion').val() == '') {
        Response.Texto = 'Debe ingresar aclaración ';
        Response.Bit = true;
        return Response;
    }

    var objJunta = JuntaAclaracionClass;

    objJunta.id = '';
    objJunta.tbl_solicitud_id = $('#_SOLICITUD').val();
    objJunta.numero_junta = $('#txt_NumJunta').val();
    objJunta.aclaracion = btoa($('#txt_Aclaracion').val());
    objJunta.fecha_aclaracion = $('.txt_FechaAclaracion').val();
    var i = $('#cbx_ReqJunta').is(":checked") ? 1 : 0;
    objJunta.req_junta = i;

    var form_data = new FormData();
    form_data.append('objJunta', JSON.stringify(objJunta));

    Response.Texto = '';
    Response.Bit = false;
    Response.objeto = objJunta;
    return Response;

}



//function MostrarAclaracion() {

//    var TextoAclaracion;
//    function CallEvents() {
//        $('#ModalAclaracion').modal('show');
//        $('#textAclaracion').text(TextoAclaracion);
//    }


//};






//Mostrar la aclaracion en modal
function muestraAclaracion(valor) {
    $('#textAclaracion').html(atob(valor));
    $('#ModalAclaracion').modal('show');
}

function GetJuntasAclaraciones() {

    $.get(con + "JuntaAclaracion/Get_Juntas/" + $('#_SOLICITUD').val(), function (data, status) {
        var Arreglo_arreglosdot = [];
        for (var i = 0; i <= data.length - 1; i++) {
            var HistoricoArr = [];

            HistoricoArr.push(data[i].fecha_aclaracion);
            HistoricoArr.push(data[i].num_solicitud);
            HistoricoArr.push(data[i].numero_junta);
            HistoricoArr.push("<button class='btn btn-success' onclick=\"muestraAclaracion(\'" + data[i].aclaracion + "\')\">Ver aclaración</button>");
            // 
            Arreglo_arreglosdot.push(HistoricoArr);
        }
        GetNumeroJunta(data)
        var table = $('#tbl_Historico_Aclaraciones').DataTable();

        table.destroy();

        $('#tbl_Historico_Aclaraciones').DataTable({
            data: Arreglo_arreglosdot,
            columns: [
                { title: "Fecha" },
                { title: "Número solicitud" },
                { title: "Número junta" },
                { title: "Aclaración" }
            ],
            columnDefs: [
                {
                    targets: -1,
                    className: 'dt-body-center'
                }]
        });
    });
}

function GoBandeja() {
    window.location.href = "/Bandeja";
}
