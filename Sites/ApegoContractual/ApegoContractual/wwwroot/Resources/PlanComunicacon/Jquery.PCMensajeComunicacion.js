var URL_SERVICIO_BASE = URL_AGREGAR_MENSAJE_COMUNICACION = URL_OBTENER_MENSAJES_COMUNICACION =
    URL_OBTENER_MENSAJE_COMUNICACION_POR_ID = URL_ELIMINAR_MENSAJE_COMUNICACION = URL_OBTENER_TIPOS_INFORMACION =
    URL_OBTENER_RESPONSABLES = URL_OBTENER_AUDIENCIAS = URL_OBTENER_CANALES = "";

var MensajeClass =
{
    id: null,
    TBLENT_CONTRATO_id: null,
    FuenteInfo_RespApego_Contrato_ac: null,
    tbl_TipoInformacion_id: null,
    tbl_TipoAudiencia_id: null,
    tbl_Canal_id: null,
    Destinatario_RespApego_Contrato_ac: null,
    Mensaje: null,
    Inclusion: null,
    Estatus: null
};

(function () {
    LaunchLoader(true);
})();

$(document).ready(function () {

    establecerRutasServicios();
    
    AjusteTablaMensajes();
    GetMensajes();
    GetDropTiposInformacion();

    GetDropResponsables();
    GetDropTiposAudiencia();
    GetDropCanal();
});

$('#AddMensaje').click(function () {
    AddMensaje();
});

function establecerRutasServicios() {
  
    var idContrato = $('#IdContrato').val();
    //idContrato = "1a826236-37b7-11ea-82d7-00155d1b3502"; //hardcode

    URL_SERVICIO_BASE                           = $("#EndPointAC").val();
    
    URL_AGREGAR_MENSAJE_COMUNICACION            = URL_SERVICIO_BASE + "/Request/MensajeComunicacion/Add";
    URL_OBTENER_MENSAJES_COMUNICACION           = URL_SERVICIO_BASE + "/Request/MensajesComunicacion/GetLista/" + idContrato;
    URL_OBTENER_MENSAJE_COMUNICACION_POR_ID     = URL_SERVICIO_BASE + "/Request/MensajeComunicacion/Get/  - idMensaje";
    URL_ELIMINAR_MENSAJE_COMUNICACION           = URL_SERVICIO_BASE + "/Request/MensajeComunicacion/Delete/ - idMensaje";
    URL_OBTENER_TIPOS_INFORMACION               = URL_SERVICIO_BASE + "/Request/TipoInformacion/Get/";
    URL_OBTENER_RESPONSABLES                    = URL_SERVICIO_BASE + "/Request/ResponsablesApego/Contrato/" + idContrato; 
    URL_OBTENER_AUDIENCIAS                      = URL_SERVICIO_BASE + "SerTipoAudiencia/Get";
    URL_OBTENER_CANALES                         = URL_SERVICIO_BASE + "SerCanal/Get";
}

function AjusteTablaMensajes() {
    $('#tbl_MensajesComunicacion').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" },
            { "width": "1%", "targets": 0 }
        ]
    });
}

function ModalAgregarMensaje() {
    $('.Clean').val('');

    GetDropTiposInformacion();
    GetDropResponsables();
    GetDropTiposAudiencia();
    GetDropCanal();

    $('#idMensaje').val('');
    $('#TxtMensaje').val('');
    $('#TitleModalMensajeC').text('Agregar mensaje de comunicación');
    $('#AddMensaje').text('Guardar');
    $('#ModalMensajeComunicacion').modal({ backdrop: 'static', keyboard: false });
    $('#ModalMensajeComunicacion').modal('show');
}

function ValidarMensaje() {

    var Response = { Texto: '', Bit: true, objeto: null };

    if ($('#TxtMensaje').val() === '') {
        Response.Texto = 'Debe agregar un mensaje';
        Response.Bit = true;
        return Response;
    }
    else if (ValidaCadena($('#TxtMensaje').val(), '') !== '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Mensaje"';
        Response.Bit = true;
        return Response;
    }

    if ($('#DropTipoInformacion').val() === ('' || null)) {
        Response.Texto = 'Debe seleccionar un tipo de información';
        Response.Bit = true;
        return Response;
    }
    if ($('#DropRespFuenteInfo').val() === ('' || null)) {
        Response.Texto = 'Debe seleccionar un responsable de fuente de información';
        Response.Bit = true;
        return Response;
    }

    if ($('#DropTipoAudiencia').val() === ('' || null)) {
        Response.Texto = 'Debe seleccionar un tipo de audiencia';
        Response.Bit = true;
        return Response;
    }

    if ($('#DropCanal').val() === ('' || null)) {
        Response.Texto = 'Debe seleccionar un canal';
        Response.Bit = true;
        return Response;
    }
    if ($('#DropDestinatario').val() === ('' || null)) {
        Response.Texto = 'Debe seleccionar un destinatario';
        Response.Bit = true;
        return Response;
    }

    Response.Texto = '';
    Response.Bit = false;
    return Response;
}

