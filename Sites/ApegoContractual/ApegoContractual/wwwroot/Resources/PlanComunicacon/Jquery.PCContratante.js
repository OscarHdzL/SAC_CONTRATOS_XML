var Responsables = null;

var URL_SERVICIO_BASE = URL_AGREGAR_CONTRATANTE = URL_OBTENER_CONTRATANTES_POR_CONTRATO = URL_OBTENER_CONTRATANTE_POR_ID
    = URL_ELIMINAR_CONTRATANTE = URL_OBTENER_RESPONSABLES = URL_OBTENER_AUDIENCIAS = URL_OBTENER_UBICACIONES = "";

var ContratanteClass =
{
    id: null,
    Contratante_RespApego_Contrato_ac: null,
    TBLENT_CONTRATO_id: null,
    tbl_ubicaciones_ac_id: null,
    tbl_TipoAudiencia_id: null,
    Inclusion: null,
    Estatus: null
};

$(document).ready(function () {

    establecerRutasServicio();
    
    AjusteTablaContratante();
    GetContratante();
    GetDropResponsablesContratante();
    GetDropTiposAudienciaContratante();
    GetDropUbicacionesContratante();
});

$('#AddContratante').click(function () {
    AddContratante();
});

$('#DropNombreContratante').change(function () {
    for (var i = 0; i <= Responsables.length - 1; i++) {
        if (Responsables[i].id === $('#DropNombreContratante').val()) {
            $('#PuestoContratante').val(Responsables[i].tbl_RespApego_SerPub_ac.TBLENT_SERVIDOR_PUBLICO.TBLENT_PUESTO.PUESTO);
            $('#CorreoContratante').val(Responsables[i].tbl_RespApego_SerPub_ac.TBLENT_SERVIDOR_PUBLICO.EMAIL);
            $('#TelefonoContratante').val(Responsables[i].tbl_RespApego_SerPub_ac.TBLENT_SERVIDOR_PUBLICO.Telefono);
            $('#ExtensionContratante').val(Responsables[i].tbl_RespApego_SerPub_ac.TBLENT_SERVIDOR_PUBLICO.Extension);
        }
    }
});

function establecerRutasServicio() {
    var idDependencia = $("#IdDependencia").val();
    var idContrato = $('#IdContrato').val();
    //idContrato = "1a826236-37b7-11ea-82d7-00155d1b3502"; //hardcode

    URL_SERVICIO_BASE                       = $("#EndPointAC").val();

    URL_AGREGAR_CONTRATANTE                 = URL_SERVICIO_BASE + "/Request/Contratante/Add";
    URL_OBTENER_CONTRATANTES_POR_CONTRATO   = URL_SERVICIO_BASE + "/Request/Contratantes/GetLista/" + idContrato;
    URL_OBTENER_CONTRATANTE_POR_ID          = URL_SERVICIO_BASE + "/Request/Contratante/Get/  - idContratante";
    URL_ELIMINAR_CONTRATANTE                = URL_SERVICIO_BASE + "/Request/Contratante/Delete/ - idContrantante";
    URL_OBTENER_RESPONSABLES                = URL_SERVICIO_BASE + "/Request/ResponsablesApego/Contrato/" + idContrato;
    URL_OBTENER_AUDIENCIAS                  = URL_SERVICIO_BASE + "SerTipoAudiencia/Get/"; 
    URL_OBTENER_UBICACIONES                 = URL_SERVICIO_BASE + "/Request/Ubicacion/Get/" + idDependencia;
}

function AjusteTablaContratante() {
    $('#tbl_Contratante').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" },
            { "width": "1%", "targets": 0 }
        ]
    });
}

function ModalAgregarContratante() {
    $('.clean').val('');

    GetDropResponsablesContratante();
    GetDropTiposAudienciaContratante();
    GetDropUbicacionesContratante();

    $('#idContratante').val('');
    $('#TitleModalContratante').text('Agregar contratante');
    $('#AddContratante').text('Guardar');
    $('#ModalContratante').modal({ backdrop: 'static', keyboard: false });
    $('#ModalContratante').modal('show');
}

function ValidarContratante() {

    var Response = { Texto: '', Bit: true, objeto: null };

    if ($('#DropNombreContratante').val() === ('' || null)) {
        Response.Texto = 'Debe seleccionar un nombre';
        Response.Bit = true;
        return Response;
    }

    if ($('#DropUbicacionContratante').val() === ('' || null)) {
        Response.Texto = 'Debe seleccionar una ubicación';
        Response.Bit = true;
        return Response;
    }

    if ($('#DropTipoAudienciaContratante').val() === ('' || null)) {
        Response.Texto = 'Debe seleccionar un tipo de audiencia';
        Response.Bit = true;
        return Response;
    }

    Response.Texto = '';
    Response.Bit = false;
    return Response;
}

