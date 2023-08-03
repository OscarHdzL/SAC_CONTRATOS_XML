
$(document).ready(function () {
    $('#tbl_Ganadores, #tbl_incumplimiento').DataTable({
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" }
        ],
    });
    var idLic = $('#_SOLICITUD').val();
    GetPropuestas(idLic);
    ValidarAvanceFase();
});

function GetPropuestas(idLic) {
    
    $.get($('#EndPointAQ').val() + "Fallo/Proposiciones/Get/" + idLic, function (data, status) {
        GetGanadores(data);
        GetIncumplimiento(data);
    });
}

function GoBandeja() {
    window.location.href = "/Bandeja";
}


function GenerarFallo() {

    var IdSolicitud = $('#_SOLICITUD').val();

    //actualizacion de servicio JC
    //var baseline = [];
    //var obj = {
    //    p_capitulo: $('#p_capitulo').html(),
    //    p_capitulo_des: $('#p_capitulo_des').html(),
    //    monto_por_ejercer: $('#monto_por_ejercer').val(),
    //    des_personal: $('#txt_numero_ejercer').val(),
    //    des_numero: $('#txt_numero_descripcion').val(),
    //    idcapgast: $('#codSeleccionado').val(),
    //    areaSeleccionada: $('#areaSeleccionada').val(),
    //    dependencia: $('#HDidDependencia').val()
    //};
    //baseline.push(obj);
    //var Presupuestos_str = JSON.stringify(baseline);
    //var form_data_file = new FormData();

    //form_data_file.append('LstPres', Presupuestos_str);    

    //$.ajax({
    //    url: "Fallo/OperaContrato/" + IdSolicitud,
    //    //url: $('#EndPointAQ').val() + "Fallo/OperaContrato/" + IdSolicitud,
    //    dataType: 'text',
    //    cache: false,
    //    contentType: false,
    //    processData: false,
    //    data: form_data_file,
    //    type: 'POST',
    //    async: false,
    //    success: function (data) {
    //        var uplo__ = uploadfile_();

    //        obj.p_token = uplo__;
    //        obj.p_id = msg_;
    //        var uri = $('#EndPointAdmon').val() + 'Contratos/Fase/formdata/' + msg_;
    //        $.ajax({
    //            dataType: 'text',
    //            cache: false,
    //            contentType: 'application/json',
    //            processData: false,
    //            data: JSON.stringify(obj),
    //            type: 'post',
    //            success: function (data) {
    //                Swal.fire({
    //                    type: 'success',
    //                    title: 'Operación Completada',
    //                    text: 'Contrato guardado con exito.'
    //                })
    //            },
    //            error: function (data) {
    //            },
    //            processData: false,
    //            type: 'POST',
    //            url: uri

    //        });
    //    },
    //    error: function (data) {
    //        var objresponse = JSON.parse(data);
    //        response_ = '';
    //    }
    //});
    //return response_;


    $.post($('#EndPointAQ').val() +"Fallo/OperaContrato/" + IdSolicitud, function (data, status) {
        //alert(data)
        if (data.cod == 'success') {
            Swal.fire({
                type: 'success',
                title: 'Éxitoso',
                text: "El fallo se generó correctamente"
            }).then((result) => {
                if (result.value) {
                    window.location.href = "/Bandeja";
                    window.open('/Contrato.pdf', '_blank');
                }
            })
        }
        else {
            Swal.fire({
                type: 'error',
                title: 'Error',
                text: "Ocurrio un error al guardar"
            })
        }
    });

}


function GetGanadores(data) {

    //$.get("/Request/Proposiciones/GetGanadores/" + idLic, function (data, status) {
        var Arreglo_arreglos = [];
        for (var i = 0; i <= data.length - 1; i++) {
            var Interno = [];
            if (data[i].ganador == true) {
                $("#idProveedor").val(data[i].rfc);
                Interno.push(data[i].licitante);
                Interno.push(data[i].nombre);
                Arreglo_arreglos.push(Interno);
            }
        }

        var table = $('#tbl_Ganadores').DataTable();

        table.destroy();
        console.log(Arreglo_arreglos);
        $('#tbl_Ganadores').DataTable({
            data: Arreglo_arreglos,
            columns: [
                { title: "Licitante" },
                { title: "Representante legal" }
            ]
        });

    //});

}
function GetIncumplimiento(data) {

    //$.get("/Request/Proposiciones/GetIncumplimiento/" + idLic, function (data, status) {
        var Arreglo_arreglos = [];
        for (var i = 0; i <= data.length - 1; i++) {
            var Interno = [];
            if (data[i].ganador == false) {
                Interno.push(data[i].licitante);
                Interno.push(data[i].nombre);
                Arreglo_arreglos.push(Interno);
            }
        }

        var table = $('#tbl_incumplimiento').DataTable();

        table.destroy();
        console.log(Arreglo_arreglos);
        $('#tbl_incumplimiento').DataTable({
            data: Arreglo_arreglos,
            columns: [
                { title: "Licitante" },
                { title: "Representante legal" }
            ]
        });

    //});

}


function GenerarContrato(IdSolicitud) {

    $.get("/Request/LlenadoContrato/get/" + IdSolicitud, function (data, status) {
        if (data.Bit) {

            //alert(data.Descripcion);
            GuardarContrato(data.Descripcion);

        }
    });

}

function GuardarContrato(json) {


    //var url = "http://localhost:59984/GuardaContrato/index";


    var url = $('#EndPointJavaScript').val() + 'GuardarContrato';

    $.ajax({
        url: url,
        data: $.parseJSON(json),
        cache: false,
        type: "POST",
        success: function (data) {
            //alert("ok");
            window.location.href = "/Bandeja";

        },
        error: function (data) {
            Swal.fire({
                type: 'error',
                title: 'Error al guardar ',
                text: data.Excepcion
            })
        }
    });
}

$('#Cerra15').click(function () {
    window.location.href = "/Bandeja";
})