function CerrarModalMensaje() {
    $('.clean').val('');
    $('#ModalMensajeComunicacion').modal('hide');

    GetDropTiposInformacion();
    GetDropResponsables();
    GetDropTiposAudiencia();
    GetDropCanal();
}

function AddMensaje() {
    var ValidacionMensaje = ValidarMensaje();

    if (ValidacionMensaje.Bit) {
        return ErrorSA('Error en los datos de entrada', ValidacionMensaje.Texto);
    }

    var OBJ_Mensaje = MensajeClass;
    var d = new Date();

    var date = d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate();

    OBJ_Mensaje.id = $('#idMensaje').val() === '' ? '00000000-0000-0000-0000-000000000000' : $('#idMensaje').val();

    OBJ_Mensaje.TBLENT_CONTRATO_id = $('#idContratoMensaje').val();
    OBJ_Mensaje.tbl_TipoInformacion_id = $('#DropTipoInformacion').val();
    OBJ_Mensaje.FuenteInfo_RespApego_Contrato_ac = $('#DropRespFuenteInfo').val();
    OBJ_Mensaje.tbl_TipoAudiencia_id = $('#DropTipoAudiencia').val();
    OBJ_Mensaje.tbl_Canal_id = $('#DropCanal').val();
    OBJ_Mensaje.Destinatario_RespApego_Contrato_ac = $('#DropDestinatario').val();
    OBJ_Mensaje.Mensaje = $('#TxtMensaje').val();
    OBJ_Mensaje.Inclusion = date;
    OBJ_Mensaje.Estatus = 1;

    var form_data = new FormData();
    form_data.append('MensajeForm', JSON.stringify(OBJ_Mensaje));

    $.ajax({
        url: URL_AGREGAR_MENSAJE_COMUNICACION,
        dataType: 'text',
        cache: false,
        contentType: false,
        processData: false,
        data: form_data,
        type: 'POST',
        success: function (data) {
            var objresponse = JSON.parse(data);

            if (!objresponse.Bit) {
                SuccessSA("Operación exitosa", "El registro se guardo correctamente");

                $('.Clean').val('');

                function Confirmacion() {
                    return CerrarModalMensaje();
                }
                var AccionSi = eval(Confirmacion);

                SuccessSAAction("Operación exitosa", "El registro se guardó correctamente", AccionSi);

                GetMensajes();
            }
            else {
                ErrorSA("", objresponse.Descripcion);
            }
        },
        error: function () {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse.Descripcion);
        }
    });
}

function GetMensajes() {
    var idcon = $('#idContratoMensaje').val();

    $.get(URL_OBTENER_MENSAJES_COMUNICACION, function (data, status) {
        var lista = [];

        for (var i = 0; i <= data.length - 1; i++) {
            var item = [];

            item.push(data[i].tbl_TipoInformacion.TipoInformacion);
            item.push(data[i].tbl_RespApego_Contrato_ac1.tbl_RespApego_SerPub_ac.TBLENT_SERVIDOR_PUBLICO.NOMBRE + ' ' +
                data[i].tbl_RespApego_Contrato_ac.tbl_RespApego_SerPub_ac.TBLENT_SERVIDOR_PUBLICO.AP_PATERNO + ' ' +
                data[i].tbl_RespApego_Contrato_ac.tbl_RespApego_SerPub_ac.TBLENT_SERVIDOR_PUBLICO.AP_MATERNO);

            item.push(data[i].tbl_TipoAudiencia.TipoAudiencia);
            item.push(data[i].tbl_Canal.Canal);
            item.push(data[i].tbl_RespApego_Contrato_ac.tbl_RespApego_SerPub_ac.TBLENT_SERVIDOR_PUBLICO.NOMBRE + ' ' +
                data[i].tbl_RespApego_Contrato_ac.tbl_RespApego_SerPub_ac.TBLENT_SERVIDOR_PUBLICO.AP_PATERNO + ' ' +
                data[i].tbl_RespApego_Contrato_ac.tbl_RespApego_SerPub_ac.TBLENT_SERVIDOR_PUBLICO.AP_MATERNO);
            item.push(data[i].Mensaje);
            //item.push('<a onclick="btnEditMensaje(\'' + data[i].id + '\')" class="fa fa-pencil-square-o btn btn-primary" title="Modificar registro"> </a> <a onclick="btnDeleteMensaje(\'' + data[i].id + '\')" class="fa fa-trash-o btn btn-danger" title="Eliminar registro"> </a>');
            item.push('<a onclick="btnEditMensaje(\'' + data[i].id + '\')" class="btn btn-sm btn-info" title="Modificar registro"><span class="glyphicon glyphicon-edit"></span></a> <a onclick="btnDeleteMensaje(\'' + data[i].id + '\')" class="btn btn-sm btn-danger" title="Eliminar registro"><span class="glyphicon glyphicon-trash"></span></a>');

            lista.push(item);
        }

        var table = $('#tbl_MensajesComunicacion').DataTable();

        table.destroy();

        $('#tbl_MensajesComunicacion').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            data: lista,
            columns: [
                { title: "Tipo de información" },
                { title: "Responsable de fuente de información" },
                { title: "Tipo de audiencia" },
                { title: "Canal" },
                { title: "Destinatario     " },
                { title: "Mensaje          " },
                { title: "Acción      " }

            ]
        });

        LaunchLoader(false);
    });
}