function CerrarModalContratante() {
    $('.clean').val('');
    $('#ModalContratante').modal('hide');

    GetDropResponsablesContratante();
    GetDropTiposAudienciaContratante();
    GetDropUbicacionesContratante();
}

function AddContratante() {
    var ValidacionContratante = ValidarContratante();

    if (ValidacionContratante.Bit) {
        return ErrorSA('Error en los datos de entrada', ValidacionContratante.Texto);
    }

    var OBJ_Contratante = ContratanteClass;
    var d = new Date();

    var date = d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate();

    OBJ_Contratante.id = $('#idContratante').val() === '' ? '00000000-0000-0000-0000-000000000000' : $('#idContratante').val();

    OBJ_Contratante.TBLENT_CONTRATO_id = $('#idContratoContratante').val();
    OBJ_Contratante.tbl_ubicaciones_ac_id = $('#DropUbicacionContratante').val();
    OBJ_Contratante.Contratante_RespApego_Contrato_ac = $('#DropNombreContratante').val();
    OBJ_Contratante.tbl_TipoAudiencia_id = $('#DropTipoAudienciaContratante').val();
    OBJ_Contratante.Inclusion = date;
    OBJ_Contratante.Estatus = 1;

    var form_data = new FormData();
    form_data.append('ContratanteForm', JSON.stringify(OBJ_Contratante));

    $.ajax({
        url: URL_AGREGAR_CONTRATANTE,
        dataType: 'text',
        cache: false,
        contentType: false,
        processData: false,
        data: form_data,
        type: 'POST',
        success: function (data) {
            var objresponse = JSON.parse(data);
            if (!objresponse.Bit) {
                //SuccessSA("Operación exitosa", "El registro se guardo correctamente");

                function Confirmacion() {
                    return CerrarModalContratante();
                }

                var AccionSi = eval(Confirmacion);

                SuccessSAAction("Operación exitosa", "El registro se guardo correctamente", AccionSi);
                $('.Clean').val('');

                //$('#ModalContratante').modal('hide');
                GetContratante();
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

function GetContratante() {
    //var idcon = $('#idContratoContratante').val();
    $.get(URL_OBTENER_CONTRATANTES_POR_CONTRATO, function (data, status) {
        var lista = [];

        for (var i = 0; i <= data.length - 1; i++) {
            var item = [];

            item.push(data[i].tbl_RespApego_Contrato_ac.tbl_RespApego_SerPub_ac.TBLENT_SERVIDOR_PUBLICO.NOMBRE + ' ' +
                data[i].tbl_RespApego_Contrato_ac.tbl_RespApego_SerPub_ac.TBLENT_SERVIDOR_PUBLICO.AP_PATERNO + ' ' +
                data[i].tbl_RespApego_Contrato_ac.tbl_RespApego_SerPub_ac.TBLENT_SERVIDOR_PUBLICO.AP_MATERNO);

            item.push(data[i].tbl_ubicaciones_ac.Direccion);
            item.push(data[i].tbl_TipoAudiencia.TipoAudiencia);
            item.push(data[i].tbl_RespApego_Contrato_ac.tbl_RespApego_SerPub_ac.TBLENT_SERVIDOR_PUBLICO.TBLENT_PUESTO.PUESTO);
            item.push(data[i].tbl_RespApego_Contrato_ac.tbl_RespApego_SerPub_ac.TBLENT_SERVIDOR_PUBLICO.Telefono);
            item.push(data[i].tbl_RespApego_Contrato_ac.tbl_RespApego_SerPub_ac.TBLENT_SERVIDOR_PUBLICO.Extension);
            item.push(data[i].tbl_RespApego_Contrato_ac.tbl_RespApego_SerPub_ac.TBLENT_SERVIDOR_PUBLICO.EMAIL);
            item.push('<a onclick="btnEditContratante(\'' + data[i].id + '\')" class="btn btn-sm btn-info" title="Modificar registro"><span class="glyphicon glyphicon-edit"></span></a> <a onclick="btnDeleteContratante(\'' + data[i].id + '\')" class="btn btn-sm btn-danger" title="Eliminar registro"><span class="glyphicon glyphicon-trash"></span></a>');

            lista.push(item);
        }

        var table = $('#tbl_Contratante').DataTable();

        table.destroy();

        $('#tbl_Contratante').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            data: lista,
            columns: [
                { title: "Nombre" },
                { title: "Ubicación" },
                { title: "Tipo de audiencia" },
                { title: "Puesto" },
                { title: "Teléfono" },
                { title: "Extensión" },
                { title: "Correo electrónico" },
                { title: "Acción" }
            ]
        });
    });
}

function btnEditContratante(idContratante) {
    $('#idContratante').val(idContratante);
    $('#TitleModalContratante').text('Modificar Contratante');
    $('#AddContratante').text('Guardar');
    $('#ModalContratante').modal({ backdrop: 'static', keyboard: false });
    $('#ModalContratante').modal('show');

    $.get(URL_OBTENER_CONTRATANTE_POR_ID + idContratante, function (data, status) {

        $('#DropNombreContratante > option[value="' + data.Contratante_RespApego_Contrato_ac + '"]').attr('selected', 'selected');
        $('#DropTipoAudienciaContratante> option[value="' + data.tbl_TipoAudiencia_id + '"]').attr('selected', 'selected');
        $('#DropUbicacionContratante> option[value="' + data.tbl_ubicaciones_ac_id + '"]').attr('selected', 'selected');
        $('#PuestoContratante').val(data.tbl_RespApego_Contrato_ac.tbl_RespApego_SerPub_ac.TBLENT_SERVIDOR_PUBLICO.TBLENT_PUESTO.PUESTO);
        $('#CorreoContratante').val(data.tbl_RespApego_Contrato_ac.tbl_RespApego_SerPub_ac.TBLENT_SERVIDOR_PUBLICO.EMAIL);
        $('#TelefonoContratante').val(data.tbl_RespApego_Contrato_ac.tbl_RespApego_SerPub_ac.TBLENT_SERVIDOR_PUBLICO.Telefono);
        $('#ExtensionContratante').val(data.tbl_RespApego_Contrato_ac.tbl_RespApego_SerPub_ac.TBLENT_SERVIDOR_PUBLICO.Extension);
    });
}

function EliminarContratante(item) {
    $.post(URL_ELIMINAR_CONTRATANTE + item, function (data, status) {
        var objresponse = JSON.parse(data);

        if (status === 'success') {
            SuccessSA('', 'El registro se eliminó exitosamente');
            GetContratante();
        }
        else {
            ErrorSA('', objresponse.Descripcion);
        }
    });
}

function btnDeleteContratante(idContratante) {
    function Confirmacion() {
        return EliminarContratante(idContratante);
    }

    var AccionSi = eval(Confirmacion);
    WarningSA('', '¿En verdad desea eliminar el registro?', 'Si, Continuar', 'No, Cancelar', AccionSi, '');
}

function GetDropResponsablesContratante() {
    //var idcon = $('#idContratoContratante').val();
    $.get(URL_OBTENER_RESPONSABLES, function (data, status) {
        Responsables = data;
        var Body = "<option disabled selected>Selecciona una opción</option>";

        for (var i = 0; i <= data.length - 1; i++) {
            Body = Body + "<option value='" + data[i].id + "'>" + data[i].tbl_RespApego_SerPub_ac.TBLENT_SERVIDOR_PUBLICO.NOMBRE + ' ' + data[i].tbl_RespApego_SerPub_ac.TBLENT_SERVIDOR_PUBLICO.AP_PATERNO + ' ' + data[i].tbl_RespApego_SerPub_ac.TBLENT_SERVIDOR_PUBLICO.AP_MATERNO + "</option>";
        }

        $('#DropNombreContratante').html(Body);
    }, 'json');
}

function GetDropTiposAudienciaContratante() {

    $.get(URL_OBTENER_AUDIENCIAS, function (data, status) {
        var Body = "<option disabled selected>Selecciona una opción</option>";

        for (var i = 0; i <= data.length - 1; i++) {
            Body = Body + "<option value='" + data[i].id + "'>" + data[i].tbl_tipo_audiencia + "</option>";
        }

        $('#DropTipoAudienciaContratante').html(Body);
    }, 'json');
}

function GetDropUbicacionesContratante() {

    //Cambiar el id '00000000-0000-0000-0000-000000000000'
    //var iddep = $('#idDependenciaPC').val();

    $.get(URL_OBTENER_UBICACIONES, function (data, status) {
        var Body = "<option disabled selected>Selecciona una opción</option>";

        for (var i = 0; i <= data.length - 1; i++) {
            Body = Body + "<option value='" + data[i].id + "'>" + data[i].Direccion + "</option>";
        }

        $('#DropUbicacionContratante').html(Body);
    }, 'json');
}