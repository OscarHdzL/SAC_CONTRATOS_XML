var URL_SERVICIO_BASE = URL_OBTENER_IVA_INSTANCIA = URL_OBTENER_ENTREGABLES = URL_OBTENER_PROVEEDORES = URL_OBTENER_RESPONSABLES = URL_AGREGAR_ESQUEMA_PAGO = URL_OBTENER_ESQUEMAS_PAGO = URL_OBTENER_ESQUEMA_PAGO_POR_ID = URL_ELIMINAR_ESQUEMA_PAGO = "";
var URL_OBTENER_PLANES_SIN_ESQUEMA = "";
$.extend($.fn.dataTable.defaults, {
    responsive: true
});

$(document).ready(function () {

    establecerRutasServicio();
    LaunchLoader(true);
    $('#EsquemaPagos').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        }
    });
    setInterval('Redimension()', 500);
    GetRazonSocial();
    GetPlanesEntrega();
    GetResponsable();
    GetEsquema();
    $('#txt_FechaEntrega').datetimepicker({
        format: 'YYYY-MM-DD'
    });
    LaunchLoader(false);
});

$('#GuardarEsqPag').click(function () {
    AddEsquema();
});

$('#Monto').keyup(function () {
    if (event.which >= 37 && event.which <= 40) {
        event.preventDefault();
    }
    $(this).val(function (index, value) {
        return value
            .replace(/\D/g, "")
            .replace(/([0-9])([0-9]{2})$/, '$1.$2')
            .replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ",")
            ;
    });

    var monto = parseInt($('#Monto').val().replace(/,/g, ""));
    var montoIVA = parseFloat($('#IVA').val());
    var resultado = parseFloat(monto * montoIVA / 100);    
    $('#MontoIVA').val(resultado.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,'));
    $('#Total').val(0);

    monto = parseFloat($('#Monto').val().replace(/,/g, ""));
    montoIVA = parseFloat($('#MontoIVA').val().replace(/,/g, ""));
    resultado = monto + montoIVA;

    $('#Total').val(resultado.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,'));                
});

$('.cerrarMEP').click(function () {
    $('#IdEsquemaPago').val('');
    $('.CleanEP').val('');
    //GetEntregables();
    GetRazonSocial();
    GetResponsable();
    GetPlanesEntrega();
    $('#Factura').val('');
    $('#Firma').prop('checked', false);
    $('#txtEstadoEntrega').prop('disabled', true);
    $('#Monto').prop('disabled', true);
    $('#MontoIVA').prop('disabled', true);
    $('#Firma').prop('disabled', true);
    $('#FacturaAdj').text('');

    $('#CuentasxPagar').val('');
    $('#PEntrega').val('');
    
});

$('#Factura').on("change", function () {
    if ($('#Factura').val() !== '') {
        $('#txtEstadoEntrega').prop('disabled', false);
        $('#Monto').prop('disabled', false);
        $('#MontoIVA').prop('disabled', true);
        $('#Firma').prop('disabled', false);
    }
});

$('#AddPago').click(function () {

    GetRazonSocial();
    GetResponsable();
    GetInstanciaIVA();
    GetPlanesEntrega();
    GetNotificaciones();
    $('#txt_FechaEntrega').datetimepicker({
        format: 'YYYY-MM-DD'
    });
    $('#downloadfilejust').hide();
    $('#IdEsquemaPago').val('');
    $('#AltaEsquemaPago').modal({ backdrop: 'static', keyboard: false });
    $('#TituloModal').html('Agregar un nuevo registro de esquema de pago');
    $('#AltaEsquemaPago').modal('show');
});

function establecerRutasServicio() {
    var idInstancia = $("#IdInstancia").val();
    var idContrato = $('#IdContrato').val();
    URL_SERVICIO_BASE = $("#EndPointAC").val();

    URL_OBTENER_IVA_INSTANCIA = URL_SERVICIO_BASE + "SerEsquemaPago/Get/Instancia/" + idInstancia;
    URL_OBTENER_ENTREGABLES = URL_SERVICIO_BASE + "/Request/EsquemaPago/GetListaEntregables/" + idContrato;
    URL_OBTENER_PROVEEDORES = URL_SERVICIO_BASE + "SerEsquemaPago/Get/Proveedores/" + idContrato;
    URL_OBTENER_RESPONSABLES = URL_SERVICIO_BASE + "SerRespApego/Get/ResponsablesApego/EsquemaPago/" + idContrato; //hardcode
    URL_AGREGAR_ESQUEMA_PAGO = URL_SERVICIO_BASE + "SerEsquemaPago/Add";
    URL_OBTENER_ESQUEMAS_PAGO = URL_SERVICIO_BASE + "SerEsquemaPago/Get/" + idContrato;
    URL_OBTENER_ESQUEMA_PAGO_POR_ID = URL_SERVICIO_BASE + "SerEsquemaPago/Get/"; //idEsquema, idContrato
    URL_ELIMINAR_ESQUEMA_PAGO = URL_SERVICIO_BASE + "SerEsquemaPago/Delete/"; //idEsquema
    URL_OBTENER_PLANES_SIN_ESQUEMA = URL_SERVICIO_BASE + "SerEsquemaPago/Get/PlanesSinEsquema/" + idContrato;
}

function CerrarModalEsqP() {
    $('#AltaEsquemaPago').modal('hide');
    $('#id_esquema_edicion').val('');
}

function GetInstanciaIVA() {

    $.get(URL_OBTENER_IVA_INSTANCIA, function (data, status) {
        console.log(data);
        if (data != null) {
            $('#IVA').val(data.iva);
        } else {
            $('#IVA').val(16);
        }
        
    }, 'json');
}

function GetRazonSocial() {

    $.get(URL_OBTENER_PROVEEDORES, function (data, status) {
        console.log(data)
        //var BodyC = "<option disabled selected value='" + data.id + "'>" + data.razon_social + "</option>";
        var BodyC = "<option value=''>Seleccione</option>";

        for (var i = 0; i <= data.length - 1; i++) {
            BodyC = BodyC + "<option value='" + data[i].id + "'>" + data[i].razon_social + "</option>";
        }

        $('.Proveedor').html(BodyC);
        $('.Proveedor').attr('readonly', true);
    }, 'json');
}

//-- planes de entrega sin esquema de pago --//
function GetPlanesEntrega() {
    PlanesSinEsquema = [];
    $.get(URL_OBTENER_PLANES_SIN_ESQUEMA, function (data, status) {
        console.log(data)
        //var BodyC = "<option disabled selected value='" + data.id + "'>" + data.razon_social + "</option>";
        var BodyC = "<option value=''>Seleccione</option>";

        for (var i = 0; i <= data.length - 1; i++) {
            BodyC = BodyC + "<option value='" + data[i].tbl_plan_entrega_id + "'>" + data[i].tbl_plan_entrega_identificador + "</option>";
            PlanesSinEsquema.push(data[i]);
        }

        $('.PEntrega').html(BodyC);
    }, 'json');
}


function GetNotificaciones() {
    var CuentasPagar = [];
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    $.get(URL_SERVICIO_BASE + 'SerEsquemaPago/ConsultarCuentasXPagar/' + $('#txt_dependencia_contrato_v').val(), function (data, status) {
        console.log(data)
        var BodyC = "<option value=''>Seleccione</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            BodyC = BodyC + "<option value='" + data[i].id + "'>" + data[i].razon_social + "</option>";
            CuentasPagar.push(data[i]);
        }
        console.log(BodyC);
        $('#CuentasxPagar').html(BodyC);
    }, 'json');
}

