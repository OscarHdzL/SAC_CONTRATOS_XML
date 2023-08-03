var Proveedores = null;

var URL_SERVICIO_BASE = URL_AGREGAR_PROVEEDOR = URL_OBTENER_PROVEEDORES = URL_OBTENER_PROVEEDOR_POR_ID =
URL_ELIMINAR_PROVEEDOR = URL_OBTENER_PROVEEDORES_POR_CONTRATO = URL_OBTENER_AUDIENCIAS = URL_OBTENER_UBICACIONES = "";

var ProveedorClass =
{
    id: null,
    TBLENT_PROVEEDOR_id: null,
    TBLENT_CONTRATO_id: null,
    tbl_ubicaciones_ac_id: null,
    tbl_TipoAudiencia_id: null,
    Inclusion: null,
    Estatus: null
};

$(document).ready(function () {
    AjusteTablaProveedor();
    GetProveedor();
    GetDropProveedores();
    GetDropTiposAudienciaProveedor();
    GetDropUbicacionesProveedor();
});

$('#AddProveedor').click(function () {
    AddProveedor();
});

$('#DropNombreProveedor').change(function () {
    for (var i = 0; i <= Proveedores.length - 1; i++) {
        if (Proveedores[i].ID_PROVEEDOR_PK === $('#DropNombreProveedor').val()) {
            $('#CorreoProveedor').val(Proveedores[i].CORREOELECTRONICO);
            $('#TelefonoProveedor').val(Proveedores[i].TELEFONO);
            $('#ExtensionProveedor').val(Proveedores[i].Extension);
        }
    }
});

function establecerRutasServicio() {
    var idDependencia = $("#idDependenciaPC").val();
    var idContrato = $('#IdContrato').val();
    //idContrato = "1a826236-37b7-11ea-82d7-00155d1b3502"; //hardcode

    URL_SERVICIO_BASE                       = $("#EndPointAC").val();

    URL_AGREGAR_PROVEEDOR                   = URL_SERVICIO_BASE + "/Request/Proveedor/Add";
    URL_OBTENER_PROVEEDORES                 = URL_SERVICIO_BASE + "SerPlanComunicacion/Get/Proveedores/" + idContrato;
    URL_OBTENER_PROVEEDOR_POR_ID            = URL_SERVICIO_BASE + "/Request/Proveedor/Get/  - idProveedor";
    URL_ELIMINAR_PROVEEDOR                  = URL_SERVICIO_BASE + "/Request/Proveedor/Delete/ - idProveedor";
    URL_OBTENER_PROVEEDORES_POR_CONTRATO    = URL_SERVICIO_BASE + "/Request/ProveedoresContrato/GetLista/" + idContrato;
    URL_OBTENER_AUDIENCIAS                  = URL_SERVICIO_BASE + "SerTipoAudiencia/Get";
    URL_OBTENER_UBICACIONES                 = URL_SERVICIO_BASE + "/Request/Ubicacion/Get/" + idDependencia;
}

function AjusteTablaProveedor() {
    $('#tbl_Proveedor').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" },
            { "width": "1%", "targets": 0 }
        ]
    });
}

function ModalAgregarProveedor() {
    $('.clean').val('');

    GetDropProveedores();
    GetDropTiposAudienciaProveedor();
    GetDropUbicacionesProveedor();

    $('#idProveedor').val('');
    $('#TitleModalProveedor').text('Agregar proveedor');
    $('#AddProveedor').text('Guardar');
    $('#ModalProveedor').modal({ backdrop: 'static', keyboard: false });
    $('#ModalProveedor').modal('show');
}

function ValidarProveedor() {

    var Response = { Texto: '', Bit: true, objeto: null };

    if ($('#DropNombreProveedor').val() === ('' || null)) {
        Response.Texto = 'Debe seleccionar un nombre';
        Response.Bit = true;
        return Response;
    }

    if ($('#DropUbicacionProveedor').val() === ('' || null)) {
        Response.Texto = 'Debe seleccionar una ubicación';
        Response.Bit = true;
        return Response;
    }

    if ($('#DropTipoAudienciaProveedor').val() === ('' || null)) {
        Response.Texto = 'Debe seleccionar un tipo de audiencia';
        Response.Bit = true;
        return Response;
    }

    Response.Texto = '';
    Response.Bit = false;
    return Response;
}

function CerrarModalProveedor() {
    $('.clean').val('');
    $('#ModalProveedor').modal('hide');

    GetDropProveedores();
    GetDropTiposAudienciaProveedor();
    GetDropUbicacionesProveedor();
}

