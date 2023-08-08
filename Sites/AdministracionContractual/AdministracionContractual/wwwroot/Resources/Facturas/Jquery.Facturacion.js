$(document).ready(function () {
    LaunchLoader(true);
    AjusteTablaF();
    getFacturasF();
});

$(".btn-danger").click(function () {
    $('.Clean').val('');
});

function AjusteTablaF() {
    $('#DatosXmlRequest').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
        },
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" },
            { "width": "5%", "targets": 0 },
            { "width": "15%", "targets": 1 },
            { "width": "15%", "targets": 2 },
            { "width": "15%", "targets": 3 },
            //{ "width": "20%", "targets": 4 },
            //{ "width": "10%", "targets": 5 },
            //{ "width": "10%", "targets": 6 },
            { "width": "10%", "targets": 4 }
        ],
    });
}

var con = $("#EndPointAdmon").val();
var ins = $('#HDidInstancia').val();

function getFacturasF() {
    $.get(con + "GeneracionXMLController/Listar", function (data, status) {
        var Arreglo_arreglos = [];
        for (var i = 0; i <= data.length - 1; i++) {
            var ir_b = null;
            var Interno = [];

            Interno.push(i + 1);
            Interno.push(data[i].tblContratoId);
            Interno.push(data[i].id);
            //Interno.push(data[i].fecha);
            //Interno.push(data[i].nombreReceptor);
            //Interno.push(data[i].rfcReceptor);
            //Interno.push(data[i].conceptos);

            Interno.push("<button class='btn btn-default' title='Listar conceptos de " + data[i].id + "' onclick=\"Conceptos('" + data[i].tblContratoId + "','" + data[i].id + "');\"><i class='fa fa-list'></i></button>");

            Interno.push("<button class='btn btn-default' title='Detalle' onclick=\"muestraModaldetalle('" + data[i].id + "');\"><i class='fa fa-send'></i></button> <button class='btn btn-primary' title='Editar' onclick=\"muestraModalEditar('" + data[i].id + "');\"><i class='fa fa-edit'></i></button>");

            Arreglo_arreglos.push(Interno);
        }
        var table = $('#DatosXmlRequest').DataTable();
        table.destroy();
        $('#DatosXmlRequest').DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.20/i18n/Spanish.json"
            },
            data: Arreglo_arreglos,
            columns: [
                { title: "No." },
                { title: "Contrato Id" },
                { title: "Factura Id" },
                //{ title: "Fecha Emisión" },
                //{ title: "Nombre Receptor" },
                //{ title: "RFC Receptor" },
                { title: "Conceptos" },
                { title: "Acción" }
            ],
            columnDefs: [
                {
                    targets: '_all',
                    className: 'dt-body-center'
                }]
        });
        LaunchLoader(false);
    });
}

function Conceptos(item, id) {
    var route = '/Facturas/Conceptos/' + item;
    $.get("/Facturas/NombreFactura/" + id, function (data, status) {
        return window.location.replace(route);
    });
}

function GetContrato(id) {
    $.get(con + "GeneracionXMLController/Listar/", function (data, status) {
        var body = "<option selected value=''>Seleccione...</option>";

        const unicos = [];

        for (var i = 0; i < data.length; i++) {

            const elemento = data[i].tblContratoId;

            if (!unicos.includes(data[i].tblContratoId)) {
                unicos.push(elemento);
            }
        }

        for (var i = 0; i <= unicos.length - 1; i++) {

            body = body + "<option value='" + unicos[i] + "'>" + unicos[i] + "</option>";
        }
        $('#ddl_Contrato').html(body);
        $('#ddled_Contrato').html(body);
        //    $('#ddled_Contrato > option[value="' + id + '"]').attr("selected", "selected");
    });
    return;
}

function muestraModalAgregarFactura() {
    $('.Clean').val('');
    $('.Clean').prop("disabled", false);
    GetContrato();
    $('#ModalAgregarFactura').modal('show');
}

function muestraModalEditar(item) {
    //GetContrato();
    $("#id_Factura_ed").val(item);
    getedit(item);
    $('#title').html('Editar factura');
    $('.Clean').prop("disabled", false);
    $('#txted_Contratoid').prop("disabled", true);
    $('#txted_Facturaid').prop("disabled", true);
    $('#EditarFactura').show(true);
    $('#ModalEditarFactura').modal('show');

}
function muestraModaldetalle(item) {
    geteditDetalle(item);
    $('#title').html('Detalle Factura');
    $('#ModalEditarFactura').modal('show');
    $('.Clean').prop("disabled", true);
    $('#SuperUser_ed').prop("disabled", true);
    $('#add_rol').prop("disabled", true);

    $('#EditarFactura').hide(true);

}