$('#PEntrega').change(function () {

    var idPlan = $('#PEntrega').val();
    var indice = PlanesSinEsquema.findIndex(x => x.tbl_plan_entrega_id == idPlan);
    if (indice != -1) {
        $('#Monto').val(PlanesSinEsquema[indice].plan_monto);
        $('#MontoIVA').val(PlanesSinEsquema[indice].plan_monto_iva);
        $('#Total').val(PlanesSinEsquema[indice].plan_total);
    }
});

//-- Fin planes de entrega sin esquema de pago --//

function GetResponsable() {

    $.get(URL_OBTENER_RESPONSABLES, function (data, status) {
        //console.log(data);
        //var Body = "<option value='' disabled selected>Selecciona una opción</option>";
        var Body = "<option disabled selected value='" + data.id + "'>" + data.nombre + "</option>";
        //for (var i = 0; i <= data.length - 1; i++) {
        //    Body = Body + "<option value='" + data[i].id + "'>" + data[i].nombre + "</option>";
        //}

        $('.Autoriza').html(Body);
        $("#hdnIdResponsableAutoriza").val(data.id);
    }, 'json');
}

function Validar() {

    var Response = { Texto: '', Bit: true, objeto: null };

    //if ($('.Autoriza').val() === null) {
    //    Response.Texto = 'Debe seleccionar un usuario';
    //    Response.Bit = true;
    //    return Response;
    //}
    //if ($('.Entregable').val() == null) {
    //    Response.Texto = 'Debe seleccionar un entregable';
    //    Response.Bit = true;
    //    return Response;
    //}
    if ($('#txt_FechaEntrega').val() === '') {
        Response.Texto = 'Debe ingresar una fecha de entrega.';
        Response.Bit = true;
        return Response;
    }
    if ($('.Proveedor').val() === null) {
        Response.Texto = 'Debe seleccionar una razón social del interlocutor';
        Response.Bit = true;
        return Response;
    }

    if (ValidaCadena($('#txtObservaciones').val(), '') !== '') {
        Response.Texto = 'No se permiten caracteres especiales en el campo "Observaciones"';
        Response.Bit = true;
        return Response;
    }
    if ($('#CuentasxPagar').val() === '') {
        Response.Texto = 'Debe ingresar una cuenta x pagar para notificar.';
        Response.Bit = true;
        return Response;
    }

    Response.Texto = '';
    Response.Bit = false;

    return Response;
}