function AddProveedor() {
    var ValidacionProveedor = ValidarProveedor();

    if (ValidacionProveedor.Bit) {
        return ErrorSA('Error en los datos de entrada', ValidacionProveedor.Texto);
    }

    var OBJ_Proveedor = ProveedorClass;
    var d = new Date();

    var date = d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate();

    OBJ_Proveedor.id = $('#idProveedor').val() === '' ? '00000000-0000-0000-0000-000000000000' : $('#idProveedor').val();

    OBJ_Proveedor.TBLENT_CONTRATO_id = $('#idContratoProveedor').val();
    OBJ_Proveedor.TBLENT_PROVEEDOR_id = $('#DropNombreProveedor').val();
    OBJ_Proveedor.tbl_ubicaciones_ac_id = $('#DropUbicacionProveedor').val();
    OBJ_Proveedor.tbl_TipoAudiencia_id = $('#DropTipoAudienciaProveedor').val();
    OBJ_Proveedor.Inclusion = date;
    OBJ_Proveedor.Estatus = 1;

    var form_data = new FormData();
    form_data.append('ProveedorForm', JSON.stringify(OBJ_Proveedor));

    $.ajax({
        url: URL_AGREGAR_PROVEEDOR,
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
                    return CerrarModalProveedor();
                }
                var AccionSi = eval(Confirmacion);

                SuccessSAAction("Operación exitosa", "El registro se guardo correctamente", AccionSi);

                //$('#ModalProveedor').modal('hide');
                GetProveedor();
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

function GetProveedor() {

    $.get(URL_OBTENER_PROVEEDORES, function (data, status) {
        var Arreglo_arreglos = [];
        for (var i = 0; i <= data.length - 1; i++) {
            var Interno = [];

            Interno.push(data[i].TBLENT_PROVEEDOR.RAZON_SOCIAL);
            Interno.push(data[i].tbl_ubicaciones_ac.Direccion);
            Interno.push(data[i].tbl_TipoAudiencia.TipoAudiencia);
            Interno.push(data[i].TBLENT_PROVEEDOR.TELEFONO);
            Interno.push(data[i].TBLENT_PROVEEDOR.Extension);
            Interno.push(data[i].TBLENT_PROVEEDOR.CORREOELECTRONICO);
            Interno.push('<a onclick="btnEditProveedor(\'' + data[i].id + '\')" class="btn btn-sm btn-info" title="Modificar registro"><span class="glyphicon glyphicon-edit"></span></a> <a onclick="btnDeleteProveedor(\'' + data[i].id + '\')" class="btn btn-sm btn-danger" title="Eliminar registro"><span class="glyphicon glyphicon-trash"></span></a>');
            Arreglo_arreglos.push(Interno);
        }

        var table = $('#tbl_Proveedor').DataTable();

        table.destroy();

        $('#tbl_Proveedor').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            data: Arreglo_arreglos,
            columns: [
                { title: "Nombre" },
                { title: "Ubicación" },
                { title: "Tipo de audiencia" },
                { title: "Teléfono" },
                { title: "Extensión" },
                { title: "Correo electrónico" },
                { title: "Acción" }
            ]
        });
    });
}

function btnEditProveedor(idProveedor) {
    $('#idProveedor').val(idProveedor);
    $('#TitleModalProveedor').text('Modificar Proveedor');
    $('#AddProveedor').text('Guardar');
    $('#ModalProveedor').modal({ backdrop: 'static', keyboard: false });
    $('#ModalProveedor').modal('show');

    $.get(URL_OBTENER_PROVEEDOR_POR_ID + idProveedor, function (data, status) {

        $('#DropNombreProveedor > option[value="' + data.TBLENT_PROVEEDOR_id + '"]').attr('selected', 'selected');
        $('#DropTipoAudienciaProveedor> option[value="' + data.tbl_TipoAudiencia_id + '"]').attr('selected', 'selected');
        $('#DropUbicacionProveedor> option[value="' + data.tbl_ubicaciones_ac_id + '"]').attr('selected', 'selected');
        $('#CorreoProveedor').val(data.TBLENT_PROVEEDOR.CORREOELECTRONICO);
        $('#TelefonoProveedor').val(data.TBLENT_PROVEEDOR.TELEFONO);
        $('#ExtensionProveedor').val(data.TBLENT_PROVEEDOR.Extension);
    });
}

function EliminarProveedor(item) {
    $.post(URL_ELIMINAR_PROVEEDOR + item, function (data, status) {
        var objresponse = JSON.parse(data);

        if (status === 'success') {
            SuccessSA('', 'El registro se eliminó exitosamente');
            GetProveedor();
        }
        else {
            ErrorSA('', objresponse.Descripcion);
        }
    });
}

function btnDeleteProveedor(idProveedor) {
    function Confirmacion() {
        return EliminarProveedor(idProveedor);
    }

    var AccionSi = eval(Confirmacion);
    WarningSA('', '¿En verdad desea eliminar el registro?', 'Si, Continuar', 'No, Cancelar', AccionSi, '');
}

function GetDropProveedores() {

    $.get(URL_OBTENER_PROVEEDORES_POR_CONTRATO, function (data, status) {
        Proveedores = data;
        var Body = "<option disabled selected>Selecciona una opción</option>";

        for (var i = 0; i <= data.length - 1; i++) {
            Body = Body + "<option value='" + data[i].ID_PROVEEDOR_PK + "'>" + data[i].RAZON_SOCIAL + "</option>";
        }

        $('#DropNombreProveedor').html(Body);
    }, 'json');
}

function GetDropTiposAudienciaProveedor() {

    $.get(URL_OBTENER_AUDIENCIAS, function (data, status) {
        var Body = "<option disabled selected>Selecciona una opción</option>";

        for (var i = 0; i <= data.length - 1; i++) {
            Body = Body + "<option value='" + data[i].id + "'>" + data[i].tbl_tipo_audiencia + "</option>";
        }

        $('#DropTipoAudienciaProveedor').html(Body);
    }, 'json');
}

function GetDropUbicacionesProveedor() {

    $.get(URL_OBTENER_UBICACIONES, function (data, status) {
        var Body = "<option disabled selected>Selecciona una opción</option>";

        for (var i = 0; i <= data.length - 1; i++) {
            Body = Body + "<option value='" + data[i].id + "'>" + data[i].Direccion + "</option>";
        }

        $('#DropUbicacionProveedor').html(Body);
    }, 'json');
}