function getedit(item) {
    correo_editar = null;
    rfc_editar = null;
    var id_Factura;
    $.get(con + "GeneracionXMLController/ListarById/" + item, function (data, status) {

        if (data !== null) {
            for (var i = 0; i <= data.length - 1; i++) {

                $("#txted_Contratoid").val(data[i].tblContratoId);
                $("#txted_Facturaid").val(data[i].id);

                $("#txted_Lugar_Expedicion").val(data[i].lugarExpedicion);
                $("#txted_Fecha_Emision").val(data[i].fecha);
                $("#txted_Receptor").val(data[i].nombreReceptor);
                $("#txted_RFC_Receptor").val(data[i].rfcReceptor);
                $("#txtedCalle").val(data[i].calleReceptor);
                $("#txtedNuExterior").val(data[i].noExteriorReceptor);
                $("#txtedNuInterior").val(data[i].noInteriorReceptor);
                $("#txtedCP").val(data[i].codigoPostalReceptor);

                $("#txtedColonia").val(data[i].coloniaReceptor);
                $("#txtedMunicipio").val(data[i].municipioReceptor);
                $("#txtedEstado").val(data[i].estadoReceptor);
                $("#txtedPais").val(data[i].paisReceptor);
                //$("#txtedRegimen_Receptor").val(data[i].re);
                $("#txted_Forma_Pago").val(data[i].formaDePago);

                $("#txted_CadenaXML").val(data[i].cadenaXml);

            }
        }

    });
}

function geteditDetalle(item) {
    correo_editar = null;
    rfc_editar = null;
    var id_Factura;
    $.get(con + "GeneracionXMLController/ListarById/" + item, function (data, status) {

        if (data !== null) {
            for (var i = 0; i <= data.length - 1; i++) {

                $("#txted_Contratoid").val(data[i].tblContratoId);
                $("#txted_Facturaid").val(data[i].id);
                $("#txted_Lugar_Expedicion").val(data[i].lugarExpedicion);
                $("#txted_Fecha_Emision").val(data[i].fecha);
                $("#txted_Receptor").val(data[i].nombreReceptor);
                $("#txted_RFC_Receptor").val(data[i].rfcReceptor);
                $("#txtedCalle").val(data[i].calleReceptor);
                $("#txtedNuExterior").val(data[i].noExteriorReceptor);
                $("#txtedNuInterior").val(data[i].noInteriorReceptor);
                $("#txtedCP").val(data[i].codigoPostalReceptor);

                $("#txtedColonia").val(data[i].coloniaReceptor);
                $("#txtedMunicipio").val(data[i].municipioReceptor);
                $("#txtedEstado").val(data[i].estadoReceptor);
                $("#txtedPais").val(data[i].paisReceptor);
                //$("#txtedRegimen_Receptor").val(data[i].re);
                $("#txted_Forma_Pago").val(data[i].formaDePago);

                $("#txted_CadenaXML").val(data[i].cadenaXml);

            }

            getRolesFacturaDetalle(id_Factura);

        }

    });
}


$("#GuardarFactura").click(function () {
    AddFactura();
});
$("#EditarFactura").click(function () {
    UpdateFactura();
});

function AddFactura() {
    var Validacion = false;
    if (Validacion.Bit) {
        ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }
    else {
        var OBJ_Form = tbl_Factura_class;
        OBJ_Form.tbl_contrato_id = $('#ddl_Contrato').val();;
        OBJ_Form.id = "string";
        OBJ_Form.folio = "string";
        OBJ_Form.serie = "string";
        //OBJ_Form.fecha = "string";
        OBJ_Form.formaDePago = "string";
        OBJ_Form.condicionesDePago = "string";
        OBJ_Form.total = 0;
        OBJ_Form.moneda = "string";
        OBJ_Form.subTotal = 0;
        OBJ_Form.metodoDePago = "string";
        OBJ_Form.tipoDeComprobante = "string";
        OBJ_Form.lugarExpedicion = "string";
        OBJ_Form.rfcEmisor = "string";
        OBJ_Form.nombreEmisor = "string";
        OBJ_Form.calleEmisor = "string";
        OBJ_Form.noInteriorEmisor = "string";
        OBJ_Form.noExteriorEmisor = "string";
        OBJ_Form.coloniaEmisor = "string";
        OBJ_Form.municipioEmisor = "string";
        OBJ_Form.estadoEmisor = "string";
        OBJ_Form.paisEmisor = "string";
        OBJ_Form.codigoPostalEmisor = "string";
        OBJ_Form.regimenEmisor = "string";
        OBJ_Form.rfcReceptor = "string";
        OBJ_Form.nombreReceptor = "string";
        OBJ_Form.calleReceptor = "string";
        OBJ_Form.noInteriorReceptor = "string";
        OBJ_Form.noExteriorReceptor = "string";
        OBJ_Form.coloniaReceptor = "string";
        OBJ_Form.municipioReceptor = "string";
        OBJ_Form.estadoReceptor = "string";
        OBJ_Form.paisReceptor = "string";
        OBJ_Form.codigoPostalReceptor = "string";
        $.ajax({
            dataType: 'text',
            cache: false,
            contentType: 'application/json',
            processData: false,
            data: JSON.stringify(OBJ_Form),
            type: 'post',

            success: function (data) {
                var data_b = $.parseJSON(data);
                var objresponse = JSON.parse(data);
                if (!objresponse.Bit) {
                    SuccessSA("Operación exitosa", data_b[0].msg);
                    getFacturasF();
                    $('#ModalAgregarFactura').modal('hide');
                }
                else {
                    ErrorSA("", objresponse.Descripcion);
                }
            },
            error: function () {
                var objresponse = JSON.parse(data);
                ErrorSA('', objresponse.Descripcion)
            },
            url: (con + "GeneracionXMLController/Generar")

        })
    }
}

