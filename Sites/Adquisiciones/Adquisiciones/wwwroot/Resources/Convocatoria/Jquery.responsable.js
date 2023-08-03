$(document).ready(function () {
    $("#tbl_responsable").DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        }
    });
    validar_responsable();
})

function Get_Areas() {
    var id_dep = $("#HDidDependencia").val();
    $.get($("#EndPointAQ").val() + "Convocatoria/Get/Areas/" + id_dep, function (data, status) {
        var Body = "<option value='' disabled selected>Selecciona una opción</option>"; 
        for (var i = 0; i <= data.length - 1; i++) {
            Body = Body + "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
        }
        $('.drop_area').html(Body);
    }, 'json');
}

function Get_Servidores_Pub(id_area) {
    $.get($("#EndPointAQ").val() + "Convocatoria/Get/Servidor/" + id_area, function (data, status) {
        var Body = "<option value='' disabled selected>Selecciona una opción</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            Body = Body + "<option value='" + data[i].value + "'>" + data[i].text + "</option>";
        }
        $('.drop_serpub').html(Body);
    }, 'json');
}

function validar_responsable() {
    $.get($("#EndPointAQ").val() + "Convocatoria/Get/Responsable/" + $("#_SOLICITUD").val(), function (data, status) {
        if (data.length > 0) {
            for (x = 0; x <= data.length - 1; x++) {
                Get_Areas();
                Get_Servidores_Pub(data[x].id_area);
                var id_area = data[x].id_area;
                var id_ser = data[x].tbl_servidor_publico_id;
                var token_ = data[x].token;
                setTimeout(function () {
                    $('.drop_area').val(id_area);
                    $('.drop_serpub').val(id_ser);
                    $('.drop_area').prop("disabled", true);
                    $('.drop_serpub').prop("disabled", true);
                    LaunchLoader(false);
                    $('#doc_file_resp').html('<a onclick="btnDowload(\'' + token_ + '\')" class="fa fa-arrow-circle-o-down"'
                        + 'title="Descargar archivo" style="cursor: pointer"> Descargar archivo </a>');
                    $('.btn_save_responsable').prop('disabled', true);
                }, 1500)
                
            }
        }
        else {
            Get_Areas();
            LaunchLoader(false);
        }
    });
}

function btnDowload(item) {
    getURL(item)
    modalVisualizacion();
}

function getURL(token_) {
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
        return RES_;
    });

}

function modalVisualizacion() {
    $('#viewer_window').modal('show');
}

$(".drop_area").change(function () {
    var id_area = $(".drop_area").val();
    Get_Servidores_Pub(id_area);
})

$(".btn_save_responsable").click(function () {
	Add_Responsable();
})

function Validar_resp() {
    var Response = { Texto: '', Bit: true, objeto: null };
    if ($('.drop_area').val() == null) {
        Response.Texto = 'Debe seleccionar un área.';
        Response.Bit = true;
        return Response;
    }
    if ($('.drop_serpub').val() == null) {
        Response.Texto = 'Debe seleccionar un responsable.';
        Response.Bit = true;
        return Response;
    }

    var countFiles = $('.file_responsable').prop('files').length;
    if (countFiles == 0) {
        Response.Texto = "No se ha cargado ningún documento.";
        Response.Bit = true;
        return Response;
    }
    Response.Texto = '';
    Response.Bit = false;

    return Response;
}

function Add_Responsable() {

    var Validacion = Validar_resp();

    if (Validacion.Bit) {
        return ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }

    var obj_resp = tbl_responsable; 
    var countFiles = $('.file_responsable').prop('files').length;
    obj_resp.p_tbl_servidor_publico_id = $('.drop_serpub').val();
    obj_resp.p_tbl_convocatoria_id = $('#id_convocatoria').val();

    var form_data_file = new FormData();
    var file_data = $('.file_responsable').prop('files')[0];

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
            obj_resp.p_token = token;


        },
        error: function (data) {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse);
        }
    });

    $.ajax({
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(obj_resp),
        type: 'post',

        success: function (data) {
            var objresponse = JSON.parse(data);
            if (objresponse.cod == "success") {
                SuccessSA("Operación exitosa", "El archivo se guardo correctamente");
                validar_responsable();
            }
            else {
                ErrorSA("", "Ocurrio un error.");
            }
        },
        error: function () {
            ErrorSA('', "Ocurrio un error.")
        },
        processData: false,
        type: 'POST',
        url: $("#EndPointAQ").val() + "Convocatoria/Add/Responsable"

    });
	
}

var tbl_responsable = {
    p_opt: 0,
    p_id:"00000000-0000-0000-0000-000000000000",                      
    p_tbl_servidor_publico_id:"00000000-0000-0000-0000-000000000000",
    p_token:"",         
    p_tbl_convocatoria_id:"00000000-0000-0000-0000-000000000000"        
}