function AddEsquema() {
    var Validacion = Validar();

    if (Validacion.Bit) {
        return ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }

    var OBJ_Esquema = EsquemaClass;
    var d = new Date();
    var date = d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate();

    OBJ_Esquema.p_id = $('#IdEsquemaPago').val() === '' ? "00000000-0000-0000-0000-000000000000" : $('#IdEsquemaPago').val();
    OBJ_Esquema.p_tbl_contrato_servidor_resp_id = $('#hdnIdResponsableAutoriza').val();
    OBJ_Esquema.p_fecha_pago = $('#txt_FechaEntrega').val();

    OBJ_Esquema.p_monto = $('#Monto').val() == "" ? '0' : $('#Monto').val().replace(/,/g, "");
    OBJ_Esquema.p_monto_iva = $('#MontoIVA').val() == "" ? '0' : $('#MontoIVA').val().replace(/,/g, "");
    OBJ_Esquema.p_total = $('#Total').val() == "" ? '0' : $('#Total').val().replace(/,/g, "");

    OBJ_Esquema.p_estado_plan_entrega = $('#txtEstadoEntrega').val();
    OBJ_Esquema.p_tbl_contrato_proveedor_id = $('.Proveedor').val();
    OBJ_Esquema.p_tiene_firma = $('#Firma').prop('checked') ? 1 : 0;
    OBJ_Esquema.p_observacion = $('#txtObservaciones').val();
    OBJ_Esquema.p_tbl_plan_entrega_id = $('#PEntrega').val();
    OBJ_Esquema.p_notificacion_proveedor_id = $('#CuentasxPagar').val();
    /////////////////////////Carga de Factura
    var archivo = $('#Factura').prop('files')[0];
    var token_edicion = $('#id_esquema_edicion').val();

    if (archivo == undefined) { // Si no se cargo archivo
        if (token_edicion != "") {
            OBJ_Esquema.p_token_fac = token_edicion;  //Se asigna el token que este guardado
        } else {
            OBJ_Esquema.p_token_fac = "00000000-0000-0000-0000-000000000000"; //Si no se cargo archivo se asigna un token
        }

    } else { // se cargo un archivo y se realiza peticion para carga de archivo

        var form_data_file = new FormData();
        var file_ = $('#Factura').prop('files')[0];
        form_data_file.append('file', file_);

        $.ajax({
            url: $("#EndPointFileAC").val() + 'Upload/',
            dataType: 'text',
            cache: false,
            contentType: false,
            processData: false,
            data: form_data_file,
            type: 'POST',
            async: false,
            success: function (data) {
                var token = data.replace(/[ '"]+/g, '');
                OBJ_Esquema.p_token_fac = token;
                console.log(token);
            },
            error: function (data) {
                var objresponse = JSON.parse(data);
                ErrorSA('', objresponse);
            }
        });
    }

    ///////////////////////////////////

    var form_data = new FormData();

    form_data.append('EsquemaForm', JSON.stringify(OBJ_Esquema));
    console.log(JSON.stringify(OBJ_Esquema));

    $.ajax({

        url: URL_AGREGAR_ESQUEMA_PAGO,
        dataType: 'text',
        cache: false,
        contentType: 'application/json',
        processData: false,
        data: JSON.stringify(OBJ_Esquema),
        type: 'post',

        success: function (data) {
            console.log(data);
            var objresponse = JSON.parse(data);
            if (!objresponse.Bit) {

                function confirmacion() {
                    return CerrarModalEsqP();
                }

                var AccionSi = eval(confirmacion);
                SuccessSAAction("Operación exitosa", "El registro se guardó correctamente", AccionSi);

                $('.CleanEP').val('');
                $('#IdEsquemaPago').val('');
                $('#id_esquema_edicion').val('');
                GetEsquema();
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


    $('#id_esquema_edicion').val('');
}

function GetEsquema() {

    $.get(URL_OBTENER_ESQUEMAS_PAGO, function (data, status) {

        var Arreglo_arreglos = [];

        for (var i = 0; i <= data.length - 1; i++) {
            var Interno = [];

            Interno.push(i + 1);
            Interno.push(data[i].nombre_responsable);
            var fechaProg = data[i].fecha_pago.substring(0, data[i].fecha_pago.indexOf('T'));

            Interno.push(fechaProg);
            Interno.push(data[i].monto === null ? "$" + " " + 0 : "$" + " " + data[i].monto.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,'));
            Interno.push(data[i].monto_iva === null ? "$" + " " + 0 : "$" + " " + data[i].monto_iva.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,'));
            Interno.push(data[i].total === null ? "$" + " " + 0 : "$" + " " + data[i].total.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,'));
            Interno.push(data[i].estado_plan_entrega === '' ? "Ninguna" : data[i].estado_plan_entrega);
            Interno.push(data[i].razon_social);
            Interno.push(data[i].tiene_firma === true ? 'Si' : 'No');
            Interno.push(data[i].observacion === null ? "Ninguna" : data[i].observacion);
            Interno.push('<a onclick="btnEditEP(\'' + data[i].id + '\')" class="fa fa-pencil-square-o btn btn-primary" title="Modificar registro"> </a> <a onclick="btnDeleteEP(\'' + data[i].id + '\')" class="fa fa-trash-o btn btn-danger" title="Eliminar registro"> </a>');

            Arreglo_arreglos.push(Interno);
        }

        var table = $('#EsquemaPagos').DataTable();

        table.destroy();

        $('#EsquemaPagos').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            data: Arreglo_arreglos,
            columns: [
                { title: "No." },
                { title: "Autoriza" },
                { title: "Fecha de pago" },
                { title: "Monto de pago" },
                { title: "Monto del IVA" },
                { title: "Total" },
                { title: "Estado de plan de entrega" },
                { title: "Razón social" },
                { title: "Firma digital" },
                { title: "Observaciones" },
                { title: "Acciones" }
            ]
        });
    });
}

function btnEditEP(id) {
    LaunchLoader(true);

    $('#AltaEsquemaPago').modal({ backdrop: 'static', keyboard: false });
    $('#TituloModal').html('Modificar registro de esquema de pago');
    //  Agregar un nuevo registro de esquema de pago
    $('#AltaEsquemaPago').modal('show');
    GetInstanciaIVA();

    $('#IdEsquemaPago').val(id);
    var idContrato = $('#IdContrato').val();

    $.get(URL_OBTENER_ESQUEMA_PAGO_POR_ID + id + "/" + idContrato, function (data, status) {

        if (data !== null) {
            //$('.Autoriza > option[value="' + data.tbl_contrato_servidor_resp_id + '"]').attr("selected", "selected");
            ////$('.Entregable > option[value="' + data.tbl_Entregables_ac.id + '"]').attr("selected", "selected");
            //$('.Proveedor > option[value="' + data.tbl_contrato_proveedor_id + '"]').attr("selected", "selected");

            //$("#Autoriza").val(data.tbl_contrato_servidor_resp_id);
            $("#Proveedor").val(data.tbl_contrato_proveedor_id);

            var fechaProg = data.fecha_pago.substring(0, data.fecha_pago.indexOf('T'));

            $('#txt_FechaEntrega').val(fechaProg);
            $('#txtObservaciones').val(data.observacion);

            $('#id_esquema_edicion').val(data.token_fac);
            $('#downloadfilejust').hide();

            if (data.token_fac !== null && data.token_fac !== '00000000-0000-0000-0000-000000000000') {
                $('#txtEstadoEntrega').prop('disabled', false);
                $('#Monto').prop('disabled', false);
                $('#MontoIVA').prop('disabled', true);
                $('#Firma').prop('disabled', false);
                $('#txtEstadoEntrega').val(data.estado_plan_entrega);
                $('#Monto').val(data.monto.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,'));
                $('#MontoIVA').val(data.monto_iva.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,'));
                $('#Total').val(data.total.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,'));
                $('#Firma').prop('checked', data.tiene_firma);
                $('#FacturaAdj').text('Este registro ya tiene una factura registrada');
                
                getURL(data.token_fac);
                $('#downloadfilejust').show();
                GetNotificacionesM(data.notificacion_tbl_proveedor_id);
                GetPlanEntregaM(data.tbl_plan_entrega_id);
            }
        }
    });

    LaunchLoader(false);
}

function GetNotificacionesM(id_cuentas_x_pagar) {
    var CuentasPagar = [];
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    $.get(URL_SERVICIO_BASE + 'SerEsquemaPago/ConsultarCuentasXPagar/' + $('#txt_dependencia_contrato_v').val(), function (data, status) {
        console.log(data)
        var BodyC = "<option value=''>Seleccione</option>";
        for (var i = 0; i <= data.length - 1; i++) {
            BodyC = BodyC + "<option value='" + data[i].id + "'>" + data[i].razon_social + "</option>";
            CuentasPagar.push(data[i]);
        }
        console.log(BodyC);
        $('#CuentasxPagar').html(BodyC);
        $('#CuentasxPagar').val(id_cuentas_x_pagar);

    }, 'json');
}

function GetPlanEntregaM(tbl_plan_entrega_id) {
    PlanesSinEsquema = [];
    var esquema = $('#IdEsquemaPago').val();
    $.get(URL_SERVICIO_BASE + 'SerEsquemaPago/Get/PlanDelEsquema/' + esquema, function (data, status) {
        console.log(data)
        //var BodyC = "<option disabled selected value='" + data.id + "'>" + data.razon_social + "</option>";
        var BodyC = "";

        for (var i = 0; i <= data.length - 1; i++) {
            BodyC = BodyC + "<option value='" + data[i].tbl_plan_entrega_id + "'>" + data[i].tbl_plan_entrega_identificador + "</option>";
            PlanesSinEsquema.push(data[i]);
        }

        $('.PEntrega').html(BodyC);
        $('#PEntrega').val(tbl_plan_entrega_id);
    }, 'json');
}

function EliminarEP(id) {
    $.ajax({
        url: URL_ELIMINAR_ESQUEMA_PAGO + id,
        dataType: 'text',
        cache: false,
        contentType: false,
        processData: false,
        type: "DELETE",
        success: function (data) {
            SuccessSA('', 'El registro se eliminó exitosamente');
            GetEsquema();
        },
        error: function (data) {
            var objresponse = JSON.parse(data);
            ErrorSA('', objresponse.Descripcion);
        }
    });
}

function btnDeleteEP(item) {
    function Confirmacion() {
        return EliminarEP(item);
    }

    var AccionSi = eval(Confirmacion);
    WarningSA('', '¿En verdad desea eliminar el registro?', 'Si, Continuar', 'No, Cancelar', AccionSi, '');
}

var EsquemaClass = {
    p_opt: 0,
    p_id: '',
    p_tbl_contrato_servidor_resp_id: '',
    //p_tbl_Entregables_id: null,
    p_tbl_contrato_proveedor_id: '',
    p_fecha_pago: '',
    p_monto: '0',
    p_monto_iva: '0',
    p_total: '0',
    p_estado_plan_entrega: '',
    p_tiene_firma: '',
    p_observacion: '',
    p_token_fac: '00000000-0000-0000-0000-000000000000',
    //p_TBLENT_CONTRATO_id: null,
    p_inclusion: '2020-01-30',
    p_estatus: 1,
    p_tbl_plan_entrega_id: '',
    p_notificacion_proveedor_id:''
};
var PlaneSinEsquema = {    
  tbl_plan_entrega_id: "",
  tbl_plan_entrega_identificador: "",
  tbl_plan_entrega_descripcion: "",
  tbl_contrato_id: "",
  plan_monto: 0,
  plan_monto_iva: 0,
  plan_total: 0
};

var PlanesSinEsquema = [];