function btnEditMensaje(idMensaje) {
    $('#idMensaje').val(idMensaje);
    $('#TitleModalMensajeC').text('Modificar mensaje comunicación');
    $('#AddMensaje').text('Guardar');
    $('#ModalMensajeComunicacion').modal({ backdrop: 'static', keyboard: false });
    $('#ModalMensajeComunicacion').modal('show');

    $.get(URL_OBTENER_MENSAJE_COMUNICACION_POR_ID + idMensaje, function (data, status) {

        $('#DropTipoInformacion > option[value="' + data.tbl_TipoInformacion_id + '"]').attr('selected', 'selected');
        $('#DropRespFuenteInfo> option[value="' + data.FuenteInfo_RespApego_Contrato_ac + '"]').attr('selected', 'selected');
        $('#DropTipoAudiencia> option[value="' + data.tbl_TipoAudiencia_id + '"]').attr('selected', 'selected');
        $('#DropCanal> option[value="' + data.tbl_Canal_id + '"]').attr('selected', 'selected');
        $('#DropDestinatario> option[value="' + data.Destinatario_RespApego_Contrato_ac + '"]').attr('selected', 'selected');
        $('#TxtMensaje').val(data.Mensaje);
    });
}

function EliminarMensaje(item) {
    $.post(URL_ELIMINAR_MENSAJE_COMUNICACION + item, function (data, status) {
        var objresponse = JSON.parse(data);

        if (status === 'success') {
            SuccessSA('', 'El registro se eliminó exitosamente');
            GetMensajes();
        }
        else {
            ErrorSA('', objresponse.Descripcion);
        }
    });
}

function btnDeleteMensaje(idMensaje) {
    function Confirmacion() {
        return EliminarMensaje(idMensaje);
    }

    var AccionSi = eval(Confirmacion);
    WarningSA('', '¿En verdad desea eliminar el registro?', 'Si, Continuar', 'No, Cancelar', AccionSi, '');
}

function GetDropTiposInformacion() {

    $.get(URL_OBTENER_TIPOS_INFORMACION, function (data, status) {
        var BodyC = "<option disabled selected>Selecciona una opción</option>";

        for (var i = 0; i <= data.length - 1; i++) {
            BodyC = BodyC + "<option value='" + data[i].id + "'>" + data[i].TipoInformacion + "</option>";
        }

        $('#DropTipoInformacion').html(BodyC);
    }, 'json');
}

function GetDropResponsables() {

    $.get(URL_OBTENER_RESPONSABLES, function (data, status) {
        var Body = "<option disabled selected>Selecciona una opción</option>";

        for (var i = 0; i <= data.length - 1; i++) {
            Body = Body + "<option value='" + data[i].id + "'>" + data[i].tbl_RespApego_SerPub_ac.TBLENT_SERVIDOR_PUBLICO.NOMBRE + ' ' + data[i].tbl_RespApego_SerPub_ac.TBLENT_SERVIDOR_PUBLICO.AP_PATERNO + ' ' + data[i].tbl_RespApego_SerPub_ac.TBLENT_SERVIDOR_PUBLICO.AP_MATERNO + "</option>";
        }

        $('#DropRespFuenteInfo').html(Body);
        $('#DropDestinatario').html(Body);
    }, 'json');
}

function GetDropTiposAudiencia() {

    $.get(URL_OBTENER_AUDIENCIAS, function (data, status) {
        var Body = "<option disabled selected>Selecciona una opción</option>";

        for (var i = 0; i <= data.length - 1; i++) {
            Body = Body + "<option value='" + data[i].id + "'>" + data[i].tbl_tipo_audiencia + "</option>";
        }

        $('#DropTipoAudiencia').html(Body);
    }, 'json');
}

function GetDropCanal() {
    
    $.get(URL_OBTENER_CANALES, function (data, status) {
        var Body = "<option disabled selected>Selecciona una opción</option>";

        for (var i = 0; i <= data.length - 1; i++) {
            Body = Body + "<option value='" + data[i].id + "'>" + data[i].canal + "</option>";
        }

        $('#DropCanal').html(Body);
    }, 'json');
}