function UpdateFactura() {
    var Validacion = false;
    if (Validacion.Bit) {
        ErrorSA('Error en los datos de entrada', Validacion.Texto);
    }
    else {
        var OBJ_Form = tbl_Factura_class;
        OBJ_Form.tbl_contrato_id = $('#txted_Contratoid').val();
        OBJ_Form.id = $('#txted_Facturaid').val();
        OBJ_Form.folio = "string";
        OBJ_Form.serie = "string";
        OBJ_Form.fecha = $('#txted_Fecha_Emision').val();
        OBJ_Form.formaDePago = "string";
        OBJ_Form.condicionesDePago = "string";
        OBJ_Form.total = 0;
        OBJ_Form.moneda = "string";
        OBJ_Form.subTotal = 0;
        OBJ_Form.metodoDePago = "string";
        OBJ_Form.tipoDeComprobante = "string";
        OBJ_Form.lugarExpedicion = $('#txted_Lugar_Expedicion').val();
        OBJ_Form.rfcEmisor = "string";
        OBJ_Form.nombreEmisor = "string";
        OBJ_Form.calleEmisor = "string";
        OBJ_Form.noInteriorEmisor = "string";
        OBJ_Form.noExteriorEmisor = "string";
        OBJ_Form.coloniaEmisor = "string";
        OBJ_Form.municipioEmisor = "string";
        OBJ_Form.estadoEmisor = "string";
        OBJ_Form.paisEmisor = "string";
        OBJ_Form.codigoPostalEmisor = "string";
        OBJ_Form.regimenEmisor = "string";
        OBJ_Form.rfcReceptor = "string";
        OBJ_Form.nombreReceptor = "string";
        OBJ_Form.calleReceptor = "string";
        OBJ_Form.noInteriorReceptor = "string";
        OBJ_Form.noExteriorReceptor = "string";
        OBJ_Form.coloniaReceptor = "string";
        OBJ_Form.municipioReceptor = "string";
        OBJ_Form.estadoReceptor = "string";
        OBJ_Form.paisReceptor = "string";
        OBJ_Form.codigoPostalReceptor = "string";
        $.ajax({
            dataType: 'text',
            cache: false,
            contentType: 'application/json',
            processData: false,
            data: JSON.stringify(OBJ_Form),
            type: 'put',

            success: function (data) {
                var data_b = $.parseJSON(data);
                var objresponse = JSON.parse(data);
                if (!objresponse.Bit) {
                    SuccessSA("Operación exitosa", data_b[0].msg);
                    getFacturasF();
                    $('#ModalEditarFactura').modal('hide');
                }
                else {
                    ErrorSA("", objresponse.Descripcion);
                }
            },
            error: function () {
                var objresponse = JSON.parse(data);
                ErrorSA('', objresponse.Descripcion)
            },
            url: (con + "GeneracionXMLController/Actualizar")

        })
    }
}




var tbl_Factura_class = {
    tbl_contrato_id: "string",
    id: "00000000-0000-0000-0000-000000000000",
    folio: "string",
    serie: "string",
    fecha: "2023-08-08T14:04:36.755Z",
    formaDePago: "string",
    condicionesDePago: "string",
    total: 0,
    moneda: "string",
    subTotal: 0,
    metodoDePago: "string",
    tipoDeComprobante: "string",
    lugarExpedicion: "string",
    rfcEmisor: "string",
    nombreEmisor: "string",
    calleEmisor: "string",
    noInteriorEmisor: "string",
    noExteriorEmisor: "string",
    coloniaEmisor: "string",
    municipioEmisor: "string",
    estadoEmisor: "string",
    paisEmisor: "string",
    codigoPostalEmisor: "string",
    regimenEmisor: "string",
    rfcReceptor: "string",
    nombreReceptor: "string",
    calleReceptor: "string",
    noInteriorReceptor: "string",
    noExteriorReceptor: "string",
    coloniaReceptor: "string",
    municipioReceptor: "string",
    estadoReceptor: "string",
    paisReceptor: "string",
    codigoPostalReceptor: "string",
    xml_cadena: "string",
    conceptos_cadena: "string",
    traslados_cadena: "string",
    retenciones_cadena: "string",
    conceptos: [{
        importe: 0,
        valorUnitario: 0,
        descripcion: "string",
        noIdentificacion: "string",
        unidad: "string",
        "cantidad": 0
    }]
}